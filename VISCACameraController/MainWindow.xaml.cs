using Microsoft.UI;
using System;
using Microsoft.UI.Xaml;
using VISCACameraController.Views;
using Microsoft.UI.Windowing;
using VISCACameraController.Strings;
using System.Runtime.InteropServices;
using Windows.Graphics;
using WinRT.Interop;

namespace VISCACameraController
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Title = LocalizedStrings.GetString("AppName");

            InitializeDefaultWindowSize();

            ContentFrame.Navigate(typeof(ControllerPage));
        }

        #region Private methods

        private void InitializeDefaultWindowSize()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            var size = new SizeInt32();
            size.Width = 316;
            size.Height = 593;

            appWindow.Resize(size);
        }
        
        #endregion
    }
}