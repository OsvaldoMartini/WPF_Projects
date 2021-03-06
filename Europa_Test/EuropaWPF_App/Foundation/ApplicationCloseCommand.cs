﻿using System;
using System.Windows;
using System.Windows.Input;

namespace EuropaWPF_App.Foundation
{
    public class ApplicationCloseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            // I may not need a body here at all...
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return Application.Current != null && Application.Current.MainWindow != null;
        }

        public void Execute(object parameter)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
