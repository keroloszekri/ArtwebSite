using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public interface IFavouriteBase<T>
    {
        List<Art> GetAll(string userID);
        int Add(int artID, string userID);
        int Delete(int artID, string userID);
    }
}
