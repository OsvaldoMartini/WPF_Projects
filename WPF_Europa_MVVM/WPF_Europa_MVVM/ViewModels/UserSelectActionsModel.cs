using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WPF_Europa_MVVM.Controls;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Views;

namespace WPF_Europa_MVVM.ViewModels
{
    public class UserSelectActionsModel : ViewModelBase
    {

        public UserSelectActionsModel()
        {
            _dataItems = new UserObservableCollection<UserVM>();

            DataItems = App.StoreXML.GetUsers();

            _listBoxCommand = new RelayCommand(() => SelectionHasChanged());
            
            //Messengers / messages
            App.Messenger.Register("UserCleared", (Action)(() => SelectedUser = null));
            App.Messenger.Register("GetUsers", (Action)(() => GetUsers()));
            App.Messenger.Register("UpdateUser", (Action<UserVM>)(param => UpdateUser(param)));
            App.Messenger.Register("DeleteUser", (Action)(() => DeleteUser()));
            App.Messenger.Register("SaveUser", (Action<UserVM>)(param => SaveUser(param)));
            App.Messenger.Register("CheckUserExist", (Action<UserVM>)(param => CheckUserExist(param)));

        }


        private readonly RelayCommand _listBoxCommand;
        public ICommand ListBoxCommand
        {
            get { return _listBoxCommand; }
        }

        private void SelectionHasChanged()
        {
            Messenger messenger = App.Messenger;
            if (_selectedUser != null)
                Call_UserWindow();
            messenger.NotifyColleagues("UserSelectionChanged", _selectedUser);
        }

        #region DoubleClick Custom Command
        private RelayCommand _mouseDoubleClickCommand;
        public ICommand MouseDoubleClickCommand
        {
            get
            {
                return _mouseDoubleClickCommand ?? (_mouseDoubleClickCommand = new RelayCommand(() => SelectionHasChanged()));
            }
        }
        #endregion

        private void GetUsers()
        {
            DataItems = App.StoreXML.GetUsers();

            if (App.StoreXML.hasError)
                App.Messenger.NotifyColleagues("SetStatus", App.StoreXML.errorMessage);
        }


        private void SaveUser(UserVM p)
        {
            _dataItems.Add(p);
        }

        private void CheckUserExist(UserVM p)
        {
            bool exist = _dataItems.Any(item => item.UserName == p.UserName);
            App.Messenger.NotifyColleagues("UserExist", exist);
        }

        private void UpdateUser(UserVM p)
        {
            int index = _dataItems.IndexOf(_selectedUser);
            _dataItems.ReplaceItem(index, p);
            SelectedUser = p;
        }


        private void DeleteUser()
        {
            _dataItems.Remove(_selectedUser);
        }

        private UserObservableCollection<UserVM> _dataItems;
        public UserObservableCollection<UserVM> DataItems
        {
            get { return _dataItems; }
            //If dataItems replaced by new collection, WPF must be told
            set { _dataItems = value; OnPropertyChanged(new PropertyChangedEventArgs("DataItems")); }
        }

        private UserVM _selectedUser;
        public UserVM SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(new PropertyChangedEventArgs("SelectedUser")); }
        }
        

        private RelayCommand _addNewUserCommand;
        public ICommand AddNewUserCommand
        {
            get { return _addNewUserCommand ?? (_addNewUserCommand = new RelayCommand(() => Call_UserWindow())); }
        }

        private void Call_UserWindow()
        {
            GlobalServices.ModalService.NavigateTo(new UserDisplay(), delegate(bool returnValue)
            {
                if (returnValue)
                    GetUsers();
            });
        }

       

        #region Close App
        //Two Differents Ways to closing the app
        private RelayCommand _closeAppCommand;
        public ICommand CloseAppCommand
        {
            get { return _closeAppCommand ?? (_closeAppCommand = new RelayCommand(() => CloseApp())); }
        }

        private void CloseApp()
        {
            new ApplicationCloseCommand().Execute(this);
        } //CloseApp()
        #endregion


        #region ApplicationCloseCommand
        private static readonly ICommand appCloseCmd = new ApplicationCloseCommand();
        public static ICommand ApplicationCloseCommand
        {
            get { return appCloseCmd; }
        }
        #endregion

    }//class UserSelectActionsModel




}
