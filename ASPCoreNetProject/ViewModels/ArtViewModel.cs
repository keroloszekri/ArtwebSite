using ASPCoreNetProject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.ViewModels
{
    public class ArtViewModel
    {
        [Required]
        [RegularExpression(@"[a-zA-Z\s]{3,}", ErrorMessage = "Name must contains letters and spaces only and contains 3 or more letters")]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [MaxLength(200, ErrorMessage = "Description shouldn't exceed 200 characters")]
        public string Description { get; set; }
        [Required]
        public ArtType TypeOfArt { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
