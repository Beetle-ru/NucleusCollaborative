using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Esms;

namespace EsmsFusionProtocol
{
    internal class HeatProtocol
    {
        public HeatProtocol()
        {
            ArCOSHistory = new List<ArCOSEvent>();
            Burner1History = new List<Burner1Event>();
            Burner2History = new List<Burner2Event>();
            Burner3History = new List<Burner3Event>();
            Burner4History = new List<Burner4Event>();
            ChemanalFusionHistory = new List<ChemanalFusionEvent>();
            CoalInjectionHistory = new List<CoalInjectionEvent>();
            DrivesBunkersHistory = new List<DrivesBunkersEvent>();
            EnergyHistory = new List<EnergyEvent>();
            FingersHistory = new List<FingersEvent>();
            FurnaceHistory = new List<FurnaceEvent>();
            FurnaceSwitch1History = new List<FurnaceSwitch1Event>();
            FurnaceSwitch2History = new List<FurnaceSwitch2Event>();
            FurnaceSwitchCommonHistory = new List<FurnaceSwitchCommonEvent>();
            GasWasteHistory = new List<GasWasteEvent>();
            HeatPassportHistory = new List<HeatPassportEvent>();
            Injector1History = new List<Injector1Event>();
            Injector2History = new List<Injector2Event>();
            Injector3History = new List<Injector3Event>();
            Injector4History = new List<Injector4Event>();
            LanceCrestHistory = new List<LanceCrestEvent>();
            LevelBunkerHistory = new List<LevelBunkerEvent>();
            MaterialNamesHistory = new List<MaterialNamesEvent>();
            MaterialsBucketHistory = new List<MaterialsBucketEvent>();
            MaterialsFurnaceHistory = new List<MaterialsFurnaceEvent>();
            ReactorTransformerHistory = new List<ReactorTransformerEvent>();
            SchieberHistory = new List<SchieberEvent>();
            ScrapLoadHistory = new List<ScrapLoadEvent>();
            SteelOutletHistory = new List<SteelOutletEvent>();
            SubmissionHistory = new List<SubmissionEvent>();
            TempHearthHistory = new List<TempHearthEvent>();
            WaterCoolingFlueHistory = new List<WaterCoolingFlueEvent>();
            WaterCoolingMineHistory = new List<WaterCoolingMineEvent>();
            WaterCoolingPanelHistory = new List<WaterCoolingPanelEvent>();
            WeighBunkersHistory = new List<WeighBunkersEvent>();
            WorkWindowHistory = new List<WorkWindowEvent>();
        }

        public List<ArCOSEvent> ArCOSHistory { get; set; }
        public List<Burner1Event> Burner1History { get; set; }
        public List<Burner2Event> Burner2History { get; set; }
        public List<Burner3Event> Burner3History { get; set; }
        public List<Burner4Event> Burner4History { get; set; }
        public List<CeloxEvent> CeloxHistory { get; set; }
        public List<ChemanalFusionEvent> ChemanalFusionHistory { get; set; }
        public List<CoalInjectionEvent> CoalInjectionHistory { get; set; }
        public List<DrivesBunkersEvent> DrivesBunkersHistory { get; set; }
        public List<EnergyEvent> EnergyHistory { get; set; }
        public List<FingersEvent> FingersHistory { get; set; }
        public List<FurnaceEvent> FurnaceHistory { get; set; }
        public List<FurnaceSwitch1Event> FurnaceSwitch1History { get; set; }
        public List<FurnaceSwitch2Event> FurnaceSwitch2History { get; set; }
        public List<FurnaceSwitchCommonEvent> FurnaceSwitchCommonHistory { get; set; }
        public List<GasWasteEvent> GasWasteHistory { get; set; }
        public List<HeatPassportEvent> HeatPassportHistory { get; set; }
        public List<Injector1Event> Injector1History { get; set; }
        public List<Injector2Event> Injector2History { get; set; }
        public List<Injector3Event> Injector3History { get; set; }
        public List<Injector4Event> Injector4History { get; set; }
        public List<LanceCrestEvent> LanceCrestHistory { get; set; }
        public List<LevelBunkerEvent> LevelBunkerHistory { get; set; }
        public List<MaterialNamesEvent> MaterialNamesHistory { get; set; }
        public List<MaterialsBucketEvent> MaterialsBucketHistory { get; set; }
        public List<MaterialsFurnaceEvent> MaterialsFurnaceHistory { get; set; }
        public List<ReactorTransformerEvent> ReactorTransformerHistory { get; set; }
        public List<SchieberEvent> SchieberHistory { get; set; }
        public List<ScrapLoadEvent> ScrapLoadHistory { get; set; }
        public List<SteelOutletEvent> SteelOutletHistory { get; set; }
        public List<SubmissionEvent> SubmissionHistory { get; set; }
        public List<TempHearthEvent> TempHearthHistory { get; set; }
        public List<WaterCoolingFlueEvent> WaterCoolingFlueHistory { get; set; }
        public List<WaterCoolingMineEvent> WaterCoolingMineHistory { get; set; }
        public List<WaterCoolingPanelEvent> WaterCoolingPanelHistory { get; set; }
        public List<WeighBunkersEvent> WeighBunkersHistory { get; set; }
        public List<WorkWindowEvent> WorkWindowHistory { get; set; }
    }
}
