using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Charge5Classes;

namespace Charge5
{
    internal partial class Program
    {
        public static void Iterate()
        {
            if (CalcModeIsAutomatic && VerifiInData(AutoInData) && VerifiInDataChange(AutoInData))
            {
                var outData = new OutData();
                var table = Program.Tables[AutoInData.SteelType];
                Alg(table, AutoInData, out outData);
                SendResultCalc(outData);
            }
        }

        public static bool VerifiInData(InData inData)
        {
            return ((inData.MHi > 0) || (inData.MSc > 0)) &&
                   (inData.SiHi != 0) &&
                   (inData.THi != 0);
        }

        public static bool VerifiInDataChange(InData inData)
        {
            var result = (inData.IsProcessingUVS != m_autoInDataPrevious.IsProcessingUVS) &&
                         (inData.MHi != m_autoInDataPrevious.MHi) &&
                         (inData.MSc != m_autoInDataPrevious.MSc) &&
                         (inData.SiHi != m_autoInDataPrevious.SiHi) &&
                         (inData.SteelType != m_autoInDataPrevious.SteelType) &&
                         (inData.THi != m_autoInDataPrevious.THi);
            if (result)
            {
                m_autoInDataPrevious.IsProcessingUVS = inData.IsProcessingUVS;
                m_autoInDataPrevious.MHi = inData.MHi;
                m_autoInDataPrevious.MSc = inData.MSc;
                m_autoInDataPrevious.SiHi = inData.SiHi;
                m_autoInDataPrevious.SteelType = inData.SteelType;
                m_autoInDataPrevious.THi = inData.THi;
            }
            return result;
        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e)
        {
            Iterate();
            Console.Write(".");
        }
    }
}
