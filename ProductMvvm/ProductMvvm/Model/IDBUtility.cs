using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductMvvm.Model
{
    interface IDBUtility<T>
    {
        T CreateData();
    }
}
