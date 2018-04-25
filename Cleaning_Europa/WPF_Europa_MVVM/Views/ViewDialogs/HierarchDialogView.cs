using System;
using WPF_Europa_MVVM.Interfaces;

namespace WPF_Europa_MVVM.Views.ViewDialogs
{
    public class HierarchDialogView : IHierarchModal
    {
        private UserHierarchyView view;

        public void BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetDialog().DataContext = viewModel;
        }

        public void Show()
        {
            GetDialog().Show();
        }


        public void Close()
        {
            GetDialog().Close();
        }

        public void ShowDialog()
        {
            GetDialog().ShowDialog();
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

