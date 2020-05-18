using ASPCoreNetProject.Models;
using ASPCoreNetProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASPCoreNetProject.ViewComponents
{
    public class RateViewComponent:ViewComponent
    {
        private readonly IRateBase<Rate> rateService;
        private readonly UserManager<ApplicationUser> manager;

        public RateViewComponent(IRateBase<Rate> RateService,UserManager<ApplicationUser>manager)
        {
            rateService = RateService;
            this.manager = manager;
        }
        public IViewComponentResult Invoke(int id)
        {
            ViewBag.ValueRate = Math.Round(rateService.GetVote(id), 2);
            var UserID = manager.GetUserId(HttpContext.User);
            ViewBag.ArtId = id;
            ViewBag.UserRate = rateService.GetVoteByUser(UserID, id);
            return View("_RatePartialView");
        }
    }
}
