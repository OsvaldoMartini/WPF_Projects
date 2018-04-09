using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ColorPicker;
using ToolTip_CustomControl;

namespace ToolTip_Using_CustomControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //end up in a file called Window1.g.cs in the \obj\Debug or \obj\release
            //directory, depends on how you run your project. Also of note when the 
            //code behind and XAML file get compiled is a file called Window1.baml 
            //("bammel"), thats a binary 
            //XAML file
            InitializeComponent();
            //look up the imported external control which has been placed 
            //on the main canvas, and subscribe to its SelectionChanged event
            //remember it was a ListBox subclass, so this event should be in
            //the custom control
            LstColorPicker.SelectionChanged += new SelectionChangedEventHandler(lstColorPicker_SelectionChanged);
            LstColorPicker.NewColor += new RoutedEventHandler(lstColorPicker_NewColor);
            LstColorPicker.NewColorCustom += new ColorPickerControl.NewColorCustomEventHandler(lstColorPicker_NewColorCustom);
        }
        private void rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            state = true;
            CustomToolTip.Visibility = Visibility.Hidden;
        }

        bool state = true;
        Random rand = new Random(DateTime.Now.Millisecond);

        private void rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (state)
            {
                CustomToolTip.Visibility = Visibility.Visible;
                CustomToolTip.UserControlToolTipTitle = (sender as Rectangle).Name.ToUpperInvariant();
                CustomToolTip.UserControlTextBlockToolTip = "";
                for (int i = 0; i < rand.Next(1, 30); i++)
                    CustomToolTip.UserControlTextBlockToolTip += (sender as Rectangle).Name + "\t" + i.ToString() + "\n";
            }
            CustomToolTip.UserControlToolTipX = Mouse.GetPosition(this).X + 10;
            CustomToolTip.UserControlToolTipY = Mouse.GetPosition(this).Y + 10;
            state = false;
        }


        #region Subscribed Events
        /// <summary>
        /// A user defined RoutedEvent within the  <see cref="ColorPickerControl">
        /// ColorPickerControl</see> that simply uses custom ColorRoutedEventArgs event args
        /// </summary>
        /// <param name="sender">the ColorPickerControl</param>
        /// <param name="e">custom ColorRoutedEventArgs event args</param>
        private void lstColorPicker_NewColorCustom(object sender, ColorRoutedEventArgs e)
        {
            MessageBox.Show(e.ColorName);
        }

        /// <summary>
        /// A user defined RoutedEvent within the  <see cref="ColorPickerControl">
        /// ColorPickerControl</see> that simply uses the standard RoutedEventArgs event args
        /// </summary>
        /// <param name="sender">the ColorPickerControl</param>
        /// <param name="e">standard RoutedEventArgs event args</param>
        private void lstColorPicker_NewColor(object sender, RoutedEventArgs e)
        {
            //get a reference to the source object and cast it to the correct type
            ColorPickerControl lstSrc = (sender as ColorPickerControl);
            //get the tooltip and show it as MessageBox
            ToolTip t = (ToolTip)(lstSrc.SelectedItem as Rectangle).ToolTip;
            this.LblColor.Content = t.Content;
        }

        /// <summary>
        /// raised when the selection change occurs in the source control
        /// </summary>
        /// <param name="sender">the source control</param>
        /// <param name="e">the event args</param>
        private void lstColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get a reference to the source object and cast it to the correct type
            ColorPickerControl lstSrc = (e.Source as ColorPickerControl);
            //set the background to the sources selected value, remember the SelectedValue
            //was set as Fill, which in fact returns a Brush. See the ColorPickerControl.cs class
            this.Background = (Brush)lstSrc.SelectedValue;
        }
        #endregion

    }
}