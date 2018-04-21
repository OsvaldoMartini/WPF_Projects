using System.ComponentModel;

namespace MVVM_Helper.Common
{
    /// <summary>
    /// Base class that implements the INotifyPropertyChanged interface for databinding
    /// </summary>
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
