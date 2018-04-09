using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Events.Tunnel.Buble
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

        private void grdButtons_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = chkHandleInGridClick.IsChecked.Value;
            ShowMethodName();
        }

        private void grdButtons_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // This is the first event in the series. Clear the result TextBox.
            txtResults.Clear();

            e.Handled = chkHandleInGridPreview.IsChecked.Value;
            ShowMethodName();
        }

        private void spButtons_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = chkHandleInStackPanelClick.IsChecked.Value;
            ShowMethodName();
        }

        private void spButtons_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = chkHandleInStackPanelPreview.IsChecked.Value;
            ShowMethodName();
        }

        private void Button1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = chkHandleInButtonPreview.IsChecked.Value;
            ShowMethodName();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = chkHandleInButtonClick.IsChecked.Value;
            ShowMethodName();
        }

        private void Button2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = chkHandleInButtonPreview.IsChecked.Value;
            ShowMethodName();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = chkHandleInButtonClick.IsChecked.Value;
            ShowMethodName();
        }

        private void Button3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = chkHandleInButtonPreview.IsChecked.Value;
            ShowMethodName();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = chkHandleInButtonClick.IsChecked.Value;
            ShowMethodName();
        }

        // Record the name of the method that called this one.
        private void ShowMethodName()
        {
            txtResults.AppendText(
                new StackTrace(1).GetFrame(0).GetMethod().Name + '\n');
        }
    }
}
