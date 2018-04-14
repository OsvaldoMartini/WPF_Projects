using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Binding.Different.Ways.Annotations;
using Binding.Different.Ways.Commands;
using Binding.Different.Ways.Model;
using Binding.Different.Ways.Utils;

namespace Binding.Different.Ways.ViewModel
{
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        public ICommand ApplyChangesCommand { get; private set; }
        public void OnApplyChangesCommand(object param)
        {
            foreach (var item in EmployeeListViewModelCollection)
            {
                item.Model.IsChecked = item.IsChecked;
            }
            

        }

        private EmployeeViewModel _selectedEmployeeViewModel;
        public EmployeeViewModel SelectedEmployeeViewModel
        {
            get { return _selectedEmployeeViewModel; }
            set
            {
                if (value != this._selectedEmployeeViewModel)
                    _selectedEmployeeViewModel = value;
                this.OnPropertyChanged("SelectedEmployeeViewModel");
            }
        }


        private BindingList<EmployeeViewModel> _employeeListViewModelCollection;
        public BindingList<EmployeeViewModel> EmployeeListViewModelCollection
        {
            get { return _employeeListViewModelCollection; }
            set
            {
                if (value != this._employeeListViewModelCollection)
                    _employeeListViewModelCollection = value;
                this.OnPropertyChanged("EmployeeListViewModelCollection");
            }
        }

        public EmployeeListViewModel()
        {
            this._employeeListViewModelCollection = new BindingList<EmployeeViewModel>()
            {

                new EmployeeViewModel(new EmployeeModel {EmpName = "Raj"}),
                new EmployeeViewModel(new EmployeeModel {EmpName = "Kumar"}),
                new EmployeeViewModel(new EmployeeModel {EmpName = "Bala"}),
                new EmployeeViewModel(new EmployeeModel {EmpName = "Manigandan"}),
                new EmployeeViewModel(new EmployeeModel {EmpName = "Prayag"}),
                new EmployeeViewModel(new EmployeeModel {EmpName = "Pavithran"}),
                new EmployeeViewModel(new EmployeeModel {EmpName = "Selva"})
            };

            //ApplyChangesCommand = new DelegatingCommand(ApplyChanges);
            ApplyChangesCommand = new ApplyChangesCommand((p) => OnApplyChangesCommand(this.EmployeeListViewModelCollection));
        }


      

      
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
