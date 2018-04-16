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
using System.Windows.Shapes;

namespace Binding.GameOfLife.Simpler
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            int fixedHeight = 50;
            int fixedWidth = 50;
            char[,] gameArray = new char[20, 30];
            wp.Orientation = Orientation.Horizontal;
            wp.MaxWidth = gameArray.GetLength(1) * fixedWidth;

            for (int i = 0; i < gameArray.GetLength(0); i++)
            {
                for (int j = 0; j < gameArray.GetLength(1); j++)
                {
                    wp.Children.Add(
                        new System.Windows.Controls.Button
                        {
                            Content = string.Format("{0}-{1}", i, j),
                            Height = fixedHeight,
                            Width = fixedWidth,
                        }
                    );
                }
            }
        }
    }
}
