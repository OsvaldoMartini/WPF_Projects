﻿using System.Windows;
using ProductMvvm.Model;
using MvvmFoundation.Wpf;
using ProductMvvm.Foundation;

namespace ProductMvvm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static StoreDB storeDB = new StoreDB();
        public static StoreDB StoreDB { get { return storeDB; } }
        internal static Messenger Messenger
        {
            get { return _messenger; }
        }

        readonly static Messenger _messenger = new Messenger();

    }
}
