using System.ComponentModel;

namespace Binding.Basics.TwoWays.ViewModel
{
    public class Person : INotifyPropertyChanged
    {
        private string fullname;
        public string FullName
        {
            get
            {
                return fullname;
            }
            set
            {
                fullname = value;
                OnPropertChanged("FullName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}
