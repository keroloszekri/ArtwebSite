using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPCoreNetProject.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ApplicationDbContext context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IWebHostEnvironment hostingEnvironment,ApplicationDbContext context)
        {   
            _userManager = userManager;
            _signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;
            this.context = context;
        }
        public string Username { get; set; }
       

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Required]
            [RegularExpression("[A-Za-z]{3,}", ErrorMessage = "First name must contains letters only and contains 3 or more letters")]
            [Display(Name = "First Name")]
            public string FName { get; set; }
            [Required]
            [RegularExpression("[A-Za-z]{3,}", ErrorMessage = "First name must contains letters only and contains 3 or more letters")]
            [Display(Name = "Last Name")]
            public string LName { get; set; }
            [Display(Name = "BIO")]
            public string BIO { get; set; }
            [Display(Name = "Gender")]
            [Required]
            public Gender Gender { get; set; }
            [Display(Name = "Address")]
            public string Address { get; set; }
            public IFormFile ProfilePicture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var FName = user.FName;
            var LName = user.LName;
            var BIO = user.Bio;
            var Address = user.Address;
            var ProfilePicture = user.ProfilePicture;
            var Gender = user.Gender;
            Username = userName;
            ViewData["picture"] = user.ProfilePicture;
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FName = FName,
                LName = LName,
                BIO = BIO,
                Address = Address,
                Gender = Gender,
               
               
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
           
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
           

            await _signInManager.RefreshSignInAsync(user);
            try
            {
                string uniqueFileName = null;
                if (Input.ProfilePicture != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Input.ProfilePicture.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    Input.ProfilePicture.CopyTo(new FileStream(filePath, FileMode.Create));
                    user.ProfilePicture = uniqueFileName;
                }
                user.FName = Input.FName;
                user.LName = Input.LName;
                user.Address = Input.Address;
                user.Bio = Input.BIO;
                user.Gender = Input.Gender;
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await LoadAsync(user);
                return Page();
            }
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
