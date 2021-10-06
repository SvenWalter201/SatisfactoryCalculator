using System;
using System.Windows.Input;


namespace SatisfactoryCalculator
{
    public class Command : ICommand
    {
        private readonly Action execute;

        public event EventHandler CanExecuteChanged;

        public Command(Action execute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}
