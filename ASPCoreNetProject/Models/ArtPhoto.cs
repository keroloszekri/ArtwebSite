using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Models
{
    public class ArtPhoto
    {
        public int ID { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public int ArtID { get; set; }
        [ForeignKey("ArtID")]
        public virtual Art Art { get; set; }
    }
}
