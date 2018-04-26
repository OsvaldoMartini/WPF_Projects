using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using EuropaWPF_App.Foundation;
using Europa_Data.Model;

namespace EuropaWPF_App.ViewModels
{
    public class HierarchTreeVm : ViewModelBase
    {
        private static HierarchTreeVm self;

        private List<OrgElementsVm> root;
        private OrgElementsVm selected;
        private ICommand selectedCommand;
        private ICommand changeDisplayLevelCommand;
        private int displayLevel = -1;  //display all levels by default


        /// <summary>
        /// For determining wich Action to be made inside  of the view
        /// </summary>
        public Mode Mode
        {
            get;
            set;
        }

        //the root of the visual tree
        public List<OrgElementsVm> Root
        {
            get 
            {
                if (root == null)
                {
                    root = new List<OrgElementsVm>();
                    root.Add(new OrgElementsVm(HierarchyOrganization.Instance().GetRoot()));
                }
                return root;            
            }
        }

        public OrgElementsVm Selected
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
            this.Selected = orgElement as OrgElementsVm;
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

        private HierarchTreeVm(){}

        public static HierarchTreeVm Instance()
        {
            if (self == null)
                self = new HierarchTreeVm();
            return self;
        }

    }
}
