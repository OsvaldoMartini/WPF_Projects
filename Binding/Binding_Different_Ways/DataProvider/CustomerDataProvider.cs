using System;
using System.Data;

namespace Binding.Different.Ways.DataProvider
{

    public class CustomerDataProvider
    {
        private DataView dw;

        public CustomerDataProvider()
        {
            dw = new DataView(TableCustomer());
        }

        public DataView GetCustomers()
        {
            return dw;
        }


        static DataTable TablePrescription()
        {
            DataTable table = new DataTable("Prescription");
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            return table;
        }

        static DataTable TableCustomer()
        {
            DataTable table = new DataTable("Customer");
            table.Columns.Add("CustomerID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            table.Rows.Add(25, "David", DateTime.Now);
            table.Rows.Add(50, "Sam", DateTime.Now);
            table.Rows.Add(10, "Christoff", DateTime.Now);
            table.Rows.Add(21,  "Janet", DateTime.Now);
            table.Rows.Add(100, "Melanie", DateTime.Now);
            table.AcceptChanges();
            return table;
        }


    }



}
