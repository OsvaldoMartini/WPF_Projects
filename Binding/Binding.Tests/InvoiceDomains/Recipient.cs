using System;

namespace Binding.Tests.InvoiceDomains
{
    public class Recipient
    {
        public int id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }

        public Recipient(int id, string FirstName, string LastName)
        {
            this.id = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public string GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public int GetAge()
        {
            DateTime today = DateTime.Now;
            int age = today.Year - BirthDate.Year;
            //If 
            if (BirthDate > today.AddYears(-age)) 
                age--;

            return age;
        }
    }
}