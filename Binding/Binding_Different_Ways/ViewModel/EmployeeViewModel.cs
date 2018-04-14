using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Binding.Different.Ways.Annotations;
using Binding.Different.Ways.Model;

namespace Binding.Different.Ways.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {


        public EmployeeModel Model { get; set; }

        private string _empName { get; set; }

        public string EmpName
        {
            get { return _empName; }
            set
            {
                _empName = value;
                OnPropertyChanged();
            }
        }

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        public EmployeeViewModel(EmployeeModel model)
        {
            Model = model;
            IsChecked = model.IsChecked;
            EmpName = model.EmpName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
