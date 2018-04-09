using System;

namespace Binding.Different.Ways.Model
{
    class AuthorGridView
    {
        public AuthorGridView(string authorName, Int16 authorAge, string authorBook, bool authorMVP)
        {
            this.Name = authorName;
            this.Age = authorAge;
            this.Book = authorBook;
            this.Mvp = authorMVP;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private Int16 age;

        public Int16 Age
        {
            get { return age; }
            set { age = value; }
        }
        private string book;

        public string Book
        {
            get { return book; }
            set { book = value; }
        }
        private bool mvp;

        public bool Mvp
        {
            get { return mvp; }
            set { mvp = value; }
        }

    }
}
