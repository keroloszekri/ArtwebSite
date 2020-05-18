using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public class ArtService : IArtBase<Art>
    {
        private readonly ApplicationDbContext context;

        public ArtService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Add(Art Model)
        {
            context.Arts.Add(Model);
            context.SaveChanges();
            return Model.ID;

        }

        public int Delete(int id)
        {
            Art art = context.Arts.FirstOrDefault(ar => ar.ID == id);
            List<string> paths = context.ArtPhotos.Where(p => p.Art == art).Select(p => p.Path).ToList();
            context.Arts.Remove(art);
            context.SaveChanges();
            foreach (var item in paths)
            {
                string f = "./wwwroot/images/" + item;
                if (System.IO.File.Exists(f))
                {
                    System.IO.File.Delete(f);
                }
            }
            return 1;
        }

        public List<Art> GetAll()
        {
            return context.Arts.Include(c => c.Photos).Include(c => c.AppUser).OrderByDescending(rate => (rate.Rates.Sum(r => r.Vote) / rate.Rates.Count())).ToList();
        }

        public List<Art> GetAllByUser(string UserID)
        {
            return context.Arts.Where(art => art.UserID == UserID).OrderBy(art => art.TypeOfArt).Include(c => c.Photos).Include(c => c.AppUser).ToList();
        }

        public Art GetDetails(int id)
        {
            return context.Arts.Include(c => c.Photos).Include(c => c.AppUser).Include(c => c.Rates).FirstOrDefault(s => s.ID == id);
        }
        public List<string> GetArtPhotosPaths(int id)
        {
            return context.ArtPhotos.Include(c => c.Art).Where(Photo => Photo.ArtID == id).Select(Photo => Photo.Path).ToList();
        }
        public int Update(int id, Art Model)
        {
            Art std = context.Arts.FirstOrDefault(s => s.ID == id);
            std.Name = Model.Name;
            std.Price = Model.Price;
            std.TypeOfArt = Model.TypeOfArt;
            std.Description = Model.Description;
            context.SaveChanges();
            return 1;
        }
    }

}
