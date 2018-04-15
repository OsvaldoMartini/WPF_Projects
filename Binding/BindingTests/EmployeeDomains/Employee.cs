using System;

namespace Binding.Different.Ways.Model
{
    public class Employee
    {


        /*Summary
         This is what is known as an immutable class.  (Typical  Domain Class)
         This is a (simplified) version of a typical domain class. It has the following characteristics:
            1-All values are passed in in the constructor
            2-All properties are read-only (at least from outside of this class)
            3-Methods provide access to calculated values (getFullName and getAge)
         */

        public Employee(int id, string firstname, string lastname, DateTime birthdate, string street)
        {
            this.ID = id;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.BirthDate = birthdate;
            this.Street = street;
        }

        public int ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Street { get; private set; }

        public string getFullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public int getAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-age)) age--;
            return age;
        }
    }
}