using ASPCoreNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.ViewModels
{
    public class ShowArtViewModel
    {
        public int ArtId { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public ArtType TypeOfArt { get; set; }
        public double price { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string color { get; set; }
        public string UserPicture { get; set; }
    }
}
