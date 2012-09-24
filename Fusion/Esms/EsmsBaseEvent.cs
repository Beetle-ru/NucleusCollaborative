using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
using CommonTypes;

namespace Esms
{
    [Serializable]
    [DataContract]
    public abstract class EsmsBaseEvent : BaseEvent
    {
       public EsmsBaseEvent(){ Time = DateTime.Now; }
    }
}
