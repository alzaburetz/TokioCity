using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;

namespace TokioCity.Services.Converters
{
    public class CaloriesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int calories = (int)value;
            return (string)string.Format("{0} kcal", calories);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
