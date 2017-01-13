using System;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using DotNetBay.SignalR.Hubs;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace DotNetBay.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IAuctionRunner AuctionRunner { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new WebUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // DotNetBay startup
            var mainRepository = container.Resolve<EFMainRepository>();
            mainRepository.SaveChanges();

            AuctionRunner = container.Resolve<AuctionRunner>(new ParameterOverride("checkInterval", TimeSpan.FromSeconds(5)));
            AuctionRunner.Start();

            AuctionRunner.Auctioneer.AuctionEnded += Auctioneer_AuctionEnded;
            AuctionRunner.Auctioneer.AuctionStarted += Auctioneer_AuctionStarted;
            AuctionRunner.Auctioneer.BidAccepted += Auctioneer_BidAccepted;
        }

        private void Auctioneer_BidAccepted(object sender, ProcessedBidEventArgs e)
        {
            AuctionsHub.NotifyBidAccepted(e.Auction, e.Bid);
        }

        private void Auctioneer_AuctionStarted(object sender, AuctionEventArgs e)
        {
            AuctionsHub.NotifyAuctionStarted(e.Auction);
        }

        private void Auctioneer_AuctionEnded(object sender, AuctionEventArgs e)
        {
            AuctionsHub.NotifyAuctionEnded(e.Auction);
        }
    }
}
