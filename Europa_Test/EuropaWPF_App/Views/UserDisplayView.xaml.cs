using System.Windows.Controls;
using EuropaWPF_App.ViewModels;

namespace EuropaWPF_App.Views
{
    /// <summary>
    /// Interaction logic for UserDisplay.xaml
    /// </summary>
    public partial class UserDisplayView : UserControl
    {
        public UserDisplayView()
        {
            InitializeComponent();
           this.DataContext = UserDisplayVm.Instance();
        }

    }
}
