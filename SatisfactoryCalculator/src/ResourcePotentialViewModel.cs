using Backend;
using Windows.UI.Xaml;

namespace SatisfactoryCalculator
{
    public class ResourcePotentialViewModel : ViewModel
    {
        public ItemListViewModel ResourcesListViewModel { get; private set; }
        public ItemListViewModel ConstructorListViewModel { get; private set; }
        public ItemListViewModel AssemblerListViewModel { get; private set; }
        public ItemListViewModel ManufacturerListViewModel { get; private set; }

        ItemRegistry registry;

        public Visibility ResourcePotentialVisibility => MainViewModel.Instance.OperationMode ? Visibility.Collapsed : Visibility.Visible;

        public ResourcePotentialViewModel()
        {
            Init();
        }

        public void Init()
        {
            registry = ItemRegistry.Instance;

            ItemViewModel[] resourceItemViewModels = new ItemViewModel[registry.Resources.Count];
            ItemViewModel[] constructorItemViewModels = new ItemViewModel[registry.ConstructorItems.Count];
            ItemViewModel[] assemblerItemViewModels = new ItemViewModel[registry.AssemblerItems.Count];
            ItemViewModel[] manufacturerItemViewModels = new ItemViewModel[registry.ManufacturerItems.Count];

            for (int i = 0; i < registry.Resources.Count; i++)
                resourceItemViewModels[i] = new ItemViewModel(registry.Resources[i]);

            for (int i = 0; i < registry.ConstructorItems.Count; i++)
                constructorItemViewModels[i] = new ItemViewModel(registry.ConstructorItems[i]);

            for (int i = 0; i < registry.AssemblerItems.Count; i++)
                assemblerItemViewModels[i] = new ItemViewModel(registry.AssemblerItems[i]);

            for (int i = 0; i < registry.ManufacturerItems.Count; i++)
                manufacturerItemViewModels[i] = new ItemViewModel(registry.ManufacturerItems[i]);

            ResourcesListViewModel = new ItemListViewModel(resourceItemViewModels);
            ConstructorListViewModel = new ItemListViewModel(constructorItemViewModels);
            AssemblerListViewModel = new ItemListViewModel(assemblerItemViewModels);
            ManufacturerListViewModel = new ItemListViewModel(manufacturerItemViewModels);
        }

        public override void Refresh()
        {
            Notify(nameof(ResourcePotentialVisibility));
            ResourcesListViewModel.Refresh();
            ConstructorListViewModel.Refresh();
            AssemblerListViewModel.Refresh();
            ManufacturerListViewModel.Refresh();
        }
    }
}
