using Mediator_GoodLoad.Extensions;
using Mediator_GoodLoad.Extensions.MediatorX;
using Mediator_GoodLoad.Models;

namespace Mediator_GoodLoad.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Mediator.Register(this, new[] { Messages.SelectedParentMenuItem, Messages.SelectedChildMenuItem, Messages.SourcePage });
        }

        private object _sourcePage;
        public object SourcePage
        {
            get
            {
                return _sourcePage;
            }
            set
            {
                _sourcePage = value;
                NotifyPropertyChanged("SourcePage");
            }
        }

        private MenuItem _currentParentMenuItem;
        public MenuItem CurrentParentMenuItem
        {
            get
            {
                return _currentParentMenuItem;
            }
            set
            {
                _currentParentMenuItem = value;
                NotifyPropertyChanged("CurrentParentMenuItem");
            }
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
                case Messages.SelectedParentMenuItem:
                    CurrentParentMenuItem = (MenuItem)args;
                    break;
                case Messages.SelectedChildMenuItem:
                    CurrentChildMenuItem = (MenuItem)args;
                    break;
                case Messages.SourcePage:
                    SourcePage = args;
                    break;
            }
        }
    }
}
