using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace LoadXML.ByAttachedP.Models
{
    /// <summary>
    /// Stores properties in an internal dictionary and fires NotifyPropertyChanged events.
    /// Also prevents string literals.
    /// Usage example (on ViewModel usually): 
    /// public string Caption { get { return GetValue(() => Caption); } set { SetValue(() => Caption, value); } }
    /// </summary>
    public abstract class NotifyBase : INotifyPropertyChanged
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetValue<T>(Expression<Func<T>> property, T value)
        {
            if (property == null)
            {
                throw new ArgumentException("Invalid property definition.");
            }
            PropertyInfo propertyInfo = this.GetPropertyInfo(property);
            T valueInternal = this.GetValueInternal<T>(propertyInfo.Name);
            if (!object.Equals(valueInternal, value))
            {
                this.values[propertyInfo.Name] = value;
                this.OnPropertyChanged(propertyInfo.Name);
            }
        }

        protected T GetValue<T>(Expression<Func<T>> property)
        {
            if (property == null)
            {
                throw new ArgumentException("Invalid property definition.");
            }
            PropertyInfo propertyInfo = this.GetPropertyInfo(property);
            return this.GetValueInternal<T>(propertyInfo.Name);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private T GetValueInternal<T>(string propertyName)
        {
            object obj;
            if (this.values.TryGetValue(propertyName, out obj))
            {
                return (T)obj;
            }
            return default(T);
        }

        private PropertyInfo GetPropertyInfo(LambdaExpression lambda)
        {
            MemberExpression operand;
            if (lambda.Body is UnaryExpression)
            {
                UnaryExpression body = lambda.Body as UnaryExpression;
                operand = body.Operand as MemberExpression;
            }
            else
            {
                operand = lambda.Body as MemberExpression;
            }
            return (operand.Member as PropertyInfo);
        }
    }
}
