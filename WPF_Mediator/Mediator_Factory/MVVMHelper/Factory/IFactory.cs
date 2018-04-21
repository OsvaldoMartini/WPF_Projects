using System.Windows;

namespace MVVM_Helper.Factory
{
    public interface IFactory
    {
        object CreateViewModel(DependencyObject sender);
    }
}
