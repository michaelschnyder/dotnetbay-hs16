using System;
using System.Linq;
using System.Web.Http;

using DotNetBay.Core;
using DotNetBay.WebApi.Dtos;
using DotNetBay.Data.Entity;
using DotNetBay.SignalR.Hubs;

namespace DotNetBay.WebApi.Controller
{
    public class BidsController : ApiController
    {
        private readonly IAuctionService auctionService;

    public BidsController(IAuctionService auctionService)
        {
            this.auctionService = auctionService;
        }

        [HttpGet]
        [Route("api/auctions/{auctionId}/bids")]
        public IHttpActionResult GetAllBidsPerAuction(long auctionId)
        {
            var auction = this.auctionService.GetAll().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return this.NotFound();
            }

            var bids = auction.Bids;

            return this.Ok(bids.Select(MapBidToDto));
        }

        [HttpPost]
        [Route("api/auctions/{auctionId}/bids")]
        public IHttpActionResult PlaceBid(long auctionId, BidDto dto)
        {
            var auction = this.auctionService.GetAll().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return this.NotFound();
            }

            try
            {
                var bid = this.auctionService.PlaceBid(auction, dto.Amount);
                AuctionsHub.NotifyNewBid(auction, bid);

                return this.Created(string.Format("api/bids/{0}", bid.TransactionId), MapBidToDto(bid));
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        private static BidDto MapBidToDto(Bid bid)
        {
            return new BidDto()
            {
                Id = bid.Id,
                Amount = bid.Amount,
                Accepted = bid.Accepted,
                BidderName = bid.Bidder.DisplayName,
                ReceivedOnUtc = bid.ReceivedOnUtc,
                TransactionId = bid.TransactionId,
                AuctionTitle = bid.Auction.Title
            };
        }
    }
}