using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class EventArgsBool : EventArgs
    {
        public bool Value;

        /// <summary>
        /// Initializes a new instance of the EventArgsBool class.
        /// </summary>
        /// <param name="aValue"></param>
        public EventArgsBool(bool aValue)
        {
            Value = aValue;
        }
    }
    public class EventArgsMOUT_Message : EventArgs
    {
        public string Message { get; set; }
        public Enumerations.MOUT_Message_Enum Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the EventArgsMOUT_Message class.
        /// </summary>
        public EventArgsMOUT_Message(Enumerations.MOUT_Message_Enum aType, string aMessage)
        {
            Message = aMessage;
            Type = aType;
        }
    }
}
