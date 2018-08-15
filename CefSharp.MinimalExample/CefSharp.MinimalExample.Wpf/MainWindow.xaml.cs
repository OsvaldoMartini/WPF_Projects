using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ExecuteJavaScriptBtn_Click(object sender, RoutedEventArgs e)
        {
            //Check if the browser can execute JavaScript and the ScriptTextBox is filled
            if (Browser.CanExecuteJavascriptInMainFrame && !string.IsNullOrWhiteSpace("Osvaldo Martini"))
            {
                //Evaluate javascript and remember the evaluation result
                JavascriptResponse response = await Browser.EvaluateScriptAsync("Osvaldo Martini");

                if (response.Result != null)
                {
                    //Display the evaluation result if it is not empty
                    MessageBox.Show(response.Result.ToString(), "JavaScript Result");
                }
            }
        }

    }
}
