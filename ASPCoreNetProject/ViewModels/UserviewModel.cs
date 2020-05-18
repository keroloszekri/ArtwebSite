using ASPCoreNetProject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.ViewModels
{
    public class UserviewModel
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}
