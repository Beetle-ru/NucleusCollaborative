using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPCFledged
{
    public static class BoolExpressions
    {
        public static bool GetBit(byte b, int bitNumber)
        {
            if (bitNumber >= 0 && bitNumber < 8)
            {
                return (b & (1 << bitNumber)) > 0;
            }

            throw new IndexOutOfRangeException("Номер бита должен быть в диапазоне от 0 до 7");
        }

        public static byte SetBit(byte b, int bitNumber, bool state)
        {
            if (bitNumber >= 0 && bitNumber < 8)
            {
                if (GetBit(b, bitNumber) != state)
                {
                    return (byte)(b ^ (1 << bitNumber));
                }
                return b;
            }
            else
            {
                throw new IndexOutOfRangeException("Номер бита должен быть в диапазоне от 0 до 7");
            }
        }
    }
}
