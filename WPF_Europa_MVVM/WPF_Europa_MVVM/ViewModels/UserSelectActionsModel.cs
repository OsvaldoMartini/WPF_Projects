using System;
using System.ComponentModel;
using System.Windows.Input;
using WPF_Europa_MVVM.Controls;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Views;

namespace WPF_Europa_MVVM.ViewModels
{
    public class UserSelectActionsModel : INotifyPropertyChanged
    {

        public UserSelectActionsModel()
        {
            dataItems = new UserObservableCollection<UserVM>();

            DataItems = App.StoreXML.GetUsers();

            listBoxCommand = new RelayCommand(() => SelectionHasChanged());
            
            //Messengers / messages
            App.Messenger.Register("UserCleared", (Action)(() => SelectedUser = null));
            App.Messenger.Register("GetUsers", (Action)(() => GetUsers()));
            App.Messenger.Register("UpdateUser", (Action<UserVM>)(param => UpdateUser(param)));
            App.Messenger.Register("DeleteUser", (Action)(() => DeleteUser()));
            App.Messenger.Register("SaveUser", (Action<UserVM>)(param => SaveUser(param)));
        }

        private void GetUsers()
        {
            DataItems = App.StoreXML.GetUsers();

            if (App.StoreXML.hasError)
                App.Messenger.NotifyColleagues("SetStatus", App.StoreXML.errorMessage);
        }


        private void SaveUser(UserVM p)
        {
            dataItems.Add(p);
        }


        private void UpdateUser(UserVM p)
        {
            int index = dataItems.IndexOf(selectedUser);
            dataItems.ReplaceItem(index, p);
            SelectedUser = p;
        }


        private void DeleteUser()
        {
            dataItems.Remove(selectedUser);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private UserObservableCollection<UserVM> dataItems;
        public UserObservableCollection<UserVM> DataItems
        {
            get { return dataItems; }
            //If dataItems replaced by new collection, WPF must be told
            set { dataItems = value; OnPropertyChanged(new PropertyChangedEventArgs("DataItems")); }
        }

        private UserVM selectedUser;
        public UserVM SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; OnPropertyChanged(new PropertyChangedEventArgs("SelectedUser")); }
        }
        

        private RelayCommand addNewUserCommand;
        public ICommand AddNewUserCommand
        {
            get { return addNewUserCommand ?? (addNewUserCommand = new RelayCommand(() => AddNewUserDisplay())); }
        }

        private void AddNewUserDisplay()
        {
            GlobalServices.ModalService.NavigateTo(new UserDisplay(), delegate(bool returnValue)
            {
                if (returnValue)
                    GetUsers();
            });
        }

        private RelayCommand listBoxCommand;
        public ICommand ListBoxCommand
        {
            get { return listBoxCommand; }
        }

        private void SelectionHasChanged()
        {
            Messenger messenger = App.Messenger;
            messenger.NotifyColleagues("UserSelectionChanged", selectedUser);
        }

        

        #region Close App
        //Two Differents Ways to closing the app
        private RelayCommand closeAppCommand;
        public ICommand CloseAppCommand
        {
            get { return closeAppCommand ?? (closeAppCommand = new RelayCommand(() => CloseApp())); }
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
