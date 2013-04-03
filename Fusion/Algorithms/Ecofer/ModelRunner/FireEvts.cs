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
    partial class DynPrepare {
        private const int DIGS = 5;
        public static void FirePerSecEvent(int nS, ConnectionProvider.FlexHelper f, Models.Dynamic mo)
        {
            var dynout = mo.LastOutputData;
            var fex = new ConnectionProvider.FlexHelper("Nedobritty.Mudak");
            fex.AddInt("@RelativeSecond", nS);
            fex.AddDbl("C", Math.Round(dynout.FP_Kov[0], DIGS));
            fex.AddDbl("T", Math.Round(dynout.T_Tavby, DIGS));
            fex.AddDbl("Si", Math.Round(dynout.FP_Kov[1], DIGS));
            fex.AddDbl("Mn", Math.Round(dynout.FP_Kov[2], DIGS));
            fex.AddDbl("P", Math.Round(dynout.FP_Kov[3], DIGS));
            fex.AddDbl("Al", Math.Round(dynout.FP_Kov[5], DIGS));
            fex.AddDbl("Cr", Math.Round(dynout.FP_Kov[7], DIGS));
            fex.AddDbl("V", Math.Round(dynout.FP_Kov[10], DIGS));
            fex.AddDbl("Ti", Math.Round(dynout.FP_Kov[11], DIGS));
            fex.AddDbl("Fe", Math.Round(dynout.FP_Kov[32], DIGS));
            fex.AddDbl("FeO", Math.Round(dynout.FP_Struska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX], DIGS));
            fex.AddDbl("CaO", Math.Round(dynout.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX], DIGS));
            fex.AddDbl("SiO2", Math.Round(dynout.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX], DIGS));
            fex.AddDbl("MnO", Math.Round(dynout.FP_Struska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX], DIGS));
            fex.AddDbl("MgO", Math.Round(dynout.FP_Struska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX], DIGS));
            double mCaO = dynout.m_SlozkaStruska[0];
            double mSiO2 = dynout.m_SlozkaStruska[1];
            if (mSiO2 > 0.0)
            {
                fex.AddDbl("CaO/SiO2", Math.Round(mCaO / mSiO2, DIGS));
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
            fex.AddDbl("Iron_Temperature", Listener.IronTemp);
            fex.AddStr("Iron_Reason", Listener.IronReason);
            fex.Fire(CoreGate);
        }

        public static void FireTempCarboneEvent(Models.Dynamic mo)
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
            fex.AddDbl("DOLMIT", Listener.CurrWeight["DOLMIT"]);
            fex.AddDbl("MAXG", Listener.CurrWeight["MAXG"]);
            fex.AddDbl("FOM", Listener.CurrWeight["FOM"]);
            fex.AddDbl("COKE", Listener.CurrWeight["COKE"]);
            fex.Fire(CoreGate);
        }

        public static void FireShixtaDoneEvent(ChargingOutput aut)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.ShixtaII");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddDbl("Oxygen", aut.OxygenAmountTotalEnd_Nm3);
            fex.AddInt("ResultCode", aut.ErrCode);
            fex.AddDbl("LIME", aut.m_lime);
            fex.AddDbl("DOLOMS", aut.m_dolomite);
            fex.AddDbl("DOLMIT", Listener.VisWeight["DOLMIT"]);
            fex.AddDbl("MAXG", Listener.VisWeight["MAXG"]);
            fex.AddDbl("FOM", Listener.VisWeight["FOM"]);
            fex.Fire(CoreGate);
        }

        public static void FireScrapDangerEvent()
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Scrap.Danger");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddDbl("Prob", Listener.ScrapDanger);
            fex.AddStr("Descr", Listener.ScrapDanger > 0.75 ? "HIGH" : "MEDIUM");
            fex.Fire(CoreGate);
        }

        public static void FireChemistryEvent(string Substance, DTO.MINP_GD_MaterialDTO mat)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Chemistry");
            fex.AddInt("Heat_No", Listener.HeatNumber);
            fex.AddStr("Substance", Substance);
            if ("IRON" == Substance)
            {
                fex.AddInt("IronIsValid", Listener.IronIsValid ? 1 : 0);
                fex.AddInt("IronCIsValid", Listener.IronCIsValid ? 1 : 0);
            }
            foreach (var m in mat.MINP_GD_MaterialItems)
            {
                fex.AddDbl(fp.fp[m.MINP_GD_MaterialElement.Index].Marking, m.Amount_p);
            }
            fex.Fire(CoreGate);
        }
    }
}