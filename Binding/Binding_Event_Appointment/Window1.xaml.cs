using System;
using System.Windows;

namespace Binding.Event.Appointment
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Appointment apt = new Appointment();
            apt.EventDescription = "Joel's Birthday";
            apt.EventDate = new DateTime(DateTime.Now.Year, 3, 28);
            this.DataContext = apt;
        }
    }
}
