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
using Implements;
using System.Configuration;

namespace ModelRunner
{
    class Listener : IEventListener
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
        public static int Converter = 0;
        public static int ForceBlow = 0;

        private const int bunkersCount = 8;
        private static List<double> m_bunkersTotalMass = new List<double>();
        private static List<string> m_bunkersNames = new List<string>();
        public static List<MINP_MatAddDTO> MatAdd = new List<MINP_MatAddDTO>();
        public static Dictionary<string, int> CurrWeight = new Dictionary<string, int>(); 
        public Listener()
        {
            Converter = Convert.ToInt32(ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["Converter"].Value);
            ForceBlow = Convert.ToInt32(ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["ForceBlow"].Value);
            for (var i = 0; i < bunkersCount; i++)
            {
                m_bunkersTotalMass.Add(0.0);
                m_bunkersNames.Add("");
            }
            CurrWeight.Add("LIME", 1);
            CurrWeight.Add("DOLOMS", 1);
            CurrWeight.Add("DOLMAX", 1);
            CurrWeight.Add("FOM", 1);
            CurrWeight.Add("COKE", 1);
        }
        public static string Encoder(string str)
        {
            char[] charArray = str.ToCharArray();
            str = "";
            foreach (char c in charArray)
            {
                if (c > 127)
                {
                    str += (char)(c + 848);
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
                        if (CurrWeight["LIME"] < (int)m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddCaO((int)m_bunkersTotalMass[i] - CurrWeight["LIME"]));
                            CurrWeight["LIME"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material added LIME: {0}", (int)m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "ДОЛОМС")
                    {
                        if (CurrWeight["DOLOMS"] < (int)m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddDolomS((int)m_bunkersTotalMass[i] - CurrWeight["DOLOMS"]));
                            CurrWeight["DOLOMS"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material added DOLOMS: {0}", (int)m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "ДОЛМИТ")
                    {
                        if (CurrWeight["DOLMAX"] < (int)m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddDolom((int)m_bunkersTotalMass[i] - CurrWeight["DOLMAX"]));
                            CurrWeight["DOLMAX"] = (int)m_bunkersTotalMass[i];
                            l.msg("Material added DOLMIT: {0}", (int)m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "МАХГ  ")
                    {
                        if (CurrWeight["DOLMAX"] < (int)m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddDolom((int)m_bunkersTotalMass[i] - CurrWeight["DOLMAX"]));
                            CurrWeight["DOLMAX"] = (int)m_bunkersTotalMass[i];
                            l.msg("Material added MAXG: {0}", (int)m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "ФОМ   ")
                    {
                        if (CurrWeight["FOM"] < (int)m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddFom((int)m_bunkersTotalMass[i] - CurrWeight["FOM"]));
                            CurrWeight["FOM"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material Added FOM: {0}", (int)m_bunkersTotalMass[i]);
                        }
                    }
                    else if (Encoder(m_bunkersNames[i]) == "KOKS  ")
                    {
                        if (CurrWeight["COKE"] < (int)m_bunkersTotalMass[i])
                        {
                            MatAdd.Add(DynPrepare.AddCoke((int)m_bunkersTotalMass[i] - CurrWeight["COKE"]));
                            CurrWeight["COKE"] = (int) m_bunkersTotalMass[i];
                            l.msg("Material added COKE: {0}", (int)m_bunkersTotalMass[i]);
                        }
                    }

                }
                //else throw new Exception("Either bunker names or values are empty");
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
                            IronWeight = corr * 1000;
                            IronReason = "OPERATOR";
                            DynPrepare.FireIronEvent();
                        }
                    }
                    else if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_WGHIRON1"))
                    {
                        if ((string)fxe.Arguments["SHEATNO"] == Convert.ToString(HeatNumber))
                        {
                            IronWeight = Convert.ToDouble(fxe.Arguments["NWGH_NETTO"]) * 1000;
                            IronReason = "PIPE-W";
                            l.msg("Iron Correction from Pipe: {0}\n", IronWeight);
                            DynPrepare.FireIronEvent();
                        }
                        else l.msg(
                            "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                            HeatNumber, fxe.Arguments["SHEATNO"]
                            );
                    }
                    else if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_XIMIRON"))
                    {
                        if ((string)fxe.Arguments["HEAT_NO"] == Convert.ToString(HeatNumber))
                        {
                            DynPrepare.fxeIron = fxe;
                            IronWeight = Convert.ToDouble(fxe.Arguments["HM_WEIGHT"]);
                            IronReason = "PIPE-X";
                            l.msg("Iron Chemistry from Pipe: {0}\n", IronWeight);
                            DynPrepare.FireIronEvent();
                        }
                        else l.msg(
                            "Iron Chemistry from Pipe: wrong heat number - expected {0} found {1}",
                            HeatNumber, fxe.Arguments["HEAT_NO"]
                            );
                    }
                    else
                    {
                        l.msg("FlexEvent Appeared: {0}\n", fxe);
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
                    HeatNumber = res * 100000 + rem;
                    IronWeight = 300011;
                    IronReason = "DEFAULT";
                    ScrapWeight = 113311;
                    ScrapReason = "DEFAULT";
                    avofg.Add(6000.1133);
                    avofg_pco.Add(0.1133);
                    avofg_pco2.Add(0.1133);
                    DynPrepare.fxeIron = null;
                    CurrWeight["LIME"] = 1;
                    CurrWeight["DOLOMS"] = 1;
                    CurrWeight["DOLMAX"] = 1;
                    CurrWeight["FOM"] = 1;
                    CurrWeight["COKE"] = 1;
                }
                else if (evt is ScrapEvent)
                {
                    var se = evt as ScrapEvent;
                    l.msg("Scrap Event: {0}", se);
                    if (se.ConverterNumber == Converter)
                    {
                        ScrapWeight = se.TotalWeight;
                        ScrapReason = "SCRAPEVENT";
                    }
                }
                else if (evt is SteelMakingPatternEvent)
                {
                    var smpe = evt as SteelMakingPatternEvent;
                    l.msg("SteelMakingPattern Event: {0}", smpe);
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
            }
        }
    }
}
