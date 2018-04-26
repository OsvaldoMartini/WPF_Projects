using System;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace Products_SQL_Linq.Views
{
    /// <summary>
    /// Interaction logic for LoadDBLinq.xaml
    /// </summary>
    public partial class LoadDBLinq : Window
    {

        XNamespace mybooks = "http://www.mybooks.com";
        XElement bookList;

        public LoadDBLinq()
        {
            InitializeComponent();
            bookList = (XElement)((ObjectDataProvider)Resources["LoadedBooks"]).Data;
        }

        void OnRemoveBook(object sender, EventArgs e)
        {
            int index = lbBooks.SelectedIndex;
            if (index < 0) return;

            XElement selBook = (XElement)lbBooks.SelectedItem;
            //Get next node before removing element.  
            XNode nextNode = selBook.NextNode;
            selBook.Remove();

            //Remove any matching newline node.  
            if (nextNode != null && nextNode.ToString().Trim().Equals(""))
            { nextNode.Remove(); }

            //Set selected item.   
            if (lbBooks.Items.Count > 0)
            { lbBooks.SelectedItem = lbBooks.Items[index > 0 ? index - 1 : 0]; }
        }

        void OnAddBook(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbAddID.Text) ||
                String.IsNullOrEmpty(tbAddValue.Text))
            {
                MessageBox.Show("Please supply both a Book ID and a Value!", "Entry Error!");
                return;
            }
            XElement newBook = new XElement(
                mybooks + "book",
                new XAttribute("id", tbAddID.Text),
                tbAddValue.Text);
            bookList.Add("  ", newBook, "\r\n");
        }
    }
}  
