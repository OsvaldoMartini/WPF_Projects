using System.Windows.Controls;
using MVC_WPF_FileSystem.Controllers;

namespace MVC_WPF_FileSystem.Views
{
    /// <summary>
    /// Interaction logic for FileSelectorView.xaml
    /// </summary>
    public partial class FileSelectorView : UserControl
    {
        public FileSelectorView()
        {
            Controller = new FileSelectorController();
            DataContext = Controller;
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the controller
        /// </summary>
        public FileSelectorController Controller { get; set; }
    }
}
