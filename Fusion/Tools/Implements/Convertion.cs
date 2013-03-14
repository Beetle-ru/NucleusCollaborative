using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Implements {
    public static class Convertion {
        public static double StrToDouble(string txtValue) {
            double dValue = 0.0;
            if (!System.Double.TryParse(txtValue, out dValue))
                throw new Exception(String.Format("Fail to convert \"{0}\" to Double", txtValue));
            return dValue;
        }

        public static Int32 StrToInt32(string txtValue) {
            Int32 iValue = 0;
            if (!System.Int32.TryParse(txtValue, out iValue))
                throw new Exception(String.Format("Fail to convert \"{0}\" to Int32", txtValue));
            return iValue;
        }

        public static Int64 StrToInt64(string txtValue) {
            Int64 iValue = 0;
            if (!System.Int64.TryParse(txtValue, out iValue))
                throw new Exception(String.Format("Fail to convert \"{0}\" to Int64", txtValue));
            return iValue;
        }

        public static DateTime StrToDateTime(string txtValue) {
            DateTime iValue;
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (!System.DateTime.TryParse(txtValue, out iValue))
                throw new Exception(String.Format("Fail to convert \"{0}\" to DateTime", txtValue));
            return iValue;
        }

        public static bool StrToBool(string txtValue) {
            bool iValue;
            if (!System.Boolean.TryParse(txtValue, out iValue))
                throw new Exception(String.Format("Fail to convert \"{0}\" to Boolean", txtValue));
            return iValue;
        }
    }
}