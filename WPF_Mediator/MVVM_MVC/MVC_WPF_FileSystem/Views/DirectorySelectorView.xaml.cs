using System.Windows.Controls;
using MVC_WPF_FileSystem.Controllers;

namespace MVC_WPF_FileSystem.Views
{
    /// <summary>
    /// Interaction logic for DirectorySelectorView.xaml
    /// </summary>
    public partial class DirectorySelectorView : UserControl
    {
        public DirectorySelectorView()
        {
            Controller = new DirectorySelectorController();
            DataContext = Controller;
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the Directory Selector controller
        /// </summary>
        public DirectorySelectorController Controller { get; set; }
    }
}
