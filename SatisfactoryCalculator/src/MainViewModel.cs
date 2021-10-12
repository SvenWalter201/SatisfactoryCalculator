using Backend;
using System;
using System.Collections.Generic;

namespace SatisfactoryCalculator
{
    public class MainViewModel : ViewModel
    {
        public ItemListViewModel ResourcesListViewModel { get; set; }
        public ItemListViewModel ConstructorListViewModel { get; set; }
        public ItemListViewModel AssemblerListViewModel { get; set; }
        public ItemListViewModel ManufacturerListViewModel { get; set; }

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

        public bool SaveCall(Action func, string messageOverride = "", SeverityLevel severityLevel = SeverityLevel.TRIVIAL)
        {
            try
            {
                func.Invoke();
                return true;
            }
            catch (Exception e)
            {
                MessageDisplay.Instance.DisplayExceptionMessage(e, severityLevel, messageOverride);
                return false;
            }
        }

        public bool SaveCall<A>(Action<A> func, A arg1, string messageOverride = "", SeverityLevel severityLevel = SeverityLevel.TRIVIAL)
        {
            try
            {
                func.Invoke(arg1);
                return true;
            }
            catch (Exception e)
            {
                MessageDisplay.Instance.DisplayExceptionMessage(e, SeverityLevel.TRIVIAL);
                return false;
            }
        }

        public bool SaveCall<A, B>(Action<A, B> func, A arg1, B arg2, string messageOverride = "", SeverityLevel severityLevel = SeverityLevel.TRIVIAL)
        {
            try
            {
                func.Invoke(arg1, arg2);
                return true;
            }
            catch (Exception e)
            {
                MessageDisplay.Instance.DisplayExceptionMessage(e, severityLevel, messageOverride);
                return false;
            }
        }

        public bool SaveCall<A,B,C>(Action<A,B,C> func, A arg1, B arg2, C arg3, string messageOverride = "", SeverityLevel severityLevel = SeverityLevel.TRIVIAL)
        {
            try
            {
                func.Invoke(arg1, arg2, arg3);
                return true;
            }
            catch(Exception e)
            {
                MessageDisplay.Instance.DisplayExceptionMessage(e, severityLevel, messageOverride);
                return false;
            }
        }

        public void Init()
        {
            ConsoleAllocator.ShowConsoleWindow();

            registry = ItemRegistry.Instance;

            if(!SaveCall(() => registry.InitRegistry(), "Could not initialize ItemRegistry\nApplication will now shut down", SeverityLevel.FATAL))
            {
                return;
            }
            //registry.PrintRegistry();

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