using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Europa_Data.Model;

namespace EuropaWPF_App.ViewModels
{
    public class OrgElementsVm : ViewModelBase
    {
        private int Id;
        private string firstName;
        private string lastName;
        private string imagePath;
        private ObservableCollection<OrgElementsVm> children;

        private bool isSelected;

        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));
            }
        }

        public ObservableCollection<OrgElementsVm> Children
        {
            get 
            {
                if (children == null) //not yet initialized
                    return GetChildren();
                return children;
            }
            set 
            { 
                children = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Children"));
            }
        }

        internal OrgElementsVm(UserVM i)
        {
            this.ID = i._UserId;
            this.FirstName = i.Forename;
            this.LastName = i.Surname;
            this.ImagePath = Path.GetFullPath("Images/" + this.ID.ToString() + ".png");
        }

        internal void ShowChildrenLevel(int levelsShown)
        {
            if (levelsShown == -1) //show all levels
                this.Children = GetChildren();
            else if (levelsShown == 0)  //don't show any more levels
                this.Children = new ObservableCollection<OrgElementsVm>();  //set as empty
            else if (levelsShown > 0)  //if a level is requested
            {
                this.Children = GetChildren();
                foreach (OrgElementsVm i in this.Children)
                    i.ShowChildrenLevel(levelsShown - 1);  //decrement 1 for next level
            }           
        }

        private ObservableCollection<OrgElementsVm> GetChildren()
        {
            children = new ObservableCollection<OrgElementsVm>();
            //get the list of children from Model
            foreach (UserVM i in HierarchyOrganization.Instance().GetChildren(this.ID))
            {
                children.Add(new OrgElementsVm(i));                     
            }
            return children;
        }
    }
}
