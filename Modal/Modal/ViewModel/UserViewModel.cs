using System.ComponentModel.DataAnnotations;
using Modal.Concrete;

namespace Modal.ViewModel
{
    public class UserViewModel : Interfaces.ViewModel
    {
        private string _userName;

        [ValidatableProperty]
        [Required]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    RaisePropertyChanged(true);
                }
            }
        }
    }
}
