using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Europa_MVVM.Interfaces;
using WPF_Europa_MVVM.ServiceDI;
using WPF_Europa_MVVM.ViewModels;
using WPF_Europa_MVVM.Views;
using WPF_Europa_MVVM.Views.ViewDialogs;

namespace WPF_Europa_MVVM.StarterDI
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
