using System.ComponentModel;
using System.Text;
using System.Windows;

namespace EuropaWPF_App.ViewModels
{
    public class IntermediateMsgVM : ViewModelBase
    {
        private int _messageId;
        private string _messageInternal;
        private string _messageScreenTransfer;
        private Visibility _btnCancelVisibility;
        
        public IntermediateMsgVM()
        {

            _messageId = 0;
            _messageInternal =
            _messageScreenTransfer =
                    null;
            //Memory Leak
        }

        public int MessageId
        {
            get { return _messageId; }
            set
            {
                _messageId = value;
            }
        }

        public string MessageInternal
        {
            get { return _messageInternal; }
            set
            {
                _messageInternal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MessageInternal"));
            }
        }

        public string MessageScreenTransfer
        {
            get { return _messageScreenTransfer; }
            set
            {
                _messageScreenTransfer = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MessageScreenTransfer"));
            }
        }


        private bool flag;
        public bool Flag
        {
            get { return flag; }
            set
            {
                if (value != flag)
                {
                    flag = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Flag"));
                }
            }
        }

        public Visibility BtnCancelVisibility
        {
            get { return _btnCancelVisibility; }
            set { _btnCancelVisibility = value; }
        }

        public override string ToString()
        {
            StringBuilder strB = new StringBuilder();
            return string.Format("{0} {1} ({2}) {3}", MessageInternal, MessageScreenTransfer, MessageId, Flag);
        }

    }
}
