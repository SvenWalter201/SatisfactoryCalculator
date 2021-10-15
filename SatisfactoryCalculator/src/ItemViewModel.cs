using Backend;

namespace SatisfactoryCalculator
{
    public class ItemViewModel : ViewModel
    {
        public string Type => Item.Type;
        public string ConstructionTime => Item.CraftTime.ToString();

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

        public string PotentialAmount => Item.PotentialAmount.ToString();

        public string ImgPath => Item.ImgPath;

        public ItemAmountViewModel[] ComponentTootipInfo { get; }

        Item Item { get; }

        public ItemViewModel(Item item)
        {
            Item = item;

            //setup tooltip information
            ComponentRequirement[] reqs = item.Components;
            ComponentTootipInfo = new ItemAmountViewModel[reqs.Length];
            for (int i = 0; i < reqs.Length; i++)
            {
                ComponentRequirement req = reqs[i];
                Item it = ItemRegistry.Instance.GetItemByName(req.item);
                float reqAmount = req.amount;
                ComponentTootipInfo[i] = new ItemAmountViewModel(it, reqAmount);
            }
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
