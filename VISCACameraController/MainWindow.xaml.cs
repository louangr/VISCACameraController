using Microsoft.UI.Xaml;
using VISCACameraController.Views;

namespace VISCACameraController
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentFrame.Navigate(typeof(ControllerPage));
        }
    }
}