using Backend;
using System;
using System.Collections.Generic;
using ApplicationUtility;

namespace SatisfactoryCalculator
{
    public class MainViewModel : ViewModel
    {
        public ItemListViewModel ResourcesListViewModel { get; set; }
        public ItemListViewModel ConstructorListViewModel { get; set; }
        public ItemListViewModel AssemblerListViewModel { get; set; }
        public ItemListViewModel ManufacturerListViewModel { get; set; }

        Caller caller;

        static MainViewModel instance;
        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainViewModel();
                
                return instance;
            }
        }

        MainViewModel()
        {
            Init();
        }

        ItemRegistry registry;

        public void Init()
        {
            caller = new Caller();
            ConsoleAllocator.ShowConsoleWindow();

            registry = ItemRegistry.Instance;

            if(!caller.SaveCall(() => registry.InitRegistry(), "Could not initialize ItemRegistry\nApplication will now shut down", SeverityLevel.FATAL))
            {
                return;
            }

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
            ResourcesListViewModel.Refresh();
            ConstructorListViewModel.Refresh();
            AssemblerListViewModel.Refresh();
            ManufacturerListViewModel.Refresh();
        }
    }
}