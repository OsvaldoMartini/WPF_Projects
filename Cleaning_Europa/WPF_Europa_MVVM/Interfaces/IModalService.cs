using System.Windows.Controls;

namespace WPF_Europa_MVVM.Interfaces
{
    public delegate void BackNavigationEventHandler(bool dialogReturn);

    public interface IModalService
    {
        void NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog);
     
        void GoBackward(bool dialogReturnValue);
    }
}
