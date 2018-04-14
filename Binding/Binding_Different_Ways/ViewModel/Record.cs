using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binding.Different.Ways.ViewModel
{
    public class Record
    {
        private readonly ObservableCollection<Property> properties = new ObservableCollection<Property>();

        public Record(params Property[] properties)
        {
            foreach (var property in properties)
                Properties.Add(property);
        }

        public ObservableCollection<Property> Properties
        {
            get { return properties; }
        }
    }
}