using System.Windows.Controls;

namespace EuropaWPF_App.Interfaces
{
    public delegate void BackNavigationEventHandler(bool dialogReturn);

    public interface IModalService
    {
        void NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog);
     
        void GoBackward(bool dialogReturnValue);
    }
}
