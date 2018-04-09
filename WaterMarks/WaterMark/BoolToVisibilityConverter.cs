using System;
using System.Windows;
using System.Windows.Data;

namespace WaterMark
{
    public class BoolToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool hasText = !(bool) values[0];
            bool hasFocus = (bool)values[1];

            if (hasText || hasFocus)
                return Visibility.Collapsed;

            return Visibility.Visible;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
