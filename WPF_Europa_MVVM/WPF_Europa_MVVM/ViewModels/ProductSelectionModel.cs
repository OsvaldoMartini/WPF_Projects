using System;
using System.ComponentModel;
using System.Windows.Input;
using WPF_Europa_MVVM.Controls;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Views;

namespace WPF_Europa_MVVM.ViewModels
{
    public class ProductSelectionModel : INotifyPropertyChanged
    {

        public ProductSelectionModel()
        {
            dataItems = new ProductObservableCollection<Product>();

            DataItems = App.StoreXML.GetUsers();

            listBoxCommand = new RelayCommand(() => SelectionHasChanged());
            addNewUserCommand = new RelayCommand(() => AddNewUserDisplay());
             

            //By XML
            App.Messenger.Register("UserCleared", (Action)(() => SelectedUser = null));
            App.Messenger.Register("GetUsers", (Action)(() => GetUsers()));
            App.Messenger.Register("UpdateUser", (Action<Product>)(param => UpdateUser(param)));
            App.Messenger.Register("DeleteUser", (Action)(() => DeleteUser()));
            App.Messenger.Register("SaveUser", (Action<Product>)(param => SaveUser(param)));
            App.Messenger.Register("AddNewUser", (Action)(() => AddNewUserDisplay()));
            App.Messenger.Register("Cancel", (Action)(() => CancelWindow()));

        }

        

        private void CancelWindow()
        {
            GlobalServices.ModalService.GoBackward(false);
        }

        private void GetUsers()
        {
            DataItems = App.StoreXML.GetUsers();

            if (App.StoreXML.hasError)
                App.Messenger.NotifyColleagues("SetStatus", App.StoreXML.errorMessage);
        }


        private void SaveUser(Product p)
        {
            dataItems.Add(p);
        }


        private void UpdateUser(Product p)
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

        private ProductObservableCollection<Product> dataItems;
        public ProductObservableCollection<Product> DataItems
        {
            get { return dataItems; }
            //If dataItems replaced by new collection, WPF must be told
            set { dataItems = value; OnPropertyChanged(new PropertyChangedEventArgs("DataItems")); }
        }

        private Product selectedUser;
        public Product SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; OnPropertyChanged(new PropertyChangedEventArgs("SelectedUser")); }
        }




        private RelayCommand addNewUserCommand;
        public ICommand AddNewUserCommand
        {
            get { return addNewUserCommand; }
        }
        private void AddNewUserDisplay()
        {
            GlobalServices.ModalService.NavigateTo(new ProductDisplay(), delegate(bool returnValue)
            {
                if (returnValue)
                    GetUsers();
                //else
                // MessageBox.Show("Return value == false");
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

        #region ApplicationCloseCommand
        private static readonly ICommand appCloseCmd = new ApplicationCloseCommand();
        public static ICommand ApplicationCloseCommand
        {
            get { return appCloseCmd; }
        }
        #endregion

    }//class ProductSelectionModel




}
