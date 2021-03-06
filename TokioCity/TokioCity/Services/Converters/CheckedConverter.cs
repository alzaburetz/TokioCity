﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;

namespace TokioCity.Services.Converters
{
    public class CheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "checkmarked";
            }
            else
            {
                return "checkmark";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}

