using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using Europa_Data.Controls;
using Europa_Data.DB_Helper;

namespace Europa_Data.Model
{
    public class UserModel
    {
        public Guid Guid { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        //[XmlIgnore]
        public DateTime StartDate { get; set; }
        public RoleModel _Role { get; set; }
        public DeptoModel Depto { get; set; }
        public bool Leaver { get; set; }
        //[XmlIgnore]
        public DateTime LeavingDate { get; set; }
        [XmlIgnore]
        public Collection<UserAnalisys> UserAnalizes { get; set; }


        private string _description;
        [XmlIgnore]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        #region Builder Fields
        private bool checkFileFirst = true;

        private UserObservableCollection<UserVM> _User;
        public UserModel WhitCheckFileFirst(bool checkFileFirst)
        {
            this.checkFileFirst = checkFileFirst;
            return this;
        }

        public UserModel WhitUserId(int p)
        {
            this.UserId = p;
            return this;
        }

        public static implicit operator UserObservableCollection<UserVM>(UserModel instance)
        {
            return instance.Build();
        }

        #endregion

        public UserModel()
        {
        }
        
        public bool DeleteByXML()
        {
            var filePath = String.Format("{0}{1}\\UserModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            return DBUtility.DeleteByXML(this.UserId, filePath);
        }

        public bool AddByXML()
        {
            var filePath = String.Format("{0}{1}\\UserModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            return DBUtility.AddByXML(this, filePath);

        }

        public bool UpdateByXML()
        {
            var filePath = String.Format("{0}{1}\\UserModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            return DBUtility.UpdateByXML(this, filePath);

        }

        public UserObservableCollection<UserVM> Build()
        {
            UserObservableCollection<UserVM> users = new UserObservableCollection<UserVM>();

            var filePath = String.Format("{0}{1}\\UserModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            List<UserModel> list = null;
            if (this.checkFileFirst)
                if (!File.Exists(filePath))
                {
                    DBUtility.CreateFile<DeptoModel>(DBUtility.MockDepartment());
                    DBUtility.CreateFile<RoleModel>(DBUtility.MockRoles());
                    list = DBUtility.CreateFile<UserModel>(DBUtility.MockUserModel());
                }
                else

                    list = DBUtility.DeserializeParamsListOf<UserModel>(filePath);

                    //list = DBUtility.DeserializeXMLFileOf <List<UserModel>>(filePath);

            if (list != null)
                foreach (UserModel sp in list)
                    users.Add(sp.UserModel2User());


            return users;
        }

        public UserModel(Guid id,int userId, string userName, string forename,
            string surname, DateTime startDate, RoleModel role, DeptoModel dpto, bool leaver, DateTime leavingDate)
        {
            Guid = id;
            UserId = userId;
            UserName = userName;
            Forename = forename;
            Surname = surname;
            StartDate = startDate;
            _Role = role;
            Depto = Depto;
            Leaver = leaver;
            LeavingDate = leavingDate;


        }

        public UserModel(UserVM p)
        {
            Guid = p._Guid;
            UserId = p._UserId;
            UserName = p.UserName;
            Forename = p.Forename;
            Surname = p.Surname;
            StartDate = p.StartDate;
            _Role = p._Role;
            Depto = p.Depto;
            Leaver = p.Leaver;
            LeavingDate = p.LeavingDate;
        }

        public UserVM UserModel2User()
        {
            return new UserVM(Guid,UserId, UserName, Forename, Surname, StartDate,_Role,Depto,Leaver,LeavingDate);
        } //UserModel2User()

        

    }
}
