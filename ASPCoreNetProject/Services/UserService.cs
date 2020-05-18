using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public class UserService : IUserBase<ApplicationUser>
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }
        ApplicationUser IUserBase<ApplicationUser>.GetUser(int ArtID)
        {
            var UserID = context.Arts.Where(d => d.ID == ArtID).Select(s => s.UserID).FirstOrDefault();
            var User = context.Users.Where(d => d.Id == UserID).FirstOrDefault();
            return User;
        }

       
    }
}
