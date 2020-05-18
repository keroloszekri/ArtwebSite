using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Models
{
    public static class MyIdentityDataInitializer
    {
       
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("Fox").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Fox";
                user.Email = "boulissamir@gmail.com";
                user.FName = "Boules";
                user.LName = "Samir";
                user.Gender = Gender.Male;
                user.PasswordHash = "AAaa1#";
                user.PhoneNumber = "01254789336";
                user.ProfilePicture = "MaleProfile.png";
                IdentityResult result = userManager.CreateAsync
                (user,user.PasswordHash ).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }


            if (userManager.FindByNameAsync("Genawy").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Genawy";
                user.Email = "genawy@gmail.com";
                user.FName = "Abdo";
                user.LName = "Mohamed";
                user.Gender = Gender.Male;
                user.PasswordHash = "AAaa1#";
                user.PhoneNumber = "01254789336";
                user.ProfilePicture = "MaleProfile.png";
                IdentityResult result = userManager.CreateAsync
                (user, user.PasswordHash).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

        }

        public static void SeedRoles  (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.Id = "201";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.Id = "200";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
       
    }
}
