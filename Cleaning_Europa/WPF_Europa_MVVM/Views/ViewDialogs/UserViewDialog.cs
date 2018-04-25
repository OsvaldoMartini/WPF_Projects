using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Europa_MVVM.Interfaces;

namespace WPF_Europa_MVVM.Views.ViewDialogs
{

    public class UserViewDialog : IModalDialog
    {
        private UserDisplayView view;

        void IModalDialog.BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetDialog().DataContext = viewModel;
        }

        void IModalDialog.ShowDialog()
        {
            GetDialog().ShowDialog();
        }

        void IModalDialog.Close()
        {
            GetDialog().Close();
        }

        private UserDisplayView GetDialog()
        {
            if (view == null)
            {
                //create the view if the view does not exist
                view = new UserDisplayView();
                view.Closed += new EventHandler(view_Closed);
            }

            return view;
        }

        void view_Closed(object sender, EventArgs e)
        {
            view = null;
        }
    }
}