using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Binding.Different.Ways.Model;
using Binding.Different.Ways.Utils;
using Binding.Different.Ways.ViewModel;
using Modules_ViewModel;
using System.Diagnostics;
using System.Linq;
using MessageBox = System.Windows.MessageBox;

namespace Binding.Different.Ways
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private ObservableCollection<Person> _person;

        ObservableCollection<string> mineObservableCollection;
        StringBuilder mineLog = new StringBuilder();


        public MainWindow()
        {
            InitializeComponent();


            Program pm = new Program();
            pm.bl[0].IsActive = false;
            pm.oc[0].IsActive = true;



            Loaded += OnLoaded;
            SetPersonCollection();

            SettingObservableEvents();

            SetUserModule_ViewModel();

            GridAuthors.ItemsSource = LoadCollectionData();


            LstGridViewOrange.ItemsSource = AuthorsGridViewList();
            CreateDynamicGridView();
            LstGridViewDynamic.ItemsSource = LstGridViewOrange.ItemsSource;



            LoadGridDynamic();


            //GridAuthors.Filter += new 

        }

        private void LoadGridDynamic()
        {
            var records = new ObservableCollection<Record>();
            records.Add(new Record(new Property("FirstName", "Paul"), new Property("LastName", "Stovell")));
            records.Add(new Record(new Property("FirstName", "Tony"), new Property("LastName", "Black")));

            var columns = records.First()
                .Properties
                .Select((x, i) => new { Name = x.Name, Index = i })
                .ToArray();

            //foreach (var column in columns)
            //{
            //    var binding = new System.Windows.Data.Binding(string.Format("Properties[{0}].Value", column.Index));

            //    dataGrid.Columns.Add(new DataGridTextColumn() { Header = column.Name, Binding = binding });
            //}

            foreach (var column in columns)
            {
                var binding = new System.Windows.Data.Binding(string.Format("Properties[{0}]", column.Index));
                dataGrid.Columns.Add(new CustomBoundColumn()
                {
                    Header = column.Name,
                    Binding = binding,
                    TemplateName = "CustomTemplate"
                });
            } 
        }

        

        private void SetUserModule_ViewModel()
        {
            UserViewModel user = new UserViewModel();
            user.UserName = "User";
            user.IsModified = false;
            //DataContext = user;
            this.GridUserViewModel.DataContext = user;
        }

        private void SettingObservableEvents()
        {
            mineLog.AppendLine("-- ObservableCollection --");
            InitOC();
            mineObservableCollection.CollectionChanged += mOC_CollectionChanged;
            TestOC();

            mineLog.AppendLine("-- ObservedCollection --");
            InitOC();
            var xObserved = new ObservedCollection<string>(mineObservableCollection);
            xObserved.OnCleared += xObserved_OnCleared;
            xObserved.OnItemAdded += xObserved_OnItemAdded;
            xObserved.OnItemMoved += xObserved_OnItemMoved;
            xObserved.OnItemRemoved += xObserved_OnItemRemoved;
            xObserved.OnItemReplaced += xObserved_OnItemReplaced;
            TestOC();
        }

        private void SetPersonCollection()
        {
            _person = new ObservableCollection<Person>()
            {
                new Person(){Name="Osvaldo",Address="Greater London"},
                new Person(){Name="Alexandre",Address="US"}
            };
            lstNames.ItemsSource = _person;
        }

        private void cmdAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            NewEmployeeDetails newEmployee = new NewEmployeeDetails();
            bool? employeeEntered = newEmployee.ShowDialog();
            if ((employeeEntered.HasValue) && (employeeEntered.Value == true))
            {
                ObservableCollection<Employee> oc = Resources["DirectContentEmployeeList"] as ObservableCollection<Employee>;
                oc.Add(newEmployee.ReturnValue);
            }
        }


        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SizeToContent = SizeToContent.Manual;

            var messages = new ObservableCollection<string>
            {
                "This is a long string to demonstrate that the list" + 
                " box content is determining window width"
            };

            for (int i = 0; i < 16; i++)
            {
                messages.Add("Test" + i);
            }

            for (int i = 0; i < 4; i++)
            {
                messages.Add("this text should be visible by vertical scrollbars only");
            }

            LstOnLoaded.ItemsSource = messages;
        }


        private void btnNames_Click(object sender, RoutedEventArgs e)
        {
            _person.Add(new Person() { Name = txtName.Text, Address = txtAddress.Text });
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }


        #region Events ObservableColletcion
        void xObserved_OnItemReplaced(ObservableCollection<string> aSender, int aIndex, string aOldItem, string aNewItem)
        {
            mineLog.AppendLine("  Replaced @ " + aIndex);
            mineLog.AppendLine("  Old: " + aOldItem);
            mineLog.AppendLine("  New: " + aNewItem);
            mineLog.AppendLine();
        }
        void xObserved_OnItemRemoved(ObservableCollection<string> aSender, int aIndex, string aItem)
        {
            mineLog.AppendLine("  Removed @ " + aIndex);
            mineLog.AppendLine("  " + aItem);
            mineLog.AppendLine();
        }
        void xObserved_OnItemMoved(ObservableCollection<string> aSender, int aOldIndex, int aNewIndex, string aItem)
        {
            mineLog.AppendLine("  Moved from " + aOldIndex + " to " + aNewIndex);
            mineLog.AppendLine("  " + aItem);
            mineLog.AppendLine();
        }
        void xObserved_OnItemAdded(ObservableCollection<string> aSender, int aIndex, string aItem)
        {
            mineLog.AppendLine("  Added @ " + aIndex);
            mineLog.AppendLine("  " + aItem);
            mineLog.AppendLine();
        }
        void xObserved_OnCleared(ObservableCollection<string> aSender)
        {
            mineLog.AppendLine("  Cleared");
            mineLog.AppendLine();
        }

        void InitOC()
        {
            mineObservableCollection = new ObservableCollection<string>();
            mineObservableCollection.Add("One");
            mineObservableCollection.Add("Two");
            mineObservableCollection.Add("Three");
            mineObservableCollection.Add("Four");
            mineObservableCollection.Add("Five");
        }

        void TestOC()
        {
            mineLog.AppendLine("mOC[2] = \"3\";");
            mineLog.AppendLine("  Count: " + mineObservableCollection.Count);
            mineObservableCollection[2] = "3";

            mineLog.AppendLine("mineObservableCollection.Add(\"Six\");");
            mineLog.AppendLine("  Count: " + mineObservableCollection.Count);
            mineObservableCollection.Add("Six");

            mineLog.AppendLine("mineObservableCollection.RemoveAt(2);");
            mineLog.AppendLine("  Count: " + mineObservableCollection.Count);
            mineObservableCollection.RemoveAt(2);

            mineLog.AppendLine("mineObservableCollection.Insert(2, \"Three\");");
            mineLog.AppendLine("  Count: " + mineObservableCollection.Count);
            mineObservableCollection.Insert(2, "Three");

            mineLog.AppendLine("mineObservableCollection.Move(0, 1);");
            mineLog.AppendLine("  Count: " + mineObservableCollection.Count);
            mineObservableCollection.Move(0, 1);

            mineLog.AppendLine("mineObservableCollection.Clear();");
            mineLog.AppendLine("  Count: " + mineObservableCollection.Count);
            mineObservableCollection.Clear();

            tboxLog.Text = mineLog.ToString();
        }

        void LogItems(System.Collections.IList aItems)
        {
            if (aItems == null)
            {
                mineLog.AppendLine("    (null)");
            }
            else
            {
                foreach (var x in aItems)
                {
                    mineLog.AppendLine("    " + x);
                }
            }
        }

        void mOC_CollectionChanged(object aSender, System.Collections.Specialized.NotifyCollectionChangedEventArgs aArgs)
        {
            mineLog.AppendLine("  " + aArgs.Action);
            mineLog.AppendLine("  " + aSender.GetType());
            mineLog.AppendLine("  Count: " + mineObservableCollection.Count);

            mineLog.AppendLine("  Old");
            mineLog.AppendLine("    Index: " + aArgs.OldStartingIndex);
            LogItems(aArgs.OldItems);

            mineLog.AppendLine("  New");
            mineLog.AppendLine("    Index: " + aArgs.NewStartingIndex);
            LogItems(aArgs.NewItems);

            mineLog.AppendLine();
        }
        #endregion


        /// List of Authors
        /// </summary>
        /// <returns></returns>
        private List<Author> LoadCollectionData()
        {
            List<Author> authors = new List<Author>();
            authors.Add(new Author()
            {
                ID = 101,
                Name = "Mahesh Chand",
                BookTitle = "Graphics Programming with GDI+",
                DOB = new DateTime(1975, 2, 23),
                IsMVP = false
            });
            authors.Add(new Author()
            {
                ID = 201,
                Name = "Mike Gold",
                BookTitle = "Programming C#",
                DOB = new DateTime(1982, 4, 12),
                IsMVP = true
            });
            authors.Add(new Author()
            {
                ID = 244,
                Name = "Mathew Cochran",
                BookTitle = "LINQ in Vista",
                DOB = new DateTime(1985, 9, 11),
                IsMVP = true
            });

            return authors;
        }

        private void RowColorButton_Click(object sender, RoutedEventArgs e)
        {
            Author author = (Author)GridAuthors.SelectedItem;
            if (author != null)
                MessageBox.Show("Selected author: " + author.Name);
        }

        private void LastNameCM_Click(object sender, RoutedEventArgs e)
        {

        }


        private ArrayList AuthorsGridViewList()
        {
            ArrayList list = new ArrayList();
            list.Add(new AuthorGridView("Mahesh Chand", 30, "ADO.NET Programming", true));
            list.Add(new AuthorGridView("Mike Gold", 35, "Programming C#", true));
            list.Add(new AuthorGridView("Raj Kumar", 25, "WPF Cookbook", false));
            list.Add(new AuthorGridView("Tony Parker", 48, "VB.NET Coding", false));
            list.Add(new AuthorGridView("Renee Ward", 22, "Coding Standards", true));
            list.Add(new AuthorGridView("Praveen Kumar", 33, "Vista Development", false));

            return list;
        }


        private void CreateDynamicGridView()
        {
            // Create a GridView  
            GridView grdView = new GridView();
            grdView.AllowsColumnReorder = true;
            grdView.ColumnHeaderToolTip = "Authors";

            GridViewColumn nameColumn = new GridViewColumn();
            nameColumn.DisplayMemberBinding = new System.Windows.Data.Binding("Name");
            nameColumn.Header = "Author Name";
            nameColumn.Width = 120;
            grdView.Columns.Add(nameColumn);

            GridViewColumn ageColumn = new GridViewColumn();
            ageColumn.DisplayMemberBinding = new System.Windows.Data.Binding("Age");
            ageColumn.Header = "Age";
            ageColumn.Width = 30;
            grdView.Columns.Add(ageColumn);

            GridViewColumn bookColumn = new GridViewColumn();
            bookColumn.DisplayMemberBinding = new System.Windows.Data.Binding("Book");
            bookColumn.Header = "Book";
            bookColumn.Width = 250;
            grdView.Columns.Add(bookColumn);

            GridViewColumn mvpColumn = new GridViewColumn();
            mvpColumn.DisplayMemberBinding = new System.Windows.Data.Binding("Mvp");
            mvpColumn.Header = "Mvp";
            mvpColumn.Width = 50;
            grdView.Columns.Add(mvpColumn);

            LstGridViewDynamic.View = grdView;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

    }
    class Program
    {
        public ObservableCollection<MyStruct> oc = new ObservableCollection<MyStruct>();
        public System.ComponentModel.BindingList<MyStruct> bl = new BindingList<MyStruct>();

        public Program()
        {
            oc.Add(new MyStruct());
            oc.CollectionChanged += CollectionChanged;

            bl.Add(new MyStruct());
            bl.ListChanged += ListChanged;
        }

        void ListChanged(object sender, ListChangedEventArgs e)
        {
            //Observe when the IsActive value is changed this event is triggered.
            Debug.WriteLine(e.ListChangedType.ToString());
        }

        void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //Observe when the IsActive value is changed this event is not triggered.
            Debug.WriteLine(e.Action.ToString());
        }



    }






    public class MyStruct : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isactive;
        public bool IsActive
        {
            get { return isactive; }
            set
            {
                isactive = value;
                NotifyPropertyChanged("IsActive");
            }
        }

        private void NotifyPropertyChanged(String PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }







}


