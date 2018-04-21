using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Mediator_BugOnLoad.Extensions;
using Mediator_BugOnLoad.Extensions.MediatorX;
using Mediator_BugOnLoad.Models;

namespace Mediator_BugOnLoad.ViewModels
{
    public class ChildMenuViewModel : ViewModelBase
    {
        public ChildMenuViewModel()
        {
            Mediator.Register(this, new[] { Messages.SelectedParentMenuItem });
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

                ChildMenuItemsOfSelectedParent
                        = new ObservableCollection<MenuItem>(
                                                                from menuItem in XDocument.Load(Messages.DataDirectory + "MenuItems.xml")
                                                                                          .Element("MenuItems").Elements("MenuItem").Elements("MenuItem")
                                                                where (int)menuItem.Parent.Attribute("Id") == CurrentParentMenuItem.Id
                                                                select new MenuItem
                                                                {
                                                                    Id = Convert.ToInt32(menuItem.Attribute("Id").Value),
                                                                    Name = menuItem.Element("Name").Value,
                                                                }
                                                            );

            }
        }

        private ObservableCollection<MenuItem> _childMenuItemsOfSelectedParent;
        public ObservableCollection<MenuItem> ChildMenuItemsOfSelectedParent
        {
            get
            {
                return _childMenuItemsOfSelectedParent;
            }
            set
            {
                _childMenuItemsOfSelectedParent = value;
                NotifyPropertyChanged("ChildMenuItemsOfSelectedParent");
            }
        }

        private MenuItem _selectedChildMenuItem;
        public MenuItem SelectedChildMenuItem
        {
            get
            {
                return _selectedChildMenuItem;
            }
            set
            {
                _selectedChildMenuItem = value;
                NotifyPropertyChanged("SelectedChildMenuItem");

                Mediator.NotifyColleagues(Messages.SelectedChildMenuItem, SelectedChildMenuItem);
            }
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.SelectedParentMenuItem:
                    CurrentParentMenuItem = (MenuItem)args;
                    break;
            }
        }
    }
}
