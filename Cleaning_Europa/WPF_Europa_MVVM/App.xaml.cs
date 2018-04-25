using System.Windows;
using Europa_Data.DB_Helper;
using EuropaWPF_App.Foundation;
using EuropaWPF_App.StarterDI;

namespace EuropaWPF_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static StoreXML storeXML = new StoreXML();
        public static StoreXML StoreXML { get { return storeXML; } }
        internal static Messenger Messenger
        {
            get { return _messenger; }
        }
        readonly static Messenger _messenger = new Messenger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BootStrapper.Initialize();
        }
    }
}
