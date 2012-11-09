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
    public enum ModelRunReady
    {
        BlowingStarted = 1,
        ModelStarted = BlowingStarted << 1,
        IronDefined = ModelStarted << 1,
        ScrapDefined = IronDefined << 1,
        AdditionsDefined = ScrapDefined << 1,
    }

    internal class DynPrepare
    {
        //public static long oHeatNumber;
        public static DateTime cTime = DateTime.Now;
        public static ModelRunReady HeatFlags = 0;
        public static Dynamic DynModel;
        public static Client CoreGate;
        private const int _N_matElements = 74;
        private static List<string> StrList = new List<string>();
        public static DynamicInput aInputData = new DynamicInput();
        public static FlexEvent fxeIron = null;
        private static DTO.MINP_MatAddDTO matIron;
        public static long HeatNumber = -1;
        private static int EmptyBlowCount = 0;
        private static void OutLine(String str)
        {
            Console.WriteLine(str + Environment.NewLine);
            StrList.Add(str + Environment.NewLine);
        }
        public static DTO.MINP_MatAddDTO AddIron(int weight)
        {
            var matIron = new DTO.MINP_MatAddDTO();
            matIron.ShortCode = "01Metal";
            matIron.Amount_kg = weight;
            matIron.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matIron.MINP_GD_Material.ShortCode = matIron.ShortCode;
            matIron.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            if (fxeIron == null)
            {
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 1400.0));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 4.75));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", 0.55));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.27));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.062));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.017));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ti", 0.1));
            }
            else
            {
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, Convert.ToDouble(fxeIron.Arguments["HM_TEMP"])));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", Convert.ToDouble(fxeIron.Arguments["ANA_C"])));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", Convert.ToDouble(fxeIron.Arguments["ANA_SI"])));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", Convert.ToDouble(fxeIron.Arguments["ANA_MN"])));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", Convert.ToDouble(fxeIron.Arguments["ANA_P"])));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", Convert.ToDouble(fxeIron.Arguments["ANA_S"])));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ti", Convert.ToDouble(fxeIron.Arguments["ANA_TI"])));
            }
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 340.0));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cu", 0.01));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cr", 0.02));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mo", 0.007));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ni", 0.01));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sn", 0.005));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sb", 0.005));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Zn", 0.01));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("N", 10.0));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("H", 5.0));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 93.0));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 0.9921));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 100.0));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 99.0));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 7000.0));
            return matIron;
        }
        public static DTO.MINP_MatAddDTO AddScrap(int weight)
        {
            var matScrap = new DTO.MINP_MatAddDTO();
            matScrap.ShortCode = "02Scrap";
            matScrap.Amount_kg = weight;
            matScrap.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matScrap.MINP_GD_Material.ShortCode = matScrap.ShortCode;
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 380.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 0.01));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", 0.2));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.2));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.01));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.005));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al", 0.03));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cu", 0.01));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cr", 0.01));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mo", 0.005));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ni", 0.01));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("V", 0.005));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sn", 0.005));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sb", 0.005));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Zn", 0.1));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("O", 50.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("N", 50.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("H", 5.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 96.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 0.5));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 0.5));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("FeO", 2.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 0.9962));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Basiticy", 1.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 1200.0));
            return matScrap;
        }
        public static DTO.MINP_MatAddDTO AddCaO(int weight)
        {
            var matCaO = new DTO.MINP_MatAddDTO();
            matCaO.ShortCode = "04CaO";
            matCaO.Amount_kg = weight;
            matCaO.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matCaO.MINP_GD_Material.ShortCode = matCaO.ShortCode;
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.4));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 96.5));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 0.5));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MgO", 2.0));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
            return matCaO;
        }
        public static DTO.MINP_MatAddDTO AddDolom(int weight)
        {
            var matDolom = new DTO.MINP_MatAddDTO();
            matDolom.ShortCode = "04Dolom";
            matDolom.Amount_kg = weight;
            matDolom.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matDolom.MINP_GD_Material.ShortCode = matDolom.ShortCode;
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.4));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 58.47));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 4.43));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 1.0));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MgO", 35.25));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
            return matDolom;
        }
        public static DTO.MINP_MatAddDTO AddDolomS(int weight)
        {
            var matDolomS = new DTO.MINP_MatAddDTO();
            matDolomS.ShortCode = "04DolomS";
            matDolomS.Amount_kg = weight;
            matDolomS.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matDolomS.MINP_GD_Material.ShortCode = matDolomS.ShortCode;
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.4));
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 20.0));
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MgO", 30.0));
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
            return matDolomS;
        }
        public static DTO.MINP_MatAddDTO AddFom(int weight)
        {
            var matFom = new DTO.MINP_MatAddDTO();
            matFom.ShortCode = "04Fom";
            matFom.Amount_kg = weight;
            matFom.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matFom.MINP_GD_Material.ShortCode = matFom.ShortCode;
            matFom.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.4));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 8.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 3.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MnO", 0.5));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.03));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 6.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MgO", 77.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 0.9852));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
            return matFom;
        }
        public static DTO.MINP_MatAddDTO AddCoke(int weight)
        {
            var matCoke = new DTO.MINP_MatAddDTO();
            matCoke.ShortCode = "05koks";
            matCoke.Amount_kg = weight;
            matCoke.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matCoke.MINP_GD_Material.ShortCode = matCoke.ShortCode;
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 350.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.35));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 97.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 85.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
            return matCoke;
        }
        public static DTO.MINP_MatAddDTO AddMaterial(string name, int weight)
        {
            var matCoke = new DTO.MINP_MatAddDTO();
            matCoke.ShortCode = "05koks";
            matCoke.Amount_kg = weight;
            matCoke.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matCoke.MINP_GD_Material.ShortCode = matCoke.ShortCode;
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();

            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 350.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.35));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 97.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 85.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
            return matCoke;
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

        private static void Main(string[] args)
        {
            using (var l = new Logger("ModelRunner::Main"))
            {
                try
                {
                    var o = new TestEvent();
                    CoreGate = new Client(new Listener());
                    CoreGate.Subscribe();
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
                    while (/*Listener.avox.Average(10) == 0.0*/ 0 == (HeatFlags & ModelRunReady.BlowingStarted))
                    {
                        Listener.avox.Add(0.0);
                        Thread.Sleep(1000);
                        Console.Write(".");
                    }
                    HeatNumber = Listener.HeatNumber;
                    if (0 == (HeatFlags & ModelRunReady.IronDefined))
                    {
                        FireModelNoDataEvent("Нет данных по чугуну", "IRON");
                        //goto WAIT_END_OF_HEAT;
                    }
                    if (0 == (HeatFlags & ModelRunReady.ScrapDefined))
                    {
                        FireModelNoDataEvent("Нет данных по лому", "SCRAP");
                        //goto WAIT_END_OF_HEAT;
                    }
                    if (0 == (HeatFlags & ModelRunReady.AdditionsDefined))
                    {
                        FireModelNoDataEvent("Нет данных по сыпучим", "ADDMAT");
                        //goto WAIT_END_OF_HEAT;
                    }
                    HeatFlags |= ModelRunReady.ModelStarted;
                    if (Listener.ScrapDanger > 0.25)
                    {
                        var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Scrap.Danger");
                        fex.AddArg("Heat_No", Listener.HeatNumber);
                        fex.AddArg("Prob", Listener.ScrapDanger);
                        fex.AddArg("Descr", Listener.ScrapDanger > 0.75 ? "HIGH" : "MEDIUM");
                        fex.Fire(CoreGate);
                    }

                    # region Charging

                    aInputData.ChargedMaterials = new List<DTO.MINP_MatAddDTO>();

                    // Iron
                    matIron = AddIron((int) Listener.IronWeight);
                    aInputData.ChargedMaterials.Add(matIron);
                    //goto LABEL_START;

                    // Scrap
                    aInputData.ChargedMaterials.Add(AddScrap((int) Listener.ScrapWeight));

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

                    # endregion

                    #region Model materials

                    // Odprasky
                    var matOdpr = new DTO.MINP_MatAddDTO();
                    matOdpr.ShortCode = "21ODPRA";
                    matOdpr.Amount_kg = 3000;
                    matOdpr.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matOdpr.MINP_GD_Material.ShortCode = matOdpr.ShortCode;
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 430.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 0.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", 0.008));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.05));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.02));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.02));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cu", 0.01));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mo", 0.005));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ni", 0.01));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("V", 0.005));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sn", 0.005));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sb", 0.01));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Zn", 0.01));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("O", 50.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("N", 50.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("H", 5.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 84.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 0.3));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 3.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("FeO", 9.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 2.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Basiticy", 0.1));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 93.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 3000.0));
                    // REMOVED:aInputData.ChargedMaterials.Add(matOdpr);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Odprasky,
                                                    matOdpr.MINP_GD_Material);

                    // Slag
                    var matSlag = new DTO.MINP_MatAddDTO();
                    matSlag.ShortCode = "22strst";
                    matSlag.Amount_kg = 30000;
                    matSlag.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matSlag.MINP_GD_Material.ShortCode = matSlag.ShortCode;
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 1640));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.35));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 50.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 15.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("FeO", 20.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MnO", 3.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 2.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 10.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Basiticy", 3.33));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 1.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 1.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
                    // REMOVED:aInputData.ChargedMaterials.Add(matSlag);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Slag,
                                                    matSlag.MINP_GD_Material);
                    //goto LABEL_START;

                    // Steel
                    var matSteel = new DTO.MINP_MatAddDTO();
                    matSteel.ShortCode = "Final";
                    matSteel.Amount_kg = 420000;
                    matSteel.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matSteel.MINP_GD_Material.ShortCode = matSteel.ShortCode;
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 1500.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 380.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 0.03));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.03));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.01));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.02));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cr", 0.01));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("V", 0.01));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ti", 0.005));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Nb", 0.005));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ca", 0.0001));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mg", 0.0001));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("O", 750.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("N", 50.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("H", 5.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 100.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.002));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 100.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 99.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 7000.0));
                    // REMOVED:aInputData.ChargedMaterials.Add(matSteel);
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
                                                    SimulationOxygenBlowing();
                                                    FireFlexEvent(++nStep, null, DynModel);
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

        public static void FireFlexEvent(int nS, ConnectionProvider.FlexHelper f, Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.PerSecond");
            fex.AddArg("@RelativeSecond", nS);
            fex.AddArg("C", (double)mo.LastOutputData.FP_Kov[0]);
            fex.AddArg("T", (double)mo.LastOutputData.T_Tavby);
            fex.AddArg("Si", (double)mo.LastOutputData.FP_Kov[1]);
            fex.AddArg("Mn", (double)mo.LastOutputData.FP_Kov[2]);
            fex.AddArg("P", (double)mo.LastOutputData.FP_Kov[3]);
            fex.AddArg("Al", (double)mo.LastOutputData.FP_Kov[5]);
            fex.AddArg("Cr", (double)mo.LastOutputData.FP_Kov[7]);
            fex.AddArg("V", (double)mo.LastOutputData.FP_Kov[10]);
            fex.AddArg("Ti", (double)mo.LastOutputData.FP_Kov[11]);
            fex.AddArg("Fe", (double)mo.LastOutputData.FP_Kov[32]);
            fex.AddArg("FeO", (double)mo.LastOutputData.FP_Struska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddArg("CaO", (double)mo.LastOutputData.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddArg("SiO2", (double)mo.LastOutputData.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddArg("MnO", (double)mo.LastOutputData.FP_Struska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddArg("MgO", (double)mo.LastOutputData.FP_Struska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            double mCaO = mo.LastOutputData.m_SlozkaStruska[0];
            double mSiO2 = mo.LastOutputData.m_SlozkaStruska[1];
            if (mSiO2 > 0.0)
            {
                fex.AddArg("CaO/SiO2", mCaO / mSiO2);
            }
            fex.Fire(CoreGate);
        }

        public static void FireModelNoDataEvent(string Reason, string RCode)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.NoData");
            fex.AddArg("Heat_No", Listener.HeatNumber);
            fex.AddArg("Reason", Reason);
            fex.AddArg("RCode", RCode);
            fex.Fire(CoreGate);
        }

        public static void FireIronEvent()
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Iron");
            fex.AddArg("Heat_No", Listener.HeatNumber);
            fex.AddArg("Iron_Weight", Listener.IronWeight);
            fex.AddArg("Iron_Reason", Listener.IronReason);
            fex.Fire(CoreGate);
        }

        public static void FireTemperatureEvent(Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Temperature");
            fex.AddArg("Heat_No", Listener.HeatNumber);
            fex.AddArg("Final_T", (double)mo.LastOutputData.T_Tavby);
            fex.AddArg("Final_C", (double)mo.LastOutputData.FP_Kov[0]);
            fex.Fire(CoreGate);
        }

        public static void FireXimstalEvent(Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.XIMSTAL");
            fex.AddArg("Heat_No", Listener.HeatNumber);
            fex.AddArg("Final_C", (double)mo.LastOutputData.FP_Kov[0]);
            fex.AddArg("Final_Si", (double)mo.LastOutputData.FP_Kov[1]);
            fex.AddArg("Final_Mn", (double)mo.LastOutputData.FP_Kov[2]);
            fex.AddArg("Final_P", (double)mo.LastOutputData.FP_Kov[3]);
            fex.AddArg("Final_Al", (double)mo.LastOutputData.FP_Kov[5]);
            fex.AddArg("Final_Cr", (double)mo.LastOutputData.FP_Kov[7]);
            fex.AddArg("Final_V", (double)mo.LastOutputData.FP_Kov[10]);
            fex.AddArg("Final_Ti", (double)mo.LastOutputData.FP_Kov[11]);
            fex.AddArg("Final_Fe", (double)mo.LastOutputData.FP_Kov[32]);
            fex.Fire(CoreGate);
        }

        public static void FireXimslagEvent(Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.XIMSLAG");
            fex.AddArg("Heat_No", Listener.HeatNumber);
            fex.AddArg("Final_FeO", (double)mo.LastOutputData.FP_Struska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddArg("Final_CaO", (double)mo.LastOutputData.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddArg("Final_SiO2", (double)mo.LastOutputData.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddArg("Final_MnO", (double)mo.LastOutputData.FP_Struska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.AddArg("Final_MgO", (double)mo.LastOutputData.FP_Struska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
            fex.Fire(CoreGate);
        }

        public static void FireAdditionsEvent(Models.Dynamic mo)
        {
            var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.Additions");
            fex.AddArg("Heat_No", Listener.HeatNumber);
            fex.AddArg("LIME", (double)Listener.CurrWeight["LIME"]);
            fex.AddArg("DOLOMS", (double)Listener.CurrWeight["DOLOMS"]);
            fex.AddArg("DOLMAX", (double)Listener.CurrWeight["DOLMAX"]);
            fex.AddArg("FOM", (double)Listener.CurrWeight["FOM"]);
            fex.AddArg("COKE", (double)Listener.CurrWeight["COKE"]);
            fex.Fire(CoreGate);
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
    }
}