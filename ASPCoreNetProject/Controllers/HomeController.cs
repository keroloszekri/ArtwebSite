using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASPCoreNetProject.Models;
using ASPCoreNetProject.Services;
using ASPCoreNetProject.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ASPCoreNetProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArtBase<Art> service;
        private readonly IFavouriteBase<Favourite> favouriteService;
        private readonly UserManager<ApplicationUser> manager;

        public HomeController(ILogger<HomeController> logger, IArtBase<Art> service, IFavouriteBase<Favourite> FavouriteService, UserManager<ApplicationUser> manager)
        {
            _logger = logger;
            this.service = service;
            favouriteService = FavouriteService;
            this.manager = manager;
        }

        public IActionResult Index()
        {
            List<ShowArtViewModel> showArtViewModel = new List<ShowArtViewModel>();
            var Arts = service.GetAll();
            if (Arts != null)
            {
                List<Art> Favourites;
                string UserID = manager.GetUserId(HttpContext.User);
                if (UserID == null)
                {
                    Favourites = null;
                }
                else
                {
                    Favourites = favouriteService.GetAll(UserID);
                }
                for (int i = 0; i < Arts.Count; i++)
                {

                    ShowArtViewModel sh = new ShowArtViewModel();
                    sh.Name = Arts[i].Name;

                    if (Arts[i].Photos != null)
                    {

                        sh.Path = Arts[i].Photos.FirstOrDefault().Path;
                    }
                    sh.DateTime = Arts[i].dateTime;
                    sh.Description = Arts[i].Description;
                    sh.price = Arts[i].Price;
                    // if ((manager.GetUserId(HttpContext.User))
                    sh.TypeOfArt = Arts[i].TypeOfArt;
                    sh.FullName = Arts[i].AppUser.FName + " " + Arts[i].AppUser.LName;
                    sh.UserPicture = Arts[i].AppUser.ProfilePicture;
                    sh.ArtId = Arts[i].ID;
                    if (Favourites == null)
                    {
                        sh.color = "Black";
                    }
                    else
                    {
                        if (Favourites.Contains(Arts[i]))
                        {
                            sh.color = "Red";
                        }
                        else
                        {
                            sh.color = "Black";
                        }
                    }
                    showArtViewModel.Add(sh);
                }
                return View(showArtViewModel);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
