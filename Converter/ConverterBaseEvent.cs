using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
using CommonTypes;

namespace Converter
{
    [Serializable]
    [DataContract]
    public abstract class ConverterBaseEvent : BaseEvent
    {
       public ConverterBaseEvent(){
          Time = DateTime.Now;
       }
    
       [DataMember]
       public int iCnvNr { set; get; }               // Номер конвертера

    }
}
