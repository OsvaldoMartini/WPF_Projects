using System.Windows.Controls;

namespace Domains_DLL.Interfaces
{
    public delegate void BackNavigationEventHandler(bool dialogReturn);

    public interface IModalService
    {
        void NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog);
        void GoBackward(bool dialogReturnValue);
    }
}
