using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;

namespace TokioCity.Services.Converters
{
    public class CategoryColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<int> category = (List<int>)value;
            if (category.Contains(1000036))
            {
                return (Color)Color.FromHex("#46A500");
            }
            else if (category.Contains(1000037))
            {
                return (Color)Color.FromHex("#E03636");
            }
            else if (category.Contains(1000039))
            {
                return (Color)Color.FromHex("#E39C00");
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
