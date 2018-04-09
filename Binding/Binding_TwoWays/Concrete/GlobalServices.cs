using System.Windows;
using Binding.Basics.TwoWays.Interfaces;

namespace Binding.Basics.TwoWays.Concrete
{
    public class GlobalServices
    {
        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
    }
}
