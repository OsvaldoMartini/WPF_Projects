using System.Collections.ObjectModel;
using System.Windows.Input;
using Domains_DLL.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Domains_DLL.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> employees;
        private Employee selectedEmployee;
        public string Title { get; set; }
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                Title = "Hello MVVM Light (Design Mode)";
            }
            else
            {
                Title = "Hello MVVM Light";
            }

            LoadEmployeesCommand = new RelayCommand(LoadEmployeesMethod);
            SaveEmployeesCommand = new RelayCommand(SaveEmployeesMethod);
        }

        public ICommand LoadEmployeesCommand { get; private set; }
        public ICommand SaveEmployeesCommand { get; private set; }

        public ObservableCollection<Employee> EmployeeList
        {
            get { return employees; }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                RaisePropertyChanged("SelectedEmployee");
            }
        }

        public void SaveEmployeesMethod()
        {
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Saved."));
        }

        private void LoadEmployeesMethod()
        {
            employees = Employee.GetSampleEmployees();
            this.RaisePropertyChanged(() => this.EmployeeList);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Loaded."));
        }
    }
}