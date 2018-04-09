using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Binding.Basics.TwoWays.Concrete;
using Binding.Basics.TwoWays.Interfaces;
using Binding.Basics.TwoWays.ViewModel;

namespace Binding.Basics.TwoWays
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class TwoWaysWindow : Window, IModalService
    {
        public TwoWaysWindow()
        {

            InitializeComponent();

            SetDataContext();


        }

        private void SetDataContext()
        {
            //Set DataContext
            //var objstudent = new Student
            //{
            //    StudentName = "Osvaldo Martini",
            //    Address = "2 Weymouth Stree"
            //};
            //this.DataContext = objstudent; 

            Employee employee = new Employee("Now the Priority is the DataContext");
            employee.State = "FL";
            AnotherClass anotherClass = new AnotherClass();
            anotherClass.EmployeeNameTest = employee;
            anotherClass.AnotherField = "Value Different 'Another Value'";
            this.DataContext = anotherClass;


        }

        private void GoTo_Person_Click(object sender, RoutedEventArgs e)
        {
         
            GlobalServices.ModalService.NavigateTo(new PersonWindow(), delegate(bool returnValue)
            {
                if (returnValue)
                    MessageBox.Show("Return value == true");
                else
                    MessageBox.Show("Return value == false");
            });
        }


        #region IMainWindow Members

        private Stack<BackNavigationEventHandler> _backFunctions
            = new Stack<BackNavigationEventHandler>();

        void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        {
            foreach (UIElement item in modalGrid.Children)
                item.IsEnabled = false;
            modalGrid.Children.Add(uc);

            _backFunctions.Push(backFromDialog);
        }

        void IModalService.GoBackward(bool dialogReturnValue)
        {
            modalGrid.Children.RemoveAt(modalGrid.Children.Count - 1);

            UIElement element = modalGrid.Children[modalGrid.Children.Count - 1];
            element.IsEnabled = true;

            BackNavigationEventHandler handler = _backFunctions.Pop();
            if (handler != null)
                handler(dialogReturnValue);
        }

        #endregion

        private void btnUpdateSource_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression be = txtWindowTitle.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }


    }
}

