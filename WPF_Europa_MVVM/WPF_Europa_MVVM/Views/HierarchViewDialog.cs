using System;
using WPF_Europa_MVVM.Interfaces;

namespace WPF_Europa_MVVM.Views
{
    public class HierarchViewDialog : IModalDialog
    {
        private UserHierarchyView view;

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

        private UserHierarchyView GetDialog()
        {
            if (view == null)
            {
                //create the view if the view does not exist
                view = new UserHierarchyView();
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

