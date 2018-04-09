using System.Windows;
using System.Windows.Controls;
using Binding.Basics.TwoWays.Concrete;

namespace Binding.Basics.TwoWays
{
    /// <summary>
    /// Interaction logic for PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : UserControl
    {
        public PersonWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs args)
        {
            GlobalServices.ModalService.GoBackward(true);
        }
        private void CancelClick(object sender, RoutedEventArgs args)
        {
            GlobalServices.ModalService.GoBackward(false);
        }

    }
}
