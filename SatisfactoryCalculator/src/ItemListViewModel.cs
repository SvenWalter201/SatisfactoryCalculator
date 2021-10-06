using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SatisfactoryCalculator
{
    public class ItemListViewModel : ViewModel
    {
        public ObservableCollection<ItemViewModel> Items { get; set; }

        public ItemListViewModel(List<ItemViewModel> items)
        {
            Items = new ObservableCollection<ItemViewModel>(items);
        }

        public override void Refresh()
        {
            foreach (var model in Items)
                model.Refresh();
        }
    }
}
