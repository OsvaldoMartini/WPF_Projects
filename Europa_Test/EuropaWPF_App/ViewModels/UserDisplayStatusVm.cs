using System;
using System.ComponentModel;
using System.Windows.Media;
using Europa_Data.Model;
using EuropaWPF_App.Foundation;

namespace EuropaWPF_App.ViewModels
{
    //For Error Detection and Messages Display
    public class UserDisplayStatusVm : ViewModelBase
    {
        private static SolidColorBrush errorBrush = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush okBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush userNameBrush = okBrush;
        private SolidColorBrush forenameBrush = okBrush;
        private SolidColorBrush surnameBrush = okBrush;
        private SolidColorBrush startDateBrush = okBrush;
        private SolidColorBrush roleBrush = okBrush;
        private SolidColorBrush deptoBrush = okBrush;
        private SolidColorBrush leaverBrush = okBrush;
        private SolidColorBrush leavingDateBrush = okBrush;
        private string status;
        private bool _userExist;

        
        public UserDisplayStatusVm()
        {
            Messenger messenger = App.Messenger;
            messenger.Register("UserExist", (Action<Boolean>)(param => UserExist = param));
        
            NoError();
        } //constructor
        
        public bool UserExist
        {
            get { return _userExist; }
            set { _userExist = value; OnPropertyChanged(new PropertyChangedEventArgs("UserExist")); }
        }


        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(new PropertyChangedEventArgs("Status")); }
        }
        
        public SolidColorBrush UserNameBrush
        {
            get { return userNameBrush; }
            set { userNameBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("UserNameBrush")); }
        }

        public SolidColorBrush ForenameBrush
        {
            get { return forenameBrush; }
            set { forenameBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("ForenameBrush")); }
        }

        public SolidColorBrush SurnameBrush 
        {
            get { return surnameBrush; }
            set { surnameBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("SurnameBrush")); }
        }


        public SolidColorBrush StartDateBrush
        {
            get { return startDateBrush; }
            set { startDateBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("StartDateBrush")); }
        }
        
        public SolidColorBrush RoleBrush
        {
            get { return roleBrush; }
            set { roleBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("RoleBrush")); }
        }


        public SolidColorBrush DeptoBrush
        {
            get { return deptoBrush; }
            set { deptoBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("DeptoBrush")); }
        }
        
        public SolidColorBrush LeaverBrush
        {
            get { return leaverBrush; }
            set { leaverBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("LeaverBrush")); }
        }

        public SolidColorBrush LeavingDateBrush 
        {
            get { return leavingDateBrush; }
            set { leavingDateBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("LeavingDateBrush ")); }
        }

        //set error field brushes to OKBrush and status msg to OK
        public void NoError()
        {
            UserNameBrush = ForenameBrush = SurnameBrush = StartDateBrush = RoleBrush = DeptoBrush = leavingDateBrush = okBrush;
            Status = "OK";
        } //NoError()



        //check all userVM fields for validity
        public bool ChkUserForAdd(UserVM p)
        {
            int errCnt = 0;
            if (String.IsNullOrEmpty(p.UserName))
            {
                errCnt++;
                UserNameBrush = errorBrush;
            }
            else UserNameBrush = okBrush;

            if (String.IsNullOrEmpty(p.Forename))
            {
                errCnt++;
                ForenameBrush = errorBrush;
            }
            else ForenameBrush = okBrush;

            if (String.IsNullOrEmpty(p.Surname))
            {
                errCnt++;
                SurnameBrush = errorBrush;
            }
            else SurnameBrush = okBrush;

            if ((p.StartDate == DateTime.MinValue))
            {
                errCnt++;
                StartDateBrush = errorBrush;
            }
            else StartDateBrush = okBrush;

            if ((p._Role== null) || ( p._Role.id == 0))
            {
                errCnt++;
                RoleBrush = errorBrush;
            }
            else RoleBrush = okBrush;

            if ((p.Depto == null) || (p.Depto.id == 0))
            {
                errCnt++;
                DeptoBrush = errorBrush;
            }
            else DeptoBrush = okBrush;

            if (p.Leaver)
            {
                if (p.LeavingDate == null || (p.LeavingDate == DateTime.MinValue) || p.LeavingDate < p.StartDate)
                {
                    errCnt++;
                    LeavingDateBrush = errorBrush;
                }
            }
            else LeavingDateBrush = okBrush;

            if (errCnt == 0) { Status = "OK"; return true; }
            else { Status = "ADD, missing or invalid fields."; return false; }
        } //ChkUserForAdd()


        //check all userVM fields for validity
        public bool ChkUserForUpdate(UserVM p)
        {
            int errCnt = 0;
            Status = string.Empty;
            
            if (String.IsNullOrEmpty(p.UserName))
            {
                errCnt++;
                UserNameBrush = errorBrush;
            }
            else UserNameBrush = okBrush;
            
            if (String.IsNullOrEmpty(p.Forename))
            {
                errCnt++;
                ForenameBrush = errorBrush;
            }
            else ForenameBrush = okBrush;

            if (String.IsNullOrEmpty(p.Surname))
            {
                errCnt++;
                SurnameBrush = errorBrush;
            }
            else SurnameBrush = okBrush;

            if ((p.StartDate > DateTime.Now || p.StartDate < p.LeavingDate))
            {
                errCnt++;
                StartDateBrush = errorBrush;
                Status = "Update, Sarte Date Bigger then today or equal at Less then Leaving Date!";

            }
            else StartDateBrush = okBrush;


            if ((p._Role == null) || (p._Role.id == 0))
            {
                errCnt++;
                RoleBrush = errorBrush;
            }
            else RoleBrush = okBrush;

            if ((p.Depto == null) || (p.Depto.id == 0))
            {
                errCnt++;
                DeptoBrush = errorBrush;
            }
            else DeptoBrush = okBrush;

            if (p.Leaver)
            {
                if (p.LeavingDate == DateTime.MinValue || p.LeavingDate <= p.StartDate)
                {
                    errCnt++;
                    Status = "Update, Leaving Date Bigger or equal at Start Date!";
                    LeavingDateBrush = errorBrush;
                }
            }
            else LeavingDateBrush = okBrush;
            
            if (errCnt == 0) { Status = "OK"; return true; }
            else
            {
                if (String.IsNullOrEmpty(Status))
                    Status = "Update, missing or invalid fields."; 
                return false;
            }
        } //ChkUserForUpdate()


        public bool CheckIfUserExist(string username)
        {
            App.Messenger.NotifyColleagues("CheckUserExist", username);

            int errCnt = 0;
            if (UserExist)
            {
                errCnt++;
                UserNameBrush = errorBrush;
            }
            else UserNameBrush = okBrush;

            if (errCnt == 0)
            {
                Status = "OK"; return true; }
            else { Status = string.Format("Username: {0} has been taken!",username); return false; }
        }
    } //class UserDisplayModelStatus
}  //NS: UserMvvm.ViewModels
