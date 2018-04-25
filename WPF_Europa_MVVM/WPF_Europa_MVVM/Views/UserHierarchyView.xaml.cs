using System.Windows;
using WPF_Europa_MVVM.ViewModels;

namespace WPF_Europa_MVVM.Views
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
            //this.DataContext = OrgTreeViewModel.Instance();
        }
    }
}
