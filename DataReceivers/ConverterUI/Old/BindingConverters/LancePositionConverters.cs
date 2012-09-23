using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ConverterUI
{
    [ValueConversion(typeof(decimal), typeof(string))]
    public class LancePageLancePositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int intValue = (int)value;
            double val = 0;
            val = -intValue * 0.31 + 100;
            if (val < -160)
                return -160;
            if(val > 30)
                return 30;
            return val;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(decimal), typeof(string))]
    public class MainPageLancePositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int intValue = (int)value;
            return -100 + (1200 - intValue)/10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
