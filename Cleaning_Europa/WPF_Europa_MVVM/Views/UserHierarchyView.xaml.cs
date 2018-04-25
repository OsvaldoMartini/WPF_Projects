using System.Windows;
using EuropaWPF_App.ViewModels;

namespace EuropaWPF_App.Views
{
    /// <summary>
    /// Interaction logic for UserHierarchyView.xaml
    /// </summary>
    public partial class UserHierarchyView : Window
    {
        public UserHierarchyView()
        {
            InitializeComponent();
            //I left this line of code just do speed up the test delivery
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
