using System;
using System.ComponentModel;

namespace WPF_Europa_MVVM.INotifyChanging
{
    /// <summary>
    /// This class extends <see cref="T:PropertyChangedEventArgs"/> and
    /// allows for storing the old and new values of the changed property.
    /// </summary>
    public class PropertyNotificationEventArgs : PropertyChangedEventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PropertyNotificationEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property that is associated with this
        /// notification.
        /// </param>
        public PropertyNotificationEventArgs(String propertyName)
            : this(propertyName, null, null)
        {
            // No-op
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PropertyNotificationEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property that is associated with this
        /// notification.
        /// </param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public PropertyNotificationEventArgs(String propertyName,
            Object oldValue, Object newValue)
            : base(propertyName)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Holds the new value of the property.
        /// </summary>
        private Object newValue;

        /// <summary>
        /// Gets the new value of the property.
        /// </summary>
        /// <value>The new value.</value>
        public Object NewValue
        {
            get
            {
                return this.newValue;
            }
        }

        /// <summary>
        /// Holds the old value of the property.
        /// </summary>
        private Object oldValue;

        /// <summary>
        /// Gets the old value of the property.
        /// </summary>
        /// <value>The old value.</value>
        public Object OldValue
        {
            get
            {
                return this.oldValue;
            }
        }

        #endregion // Properties
    }
}
