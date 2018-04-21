using System.Windows;
using System.Windows.Controls;

namespace LoadXML.ByAttachedP.Controls
{
    /// <summary>
    /// In the current simple state, I would just use ListBox directly in MainWindow.
    /// This is just meant to be an example for having reusable custom controls,
    /// rather than making this an own view.
    /// </summary>
    public class VerticalMenu : ListBox
    {
        static VerticalMenu()
        {
            // link to default style (in Themes/generic.xaml)
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VerticalMenu), new FrameworkPropertyMetadata(typeof(VerticalMenu)));
        }
    }
}
