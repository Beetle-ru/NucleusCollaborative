using System;
using System.Collections.Generic;


namespace Converter
{
    [Serializable]
    public class Heat: HeatAttributes
    {
        public Heat()
        {
            #region HistoryData init
            BlowingHistory = new List<BlowingEvent>();
            BlowingInterruptHistory = new List<BlowingInterruptEvent>();
            ConverterAngleHistory = new List<ConverterAngleEvent>();
            HeatChangeEvent = new List<HeatChangeEvent>();
            HeatingScrapHistory = new List<HeatingScrapEvent>();
            HotMetalLadleHistory = new List<HotMetalLadleEvent>();
            IgnitionHistory = new List<IgnitionEvent>();
            LanceHistory = new List<LanceEvent>();
            MixerAnalysisHistory = new List<MixerAnalysisEvent>();
            OffGasAnalysisHistory = new List<OffGasAnalysisEvent>();
            OffGasHistory = new List<OffGasEvent>();
            ReBlowingHistory = new List<ReBlowingEvent>();
            ResetO2TotalVolHistory = new List<ResetO2TotalVolEvent>();
            ScrapHistory = new List<ScrapEvent>();
            SlagBlowingHistory = new List<SlagBlowingEvent>();
            SteelAnalysisHistory = new List<SteelAnalysisEvent>();
            TorkretingHistory = new List<TorkretingEvent>();
            AdditionsHistory = new List<AdditionsEvent>();
            AdditionsNewHistory = new List<AdditionsEventNew>();
            DeslaggingHistory = new List<DeslaggingEvent>();
            HotMetalPouringHistory = new List<HotMetalPouringEvent>();
            ScrapChargingHistory = new List<ScrapChargingEvent>();
            SublanceStartHistory = new List<SublanceStartEvent>();
            SublanceCHistory = new List<SublanceCEvent>();
            SublanceOxidationHistory = new List<SublanceOxidationEvent>();
            SublanceTemperatureHistory = new List<SublanceTemperatureEvent>();
            TappingHistory = new List<TappingEvent>();
            BoilerWaterCoolingHistory = new List<BoilerWaterCoolingEvent>();
            SlagOutburstHistory = new List<SlagOutburstEvent>();
            
            // Command
            comAdditionsHistory = new List<comAdditionsEvent>();
            comAdditionsSchemaHistory = new List<comAdditionsSchemaEvent>();
            comO2FlowRateHistory = new List<comO2FlowRateEvent>();
            comBlowingSchemaHistory = new List<comBlowingSchemaEvent>();
            
            // Counters
            cntAdditionsSchemaHistory = new List<cntAdditionsSchemaEvent>();
            cntAdditionsHistory = new List<cntAdditionsEvent>();
            cntBlowingSchemaHistory = new List<cntBlowingSchemaEvent>();
            cntO2FlowRateHistory = new List<cntO2FlowRateEvent>();
            cntWatchDogPLC01History = new List<cntWatchDogPLC01Event>();
            cntWatchDogPLC1History = new List<cntWatchDogPLC1Event>();
            cntWatchDogPLC2History = new List<cntWatchDogPLC2Event>();
            cntWatchDogPLC3History = new List<cntWatchDogPLC3Event>();

            // Visualisation

            // Blowing
            visBlowingHeatHistory = new List<visBlowingHeatEvent>();
            visBlowingFlowRatesHistory = new List<visBlowingFlowRatesEvent>();
            visBlowingHistory = new List<visBlowingEvent>();
            visSteelAttributesHistory = new List<visSteelAttributesEvent>();

            // Sublance
            visSublanceHistory = new List<visSublanceEvent>();

            // VerticalTractUnit
            visAdditionScalesHistory = new List<visAdditionScalesEvent>();
            visIndustrialBunkersHistory = new List<visIndustrialBunkersEvent>();
            visAdditionBunkersHistory = new List<visAdditionBunkersEvent>();
            visTractControlModeHistory = new List<visTractControlModeEvent>();
            visAlloyingBunker3AHistory = new List<visAlloyingBunker3AEvent>();
            visAlloyingBunkersHistory = new List<visAlloyingBunkersEvent>();
            visCalcinatingFurnacesHistory = new List<visCalcinatingFurnacesEvent>();
            visAlloyingScalesHistory = new List<visAlloyingScalesEvent>();
            #endregion
        }

