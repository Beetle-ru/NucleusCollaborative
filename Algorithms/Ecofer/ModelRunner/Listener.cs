using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;

namespace ModelRunner
{
    class Listener : IEventListener
    {
        public static RollingAverage avox = new RollingAverage();
        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("FlexEventSaver"))
            {
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("CastIronCorrection"))
                    {
                        l.msg("Iron Correction: {0}\n", fxe.Arguments["Correction"]);
                    }
                    else
                    {
                        l.msg("FlexEvent Appeared: {0}\n", fxe);
                    }
                }
                else if (evt is LanceEvent)
                {
                    var lae = evt as LanceEvent;
                    l.msg("Oxygen Flow: {0}", lae.O2Flow);
                    avox.Add(lae.O2Flow);
                }
            }
        }
    }
}
