using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPCoreNetProject.Models;
using ASPCoreNetProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreNetProject.Controllers
{
    [Authorize]
    public class RateController : Controller
    {
        private readonly IRateBase<Rate> RateService;
        public RateController(IRateBase<Rate> RateService)
        {
            this.RateService = RateService;
        }

        [HttpPost]
        public IActionResult New(int ArtID, double rate)
        {
            Rate r = new Rate() { Vote = rate, ArtID = ArtID };
            ViewBag.ValueRate = RateService.GetVote(ArtID);
            r.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserRate = RateService.GetVoteByUser(r.UserID, ArtID);


            try
            {
                ViewBag.ArtId = ArtID;
                RateService.Add(r);
                ViewBag.ValueRate = RateService.GetVote(ArtID);
                ViewBag.UserRate = RateService.GetVoteByUser(r.UserID, ArtID);
                return Content(Math.Round(ViewBag.ValueRate, 2) + " / 5");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Content("Error " + ex.Message);
            }
        }
    }
}