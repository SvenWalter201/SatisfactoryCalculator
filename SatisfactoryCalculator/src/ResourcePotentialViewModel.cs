using System.Collections.Generic;
using Backend;
using System;
using Windows.UI.Xaml;

namespace SatisfactoryCalculator
{
    public class ResourcePotentialViewModel : ViewModel
    {
        public ItemListViewModel ResourcesListViewModel { get; set; }
        public ItemListViewModel ConstructorListViewModel { get; set; }
        public ItemListViewModel AssemblerListViewModel { get; set; }
        public ItemListViewModel ManufacturerListViewModel { get; set; }

        ItemRegistry registry;

        public Visibility ResourcePotentialVisibility
        {
            get
            {
                return MainViewModel.Instance.OperationMode ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public ResourcePotentialViewModel()
        {
            Init();
        }

        public void Init()
        {
            registry = ItemRegistry.Instance;

            List<ItemViewModel> resourceItemViewModels = new List<ItemViewModel>(registry.Resources.Count);
            List<ItemViewModel> constructorItemViewModels = new List<ItemViewModel>(registry.ConstructorItems.Count);
            List<ItemViewModel> assemblerItemViewModels = new List<ItemViewModel>(registry.AssemblerItems.Count);
            List<ItemViewModel> manufacturerItemViewModels = new List<ItemViewModel>(registry.ManufacturerItems.Count);

            for (int i = 0; i < registry.Resources.Count; i++)
                resourceItemViewModels.Add(new ItemViewModel(registry.Resources[i]));

            for (int i = 0; i < registry.ConstructorItems.Count; i++)
                constructorItemViewModels.Add(new ItemViewModel(registry.ConstructorItems[i]));

            for (int i = 0; i < registry.AssemblerItems.Count; i++)
                assemblerItemViewModels.Add(new ItemViewModel(registry.AssemblerItems[i]));

            for (int i = 0; i < registry.ManufacturerItems.Count; i++)
                manufacturerItemViewModels.Add(new ItemViewModel(registry.ManufacturerItems[i]));

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
