using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Models
{
    public class Rate
    {
        public string UserID { get; set; }
        public int ArtID { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
        public virtual Art Art { get; set; }
        [Required]
        [Range(0.0, 5.0)]
        public double Vote { get; set; }
    }
}
