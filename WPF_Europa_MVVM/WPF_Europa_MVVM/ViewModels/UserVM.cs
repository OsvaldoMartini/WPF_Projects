﻿using System;
using System.ComponentModel;
using WPF_Europa_MVVM.Model;

namespace WPF_Europa_MVVM.ViewModels
{
    //Class for the GUI to display and modify users.
    //All user properties the GUI can touch are strings.
    //A single integer property, UserId, is for database use only.
    //It is assigned by the DB when it creates a new user.  It is used
    //to identify a user and must not be modified by the GUI.

    public class UserVM : ViewModelBase
    {
       
        //For DB use only!
        private Guid _guiid;
        public Guid _Guid{ get { return _guiid; } }
        //For DB use only!
        private int _userId;
        public int _UserId { get { return _userId; } }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
                }
        }

        private string forename;
        public string Forename
        {
            get { return forename; }
            set { forename = value; OnPropertyChanged(new PropertyChangedEventArgs("Forename")); }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; OnPropertyChanged(new PropertyChangedEventArgs("Surname")); }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; OnPropertyChanged(new PropertyChangedEventArgs("StartDate")); }
        }

        private RoleModel role;
        public RoleModel _Role
        {
            get { return role; }
            set { role = value; OnPropertyChanged(new PropertyChangedEventArgs("_Role")); }
        }

        private DeptoModel depto;
        public DeptoModel Depto
        {
            get { return depto; }
            set { depto = value; OnPropertyChanged(new PropertyChangedEventArgs("Depto")); }
        }


        private bool leaver;
        public bool Leaver
        {
            get { return leaver; }
            set { leaver = value; OnPropertyChanged(new PropertyChangedEventArgs("Leaver")); }
        }

        private DateTime? leavingDate;
        public DateTime? LeavingDate
        {
            get { return leavingDate; }
            set { leavingDate = value; OnPropertyChanged(new PropertyChangedEventArgs("LeavingDate")); }
        }

        public UserVM()
        {
        }

        public UserVM(Guid id, int userId, string userName, string forename,
                       string surname, DateTime startDate, RoleModel role,DeptoModel depto, bool leaver, DateTime? leavingDate)
        {
            this._guiid = id;
            this._userId = userId;
            UserName = userName;
            Forename = forename;
            Surname = surname;
            StartDate = startDate;
            _Role = role;
            Depto = depto;
            Leaver = leaver;
            LeavingDate = leavingDate==DateTime.MinValue?null:leavingDate;

        }

        public void CopyUser(UserVM p)
        {
            this._guiid = p._Guid;
            this._userId = p._UserId;
            this.UserName = p.UserName;
            this.Forename = p.Forename;
            this.Surname = p.Surname;
            this.StartDate = p.StartDate;
            this._Role = p._Role;
            this.Depto = p.Depto;
            this.Leaver = p.Leaver;
            this.LeavingDate = p.LeavingDate;
        }

        //Creating a new user in the DB assigns the UserId
        //Update the UserId from the value in the corresponding UserModel
        public void UserAdded2DB(UserModel userModel)
        {
            this._userId = userModel.UserId;
        }

    } //class User

    //Communiction to/from SQL uses this class for user
    //It has a decimal, not string, definition for Leaver
    //Consversion routines UserModel <--> User provided

   

}
