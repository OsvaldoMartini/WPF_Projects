using System.Windows;
using System.Windows.Input;

namespace Events.Basics
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

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Window_PreviewKeyDown");
        }

        private void StackPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("StackPanel_PreviewKeyDown");
            e.Handled = true;
        }

        private void Grid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Grid1_PreviewKeyDown");
            e.Handled = true;
        }

        private void Grid1_KeyUp(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Grid1_KeyUp");
            e.Handled = true;
        }

        private void StackPanel_KeyUp(object sender, KeyEventArgs e)
        {
            MessageBox.Show("StackPanel_KeyUp");
            e.Handled = true;
        }

        private void StackPanel_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("StackPanel_KeyDown");
            e.Handled = true;
        }

        private void Grid1_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Grid1_KeyDown");
            e.Handled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Window_KeyDown");
            e.Handled = true;
        }
    }
}
