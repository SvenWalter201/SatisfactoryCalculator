using System;
using System.Collections.Generic;

namespace Backend
{
    public class ItemRegistry
    {
        public List<Item> Items { get; private set; }

        public List<Item> Resources { get; private set; }
        public List<Item> ConstructorItems { get; private set; }
        public List<Item> AssemblerItems { get; private set; }
        public List<Item> ManufacturerItems { get; private set; }

        static ItemRegistry instance;
        public static ItemRegistry Instance
        {
            get
            {
                if (instance == null)
                    instance = new ItemRegistry();

                return instance;
            }
        }

        ItemRegistry()
        {
        }

        public void InitRegistry()
        {
            CSVReader reader = new CSVReader();

            try
            {
                Items = reader.ReadItems();
            }
            catch (Exception e)
            {
                Console.WriteLine("T2");
                throw e;
            }

            SortItemsIntoLists();
            //CalculatePotentialAmounts();
        }

        void Reduce(string cmpr)
        {
            Reduce(ConstructorItems, cmpr);
            Reduce(AssemblerItems, cmpr);
            Reduce(ManufacturerItems, cmpr);
        }

        void Reduce(List<Item> itemList, string cmpr)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Item it = itemList[i];
                for (int j = 0; j < it.Components.Length; j++)
                {
                    if (it.Components[j].item == cmpr)
                    {
                        it.Amount = 0;
                        Reduce(it.Type);
                    }
                }
            }
        }

        public void CalculatePotentialAmounts(Item item, int newAmount)
        {
            if (newAmount < item.Amount)
                Reduce(item.Type);
            

            item.Amount = newAmount;

            float[] usedItems = new float[Items.Count];
            for (int i = 0; i < usedItems.Length; i++)
                usedItems[i] = 0f;

            SumUsedResources(ConstructorItems, ref usedItems);
            SumUsedResources(AssemblerItems, ref usedItems);
            SumUsedResources(ManufacturerItems, ref usedItems);

            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].AmountInUse = (int)usedItems[i];
                //Console.WriteLine("{0} has {1} in use", Items[i].Type, Items[i].AmountInUse);
            }

            DeterminePotentialAmount(ConstructorItems, usedItems);
            DeterminePotentialAmount(AssemblerItems, usedItems);
            DeterminePotentialAmount(ManufacturerItems, usedItems);
        }

        void DeterminePotentialAmount(List<Item> itemList, float[] usedItems)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Item item = itemList[i];
                int componentAmount = item.Components.Length;
                int[] maximumAmounts = new int[componentAmount];
                for (int j = 0; j < componentAmount; j++)
                {
                    ComponentRequirement req = item.Components[j];
                    int index = GetItemIndexByName(req.item);
                    float remainingResource = Items[index].Amount - usedItems[index];
                    maximumAmounts[j] = (int)(remainingResource / req.amount);
                }

                int smallest = maximumAmounts[0];
                for (int j = 1; j < componentAmount; j++)
                {
                    if (maximumAmounts[j] < smallest)
                        smallest = maximumAmounts[j];
                }

                item.PotentialAmount = smallest + item.Amount;
            }
        }

        void SumUsedResources(List<Item> itemList, ref float[] usedItems)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Item item = itemList[i];
                int amount = item.Amount;

                for (int j = 0; j < item.Components.Length; j++)
                {
                    ComponentRequirement req = item.Components[j];
                    int index = GetItemIndexByName(req.item);
                    float totalRequiredAmount = req.amount * amount;
                    usedItems[index] += totalRequiredAmount;
                }
            }
        }

        int GetItemIndexByName(string name)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Type == name)
                    return i;
            }

            Console.WriteLine("The Item {1} does not exist", name);
            return -1;
        }

        public void SortItemsIntoLists()
        {
            Resources = new List<Item>();
            ConstructorItems = new List<Item>();
            AssemblerItems = new List<Item>();
            ManufacturerItems = new List<Item>();

            foreach (var item in Items)
            {
                int componentAmount = item.Components.Length;
                switch (componentAmount)
                {
                    case 0:
                        Resources.Add(item);
                        break;
                    case 1:
                        ConstructorItems.Add(item);
                        break;
                    case 2:
                        AssemblerItems.Add(item);
                        break;
                    case 3:
                    case 4:
                        ManufacturerItems.Add(item);
                        break;
                }
            }
        }

        public void PrintRegistry()
        {
            Console.WriteLine("Registry: \n ---");

            for (int i = 0; i < Items.Count; i++)
                Console.WriteLine(Items[i].ToString());
            
        }
    }
}
