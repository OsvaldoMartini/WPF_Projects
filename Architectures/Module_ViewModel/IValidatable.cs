using System.ComponentModel;

namespace Modules_ViewModel
{
    public interface IValidatable : IDataErrorInfo
    {
        /// <summary>
        /// Gets if the entiy is valid
        /// </summary>
        bool IsValid { get; }
    }
}
