using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Binding.GameOfLife.ViewModels;

namespace Binding.GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameOfLifeWindow : Window
    {
        GameOfLifeVM vm;

        public GameOfLifeWindow()
        {
            InitializeComponent();
            vm = new GameOfLifeVM();
            this.DataContext = vm;
            DispatcherTimer timer = new DispatcherTimer();
            int delay = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("timer"));
            timer.Interval = TimeSpan.FromSeconds(delay);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {

            vm.Next();

        }

    }
}
