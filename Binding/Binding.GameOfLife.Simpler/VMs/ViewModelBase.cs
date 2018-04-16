using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binding.GameOfLife.Simpler.VMs
{
    public class ViewModelBase
    {
        public char[][] MyArray { get; set; }

        public ViewModelBase()
        {
            MyArray = new char[30][];

            for (int row = 0; row < 30; row++)
            {
                MyArray[row] = new char[80];

                for (int col = 0; col < 80; col++)
                {
                    MyArray[row][col] = 'O';
                }
            }
        }
    }
}
