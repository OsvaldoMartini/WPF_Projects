using System.Windows;
using System.Windows.Controls;
using EuropaWPF_App.Foundation;
using EuropaWPF_App.ViewModels;

namespace EuropaWPF_App.Views
{
    /// <summary>
    /// Interaction logic for YesOrNoMessage.xaml
    /// </summary>
    public partial class YesOrNoMessage : UserControl
    {
        //90% of MVVM.
        public IntermediateMsgVm MessagesModel { get; set; }

        public YesOrNoMessage(IntermediateMsgVm model)
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
