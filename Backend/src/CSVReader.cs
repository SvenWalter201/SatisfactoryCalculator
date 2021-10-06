using System;
using System.IO;
using System.Collections.Generic;

namespace Backend
{
    class CSVReader
    {
        const string filePath = "Items.csv";

        public List<Item> ReadItems()
        {
            var file = File.ReadLines(filePath);
            List<string> lines = new List<string>(file);
            List<Item> items = new List<Item>(lines.Count);

            for (int i = 0; i < lines.Count; i++)
            {
                string currentLine = lines[i];

                string[] fragments = currentLine.Split(';');
                string name = fragments[0];
                string componentsAmount = fragments[1];
                string craftTimeStr = fragments[fragments.Length - 1];

                ComponentRequirement[] components = new ComponentRequirement[Convert(componentsAmount)];

                for (int j = 2, index = 0; j < fragments.Length - 2; j += 2, index++)
                {
                    string componentAmount = fragments[j];
                    string componentName = fragments[j + 1];

                    components[index] = new ComponentRequirement
                    {
                        amount = ConvertF(componentAmount),
                        item = componentName
                    };
                }

                float craftTime = ConvertF(craftTimeStr);

                Item item = new Item(name, components, craftTime);
                items.Add(item);
            }

            return items;
        }

        public int Convert(string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch (Exception)
            {
                Console.WriteLine("CSV file invalid");
            }

            return 0;
        }

        public float ConvertF(string input)
        {
            try
            {
                return float.Parse(input);
            }
            catch (Exception)
            {
                Console.WriteLine("CSV file invalid");
            }

            return 0f;
        }
    }
}
