using System.ComponentModel;

namespace Modal.Interfaces
{
    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsModified { get; set; }

        bool IsLoading { get; set; }
    }
}
