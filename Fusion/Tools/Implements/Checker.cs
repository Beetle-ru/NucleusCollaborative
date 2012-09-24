using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Implements
{
    public class dMargin
    {
        public Double Low, High;
        public dMargin(Double _Low = Double.NaN, Double _High = Double.NaN)
        {
            Low = _Low;
            High = _High;
        }
    }
    public class iMargin
    {
        public Int32 Low, High;
        public iMargin(Int32 _Low = Int32.MinValue, Int32 _High = Int32.MaxValue)
        {
            Low = _Low;
            High = _High;
        }
    }

    public static class Checker
    {
        public static Color cErr = Color.FromArgb(255, 255, 200, 200);
        public static Color cEmpty = Color.FromArgb(255, 0, 255, 100);
        public static Color cNormal = Color.White;
        public static Color cOutOfRange = Color.Violet;
        public static bool isDoubleCorrect(string str, out Color color, dMargin m = null)
        {
            Double dValue;
            color = cEmpty;
            if (string.IsNullOrEmpty(str)) return false;
            color = cErr;
            if (!Double.TryParse(str, out dValue)) return false;
            if (m != null)
            {
                color = cOutOfRange;
                if (!Double.IsNaN(m.Low) && (dValue < m.Low)) return false;
                if (!Double.IsNaN(m.High) && (dValue > m.High)) return false;
            }
            color = cNormal;
            return true;
        }
        public static bool isIntCorrect(string str, out Color color, iMargin m = null)
        {
            Int32 iValue;
            color = cEmpty;
            if (string.IsNullOrEmpty(str)) return false;
            color = cErr;
            if (!Int32.TryParse(str, out iValue)) return false;
            if (m != null)
            {
                color = cOutOfRange;
                if (iValue < m.Low) return false;
                if (iValue > m.High) return false;
            }
            color = cNormal;
            return true;
        }
    }
}
