using Binding.Different.Ways.Abstract;

namespace Binding.Different.Ways.Model
{
    public class FileObject : ViewModelBase
    {
        #region Properties
        #region Location
        private string _location = string.Empty;
        public string Location
        {
            get { return _location; }
            set
            {
                if (value != this._location)
                    _location = value;
                this.SetPropertyChanged("Location");
            }
        }
        #endregion Location

        #region FileName
        private string _fileName = string.Empty;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (value != this._fileName)
                    _fileName = value;
                this.SetPropertyChanged("FileName");
            }
        }
        #endregion FileName
        #endregion Properties

        #region String Override
        public override string ToString()
        {
            //.FormatString(this string myString) is an extension.
            string returnString = string.Empty;
            if (this._fileName != string.Empty)
                returnString = string.Format("File Name: {0}.", this._fileName);
            return returnString;
        }
        #endregion String Override
    }
}
