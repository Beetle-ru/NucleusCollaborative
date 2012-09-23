using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;

namespace FlexEventSaver
{
    class Listener : IEventListener
    {
        //public Listener()
        //{
        //}
        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("FlexEventSaver"))
            {
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("PipeCatcher.Call."))
                    {
                        string str = "\n================================================";
                        foreach(String key in fxe.Arguments.Keys)
                        {
                            str += String.Format("\n{0}\t:{1}", key, fxe.Arguments[key]);
                        }
                        l.msg(str);
                    }
                    else
                    {
                        string s = String.Format("Id={0} Time={1}\nOperation={2}\nFlags={3}\nArguments:", fxe.Id, fxe.Time, fxe.Operation, fxe.Flags);
                        s = fxe.Arguments.Keys.Aggregate(s, (current, key) => current + String.Format("\n\t{0}\t:{1}", key, fxe.Arguments[key]));
                        l.msg("{0}\n==============================\n", s);
                    }
                }
            }
        }
    }
}
