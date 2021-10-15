using Backend;
using ApplicationUtility;

namespace SatisfactoryCalculator
{
    public class MainViewModel : ViewModel
    {
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

        public Command LoadFileCommand { get; private set; }
        public Command SaveFileCommand { get; private set; }
        public Command SaveFileAsCommand { get; private set; }

        public Command SwitchOperationModeCommand { get; private set; }

        bool operationMode = false;
        public bool OperationMode => operationMode;

        public Command OpenNewWindowCommand { get; private set; }

        public ResourcePotentialViewModel ResourcePotentialViewModel { get; private set; }
        public DependencyCostViewModel DependencyCostViewModel { get; private set; }

        void OpenNewWindow()
        {
            //WindowManager wC = new WindowManager();
            //wC.CreateNewWindow<MainPage>();
        }

        void LoadFile()
        {
            SCLog.INFO("Event 1 fired");
        }

        void SaveFile()
        {
            SCLog.INFO("Event 2 fired");
        }

        void SaveFileAs()
        {
            SCLog.INFO("Event 3 fired");
        }

        void SwitchOperationMode()
        {
            operationMode = !operationMode;
            Refresh();
        }

        public void Init()
        {
            caller = new Caller();
            ConsoleAllocator.ShowConsoleWindow();
            SCLog.TIMER_START();
            LoadFileCommand = new Command(LoadFile);
            SaveFileCommand = new Command(SaveFile);
            SaveFileAsCommand = new Command(SaveFileAs);
            OpenNewWindowCommand = new Command(OpenNewWindow);
            SwitchOperationModeCommand = new Command(SwitchOperationMode);

            registry = ItemRegistry.Instance;

            if(!caller.SaveCall(() => registry.InitRegistry(), "Could not initialize ItemRegistry\nApplication will now shut down", SeverityLevel.CRITICAL))
            {
                return;
            }

            ResourcePotentialViewModel = new ResourcePotentialViewModel();
            DependencyCostViewModel = new DependencyCostViewModel();
            Refresh();
            SCLog.TIMER_STOP("INITIALIZATION");
        }

        public override void Refresh()
        {
            //Notify("ResourcePotentialVisibility");
            //Notify("DependencyCostVisibility");
            ResourcePotentialViewModel.Refresh();
            DependencyCostViewModel.Refresh();
        }
    }
}