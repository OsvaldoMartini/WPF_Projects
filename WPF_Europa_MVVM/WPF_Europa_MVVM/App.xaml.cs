using System.Windows;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Model;
using WPF_Europa_MVVM.StarterDI;

namespace WPF_Europa_MVVM
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
