using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Europa_App.Controls
{
    public class ProductObservableCollection<Product> : ObservableCollection<Product>
    {
        public void UpdateCollection()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
        }


        public void ReplaceItem(int index, Product item)
        {
            base.SetItem(index, item);
        }

    } // class MyObservableCollection
}
