using System.Windows;
using EuropaWPF_App.Interfaces;

namespace EuropaWPF_App.Foundation
{
    public class GlobalServices
    {
        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
    }
}
