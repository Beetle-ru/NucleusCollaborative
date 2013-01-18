using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using ConnectionProvider;
using Converter;
using DTO;
using Data;
using Data.Model;
using Common;
using Implements;
using Models;
using System.Linq;

namespace ModelRunner
{
    partial class DynPrepare
    {
        public static void FirePerSecEvent(int nS, ConnectionProvider.FlexHelper f, Models.Dynamic mo)
        {
            var dynout = mo.LastOutputData;
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.PerSecond");
            fex.AddInt("@RelativeSecond", nS);
            fex.AddDbl("C", dynout.FP_Kov[0]);
            fex.AddDbl("T", dynout.T_Tavby);
            fex.AddDbl("Si", dynout.FP_Kov[1]);
            fex.AddDbl("Mn", dynout.FP_Kov[2]);
            fex.AddDbl("P", dynout.FP_Kov[3]);
            fex.AddDbl("Al", dynout.FP_Kov[5]);
            fex.AddDbl("Cr", dynout.FP_Kov[7]);
            fex.AddDbl("V", dynout.FP_Kov[10]);
            fex.AddDbl("Ti", dynout.FP_Kov[11]);
            fex.AddDbl("Fe", dynout.FP_Kov[32]);
            fex.AddDbl("FeO", dynout.FP_Struska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddDbl("CaO", dynout.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddDbl("SiO2", dynout.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddDbl("MnO", dynout.FP_Struska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddDbl("MgO", dynout.FP_Struska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            double mCaO = dynout.m_SlozkaStruska[0];
            double mSiO2 = dynout.m_SlozkaStruska[1];
            if (mSiO2 > 0.0)
            {
                fex.AddDbl("CaO/SiO2", mCaO / mSiO2);
            }
            fex.Fire(CoreGate);
        }

        public static void FireModelNoDataEvent(string Reason, string RCode)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.NoData");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddStr("Reason", Reason);
            fex.AddStr("RCode", RCode);
            fex.Fire(CoreGate);
        }

        public static void FireIronEvent()
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Iron");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddDbl("Iron_Weight", Listener.IronWeight);
            fex.AddStr("Iron_Reason", Listener.IronReason);
            fex.Fire(CoreGate);
        }

        public static void FireTemperatureEvent(Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Temperature");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddDbl("Final_T", mo.LastOutputData.T_Tavby);
            fex.AddDbl("Final_C", mo.LastOutputData.FP_Kov[0]);
            fex.Fire(CoreGate);
        }

        public static void FireXimstalEvent(Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.XIMSTAL");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddDbl("Final_C", mo.LastOutputData.FP_Kov[0]);
            fex.AddDbl("Final_Si", mo.LastOutputData.FP_Kov[1]);
            fex.AddDbl("Final_Mn", mo.LastOutputData.FP_Kov[2]);
            fex.AddDbl("Final_P", mo.LastOutputData.FP_Kov[3]);
            fex.AddDbl("Final_Al", mo.LastOutputData.FP_Kov[5]);
            fex.AddDbl("Final_Cr", mo.LastOutputData.FP_Kov[7]);
            fex.AddDbl("Final_V", mo.LastOutputData.FP_Kov[10]);
            fex.AddDbl("Final_Ti", mo.LastOutputData.FP_Kov[11]);
            fex.AddDbl("Final_Fe", mo.LastOutputData.FP_Kov[32]);
            fex.Fire(CoreGate);
        }

        public static void FireXimslagEvent(Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.XIMSLAG");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddDbl("Final_FeO", mo.LastOutputData.FP_Struska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddDbl("Final_CaO", mo.LastOutputData.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddDbl("Final_SiO2", mo.LastOutputData.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddDbl("Final_MnO", mo.LastOutputData.FP_Struska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddDbl("Final_MgO", mo.LastOutputData.FP_Struska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.Fire(CoreGate);
        }

        public static void FireAdditionsEvent(Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Additions");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddDbl("LIME", Listener.CurrWeight["LIME"]);
            fex.AddDbl("DOLOMS", Listener.CurrWeight["DOLOMS"]);
            fex.AddDbl("DOLMAX", Listener.CurrWeight["DOLMAX"]);
            fex.AddDbl("FOM", Listener.CurrWeight["FOM"]);
            fex.AddDbl("COKE", Listener.CurrWeight["COKE"]);
            fex.Fire(CoreGate);
        }

        public static void FireShixtaDoneEvent(ChargingOutput aut)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.ShixtaII");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddInt("ResultCode", aut.ErrCode);
            fex.AddDbl("Oxygen", aut.OxygenAmountTotalEnd_Nm3);
            fex.AddDbl("LIME", aut.m_lime);
            fex.AddDbl("DOLOMS", aut.m_dolomite);
            fex.AddDbl("DOLMAX", Listener.VisWeight["DOLMAX"]);
            fex.AddDbl("FOM", Listener.VisWeight["FOM"]);
            fex.AddDbl("COKE", Listener.VisWeight["COKE"]);
            fex.Fire(CoreGate);
        }
    }
}