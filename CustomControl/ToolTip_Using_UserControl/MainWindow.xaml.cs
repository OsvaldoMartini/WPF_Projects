using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ToolTip_Using_UserControl
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
        private void rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            state = true;
            customToolTip.Visibility = Visibility.Hidden;
        }

        bool state = true;
        Random rand = new Random(DateTime.Now.Millisecond);

        private void rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (state)
            {
                customToolTip.Visibility = Visibility.Visible;
                customToolTip.UserControlToolTipTitle = (sender as Rectangle).Name.ToUpperInvariant();
                customToolTip.UserControlTextBlockToolTip = "";
                for (int i = 0; i < rand.Next(1, 30); i++)
                    customToolTip.UserControlTextBlockToolTip += (sender as Rectangle).Name + "\t" + i.ToString() + "\n";
            }
            customToolTip.UserControlToolTipX = Mouse.GetPosition(this).X + 10;
            customToolTip.UserControlToolTipY = Mouse.GetPosition(this).Y + 10;
            state = false;
        }
    }
}