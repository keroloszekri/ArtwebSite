using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public interface IArtPhotoBase<T>
    {
        public int Add(T Model);
        public int Delete(int ArtId);
    }
}
