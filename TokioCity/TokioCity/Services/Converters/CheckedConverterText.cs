using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using System.Globalization;

namespace TokioCity.Services.Converters
{
    public class CheckedConverterText: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return 1.0f;
            }
            else
            {
                return 0.5f;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