        #region HistoryData
        public List<visAlloyingBunker3AEvent> visAlloyingBunker3AHistory { get; set; }
        public List<visAlloyingBunkersEvent> visAlloyingBunkersHistory { get; set; }
        public List<visAlloyingScalesEvent> visAlloyingScalesHistory { get; set; }
        public List<visCalcinatingFurnacesEvent> visCalcinatingFurnacesHistory { get; set; }
        public List<visTractControlModeEvent> visTractControlModeHistory { get; set; }
        public List<visSteelAttributesEvent> visSteelAttributesHistory { get; set; }
        public List<visBlowingFlowRatesEvent> visBlowingFlowRatesHistory { get; set; }
        public List<visBlowingEvent> visBlowingHistory { get; set; }
        public List<visSublanceEvent> visSublanceHistory { get; set; }
        public List<visAdditionBunkersEvent> visAdditionBunkersHistory { get; set; }
        public List<visIndustrialBunkersEvent> visIndustrialBunkersHistory { get; set; }
        public List<visAdditionScalesEvent> visAdditionScalesHistory { get; set; }
        public List<visBlowingHeatEvent> visBlowingHeatHistory { get; set; }
        public List<comBlowingSchemaEvent> comBlowingSchemaHistory { get; set; }
        public List<comAdditionsEvent> comAdditionsHistory { get; set; }
        public List<comJobW3Event> comJobW3History { get; set; }
        public List<comJobW4Event> comJobW4History { get; set; }
        public List<comJobW5Event> comJobW5History { get; set; }
        public List<comJobW6Event> comJobW6History { get; set; }
        public List<comJobW7Event> comJobW7History { get; set; }
        public List<ReleaseWeigherEvent> ReleaseWeigherHistory { get; set; }
        public List<SteelMakingPatternEvent> SteelMakingPatternHistory { get; set; }
        public List<BoundNameMaterialsEvent> BoundNameMaterialsHistory { get; set; }
        public List<FixDataMfactorModelEvent> FixDataMfactorModelHistory { get; set; }
        public List<CalculatedCarboneEvent> CalculatedCarboneHistory { get; set; }
        public List<OPCDirectReadEvent> OPCDirectReadHistory { get; set; }
        public List<ComName1MatEvent> comName1MatHistory { get; set; }
        public List<ComName2MatEvent> comName2MatHistory { get; set; }
        public List<comAdditionsSchemaEvent> comAdditionsSchemaHistory { get; set; }
        public List<comO2FlowRateEvent> comO2FlowRateHistory { get; set; }
        public List<comRealOrSimulOxygenSelectEvent> comRealOrSimulOxygenSelectHistory { get; set; }
        public List<comSelectOxygenModeW3Event> comSelectOxygenModeW3History { get; set; }
        public List<comSelectOxygenModeW4Event> comSelectOxygenModeW4History { get; set; }
        public List<comSelectOxygenModeW5Event> comSelectOxygenModeW5History { get; set; }
        public List<comSelectOxygenModeW6Event> comSelectOxygenModeW6History { get; set; }
        public List<comSelectOxygenModeW7Event> comSelectOxygenModeW7History { get; set; }
        public List<comOxigenSimilatorEvent> comOxigenSimilatorHistory { get; set; }
        public List<comOxigenW3SimilatorEvent> comOxigenW3SimilatorHistory { get; set; }
        public List<comOxigenW4SimilatorEvent> comOxigenW4SimilatorHistory { get; set; }
        public List<comOxigenW5SimilatorEvent> comOxigenW5SimilatorHistory { get; set; }
        public List<comOxigenW6SimilatorEvent> comOxigenW6SimilatorHistory { get; set; }
        public List<comOxigenW7SimilatorEvent> comOxigenW7SimilatorHistory { get; set; }
        public List<cntAdditionsEvent> cntAdditionsHistory { get; set; }
        public List<cntWeigher3JobReadyEvent> cntWeigher3JobReadyHistory { get; set; }
        public List<cntWeigher4JobReadyEvent> cntWeigher4JobReadyHistory { get; set; }
        public List<cntWeigher5JobReadyEvent> cntWeigher5JobReadyHistory { get; set; }
        public List<cntWeigher6JobReadyEvent> cntWeigher6JobReadyHistory { get; set; }
        public List<cntWeigher7JobReadyEvent> cntWeigher7JobReadyHistory { get; set; }
        public List<cntAdditionsSchemaEvent> cntAdditionsSchemaHistory { get; set; }
        public List<cntBlowingSchemaEvent> cntBlowingSchemaHistory { get; set; }
        public List<cntO2FlowRateEvent> cntO2FlowRateHistory { get; set; }
        public List<cntWatchDogPLC01Event> cntWatchDogPLC01History { get; set; }
        public List<cntWatchDogPLC1Event> cntWatchDogPLC1History { get; set; }
        public List<cntWatchDogPLC2Event> cntWatchDogPLC2History { get; set; }
        public List<cntWatchDogPLC3Event> cntWatchDogPLC3History { get; set; }
        public List<BoilerWaterCoolingEvent> BoilerWaterCoolingHistory { get; set; }
        public List<TappingEvent> TappingHistory { get; set; }
        public List<SublanceTemperatureEvent> SublanceTemperatureHistory { get; set; }
        public List<SublanceOxidationEvent> SublanceOxidationHistory { get; set; }
        public List<SublanceCEvent> SublanceCHistory { get; set; }
        public List<SublanceStartEvent> SublanceStartHistory { get; set; }
        public List<ScrapChargingEvent> ScrapChargingHistory { get; set; }
        public List<HotMetalPouringEvent> HotMetalPouringHistory { get; set; }
        public List<DeslaggingEvent> DeslaggingHistory { get; set; }
        public List<AdditionsEvent> AdditionsHistory { get; set; }
        public List<TestEvent> TestHistory { get; set; }
        public List<WeighersStateEvent> WeighersStateHistory { get; set; }
        public List<AdditionsEventNew> AdditionsNewHistory { get; set; }
        public List<BlowingEvent> BlowingHistory { get; set; }
        public List<BlowingInterruptEvent> BlowingInterruptHistory { get; set; }
        public List<HeatChangeEvent> HeatChangeEvent { get; set; }
        public List<HeatingScrapEvent> HeatingScrapHistory { get; set; }
        public List<HotMetalLadleEvent> HotMetalLadleHistory { get; set; }
        public List<IgnitionEvent> IgnitionHistory { get; set; }
        public List<LanceEvent> LanceHistory { get; set; }
        public List<ModeLanceEvent> ModeLanceHistory { get; set; }
        public List<ModeVerticalPathEvent> ModeVerticalPathHistory { get; set; }
        public List<MixerAnalysisEvent> MixerAnalysisHistory { get; set; }
        public List<OffGasEvent> OffGasHistory { get; set; }
        public List<ReBlowingEvent> ReBlowingHistory { get; set; }
        public List<ResetO2TotalVolEvent> ResetO2TotalVolHistory { get; set; }
        public List<ScrapEvent> ScrapHistory { get; set; }
        public List<SlagBlowingEvent> SlagBlowingHistory { get; set; }
        public List<SteelAnalysisEvent> SteelAnalysisHistory { get; set; }
        public List<TorkretingEvent> TorkretingHistory { get; set; }
        public List<OffGasAnalysisEvent> OffGasAnalysisHistory { get; set; }
        public List<ConverterAngleEvent> ConverterAngleHistory { get; set; }
        public List<SlagOutburstEvent> SlagOutburstHistory { get; set; }
        #endregion
        public comBlowingSchemaEvent[] BlowingScheme;
    }
}
