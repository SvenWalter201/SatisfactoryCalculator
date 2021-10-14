using System;
using System.Collections.Generic;
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

        public List<ComponentInfo> ComponentTootipInfo 
        {
            get
            {
                return Item.ComponentTooltipInfo;
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
                SCLog.INFO("Property {0} has changed", Type);
                ItemRegistry.Instance.CalculatePotentialAmounts(Item, amount);
                MainViewModel.Instance.Refresh();
            }
            else
            {
                Notify(nameof(Amount));
                SCLog.WARN("Property {0} couldn't be changed", Type);
            }
        }

        public override void Refresh()
        {
            Notify(nameof(Type));
            Notify(nameof(ConstructionTime));
            Notify(nameof(Amount));
            Notify(nameof(AmountDist));
            Notify(nameof(PotentialAmount));
            Notify(nameof(ImgPath));
        }
    }
}
