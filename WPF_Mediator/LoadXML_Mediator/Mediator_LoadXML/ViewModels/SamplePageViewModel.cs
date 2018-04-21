using Mediator_GoodLoad.Extensions;
using Mediator_GoodLoad.Extensions.MediatorX;
using Mediator_GoodLoad.Models;

namespace Mediator_GoodLoad.ViewModels
{
    public class SamplePageViewModel : ViewModelBase
    {
        public SamplePageViewModel()
        {
            Mediator.Register(this, new[] { Messages.SelectedChildMenuItem });
        }

        private MenuItem _currentChildMenuItem;
        public MenuItem CurrentChildMenuItem
        {
            get
            {
                return _currentChildMenuItem;
            }
            set
            {
                _currentChildMenuItem = value;
                NotifyPropertyChanged("CurrentChildMenuItem");
            }
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.SelectedChildMenuItem:
                    CurrentChildMenuItem = (MenuItem)args;
                    break;
            }
        }
    }
}
