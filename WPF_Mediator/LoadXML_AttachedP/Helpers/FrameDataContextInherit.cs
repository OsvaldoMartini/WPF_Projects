using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LoadXML.ByAttachedP.Helpers
{
    /// <summary>
    /// Attached property to make the frame pass on its DataContext to the content
    /// </summary>
    public class FrameDataContextInherit
    {
        // Inherit
        public static bool GetInherit(Frame frame) { return (bool)frame.GetValue(InheritProperty); }
        public static void SetInherit(Frame frame, bool value) { frame.SetValue(InheritProperty, value); }
        public static readonly DependencyProperty InheritProperty = DependencyProperty.RegisterAttached("Inherit", typeof(bool), typeof(FrameDataContextInherit), new PropertyMetadata(false, OnInheritChanged));

        private static void OnInheritChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var frame = (Frame)d;

            frame.DataContextChanged -= frame_DataContextChanged;
            frame.LoadCompleted -= frame_LoadCompleted;

            if ((bool)e.NewValue)
            {
                frame.DataContextChanged += frame_DataContextChanged;
                frame.LoadCompleted += frame_LoadCompleted;
            }
        }

        private static void frame_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateFrameDataContext(sender);
        }
        private static void frame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            UpdateFrameDataContext(sender);
        }
        private static void UpdateFrameDataContext(object sender)
        {
            var frame = (Frame)sender;
            var content = frame.Content as FrameworkElement;
            if (content == null)
                return;
            content.DataContext = frame.DataContext;
        }
    }
}
