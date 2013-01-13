using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;

namespace HunterEvents
{
    class Listener : IEventListener
    {
        public Listener()
        {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        public void OnEvent(BaseEvent evt)
        {
            if (evt is FlexEvent)
            {
                var flx = evt as FlexEvent;
                Program.Update(flx);
            }
        }
    }
}
