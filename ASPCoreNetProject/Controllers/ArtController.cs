using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using ASPCoreNetProject.Services;
using ASPCoreNetProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreNetProject.Controllers
{
    [Authorize]
    public class ArtController : Controller
    {
        private readonly IFavouriteBase<Favourite> favouriteService;
        private readonly IArtBase<Art> service;
        private readonly IArtPhotoBase<ArtPhoto> photoService;
        private readonly UserManager<ApplicationUser> manager;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ArtController(IArtBase<Art> service, IArtPhotoBase<ArtPhoto> photoService, UserManager<ApplicationUser> manager, IWebHostEnvironment hostingEnvironment, IFavouriteBase<Favourite> FavouriteService)
        {
            this.service = service;
            this.photoService = photoService;
            this.manager = manager;
            this.hostingEnvironment = hostingEnvironment;
            favouriteService = FavouriteService;
        }
        public IActionResult Index()
        {
           
            List<ShowArtViewModel> showArtViewModel = new List<ShowArtViewModel>();
            var Arts = service.GetAllByUser(manager.GetUserId(HttpContext.User));
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
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ArtViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Art art = new Art();
            art.Name = model.Name;
            art.Price = model.Price;
            art.TypeOfArt = model.TypeOfArt;
            art.dateTime = DateTime.Now;
            art.Description = model.Description;
            art.UserID = manager.GetUserId(HttpContext.User);
            try
            {
                service.Add(art);
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        ArtPhoto artPhoto = new ArtPhoto();
                        artPhoto.Path = uniqueFileName;
                        artPhoto.ArtID = art.ID;
                        photoService.Add(artPhoto);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            return RedirectToAction("ShowDetails", new { id = art.ID });
        }
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.id = id;
            Art art = service.GetDetails(id);
            ArtViewModel model = new ArtViewModel();
            model.Name = art.Name;
            model.Price = art.Price;
            model.TypeOfArt = art.TypeOfArt;
            model.Description = art.Description;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, ArtViewModel model)
        {
            ViewBag.id = id;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Art art = new Art();
            art.Name = model.Name;
            art.Price = model.Price;
            art.Description = model.Description;
            art.TypeOfArt = model.TypeOfArt;
            try
            {
                service.Update(id, art);
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    photoService.Delete(id);
                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        ArtPhoto artPhoto = new ArtPhoto();
                        artPhoto.Path = uniqueFileName;
                        artPhoto.ArtID = id;
                        photoService.Add(artPhoto);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            return RedirectToAction("ShowDetails", new { id = id });
        }
        public IActionResult ShowDetails(int id)
            {
            ArtService artService = (ArtService)service;
            Art art = artService.GetDetails(id);
            ArtDetailsViewModel artDetails = new ArtDetailsViewModel();
            artDetails.ArtId = art.ID;
            artDetails.Path = artService.GetArtPhotosPaths(id);
            artDetails.TypeOfArt = art.TypeOfArt;
            artDetails.Name = art.Name;
            artDetails.Description = art.Description;
            artDetails.Price = art.Price;
            artDetails.FullName = art.AppUser.FName + " " + art.AppUser.LName;
            artDetails.DateTime = art.dateTime;
            return View(artDetails);
        }
    }
}