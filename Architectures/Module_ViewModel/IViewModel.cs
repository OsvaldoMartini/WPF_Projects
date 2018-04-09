using System.ComponentModel;

namespace Modules_ViewModel
{
    public interface IViewModel: INotifyPropertyChanged
    {
        bool IsModified { get; set; }

        bool IsLoading { get; set; }
    }
}
