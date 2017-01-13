using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.WPF.ViewModel;
using Microsoft.Win32;
using DotNetBay.WPF.Services;
using Microsoft.Practices.Unity;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {

        public SellView()
        {
            this.InitializeComponent();

            this.DataContext = WpfUnityContainer.Instance.Resolve<SellViewModel>();
        }
    }
}
