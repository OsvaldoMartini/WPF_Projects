namespace WPF_Europa_MVVM.Interfaces
{
    public interface IServiceLocator
    {
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;

        TInterface Get<TInterface>();
    }
}
