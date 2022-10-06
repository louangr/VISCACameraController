using Microsoft.UI.Xaml.Controls;
using VISCACameraController.Core;

namespace VISCACameraController.Views
{
    public sealed partial class ControllerPage : CorePage
    {
        public ControllerPage()
        {
            this.InitializeComponent();
            this.DataContext = App.Current.Services.GetService(typeof(ControllerPageViewModel));
        }

        #region Properties

        public ControllerPageViewModel PageViewModel
            => DataContext as ControllerPageViewModel;

        #endregion Properties

        private void CameraPowerToogleSwitchToggled(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ToggleSwitch ts = sender as ToggleSwitch;
            PageViewModel.CameraPowerToogleSwitchToggled(ts.IsOn);
        }
    }
}
