using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public class FavouriteService : IFavouriteBase<Favourite>
    {
        private readonly ApplicationDbContext context;
        public FavouriteService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Add(int artID, string userID)
        {
            Favourite fav = new Favourite();
            fav.ArtID = artID;
            fav.UserID = userID;
            context.Favourites.Add(fav);
            context.SaveChanges();
            return fav.ArtID;
        }
        public int Delete(int Artid, string app)
        {
            context.Favourites.Remove(context.Favourites.FirstOrDefault(s => s.ArtID == Artid && s.UserID == app));
            context.SaveChanges();
            return Artid;

        }
        public List<Art> GetAll(string appuser)
        {
            var arts = (from fav in context.Favourites
                        from art in context.Arts
                        where art.ID == fav.ArtID && appuser == fav.UserID
                        select art).Include(a => a.AppUser).Include(a=>a.Photos).ToList();
            //var art = context.Favourites.Include(c => c.Art).Include(c => c.AppUser).Where(f => f.UserID == appuser).Select(a => a.Art).ToList();
            return arts;
        }
    }

}