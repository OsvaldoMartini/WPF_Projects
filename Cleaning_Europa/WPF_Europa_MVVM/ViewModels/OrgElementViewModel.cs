using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Europa_Data.Model;
using Europa_Data.ViewModel;

namespace WPF_Europa_MVVM.ViewModels
{
    public class OrgElementViewModel : ViewModelBase
    {
        private int Id;
        private string firstName;
        private string lastName;
        private string imagePath;
        private ObservableCollection<OrgElementViewModel> children;

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

        public ObservableCollection<OrgElementViewModel> Children
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

        internal OrgElementViewModel(UserVM i)
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
                this.Children = new ObservableCollection<OrgElementViewModel>();  //set as empty
            else if (levelsShown > 0)  //if a level is requested
            {
                this.Children = GetChildren();
                foreach (OrgElementViewModel i in this.Children)
                    i.ShowChildrenLevel(levelsShown - 1);  //decrement 1 for next level
            }           
        }

        private ObservableCollection<OrgElementViewModel> GetChildren()
        {
            children = new ObservableCollection<OrgElementViewModel>();
            //get the list of children from Model
            foreach (UserVM i in HierarchyOrganization.Instance().GetChildren(this.ID))
            {
                children.Add(new OrgElementViewModel(i));                     
            }
            return children;
        }
    }
}
