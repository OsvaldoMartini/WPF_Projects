using WPF_Europa_MVVM.Interfaces;

namespace WPF_Europa_MVVM.ServiceDI
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
