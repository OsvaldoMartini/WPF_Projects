using System;
using System.ComponentModel;

namespace WPF_Europa_MVVM.INotifyChanging
{
    /// <summary>
    /// Notifies clients that a property value is changing or changed.
    /// </summary>
    public interface IPropertyNotification : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        event PropertyChangingEventHandler PropertyChanging;

        #endregion // Events
    }

    /// <summary>
    /// Represents the method that will handle the
    /// <see cref="E:IPropertyNotification.PropertyChanging"/> event raised
    /// when a property is changing on a component.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">
    /// A <see cref="T:CancelPropertyNotificationEventArgs"/> that contains the event
    /// data.
    /// </param>
    public delegate void PropertyChangingEventHandler(Object sender, CancelPropertyNotificationEventArgs e);

}
