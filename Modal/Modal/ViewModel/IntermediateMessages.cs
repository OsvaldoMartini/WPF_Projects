using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Modal.Model;

namespace Modal.ViewModel
{
    public class IntermediateMessages : INotifyPropertyChanged
    {
        private int _messageId;
        private string _messageInternal;
        private string _messageScreenTransfer;
        public List<Inventory> Inventory{get;set;}

        #region PropertyChangedEventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        public IntermediateMessages()
        {
            _messageId = 0;
            _messageInternal =
                _messageScreenTransfer =
                    null;
            //Memory Leak
            Inventory = new List<Inventory>();

            for (int i = 1; i < 10; i++)
            {
                Inventory iv = new Inventory();
                iv.Heading = "R" + i;
                iv.Values = new List<string>();
                for (int j = 0; j < 5; j++)
                {
                    iv.Values.Add("Pic");
                }
                Inventory.Add(iv);
            }

        }


        public int MessageId
        {
            get { return _messageId; }
            set
            {
                _messageId = value;
                OnPropertyChanged("MessageId");
                Debug.WriteLine(string.Format("MessageId: {0}", MessageId));
            }
        }

        public string MessageInternal
        {
            get { return _messageInternal; }
            set
            {
                _messageInternal = value;
                OnPropertyChanged("MessageInternal");
                Debug.WriteLine(string.Format("MessageInternal: {0}", MessageInternal));
            }
        }

        public string MessageScreenTransfer
        {
            get { return _messageScreenTransfer; }
            set
            {
                _messageScreenTransfer = value;
                OnPropertyChanged("MessageScreenTransfer");
                Debug.WriteLine(string.Format("MessageScreenTransfer: {0}", MessageScreenTransfer));
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
                    PropertyChanged(this, new PropertyChangedEventArgs("Flag"));
                    Debug.WriteLine(string.Format("Flag: {0}", Flag));
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
