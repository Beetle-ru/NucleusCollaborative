using System;
using System.Linq;
using ConnectionProvider;
using CommonTypes;
using Implements;
using Converter;

namespace OPCFlex
{
    internal class CoreListener : IEventListener
    {
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


        public CoreListener()
        {
            using (Logger l = new Logger("CoreListener"))
            {
                l.msg("Started", "Listener", InstantLogger.TypeMessage.unimportant);
            }
        }

        public void OnEvent(BaseEvent evt)
        {
            using (Logger l = new Logger("OnEvent"))
            {
                if (evt is FlexEvent)
                {
                    var fe = evt as FlexEvent;
                    //Console.WriteLine("\n" + fe);
                }
            }
        }
    }
}