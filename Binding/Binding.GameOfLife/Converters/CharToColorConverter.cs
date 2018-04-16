using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Binding.GameOfLife.Interfaces;

namespace Binding.GameOfLife.Converters
{
    public class CharToColorConverter : IValueConverter
    {

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                switch ((char)value)
                {
                    case 'A':
                        return Brushes.Green;
                    case 'D':
                        return Brushes.Red;
                    case 'E':
                        return Brushes.WhiteSmoke;
                    default:
                        return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
