using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WaterMark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_CLick(object sender, RoutedEventArgs e)
        {
            BindingExpression bindingExpression = TxtInput3.GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();
        }
    }
}
