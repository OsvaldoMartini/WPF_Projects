using System;
using System.ComponentModel;

namespace WPF_Europa_MVVM.NotifyProp
{
    /// <summary>
    /// This class implements the <see cref="T:IPropertyNotification"/>
    /// interface and provides helper methods for derived classes.
    /// </summary>
    public class PropertyNotificationObject : IPropertyNotification
    {
        #region IPropertyNotification

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion // IPropertyNotification

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property that changed.
        /// </param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected void OnPropertyChanged(String propertyName,
            Object oldValue, Object newValue)
        {
            PropertyNotificationEventArgs e = new PropertyNotificationEventArgs(propertyName,
                oldValue, newValue);
            OnPropertyChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="PropertyNotificationEventArgs"/> instance
        /// containing the event data.
        /// </param>
        protected void OnPropertyChanged(PropertyNotificationEventArgs e)
        {
            PropertyChangedEventHandler temp = this.PropertyChanged;
            if (null != temp)
                temp(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property that is changing.
        /// </param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns><c>true</c> if the change can continue; otherwise <c>false</c>.</returns>
        protected Boolean OnPropertyChanging(String propertyName,
            Object oldValue, Object newValue)
        {
            CancelPropertyNotificationEventArgs e = new CancelPropertyNotificationEventArgs(propertyName,
                oldValue, newValue);
            OnPropertyChanging(e);
            return !e.Cancel;
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="CancelPropertyNotificationEventArgs"/> instance
        /// containing the event data.
        /// </param>
        protected void OnPropertyChanging(CancelPropertyNotificationEventArgs e)
        {
            PropertyChangingEventHandler temp = this.PropertyChanging;
            if (null != temp)
                temp(this, e);
        }

        /// <summary>
        /// This method is used to set a property while firing associated
        /// PropertyChanging and PropertyChanged events.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyField">The property field.</param>
        /// <param name="value">The value.</param>
        protected void SetProperty<T>(String propertyName, ref T propertyField,
            T value)
        {
            if (false == Object.Equals(value, propertyField))
            {
                if (true == OnPropertyChanging(propertyName, propertyField, value))
                {
                    T oldValue = propertyField;
                    propertyField = value;
                    OnPropertyChanged(propertyName, oldValue, propertyField);
                }
            }
        }

        #endregion // Methods
    }
}
