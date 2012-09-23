using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Converter
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class OPCDirectReadEvent : ConverterBaseEvent
    {
        /// <summary>
        /// </summary>
        public string EventName { set; get; }

        public OPCDirectReadEvent()
        {
            EventName = "OPCDirectReadEvent";
        }
        public override string ToString()
        {
            string str = base.ToString() + "<";
            str += EventName + ";";
            return str + ">";
        }

    }
}
