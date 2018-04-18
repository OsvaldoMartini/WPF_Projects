    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Binding.Different.Ways.Properties;

namespace Binding.Different.Ways.Abstract
{
    // The ViewModelBase
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
