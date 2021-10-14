using System.Collections.Generic;
using Backend;
using Windows.UI.Xaml;

namespace SatisfactoryCalculator
{
    public class DependencyCostViewModel : ViewModel
    {
        public Visibility DependencyCostVisibility
        {
            get
            {
                return MainViewModel.Instance.OperationMode ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public override void Refresh()
        {
            Notify(nameof(DependencyCostVisibility));
        }
    }
}
