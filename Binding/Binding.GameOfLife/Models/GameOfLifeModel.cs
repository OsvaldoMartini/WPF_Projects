using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binding.GameOfLife.Models
{
    public class GameOfLifeModel
    {


        private char[,] workingGrid, finalGrid;

        /// <summary>
        /// Configures initial state of grid based on degree defined in app.config  
        /// </summary>
        /// <param name="grid"></param>
        public void FillGrid(char[,] grid)
        {
            Int32 degree = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("degree"));
            degree = Convert.ToInt32((degree * grid.GetLength(0) * grid.GetLength(1)) / 100);
            Random _random = new Random();

            for (int i = 0; i <= grid.GetUpperBound(0); i++)
                for (int j = 0; j <= grid.GetUpperBound(1); j++)
                    grid[i, j] = 'E';


            int x, y;
            for (int i = 0; i <= grid.GetUpperBound(0); i++)
                for (int j = 0; j <= grid.GetUpperBound(1); j++)
                {
                    if (degree <= 0)
                        return;
                    x = _random.Next(0, grid.GetUpperBound(0));
                    y = _random.Next(0, grid.GetUpperBound(1));

                    if (grid[x, y] != 'A')
                    {
                        grid[x, y] = 'A';
                        degree--;
                    }
                }


        }

        /// <summary>
        /// Generates the next generation
        /// </summary>
        /// <param name="grid"> generation n</param>
        /// <returns> generation n+1</returns>
        public char[,] GenerateNextState(char[,] grid)
        {
            GetWorkingGrid(grid);
            GenerateFinalGrid();

            return finalGrid;

        }

        /// <summary>
        /// Generates the final grid to be returned
        /// </summary>
        public void GenerateFinalGrid()
        {

            for (int i = 1; i < workingGrid.GetUpperBound(0); i++)
            {
                for (int j = 1; j < workingGrid.GetUpperBound(1); j++)
                {
                    ApplyRulesOnEachCell(i, j);

                }
            }


        }

        /// <summary>
        /// Print the grid
        /// </summary>
        /// <param name="grid"></param>
        public void PrintState(char[,] grid)
        {
            for (int i = 0; i <= grid.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= grid.GetUpperBound(1); j++)
                    Console.Write(grid[i, j]);
                Console.WriteLine();
            }
        }

        public ObservableCollection<ObservableCollection<char>> ConvertArrayToList(char[,] grid)
        {
            ObservableCollection<ObservableCollection<char>> lsts = new ObservableCollection<ObservableCollection<char>>();

            for (int i = 0; i <= grid.GetUpperBound(0); i++)
            {
                lsts.Add(new ObservableCollection<char>());

                for (int j = 0; j <= grid.GetUpperBound(1); j++)
                {
                    lsts[i].Add(grid[i, j]);
                }
            }
            return lsts;

        }

        
        /// <summary>
        /// Gets a working grid which I will use internally to handle border cells.
        /// </summary>
        /// <param name="grid"></param>
        private void GetWorkingGrid(char[,] grid)
        {

            int lastX = grid.GetUpperBound(0);
            int lastY = grid.GetUpperBound(1);

            workingGrid = new char[lastX + 3, lastY + 3];
            finalGrid = new char[lastX + 1, lastY + 1];

            for (int i = grid.GetLowerBound(0); i <= grid.GetUpperBound(0); i++)
                for (int j = grid.GetLowerBound(1); j <= grid.GetUpperBound(1); j++)
                    workingGrid[i + 1, j + 1] = grid[i, j];


            workingGrid[0, 0] = grid[lastX, lastY];
            workingGrid[0, lastY + 2] = grid[lastX, 0];
            workingGrid[lastX + 2, 0] = grid[0, lastY];
            workingGrid[lastX + 2, lastY + 2] = grid[0, 0];

            for (int i = 0; i <= lastY; i++)
            {
                workingGrid[0, i + 1] = grid[lastX, i];
                workingGrid[lastX + 2, i + 1] = grid[0, i];
            }

            for (int i = 0; i <= lastX; i++)
            {
                workingGrid[i + 1, 0] = grid[i, lastY];
                workingGrid[i + 1, lastY + 2] = grid[i, 0];
            }

        }

        /// <summary>
        /// Applies rules to each cell to determine its new state
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void ApplyRulesOnEachCell(int i, int j)
        {
            //count the live cells
            int count = 0;
            for (int row = i - 1; row <= i + 1; row++)
            {
                for (int col = j - 1; col <= j + 1; col++)
                {
                    if (row == i && col == j)
                        continue;
                    else if ((workingGrid[row, col] == 'A'))
                        count += 1;

                }
            }

            if ((workingGrid[i, j] == 'D') && count == 3)    //If cell is dead.
                finalGrid[i - 1, j - 1] = 'A';
            else if (workingGrid[i, j] == 'A' && (count < 2 || count > 3))  // If cell is alive.
                finalGrid[i - 1, j - 1] = 'D';
            else
                finalGrid[i - 1, j - 1] = workingGrid[i, j];


        }

        /// <summary>
        /// Used to determine if the next iteration will happen or not.Iteration stops when all cells are dead.
        /// </summary>
        /// <param name="newGrid"></param>
        /// <returns>returns true if any cell in the grid is still alive</returns>
        private static bool CheckIfAnyCellIsAlive(char[,] newGrid)
        {
            bool aliveCellsPresent = false;

            for (int i = 0; i <= newGrid.GetUpperBound(0); i++)
                for (int j = 0; j <= newGrid.GetUpperBound(1); j++)
                {
                    if (newGrid[i, j] == 'A')
                        aliveCellsPresent = true;
                }

            return aliveCellsPresent;

        }

 
    }
}