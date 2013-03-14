using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;

namespace OPCDirectWriter {
    internal class Listener : IEventListener {
        public Listener() {
            InstantLogger.log("Started", "Listener", InstantLogger.TypeMessage.unimportant);
        }

        public void OnEvent(BaseEvent newEvent) {
            if (newEvent is comAdditionsEvent) {
                InstantLogger.log(newEvent.ToString(), "receive message", InstantLogger.TypeMessage.important);
                Program.OPCCon.Send(newEvent as comAdditionsEvent);
            }
        }
    }
}