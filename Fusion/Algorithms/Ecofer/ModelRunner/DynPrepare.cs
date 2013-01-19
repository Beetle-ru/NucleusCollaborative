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
    [Flags]
    public enum ModelStatus
    {
        BlowingStarted = 1,
        ModelDisabled = BlowingStarted << 1,
        ModelStarted = ModelDisabled << 1,
        IronDefined = ModelStarted << 1,
        ScrapDefined = IronDefined << 1,
        AdditionsDefined = ScrapDefined << 1,
    }

    partial class DynPrepare
    {
        public enum ChargingReason
        {
            forCharging,
            forRecalculation
        }
        //public static long oHeatNumber;
        public static DateTime cTime = DateTime.Now;
        public static ModelStatus HeatFlags = 0;
        public static Dynamic DynModel;
        public static Client CoreGate;
        private const int _N_matElements = 74;
        private static List<string> StrList = new List<string>();
        public static DynamicInput aInputData = new DynamicInput();
        public static FlexHelper fxeIron = null;
        private static DTO.MINP_MatAddDTO matIron;
        public static long HeatNumber = -1;
        private static int EmptyBlowCount = 0;
        public static FlexHelper visTargetVal = null;
        public static bool recallChargingReq = true;
        private static void OutLine(String str)
        {
            Console.WriteLine(str + Environment.NewLine);
            StrList.Add(str + Environment.NewLine);
        }
        private static HeatCharge.FPCarrier fp = new HeatCharge.FPCarrier();
        private static MINP_GD_MaterialElementDTO[] mael = new MINP_GD_MaterialElementDTO[_N_matElements];

        private static MINP_GD_MaterialItemsDTO ps(string key, double val)
        {
            var vx = new MINP_GD_MaterialItemsDTO();
            vx.Amount_p = val;
            vx.MINP_GD_MaterialElement = mael[fp.name2ix(key)];
            return vx;
        }

        private static MINP_GD_MaterialItemsDTO ps(int ix, double val)
        {
            var vx = new MINP_GD_MaterialItemsDTO();
            vx.Amount_p = val;
            vx.MINP_GD_MaterialElement = mael[ix];
            return vx;
        }

        private static void MakeDynamicCharging()
        {
            aInputData.ChargedMaterials = new List<DTO.MINP_MatAddDTO>();

            // Iron
            matIron = AddIron((int)Listener.IronWeight);
            aInputData.ChargedMaterials.Add(matIron);
            //goto LABEL_START;

            // Scrap
            aInputData.ChargedMaterials.Add(AddScrap((int)Listener.ScrapWeight));

            MINP.MINP_GD_ModelMaterials =
                new Dictionary<Common.Enumerations.MINP_GD_Material_ModelMaterial, DTO.MINP_GD_MaterialDTO>();
            // CaO ИЗВЕСТ
            var matCaO = AddCaO(1);
            aInputData.ChargedMaterials.Add(matCaO);
            MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.CaO,
                                            matCaO.MINP_GD_Material);

            // Dolom ДОЛМИТ
            var matDolom = AddDolom(1);
            aInputData.ChargedMaterials.Add(matDolom);
            MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Dolomite,
                                            matDolom.MINP_GD_Material);

            // FOM
            var matFom = AddFom(1);
            aInputData.ChargedMaterials.Add(matFom);
            MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.FOM,
                                            matFom.MINP_GD_Material);

            // Coke
            var matCoke = AddCoke(1);
            aInputData.ChargedMaterials.Add(matCoke);
            MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Coke,
                                            matCoke.MINP_GD_Material);

            
        }

        private static void Main(string[] args)
        {
            //AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            //                                                  {
            //                                                      InstantLogger.err("Unhandled exception {0} is terminating {1}", e.ExceptionObject, e.IsTerminating);
            //                                                  };
            using (var l = new Logger("ModelRunner::Main"))
            {
                try
                {
                    
                    var o = new TestEvent();
                    CoreGate = new Client(new Listener());
                    CoreGate.Subscribe();
                    ConnectionProvider.Client.protectedMode = false; 
                    Thread.Sleep(1000);
                    // текущий номер плавки
                    CoreGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(HeatChangeEvent).Name });
                    Thread.Sleep(1000);
                    // запрашиваем привязку бункеров к материалам
                    CoreGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(BoundNameMaterialsEvent).Name });
                    // навески
                    CoreGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(visAdditionTotalEvent).Name });
                    int nStep = 0;

                    for (var i = 0; i < _N_matElements; i++)
                    {
                        mael[i] = new MINP_GD_MaterialElementDTO();
                        mael[i].Vector = (int) (1.0/fp.fp[i]._rcv);
                        mael[i].Mm = fp.fp[i].Mm;
                        mael[i].O2 = fp.fp[i].O2Stoichio;
                        mael[i].E_Ox1 = fp.fp[i].E_ox1;
                        mael[i].E_Ox2 = fp.fp[i].E_ox2;
                        mael[i].Eta_Ox1 = fp.fp[i].Eta_ox1;
                        mael[i].Eta_Ox2 = fp.fp[i].Eta_ox2;
                        mael[i].Index = i;
                    }
                    MINP.MINP_GD_MaterialElements = new Dictionary<int, DTO.MINP_GD_MaterialElementDTO>();
                    for (var i = 0; i < _N_matElements; i++)
                    {
                        MINP.MINP_GD_MaterialElements.Add(i, mael[i]);
                    }
                    HeatNumber = Listener.HeatNumber;
