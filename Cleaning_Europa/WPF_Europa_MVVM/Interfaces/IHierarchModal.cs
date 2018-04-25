using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuropaWPF_App.Interfaces
{
    interface  IHierarchModal: IModalDialog
    {
        void BindViewModel<TViewModel>(TViewModel viewModel); //bind to viewModel
        void ShowDialog();  //show the modal window 
        void Show();
        void Close();  //close the dialog   

    }
}
