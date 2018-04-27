using Domains_DLL.Interfaces;
using System;
using System.Collections.Generic;

namespace Domains_DLL.Models
{
    public class Department : IGeneric<Department>
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }

        public IList<Department> ListDept { get; set; }

        public IList<Department> GetAll()
        {
            throw new NotImplementedException();
        }

        public Department GetById(int deptoId)
        {
            throw new NotImplementedException();
        }
    }
}