NEXT_HEAT:
                    DynPrepare.aInputData.OxygenBlowingPhases = new List<PhaseItem>();
                    var ph1 = new PhaseItemL1Command();
                    var ph2 = new PhaseItemOxygenBlowing();
                    ph1.PhaseName = "Initial";
                    ph1.L1Command = Enumerations.L2L1_Command.OxygenBlowingStart;
                    ph1.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    DynPrepare.aInputData.OxygenBlowingPhases.Add(ph1);
                    ph2 = new PhaseItemOxygenBlowing();
                    ph2.PhaseName = "OxyBlowStep0";
                    ph2.LanceDistance_mm = 300;
                    ph2.O2Amount_Nm3 = 15000;
                    ph2.O2Flow_Nm3_min = 1000;
                    ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    DynPrepare.aInputData.OxygenBlowingPhases.Add(ph2);
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
                    Listener.avox.Add(0.0);
                    while (/*Listener.avox.Average(10) == 0.0*/ 0 == (HeatFlags & ModelStatus.BlowingStarted))
                    {
                        Listener.avox.Add(0.0);
                        if (recallChargingReq && visTargetVal != null)
                        {
                            lock (Listener.Lock)
                            {
                                recallChargingReq = false;
                                Listener.shixtaII = new Charging(MakeCharging(visTargetVal.evt, Listener.VisWeight, 
                                    ChargingReason.forCharging));
                                FireShixtaDoneEvent(Listener.shixtaII.Run());
                                for (var iw = 0; iw < Listener.VisWeight.Count; iw++)
                                {
                                    Listener.VisWeight[Listener.VisWeight.ElementAt(iw).Key] = 0;
                                }
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Write(".");
                    }
                    HeatNumber = Listener.HeatNumber;
                    if (0 == (HeatFlags & ModelStatus.IronDefined))
                    {
                        FireModelNoDataEvent("Нет данных по чугуну", "IRON");
                        // HeatFlags |= ModelStatus.ModelDisabled;
                    }
                    if (0 == (HeatFlags & ModelStatus.ScrapDefined))
                    {
                        FireModelNoDataEvent("Нет данных по лому", "SCRAP");
                        // HeatFlags |= ModelStatus.ModelDisabled;
                    }
                    if (0 == (HeatFlags & ModelStatus.AdditionsDefined))
                    {
                        FireModelNoDataEvent("Нет данных по сыпучим", "ADDMAT");
                        // HeatFlags |= ModelStatus.ModelDisabled;
                    }
                    HeatFlags |= ModelStatus.ModelStarted;
                    if (Listener.ScrapDanger > 0.25)
                    {
                        var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Scrap.Danger");
                        fex.AddInt("Heat_No", Listener.HeatNumber);
                        fex.AddDbl("Prob", Listener.ScrapDanger);
                        fex.AddStr("Descr", Listener.ScrapDanger > 0.75 ? "HIGH" : "MEDIUM");
                        fex.Fire(CoreGate);
                    }

                    MakeDynamicCharging();

                    #region Model materials

                    // Odprasky
                    var matOdpr = AddOdprasky(3000);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Odprasky,
                                                    matOdpr.MINP_GD_Material);

                    // Slag
                    var matSlag = AddSlag(30000);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Slag,
                                                    matSlag.MINP_GD_Material);
                    // Steel
                    var matSteel = AddSteel(420000);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Steel,
                                                    matSteel.MINP_GD_Material);

                    #endregion

                    for (int i = 0; i < matIron.MINP_GD_Material.MINP_GD_MaterialItems.Count; i++)
                    {
                        if (matIron.MINP_GD_Material.MINP_GD_MaterialItems[i].MINP_GD_MaterialElement.Index == 69)
                        {
                            aInputData.HotMetal_Temperature = (int)matIron.MINP_GD_Material.MINP_GD_MaterialItems[i].Amount_p;
                            break;
                        }
                    }
                    aInputData.Scrap_Temperature = 0;
                    if (0 != (HeatFlags & ModelStatus.ModelDisabled))
                    {
                        goto WAIT_END_OF_HEAT;
                    }

                    #region Define Oxygen Blowing Phases

                    // see listener::onEvent SteelMakingPattern

                    #endregion

                    MINP.HeatAimData = new MINP_HeatAimDataDTO();
                    MINP.HeatAimData.FinalTemperature = 1700;

                    Data.MINP.Phases = new Phases(aInputData.OxygenBlowingPhases);
                    Data.MINP.Phases.SwitchToNextPhase();

                    DynModel = new Dynamic(aInputData, 1, Dynamic.RunningType.RealTime);

                    foreach (var m in Listener.MatAdd)
                    {
                        DynModel.EnqueueMaterialAdded(m);
                    }
                    Listener.MatAdd.Clear();
                    SimulationOxygenBlowing();

                    DynModel.PhaseChanged += (s, e) =>
                                               {
                                                   l.msg("Phase Duration: {0}, Next phase is {1}",
                                                                     Data.Clock.Current.Duration,
                                                                     e.CurrentPhase.PhaseName
                                                       );

                                                   if (e.CurrentPhase is PhaseItemL1Command &&
                                                       ((PhaseItemL1Command) e.CurrentPhase).L1Command ==
                                                       Enumerations.L2L1_Command.OxygenLanceToParkingPosition)
                                                   {
                                                       FireAdditionsEvent(DynModel);
                                                       DynModel.Stop();
                                                       l.msg("Model finished. HEATNO={0}", HeatNumber);
                                                   }
                                                   else if (e.CurrentPhase is PhaseItemL1Command &&
                                                       ((PhaseItemL1Command) e.CurrentPhase).L1Command ==
                                                       Enumerations.L2L1_Command.TemperatureMeasurement)
                                                   {
                                                       var lTM = new MINP_TempMeasDTO();
                                                       lTM.Temperature = (int)DynModel.LastOutputData.T_Tavby;
                                                       DynModel.EnqueueTemperatureMeasured(lTM);
                                                       l.msg("Waiting for temperature measurement: {0}", lTM.Temperature);
                                                   }
                                               };
                    DynModel.ModelLoopDone += (s, e) =>
                                                {
                                                    if (DynModel.mRunningType == Dynamic.RunningType.RealTime)
                                                    {
                                                        SimulationOxygenBlowing();
                                                        //SimulationOxygenBlowing();
                                                        FirePerSecEvent(++nStep, null, DynModel);
                                                        //Thread.Sleep(1000);
                                                        if (Dynamic.ModelPhaseState.S10_MainOxygenBlowing == DynModel.State())
                                                        {
                                                            foreach (var m in Listener.MatAdd)
                                                            {
                                                                DynModel.EnqueueMaterialAdded(m);
                                                            }
                                                            Listener.MatAdd.Clear();
                                                        }
                                                        else if (Dynamic.ModelPhaseState.S30_Correction == DynModel.State())
                                                        {
                                                            if (Listener.avox.Average(10) < 10.0)
                                                            {
                                                                if (++EmptyBlowCount > 5) DynModel.SwitchPhaseToL1OxygenLanceParking();
                                                            }
                                                            else EmptyBlowCount = 0;
                                                        }
                                                        else
                                                        {
                                                            l.err("TROUBLE!!! Model is in an unexpected state {0}",
                                                                  DynModel.State());
                                                        }
                                                    }
                                                };
                    DynModel.Start();
                    do
                    {
                        Thread.Sleep(1000);
                    } while (DynModel.State() < Dynamic.ModelPhaseState.S50_Finished);
                    Console.WriteLine();
