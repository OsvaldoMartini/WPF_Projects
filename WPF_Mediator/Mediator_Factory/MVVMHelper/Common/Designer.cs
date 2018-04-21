using System.ComponentModel;
using System.Windows;

namespace MVVM_Helper.Common
{
    /// <summary>
    /// Can be used to resolve whether the application is currently in design
    /// mode.
    /// </summary>
    public static class Designer
    {
        #region IsDesignMode

        private static readonly bool isDesignMode;

        /// <summary>
        /// Checks whether the application is currently in design mode.
        /// </summary>
        public static bool IsDesignMode
        {
            get { return isDesignMode; }
        }

        #endregion

        static Designer()
        {
            DependencyProperty prop = DesignerProperties.IsInDesignModeProperty;
            isDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement))
                                                             .Metadata.DefaultValue;

        }
    }
}
