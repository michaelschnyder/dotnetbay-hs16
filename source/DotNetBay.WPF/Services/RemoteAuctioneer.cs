using DotNetBay.Core.Execution;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace DotNetBay.WPF.Services
{
    public class RemoteAuctioneer : IAuctioneer
    {
        private readonly RemoteAuctionService auctionService;
        private Uri baseAddress;

        public event EventHandler<AuctionEventArgs> AuctionEnded;
        public event EventHandler<AuctionEventArgs> AuctionStarted;
        public event EventHandler<ProcessedBidEventArgs> BidAccepted;
        public event EventHandler<ProcessedBidEventArgs> BidDeclined;

        public RemoteAuctioneer(RemoteAuctionService auctionService)
        {
            this.auctionService = auctionService;
            this.baseAddress = new Uri("http://localhost:52287/");

            Task.Run(this.Connect);
        }

        private async Task Connect()
        {
            var hubConnection = new HubConnection(this.baseAddress.ToString());
            hubConnection.TraceLevel = TraceLevels.All;
            hubConnection.TraceWriter = Console.Out;
            
            var hubProxy = hubConnection.CreateHubProxy("AuctionsHub");

            // Unfortunately, there is no event for a new auction and and neither this or the RemoteAuctionService has an event for it. Therefore only wire the related events
            hubProxy.On<long, string, decimal, string>("bidAccepted", this.RemoteBidAccepted);
            hubProxy.On<long, string>("auctionStarted", this.RemoteAuctionStarted);
            hubProxy.On<long, string>("auctionEnded", this.RemoteAuctionEnded);

            await hubConnection.Start();
        }

        private void RemoteBidAccepted(long auctionId, string auctionTitle, decimal amount, string bidder)
        {
            var auction = this.auctionService.GetAll().Single(a => a.Id == auctionId);

            this.OnBidAccepted(new ProcessedBidEventArgs() { Auction = auction, Bid = auction.ActiveBid });
        }

        private void RemoteAuctionStarted(long auctionId, string auctionTitle)
        {
            var auction = this.auctionService.GetAll().Single(a => a.Id == auctionId);

            this.OnAuctionStarted(new AuctionEventArgs() { Auction = auction });
        }
        private void RemoteAuctionEnded(long auctionId, string auctionTitle)
        {
            var auction = this.auctionService.GetAll().Single(a => a.Id == auctionId);

            this.OnAuctionEnded(new AuctionEventArgs() { Auction = auction });
        }

        protected virtual void OnBidDeclined(ProcessedBidEventArgs e)
        {
            BidDeclined?.Invoke(this, e);
        }

        protected virtual void OnBidAccepted(ProcessedBidEventArgs e)
        {
            BidAccepted?.Invoke(this, e);
        }

        protected virtual void OnAuctionStarted(AuctionEventArgs e)
        {
            AuctionStarted?.Invoke(this, e);
        }

        protected virtual void OnAuctionEnded(AuctionEventArgs e)
        {
            AuctionEnded?.Invoke(this, e);
        }
    }
}
