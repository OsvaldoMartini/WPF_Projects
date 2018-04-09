using System.ComponentModel;

namespace Modal.Interfaces
{
    public interface IValidatable : IDataErrorInfo
    {
        /// <summary>
        /// Gets if the entiy is valid
        /// </summary>
        bool IsValid { get; }
    }
}
