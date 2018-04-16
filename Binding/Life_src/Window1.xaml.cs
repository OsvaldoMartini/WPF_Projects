using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Binding.GameOfLife.XBAL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DispatcherTimer _timer = new DispatcherTimer();

        public Window1()
        {
            try
            {
                InitializeComponent();
                _timer.Interval = TimeSpan.FromMilliseconds(50);// 50ms = 20 frames per sec
                _timer.Tick += new EventHandler(_timer_Tick);
            }
            catch (Exception ex)// this is only here to get better info on XAML problems
            {
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            bool changed = lifeView1.Next();
            //UpdateGeneration();
            if (!changed)
            {
                btnAnimate.IsChecked = false;
                _timer.IsEnabled = false;
            }
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            lifeView1.Next();
            //UpdateGeneration();
        }

        //void UpdateGeneration()
        //{
        //    label1.Text = string.Format("Generation: {0}", lifeView1.Generation);
        //}

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = e.OriginalSource as ToggleButton;
            if (btn.IsChecked == true)
                lifeView1.Generation = 0;
            _timer.IsEnabled = btn.IsChecked == true;
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            lifeView1.Clear();
            //UpdateGeneration();
        }

        

    }
}
