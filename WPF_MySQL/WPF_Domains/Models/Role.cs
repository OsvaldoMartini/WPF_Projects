using System;
using System.Collections.Generic;
using Domains_DLL.Interfaces;

namespace Domains_DLL.Models
{
    public class Role :IGeneric<Role>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Role GetById(int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
