using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binding.GameOfLife.Models;

namespace Binding.GameOfLife.ViewModels
{
    public class GameOfLifeVM : System.ComponentModel.INotifyPropertyChanged
    {

        #region Fields and Properties

        static int len = Int32.Parse(ConfigurationManager.AppSettings.Get("gridLength"));
        static int wid = Int32.Parse(ConfigurationManager.AppSettings.Get("gridWidth"));
        private int generation = 0;

        public int Generation
        {
            get { return generation; }
            set
            {
                generation = value;
                OnPropertyChanged("Generation");

            }
        }

        char[,] initialGrid = new char[len, wid];
        char[,] newgrid = new char[len, wid];
        char[,] tempgrid = new char[len, wid];
        GameOfLifeModel obj;

        private ObservableCollection<ObservableCollection<char>> _lst;

        public ObservableCollection<ObservableCollection<char>> Lst
        {
            get { return _lst; }

            set
            {
                _lst = value;
                OnPropertyChanged("Lst");
            }
        }

        # endregion

        #region Constructor

        public GameOfLifeVM()
        {

            obj = new GameOfLifeModel();
            char[,] initialGrid = new char[5, 5] { { 'A', 'D', 'E', 'A', 'D' }, { 'A', 'D', 'E', 'A', 'D' }, { 'A', 'A', 'E', 'D', 'D' }, { 'A', 'E', 'A', 'D', 'D' }, { 'A', 'D', 'E', 'A', 'D' } };
            obj.FillGrid(initialGrid);
            Lst = obj.ConvertArrayToList(initialGrid);
            tempgrid = initialGrid;

        }

      

        # endregion

        #region Methods

        public void Next()
        {
            newgrid = obj.GenerateNextState(tempgrid);
            Lst = obj.ConvertArrayToList(newgrid);
            OnPropertyChanged("Lst");
            Generation++;
            tempgrid = newgrid;

        }

        #endregion

        #region NotifyPropertyChanged Items

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));

        }

        #endregion


    }
}
