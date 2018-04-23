using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Model;

namespace WPF_Europa_MVVM.ViewModels
{
    public class UserDisplayModel : INotifyPropertyChanged
    {
        
        
        private bool isSelected = false;

        private List<RoleModel> _roles; 
        private List<DeptoModel> _departments; 
     
        //Messegers
        public UserDisplayModel()
        {
            Roles = App.StoreXML.GetRoles(); 
            Departments = App.StoreXML.GetDepartments(); 

            Messenger messenger = App.Messenger;
            messenger.Register("UserSelectionChanged", (Action<UserVM>)(param => UserToProcess(param)));
            messenger.Register("SetStatus", (Action<String>)(param => stat.Status = param));
        } //Constructor


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        //data checks and status indicators done in another class
        private readonly UserDisplayModelStatus stat = new UserDisplayModelStatus();
        public UserDisplayModelStatus Stat { get { return stat; } }

        private UserVM userToDisplay = new UserVM();
        public UserVM UserToDisplay
        {
            get { return userToDisplay; }
            set { userToDisplay = value; OnPropertyChanged(new PropertyChangedEventArgs("UserToDisplay")); }
        }


        private RelayCommand getUsersCommand;
        public ICommand GetUsersCommand
        {
            get { return getUsersCommand ?? (getUsersCommand = new RelayCommand(() => GetUsers())); }
        }

        private void GetUsers()
        {
            isSelected = false;
            stat.NoError();
            UserToDisplay = new UserVM();
            App.Messenger.NotifyColleagues("GetUsers");
        }

        #region CancelCommand
        private RelayCommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand ?? (cancelCommand = new RelayCommand(() => CancelOperation()/*, ()=>isSelected*/)); }
        }

        private void CancelOperation()
        {
            GlobalServices.ModalService.GoBackward(false);
            App.Messenger.NotifyColleagues("UserCleared");
        } //CancelOperation()
        #endregion


        #region ClearUserCommand
        private RelayCommand clearUserCommand;
        public ICommand ClearUserCommand
        {
            get { return clearUserCommand ?? (clearUserCommand = new RelayCommand(() => ClearUserDisplay()/*, ()=>isSelected*/)); }
        }

        private void ClearUserDisplay()
        {
            isSelected = false;
            stat.NoError();
            UserToDisplay = new UserVM();
            App.Messenger.NotifyColleagues("UserCleared");
        } //ClearUserDisplay()
        #endregion

        #region UpdateCommand
        private RelayCommand updateCommand;
        public ICommand UpdateCommand
        {
            get { return updateCommand ?? (updateCommand = new RelayCommand(() => UpdateUser(), () => isSelected)); }
        }

        private void UpdateUser()
        {
            if (!stat.ChkUserForUpdate(UserToDisplay)) return;
                if(!App.StoreXML.UpdateUser(UserToDisplay))
                {
                    stat.Status = App.StoreXML.errorMessage;
                    return;
                }
                App.Messenger.NotifyColleagues("UpdateUser", UserToDisplay);
        } //UpdateUser()
        #endregion

        #region DeleteCommand
        private RelayCommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand ?? (deleteCommand = new RelayCommand(() => DeleteUser(), () => isSelected)); }
        }

        private void DeleteUser()
        {
            if (!App.StoreXML.DeleteUser(UserToDisplay._UserId))
            {
                stat.Status = App.StoreXML.errorMessage;
                return;
            }
            isSelected = false;
            App.Messenger.NotifyColleagues("DeleteUser");
        } //DeleteUser
        #endregion

        #region SaveCommand
        private RelayCommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand ?? (saveCommand = new RelayCommand(() => SaveUser(), () => !isSelected)); }
        }

        public List<RoleModel> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public List<DeptoModel> Departments
        {
            get { return _departments; }
            set { _departments = value; }
        }

        private void SaveUser()
        {
            if (!stat.ChkUserForAdd(UserToDisplay)) return;
            if (!App.StoreXML.SaveUser(UserToDisplay))
            {
                stat.Status = App.StoreXML.errorMessage;
                return;
            }
            App.Messenger.NotifyColleagues("SaveUser", UserToDisplay);
        } //SaveUser()
        #endregion



        public void UserToProcess(UserVM p)
        {
            if (p == null) { /*UserToDisplay = null;*/  isSelected = false;  return; }
            UserVM temp = new UserVM();
            temp.CopyUser(p);
            UserToDisplay = temp;
            isSelected = true;
            stat.NoError();
        } // ProcessUser()
    }
}
