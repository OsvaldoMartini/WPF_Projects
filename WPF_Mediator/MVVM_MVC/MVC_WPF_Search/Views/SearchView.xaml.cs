using System.Windows.Controls;
using MVC_WPF_Search.Controllers;

namespace MVC_WPF_Search.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            Controller = new SearchController();
            DataContext = Controller;
            InitializeComponent();
        }

        /// <summary>
        /// Gets the controller for this view 
        /// </summary>
        public SearchController Controller { get; private set; }
    }
}
