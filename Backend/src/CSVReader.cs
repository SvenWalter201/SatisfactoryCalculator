using System;
using System.IO;
using System.Collections.Generic;

namespace Backend
{


    public class CSVReader
    {
        const string itemsFilePath = "Items.csv";
        const string imgPathPrefix = "Assets/ItemIcons/";

        int itemsCount = 0;

        public List<Item> ReadItems()
        {
            var file = File.ReadLines(itemsFilePath);
            List<string> lines = new List<string>(file);
            itemsCount = lines.Count;
            List<Item> items = new List<Item>(itemsCount);

            for (int i = 0; i < itemsCount; i++)
            {
                string currentLine = lines[i];

                string[] fragments = currentLine.Split(';');
                string name = fragments[0];
                string componentsAmount = fragments[1];
                string craftTimeStr = fragments[fragments.Length - 2];
                string imgPath = imgPathPrefix + fragments[fragments.Length - 1];
                
                //Console.WriteLine(imgPath);
                ComponentRequirement[] components = new ComponentRequirement[Convert(componentsAmount)];

                for (int j = 2, index = 0; j < fragments.Length - 3; j += 2, index++)
                {
                    string componentAmount = fragments[j];
                    string componentName = fragments[j + 1];

                    try
                    {
                        components[index] = new ComponentRequirement
                        {
                            amount = ConvertF(componentAmount),
                            item = componentName
                        };
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                }

                try
                {
                    float craftTime = ConvertF(craftTimeStr);

                    Item item = new Item(name, components, craftTime, imgPath);
                    items.Add(item);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

            return items;
        }

        public int Convert(string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public float ConvertF(string input)
        {
            try
            {
                return float.Parse(input);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void SaveAmounts(List<Item> items)
        {
            
        }

        public int[] LoadAmounts(string saveFileName)
        {
            string amounts = File.ReadAllText(saveFileName);
            string[] amountsSeperated = amounts.Split(';');

            if(amountsSeperated.Length != itemsCount)
            {
                Console.WriteLine("Can not load Save File, because Item Setup was changed");
                return null;
            }

            int[] itemAmounts = new int[itemsCount];

            for (int i = 0; i < itemsCount; i++)
                itemAmounts[i] = Convert(amountsSeperated[i]);
            

            return itemAmounts;
        }
    }
}
