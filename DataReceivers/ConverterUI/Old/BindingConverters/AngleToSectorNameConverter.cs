using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ConverterUI
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class AngleToSectorNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Helper.HeatInfo.AggregateAngle.SectorName(Helper.HeatInfo.AggregateNumber);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
