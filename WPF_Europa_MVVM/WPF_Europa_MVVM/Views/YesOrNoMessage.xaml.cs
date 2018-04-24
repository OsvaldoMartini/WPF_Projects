using System.Windows;
using System.Windows.Controls;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.ViewModels;

namespace WPF_Europa_MVVM.Views
{
    /// <summary>
    /// Interaction logic for YesOrNoMessage.xaml
    /// </summary>
    public partial class YesOrNoMessage : UserControl
    {
        //90% of MVVM.
        public IntermediateMsgVM MessagesModel { get; set; }

        public YesOrNoMessage(IntermediateMsgVM model)
        {
            InitializeComponent();

            //get 90% of MVVM.
            MessagesModel = model;
            this.CancelButton.Visibility = model.BtnCancelVisibility;
            this.GridYesOrNo.DataContext = MessagesModel;
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
