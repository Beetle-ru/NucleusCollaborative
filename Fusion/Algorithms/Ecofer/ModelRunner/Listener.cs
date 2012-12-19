using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using ConnectionProvider;
using Converter;
using CommonTypes;
using DTO;
using Data;
using Data.Model;
using Implements;
using System.Configuration;
using Models;

namespace ModelRunner
{
    internal class Listener : IEventListener
    {
        public static RollingAverage avox = new RollingAverage(1200.1133);
        public static RollingAverage avofg = new RollingAverage(360000.1133);
        public static RollingAverage avofg_pco = new RollingAverage(10.1133);
        public static RollingAverage avofg_pco2 = new RollingAverage(10.1133);
        public static long HeatNumber = -1;
        public static double IronWeight = 300000;
        public static string IronReason = "DEFAULT";
        public static double ScrapWeight = 114000;
        public static string ScrapReason = "DEFAULT";
        public static double ScrapDanger = 0.0;
        public static int Converter = 0;
        public static int ForceBlow = 0;

        private const int bunkersCount = 8;
        private static List<double> m_bunkersTotalMass = new List<double>();
        private static List<string> m_bunkersNames = new List<string>();
        public static List<MINP_MatAddDTO> MatAdd = new List<MINP_MatAddDTO>();
        public static Dictionary<string, int> CurrWeight = new Dictionary<string, int>();
        public static Dictionary<string, int> ModWeight = new Dictionary<string, int>();
        public static Dictionary<string, int> VisWeight = new Dictionary<string, int>();
        public static Dictionary<string, string> VisKey = new Dictionary<string, string>();
        public static Dictionary<string, string> matRename = new Dictionary<string, string>();

        private class VBItem
        {
            public double coeff;
            public string scraps;

            public VBItem(double c, string s)
            {
                coeff = c;
                scraps = s;
            }
        }

        private static List<VBItem> lvb = new List<VBItem>();

        public static Charging shixtaII;
        public static object Lock = new object();

        public Listener()
        {
            Converter =
                Convert.ToInt32(ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["Converter"].Value);
            ForceBlow =
                Convert.ToInt32(ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["ForceBlow"].Value);
            for (var i = 0; i < bunkersCount; i++)
            {
                m_bunkersTotalMass.Add(0.0);
                m_bunkersNames.Add("");
            }
            matRename.Add("?", "---");
            matRename.Add("ИЗВЕСТ", "LIME");
            matRename.Add("ДОЛОМС", "DOLOMS");
            matRename.Add("ДОЛМИТ", "DOLMAX");
            matRename.Add("МАХГ  ", "DOLMAX");
            matRename.Add("ФОМ   ", "FOM");
            matRename.Add("KOKS  ", "COKE");
            matRename.Add("ALKонц", "ALCONZ");
            CurrWeight.Add("LIME", 1);
            CurrWeight.Add("DOLOMS", 1);
            CurrWeight.Add("DOLMAX", 1);
            CurrWeight.Add("FOM", 1);
            CurrWeight.Add("COKE", 1);
            VisWeight.Add("LIME", 0);
            VisWeight.Add("DOLOMS", 0);
            VisWeight.Add("DOLMAX", 0);
            VisWeight.Add("FOM", 0);
            VisWeight.Add("COKE", 0);
            VisWeight.Add("ALCONZ", 0);
            VisKey.Add("LIME", "?");
            VisKey.Add("DOLOMS", "?");
            VisKey.Add("DOLMAX", "?");
            VisKey.Add("FOM", "?");
            VisKey.Add("COKE", "?");
            VisKey.Add("ALCONZ", "?");
            lvb.Add(new VBItem(0.2, "=3="));
            lvb.Add(new VBItem(0.3, "=8=24=36="));
            lvb.Add(new VBItem(0.5, "=18=2=73="));
            lvb.Add(new VBItem(0.8, "=99=66="));
            lvb.Add(new VBItem(1.0, "=89=33=35=28=98=96="));
            for (var i = 0; i < 8; i++) m_bunkersNames[i] = "?";
        }

