using System.ComponentModel;
using Mediator_GoodLoad.Extensions.MediatorX;

namespace Mediator_GoodLoad.Extensions
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IColleague
    {

        public ViewModelBase()
        {
            Mediator = mediatorInstance;
        }

        static Mediator mediatorInstance = new Mediator();

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IColleague

        public Mediator Mediator { get; private set; }

        public abstract void MessageNotification(string message, object args);

        #endregion
    }
}
