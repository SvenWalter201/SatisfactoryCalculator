using Windows.UI.Popups;
using Windows.UI.Xaml;
using System;
using Backend;

namespace ApplicationUtility
{
    public class Caller
    {
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
                MessageDisplay.Instance.DisplayExceptionMessage(e, severityLevel, messageOverride);
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

        public bool SaveCall<A, B, C>(Action<A, B, C> func, A arg1, B arg2, C arg3, string messageOverride = "", SeverityLevel severityLevel = SeverityLevel.TRIVIAL)
        {
            try
            {
                func.Invoke(arg1, arg2, arg3);
                return true;
            }
            catch (Exception e)
            {
                MessageDisplay.Instance.DisplayExceptionMessage(e, severityLevel, messageOverride);
                return false;
            }
        }
    }

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
            string msg = (messageOverride != "") ? messageOverride : e.Message;
            SCLog.LOG((int)severityLevel, msg);

            var messageDialog = new MessageDialog("An Error occured:\n" + msg + "\nSeverityLevel: " + severityLevel);

            string dismissalOption;
            switch (severityLevel)
            {
                case SeverityLevel.CRITICAL:
                    dismissalOption = "Quit";
                    break;
                default:
                    dismissalOption = "Close";
                    break;
            }

            messageDialog.Commands.Add(new UICommand(dismissalOption, new UICommandInvokedHandler(CommandInvokedHandler), severityLevel));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 0;
            await messageDialog.ShowAsync();
        }

        void CommandInvokedHandler(IUICommand command)
        {
            switch ((SeverityLevel)command.Id)
            {
                case SeverityLevel.CRITICAL:
                    {
                        Application.Current.Exit();
                        break;
                    }
            }
        }
    }

    public enum SeverityLevel
    {
        TRIVIAL,
        WARN,
        ERROR,
        CRITICAL
    }
    
}

