using System;
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
            internal set { _guiid = value; }
        }
        public int ParentId { get; set; }
        private int _userId;

        public int _UserId
        {
            get { return _userId; }
            internal set { _userId = value; }
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
            set { forename = value; }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private RoleModel role;
        public RoleModel _Role
        {
            get { return role; }
            set { role = value; }
        }

        private DeptoModel depto;
        public DeptoModel Depto
        {
            get { return depto; }
            set { depto = value; }
        }

        private bool leaver;
        public bool Leaver
        {
            get { return this.leaver; }
            set
            {
                SetProperty<bool>("Leaver", ref this.leaver, value);
            }
        }

        private DateTime? leavingDate;
        public DateTime? LeavingDate
        {
            get { return leavingDate; }
            set { leavingDate = value; }
        }

        public UserVM()
        {
        }


        public UserVM(Guid id, int userId, string userName, string forename,
                       string surname, DateTime? startDate, RoleModel role, DeptoModel depto, bool leaver, DateTime? leavingDate)
        {
            this._Guid = id;
            this._UserId = userId;
            UserName = userName;
            Forename = forename;
            Surname = surname;
            StartDate = startDate == DateTime.MinValue ? null : startDate;
            _Role = role;
            Depto = depto;
            Leaver = leaver;
            LeavingDate = leavingDate == DateTime.MinValue ? null : leavingDate;

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
        }

        //Creating a new user in the DB assigns the UserId
        //Update the UserId from the value in the corresponding UserModel
        public void UserAdded2DB(UserModel userModel)
        {
            this._UserId = userModel.UserId;
        }

        public override string ToString()
        {
            return string.Format("Username:{0}\nForename:{1}\nSurname{2}", this.UserName, this.Forename, this.Surname);
        }

    }

    //class User




}
