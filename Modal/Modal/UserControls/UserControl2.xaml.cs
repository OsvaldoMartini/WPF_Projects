using System.Windows;
using Modal.Concrete;
using Modal.ViewModel;
using Controls = System.Windows.Controls;

namespace Modal.UserControls {
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>

    public partial class UserControl2 : Controls.UserControl
    {

        //If you get this example you'll get 90% of MVVM.
        public IntermediateMessages MessagesModel { get; set; }

        public UserControl2(IntermediateMessages model)
        {
            InitializeComponent();

            //If you get this example you'll get 90% of MVVM.
            MessagesModel = model;
            this.GridUserCrtl2.DataContext = MessagesModel;
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