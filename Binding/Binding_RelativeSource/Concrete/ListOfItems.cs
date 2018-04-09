using System.Collections.ObjectModel;

namespace Binding.RelativeSource.ViewModel
{
    public class ListOfItems : ObservableCollection<Item>
    {
        public ListOfItems()
        {
            Add(new Item { Value = 80.23 });
            Add(new Item { Value = 126.17 });
            Add(new Item { Value = 130.21 });
            Add(new Item { Value = 115.28 });
            Add(new Item { Value = 131.21 });
            Add(new Item { Value = 135.22 });
        }
    }
}
