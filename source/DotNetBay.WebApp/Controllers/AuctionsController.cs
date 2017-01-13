using System.Linq;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.WebApp.ViewModel;
using DotNetBay.SignalR.Hubs;

namespace DotNetBay.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IAuctionService service;
        private readonly IMemberService memberService;

        public AuctionsController(IAuctionService service, IMemberService memberService)
        {
            this.service = service;
            this.memberService = memberService;
        }

        // GET: Auctions
        public ActionResult Index()
        {
            return View(this.service.GetAll().ToList());
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create(NewAuctionViewModel auction)
        {
            if (this.ModelState.IsValid)
            {
                var newAuction = new Auction()
                {
                    Title = auction.Title,
                    Description = auction.Description,
                    StartDateTimeUtc = auction.StartDateTimeUtc,
                    EndDateTimeUtc = auction.EndDateTimeUtc,
                    StartPrice = auction.StartPrice,
                    Seller = this.memberService.GetCurrentMember()
                };

                // Get File Contents
                if (auction.Image != null)
                {
                    byte[] fileContent = new byte[auction.Image.InputStream.Length];
                    auction.Image.InputStream.Read(fileContent, 0, fileContent.Length);

                    newAuction.Image = fileContent;
                }

                this.service.Save(newAuction);

                AuctionsHub.NotifyNewAuction(newAuction);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Image(int auctionId)
        {
            var auction = this.service.GetAll().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return this.HttpNotFound();
            }

            if (auction.Image != null)
            {
                return new FileContentResult(auction.Image, "image/jpg");
            }

            return new EmptyResult();
        }

    }
}
