using ASPCoreNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.ViewModels
{
    public class ArtDetailsViewModel
    {
        public int ArtId { get; set; }
        public List<string> Path { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public ArtType TypeOfArt { get; set; }
        public DateTime DateTime { get; set; }
        public string FullName { get; set; }
    }
}
