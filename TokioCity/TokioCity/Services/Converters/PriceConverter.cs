using System.Globalization;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System;


namespace TokioCity.Services.Converters
{
    public class PriceConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int price = (int)value;
            try
            {
                return (string)string.Format("{0}.-", price);
            }
            catch
            {
                return (string)string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
