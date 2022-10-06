using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace VISCACameraController.Core
{
    public class CorePage : Page
    {
        #region Properties

        public virtual CorePageViewModel ViewModel
        {
            get => this.DataContext as CorePageViewModel;
            set => this.DataContext = value;
        }

        #endregion Properties

        #region Overridden methods

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel?.LoadState(e.Parameter, e.NavigationMode);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel?.SaveState(e.NavigationMode);
            base.OnNavigatedFrom(e);
        }

        #endregion Overridden methods
    }
}