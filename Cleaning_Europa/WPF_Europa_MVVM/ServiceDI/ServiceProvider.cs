using EuropaWPF_App.Interfaces;

namespace EuropaWPF_App.ServiceDI
{
    class ServiceProvider
    {
        public static IServiceLocator Instance { get; private set; }

        public static void RegisterServiceLocator(IServiceLocator s)
        {
            Instance = s;
        }
    }
}
