﻿using System;
using System.ComponentModel;
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


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }

        #region INotifyPropertyChanged Members
        
        //hides inherited member, Use the new keyword if hiding was intended.
        //public new event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
