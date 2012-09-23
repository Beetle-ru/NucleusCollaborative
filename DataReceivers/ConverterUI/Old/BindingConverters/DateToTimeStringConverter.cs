using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ConverterUI
{

    [ValueConversion(typeof(bool), typeof(bool))]
    public class DateToTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime? dt = value as DateTime?;
                if (dt.HasValue && dt.Value != DateTime.MinValue)
                {
                    return dt.Value.ToLongTimeString();
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
