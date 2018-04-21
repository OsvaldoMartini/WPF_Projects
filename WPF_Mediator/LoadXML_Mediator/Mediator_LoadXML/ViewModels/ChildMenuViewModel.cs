using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Mediator_GoodLoad.Extensions;
using Mediator_GoodLoad.Extensions.MediatorX;
using Mediator_GoodLoad.Models;

namespace Mediator_GoodLoad.ViewModels
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
            get { return _selectedChildMenuItem; }
            set
            {
                _selectedChildMenuItem = value;
                NotifyPropertyChanged("SelectedChildMenuItem");

                LoadSourcePage(); // first instantiate the page (register it to mediator)
                Mediator.NotifyColleagues(Messages.SelectedChildMenuItem, SelectedChildMenuItem); // only now notify
            }
        }

        /// <summary>
        /// Get the page content and notify MainWindowViewModel of the SourcePage
        /// </summary>
        private void LoadSourcePage()
        {
            if (SelectedChildMenuItem != null)
            {
                var sourceUri = (from menuItem in XDocument.Load(Messages.DataDirectory + "MenuItems.xml")
                                                        .Element("MenuItems").Elements("MenuItem").Elements("MenuItem")
                                 where (int)menuItem.Parent.Attribute("Id") == CurrentParentMenuItem.Id &&
                                     (int)menuItem.Attribute("Id") == SelectedChildMenuItem.Id
                                 select menuItem.Element("SourcePage").Value).FirstOrDefault();

                var relativePart = sourceUri.Substring(sourceUri.IndexOf(",,,") + 3);

                var sourcePage = System.Windows.Application.LoadComponent(new Uri(relativePart, UriKind.Relative));
                Mediator.NotifyColleagues(Messages.SourcePage, sourcePage);
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
