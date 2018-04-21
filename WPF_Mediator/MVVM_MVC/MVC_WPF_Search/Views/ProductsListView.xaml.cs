using System.Windows.Controls;
using MVC_WPF_Search.Controllers;

namespace MVC_WPF_Search.Views
{
    /// <summary>
    /// Interaction logic for ProductsListView.xaml
    /// </summary>
    public partial class ProductsListView : UserControl
    {
        public ProductsListView()
        {
            Controller = new ProductListController();
            DataContext = Controller;
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the controller for this view
        /// </summary>
        public ProductListController Controller { get; private set; }
    }
}
