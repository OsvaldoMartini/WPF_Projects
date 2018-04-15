using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingTests.PersonDomain
{
    public class PersonDomain
    {
        public string personName;
        public string address;

        public PersonDomain(string name, string address)
        {
            this.personName = name;
            this.address = address;
        }

        public override string ToString()
        {
            return this.personName + " " + this.address;
        }
    }

}
