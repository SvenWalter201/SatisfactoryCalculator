using Backend;

namespace SatisfactoryCalculator
{
    public class ItemDependencyViewModel : ViewModel
    {
        public ItemDependencyViewModel[] ComponentViewModels { get; set; }

        public ItemAmountViewModel ItemAmountViewModel { get; }

        public int Width { get; }
        public int Height { get; }

        public Item Item { get; }

        public ItemDependencyViewModel(Item item, int remainingWidth, int remainingHeight, float amount)
        {
            Width = remainingWidth;
            Height = remainingHeight;
            ItemAmountViewModel = new ItemAmountViewModel(item, amount);

            ComponentRequirement[] componentInfos = item.Components;
            int componentsAmount = componentInfos.Length;

            ItemRegistry reg = ItemRegistry.Instance;

            ComponentViewModels = new ItemDependencyViewModel[componentsAmount];
            for (int i = 0; i < componentsAmount; i++)
            {
                ComponentRequirement req = componentInfos[i];
                Item it = reg.GetItemByName(req.item);
                float reqAmount = req.amount * amount;
                ComponentViewModels[i] = new ItemDependencyViewModel(it, Width, Height, reqAmount);
            }
        
        }

        public override void Refresh()
        {
            Notify(nameof(Width));
            Notify(nameof(Height));
            Notify(nameof(ItemAmountViewModel));
        }
    }
}
