using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Europa_MVVM.Foundation;

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
