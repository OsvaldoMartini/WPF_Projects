using System;
using System.ComponentModel;
using System.Windows.Input;
using Products_SQL_Linq.Controls;
using Products_SQL_Linq.Foundation;

namespace Products_SQL_Linq.ViewModels
{
    public class ProductSelectionVM : INotifyPropertyChanged
    {

        public ProductSelectionVM()
        {
            dataItems = new ProductObservableCollection<ProductVM>();
            
            //DataItems = App.StoreDB.GetProducts();
            DataItems = App.StoreDB.GetProductsByXML();

            listBoxCommand = new RelayCommand(() => SelectionHasChanged());
            App.Messenger.Register("ProductCleared", (Action)(() => SelectedProduct=null));
            App.Messenger.Register("GetProducts", (Action)(() => GetProducts()));
            App.Messenger.Register("UpdateProduct",  (Action<ProductVM>)(param => UpdateProduct(param)));
            App.Messenger.Register("DeleteProduct", (Action)(() => DeleteProduct()));
            App.Messenger.Register("AddProduct", (Action<ProductVM>)(param => AddProduct(param)));
            //By XML
            App.Messenger.Register("GetProductsByXML", (Action)(() => GetProductsByXML()));
        }


        private void GetProducts()
        {
            DataItems = App.StoreDB.GetProducts();
            
            if (App.StoreDB.hasError)
                App.Messenger.NotifyColleagues("SetStatus", App.StoreDB.errorMessage);
        }

        private void GetProductsByXML()
        {
            DataItems = App.StoreDB.GetProductsByXML();

            if (App.StoreDB.hasError)
                App.Messenger.NotifyColleagues("SetStatus", App.StoreDB.errorMessage);
        }


        private void AddProduct(ProductVM p)
        {
            dataItems.Add(p);
        }


        private void UpdateProduct(ProductVM p)
        {
            int index = dataItems.IndexOf(selectedProduct);
            dataItems.ReplaceItem(index, p);
            SelectedProduct = p;
        }


        private void DeleteProduct()
        {
            dataItems.Remove(selectedProduct);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private ProductObservableCollection<ProductVM> dataItems;
        public ProductObservableCollection<ProductVM> DataItems
        {
            get { return dataItems; }
            //If dataItems replaced by new collection, WPF must be told
            set { dataItems = value; OnPropertyChanged(new PropertyChangedEventArgs("DataItems")); }
        }

        private ProductVM selectedProduct;
        public ProductVM SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; OnPropertyChanged(new PropertyChangedEventArgs("SelectedProduct")); }
        }

        private RelayCommand listBoxCommand;
        public ICommand ListBoxCommand
        {
            get { return listBoxCommand; }
        }

        private void SelectionHasChanged()
        {
            Messenger messenger = App.Messenger;
            messenger.NotifyColleagues("ProductSelectionChanged", selectedProduct);
        }
    }//class ProductSelectionModel



    
}
