using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;

namespace SatisfactoryCalculator
{
    public class MessageDisplay
    {
        static MessageDisplay instance;
        public static MessageDisplay Instance
        {
            get
            {
                if (instance == null)
                    instance = new MessageDisplay();

                return instance;
            }
        }

        MessageDisplay()
        {
        }

        public async void DisplayExceptionMessage(Exception e, SeverityLevel severityLevel, string messageOverride = "")
        {
            // Create the message dialog and set its content
            string msg = (messageOverride != "") ? messageOverride : e.Message;
            var messageDialog = new MessageDialog("An Error occured:\n" + msg + "\nSeverityLevel: " + severityLevel);

            string label = "Close";
            switch (severityLevel)
            {
                case SeverityLevel.FATAL:
                    label = "Quit";
                    break;
            }
            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(label, new UICommandInvokedHandler(CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        void CommandInvokedHandler(IUICommand command)
        {
            switch (command.Label)
            {
                case "Quit":
                    {
                        Application.Current.Exit();
                        break;
                    }
            }
            /*
            // Display message showing the label of the command that was invoked
            rootPage.NotifyUser("The '" + command.Label + "' command has been selected.",
                NotifyType.StatusMessage);
            */
        }
    }

    public enum SeverityLevel
    {
        TRIVIAL,
        FATAL
    }
    
}

