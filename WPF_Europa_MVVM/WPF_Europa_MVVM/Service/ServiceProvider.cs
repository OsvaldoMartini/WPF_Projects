using WPF_Europa_MVVM.Interfaces;

namespace WPF_Europa_MVVM.Service
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
