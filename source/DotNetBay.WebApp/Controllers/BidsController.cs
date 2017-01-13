﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Interfaces;
using DotNetBay.WebApp.ViewModel;
using DotNetBay.SignalR.Hubs;

namespace DotNetBay.WebApp.Controllers
{
    public class BidsController : Controller
    {
        private IMainRepository mainRepository;

        private IAuctionService service;

        public BidsController(IMainRepository mainRepository, IAuctionService service)
        {
            this.mainRepository = mainRepository;
            this.service = service;
        }

        // GET: Bids
        public ActionResult Create(int auctionId)
        {
            var auction = this.service.GetAll().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return this.HttpNotFound();
            }

            var vm = new NewBidViewModel()
            {
                AuctionId = auctionId,
                AuctionTitle = auction.Title,
                AuctionDescription = auction.Description,
                StartPrice = auction.StartPrice,
                CurrentPrice = auction.CurrentPrice,
                BidAmount = Math.Max(auction.StartPrice, auction.CurrentPrice)
            };

            return View(vm);
        }

        // GET: Bids
        [HttpPost]
        public ActionResult Create(NewBidViewModel bid)
        {
            if (this.ModelState.IsValid)
            {
                var auction = this.service.GetAll().FirstOrDefault(a => a.Id == bid.AuctionId);

                var bidFromDb = this.service.PlaceBid(auction, bid.BidAmount);
                AuctionsHub.NotifyNewBid(auction, bidFromDb);
            }

            return this.RedirectToAction("Index", "Auctions");
        }
    }
}
