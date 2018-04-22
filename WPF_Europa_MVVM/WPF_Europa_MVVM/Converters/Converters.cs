using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPF_Europa_MVVM.Converters
{
    [ValueConversion(typeof(string), typeof(String))]
    public class DateTimeToShortDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime dt = (DateTime)value;
                string date = dt.ToShortDateString();
                //string date = dt.ToString("d/M/yyyy");
                return date;
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return DependencyProperty.UnsetValue;
        }
    }


    public class BooleanToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return (bool) value ? "Yes" : "No";
            }
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = (string) value;
            if (s.Equals("Yes",StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (s.Equals("No", StringComparison.CurrentCultureIgnoreCase))
                return false;
            throw new Exception(string.Format("Cannot convert, unknown value {0}", value));
        }
    }


    public class ToLowerCaseConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString().ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.ToString().ToLower();
        }
    }
}

