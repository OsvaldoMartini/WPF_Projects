using System;
using WPF_Europa_MVVM.Controls;
using WPF_Europa_MVVM.ViewModels;

namespace WPF_Europa_MVVM.Model
{
    public class StoreXML
    {
        public bool hasError = false;
        public string errorMessage;

        public bool DeleteUser(int p)
        {
            try
            {
                return new ProductModel().WhitProductId(p).DeleteByXML();

            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
            }

            return (!hasError);
        }//DeleteUserByXML


        public bool SaveUser(Product DisplayedProduct)
        {
            try
            {
                return new ProductModel(DisplayedProduct).AddByXML();

            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
            }

            return (!hasError);
        }//SaveUserByXML

        public bool UpdateUser(Product displayP)
        {

            try
            {
                return new ProductModel(displayP).UpdateByXML();

            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
            }

            return (!hasError);
        }//UpdateUserByXML


        public ProductObservableCollection<Product> GetUsers()
        {
            hasError = false;
            ProductObservableCollection<Product> products = new ProductObservableCollection<Product>();
            try
            {
                //True: Check if File Exist
                products = new ProductModel().WhitCheckFileFirst(true);

            } //try
            catch (Exception ex)
            {
                errorMessage = "GetUsers() error, " + ex.Message;
                hasError = true;
            }

            return products;
        }//GetUsersByXML

    }
} 

