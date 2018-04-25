using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Interfaces;

namespace WPF_Europa_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IModalService
    {
        public MainWindow()
        {
            InitializeComponent();

            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        #region IMainWindow Members

        private Stack<BackNavigationEventHandler> _backFunctions
            = new Stack<BackNavigationEventHandler>();

        void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        {
            foreach (UIElement item in BaseModalGrid.Children)
                item.IsEnabled = false;
            BaseModalGrid.Children.Add(uc);

            _backFunctions.Push(backFromDialog);
        }

        void IModalService.GoBackward(bool dialogReturnValue)
        {
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
