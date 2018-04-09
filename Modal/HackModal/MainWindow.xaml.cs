using System.Windows;

namespace HackModal
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow : Window
	{
        public MainWindow()
		{
			InitializeComponent();
			HackModalWindow.SetParent(ModalDialogParent);
		}

		private void ShowModalDialog_Click(object sender, RoutedEventArgs e)
		{
            var res = HackModalWindow.ShowHandlerDialog(txtMsgSender.Text);
			var resultMessagePrefix = "Result: ";
			if (res)
				ResultText.Text = resultMessagePrefix + "Ok";
			else
				ResultText.Text = resultMessagePrefix + "Cancel";
		}
	}
}
