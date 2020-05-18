using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [RegularExpression("[A-Za-z -]{3,}", ErrorMessage = "First name must contains letters, spaces and dashes only and contains 3 or more characters")]
        public string FName { get; set; }

        [Required]
        [RegularExpression("[A-Za-z -]{3,}", ErrorMessage = "Last name must contains letters, spaces and dashes only and contains 3 or more characters")]
        public string LName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public string Address { get; set; }

        public string ProfilePicture { get; set; }

        public string Bio { get; set; }
        public virtual ICollection<Art> Arts { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}
