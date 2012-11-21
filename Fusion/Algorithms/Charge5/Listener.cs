using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConnectionProvider;
using Core;
using Converter;
using CommonTypes;
using ConnectionProvider.MainGate;
using Implements;
//using System.ServiceModel;
//using System.Windows.Forms;

namespace Charge5
{
    class Listener : IEventListener
    {
        public Listener()
        {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        

        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("Listener"))
            {
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("test"))
                    {
                        l.msg(fxe.ToString());
                    }
                }
            }
        }
    }
}
