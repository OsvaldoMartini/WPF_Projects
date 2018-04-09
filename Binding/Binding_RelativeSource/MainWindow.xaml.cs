using System.Windows;
using Binding.RelativeSource.ViewModel;

namespace Binding_RelativeSource
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ListOfItems();
        }
    }
}
