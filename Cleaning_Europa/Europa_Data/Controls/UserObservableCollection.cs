using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Europa_Data.Controls
{
    public class UserObservableCollection<UserVM> : ObservableCollection<UserVM>
    {
        public void UpdateCollection()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
        }


        public void ReplaceItem(int index, UserVM item)
        {
            base.SetItem(index, item);
        }

    } // class MyObservableCollection
}
