using System.Collections.ObjectModel;

namespace LoadXML.ByAttachedP.Models
{
    public class MenuModel : NotifyBase
    {
        public ObservableCollection<MenuModel> Items
        {
            get
            {
                return GetValue(() => Items);
            }
            set
            {
                SetValue(() => Items, value);
            }
        }
        public MenuModel SelectedItem
        {
            get
            {
                return GetValue(() => SelectedItem);
            }
            set
            {
                SetValue(() => SelectedItem, value);
            }
        }
        public int Id
        {
            get
            {
                return GetValue(() => Id);
            }
            set
            {
                SetValue(() => Id, value);
            }
        }
        public string Name
        {
            get
            {
                return GetValue(() => Name);
            }
            set
            {
                SetValue(() => Name, value);
            }
        }
        public string SourcePage
        {
            get
            {
                return GetValue(() => SourcePage);
            }
            set
            {
                SetValue(() => SourcePage, value);
            }
        }
    }
}