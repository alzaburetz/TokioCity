using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;

namespace TokioCity.Services.Converters
{
    public class CategoryConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<int> category = (List<int>)value;
            if  (category.Contains(1000036))
            {
                return (string)"Новое";
            }
            else if (category.Contains(1000037))
            {
                return (string)"Острое";
            }
            else if (category.Contains(1000039))
            {
                return (string)"Хит";
            }
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
