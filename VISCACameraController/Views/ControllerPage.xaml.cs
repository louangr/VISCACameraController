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

            TopTiltButton.AddHandler(PointerPressedEvent, new PointerEventHandler(TopTiltButtonPointerPressed), true);
            TopTiltButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(TopTiltButtonPointerReleased), true);

            LeftPanButton.AddHandler(PointerPressedEvent, new PointerEventHandler(LeftPanButtonPointerPressed), true);
            LeftPanButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(LeftPanButtonPointerReleased), true);

            RightPanButton.AddHandler(PointerPressedEvent, new PointerEventHandler(RightPanButtonPointerPressed), true);
            RightPanButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(RightPanButtonPointerReleased), true);

            BottomTiltButton.AddHandler(PointerPressedEvent, new PointerEventHandler(BottomTiltButtonPointerPressed), true);
            BottomTiltButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(BottomTiltButtonPointerReleased), true);
        }

        private void BottomTiltButtonPointerReleased(object sender, PointerRoutedEventArgs e) => PageViewModel.StopTiltPanMoveCommand();

        private void BottomTiltButtonPointerPressed(object sender, PointerRoutedEventArgs e) => PageViewModel.MoveBottomTiltCommand();

        private void RightPanButtonPointerReleased(object sender, PointerRoutedEventArgs e) => PageViewModel.StopTiltPanMoveCommand();

        private void RightPanButtonPointerPressed(object sender, PointerRoutedEventArgs e) => PageViewModel.MoveRightPanCommand();

        private void LeftPanButtonPointerReleased(object sender, PointerRoutedEventArgs e) => PageViewModel.StopTiltPanMoveCommand();

        private void LeftPanButtonPointerPressed(object sender, PointerRoutedEventArgs e) => PageViewModel.MoveLeftPanCommand();

        private void TopTiltButtonPointerReleased(object sender, PointerRoutedEventArgs e) => PageViewModel.StopTiltPanMoveCommand();

        private void TopTiltButtonPointerPressed(object sender, PointerRoutedEventArgs e) => PageViewModel.MoveTopTiltCommand();

        private void ZoomOutButtonPointerReleased(object sender, PointerRoutedEventArgs e) => PageViewModel.StopZoomCommand();

        private void ZoomOutButtonPointerPressed(object sender, PointerRoutedEventArgs e) => PageViewModel.ZoomOutCommand();

        private void ZoomInButtonPointerReleased(object sender, PointerRoutedEventArgs e) => PageViewModel.StopZoomCommand();

        private void ZoomInButtonPointerPressed(object sender, PointerRoutedEventArgs e) => PageViewModel.ZoomInCommand();

        private void FarManualFocusButtonPointerReleased(object sender, PointerRoutedEventArgs e) => PageViewModel.StopFocusCommand();

        private void FarManualFocusButtonPointerPressed(object sender, PointerRoutedEventArgs e) => PageViewModel.FocusFurtherCommand();

        private void NearManualFocusButtonPointerReleased(object sender, PointerRoutedEventArgs e) => PageViewModel.StopFocusCommand();

        private void NearManualFocusButtonPointerPressed(object sender, PointerRoutedEventArgs e) => PageViewModel.FocusNearerCommand();

        private void CameraPowerToogleSwitchToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = sender as ToggleSwitch;
            PageViewModel.CameraPowerToogleSwitchToggled(ts.IsOn);
        }

        private void Preset8ButtonDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (!PageViewModel.IsConnected)
            {
                PageViewModel.IsHiddenSetPresetButtonEnabled = !PageViewModel.IsHiddenSetPresetButtonEnabled;
            }
        }

        #endregion
    }
}
