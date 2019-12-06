using System.Globalization;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System;

using System.Collections.ObjectModel;
using TokioCity.Models;

namespace TokioCity.Services.Converters
{
    public class ListMoreThanZero : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typ = value.GetType();
            if (value == null)
            {
                return false;
            }
            else
            {
                return ((List<AppItem>)value as List<AppItem>).Count > 0;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
