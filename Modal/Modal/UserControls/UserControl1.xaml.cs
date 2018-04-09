using System.Windows;
using Modal.Concrete;
using Modal.ViewModel;

namespace Modal.UserControls {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>

    public partial class UserControl1 : System.Windows.Controls.UserControl {

        //If you get this example you'll get 90% of MVVM.
        public IntermediateMessages MessagesModel { get; set; }
        public UserControl1(IntermediateMessages model) {
            InitializeComponent();

            MessagesModel = model;
        }

        private void ButtonClick(object sender, RoutedEventArgs args) {
            GlobalServices.ModalService.GoBackward(true);
        }
        private void CancelClick(object sender, RoutedEventArgs args) {
            GlobalServices.ModalService.GoBackward(false);
        }
        private void Screen2_Click(object sender, RoutedEventArgs args)
        {
            GlobalServices.ModalService.NavigateTo(new UserControl2(MessagesModel), delegate(bool returnValue)
            {
                if (returnValue)
                    MessageBox.Show("Return value == true");
                else
                    MessageBox.Show("Return value == false");            
            });
        }
    }
}