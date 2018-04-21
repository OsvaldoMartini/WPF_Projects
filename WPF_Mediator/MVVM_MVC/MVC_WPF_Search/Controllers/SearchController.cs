using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVC_WPF_Search.Controllers
{
    /// <summary>
    /// Controller for the Search
    /// </summary>
    public class SearchController : BaseController
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchController()
        {
            //register the search product by name command
            CommandManager.RegisterClassCommandBinding(typeof(Control),
                new CommandBinding(Commands.SearchProductByName, ExecuteSearchByName, CanExecuteSearchByName));

            //register to the Get all products command
            CommandManager.RegisterClassCommandBinding(typeof(Control),
                new CommandBinding(Commands.GetAllProducts, GetAllProducts));
        }

        #region Command handlers
        //execute handler for the SearchProductByName command
        void ExecuteSearchByName(object sender, ExecutedRoutedEventArgs e)
        {
            //notify other colleagues that the user wants to search by name. 
            //Also pass the name to search with, as parameter
            Mediator.NotifyColleagues(Messages.SearchByName, e.Parameter.ToString());
        }

        //can execute handler for the SearchProductByName command
        void CanExecuteSearchByName(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter == null)
                e.CanExecute = false;
            else
                e.CanExecute = !String.IsNullOrEmpty(
                    e.Parameter.ToString());
        }

        //event handler for the get all products command
        void GetAllProducts(object sender, ExecutedRoutedEventArgs e)
        {
            //pass a message to get all products
            Mediator.NotifyColleagues(Messages.GetAllProducts, null);
        }
        #endregion


        /// <summary>
        /// Notification from the Mediator
        /// </summary>
        /// <param name="message">The message type</param>
        /// <param name="args">Arguments for the message</param>
        public override void MessageNotification(string message, object args)
        {
        }
    }
}
