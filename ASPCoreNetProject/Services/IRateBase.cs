using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public interface IRateBase<T>
    {
        int Add( T Model);
        double GetVote(int ArtID);
        double GetVoteByUser(String UserID ,int ArtID);
        int Delete(String UserID , int ArtID);
    }
}
