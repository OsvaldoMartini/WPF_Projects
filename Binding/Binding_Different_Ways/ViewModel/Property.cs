using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Binding.Different.Ways.Properties;

namespace Binding.Different.Ways.ViewModel
{
    // This is a simple customer class that 
    // implements the IPropertyChange interface.
    public class Property : INotifyPropertyChanged
    {
        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value
        {
            get { return Value;}
            set { OnPropertyChanged("Value");}
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}