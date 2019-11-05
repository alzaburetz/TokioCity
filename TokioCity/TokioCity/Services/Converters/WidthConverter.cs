using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using System.Globalization;
using Xamarin.Essentials;

namespace TokioCity.Services.Converters
{
    public class WidthConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)DeviceDisplay.MainDisplayInfo.Width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
