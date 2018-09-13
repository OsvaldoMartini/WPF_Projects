using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIAutomationTest
{
    /// <summary>
    /// Interaction logic for WebBrowser.xaml
    /// </summary>
    public partial class WebBrowser : Window
    {
        public WebBrowser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)

        {

            //FrameWithinGrid.Source = new Uri("http://Janes.ihs.com", UriKind.Absolute);
            NavigationWindow window = new NavigationWindow();

            Uri source = new Uri("http://Janes.ihs.com", UriKind.Absolute);

            window.Source = source; window.Show();
        }
    }
}
