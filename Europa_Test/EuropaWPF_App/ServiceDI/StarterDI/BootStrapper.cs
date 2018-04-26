using EuropaWPF_App.Interfaces;
using EuropaWPF_App.Views.ViewDialogs;

namespace EuropaWPF_App.ServiceDI.StarterDI
{
    class BootStrapper
    {
        public static void Initialize()
        {
            //initialize all the services needed for dependency injection
            //I am using Unity framework for dependency injection
            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());

            //register the IModalDialog using an instance of the HierarchViewDialog
            //this sets up the view
            ServiceProvider.Instance.Register<IHierarchModal, HierarchDialogView>();
            //ServiceProvider.Instance.Register<IUserDisplayVM, UserDisplayVM>();
        }
    }
}
