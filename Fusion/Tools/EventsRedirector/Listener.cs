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

namespace EventsRedirector {
    internal class Listener : IEventListener {
        public Listener() {
            InstantLogger.log("Listener", "Started\n", InstantLogger.TypeMessage.important);
        }

        public void OnEvent(BaseEvent evt) {
            Program.MainGateProvider.PushEvent(evt);
        }
    }
}