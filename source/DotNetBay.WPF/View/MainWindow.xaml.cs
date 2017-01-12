using System.Windows;
using DotNetBay.WPF.ViewModel;
using DotNetBay.WPF.Services;

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

            var auctionService = new RemoteAuctionService();
            var auctioneer = new RemoteAuctioneer(auctionService);

            this.DataContext = new MainViewModel(auctioneer, auctionService);
        }
    }
}
