using System.Linq;
using System.Xml.Linq;
using Mediator_BugOnLoad.Extensions;
using Mediator_BugOnLoad.Extensions.MediatorX;
using Mediator_BugOnLoad.Models;

namespace Mediator_BugOnLoad.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Mediator.Register(this, new[] { Messages.SelectedParentMenuItem, Messages.SelectedChildMenuItem });
        }

        private string _sourcePage;
        public string SourcePage
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

                if (CurrentChildMenuItem != null)
                {
                    SourcePage = (from menuItem in XDocument.Load(Messages.DataDirectory + "MenuItems.xml")
                                                            .Element("MenuItems").Elements("MenuItem").Elements("MenuItem")
                                  where (int)menuItem.Parent.Attribute("Id") == CurrentParentMenuItem.Id &&
                                        (int)menuItem.Attribute("Id") == CurrentChildMenuItem.Id
                                  select menuItem.Element("SourcePage").Value).FirstOrDefault();
                }
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
            }
        }
    }
}
