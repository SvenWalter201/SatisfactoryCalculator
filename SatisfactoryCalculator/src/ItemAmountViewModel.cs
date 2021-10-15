using Backend;

namespace SatisfactoryCalculator
{
    public class ItemAmountViewModel : ViewModel
    {
        public string ImagePath
        {
            get
            {
                return Item.ImgPath;
            }
        }

        public float Amount { get; }

        public Item Item { get; }

        public ItemAmountViewModel(Item item, float amount)
        {
            Item = item;
            Amount = amount;
        }

        public override void Refresh()
        {
            Notify(nameof(ImagePath));
            Notify(nameof(Amount));
        }
    }
}
