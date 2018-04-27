using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Europa_Data.Model
{
    public class UserAnalisys
    {
        public UserAnalisys(string nameAnalize, long valueReached)
        {
            this.NameAnalize= nameAnalize;
            this.ValueReached = valueReached;
        }
        public string NameAnalize
        {
            get;
            set;
        }
        public long ValueReached
        {
            get;
            set;
        }
    }

    public class UserAnalisysCollection : Collection<UserAnalisys>
    {
        public UserAnalisysCollection()
        {
            Random rnd = new Random();
            Add(new UserAnalisys("Performance", rnd.Next(99999999, 1999999999)));
            Add(new UserAnalisys("Communication", rnd.Next(99999999, 1999999999)));
            Add(new UserAnalisys("Pro-Activity", rnd.Next(99999999, 1999999999)));
            Add(new UserAnalisys("Organization", rnd.Next(99999999, 1999999999)));
            Add(new UserAnalisys("Evolution", rnd.Next(99999999, 1999999999)));
        }
    }

}
