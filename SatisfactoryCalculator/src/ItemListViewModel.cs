namespace SatisfactoryCalculator
{
    public class ItemListViewModel : ViewModel
    {
        public ItemViewModel[] Items { get; set; }

        public ItemListViewModel(ItemViewModel[] items)
        {
            Items = items;
        }

        public override void Refresh()
        {
            foreach (var model in Items)
                model.Refresh();
        }
    }
}
