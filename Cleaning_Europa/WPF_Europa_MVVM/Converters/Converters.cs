using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EuropaWPF_App.Converters
{
    [ValueConversion(typeof(string), typeof(String))]
    public class DateTimeToShortDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime dt = (DateTime) value;
                if (dt == DateTime.MinValue)
                    return string.Empty;
                {
                    //string date = dt.ToString("d/M/yyyy");
                    string date = dt.ToShortDateString();
                    return date;
                }
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


    public class ToLowerUpperConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            if (s == null)
                return value;

            CharacterCasing casing;
            if (!Enum.TryParse(parameter as string, out casing))
                casing = CharacterCasing.Upper;

            switch (casing)
            {
                case CharacterCasing.Lower:
                    return s.ToLower(culture);
                case CharacterCasing.Upper:
                    return s.ToUpper(culture);
                default:
                    return s;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
      
    }

    [ValueConversion(typeof(string), typeof(String))]

    public class IsNotDateFilledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is DateTime)
            {
                DateTime dt = (DateTime)value;
                if (dt > DateTime.MinValue)
                    return false;
                else
                    return true;
            }

            return true;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}

