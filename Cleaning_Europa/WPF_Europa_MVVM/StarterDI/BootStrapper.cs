using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuropaWPF_App.Interfaces;
using EuropaWPF_App.ServiceDI;
using EuropaWPF_App.ViewModels;
using EuropaWPF_App.Views;
using EuropaWPF_App.Views.ViewDialogs;

namespace EuropaWPF_App.StarterDI
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
