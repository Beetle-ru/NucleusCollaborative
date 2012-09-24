using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CommonTypes;

namespace Converter
{
    public enum FlexEventFlag
    {
        FlexEventCreated = 1,
        FlexEventFired = FlexEventCreated << 1,
    }
    [Serializable]
    [DataContract]
    public class FlexEvent : BaseEvent
    {
        [DataMember]
        public Guid Id { set; get; }
        [DataMember]
        public string Operation { set; get; }
        [DataMember]
        public FlexEventFlag Flags { set; get; }
        [DataMember]
        public Dictionary<string, object> Arguments { set; get; }

        public FlexEvent(/*String Operation_*/)
        {
            Id = new Guid();
            /*Operation = Operation_;*/
            Flags = FlexEventFlag.FlexEventCreated;
            Arguments = new Dictionary<string, object>();
        }
        public FlexEvent(String Operation_)
        {
            Id = new Guid();
            Operation = Operation_;
            Flags = FlexEventFlag.FlexEventCreated;
            Arguments = new Dictionary<string, object>();
        }
        public override string ToString()
        {

            var properties = this.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}:", GetType().Name);

            foreach (var property in properties)
            {
                if (property.Name == "Arguments")
                {
                    string s = "\nArguments=<dictionary>";
                    s = Arguments.Keys.Aggregate(s, (current, key) => current + String.Format("\n\t{0}\t:{1}", key, Arguments[key]));
                    sb.AppendFormat("{0}\n</dictionary>;", s);
                    
                }
                else sb.AppendFormat("\n{0}={1};", property.Name, property.GetValue(this, null));
            }

            return sb.ToString();
        }
    }
}
