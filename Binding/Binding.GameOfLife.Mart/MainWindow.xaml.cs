using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace Binding.GameOfLife.Mart
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
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
