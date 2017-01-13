using System.Windows;
using DotNetBay.WPF.ViewModel;
using DotNetBay.WPF.Services;
using Microsoft.Practices.Unity;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var app = Application.Current as App;

            InitializeComponent();

            this.DataContext = WpfUnityContainer.Instance.Resolve<MainViewModel>();
        }
    }
}
