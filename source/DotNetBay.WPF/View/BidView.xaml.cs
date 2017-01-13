using System;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.WPF.ViewModel;
using DotNetBay.WPF.Services;
using Microsoft.Practices.Unity;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        public BidView(Auction selectedAuction)
        {
            this.InitializeComponent();

            this.DataContext = WpfUnityContainer.Instance.Resolve<BidViewModel>(new ParameterOverride("auction", selectedAuction));
        }
    }
}
