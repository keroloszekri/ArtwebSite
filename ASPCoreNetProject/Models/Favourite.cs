using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Models
{
    public class Favourite
    {
        public string UserID { get; set; }
        public int ArtID { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
        public virtual Art Art { get; set; }
    }
}
