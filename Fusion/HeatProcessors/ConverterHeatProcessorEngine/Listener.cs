using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;

namespace ConverterHeatProcessorEngine {
    internal class Listener : IEventListener {
        public Listener() {
            // InstantLogger.log("Listener started");
            // HeatEngine.RequestWeighersState(); // как только листененр готов получаем состояние весов
        }

        public void OnEvent(BaseEvent newEvent) {
            if (
                (newEvent is SteelMakingPatternEvent) ||
                (newEvent is BlowingEvent) ||
                (newEvent is WeighersStateEvent) ||
                (newEvent is ReleaseWeigherEvent) ||
                (newEvent is HeatChangeEvent) ||
                (newEvent is LanceEvent)
                ) {
                //HeatEngine.SmPattern = newEvent as SteelMakingPatternEvent;
                HeatEngine.Processor(newEvent);
                // InstantLogger.log(newEvent.ToString(), "receive message", InstantLogger.TypeMessage.important);
            }

            //InstantLogger.log(newEvent.ToString(), "receive message", InstantLogger.TypeMessage.important);
            // HeatEngine.ComSender();
            //throw new NotImplementedException();
        }
    }
}