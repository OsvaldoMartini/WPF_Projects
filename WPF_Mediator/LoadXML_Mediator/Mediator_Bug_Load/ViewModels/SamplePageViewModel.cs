using Mediator_BugOnLoad.Extensions;
using Mediator_BugOnLoad.Extensions.MediatorX;
using Mediator_BugOnLoad.Models;

namespace Mediator_BugOnLoad.ViewModels
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
