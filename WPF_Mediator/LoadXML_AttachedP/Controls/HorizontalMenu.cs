using System.Windows;
using System.Windows.Controls;

namespace LoadXML.ByAttachedP.Controls
{
    public class HorizontalMenu : ListBox
    {
        static HorizontalMenu()
        {
            // link to default style (in Themes/generic.xaml)
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HorizontalMenu), new FrameworkPropertyMetadata(typeof(HorizontalMenu)));
        }
    }
}
