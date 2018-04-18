using System;
using System.ComponentModel;

namespace Domains_DLL.Models
{
    public class User
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #region Fields

        //For DB use only!
        private int _userId;
        public int _UserId { get { return _UserId; } }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value; OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }
        private string forename;
        public string Forename
        {
            get { return forename; }
            set
            {
                forename = value; OnPropertyChanged(new PropertyChangedEventArgs("Forename"));
            }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value; OnPropertyChanged(new PropertyChangedEventArgs("Surname"));
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value; OnPropertyChanged(new PropertyChangedEventArgs("StartDate"));
            }
        }
        public Role  Role{ get; set; }
        public Department Depto{ get; set; }
        
        private bool leaver;
        public bool Leaver
        {
            get { return leaver; }
            set
            {
                leaver = value; OnPropertyChanged(new PropertyChangedEventArgs("Leaver"));
            }
        }

        private DateTime? leavingDate;
        public DateTime LeavingDate
        {
            get { return (DateTime) leavingDate; }
            set
            {
                leavingDate = value; OnPropertyChanged(new PropertyChangedEventArgs("LeavingDate"));
            }
        }
        #endregion

        #region Constructors

        public User()
        {
            //DoSomething
        }
        public User(User cloneOf)
        {
            this._userId = cloneOf._UserId;
            this.userName = cloneOf.UserName;
            this.forename = cloneOf.Forename;
            this.surname = cloneOf.Surname;
            this.startDate = cloneOf.StartDate;
            this.Role = cloneOf.Role;
            this.Depto = cloneOf.Depto;
            this.leaver = cloneOf.Leaver;
            this.leavingDate = cloneOf.LeavingDate;
        }
        #endregion


        //Creating a new user in the XML assigns the UserID
        //Update the UserId from the value in the corresponding XMLUser
        public void UserAdded(XMLUser xmlUser)
        {
            this._userId = xmlUser.UserId;
        }
    }
}
