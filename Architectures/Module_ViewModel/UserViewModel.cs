using System.ComponentModel.DataAnnotations;

namespace Modules_ViewModel
{
    public class UserViewModel : ViewModel
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