WAIT_END_OF_HEAT:
                    do
                    {
                        Thread.Sleep(1000);
                        Console.Write("+");
                    } while (HeatNumber == Listener.HeatNumber);
                    if (cTime.Day != DateTime.Now.Day)
                    {
                        cTime = DateTime.Now;
                        InstantLogger.LogFileInit();
                    }
                    l.msg("Heat Number old: {0}, new: {1}", HeatNumber, Listener.HeatNumber);
                    HeatNumber = Listener.HeatNumber;
                    nStep = 0;

                    goto NEXT_HEAT;
 
                    DynModel.Dispose();
                    throw new Exception("************** End Of Heat ***************");
                }
                catch (Exception e)
                {
                    OutLine("ModelRunner.Main exception " + e.ToString());
                }
                System.IO.File.WriteAllLines("ModelRunner.txt", StrList.ToArray());
            }
        }

        public static void SimulationOxygenBlowing()
        {
            TimeSpan lTotalDuration = Data.Clock.Current.ActualTime - Data.Clock.Current.StartTime;
            int lO2Consumption = 0;
            int lO2Flow = 0;

            if (Data.MINP.Phases.CurrentPhase == null)
            {
                // generates zero data
                DTO.MINP_CyclicDTO lCyclicDTO = new DTO.MINP_CyclicDTO()
                                                    {
                                                        MINP_HeatID = Data.MINP.Heat.ID,
                                                        TimeProcessed = Data.Clock.Current.ActualTime,
                                                        OxygenConsumption_m3 = 0,
                                                        OxygenFlow_Nm3_min = 0,
                                                        WastegasFlow_Nm3_min = 0
                                                    };
                Data.MINP.MINP_Cyclic.Add(lCyclicDTO);
            }
            else
            {
                IEnumerable<Data.PhaseItemOxygenBlowing> lOxygenBlowingPhases =
                    Data.MINP.Phases.Items
                        .Where(aR => aR is Data.PhaseItemOxygenBlowing)
                        .Cast<Data.PhaseItemOxygenBlowing>()
                        .Where(aR => aR.O2Amount_Nm3.HasValue)
                        .OrderBy(aR => aR.O2Amount_Nm3.Value);

                foreach (var nItem in lOxygenBlowingPhases)
                {
                    if (nItem.SimulationDuration < lTotalDuration)
                    {
                        // whole phase
                        lO2Consumption = nItem.O2Amount_Nm3.Value;
                        lO2Flow = nItem.O2Flow_Nm3_min;
                        lTotalDuration -= nItem.SimulationDuration;
                    }
                    else
                    {
                        // part of the phase
                        lO2Consumption += (int) Math.Round(nItem.O2Flow_Nm3_min*lTotalDuration.TotalMinutes);
                        lO2Flow = nItem.O2Flow_Nm3_min;
                        lTotalDuration = TimeSpan.Zero;
                        break;
                    }
                }

                // behind aimed O2 amount
                if (lTotalDuration > TimeSpan.Zero)
                {
                    lO2Consumption += (int) Math.Round(lO2Flow*lTotalDuration.TotalMinutes);
                }
                const int _3_ = 3;

                DTO.MINP_CyclicDTO lCyclicDTO = new DTO.MINP_CyclicDTO();
                lCyclicDTO.MINP_HeatID = Data.MINP.Heat.ID;
                lCyclicDTO.TimeProcessed = Data.Clock.Current.ActualTime;
                lCyclicDTO.OxygenConsumption_m3 = lO2Consumption;
                lCyclicDTO.OxygenFlow_Nm3_min = (int) Listener.avox.Average(_3_);
                var d = Math.Round(Listener.avofg.Average(_3_) * 0.0166666666666667);
                lCyclicDTO.WastegasFlow_Nm3_min = (int) d;
                lCyclicDTO.Wastegas_CO2_p = (int) Listener.avofg_pco2.Average(_3_);
                lCyclicDTO.Wastegas_CO_p = (int) Listener.avofg_pco.Average(_3_);
                Data.MINP.MINP_Cyclic.Add(lCyclicDTO);
                //if (DynModel.mRunningType == Dynamic.RunningType.RealTime)
                //{
                //    Data.MINP.MINP_Cyclic.Add(lCyclicDTO);
                //}
                Data.MINP.O2Request.Add(new Data.Graph.O2RequestItem()
                                            {
                                                TimeProcessed = Data.Clock.Current.ActualTime,
                                                O2Request =
                                                    (Data.MINP.Heat.CalculatedOxygenAmount_Nm3.HasValue &&
                                                     Data.MINP.Heat.CalculatedOxygenAmount_Nm3.Value != 0)
                                                        ? (float) lO2Consumption/
                                                          Data.MINP.Heat.CalculatedOxygenAmount_Nm3.Value
                                                        : 0
                                            });
            }
        }
        private char m_separator = ':';
        public void LoadCSVData(DTO.MINP_MatAddDTO Material, string Name, string Dir = "data")
        {
            using (var l = new Logger("ModelRunner::LoadCSVData"))
            {
                string filePath = String.Format("{0}\\{1}.csv", Dir, Name);
                string[] strings;
                try
                {
                    strings = File.ReadAllLines(filePath);
                }
                catch (Exception e)
                {
                    strings = new string[0];
                    l.err("Cannot read the file: {0}, call: {1}", filePath, e.ToString());
                    return;
                }
                try
                {
                    for (int strCnt = 0; strCnt < strings.Count(); strCnt++)
                    {
                        string[] values = strings[strCnt].Split(m_separator);
                        Material.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(values[0], Convert.ToDouble(values[1])));
                    }
                }
                catch (Exception e)
                {
                    l.err("Cannot read the file: {0}, bad format call exeption: {1}", filePath, e.ToString());
                    throw e;
                }

            }
        }

        public static ChargingInput MakeCharging(FlexEvent fxe, Dictionary<string, int> weight, ChargingReason reason)
        {
            Data.Model.ChargingInput inp = new ChargingInput();
            inp.Basicity = 2.7f; ///! (float)Convert.ToDouble(fxe.Arguments["CaOSio2"]);
            inp.FeO_p = Convert.ToInt32(fxe.Arguments["FeO"]);
            inp.MgO_p = Convert.ToInt32(fxe.Arguments["MgO"]);
            inp.Final_Temperature = Convert.ToInt32(fxe.Arguments["T"]);
            inp.Scrap_Temperature = 0; ///!AR: Correction

            ///! IRON+SCRAP
            inp.HotMetals = new MINP_GD_MaterialDTO[1];
            var matIron = DynPrepare.AddIron((int) Listener.IronWeight);
            inp.HotMetals[0] = matIron.MINP_GD_Material;
            inp.HotMetals_t[0] = (int) (matIron.Amount_kg*0.001);

            inp.Scraps = new MINP_GD_MaterialDTO[1];
            var matScrap = DynPrepare.AddScrap((int) Listener.ScrapWeight);
            inp.Scraps[0] = matScrap.MINP_GD_Material;
            inp.Scraps_t[0] = (int) (matScrap.Amount_kg*0.001);

            inp.Odprasky = DynPrepare.AddOdprasky(0).MINP_GD_Material;
            inp.StrStr = DynPrepare.AddSlag(0).MINP_GD_Material;
            inp.Steel = DynPrepare.AddSteel(0).MINP_GD_Material;

            var matCoke = DynPrepare.AddCoke(weight["COKE"]);
            inp.Coke = matCoke.MINP_GD_Material;
            inp.Coke_kg = matCoke.Amount_kg;

            var matDolomite = DynPrepare.AddDolom(weight["DOLMAX"]);
            inp.Dolomite = matDolomite.MINP_GD_Material;
            inp.Dolomite_kg = matDolomite.Amount_kg;

            var matFOM = DynPrepare.AddFom(weight["FOM"]);
            inp.FOM = matFOM.MINP_GD_Material;
            inp.FOM_kg = matFOM.Amount_kg;

            var matLime = DynPrepare.AddCaO(weight["LIME"]);
            inp.Lime = matLime.MINP_GD_Material;
            inp.Lime_kg = matLime.Amount_kg;

            Data.MINP.MINP_MatAdds.RemoveAll(aR => aR.MINP_GD_Material.ShortCode.StartsWith("01"));
            Data.MINP.MINP_MatAdds.RemoveAll(aR => aR.MINP_GD_Material.ShortCode.StartsWith("02"));
            Data.MINP.MINP_MatAdds.Insert(0, matIron);
            Data.MINP.MINP_MatAdds.Insert(1, matScrap);
            return inp;
        }
    }
}