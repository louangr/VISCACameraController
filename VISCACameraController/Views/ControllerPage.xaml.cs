using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using VISCACameraController.Core;

namespace VISCACameraController.Views
{
    public sealed partial class ControllerPage : CorePage
    {
        public ControllerPage()
        {
            this.InitializeComponent();
            this.DataContext = App.Current.Services.GetService(typeof(ControllerPageViewModel));

            Initialize();
        }

        #region Properties

        public ControllerPageViewModel PageViewModel
            => DataContext as ControllerPageViewModel;

        #endregion

        #region Privates methods

        private void Initialize()
        {
            NearManualFocusButton.AddHandler(PointerPressedEvent, new PointerEventHandler(NearManualFocusButtonPointerPressed), true);
            NearManualFocusButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(NearManualFocusButtonPointerReleased), true);

            FarManualFocusButton.AddHandler(PointerPressedEvent, new PointerEventHandler(FarManualFocusButtonPointerPressed), true);
            FarManualFocusButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(FarManualFocusButtonPointerReleased), true);

            ZoomInButton.AddHandler(PointerPressedEvent, new PointerEventHandler(ZoomInButtonPointerPressed), true);
            ZoomInButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(ZoomInButtonPointerReleased), true);

            ZoomOutButton.AddHandler(PointerPressedEvent, new PointerEventHandler(ZoomOutButtonPointerPressed), true);
            ZoomOutButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(ZoomOutButtonPointerReleased), true);
        }

        private void ZoomOutButtonPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            PageViewModel.StopZoomCommand();
        }

        private void ZoomOutButtonPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PageViewModel.ZoomOutCommand();
        }

        private void ZoomInButtonPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            PageViewModel.StopZoomCommand();
        }

        private void ZoomInButtonPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PageViewModel.ZoomInCommand();
        }

        private void FarManualFocusButtonPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            PageViewModel.StopFocusCommand();
        }

        private void FarManualFocusButtonPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PageViewModel.FocusFurtherCommand();
        }

        private void NearManualFocusButtonPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            PageViewModel.StopFocusCommand();
        }

        private void NearManualFocusButtonPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PageViewModel.FocusNearerCommand();
        }

        private void CameraPowerToogleSwitchToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = sender as ToggleSwitch;
            PageViewModel.CameraPowerToogleSwitchToggled(ts.IsOn);
        }
        
        #endregion
    }
}
