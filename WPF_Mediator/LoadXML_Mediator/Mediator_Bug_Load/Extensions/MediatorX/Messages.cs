using System;
using System.IO;

namespace Mediator_BugOnLoad.Extensions.MediatorX
{
    public static class Messages
    {
        public const string SelectedParentMenuItem = "SelectedParentMenuItem";
        public const string SelectedChildMenuItem = "SelectedChildMenuItem";

        //Base directory
        public static string RootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\");
        public static string DataDirectory = Path.Combine(RootDirectory + @"Data\");
    }
}
