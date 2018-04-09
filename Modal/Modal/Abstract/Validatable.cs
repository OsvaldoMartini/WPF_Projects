using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Modal.Concrete;
using Modal.Interfaces;

namespace Modal.Abstract
{
    public abstract class Validatable : IValidatable
    {

        /// <summary>
        /// Gets if the entiy has errors.
        /// </summary>
        [Browsable(false)]
        public virtual bool IsValid
        {
            get { return string.IsNullOrWhiteSpace(Error); }
        }

        /// <summary>
        /// Gets the first error found.
        /// </summary>
        public virtual string Error
        {
            get
            {
                string retVal = string.Empty;

                PropertyInfo[] properties = this.GetType().GetProperties();

                foreach (PropertyInfo info in properties)
                {
                    if (info.Name != "Error" && info.Name != "IsValid")
                    {
                        retVal = GetError(info.Name);

                        if (!string.IsNullOrWhiteSpace(retVal))
                        {
                            break;
                        }
                    }
                }

                return retVal;
            }
        }

        /// <summary>
        /// Error calculation from IDataErrorInfo
        /// Gets the first error for the given property
        /// </summary>
        /// <param name="columnName">property name</param>
        /// <returns></returns>
        public abstract string this[string columnName] { get; }


        /// <summary>
        /// With this method you can apply custom validation if it is needed for a complex and
        /// really special type of validation that can´t be perform or is not generic at all
        /// with a custom data annotation validation attribute
        /// </summary>
        /// <param name="propertyName">The property that requires validation</param>
        /// <returns>The error message</returns>
        protected virtual string OnCustomPropertyValidation(string propertyName)
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets the first error found (if any) from the given property name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual string GetError(string propertyName)
        {
            string retVal = string.Empty;

            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName);
            var results = new List<ValidationResult>();

            if (propertyInfo.GetCustomAttribute<ValidatableProperty>() != null)
            {

                var result = Validator.TryValidateProperty(
                                          propertyInfo.GetValue(this, null),
                                          new ValidationContext(this, null, null)
                                          {
                                              MemberName = propertyName
                                          },
                                          results);

                if (!result)
                {
                    retVal = results.First().ErrorMessage;
                }
                else
                {
                    retVal = OnCustomPropertyValidation(propertyInfo.Name);
                }

            }

            return retVal;
        }

    }
}