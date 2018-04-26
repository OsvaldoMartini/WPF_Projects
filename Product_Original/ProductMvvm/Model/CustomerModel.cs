using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ProductMvvm.Util;

namespace ProductMvvm.Model
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public CustomerModel()
        {

        }


        public CustomerModel(bool checkFirsFile)
        {
            checkFirsFile = true;
            if (checkFirsFile)
            {
                // See if this file exists in the C:\ directory. [Note the @]
                var path = String.Format("{0}DBFiles\\CustomerModel.xml", AppDomain.CurrentDomain.BaseDirectory);
                if (!File.Exists(path))
                    DBUtility.CreateFile<CustomerModel>(DBUtility.MockCustomerModel(), path);


            }
        }
    }
}