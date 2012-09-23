﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ConverterUI
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class CandleFireImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                bool? b = value as bool?;
                if (b.HasValue)
                {
                    return b.Value ? "/ConverterUI;component/Images/fire.png" : "/ConverterUI;component/Images/no-fire.png";
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
