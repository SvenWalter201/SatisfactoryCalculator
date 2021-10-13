using System.Collections.Generic;
using System;

namespace Backend
{
    public class ComponentInfo
    {
        public ComponentInfo(string path, float amount)
        {
            Path = "Assets/ItemIcons/Resources/" + path.ToLower() + ".png";
            Amount = amount.ToString();
        }

        public string Path { get; set; }
        public string Amount { get; set; }
    }

    public class Item
    {
        public string Type { get; }
        public ComponentRequirement[] Components { get; }
        public float CraftTime { get; }
        public string ImgPath { get; }

        List<ComponentInfo> componentTooltipInfo;
        public List<ComponentInfo> ComponentTooltipInfo { 
            get 
            {
                return componentTooltipInfo;
            } 
        }


        int amount = 0;
        public int Amount 
        {
            get 
            {
                return amount;
            }
            set 
            {
                if(value <= PotentialAmount)
                    amount = value;

                if (AmountInUse > amount)
                    AmountInUse = amount;
            } 
        }
        int potentialAmount = 0;
        public int PotentialAmount { get => potentialAmount; set => potentialAmount = value; }

        int amountInUse = 0;
        public int AmountInUse { get => amountInUse; set 
            {
                amountInUse = value;
                amountFree = amount - amountInUse;
            }  
        }

        int amountFree = 0;
        public int AmountFree { get => amountFree; set => amountFree = value; }

        public Item(string type, ComponentRequirement[] requirements, float craftTime, string imgPath)
        {
            int componentAmount = requirements.Length;
            Type = type;
            Components = requirements;
            CraftTime = craftTime;
            if (componentAmount == 0)
                potentialAmount = 1000;
            else
                potentialAmount = 0;

            ImgPath = imgPath;

            componentTooltipInfo = new List<ComponentInfo>(componentAmount);
            for (int i = 0; i < componentAmount; i++)
            {
                ComponentRequirement req = requirements[i];
                componentTooltipInfo.Add(new ComponentInfo(req.item, req.amount));
            }
        }

        public override string ToString()
        {
            string str = Type;
            str += " Components: {";

            for (int i = 0; i < Components.Length; i++)
            {
                ComponentRequirement req = Components[i];
                str += req.amount.ToString();
                str += "; ";
                str += req.item;
            }

            str += "} ";

            str += "CraftTime: " + CraftTime + " s";

            return str;
        }
    }

    public struct ComponentRequirement
    {
        public string item;
        public float amount;
    }
}
