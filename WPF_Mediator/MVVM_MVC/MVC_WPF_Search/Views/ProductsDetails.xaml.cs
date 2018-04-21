using System.Windows.Controls;
using MVC_WPF_Search.Controllers;

namespace MVC_WPF_Search.Views
{
    /// <summary>
    /// Interaction logic for ProductsDetails.xaml
    /// </summary>
    public partial class ProductsDetails : UserControl
    {
        
        public ProductsDetails()
        {
            Controller = new ProductDetailsController();
            DataContext = Controller;
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the controller for this view
        /// </summary>
        public ProductDetailsController Controller { get; private set; }
    }
}
