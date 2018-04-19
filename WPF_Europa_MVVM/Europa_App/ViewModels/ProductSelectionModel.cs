using System;
using System.ComponentModel;
using System.Windows.Input;
using Europa_App.Controls;
using Europa_App.Foundation;

namespace Europa_App.ViewModels
{
    public class ProductSelectionModel : INotifyPropertyChanged
    {

        public ProductSelectionModel()
        {
            dataItems = new ProductObservableCollection<Product>();
            
            //DataItems = App.StoreDB.GetProducts();
            DataItems = App.StoreDB.GetProductsByXML();

            listBoxCommand = new RelayCommand(() => SelectionHasChanged());
            App.Messenger.Register("ProductCleared", (Action)(() => SelectedProduct=null));
            App.Messenger.Register("GetProducts", (Action)(() => GetProducts()));
            App.Messenger.Register("UpdateProduct",  (Action<Product>)(param => UpdateProduct(param)));
            App.Messenger.Register("DeleteProduct", (Action)(() => DeleteProduct()));
            App.Messenger.Register("AddProduct", (Action<Product>)(param => AddProduct(param)));
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


        private void AddProduct(Product p)
        {
            dataItems.Add(p);
        }


        private void UpdateProduct(Product p)
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

        private ProductObservableCollection<Product> dataItems;
        public ProductObservableCollection<Product> DataItems
        {
            get { return dataItems; }
            //If dataItems replaced by new collection, WPF must be told
            set { dataItems = value; OnPropertyChanged(new PropertyChangedEventArgs("DataItems")); }
        }

        private Product selectedProduct;
        public Product SelectedProduct
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
