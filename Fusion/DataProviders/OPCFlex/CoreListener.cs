using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionProvider;
using CommonTypes;
using Implements;
using Converter;

namespace OPCFlex {
    internal class CoreListener : IEventListener {
        private object conv(string str) {
            var res = new byte[6] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20};

            for (int i = 0; i < Math.Min(str.Length, 6); i++) {
                int code = (int) str.ElementAt(i);
                if (code > 900) code -= 848;
                res[i] = (byte) code;
            }
            return res;
        }


        public CoreListener() {
            using (Logger l = new Logger("CoreListener"))
                l.msg("Started", "Listener", InstantLogger.TypeMessage.unimportant);
        }

        public void OnEvent(BaseEvent evt) {
            using (Logger l = new Logger("OnEvent")) {
                const string OPKEY = "OPC.Read-";
                if (evt is FlexEvent) {
                    var fex = evt as FlexEvent;
                    if (fex.Operation.StartsWith(OPKEY)) {
                        var targetOpCode = fex.Operation.Substring(OPKEY.Length);
                        foreach (var d in Program.descriptions) {
                            if (d.Operation.StartsWith(targetOpCode))
                                Program.fireFlex(d);
                        }
                    }
                    else if ((fex.Flags & FlexEventFlag.FlexEventOpcNotification) == 0) {
                        int[] aE;
                        foreach (var d in Program.descriptions) {
                            if (fex.Operation == d.Operation) {
                                Console.WriteLine("Matching FlexEvent \"{0}\"", fex.Operation);
                                var sHandles = new List<int>();
                                var values = new List<object>();
                                foreach (var fa in fex.Arguments) {
                                    foreach (var da in d.Arguments) {
                                        if (fa.Key == da.Key) {
                                            // Copy Value by ServerHandle
                                            sHandles.Add(((Element) da.Value).sHandle);
                                            ((Element) da.Value).val = fa.Value;
                                            values.Add(fa.Value);
                                        }
                                    }
                                    Program.OpcGroup_.Write(sHandles.ToArray(), values.ToArray(), out aE);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}