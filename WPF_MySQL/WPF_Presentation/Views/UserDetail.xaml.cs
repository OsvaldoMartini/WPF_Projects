using System.Windows;
using System.Windows.Controls;
using Domains_DLL.Helper;
using Domains_DLL.ViewModel;

namespace WPF_Presentation.Views
{
    /// <summary>
    /// Interaction logic for UserDetail.xaml
    /// </summary>
    public partial class UserDetail : UserControl
    {
        //If you get this example you'll get 90% of MVVM.
        public IntermediateMessages MessagesModel { get; set; }
        public UserDetail(IntermediateMessages model) {
            InitializeComponent();

            MessagesModel = model;
        }

        private void ButtonClick(object sender, RoutedEventArgs args) {
            GlobalServices.ModalService.GoBackward(true);
        }
        private void CancelClick(object sender, RoutedEventArgs args) {
            GlobalServices.ModalService.GoBackward(false);
        }
       
    }

}
