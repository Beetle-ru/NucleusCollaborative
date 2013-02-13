using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Implements;

namespace OGDecarbonaterFine
{
    static partial class Iterator
    {
        public static void Iterate()
        {

        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e)
        {
            Iterate();
            Console.Write(".");
        }
    }
}
