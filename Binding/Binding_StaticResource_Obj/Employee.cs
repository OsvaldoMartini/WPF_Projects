﻿namespace Binding.StaticResource.Obj
{
    public class Employee
    {
        int _employeeNumber;
        string _firstName;
        string _lastName;
        string _department;
        string _title;

        public Employee() 
        {
            _employeeNumber = 0;
            _firstName =
                _lastName =
                _department =
                _title = null;
        }
        public int EmployeeNumber
        {
            get { return _employeeNumber; }
            set { _employeeNumber = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

    }
}
