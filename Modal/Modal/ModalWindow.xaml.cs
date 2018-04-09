using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Modal.Concrete;
using Modal.Interfaces;
using Modal.UserControls;
using Modal.ViewModel;

namespace Modal {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class ModalWindow : Window, IModalService
    {
        //If you get this example you'll get 90% of MVVM.
        public IntermediateMessages MessagesModel { get; set; }

        public ModalWindow() {
            InitializeComponent();
            SetDataContext();

            SetUserModule_ViewModel();
        }

        private void SetUserModule_ViewModel()
        {
            UserViewModel user = new UserViewModel();
            user.UserName = "User";
            user.IsModified = false;
            //DataContext = user;
            this.GridUserViewModel.DataContext = user;
        }

        public void SetDataContext()
        {
            //If you get this example you'll get 90% of MVVM.
            MessagesModel = new IntermediateMessages();

            MessagesModel.MessageInternal = "Internal Message";
            MessagesModel.MessageScreenTransfer = "Screen Transfer";
            MessagesModel.MessageId = 1;

            this.GridRow1.DataContext = MessagesModel;

        }

        private void ModalClick(object sender, RoutedEventArgs args) {
            GlobalServices.ModalService.NavigateTo(new UserControl1(MessagesModel), delegate(bool returnValue) {
                if (returnValue)
                    MessageBox.Show("Return value == true");
                else
                    MessageBox.Show("Return value == false");
            });
        }

        #region IMainWindow Members

        private Stack<BackNavigationEventHandler> _backFunctions
            = new Stack<BackNavigationEventHandler>();

        void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog) {
            foreach (UIElement item in BaseModalGrid.Children)
                item.IsEnabled = false;
            BaseModalGrid.Children.Add(uc);

            _backFunctions.Push(backFromDialog);
        }

        void IModalService.GoBackward(bool dialogReturnValue) {
            BaseModalGrid.Children.RemoveAt(BaseModalGrid.Children.Count - 1);

            UIElement element = BaseModalGrid.Children[BaseModalGrid.Children.Count - 1];
            element.IsEnabled = true;

            BackNavigationEventHandler handler = _backFunctions.Pop();
            if (handler != null)
                handler(dialogReturnValue); 
        }

        #endregion
    }
    
}