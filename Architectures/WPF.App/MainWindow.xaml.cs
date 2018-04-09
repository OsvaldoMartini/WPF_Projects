using System.Windows;
using Modules_ViewModel;

namespace WPF.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserViewModel user = new UserViewModel();
            user.UserName = "User";
            user.IsModified = false;
            DataContext = user;
        }
    }
}
