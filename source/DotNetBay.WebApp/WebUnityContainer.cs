using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using Microsoft.Practices.Unity;

namespace DotNetBay.WebApp
{
    public class WebUnityContainer : UnityContainer
    {
        public WebUnityContainer()
        {
            this.RegisterType<IAuctionService, AuctionService>(new HierarchicalLifetimeManager());
            this.RegisterType<IMemberService, SimpleMemberService>(new HierarchicalLifetimeManager());
            this.RegisterType<IMainRepository, EFMainRepository>(new HierarchicalLifetimeManager());
        }
    }
}