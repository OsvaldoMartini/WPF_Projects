using System.Windows;
using Domains_DLL.Interfaces;

namespace Domains_DLL.Helper
{
    public class GlobalServices
    {
        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
    }
}
