using CommonTypes;
using ConnectionProvider;
using Converter;
using Implements;

namespace DBFlex {
    internal class Listener : IEventListener {
        public Listener() {
            InstantLogger.log("Listener", "Started\n", InstantLogger.TypeMessage.important);
        }

        #region IEventListener Members

        public void OnEvent(BaseEvent evt) {
            using (var l = new Logger("Listener")) {
                if (evt is FlexEvent) {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("DBFlex.Request")) Program.Job(fxe);
                }
            }
        }

        #endregion
    }
}