using System;
using System.ComponentModel;
using System.Windows.Input;
using WPF_Europa_MVVM.Foundation;

namespace WPF_Europa_MVVM.ViewModels
{
    public class ProductDisplayModel : INotifyPropertyChanged
    {
        private bool isSelected = false;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        //data checks and status indicators done in another class
        private readonly ProductDisplayModelStatus stat = new ProductDisplayModelStatus();
        public ProductDisplayModelStatus Stat { get { return stat; } }

        private Product displayedProduct = new Product();
        public Product DisplayedProduct
        {
            get { return displayedProduct; }
            set { displayedProduct = value; OnPropertyChanged(new PropertyChangedEventArgs("DisplayedProduct")); }
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
            DisplayedProduct = new Product();
            App.Messenger.NotifyColleagues("GetUsers");
        }

        private RelayCommand closeAppCommand;
        public ICommand CloseAppCommand
        {
            get { return closeAppCommand ?? (closeAppCommand = new RelayCommand(() => CloseApp()/*, ()=>isSelected*/)); }
        }

        private void CloseApp()
        {
            isSelected = false;
            stat.NoError();
            DisplayedProduct = new Product();
            App.Messenger.NotifyColleagues("UserCleared");
        } //ClearProductDisplay()

        #region AddNewUserCommand
        private RelayCommand addNewUserCommand;
        public ICommand AddNewUserCommand
        {
            get { return addNewUserCommand ?? (addNewUserCommand = new RelayCommand(() => AddNewUserDisplay()/*, ()=>isSelected*/)); }
        }

        private void AddNewUserDisplay()
        {
            isSelected = false;
            stat.NoError();
            DisplayedProduct = new Product();
            App.Messenger.NotifyColleagues("UserCleared");
        } //AddNewUserDisplay()
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
            DisplayedProduct = new Product();
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
            if (!stat.ChkProductForUpdate(DisplayedProduct)) return;
                if(!App.StoreXML.UpdateUser(DisplayedProduct))
                {
                    stat.Status = App.StoreXML.errorMessage;
                    return;
                }
                App.Messenger.NotifyColleagues("UpdateUser", DisplayedProduct);
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
            if (!App.StoreXML.DeleteUser(DisplayedProduct._ProductId))
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

        private void SaveUser()
        {
            if (!stat.ChkProductForAdd(DisplayedProduct)) return;
            if (!App.StoreXML.SaveUser(DisplayedProduct))
            {
                stat.Status = App.StoreXML.errorMessage;
                return;
            }
            App.Messenger.NotifyColleagues("SaveUser", DisplayedProduct);
        } //SaveUser()
        #endregion

        public ProductDisplayModel()
        {
            Messenger messenger = App.Messenger;
            messenger.Register("UserSelectionChanged", (Action<Product>)(param => ProcessUser(param)));
            messenger.Register("SetStatus", (Action<String>)(param => stat.Status = param));
        } //ctor

        public void ProcessUser(Product p)
        {
            if (p == null) { /*DisplayedProduct = null;*/  isSelected = false;  return; }
            Product temp = new Product();
            temp.CopyProduct(p);
            DisplayedProduct = temp;
            isSelected = true;
            stat.NoError();
        } // ProcessUser()
    }
}
