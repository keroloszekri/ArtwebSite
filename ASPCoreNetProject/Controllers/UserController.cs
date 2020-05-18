using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreNetProject.Models;
using ASPCoreNetProject.Services;
using ASPCoreNetProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreNetProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> manager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<ApplicationUser> manager, RoleManager<IdentityRole> roleManager)
        {
            this.manager = manager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<UserviewModel> users = new List<UserviewModel>();
            foreach (var item in manager.Users.ToList())
            {
                UserviewModel userviewModel = new UserviewModel();
                userviewModel.ID = item.Id;
                userviewModel.FullName = item.FName + item.LName;
                userviewModel.Phone = item.PhoneNumber;
                userviewModel.ProfilePicture = item.ProfilePicture;
                userviewModel.Gender = item.Gender;
                userviewModel.Email = item.Email;
                var roles = await manager.GetRolesAsync(item);
                userviewModel.UserRole = roles[0];
                users.Add(userviewModel);

            }
            ViewBag.roles = roleManager.Roles.ToList();
            return View(users);
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(string UserId)
        {
            var user = await manager.FindByIdAsync(UserId);
            IdentityResult result = await manager.DeleteAsync(user);
            if (result.Succeeded)
                return Content("deleted");
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(string UserUpdated, string RoleName)
        {
            var user = await manager.FindByIdAsync(UserUpdated);
            var oldrols = await manager.GetRolesAsync(user);
            if (RoleName != oldrols[0])
            {
                IdentityResult result = await manager.RemoveFromRoleAsync(user, oldrols[0]);
                if (result.Succeeded)
                {
                    IdentityResult result1 = await manager.AddToRoleAsync(user, RoleName);
                    if (result1.Succeeded)
                        return Content("Updated");
                }
            }
            return RedirectToAction("Index");
        }
    }
}