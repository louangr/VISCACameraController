using System;
using Microsoft.UI.Xaml;
using VISCACameraController.Core;
using VISCACameraController.Views;
using WinRT;

namespace VISCACameraController
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadIcon("icon.ico");
            ContentFrame.Navigate(typeof(ControllerPage));
        }

        #region Privates methods

        private void LoadIcon(string iconName)
        {
            var hwnd = this.As<IWindowNative>().WindowHandle;
            IntPtr hIcon = PInvoke.User32.LoadImage(IntPtr.Zero, iconName, PInvoke.User32.ImageType.IMAGE_ICON, 16, 16, PInvoke.User32.LoadImageFlags.LR_LOADFROMFILE);
            PInvoke.User32.SendMessage(hwnd, PInvoke.User32.WindowMessage.WM_SETICON, (IntPtr)0, hIcon);
        }

        #endregion
    }
}