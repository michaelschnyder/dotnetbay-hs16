using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.WPF.Services;
using DotNetBay.WPF.ViewModel;
using Microsoft.Practices.Unity;

namespace DotNetBay.WPF
{
    public class WpfUnityContainer : UnityContainer
    {
        private static WpfUnityContainer instance;

        public WpfUnityContainer()
        {
            // Services
            this.RegisterType<IAuctionService, RemoteAuctionService>();
            this.RegisterType<IAuctioneer, RemoteAuctioneer>();
            this.RegisterType<IMemberService, SimpleMemberService>();

            this.RegisterType<MainViewModel>();
            this.RegisterType<BidViewModel>();
            this.RegisterType<SellViewModel>();
        }

        public static UnityContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WpfUnityContainer();
                }

                return instance;
            }
        }
    }
}
