using Backend;
using Windows.UI.Xaml;

namespace SatisfactoryCalculator
{
    public class DependencyCostViewModel : ViewModel
    {
        public Visibility DependencyCostVisibility => 
                MainViewModel.Instance.OperationMode ? Visibility.Visible : Visibility.Collapsed;

        public ItemDependencyViewModel ItemDependencyViewModel { get; private set; }

        public DependencyCostViewModel()
        {
            Item heavyModFrame = ItemRegistry.Instance.ManufacturerItems[0];
            ItemDependencyViewModel = new ItemDependencyViewModel(heavyModFrame, 200, 200, 100);
        }

        public void SelectItem()
        {

        }

        public override void Refresh()
        {
            Notify(nameof(DependencyCostVisibility));
            Notify(nameof(ItemDependencyViewModel));
            ItemDependencyViewModel.Refresh();
        }
    }
}
