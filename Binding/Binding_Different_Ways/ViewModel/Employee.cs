﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using Binding.Different.Ways.Abstract;

namespace Binding.Different.Ways.ViewModel
{
    public class Employee : ViewModelBase
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
            set 
            { 
                _employeeNumber = value;
                OnPropertyChanged("EmployeeNumber");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set 
            { 
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set 
            { 
                _lastName = value;
                Debug.WriteLine(_lastName);
                OnPropertyChanged("LastName");
            }
        }

        public string Department
        {
            get { return _department; }
            set 
            { 
                _department = value;
                OnPropertyChanged("Department");
            }
        }

        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public override string ToString()
        {
            return String.Format("{0} {1} ({2})", FirstName, LastName, EmployeeNumber);
        }
    }
}
