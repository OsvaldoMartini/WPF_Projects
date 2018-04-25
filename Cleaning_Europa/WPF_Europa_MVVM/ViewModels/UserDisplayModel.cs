using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPF_Europa_MVVM.Foundation;
using Europa_Data.INotifyChanging;
using Europa_Data.Model;
using Europa_Data.ViewModel;
using WPF_Europa_MVVM.Views;
using PropertyChangingEventHandler = Europa_Data.INotifyChanging.PropertyChangingEventHandler;

namespace WPF_Europa_MVVM.ViewModels
{
    public class UserDisplayModel : ViewModelBase
    {
        private static UserDisplayModel self;

        private bool isSelected = false;
        private bool canProceed { get; set; }
        private List<RoleModel> _roles;
        private List<DeptoModel> _deptos;
        //data checks and status indicators done in another class
        private readonly UserDisplayModelStatus stat = new UserDisplayModelStatus();
        public static UserDisplayModel Instance()
        {
            if (self == null)
                self = new UserDisplayModel();
            return self;
        }

        /// <summary>
        /// For determining wich Action to be made inside  of the view
        /// </summary>
        public Mode Mode
        {
            get;
            set;
        }

        #region Constructor
        public UserDisplayModel()
        {
            Roles = App.StoreXML.GetRoles();
            Deptos = App.StoreXML.GetDepartments();

            Messenger messenger = App.Messenger;
            messenger.Register("UserSelectionChanged", (Action<UserVM>)(param => UserToProcess(param)));
            messenger.Register("ClearUserDisplay", (Action)(() => ClearUserDisplay()));
            messenger.Register("SetStatus", (Action<String>)(param => stat.Status = param));
            messenger.Register("AskToProceed", (Action<IntermediateMsgVM>)(param => CallYesOrNo(param)));
        }
        #endregion




        public UserDisplayModelStatus Stat
        {
            get { return stat; }
        }

        private UserVM userToDisplay = new UserVM();

        public UserVM UserToDisplay
        {
            get { return userToDisplay; }
            set
            {
                userToDisplay = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserToDisplay"));
            }
        }


        private RoleModel _roleItem;
        public RoleModel RoleItemSelected
        {
            get { return _roleItem; }
            set
            {
                _roleItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RoleItemSelected"));
                UpdateRoleUser(_roleItem);
            }
        }


        private DeptoModel _deptoItemSelected;
        public DeptoModel DeptoItemSelected
        {
            get { return _deptoItemSelected; }
            set
            {
                _deptoItemSelected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RoleItemSelected"));
                UpdateDeptoUser(_deptoItemSelected);
            }
        }
        private void UpdateRoleUser(RoleModel r)
        {
            int index = _roles.IndexOf(r);
            UserToDisplay._Role = _roles[index];
        }

        private void UpdateDeptoUser(DeptoModel d)
        {
            int index = _deptos.IndexOf(d);
            UserToDisplay.Depto = _deptos[index];
        }


        private RelayCommand getUsersCommand;
        public ICommand GetUsersCommand
        {
            get { return getUsersCommand ?? (getUsersCommand = new RelayCommand(() => GetUsers())); }
        }

        #region GetUsers
        private void GetUsers()
        {
            isSelected = false;
            stat.NoError();
            UserToDisplay = new UserVM();
            App.Messenger.NotifyColleagues("GetUsers");
        }
        #endregion

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
            //if ((!isSelected) && (!stat.CheckIfUserExist(UserToDisplay.UserName))) return;

            if (!stat.ChkUserForUpdate(UserToDisplay)) return;
            if (!App.StoreXML.UpdateUser(UserToDisplay))
            {
                stat.Status = App.StoreXML.errorMessage;
                return;
            }

            App.Messenger.NotifyColleagues("UpdateUser", UserToDisplay);
            var message = new IntermediateMsgVM { Flag = true, MessageId = 1, BtnCancelVisibility = Visibility.Hidden, MessageScreenTransfer = "Was was successfully updated !" };
            CallYesOrNo(message);

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
            var message = new IntermediateMsgVM { Flag = true, MessageId = 1, BtnCancelVisibility = Visibility.Hidden, MessageScreenTransfer = "Was was successfully deleted !" };
            CallYesOrNo(message);

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

        public List<DeptoModel> Deptos
        {
            get { return _deptos; }
            set { _deptos = value; }
        }

        private void SaveUser()
        {
            //if (!stat.CheckIfUserExist(UserToDisplay.UserName)) return;

            if (!stat.ChkUserForAdd(UserToDisplay)) return;

            if (!App.StoreXML.SaveUser(UserToDisplay))
            {
                stat.Status = App.StoreXML.errorMessage;
                return;
            }
            App.Messenger.NotifyColleagues("SaveUser", UserToDisplay);
            var message = new IntermediateMsgVM { Flag = true, MessageId = 1, BtnCancelVisibility = Visibility.Hidden, MessageScreenTransfer = "Was was successfully added !" };
            CallYesOrNo(message);

        } //SaveUser()
        #endregion

        private void CallYesOrNo(IntermediateMsgVM param)
        {
            GlobalServices.ModalService.NavigateTo(new YesOrNoMessage(param), delegate(bool returnValue)
            {
                this.canProceed = returnValue;
            });
        }

        public void UserToProcess(UserVM p)
        {
            if (p == null) { /*UserToDisplay = null;*/  isSelected = false; return; }
            UserVM temp = new UserVM();
            temp.CopyUser(p);
            temp.PropertyChanged += new PropertyChangedEventHandler(userVM_PropertyChanged);
            temp.PropertyChanging += new PropertyChangingEventHandler(userVM_PropertyChanging);


            UserToDisplay = temp;
            isSelected = true;
            stat.NoError();
        } // ProcessUser()

        private void userVM_PropertyChanging(object sender, CancelPropertyNotificationEventArgs e)
        {
            var strOld = (null == e.OldValue) ? string.Empty : e.OldValue.ToString();
            var strNew = (null == e.NewValue) ? string.Empty : e.NewValue.ToString();

            if ((e.PropertyName == "UserName") && ((strNew + strOld).Length > 0))
                e.Cancel = !stat.CheckIfUserExist(!isSelected ? strOld : strNew);

        }

        private void userVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyNotificationEventArgs e2 = e as PropertyNotificationEventArgs;
            String text;
            if (null != e2)
            {
                text = String.Format("The property '{0}' was changed from '{1}' to '{2}'.",
                    e2.PropertyName,
                    (null == e2.OldValue) ? "<null>" : e2.OldValue.ToString(),
                    (null == e2.NewValue) ? "<null>" : e2.NewValue.ToString());
            }
            else
            {
                text = String.Format("The property '{0}' was changed.",
                    e.PropertyName);
            }
        }



    }
}
