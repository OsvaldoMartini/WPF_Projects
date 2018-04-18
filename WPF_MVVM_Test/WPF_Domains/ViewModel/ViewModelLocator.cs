using System.Diagnostics.CodeAnalysis;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace Domains_DLL.ViewModel
{
    public class ViewModelLocator
    {

        private static MainViewModel _main;
        private static UserDetailViewModel _userDetail;



        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            //SimpleIoc.Default.Register<UserDetailViewModel>();
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);

            CreateMain();
        }

        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static MainViewModel MainStatic
        {
            get
            {
                if (_main == null)
                {
                    CreateMain();
                }

                return _main;
            }
        }

        /// <summary>
        /// Gets the Settings property.
        /// </summary>
        public static UserDetailViewModel UserDetailStatic
        {
            get
            {
                if (_userDetail == null)
                {
                    CreateUserDetail();
                }

                return _userDetail;
            }
        }


        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        /// <summary>
        /// Gets the UserDetail property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UserDetailViewModel UserDetail
        {
            get
            {
                return UserDetailStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearMain()
        {
            _main = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the UserDetail property.
        /// </summary>
        public static void ClearUserDetail()
        {
            _userDetail = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateMain()
        {
            if (_main == null)
            {
                _main = new MainViewModel();
            }
        }


        /// <summary>
        /// Provides a deterministic way to create the UserDetail property.
        /// </summary>
        public static void CreateUserDetail()
        {
            if (_userDetail == null)
            {
                _userDetail = new UserDetailViewModel();
            }
        }
       
    }
}