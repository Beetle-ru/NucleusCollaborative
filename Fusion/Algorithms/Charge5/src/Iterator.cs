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
            if (CalcModeIsAutomatic && VerifiInData(AutoInData) && IsRefrashData)
            {
                var outData = new OutData();
                var table = Program.Tables[AutoInData.SteelType];
                Alg(table, AutoInData, out outData);
                outData = ConverToKg(outData);
                SendResultCalc(outData);

                Saver.GetData(outData);
                Saver.SiHi = AutoInData.SiHi;
                Saver.THi = AutoInData.THi;
                Saver.SaveArch();

                IsRefrashData = false;
            }
        }

        public static OutData ConverToKg(OutData od)
        {
            var k = 1000;
            od.MDlm *= k;
            od.MDlms *= k;
            od.MFom *= k;
            od.MHi *= k;
            od.MLi *= k;
            od.MSc *= k;
            return od;
        }

        public static bool VerifiInData(InData inData)
        {
           // return ((inData.MHi > 0) || (inData.MSc > 0)) &&
             return(inData.SiHi != 0) &&
                   (inData.THi != 0);
        }

        public static bool VerifiInDataChange(InData inData)
        {
            //var result = (inData.IsProcessingUVS != m_autoInDataPrevious.IsProcessingUVS) &&
            var result = (inData.MHi != m_autoInDataPrevious.MHi) &&
                         (inData.MSc != m_autoInDataPrevious.MSc) &&
                         (inData.SiHi != m_autoInDataPrevious.SiHi) &&
                         (inData.SteelType != m_autoInDataPrevious.SteelType) &&
                         (inData.THi != m_autoInDataPrevious.THi);
            if (m_autoInDataPrevious.MHi != -1)
            {
                result = result && (inData.IsProcessingUVS != m_autoInDataPrevious.IsProcessingUVS);
            }
            if (result)
            {
                //m_autoInDataPrevious.IsProcessingUVS = inData.IsProcessingUVS;
                m_autoInDataPrevious.MHi = inData.MHi;
                m_autoInDataPrevious.MSc = inData.MSc;
                m_autoInDataPrevious.SiHi = inData.SiHi;
                m_autoInDataPrevious.SteelType = inData.SteelType;
                m_autoInDataPrevious.THi = inData.THi;
            }
            return result;
            //return true;
        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e)
        {
            Iterate();
            Console.Write(".");
        }
    }
}
