using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MVC_WPF_Search.Model;

namespace MVC_WPF_Search.Controllers
{
    /// <summary>
    /// Controller for the product list
    /// </summary>
    public class ProductListController : BaseController
    {
        #region Properties

        IList<Product> productList;
        /// <summary>
        /// Gets a product list that the View can bind to
        /// </summary>
        public IList<Product> ProductList 
        {
            get { return productList; }
            private set
            {
                productList = value;
                //Notify that the Product List has changed so that binding is updated
                OnPropertyChanged("ProductList");
            }
        }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductListController()
        {
            //Register to the search by name command and the get all products
            Mediator.Register(this, new[]
                {
                    Messages.SearchByName,
                    Messages.GetAllProducts
                });

            //Register to the SelectedIndex Routed event
            EventManager.RegisterClassHandler(typeof(Control), ListView.SelectionChangedEvent, 
                (SelectionChangedEventHandler)SelectionChanged);

            //Get the full list of products by default
            GetAllProducts();
        }

        #region Event Handler
        //event handler for the selection changed
        void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Notify that the selected item has changed
            if(e.AddedItems != null && e.AddedItems.Count > 0)
                Mediator.NotifyColleagues(Messages.SelectProduct, e.AddedItems[0]);
        }
        #endregion

        /// <summary>
        /// Notification from the Mediator
        /// </summary>
        /// <param name="message">The message type</param>
        /// <param name="args">Arguments for the message</param>
        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.SearchByName:
                    ApplySearchByName(args.ToString());
                    break;

                case Messages.GetAllProducts:
                    GetAllProducts();
                    break;
            }
        }

        /// <summary>
        /// Applies a seach by the product name
        /// </summary>
        /// <param name="productName">The product name to search with</param>
        public void ApplySearchByName(string productName)
        {
            ProductList = ProductsDataService.GetProductsByName(productName);
        }

        /// <summary>
        /// Gets all the products
        /// </summary>
        public void GetAllProducts()
        {
            ProductList = ProductsDataService.GetAllProducts();
        }
    }
}
