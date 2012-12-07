using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Charge5Classes;
using ConnectionProvider;

namespace Charge5
{
    internal partial class Program
    {
        public static void SendResultCalc(OutData outData)
        {
            var fex = new FlexHelper("Charge5.ResultCalc");

            fex.AddArg("MDlm", outData.MDlm);       // int
            fex.AddArg("MDlms", outData.MDlms);     // int
            fex.AddArg("MFom", outData.MFom);       // int
            fex.AddArg("MHi", outData.MHi);         // int
            fex.AddArg("MLi", outData.MLi);         // int
            fex.AddArg("MSc", outData.MSc);         // int
            fex.AddArg("IsFound", outData.IsFound); // bool

            fex.Fire(Program.MainGate);
        }
    }
}