using System.Collections.Generic;
using System.IO;
using MVC_WPF_FileSystem.Model;

namespace MVC_WPF_FileSystem.Controllers
{
    /// <summary>
    /// Controller for the Directory structure
    /// </summary>
    public class FileSelectorController : BaseController
    {
        #region Properties
        IList<FileInfo> dataSource;
        /// <summary>
        /// Gets or sets the DataSource that contians the list of files
        /// </summary>
        public IList<FileInfo> DataSource
        {
            get { return dataSource; }
            set 
            {
                dataSource = value;
                OnPropertyChanged("DataSource");
            }
        }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileSelectorController()
        {
            Mediator.Register(this, new[]
            {
                Messages.DirectorySelectedChanged
            });
        }

        #region IColleague interface
        /// <summary>
        /// Notification from the Mediator
        /// </summary>
        /// <param name="message">The message type</param>
        /// <param name="args">Arguments for the message</param>
        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.DirectorySelectedChanged:
                    //load all files for the directory specified
                    LoadFiles((DirectoryDisplayItem)args);
                    break;
            }
        }
        #endregion

        //load all the files for the directory
        private void LoadFiles(DirectoryDisplayItem dir)
        {
            if (dir != null)
            {
                DataSource = FileSystemDataService.GetChildFiles(dir.Path);
            }
        }
    }
}
