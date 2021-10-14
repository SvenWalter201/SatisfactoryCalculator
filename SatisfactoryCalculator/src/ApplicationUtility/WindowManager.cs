using System;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace ApplicationUtility
{
    public class WindowManager<T> where T : UserControl
    {
        AppWindow appWindow;
        Frame appWindowContentFrame;
        public async void CreateNewWindow()
        {
            appWindow = await AppWindow.TryCreateAsync();
            appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(T));

            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);

            await appWindow.TryShowAsync();

            appWindow.Closed += OnWindowClosed;
        }

        public async void DestroyWindow()
        {
            await appWindow.CloseAsync();
        }

        void OnWindowClosed(AppWindow w , AppWindowClosedEventArgs args)
        {
            appWindowContentFrame.Content = null;
            appWindow = null;
        }
    }
}
