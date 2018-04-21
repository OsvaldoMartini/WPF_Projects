using System.Windows.Input;

namespace MVC_WPF_Search
{
    /// <summary>
    /// Where all application commands are defined
    /// </summary>
    public static class Commands
    {
        #region SearchProductByName

        /// <summary>
        /// The SearchProductByName command to search for a set of products by product name.
        /// </summary>
        public static RoutedUICommand SearchProductByName
            = new RoutedUICommand("Search product by name", "SearchProductByName", typeof(Commands));

        #endregion

        #region GetAllProducts

        /// <summary>
        /// The GetAllProducts command , gets all the products.
        /// </summary>
        public static RoutedUICommand GetAllProducts
            = new RoutedUICommand("Get all products", "GetAllProducts", typeof(Commands));

        #endregion


    }
}
