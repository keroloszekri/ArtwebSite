using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Models
{
    public class Art
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z\s]{3,}", ErrorMessage = "Name must contains letters and spaces only and contains 3 or more letters")]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [MaxLength(200, ErrorMessage = "Description shouldn't exceed 200 characters")]
        public string Description { get; set; }
        [Required]
        public ArtType TypeOfArt { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime dateTime { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser AppUser { get; set; }
        public virtual ICollection<ArtPhoto>Photos { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}
