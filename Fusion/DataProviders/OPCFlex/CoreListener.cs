using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using CommonTypes;
using Implements;
using Core;
using OPC.Data;
using OPC.Common;

namespace OPCFlex
{
    internal class CoreListener : IEventListener
    {
        private Type m_flexEventType;
        private object conv(string str)
        {
            var res = new byte[6] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20};

            for (int i = 0; i < Math.Min(str.Length, 6); i++)
            {
                int code = (int) str.ElementAt(i);
                if (code > 900) code -= 848;
                res[i] = (byte) code;
            }
            return res;
        }


        public CoreListener(Type flexEventType)
        {
            using (Logger l = new Logger("CoreListener"))
            {
                m_flexEventType = flexEventType;
                l.msg("Started", "Listener", InstantLogger.TypeMessage.unimportant);
            }
        }

        public void OnEvent(BaseEvent evt)
        {
            
            using (Logger l = new Logger("OnEvent"))
            {
                if (evt.GetType() == m_flexEventType)
                {
                    Console.WriteLine(evt);
                }
            }
        }
    }
}