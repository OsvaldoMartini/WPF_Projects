using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Views;

namespace WPF_Europa_MVVM.ViewModels
{
    public class IntermediateMsgVM : ViewModelBase
    {
        private int _messageId;
        private string _messageInternal;
        private string _messageScreenTransfer;
        
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

        public override string ToString()
        {
            StringBuilder strB = new StringBuilder();
            return string.Format("{0} {1} ({2}) {3}", MessageInternal, MessageScreenTransfer, MessageId, Flag);
        }

    }
}
