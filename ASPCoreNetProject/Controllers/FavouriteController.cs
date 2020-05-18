using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreNetProject.Models;
using ASPCoreNetProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreNetProject.Controllers
{
    [Authorize]
    public class FavouriteController : Controller
    {
        private readonly IFavouriteBase<Favourite> favouriteservice;
        private readonly UserManager<ApplicationUser> manager;
        public FavouriteController(IFavouriteBase<Favourite> favouriteservice, UserManager<ApplicationUser> manager)
        {
            this.manager = manager;
            this.favouriteservice = favouriteservice;
        }
        // GET: Favourite
        public ActionResult AllFavourite()
        {
            if (!ModelState.IsValid)
                return Content("Not valid");
            else
                try
                {
                    string UserID = manager.GetUserId(HttpContext.User);
                    return View(favouriteservice.GetAll(UserID));
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
        }
        // POST: Favourite/Create
        [HttpPost]
       
        public ActionResult Create(int artid)
        {
            string UserID = manager.GetUserId(HttpContext.User);
            if (!ModelState.IsValid)
            {
                return Content("Error Occured");
            }
            try
            {
                favouriteservice.Add(artid, UserID);
                return Content("Addition sucessed");
            }
            catch (Exception ex)
            {
                //  ModelState.AddModelError("", ex.Message);
                return Content("Exception occured " + ex.Message);
            }

        }
        //public ActionResult Delete()
        //{
        //    return View();
        //}

        // POST: Favourite/Delete/5
      [HttpPost]
        public ActionResult Delete(int artid)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                string UserID = manager.GetUserId(HttpContext.User);
                favouriteservice.Delete(artid, UserID);
                return Content("Delete Succeded");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}