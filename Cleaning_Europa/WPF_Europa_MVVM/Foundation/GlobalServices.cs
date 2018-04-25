using System.Windows;
using WPF_Europa_MVVM.Interfaces;

namespace WPF_Europa_MVVM.Foundation
{
    public class GlobalServices
    {
        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
    }
}
