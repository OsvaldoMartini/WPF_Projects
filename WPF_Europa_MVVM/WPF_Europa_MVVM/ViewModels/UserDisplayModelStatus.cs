﻿using System;
using System.ComponentModel;
using System.Windows.Media;
using WPF_Europa_MVVM.Foundation;

namespace WPF_Europa_MVVM.ViewModels
{
    //UserVM Error detection, error display and status msg
    //Note, a Delete may be performed without checking any Usert fields
    public class UserDisplayModelStatus : INotifyPropertyChanged
    {
        public UserDisplayModelStatus()
        {
            Messenger messenger = App.Messenger;
            messenger.Register("UserExist", (Action<Boolean>)(param => UserExist = param));
        
            NoError();
        } //constructor
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private bool _userExist;
        public bool UserExist
        {
            get { return _userExist; }
            set { _userExist = value; OnPropertyChanged(new PropertyChangedEventArgs("UserExist")); }
        }


        //Error status msg and field Brushes to indicate userVM field errors
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(new PropertyChangedEventArgs("Status")); }
        }
        private static SolidColorBrush errorBrush = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush okBrush = new SolidColorBrush(Colors.Black);

        private SolidColorBrush userNameBrush = okBrush;
        public SolidColorBrush UserNameBrush
        {
            get { return userNameBrush; }
            set { userNameBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("UserNameBrush")); }
        }

        private SolidColorBrush forenameBrush = okBrush;
        public SolidColorBrush ForenameBrush
        {
            get { return forenameBrush; }
            set { forenameBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("ForenameBrush")); }
        }

        private SolidColorBrush surnameBrush = okBrush;
        public SolidColorBrush SurnameBrush 
        {
            get { return surnameBrush; }
            set { surnameBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("SurnameBrush")); }
        }


        private SolidColorBrush startDateBrush = okBrush;
        public SolidColorBrush StartDateBrush
        {
            get { return startDateBrush; }
            set { startDateBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("StartDateBrush")); }
        }
        
        private SolidColorBrush roleBrush = okBrush;
        public SolidColorBrush RoleBrush
        {
            get { return roleBrush; }
            set { roleBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("RoleBrush")); }
        }


        private SolidColorBrush deptoBrush = okBrush;
        public SolidColorBrush DeptoBrush
        {
            get { return deptoBrush; }
            set { deptoBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("DeptoBrush")); }
        }
        
        private SolidColorBrush leaverBrush = okBrush;
        public SolidColorBrush LeaverBrush
        {
            get { return leaverBrush; }
            set { leaverBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("LeaverBrush")); }
        }

        private SolidColorBrush leavingDateBrush = okBrush;
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
                if ((p.LeavingDate == DateTime.MinValue) || p.LeavingDate < p.StartDate)
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
                if ((p.LeavingDate == DateTime.MinValue) || p.LeavingDate < p.StartDate)
                {
                    {
                        errCnt++;
                    }
                    LeavingDateBrush = errorBrush;
                }
            }
            else LeavingDateBrush = okBrush;
            
            if (errCnt == 0) { Status = "OK"; return true; }
            else { Status = "Update, missing or invalid fields."; return false; }
        } //ChkUserForUpdate()


        public bool CheckIfUserExist(UserVM p)
        {
            App.Messenger.NotifyColleagues("CheckUserExist", p);

            int errCnt = 0;
            if (UserExist)
            {
                errCnt++;
                UserNameBrush = errorBrush;
            }
            else UserNameBrush = okBrush;

            if (errCnt == 0) { return true; }
            else { Status = "Username has been taken!"; return false; }
        }
    } //class UserDisplayModelStatus
}  //NS: UserMvvm.ViewModels
