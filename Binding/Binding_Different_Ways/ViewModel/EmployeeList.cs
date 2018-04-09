using System.Collections.Generic;
using System.Collections.ObjectModel;
using Binding.Different.Ways.Abstract;
using Binding.Different.Ways.Model;

namespace Binding.Different.Ways.ViewModel
{
    public class EmployeeList : ViewModelBase
    {
        private Employee _selectedEmployeeObject;
        public Employee SelectedEmployeeObject
        {
            get { return _selectedEmployeeObject; }
            set
            {
                if (value != this._selectedEmployeeObject)
                    _selectedEmployeeObject = value;
                this.SetPropertyChanged("SelectedEmployeeObject");
            }
        }

        private ObservableCollection<Employee> _employeeObjectCollection;
        public ObservableCollection<Employee> EmployeeObjectCollection
        {
            get { return _employeeObjectCollection; }
            set
            {
                if (value != this._employeeObjectCollection)
                    _employeeObjectCollection = value;
                this.SetPropertyChanged("EmployeeObjectCollection");
            }
        }

        private ObservableCollection<Inventory> _inventoryObjectCollection;
        public ObservableCollection<Inventory> InventoryObjectCollection
        {
            get { return _inventoryObjectCollection; }
            set
            {
                if (value != this._inventoryObjectCollection)
                    _inventoryObjectCollection = value;
                this.SetPropertyChanged("InventoryObjectCollection");
            }
        }
        public EmployeeList()
        {

            SetFiles();
        }

        private void SetFiles()
        {
            this._employeeObjectCollection = new ObservableCollection<Employee>();
            
            this._employeeObjectCollection.Add(new Employee
            {
                EmployeeNumber = 1,
                FirstName = "Osvaldo",
                LastName = "Martini",
                Title = "Software Architect",
                Department = "Engineering Operation"
            });
            this._employeeObjectCollection.Add(new Employee
            {
                EmployeeNumber = 2,
                FirstName = "Claudia",
                LastName = "Almeida",
                Title = "Account Executive",
                Department = "Engineering Operation"
            });
            this._employeeObjectCollection.Add(new Employee
            {
                EmployeeNumber = 3,
                FirstName = "Oscar",
                LastName = "Matias",
                Title = "QA Manager",
                Department = "Web Design"
            });


            this._inventoryObjectCollection = new ObservableCollection<Inventory>();
            for (int i = 1; i < 10; i++)
            {
                Inventory iv = new Inventory();
                iv.Heading = "R" + i;
                iv.Values = new List<string>();
                for (int j = 0; j < 5; j++)
                {
                    iv.Values.Add("Pic");
                }

                this._inventoryObjectCollection.Add(iv);
            }


        }

        //private Action<Employee> SetFileObjectCollection()
        //{
        //    this._fileObjectCollection = new ObservableCollection<FileObject>();
        //    return f => this._fileObjectCollection.Add(new FileObject { FileName = f.Name, Location = f.DirectoryName });
        //}


    }
}
