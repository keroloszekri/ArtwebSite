using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public interface IArtBase<T>
    {
        List<T> GetAll();
        List<T> GetAllByUser(string UserID);
        int Add(T Model);
        T GetDetails(int id);
        int Update(int id, T Model);
        int Delete(int id);
    }
}
