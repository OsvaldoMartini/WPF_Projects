using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Mediator_BugOnLoad.Extensions;
using Mediator_BugOnLoad.Extensions.MediatorX;
using Mediator_BugOnLoad.Models;

namespace Mediator_BugOnLoad.ViewModels
{
    public class ParentMenuViewModel : ViewModelBase
    {
        public ParentMenuViewModel()
        {
            ParentMenuItems = new ObservableCollection<MenuItem>(
                                                                    from menuItem in XDocument.Load(Messages.DataDirectory + "MenuItems.xml")
                                                                                              .Element("MenuItems").Elements("MenuItem")
                                                                    select new MenuItem
                                                                    {
                                                                        Id = Convert.ToInt32(menuItem.Attribute("Id").Value),
                                                                        Name = menuItem.Element("Name").Value
                                                                    }
                                                                );
        }

        private ObservableCollection<MenuItem> _parentMenuItems;
        public ObservableCollection<MenuItem> ParentMenuItems
        {
            get
            {
                return _parentMenuItems;
            }
            set
            {
                _parentMenuItems = value;
                NotifyPropertyChanged("ParentMenuItems");
            }
        }

        private MenuItem _selectedParentMenuItem;
        public MenuItem SelectedParentMenuItem
        {
            get
            {
                return _selectedParentMenuItem;
            }
            set
            {
                _selectedParentMenuItem = value;
                NotifyPropertyChanged("SelectedParentMenuItem");

                Mediator.NotifyColleagues(Messages.SelectedParentMenuItem, SelectedParentMenuItem);
            }
        }

        public override void MessageNotification(string message, object args)
        {
            throw new NotImplementedException();
        }
    }
}
