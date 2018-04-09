using System.ComponentModel;
using System.Diagnostics;

namespace Binding.Basics.TwoWays.ViewModel
{
    // This class implements INotifyPropertyChanged to 
    // support one-way and two-way bindings
    // (such that the UI element updates when the source has been changed dynamically)
    public class Employee : INotifyPropertyChanged
    {
        private string name;

        private string state;

        #region PropertyChangedEventHandler
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        //// Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        public Employee()
        {
        }

        public Employee(string value)
        {
            this.name = value;
        }

        public string EmployeeName
        {
            get { return name; }
            set
            {
                name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("EmployeeName");
                Debug.WriteLine(EmployeeName);
            }
        }

        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }


    }
}
