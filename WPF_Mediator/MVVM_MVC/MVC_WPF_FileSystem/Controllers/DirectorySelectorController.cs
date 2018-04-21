using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MVC_WPF_FileSystem.Model;

namespace MVC_WPF_FileSystem.Controllers
{
    /// <summary>
    /// Controller for the DirectorySelectorView
    /// </summary>
    public class DirectorySelectorController : BaseController
    {
        #region Properties
        IList<DirectoryDisplayItem> dataSource;
        /// <summary>
        /// Gets the list of items to display
        /// </summary>
        public IList<DirectoryDisplayItem> DataSource
        {
            get { return dataSource; }
            private set
            {
                dataSource = value;
                OnPropertyChanged("DataSource");
            }
        }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectorySelectorController()
        {
            DataSource = (from x in FileSystemDataService.GetRootDirectories()
                          select new DirectoryDisplayItem { Name = x.Name, Path = x.FullName }).ToList();

            //Register to the SelectedIndex Routed event
            EventManager.RegisterClassHandler(typeof(Control), TreeViewItem.ExpandedEvent,
                (RoutedEventHandler)ItemExpanded);

            //Register to the SelectedIndex Routed event
            EventManager.RegisterClassHandler(typeof(Control), TreeView.SelectedItemChangedEvent,
                (RoutedEventHandler)ItemSelected);
        }

        #region Event Handlers
        //event handler for the selecting changed
        void ItemSelected(object sender, RoutedEventArgs e)
        {
            TreeView treeView = (TreeView)e.OriginalSource;
            //Send a message that an item is selected and pass the object selected
            Mediator.NotifyColleagues(Messages.DirectorySelectedChanged, treeView.SelectedItem);
        }

        //event handler for the Item Expanded event
        void ItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem itemExpanded = (TreeViewItem)e.OriginalSource;
            //get the object being expanded
            DirectoryDisplayItem displayItem = (DirectoryDisplayItem)itemExpanded.Header;

            //check if the children of this item are dummy items
            if (displayItem.ContainsDummyItems)
            {
                //if the children are dummy children replace them with the actual data
                displayItem.ChildItems.Clear();
                var modelData = FileSystemDataService.GetChildDirectories(displayItem.Path);
                if (modelData != null)
                {
                    var children = from x in modelData
                                   select new DirectoryDisplayItem { Name = x.Name, Path = x.FullName };

                    //add all the data to the list
                    foreach (var child in children)
                        displayItem.ChildItems.Add(child);
                    displayItem.ContainsDummyItems = false;
                }
            }
        }
        #endregion

        #region IColleague interface
        /// <summary>
        /// Notification from the Mediator
        /// </summary>
        /// <param name="message">The message type</param>
        /// <param name="args">Arguments for the message</param>
        public override void MessageNotification(string message, object args)
        { }
        #endregion
    }
}
