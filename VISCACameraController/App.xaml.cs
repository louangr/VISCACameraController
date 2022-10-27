using System;
using Microsoft.UI.Xaml;
using VISCACameraController.Core;
using VISCACameraController.Strings;
using WinRT;

namespace VISCACameraController
{
    public partial class App : Application
    {
        private Window m_window;
        private IntPtr m_windowHandle;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Services = IoCInitializer.ConfigureServices();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        public IntPtr WindowHandle { get { return m_windowHandle; } }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            var windowNative = m_window.As<IWindowNative>();
            m_windowHandle = windowNative.WindowHandle;
            m_window.Title = LocalizedStrings.GetString("AppName");
            m_window.Activate();

            SetWindowSize(m_windowHandle, 316, 604);
        }

        private void SetWindowSize(IntPtr hwnd, int width, int height)
        {
            var dpi = PInvoke.User32.GetDpiForWindow(hwnd);
            float scalingFactor = (float)dpi / 96;
            width = (int)(width * scalingFactor);
            height = (int)(height * scalingFactor);

            PInvoke.User32.SetWindowPos(hwnd, PInvoke.User32.SpecialWindowHandles.HWND_TOP, 0, 0, width, height, PInvoke.User32.SetWindowPosFlags.SWP_NOMOVE);
        }
    }
}
