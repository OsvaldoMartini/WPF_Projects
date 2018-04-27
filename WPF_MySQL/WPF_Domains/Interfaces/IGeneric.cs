using System.Collections.Generic;

namespace Domains_DLL.Interfaces
{
    public interface IGeneric<T>
    {
        IList<T> GetAll();
        T GetById(int id);
    }
}
