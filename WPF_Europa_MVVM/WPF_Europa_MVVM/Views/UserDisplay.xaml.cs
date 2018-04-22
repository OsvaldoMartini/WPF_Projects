using System.Windows;
using System.Windows.Controls;
using WPF_Europa_MVVM.Foundation;

namespace WPF_Europa_MVVM.Views
{
    /// <summary>
    /// Interaction logic for ProductDisplay.xaml
    /// </summary>
    public partial class UserDisplay : UserControl
    {
        public UserDisplay()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs args)
        {
            GlobalServices.ModalService.GoBackward(true);
        }
        private void CancelButton_Click(object sender, RoutedEventArgs args)
        {
            GlobalServices.ModalService.GoBackward(false);
        }
    }
}
