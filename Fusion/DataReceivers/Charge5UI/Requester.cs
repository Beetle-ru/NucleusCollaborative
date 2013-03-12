using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charge5Classes;
using ConnectionProvider;

namespace Charge5UI {
    internal static class Requester {
        public static Client MainGate;

        public static void ReqCalc(Client CoreGate, InData modelInData) {
            var fex = new FlexHelper("UI.Calc");
            fex.AddArg("SteelType", modelInData.SteelType);
            fex.AddArg("MHi", modelInData.MHi);
            fex.AddArg("MSc", modelInData.MSc);
            fex.AddArg("SiHi", modelInData.SiHi);
            fex.AddArg("THi", modelInData.THi);
            fex.AddArg("IsProcessingUVS", modelInData.IsProcessingUVS);
            fex.Fire(CoreGate);
        }

        public static void ReqPatternLoad(Client CoreGate, string PatternName) {
            var fex = new FlexHelper("UI.LoadPattern");
            fex.AddArg("Name", PatternName);
            fex.Fire(CoreGate);
        }

        public static void ReqGetPattern(Client CoreGate, string PatternName) {
            var fex = new FlexHelper("UI.GetPattern");
            fex.AddArg("Name", PatternName);
            fex.Fire(CoreGate);
        }

        public static void ReqRemoovePattern(Client CoreGate, string PatternName) {
            var fex = new FlexHelper("UI.RemoovePattern");
            fex.AddArg("Name", PatternName);
            fex.Fire(CoreGate);
        }

        public static void ReqPatternNames(Client CoreGate) {
            var fex = new FlexHelper("UI.GetNamePatterns");
            fex.Fire(CoreGate);
        }
    }
}