using System;
using Backend;

namespace SatisfactoryCalculator
{
    public class ItemViewModel : ViewModel
    {
        public string Type 
        { 
            get 
            {
                return Item.Type;
            } 
        }
        public string ConstructionTime 
        { 
            get    
            {
                return Item.CraftTime.ToString();
            } 
        }

        public string Amount { 
            get 
            {
                return Item.Amount.ToString();
            }
            set 
            {
                OnAmountChanged(value);
            } 
        }

        public string AmountDist
        {
            get
            {
                int inUse = Item.AmountInUse;
                int free = Item.AmountFree;
                return inUse + "/" + free;
            }
        }

        public string PotentialAmount
        {
            get
            {
                return Item.PotentialAmount.ToString();
            }
        }

        public string ImgPath
        {
            get
            {
                return Item.ImgPath;
            }
        }

        Item Item { get; }

        public ItemViewModel(Item item)
        {
            Item = item;
        }

        void OnAmountChanged(string value)
        {
            if(int.TryParse(value, out int amount))
            {
                Console.WriteLine("Property {0} has changed", Type);
                ItemRegistry.Instance.CalculatePotentialAmounts(Item, amount);
                MainViewModel.Instance.Refresh();
            }
            else
            {
                Notify("Amount");
                Console.WriteLine("Property {0} couldn't be changed", Type);
            }
        }

        public override void Refresh()
        {
            Notify("Type");
            Notify("ConstructionTime");
            Notify("Amount");
            Notify("AmountDist");
            Notify("PotentialAmount");
            Notify("ImgPath");
        }
    }
}
