using System;
using System.Collections.ObjectModel;
using Europa_Data.INotifyChanging;

namespace Europa_Data.Model
{
    //Communication to / from SQL uses this class for user
    // Consversion routines UserModel <--> User provided
    //Class for the GUI to display and modify users.
    //All user properties the GUI can touch are strings.
    //A single integer property, UserId, is for database use only.
    //It is assigned by the DB when it creates a new user.  It is used
    //to identify a user and must not be modified by the GUI.

    public class UserVM : PropertyNotificationObject
    {

        private Guid _guiid;

        public Guid _Guid
        {
            get { return _guiid; }
            internal set
            {
                _guiid = value;
                SetProperty<Guid>("_Guid", ref this._guiid, value);
            }
        }

        public int ParentId { get; set; }
        private int _userId;

        public int _UserId
        {
            get { return _userId; }
            internal set
            {
                _userId = value;
                SetProperty<int>("_UserId", ref this._userId, value);
            }
        }


        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>("UserName", ref this.userName, value); }
        }

        private string forename;

        public string Forename
        {
            get { return forename; }
            set { SetProperty<string>("Forename", ref this.forename, value); }
        }

        private string surname;

        public string Surname
        {
            get { return surname; }
            set { SetProperty<string>("Surname", ref this.surname, value); }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { SetProperty<DateTime>("StartDate", ref this.startDate, value); }
        }

        private RoleModel role;

        public RoleModel _Role
        {
            get { return role; }
            set { SetProperty<RoleModel>("_Role", ref this.role, value); }
        }

        private DeptoModel depto;

        public DeptoModel Depto
        {
            get { return depto; }
            set { SetProperty<DeptoModel>("_Role", ref this.depto, value); }
        }

        private bool leaver;

        public bool Leaver
        {
            get { return this.leaver; }
            set { SetProperty<bool>("Leaver", ref this.leaver, value); }
        }

        private DateTime leavingDate;

        public DateTime LeavingDate
        {
            get { return leavingDate; }
            set { SetProperty<DateTime>("LeavingDate", ref this.leavingDate, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty<string>("Description", ref this._description, value); }
        }

        private UserAnalisysCollection _userAnalizes;

        public UserAnalisysCollection UserAnalizes
        {

            get { return _userAnalizes; }
            set { this._userAnalizes = value; }
        }



        public UserVM()
        {
        }


        public UserVM(Guid id, int userId, string userName, string forename,
            string surname, DateTime startDate, RoleModel role, DeptoModel depto, bool leaver, DateTime leavingDate)
        {
            this._Guid = id;
            this._UserId = userId;
            UserName = userName;
            Forename = forename;
            Surname = surname;
            StartDate = Convert(startDate);
            _Role = role;
            Depto = depto;
            Leaver = Convert(leaver);
            LeavingDate = Convert(leavingDate);
            Description = this.ToString();
            UserAnalizes = new UserAnalisysCollection();
        }

        public void CopyUser(UserVM p)
        {

            this._Guid = p._Guid;
            this._UserId = p._UserId;
            this.UserName = p.UserName;
            this.Forename = p.Forename;
            this.Surname = p.Surname;
            this.StartDate = p.StartDate;
            this._Role = p._Role;
            this.Depto = p.Depto;
            this.Leaver = p.Leaver;
            this.LeavingDate = p.LeavingDate;
            this.Description = this.ToString();
        }

        //Creating a new user in the DB assigns the UserId
        //Update the UserId from the value in the corresponding UserModel
        public void UserAdded2DB(UserModel userModel)
        {
            this._UserId = userModel.UserId;
        }

        public override string ToString()
        {
            return string.Format("Username:  {0}\nFull name:{1} {2}", this.UserName, this.Forename, this.Surname);
        }


        public static DateTime Convert(DateTime value)
        {
            if (value is DateTime)
            {
                DateTime dt = (DateTime) value;

                try
                {
                    if (dt > DateTime.MinValue)
                    {
                        return dt;

                    }
                }
                finally
                {
                }

                return DateTime.MinValue;
            }

            return DateTime.MinValue;
        }



        public static Boolean Convert(bool value)
        {
            if (value is string)
                if (value.ToString().ToLower().Equals("false"))
                    return false;
                else
                    return true;
            
            if (value is Boolean)
                return value;
            
            return false;

        }
    }
}
