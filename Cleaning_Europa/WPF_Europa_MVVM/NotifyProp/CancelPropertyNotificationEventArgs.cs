using System;

namespace WPF_Europa_MVVM.NotifyProp
{
    /// <summary>
    /// This class extends <see cref="T:PropertyNotificationEventArgs"/> and
    /// allows for cancelling of the associated event.
    /// </summary>
    public class CancelPropertyNotificationEventArgs : PropertyNotificationEventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CancelPropertyNotificationEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property that is associated with this
        /// notification.
        /// </param>
        public CancelPropertyNotificationEventArgs(String propertyName)
            : base(propertyName)
        {
            // No-op
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CancelPropertyNotificationEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property that is associated with this
        /// notification.
        /// </param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public CancelPropertyNotificationEventArgs(String propertyName,
            Object oldValue, Object newValue)
            : base(propertyName, oldValue, newValue)
        {
            // No-op
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Holds a value indicating whether the associated event should be
        /// cancelled.
        /// </summary>
        private Boolean cancel = false;

        /// <summary>
        /// Gets or sets a value indicating whether the associated event should
        /// be cancelled.
        /// </summary>
        /// <value>
        /// <c>true</c> if the event should be cancelled; otherwise, <c>false</c>.
        /// </value>
        public Boolean Cancel
        {
            get
            {
                return this.cancel;
            }
            set
            {
                this.cancel = value;
            }
        }

        #endregion // Properties
    }
}