        private static double VBProb(int ScrapType, int ScrapWeight)
        {
            foreach (var lvbi in lvb)
            {
                if (lvbi.scraps.Contains(string.Format("={0}=", ScrapType)))
                {
                    return 5*lvbi.coeff*ScrapWeight;
                }
            }
            return 0.0;
        }

        public static string Encoder(string str)
        {
            char[] charArray = str.ToCharArray();
            str = "";
            foreach (char c in charArray)
            {
                if (c > 127)
                {
                    str += (char) (c + 848);
                }
                else
                {
                    str += c;
                }
            }
            return str;
        }

        public static void MVBounder(Logger l)
        {
            Dictionary<string, double> MVDic = new Dictionary<string, double>();
            for (var i = 0; i < bunkersCount; i++)
            {
                ///! AR: reset bunkerNames if empty
                if ((m_bunkersNames[i] != "") && (m_bunkersTotalMass[i] != 0))
                {
                    if (Encoder(m_bunkersNames[i]) == "ИЗВЕСТ")
                    {
                        if (CurrWeight["LIME"] < (int) m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddCaO((int) m_bunkersTotalMass[i] - CurrWeight["LIME"]));
                            CurrWeight["LIME"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material added LIME: {0}", (int) m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "ДОЛОМС")
                    {
                        if (CurrWeight["DOLOMS"] < (int) m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddDolomS((int) m_bunkersTotalMass[i] - CurrWeight["DOLOMS"]));
                            CurrWeight["DOLOMS"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material added DOLOMS: {0}", (int) m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "ДОЛМИТ")
                    {
                        if (CurrWeight["DOLMAX"] < (int) m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddDolom((int) m_bunkersTotalMass[i] - CurrWeight["DOLMAX"]));
                            CurrWeight["DOLMAX"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material added DOLMIT: {0}", (int) m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "МАХГ  ")
                    {
                        if (CurrWeight["DOLMAX"] < (int) m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddDolom((int) m_bunkersTotalMass[i] - CurrWeight["DOLMAX"]));
                            CurrWeight["DOLMAX"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material added MAXG: {0}", (int) m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "ФОМ   ")
                    {
                        if (CurrWeight["FOM"] < (int) m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddFom((int) m_bunkersTotalMass[i] - CurrWeight["FOM"]));
                            CurrWeight["FOM"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material Added FOM: {0}", (int) m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "KOKS  ")
                    {
                        if (CurrWeight["COKE"] < (int) m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddCoke((int) m_bunkersTotalMass[i] - CurrWeight["COKE"]));
                            CurrWeight["COKE"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material added COKE: {0}", (int) m_bunkersTotalMass[i]);
                        }
                    }
                }
                //else throw new Exception("Either bunker names or values are empty");
            }
        }

        private void CollectOxygen(SteelMakingPatternEvent smpe)
        {
            DynPrepare.aInputData.OxygenBlowingPhases = new List<PhaseItem>();
            var ph1 = new PhaseItemL1Command();
            var ph2 = new PhaseItemOxygenBlowing();
            ph1.PhaseName = "Initial";
            ph1.L1Command = Enumerations.L2L1_Command.OxygenBlowingStart;
            ph1.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
            DynPrepare.aInputData.OxygenBlowingPhases.Add(ph1);
            for (var step = 0; step < smpe.steps.Count; step++)
            {
                var cstep = smpe.steps[step];
                ph2 = new PhaseItemOxygenBlowing();
                ph2.PhaseName = String.Format("OxyBlowStep{0}", step);
                ph2.LanceDistance_mm = cstep.lance.LancePositin;
                ph2.O2Amount_Nm3 = cstep.O2Volume;
                ph2.O2Flow_Nm3_min = Convert.ToInt32(Math.Ceiling(cstep.lance.O2Flow));
                ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                DynPrepare.aInputData.OxygenBlowingPhases.Add(ph2);
            }
            ph1 = new PhaseItemL1Command();
            ph1.PhaseName = "Measure";
            ph1.L1Command = Enumerations.L2L1_Command.TemperatureMeasurement;
            ph1.PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection;
            DynPrepare.aInputData.OxygenBlowingPhases.Add(ph1);

            ph2 = new PhaseItemOxygenBlowing();
            ph2.PhaseName = "Correction";
            ph2.LanceDistance_mm = 220;
            ph2.O2Flow_Nm3_min = 1200;
            ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection;
            DynPrepare.aInputData.OxygenBlowingPhases.Add(ph2);

            ph1 = new PhaseItemL1Command();
            ph1.PhaseName = "Parking";
            ph1.L1Command = Enumerations.L2L1_Command.OxygenLanceToParkingPosition;
            ph1.PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection;
            DynPrepare.aInputData.OxygenBlowingPhases.Add(ph1);
        }

        private void CollectAdditions(SteelMakingPatternEvent smpe, Logger l)
        {
            ModWeight["LIME"] = 0;
            ModWeight["DOLOMS"] = 0;
            ModWeight["DOLMAX"] = 0;
            ModWeight["FOM"] = 0;
            ModWeight["COKE"] = 0;
            for (var step = 0; step < smpe.steps.Count; step++)
            {
                if (smpe.steps[step] == null) continue;
                for (int weirline = 0; weirline < smpe.steps[step].weigherLines.Count; weirline++)
                {
                    if (smpe.steps[step].weigherLines[weirline] == null) continue;
                    for (int bunkerId = 0; bunkerId < m_bunkersNames.Count; bunkerId++)
                    {
                        if (smpe.steps[step].weigherLines[weirline].BunkerId == bunkerId)
                        {
                            try
                            {
                                var weight = smpe.steps[step].weigherLines[weirline].PortionWeight;
                                var name = m_bunkersNames[bunkerId];
                                ModWeight[matRename[name]] += (int) weight;
                            }
                            catch (Exception e)
                            {
                                var sb = new StringBuilder("SteelMaking trap:");
                                sb.AppendFormat(" step={0}", step);
                                sb.AppendFormat(" bunkerId={0}", bunkerId);
                                sb.AppendFormat(" weirline={0}", weirline);
                                l.err("exceptioninfo {0}\n\t{1}", sb.ToString(), e.ToString());
                            }
                        }
                    }
                }
            }
        }

        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("ModelRunner::Listener"))
            {
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("CastIronCorrection"))
                    {
                        int corr = Convert.ToInt32(fxe.Arguments["Correction"]);
                        if ((corr > 200) && (corr < 399))
                        {
                            l.msg("Iron Correction from HMI: {0}\n", corr);
                            IronWeight = corr*1000;
                            IronReason = "OPERATOR";
                            DynPrepare.FireIronEvent();
                        }
                    }
                    else if (fxe.Operation.StartsWith("Vis.Output.Preliminary.Additions"))
                    {
                        l.msg("Visual Preliminary Additions Event Appeared: {0}\n", fxe);
                    }
                    else if (fxe.Operation.StartsWith("Vis.Output.Bunker.Additions"))
                    {
                        var fxh = new FlexHelper(fxe);
                        var sb = new StringBuilder("Visual Bunker Additions Event Appeared:\n");
                        bool allZero = true;
                        foreach (var mat in fxe.Arguments)
                        {
                            if (mat.Key.StartsWith("Heat")) continue;
                            var ix = Convert.ToInt32(mat.Key);
                            var val = Convert.ToInt32(mat.Value);
                            allZero &= (val == 0);
                            
                            var s = Encoder(m_bunkersNames[-5 + ix]);
                            if (matRename.ContainsKey(s))
                            {
                                sb.AppendFormat("   {2}[{0}] = {1}\n",
                                    matRename[s], val, mat.Key);
                                VisWeight[matRename[s]] += val;
                                VisKey[matRename[s]] = mat.Key;
                            }
                            else
                            {
                                l.msg("matRename does not contains key <{0}>", s);
                            }
                        }
                        if (!allZero)
                        {
                            sb.Append("Signal to run Shixta II generated");
                            DynPrepare.recallChargingReq = true;
                        }
                        else
                        {
                            sb.Append("Event ignored as having no useful data");
                        }
                        l.msg(sb.ToString());
                    }
                    else if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_WGHIRON1"))
                    {
                        if ((string) fxe.Arguments["SHEATNO"] == Convert.ToString(HeatNumber))
                        {
                            IronWeight = Convert.ToDouble(fxe.Arguments["NWGH_NETTO"])*1000;
                            IronReason = "PIPE-W";
                            l.msg("Iron Correction from Pipe: {0}\n", IronWeight);
                            DynPrepare.FireIronEvent();
                        }
                        else
                            l.msg(
                                "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                                HeatNumber, fxe.Arguments["SHEATNO"]
                                );
                    }
                    else if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_XIMIRON"))
                    {
                        if ((string) fxe.Arguments["HEAT_NO"] == Convert.ToString(HeatNumber))
                        {
                            if (0 == (DynPrepare.HeatFlags & ModelStatus.ModelDisabled))
                            {
                                DynPrepare.fxeIron = new FlexHelper(fxe);
                                IronWeight = Convert.ToDouble(fxe.Arguments["HM_WEIGHT"]);
                                IronReason = "PIPE-X";
                                l.msg("Iron Chemistry from Pipe: {0}\n", IronWeight);
                                DynPrepare.FireIronEvent();
                                DynPrepare.HeatFlags |= ModelStatus.IronDefined;
                                if (0 != (DynPrepare.HeatFlags & ModelStatus.BlowingStarted))
                                {
                                    ///! UNTIL RECALC WORKS !!! DynPrepare.ironRecalcRequest = true;
                                    l.err("recalculation not works yet");
                                }
                            }
                            else
                            {
                                l.err("XIMIRON appeared but too late -- model disabled");
                            }
                        }
                        else
                            l.msg(
                                "Iron Chemistry from Pipe: wrong heat number - expected {0} found {1}",
                                HeatNumber, fxe.Arguments["HEAT_NO"]
                                );
                    }
                    else if (fxe.Operation.StartsWith("ConverterUI.TargetValues"))
                    {
                        lock (Listener.Lock)
                        {
                            DynPrepare.visTargetVal = new FlexHelper(fxe);
                            l.msg("Target Values From ConverterUI appeared: {0}", fxe);
                        }
                    }
                    else if (fxe.Operation.StartsWith("Model.Dynamic"))
                    {
                        l.msg("Model Related Event Appeared: {0}\n", fxe);
                    }
                }
                else if (evt is LanceEvent)
                {
                    var lae = evt as LanceEvent;
                    avox.Add(ForceBlow == -1 ? 0 : ForceBlow == 0 ? 100.0 : lae.O2Flow);
                    if (lae.O2Flow > 0.0)
                    {
                        l.msg("Oxygen Flow: {0}", lae.O2Flow);
                    }
                }
                else if (evt is HeatChangeEvent)
                {
                    var hce = evt as HeatChangeEvent;
                    if (HeatNumber == hce.HeatNumber) return;
                    l.msg("Heat Changed. New Heat ID: {0}", hce.HeatNumber);
                    Int64 rem;
                    Int64 res = Math.DivRem(hce.HeatNumber, 10000, out rem);
                    HeatNumber = res*100000 + rem;
                    IronWeight = 300011;
                    IronReason = "DEFAULT";
                    ScrapWeight = 113311;
                    ScrapReason = "DEFAULT";
                    ScrapDanger = 0.0;
                    avofg.Add(6000.1133);
                    avofg_pco.Add(0.1133);
                    avofg_pco2.Add(0.1133);
                    DynPrepare.fxeIron = null;
                    CurrWeight["LIME"] = 1;
                    CurrWeight["DOLOMS"] = 1;
                    CurrWeight["DOLMAX"] = 1;
                    CurrWeight["FOM"] = 1;
                    CurrWeight["COKE"] = 1;
                    DynPrepare.HeatFlags = 0;
                }
                else if (evt is ScrapEvent)
                {
                    var se = evt as ScrapEvent;
                    l.msg("Scrap Event: {0}", se);
                    if (se.ConverterNumber == Converter)
                    {
                        ScrapWeight = se.TotalWeight;
                        ScrapReason = "SCRAPEVENT";
                        ScrapDanger = 0.2;
                        ScrapDanger += VBProb(se.ScrapType1, se.Weight1);
                        ScrapDanger += VBProb(se.ScrapType2, se.Weight2);
                        ScrapDanger += VBProb(se.ScrapType3, se.Weight3);
                        ScrapDanger += VBProb(se.ScrapType4, se.Weight4);
                        ScrapDanger += VBProb(se.ScrapType5, se.Weight5);
                        ScrapDanger += VBProb(se.ScrapType6, se.Weight6);
                        ScrapDanger += VBProb(se.ScrapType7, se.Weight7);
                        ScrapDanger += VBProb(se.ScrapType8, se.Weight8);
                        ScrapDanger /= ScrapWeight;
                        DynPrepare.HeatFlags |= ModelStatus.ScrapDefined;
                    }
                }
                else if (evt is SteelMakingPatternEvent)
                {
                    var smpe = evt as SteelMakingPatternEvent;
                    l.msg("SteelMakingPattern Event: {0}", smpe);
                    CollectOxygen(smpe);
                    CollectAdditions(smpe, l);
                    var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Template.Additions");
                    fex.AddArg("Heat_No", HeatNumber);
                    fex.AddArg("Iron_Weight", IronWeight);
                    fex.AddArg("Iron_Reason", IronReason);
                    fex.AddArg("Scrap_Weight", ScrapWeight);
                    fex.AddArg("Scrap_Reason", ScrapReason);
                    fex.AddArg("LIME", (double) ModWeight["LIME"]);
                    fex.AddArg("DOLOMS", (double) ModWeight["DOLOMS"]);
                    fex.AddArg("DOLMAX", (double) ModWeight["DOLMAX"]);
                    fex.AddArg("FOM", (double) ModWeight["FOM"]);
                    fex.AddArg("COKE", (double) ModWeight["COKE"]);
                    //fex.Fire(DynPrepare.CoreGate);
                    DynPrepare.HeatFlags |= ModelStatus.AdditionsDefined;
                }
                else if (evt is BoundNameMaterialsEvent)
                {
                    var bnme = evt as BoundNameMaterialsEvent;
                    l.msg("BoundNameMaterials Event: {0}", bnme);

                    m_bunkersNames[0] = bnme.Bunker5MaterialName;
                    m_bunkersNames[1] = bnme.Bunker6MaterialName;
                    m_bunkersNames[2] = bnme.Bunker7MaterialName;
                    m_bunkersNames[3] = bnme.Bunker8MaterialName;
                    m_bunkersNames[4] = bnme.Bunker9MaterialName;
                    m_bunkersNames[5] = bnme.Bunker10MaterialName;
                    m_bunkersNames[6] = bnme.Bunker11MaterialName;
                    m_bunkersNames[7] = bnme.Bunker12MaterialName;
                }
                else if (evt is visAdditionTotalEvent)
                {
                    var vate = evt as visAdditionTotalEvent;
                    l.msg("{0}", vate);
                    m_bunkersTotalMass[0] = vate.RB5TotalWeight;
                    m_bunkersTotalMass[1] = vate.RB6TotalWeight;
                    m_bunkersTotalMass[2] = vate.RB7TotalWeight;
                    m_bunkersTotalMass[3] = vate.RB8TotalWeight;
                    m_bunkersTotalMass[4] = vate.RB9TotalWeight;
                    m_bunkersTotalMass[5] = vate.RB10TotalWeight;
                    m_bunkersTotalMass[6] = vate.RB11TotalWeight;
                    m_bunkersTotalMass[7] = vate.RB12TotalWeight;
                    MVBounder(l);
                }
                else if (evt is OffGasEvent)
                {
                    var wgtotal = evt as OffGasEvent;
                    //l.msg("{0}", wgtotal);
                    avofg.Add(wgtotal.OffGasFlow);
                }
                else if (evt is OffGasAnalysisEvent)
                {
                    var wgpercent = evt as OffGasAnalysisEvent;
                    //l.msg("{0}", wgpercent);
                    avofg_pco.Add(wgpercent.CO);
                    avofg_pco2.Add(wgpercent.CO2);
                }
                else if (evt is SublanceStartEvent)
                {
                    var zamer = evt as SublanceStartEvent;
                    l.msg("Received: {0}", zamer);
                    if (zamer.SublanceStartFlag == 0)
                    {
                        if (0 != (DynPrepare.HeatFlags & ModelStatus.ModelStarted))
                        {
                            DynPrepare.FireTemperatureEvent(DynPrepare.DynModel);
                            DynPrepare.FireXimstalEvent(DynPrepare.DynModel);
                        }
                    }
                }
                else if (evt is TappingEvent)
                {
                    var sliv = evt as TappingEvent;
                    l.msg("Received: {0}", sliv);
                    if (sliv.TappingFlag == 0)
                    {
                        if (0 != (DynPrepare.HeatFlags & ModelStatus.ModelStarted))
                        {
                            DynPrepare.FireXimslagEvent(DynPrepare.DynModel);
                        }
                    }
                }
                else if (evt is BlowingEvent)
                {
                    var blow = evt as BlowingEvent;
                    if (0 == (DynPrepare.HeatFlags & ModelStatus.BlowingStarted))
                    {
                        if (blow.BlowingFlag == 1)
                        {
                            DynPrepare.HeatFlags |= ModelStatus.BlowingStarted;
                            l.msg("Heat {0} : main blowing started", HeatNumber);
                        }
                    }
                }
                if (evt is SublanceTemperatureEvent)
                {
                    var fxe = new FlexHelper("Model.Dynamic.Output.RecommendBalanceBlow");
                    var ste = evt as SublanceTemperatureEvent;
                    const int maxT = 1770;
                    const int minT = 1550;
                    if ((ste.SublanceTemperature < maxT) && (ste.SublanceTemperature > minT))
                    {
                        fxe.AddInt("CurrentT", ste.SublanceTemperature);
                        fxe.AddInt("TargetT", MINP.HeatAimData.FinalTemperature);
                        if (fxe.GetInt("TargetT") > fxe.GetInt("CurrentT"))
                        {
                            var lTM = new MINP_TempMeasDTO();
                            lTM.Temperature = fxe.GetInt("CurrentT");
                            DynPrepare.DynModel.EnqueueTemperatureMeasured(lTM);
                            DynPrepare.DynModel.Resume();
                            System.Threading.Thread.Sleep(300);
                            fxe.AddDbl("CorrectionO2", lTM.CorrectionOxigen);
                        } 
                        else
                        {
                            DynPrepare.DynModel.Stop();
                            fxe.AddDbl("CorrectionO2", -3.0);
                        }
                        l.msg("SublanceTemperature = " + ste.SublanceTemperature);
                    }
                    else
                    {
                        DynPrepare.DynModel.Stop();
                        fxe.AddDbl("CorrectionO2", -5.0);
                    }
                    fxe.Fire(DynPrepare.CoreGate);
                    var tmpOx = fxe.GetDbl("CorrectionO2");
                    fxe.evt.Operation = "Model.Dynamic.Output.CorrectionO2";
                    fxe.ClearArgs();
                    fxe.AddDbl("CorrectionO2", tmpOx);
                    fxe.Fire(DynPrepare.CoreGate);
                }
            }
        }
    }
}