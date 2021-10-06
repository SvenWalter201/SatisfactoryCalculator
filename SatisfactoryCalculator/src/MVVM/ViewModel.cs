using System.ComponentModel;

namespace SatisfactoryCalculator
{
    public abstract class ViewModel : INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void Refresh();
    }
}
