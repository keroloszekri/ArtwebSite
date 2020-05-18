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
    public class UserViewComponent : ViewComponent
    {
        private readonly IUserBase<ApplicationUser> user;

        public UserViewComponent(IUserBase<ApplicationUser> user)
        {
            this.user = user;
        }
        public IViewComponentResult Invoke(int id)
        {
            
            return View("_UserPartialView", user.GetUser(id));
        }
    }
}
