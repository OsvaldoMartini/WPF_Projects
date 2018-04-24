using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Model;
using WPF_Europa_MVVM.ViewModels;

namespace DevLake.OrgChart.UI.ViewModel
{
    public class OrgTreeViewModel : ViewModelBase
    {
        private static OrgTreeViewModel self;

        private List<OrgElementViewModel> root;
        private OrgElementViewModel selected;
        private ICommand selectedCommand;
        private ICommand changeDisplayLevelCommand;
        private int displayLevel = -1;  //display all levels by default

        //the root of the visual tree
        public List<OrgElementViewModel> Root
        {
            get 
            {
                if (root == null)
                {
                    root = new List<OrgElementViewModel>();
                    root.Add(new OrgElementViewModel(HierarchyOrganization.Instance().GetRoot()));
                }
                return root;            
            }
        }

        public OrgElementViewModel Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                selected.IsSelected = true;
                ShowChildrenLevel();  //show only the levels chosen by the user
                OnPropertyChanged(new PropertyChangedEventArgs("Selected"));
            }
        }

        public ICommand SelectedCommand
        {
            get
            {
                if (selectedCommand == null)
                {
                    selectedCommand = new CommandBase(i => this.SetSelected(i), null);
                }
                return selectedCommand;
            }
        }

        public ICommand ChangeDisplayLevelCommand
        {
            get
            {
                if (changeDisplayLevelCommand == null)
                {
                    changeDisplayLevelCommand = new CommandBase(i => ChangeDisplayLevel(i), null);
                }
                return changeDisplayLevelCommand;
            }
        }

        private void SetSelected(object orgElement)
        {
            this.Selected = orgElement as OrgElementViewModel;
        }

        private void ChangeDisplayLevel(object i)
        {
            int level;
            if (int.TryParse(i.ToString(), out level))
            {
                this.displayLevel = level;
                ShowChildrenLevel(); //show only the levels chosen by the user
            }
        }

        private void ShowChildrenLevel()
        {
            if (this.Selected != null)
            {
                this.Selected.ShowChildrenLevel(this.displayLevel);
            }
        }

        private OrgTreeViewModel(){}

        public static OrgTreeViewModel Instance()
        {
            if (self == null)
                self = new OrgTreeViewModel();
            return self;
        }

    }
}
