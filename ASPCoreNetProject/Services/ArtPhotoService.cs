using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public class ArtPhotoService:IArtPhotoBase<ArtPhoto>
    {
        private readonly ApplicationDbContext context;

        public ArtPhotoService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Add(ArtPhoto Model)
        {
            context.ArtPhotos.Add(Model);
            context.SaveChanges();
            return Model.ID;

        }

        public int Delete(int ArtId)
        {
            var artPhotos=context.ArtPhotos.Where(p => p.ArtID == ArtId);
            context.ArtPhotos.RemoveRange(artPhotos);
            return 1;
        }
    }
}
