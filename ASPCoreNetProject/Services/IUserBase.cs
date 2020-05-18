using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public interface IUserBase<T>
    {
        T GetUser(int ArtID);
    }
      
}
