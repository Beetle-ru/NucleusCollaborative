#define LANCE_IN_PARKING_POSITION_NOSIGNAL
//#define MAIN_OXYGEN_BLOWING_ONLY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common;
using Data;
using System.Collections;

namespace Models
{
    /// <summary>
    /// Loop is processed in a separate thread.
    /// </summary>
    public class Dynamic : IDisposable
    {
        public enum RunningType
        {
            RealTime,                   // real time
            Simulation,                 // simulation without wastegas analyzer
            RealTimeDataSimulation      // simulation from real process data sim_ tables
        }
        public enum ModelPhaseState
        {
            S00_Waiting4Start,
            S10_MainOxygenBlowing,
            S20_TemperatureMeasurementCommand,
            S25_Waiting4TemperatureMeasurement,
            S30_Correction,
            S40_LanceParkingCommand,
            S45_Waiting4LanceParking,
            S50_Finished,
            S90_Aborted
        }

        public Data.Model.DynamicOutput LastOutputData
        {
            get
            {
                lock (mOutputData)
                {
                    if (mOutputData.Count == 0) return null;
                    return mOutputData.ElementAt(mOutputData.Count - 1).Value;
                }
            }
        }
        public Dictionary<DateTime, Data.Model.DynamicOutput> OutputData
        {
            get
            {
                return mOutputData;
            }
        }

        public event EventHandler<Data.EventArgs.CurrentPhaseChangedEventArgs> PhaseChanged;
        public event EventHandler ModelLoopDone;

        // public methods, constructor
        public Dynamic(Data.Model.DynamicInput aInputData, int aDeltaT_s, RunningType aRunningType = RunningType.RealTime)
        {
            mRecalculateFromTheBeginning = false;
            mInputData = aInputData;
            mCurrentOutputData = new Data.Model.DynamicOutput();
            mOutputData = new Dictionary<DateTime, Data.Model.DynamicOutput>();

            mStepsCount = 0;
            mDeltaT_s = aDeltaT_s;
            mDeltaT_min = (float)aDeltaT_s / 60;

            mRunningType = aRunningType;

            switch (mRunningType)
            {
                case RunningType.RealTime: mClock = new Data.Clock(); break;
                case RunningType.Simulation: mClock = new Data.Clock(aDeltaT_s); break;
                case RunningType.RealTimeDataSimulation: mClock = new Data.Clock(aDeltaT_s); break;
            }

            mRequestQueue = new Queue<object>();

            lock (((ICollection)mRequestQueue).SyncRoot)
            {
                Initialization();
            }

            mCurrentPhaseState = ModelPhaseState.S00_Waiting4Start;
        }

        public void Start()
        {
            mHeatNumber = Data.MINP.Heat.HeatNumber;
            mPaused = false;

            // check if oxygen amount is available
            if (!ArePhasesValid()) throw new ApplicationException("Defined OxygenBlowingPhases in model input data are not valid.");
            if (mCurrentPhaseState >= ModelPhaseState.S50_Finished) Stop();

            mCurrentPhase = mInputData.OxygenBlowingPhases
                .Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowing && aR is Data.PhaseItemOxygenBlowing).Cast<Data.PhaseItemOxygenBlowing>()
                .First();

            mLastMainOxygenBlowingPhase = mInputData.OxygenBlowingPhases
                .Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowing && aR is Data.PhaseItemOxygenBlowing).Cast<Data.PhaseItemOxygenBlowing>()
                .Last();

            mFinalOxygenAmount = mLastMainOxygenBlowingPhase.O2Amount_Nm3.Value;

            mCorrectionOxygenBlowingPhase = mInputData.OxygenBlowingPhases
                .Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowingCorrection && aR is Data.PhaseItemOxygenBlowing).Cast<Data.PhaseItemOxygenBlowing>()
                .FirstOrDefault();

            if (mRunningType != RunningType.RealTimeDataSimulation) Data.Clock.Current.ResetStartTime();
            mCurrentPhaseState = ModelPhaseState.S10_MainOxygenBlowing;
            mSondaRemaining_s = Global.M3_Stat_OpozdeniKonceFoukani;

            #region Output file initial values
            if (Global.M3_GenerateOutputFile)
            {
                try
                {
                    DateTime lActualTime = Data.Clock.Current.ActualTime;
                    mCSVOutput = new StringBuilder();
                    mCSVOutput.Append(mStepsCount);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lActualTime.Date);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lActualTime.TimeOfDay);
                    mCSVOutput.Append(';'); mCSVOutput.Append(Data.Clock.Current.Duration);
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append(mInputData.ChargedMaterials.Where(aR => aR.ShortCode.StartsWith("01")).ToArray().Sum(aR => aR.Amount_kg)); // m_SŽ
                    mCSVOutput.Append(';'); mCSVOutput.Append(mInputData.ChargedMaterials.Where(aR => aR.ShortCode.StartsWith("02")).ToArray().Sum(aR => aR.Amount_kg));
                    mCSVOutput.Append(';');
                    double sum = 0.0;
                    if (Data.MINP.MINP_GD_ModelMaterials.ContainsKey(Enumerations.MINP_GD_Material_ModelMaterial.Coke))
                        mCSVOutput.Append(mInputData.ChargedMaterials.Where(aR => aR.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Coke].ShortCode).ToArray().Sum(aR => aR.Amount_kg));
                    else
                        mCSVOutput.Append("");
                    mCSVOutput.Append(';');
                    if (Data.MINP.MINP_GD_ModelMaterials.ContainsKey(Enumerations.MINP_GD_Material_ModelMaterial.CaO))
                        mCSVOutput.Append(mInputData.ChargedMaterials.Where(aR => aR.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.CaO].ShortCode).ToArray().Sum(aR => aR.Amount_kg));
                    else
                        mCSVOutput.Append("");
                    mCSVOutput.Append(';');
                    if (Data.MINP.MINP_GD_ModelMaterials.ContainsKey(Enumerations.MINP_GD_Material_ModelMaterial.Dolomite))
                        mCSVOutput.Append(mInputData.ChargedMaterials.Where(aR => aR.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Dolomite].ShortCode).ToArray().Sum(aR => aR.Amount_kg));
                    else
                        mCSVOutput.Append("");
                    mCSVOutput.Append(';');
                    if (Data.MINP.MINP_GD_ModelMaterials.ContainsKey(Enumerations.MINP_GD_Material_ModelMaterial.FOM))
                        mCSVOutput.Append(mInputData.ChargedMaterials.Where(aR => aR.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.FOM].ShortCode).ToArray().Sum(aR => aR.Amount_kg));
                    else
                        mCSVOutput.Append("");
                    int lAmount = 0;
                    if (Data.MINP.MINP_GD_ModelMaterials.ContainsKey(Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer1))
                        lAmount += mInputData.ChargedMaterials.Where(aR => aR.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer1].ShortCode).Sum(aR => aR.Amount_kg);
                    if (Data.MINP.MINP_GD_ModelMaterials.ContainsKey(Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer2))
                        lAmount += mInputData.ChargedMaterials.Where(aR => aR.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer2].ShortCode).Sum(aR => aR.Amount_kg);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lAmount); // m_CaCO3
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");    // T mer
                    mCSVOutput.Append(';'); mCSVOutput.Append("");    // C mer
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.T_Tavby);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_Tavby);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_Kov);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_Struska);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[0]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[1]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[2]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[3]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[7]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[10]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[11]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[5]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaKov[32]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaStruska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaStruska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaStruska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaStruska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaStruska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_SlozkaStruska[55 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.E_Tavby);
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                    {
                        Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;

                        if (lEleIndex == Enumerations.M3ElementEnum.Fe)
                        {
                            mCSVOutput.Append(';'); mCSVOutput.Append("");
                        }
                        else
                        {
                            mCSVOutput.Append(';'); mCSVOutput.Append("");
                        }
                    }

                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[0]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[1]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[2]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[3]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[7]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[10]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[11]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[5]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Kov[32]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Struska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Struska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Struska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Struska[55 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX] / mCurrentStateData.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    double lDeCSpaliny = 0;
                    mCSVOutput.Append(';'); mCSVOutput.Append(lDeCSpaliny);
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    if (Data.MINP.MINP_Cyclic.Count > 1)
                    {
                        DTO.MINP_CyclicDTO lCyclicDataPrevious = Data.MINP.MINP_Cyclic.OrderByDescending(aR => aR.C__Created).Skip(1).First();
                        mCSVOutput.Append(';'); mCSVOutput.Append("");
                    }
                    else
                    {
                        mCSVOutput.Append(';'); mCSVOutput.Append(0);
                    }
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCO2Buffer);
                    mCSVOutput.AppendLine();
                }
                catch { }
            }
            #endregion

            mTimer = new System.Threading.Timer(new System.Threading.TimerCallback(ControlLoop));

            if (mRunningType == RunningType.RealTime)
            {
                mTimer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(mDeltaT_s));
            }
            else
            {
                mTimer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0));
            }
        }
        public void Stop()
        {
            if (mTimer != null) mTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            // save to CSV
            SaveData2CSV();

            if (mCurrentPhaseState != ModelPhaseState.S90_Aborted) mCurrentPhaseState = ModelPhaseState.S50_Finished;
        }
        public void Pause()
        {
            // for simulation
            StopSimulationTimer();
            // mPaused for real time
            mPaused = true;
        }
        public void Resume()
        {
            // for simulation
            StartSimulationTimer();
            // mPaused for real time
            mPaused = false;
        }
        public void Dispose()
        {
            Stop();
            if (mTimer != null) mTimer.Dispose();
        }

        public void EnqueueTemperatureMeasured(DTO.MINP_TempMeasDTO aTemperatureMeasured)
        {
            mRequestQueue.Enqueue(aTemperatureMeasured);
        }
        public void EnqueueMaterialAdded(DTO.MINP_MatAddDTO aMaterial)
        {
            mRequestQueue.Enqueue(aMaterial);
        }
        /// <summary>
        /// Can be called only in MainOxygenBlowingPhase.
        /// Recalculates the model in the next ControlLoop call.
        /// The model cannot be paused.
        /// </summary>
        /// <remarks>
        /// 1) Call Pause().
        /// 2) Do modifications in model input data (Data.MINP structure).
        /// 3) Call RecalculateFromBeginning().
        /// 4) Call Resume().
        /// </remarks>
        public void RecalculateFromBeginning(Data.Model.ChargingInput aInputData)
        {
            if (mCurrentPhase.PhaseGroup != PhasePrimaryDivision.OxygenBlowing)
                throw new ApplicationException("Dynamic model can be recalculated only within main oxygen blowing phase.");
            mRecalculateFromTheBeginning = true;
            mInputData.HotMetal_Temperature = aInputData.HotMetal_Temperature;
            mInputData.Scrap_Temperature = aInputData.Scrap_Temperature;
            mInputData.ChargedMaterials = Data.MINP.MINP_MatAdds.Where(aR => aR.ShortCode.StartsWith("01") || aR.ShortCode.StartsWith("02")).ToList();
        }
        /// <summary>
        /// Runs simulation from the beginning of the heat until now with modified model input data.
        /// </summary>

        public bool RecalcFlagForDebaging; // remoove this after debug complete
        private void RecalculateFromBeginningInThread()
        {
            RecalcFlagForDebaging = true;
            mRecalculateFromTheBeginning = false;
            if (mRunningType == RunningType.RealTime) mTimer.Change(-1, -1);
            RunningType lPreviousRunningType = mRunningType;
            mRunningType = RunningType.Simulation;

            // new Initialization
            Initialization();

            DateTime lStartTime = Data.Clock.Current.StartTime;
            DateTime lNow = Data.Clock.Current.ActualTime;
            List<DTO.MINP_CyclicDTO> lMINP_CyclicData = Data.MINP.MINP_Cyclic.OrderBy(aR => aR.TimeProcessed).ToList();
            List<DTO.MINP_MatAddDTO> lMINP_MatAddData = Data.MINP.MINP_MatAdds.Where(aR => !aR.ShortCode.StartsWith("01") && !aR.ShortCode.StartsWith("02")).OrderBy(aR => aR.TimeProcessed).ToList();
            Data.MINP.MINP_Cyclic = new List<DTO.MINP_CyclicDTO>();
            Data.MINP.MINP_MatAdds = new List<DTO.MINP_MatAddDTO>();
            int lStepsCount = mStepsCount;
            mStepsCount = 0;

            Data.Clock lOldClock = Data.Clock.Current;
            new Data.Clock(lStartTime, mDeltaT_s);
            mOutputData.Clear();

            // loops
            int cs = 0;
            while (cs < lStepsCount)
            {
                if (lMINP_CyclicData.Count > 0)
                {
                    Data.MINP.MINP_Cyclic.Add(lMINP_CyclicData[0]);
                    lMINP_CyclicData.RemoveAt(0);
                }

                while (lMINP_MatAddData.Count > 0 && lMINP_MatAddData[0].TimeProcessed <= Data.Clock.Current.ActualTime)
                {
                    Data.MINP.MINP_MatAdds.Add(lMINP_MatAddData[0]);
                    EnqueueMaterialAdded(lMINP_MatAddData[0]);
                    lMINP_MatAddData.RemoveAt(0);
                }
                ControlLoop(null);
                cs++;
            }

            Data.Clock.Current = lOldClock;
            

            mRunningType = lPreviousRunningType;
            if (mRunningType == RunningType.RealTime) mTimer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(mDeltaT_s)); //was 0

            RecalcFlagForDebaging = false;
        }

        private void StartSimulationTimer()
        {
            if (mRunningType != RunningType.RealTime)
                mTimer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0));
        }
        private void StopSimulationTimer()
        {
            if (mRunningType != RunningType.RealTime)
                mTimer.Change(-1, -1);
        }

        // private methods
        private void Initialization()
        {
            // R 5.9 .. R 5.15
            mC_kov_min_p = new Dictionary<Enumerations.M3ElementEnum, float>();
            foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
            {
                Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                mC_kov_min_p.Add(lEleIndex, Global.M3_c_kov_min_p[lEleIndex] / MINP.ConversionVector(nIndex));
            }

            #region Data structures initialization
            mCurrentOutputData = new Data.Model.DynamicOutput();
            mCurrentOutputData.FP_Tavby = new float[Global.MATERIALELEMENTS_COUNT];
            mCurrentOutputData.m_SlozkaTavby = new float[Global.MATERIALELEMENTS_STEELANDSLAG_COUNT];
            mCurrentOutputData.FP_Kov = new float[Global.MATERIALELEMENTS_STEEL_COUNT];
            mCurrentOutputData.FP_Struska = new float[Global.MATERIALELEMENTS_SLAG_COUNT];
            mCurrentOutputData.m_SlozkaKov = new float[Global.MATERIALELEMENTS_STEEL_COUNT];
            mCurrentOutputData.m_SlozkaStruska = new float[Global.MATERIALELEMENTS_SLAG_COUNT];
            mCurrentOutputData.c_Kov = new float[Global.MATERIALELEMENTS_STEEL_COUNT];
            mCurrentOutputData.c_Struska = new float[Global.MATERIALELEMENTS_SLAG_COUNT];
            for (int i = 0; i < Global.MATERIALELEMENTS_COUNT; i++) mCurrentOutputData.FP_Tavby[i] = 0;

            mCurrentStateData = new Data.Model.DynamicState();
            mCurrentStateData.FP_Tavby = new float[Global.MATERIALELEMENTS_COUNT];
            mCurrentStateData.m_SlozkaTavby = new float[Global.MATERIALELEMENTS_STEELANDSLAG_COUNT];
            mCurrentStateData.FP_Kov = new float[Global.MATERIALELEMENTS_STEEL_COUNT];
            mCurrentStateData.FP_Struska = new float[Global.MATERIALELEMENTS_SLAG_COUNT];
            mCurrentStateData.m_SlozkaKov = new float[Global.MATERIALELEMENTS_STEEL_COUNT];
            mCurrentStateData.m_SlozkaStruska = new float[Global.MATERIALELEMENTS_SLAG_COUNT];
            mCurrentStateData.c_Kov = new float[Global.MATERIALELEMENTS_STEEL_COUNT];
            mCurrentStateData.c_Struska = new float[Global.MATERIALELEMENTS_SLAG_COUNT];
            for (int i = 0; i < Global.MATERIALELEMENTS_COUNT; i++) mCurrentStateData.FP_Tavby[i] = 0;
            mCurrentStateData.E_Elements = new Dictionary<Enumerations.M3ElementEnum, float>();
            mCO2Buffer = 0;
            #endregion

            #region m, E, T Tavby
            // R 5.4
            mCurrentStateData.m_Tavby = mInputData.ChargedMaterials.Sum(aR => aR.Amount_kg);

            // R 5.5 - 5.7
            foreach (var nItem in mInputData.ChargedMaterials)
            {
                for (int i = 0; i < Global.MATERIALELEMENTS_COUNT; i++)
                {
                    mCurrentStateData.FP_Tavby[i] += nItem.Amount_kg * MINP.FP(nItem.MINP_GD_Material, i);
                }
                mCO2Buffer += nItem.Amount_kg * MINP.FP(nItem.MINP_GD_Material, 44);
            }

            for (int i = 0; i < Global.MATERIALELEMENTS_COUNT; i++)
            {
                mCurrentStateData.FP_Tavby[i] /= mCurrentStateData.m_Tavby;
            }

            float lSuma_m_SZ_real = 0;
            float[] lStredni_SZ = new float[Global.MATERIALELEMENTS_COUNT];
            float lSuma_m_Other_real = 0;
            float[] lStredni_Other = new float[Global.MATERIALELEMENTS_COUNT];

            DTO.MINP_MatAddDTO[] lHotMetals;
            DTO.MINP_MatAddDTO[] lOtherMaterials;

            for (int i = 0; i < Global.HOTMETAL_COUNT; i++) lStredni_SZ[i] = 0;
            for (int i = 0; i < Global.SCRAPYARDS_COUNT; i++) lStredni_Other[i] = 0;

            lHotMetals = mInputData.ChargedMaterials.Where(aR => aR.ShortCode.StartsWith("01")).ToArray();
            lOtherMaterials = mInputData.ChargedMaterials.Where(aR => !aR.ShortCode.StartsWith("01")).ToArray();

            #region Stredni SZ index 69 .. 72
            foreach (DTO.MINP_MatAddDTO nItem in lHotMetals)
            {
                lSuma_m_SZ_real += nItem.Amount_kg;

                for (int iElement = 69; iElement <= 72; iElement++)
                {
                    DTO.MINP_GD_MaterialItemsDTO lMaterialItem = nItem.MINP_GD_Material.MINP_GD_MaterialItems.SingleOrDefault(aR => aR.MINP_GD_MaterialElement.Index == iElement);
                    if (lMaterialItem != null) lStredni_SZ[iElement] += nItem.Amount_kg * (float)lMaterialItem.Amount_p;
                }
            }

            for (int iElement = 69; iElement <= 72; iElement++)
            {
                lStredni_SZ[iElement] = lStredni_SZ[iElement] / lSuma_m_SZ_real;
            }
            #endregion
            #region Stredni Ostatni (R 6..10)
            foreach (DTO.MINP_MatAddDTO nItem in lOtherMaterials)
            {
                lSuma_m_Other_real += nItem.Amount_kg;

                for (int iElement = 69; iElement <= 72; iElement++)
                {
                    DTO.MINP_GD_MaterialItemsDTO lMaterialItem = nItem.MINP_GD_Material.MINP_GD_MaterialItems.SingleOrDefault(aR => aR.MINP_GD_MaterialElement.Index == iElement);
                    if (lMaterialItem != null) lStredni_Other[iElement] += nItem.Amount_kg * (float)lMaterialItem.Amount_p;
                }
            }

            for (int iElement = 69; iElement <= 72; iElement++)
            {
                lStredni_Other[iElement] = lStredni_Other[iElement] / lSuma_m_Other_real;
            }
            #endregion

            // temperatures - real x [69]
            mT_SZ = (mInputData.HotMetal_Temperature.HasValue) ? mInputData.HotMetal_Temperature.Value : lStredni_SZ[69];
            mT_Other = (mInputData.Scrap_Temperature.HasValue) ? mInputData.Scrap_Temperature.Value : lStredni_Other[69];

            float lH_SZ = lSuma_m_SZ_real * (lStredni_SZ[70] / MINP.ConversionVector(70) / lStredni_SZ[72]) * mT_SZ;
            if (mT_SZ > lStredni_SZ[72])
                lH_SZ = lSuma_m_SZ_real * (lStredni_SZ[70] / MINP.ConversionVector(70) + lStredni_SZ[71] / MINP.ConversionVector(71) * (mT_SZ - lStredni_SZ[72]));

            // jiz spocten prumer pro vsechny ostatni pridane prisady
            float lH_Cold = lSuma_m_Other_real * (lStredni_Other[70] / MINP.ConversionVector(70) / lStredni_Other[72]) * mT_Other;

            mCurrentStateData.E_Tavby = lH_SZ + lH_Cold;

            // R 5..7
            mCurrentStateData.T_Tavby = mCurrentStateData.E_Tavby / mCurrentStateData.FP_Tavby[70] * mCurrentStateData.FP_Tavby[72] / mCurrentStateData.m_Tavby * 1000;

            // R 5.8
            for (int i = 0; i < Global.MATERIALELEMENTS_STEELANDSLAG_COUNT; i++)
            {
                mCurrentStateData.m_SlozkaTavby[i] = mCurrentStateData.m_Tavby * mCurrentStateData.FP_Tavby[i] / MINP.ConversionVector(i);
            }
            #endregion

            RecalculateKovStruskaFromTavba();

            mC_kov_start = mCurrentStateData.FP_Kov[0];
            mC_kov_end = Global.M3_Stat_C_konec + (float)(new Random()).NextDouble() * Global.M3_Stat_C_konec_random * 2f - Global.M3_Stat_C_konec_random;
        }
        /// <summary>
        /// Check if Phases in model input are valid.
        /// aCorrection = false ~ correction phase oxygen blowing amount can be null.
        /// </summary>
        private bool ArePhasesValid(bool aValidateCorrectionPhase = false)
        {
            if (mInputData.OxygenBlowingPhases == null) return false;
            // all oxygen blowing phases must have amount
            if (mInputData.OxygenBlowingPhases.Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowing && aR is Data.PhaseItemOxygenBlowing).Cast<Data.PhaseItemOxygenBlowing>().Any(aR => !aR.O2Amount_Nm3.HasValue)) return false;
            // only matadd phases are allowed in main oxygen blowing
            if (mInputData.OxygenBlowingPhases.Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowing && !(aR is Data.PhaseItemOxygenBlowing) && !(aR is Data.PhaseItemL1Command)).Any(aR => !(aR is Data.PhaseItemMatAdd))) return false;
            // correction phases ~ L1 tempmeas, correction, L1 lance parking
            IEnumerable<PhaseItem> lCorrectionPhases = mInputData.OxygenBlowingPhases.Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowingCorrection);

            if (mRunningType == RunningType.RealTimeDataSimulation) return true;

            if (lCorrectionPhases.Count() != 3) return false;
            Data.PhaseItem lPhaseL1TempMeas = lCorrectionPhases.First();
            Data.PhaseItem lPhaseCorrection = lCorrectionPhases.Skip(1).Take(1).First();
            Data.PhaseItem lPhaseL1LanceParking = lCorrectionPhases.Last();
            if (!(lPhaseL1TempMeas is Data.PhaseItemL1Command)) return false;
            if (!(lPhaseCorrection is Data.PhaseItemOxygenBlowing)) return false;
            if (!(lPhaseL1LanceParking is Data.PhaseItemL1Command)) return false;
            Data.PhaseItemL1Command lTPhaseL1TempMeas = (Data.PhaseItemL1Command)lPhaseL1TempMeas;
            Data.PhaseItemOxygenBlowing lTPhaseCorrection = (Data.PhaseItemOxygenBlowing)lPhaseCorrection;
            Data.PhaseItemL1Command lTPhaseL1LanceParking = (Data.PhaseItemL1Command)lPhaseL1LanceParking;
            if (lTPhaseL1TempMeas.L1Command != Enumerations.L2L1_Command.TemperatureMeasurement) return false;
            if (aValidateCorrectionPhase) if (!lTPhaseCorrection.O2Amount_Nm3.HasValue) return false;
            if (lTPhaseL1LanceParking.L1Command != Enumerations.L2L1_Command.OxygenLanceToParkingPosition) return false;

            return true;
        }

        private void ControlLoop(object aState)
        {
            if (mPaused) return;

            //CHEREP      StopSimulationTimer();

            if (mRecalculateFromTheBeginning) RecalculateFromBeginningInThread();

            #region Current phase validation
            if (mCurrentPhase == null)
            {
                Stop();
                throw new ApplicationException("CurrentPhase is null during model Loop. Cannot continue.");
            }
            #endregion
            #region Model loop
            ProcessQueueRequests();

            Data.Model.DynamicOutput lLoopOutputData = ModelLoop();

            bool tryAgainLater = true;
            do
            {
                var lKey = Clock.Current.ActualTime;
                lock (mOutputData)
                {
                    if (mOutputData.ContainsKey(lKey))
                    {
                        System.Threading.Thread.Sleep(1);
                    }
                    else
                    {
                        mOutputData.Add(lKey, lLoopOutputData);
                        tryAgainLater = false;
                    }
                }
            } while (tryAgainLater);
            mStepsCount++;
            if (mRunningType != RunningType.RealTime) Data.Clock.Current.IncSimulationStep();
            #endregion

            DTO.MINP_CyclicDTO lCyclicData = Data.MINP.MINP_Cyclic.Last();
            mCurrentO2Amount = lCyclicData.OxygenConsumption_m3.Value;

            while (true)
            {
                #region Main oxygen blowing
                if (mCurrentPhaseState == ModelPhaseState.S10_MainOxygenBlowing)
                {
                    float lAmount_p = (float)lCyclicData.OxygenConsumption_m3.Value / mFinalOxygenAmount * 100;

                    if (mRunningType == RunningType.Simulation || !Global.M3_Stat_C_ON)
                    {
                        // O2 amount condition fullfilled?
                        if ((lAmount_p >= Global.M3_End_Condition_O2_Max)
                            || (0 >= Global.M3_End_Condition_K2_Max)
                            || (lAmount_p >= Global.M3_End_Condition_O2_Aim && 0 >= Global.M3_End_Condition_K2_Min)
                            || (lAmount_p >= Global.M3_End_Condition_O2_Min && 0 >= Global.M3_End_Condition_K2_Aim))
                        {
#if MAIN_OXYGEN_BLOWING_ONLY
                            if (ModelLoopDone != null) ModelLoopDone(this, EventArgs.Empty);
                            SwitchPhaseToL1OxygenLanceParking();
                            Stop();
                            return;
#endif
                            mCurrentPhaseState = ModelPhaseState.S20_TemperatureMeasurementCommand;
                        }
                    }
                    else
                    {
                        #region C statistical correction
                        float lOprava_C = 0;

                        if (LastOutputData != null && lCyclicData.Wastegas_CO2_p <= lCyclicData.Wastegas_CO_p)
                        {
                            mSondaRemaining_s = Global.M3_Stat_OpozdeniKonceFoukani;
                        }

                        if (LastOutputData != null /*&& lCyclicData.Wastegas_CO2_p <= lCyclicData.Wastegas_CO_p*/ && LastOutputData.FP_Kov[0] < mC_kov_end)
                        {
                            lOprava_C = LastOutputData.FP_Kov[0] - mC_kov_end;
                            mCurrentStateData.FP_Kov[0] = mC_kov_end;
                            mCurrentStateData.m_SlozkaKov[0] = mC_kov_end * mCurrentStateData.m_Kov / MINP.ConversionVector(0);
                            mCurrentStateData.FP_Tavby[0] = mCurrentStateData.FP_Kov[0] * mCurrentStateData.m_Kov / mCurrentStateData.m_Tavby;
                            mCurrentStateData.m_SlozkaTavby[0] = mCurrentStateData.FP_Tavby[0] * mCurrentStateData.m_Kov / MINP.ConversionVector(0);
                            LastOutputData.FP_Kov[0] = mC_kov_end;
                            LastOutputData.m_SlozkaKov[0] = mC_kov_end * LastOutputData.m_Kov / MINP.ConversionVector(0);
                            LastOutputData.FP_Tavby[0] = LastOutputData.FP_Kov[0] * LastOutputData.m_Kov / LastOutputData.m_Tavby;
                            LastOutputData.m_SlozkaTavby[0] = LastOutputData.FP_Tavby[0] * LastOutputData.m_Kov / MINP.ConversionVector(0);
                        }

                        if (lAmount_p >= Global.M3_End_Condition_O2_Min)
                        {
                            if (LastOutputData != null && lCyclicData.Wastegas_CO2_p >= lCyclicData.Wastegas_CO_p)
                                mSondaRemaining_s -= mDeltaT_s;

                            if (mRunningType == RunningType.RealTime && mSondaRemaining_s <= 0)
                            {
                                Run_C_Correction();

                                // start temperature measurement
#if MAIN_OXYGEN_BLOWING_ONLY
                                if (ModelLoopDone != null) ModelLoopDone(this, EventArgs.Empty);
                                SwitchPhaseToL1OxygenLanceParking();
                                Stop();
                                return;
#endif
                                mCurrentPhaseState = ModelPhaseState.S20_TemperatureMeasurementCommand;
                            }
                        }

                        if (lOprava_C != 0)
                        {
                            Run_C_Correction();
                        }

                        // O2 over maxmax
                        if ((mRunningType == RunningType.RealTimeDataSimulation && lAmount_p >= Global.M3_End_Condition_O2_Aim)
                            || (mRunningType == RunningType.RealTime && lAmount_p >= Global.M3_End_Condition_O2_Max))
                        {
                            Run_C_Correction();

                            // start temperature measurement
#if MAIN_OXYGEN_BLOWING_ONLY
                            if (ModelLoopDone != null) ModelLoopDone(this, EventArgs.Empty);
                            SwitchPhaseToL1OxygenLanceParking();
                            Stop();
                            return;
#endif
                            mCurrentPhaseState = ModelPhaseState.S20_TemperatureMeasurementCommand;
                        }
                        #endregion
                    }
                }
                #endregion

                // after C correction
                if (ModelLoopDone != null) ModelLoopDone(this, EventArgs.Empty);

                #region Temperature measurement and waiting
                if (mCurrentPhaseState == ModelPhaseState.S20_TemperatureMeasurementCommand)
                {
                    SwitchPhaseToL1TemperatureMeasurement();
                    break;
                }
                if (mCurrentPhaseState == ModelPhaseState.S25_Waiting4TemperatureMeasurement)
                {
                    if (mRunningType == RunningType.Simulation)
                        System.Threading.Thread.Sleep(mDeltaT_s * 1000);
                    break;
                }
                #endregion
                #region Correction
                if (mCurrentPhaseState == ModelPhaseState.S30_Correction)
                {
                    if (mCurrentO2Amount >= mCorrectionOxygenAmount)
                    {
                        mCurrentPhaseState = ModelPhaseState.S40_LanceParkingCommand;
                    }
                }
                #endregion
                #region Oxygen lance parking, Finish
                if (mCurrentPhaseState == ModelPhaseState.S40_LanceParkingCommand)
                {
                    SwitchPhaseToL1OxygenLanceParking();
#if LANCE_IN_PARKING_POSITION_NOSIGNAL
                    Stop();     // in case of no signal from L1
                    return;
#else
                    break;
#endif
                }
                if (mCurrentPhaseState == ModelPhaseState.S45_Waiting4LanceParking)
                {
                    if (mRunningType == RunningType.Simulation)
                        System.Threading.Thread.Sleep(mDeltaT_s * 1000);
                    break;
                }

                if (mCurrentPhaseState == ModelPhaseState.S50_Finished)
                {
                    Stop();
                    return;
                }
                #endregion

                // switch phase in other case
                if (mCurrentPhase != mLastMainOxygenBlowingPhase
                    && mCurrentPhase is Data.PhaseItemOxygenBlowing
                    && lCyclicData.OxygenConsumption_m3 > ((Data.PhaseItemOxygenBlowing)mCurrentPhase).O2Amount_Nm3)
                    SwitchToNextPhase();
                else if (mCurrentPhase is Data.PhaseItemMatAdd
                    && lCyclicData.OxygenConsumption_m3 > ((Data.PhaseItemMatAdd)mCurrentPhase).O2Amount_Nm3) SwitchToNextPhase();

                break;
            }
            
            //CHEREP    StartSimulationTimer();
        }
        private void ProcessQueueRequests()
        {
            // process queue requests (temperature measurement, material additions)
            lock (((ICollection)mRequestQueue).SyncRoot)
            {
                mCSVCoke = mCSVDolom = mCSVFOM = mCSVLime = mCSVS1S2 = 0;

                while (mRequestQueue.Count > 0)
                {
                    object lValue = mRequestQueue.Dequeue();

                    if (lValue is DTO.MINP_TempMeasDTO)
                    {
                        ProcessTemperatureMeasured((DTO.MINP_TempMeasDTO)lValue);
                    }
                    else if (lValue is DTO.MINP_MatAddDTO)
                    {
                        ProcessMaterialAdded((DTO.MINP_MatAddDTO)lValue);
                    }
                    else
                    {
                        throw new NotImplementedException(String.Format("Dynamic model cannot process {0} type request.", lValue));
                    }
                }
            }
        }
        private void ProcessTemperatureMeasured(DTO.MINP_TempMeasDTO aTempMeas)
        {
            if (!aTempMeas.Temperature.HasValue)
            {
                mCorrectionOxygenAmount = 0;
            }

            float lO_Dofuk_C = 0;
            float lO_Dofuk_T = 0;

            if (aTempMeas.Temperature <= Data.MINP.HeatAimData.FinalTemperature)
            {
                lO_Dofuk_T = Global.M3_O_T * (Data.MINP.HeatAimData.FinalTemperature - aTempMeas.Temperature.Value);
            }
            if (aTempMeas.Carbon_p.HasValue && aTempMeas.Carbon_p.Value > Data.MINP.HeatAimData.FinalC_p)
            {
                lO_Dofuk_C = (float)(Global.M3_O_C * (aTempMeas.Carbon_p.Value - Data.MINP.HeatAimData.FinalC_p));
            }

            mCorrectionOxygenAmount = (int)Math.Round(lO_Dofuk_T);
            if (lO_Dofuk_C > lO_Dofuk_T) mCorrectionOxygenAmount = (int)Math.Round(lO_Dofuk_C);

            aTempMeas.CorrectionOxigen = mCorrectionOxygenAmount;
            // calculated amount + main oxygen blowing amount
            mCorrectionOxygenAmount += mFinalOxygenAmount;
            aTempMeas.TotalOxigen = mCorrectionOxygenAmount;

            if (mCorrectionOxygenBlowingPhase == null) Stop();
            else
            {
                // set correction amount
                mCorrectionOxygenBlowingPhase.O2Amount_Nm3 = mCorrectionOxygenAmount;
                SwitchPhaseToO2Correction();
            }
        }
        private void ProcessMaterialAdded(DTO.MINP_MatAddDTO aMatAdd)
        {
            // csv
            if (aMatAdd.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Coke].ShortCode) mCSVCoke += aMatAdd.Amount_kg;
            if (aMatAdd.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.CaO].ShortCode) mCSVLime += aMatAdd.Amount_kg;
            if (aMatAdd.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Dolomite].ShortCode) mCSVDolom += aMatAdd.Amount_kg;
            if (aMatAdd.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.FOM].ShortCode) mCSVFOM += aMatAdd.Amount_kg;
            if (Data.MINP.MINP_GD_ModelMaterials.ContainsKey(Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer1) && aMatAdd.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer1].ShortCode) mCSVS1S2 += aMatAdd.Amount_kg;
            if (Data.MINP.MINP_GD_ModelMaterials.ContainsKey(Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer2) && aMatAdd.ShortCode == Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer2].ShortCode) mCSVS1S2 += aMatAdd.Amount_kg;

            // ProcessMaterialAdded
            float lSuma_m_Other_real = 0;
            float[] lStredni_Other = new float[Global.MATERIALELEMENTS_COUNT];

            // FP Tavby
            for (int j = 0; j < Global.MATERIALELEMENTS_COUNT; j++)
            {
                mCurrentStateData.FP_Tavby[j] = (mCurrentStateData.m_Tavby * mCurrentStateData.FP_Tavby[j] + aMatAdd.Amount_kg * MINP.FP(aMatAdd.MINP_GD_Material, j))
                    / (mCurrentStateData.m_Tavby + aMatAdd.Amount_kg);
            }

            mCurrentStateData.m_Tavby += aMatAdd.Amount_kg;

            lSuma_m_Other_real += aMatAdd.Amount_kg;

            for (int iElement = 69; iElement <= 72; iElement++)
            {
                DTO.MINP_GD_MaterialItemsDTO lMaterialItem = aMatAdd.MINP_GD_Material.MINP_GD_MaterialItems.SingleOrDefault(aR => aR.MINP_GD_MaterialElement.Index == iElement);
                lStredni_Other[iElement] += aMatAdd.Amount_kg * (float)lMaterialItem.Amount_p;
            }


            for (int i = 0; i < Global.MATERIALELEMENTS_STEELANDSLAG_COUNT; i++)
            {
                mCurrentStateData.m_SlozkaTavby[i] = mCurrentStateData.m_Tavby * mCurrentStateData.FP_Tavby[i] / MINP.ConversionVector(i);
            }

            // H_Studene
            for (int iElement = 69; iElement <= 72; iElement++)
            {
                lStredni_Other[iElement] = lStredni_Other[iElement] / lSuma_m_Other_real;
            }

            float lH_Cold = lSuma_m_Other_real * (lStredni_Other[70] / MINP.ConversionVector(70) / lStredni_Other[72]) * mT_Other;
            mCurrentStateData.E_Tavby += lH_Cold;
            // CO2 buffer
            mCO2Buffer += aMatAdd.Amount_kg * MINP.FP(aMatAdd.MINP_GD_Material, 44);
        }
        
        private Data.Model.DynamicOutput ModelLoop()
        {
            float lm_Odprasky_krok = (Global.M_Odprasky / Global.TauTavby * mDeltaT_min);

            #region m, c - kov, struska R 16 .. R 25
            mCurrentStateData.m_Struska = 0;
            for (int i = 0; i < Global.MATERIALELEMENTS_SLAG_COUNT; i++)
            {
                int lIndex = i + Global.MATERIALELEMENTS_SLAG_STARTINDEX;
                mCurrentStateData.m_Struska += mCurrentStateData.m_Tavby * (mCurrentStateData.FP_Tavby[lIndex] / MINP.ConversionVector(lIndex));
            }
            mCurrentStateData.m_Kov = 0;
            for (int i = 0; i < Global.MATERIALELEMENTS_STEEL_COUNT; i++)
            {
                mCurrentStateData.m_Kov += mCurrentStateData.m_Tavby * (mCurrentStateData.FP_Tavby[i] / MINP.ConversionVector(i));
            }

            for (int i = 0; i < Global.MATERIALELEMENTS_SLAG_COUNT; i++)
            {
                int lIndex = i + Global.MATERIALELEMENTS_SLAG_STARTINDEX;
                mCurrentStateData.FP_Struska[i] = mCurrentStateData.FP_Tavby[lIndex] * mCurrentStateData.m_Tavby / mCurrentStateData.m_Struska;
                mCurrentStateData.m_SlozkaStruska[i] = mCurrentStateData.FP_Struska[i] * mCurrentStateData.m_Struska / MINP.ConversionVector(lIndex);
                mCurrentStateData.c_Struska[i] = mCurrentStateData.FP_Struska[i] / MINP.ConversionVector(lIndex);
            }
            for (int i = 0; i < Global.MATERIALELEMENTS_STEEL_COUNT; i++)
            {
                mCurrentStateData.FP_Kov[i] = mCurrentStateData.FP_Tavby[i] * mCurrentStateData.m_Tavby / mCurrentStateData.m_Kov;
                mCurrentStateData.m_SlozkaKov[i] = mCurrentStateData.FP_Kov[i] * mCurrentStateData.m_Kov / MINP.ConversionVector(i);
            }

            for (int i = 0; i < Global.MATERIALELEMENTS_STEEL_COUNT; i++)
            {
                mCurrentStateData.c_Kov[i] = mCurrentStateData.FP_Kov[i] / MINP.ConversionVector(i);
            }

            #endregion

            // m_SlozkaKov, m_SlozkaStruska - budou adekvatne odecteny odprasky za 1 krok
            for (int i = 0; i < Global.MATERIALELEMENTS_SLAG_COUNT; i++)
            {
                int lIndex = i + Global.MATERIALELEMENTS_SLAG_STARTINDEX;
                mCurrentStateData.m_SlozkaStruska[i] -= lm_Odprasky_krok * MINP.FP(MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], lIndex) / MINP.ConversionVector(lIndex);
            }
            for (int i = 0; i < Global.MATERIALELEMENTS_STEEL_COUNT; i++)
            {
                mCurrentStateData.m_SlozkaKov[i] -= lm_Odprasky_krok * MINP.FP(MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], i) / MINP.ConversionVector(i);
            }

            float lm_C_Start = mCurrentStateData.m_SlozkaKov[0];

            Dictionary<Enumerations.M3ElementEnum, float> lm_Ele_Start = new Dictionary<Enumerations.M3ElementEnum, float>();
            foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                lm_Ele_Start.Add((Enumerations.M3ElementEnum)nIndex, mCurrentStateData.m_SlozkaKov[nIndex]);
            
            DTO.MINP_CyclicDTO lCyclicData = Data.MINP.MINP_Cyclic.Last();


            // R 37 - 39
            float lm_O2_blown = lCyclicData.OxygenFlow_Nm3_min.Value * mDeltaT_min * MINP.Mm(28) / MINP.O2_Stechio(28);
            float lm_O2_blownC = 0;

            if (mRunningType == RunningType.Simulation)
            {
                if (mCurrentStateData.c_Kov[0] < Global.M3_SimulationWasteGas_C_kov_zlom)
                {
                    lm_O2_blownC = lCyclicData.WastegasFlow_Nm3_min.Value * mDeltaT_min * Global.M3_SimulationWasteGas_Podil_na_C * MINP.Mm(28) / MINP.O2_Stechio(28)
                        * mCurrentStateData.c_Kov[0] / Global.M3_SimulationWasteGas_C_kov_zlom;
                }
                else if (Data.Clock.Current.Duration > TimeSpan.FromMinutes(2))
                {
                    lm_O2_blownC = lCyclicData.WastegasFlow_Nm3_min.Value * mDeltaT_min * Global.M3_SimulationWasteGas_Podil_na_C * MINP.Mm(28) / MINP.O2_Stechio(28);
                }
            }
            else
            {
                float lCO2 = (float)lCyclicData.Wastegas_CO2_p.Value;

                #region CO2 buffer
                if (Global.M3_CO2_Buffer)
                {
                    if (mCO2Buffer > 0)
                    {
                        float lCO2Calc = (float)lCyclicData.WastegasFlow_Nm3_min.Value * mDeltaT_min * (float)lCyclicData.Wastegas_CO2_p.Value / 100
                            * MINP.Mm(44) / MINP.O2_Stechio(44) * Global.M3_V_Wastegas;

                        if (mCO2Buffer > lCO2Calc)
                        {
                            lCO2 = 0;
                            mCO2Buffer -= lCO2Calc;
                        }
                        else
                        {
                            lCO2 = lCO2 * lCO2Calc / mCO2Buffer;
                            mCO2Buffer = 0;
                        }
                    }
                }
                #endregion

                lm_O2_blownC = (float)(lCyclicData.WastegasFlow_Nm3_min.Value * mDeltaT_min
                    * (lCyclicData.Wastegas_CO_p.Value + lCO2) / 100
                    * (1 + Global.PostCombustion) * MINP.Mm(28) / MINP.O2_Stechio(0)) * Global.M3_V_Wastegas;

                if (lm_O2_blown <= lm_O2_blownC)
                {
                    mCurrentStateData.m_SlozkaKov[0] = mCurrentStateData.m_SlozkaKov[0] - lm_O2_blownC * MINP.Mm(0) / MINP.Mm(28) / (1 + Global.PostCombustion);
                    mCurrentStateData.m_SlozkaKov[32] -= (lm_O2_blown - lm_O2_blownC) * MINP.Mm(32) / MINP.Mm(28) * MINP.O2_Stechio(28) / MINP.O2_Stechio(32);
                }
            }

            if (lm_O2_blown > lm_O2_blownC)
            {
                #region Priprava pro rozdeleni kysliku
                float lm_O2_total = lm_O2_blown - lm_O2_blownC;

                Dictionary<Enumerations.M3ElementEnum, float> lDelta_G = new Dictionary<Enumerations.M3ElementEnum, float>();
                foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                {
                    Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                    lDelta_G[lEleIndex] = Global.M3_TZ_A[lEleIndex] + Global.M3_TZ_C[lEleIndex] * (mCurrentStateData.T_Tavby + Global.M3_K_273);
                }

                // R 50
                float lK_Fe =
                    (float)Math.Exp(
                        -1 * (Global.M3_TZ_A[Enumerations.M3ElementEnum.Fe] + Global.M3_TZ_C[Enumerations.M3ElementEnum.Fe] * (mCurrentStateData.T_Tavby + Global.M3_K_273))
                        / Global.M3_K_8314 / (mCurrentStateData.T_Tavby + Global.M3_K_273))
                    * Global.M3_KF[Enumerations.M3ElementEnum.Fe];
                Dictionary<Enumerations.M3ElementEnum, float> lK_Ele_rel = new Dictionary<Enumerations.M3ElementEnum, float>();

                foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                {
                    Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                    lK_Ele_rel[lEleIndex] = (float)Math.Exp(-1 * (Global.M3_TZ_A[lEleIndex] + Global.M3_TZ_C[lEleIndex] * (mCurrentStateData.T_Tavby + Global.M3_K_273))
                        / Global.M3_K_8314 / (mCurrentStateData.T_Tavby + Global.M3_K_273)) * Global.M3_KF[lEleIndex] / lK_Fe;

                    if (float.IsInfinity(lK_Ele_rel[lEleIndex])) lK_Ele_rel[lEleIndex] = float.MaxValue;
                }

                lK_Ele_rel[Enumerations.M3ElementEnum.Fe] = 1;

                // R 58
                Dictionary<Enumerations.M3ElementEnum, float> lK_Ele_rel_max = new Dictionary<Enumerations.M3ElementEnum, float>();
                foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                {
                    Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                    lK_Ele_rel_max[lEleIndex] = lK_Ele_rel[lEleIndex] < 0 ? 0 : lK_Ele_rel[lEleIndex] * (mCurrentStateData.c_Kov[nIndex] - mC_kov_min_p[lEleIndex]);
                }

                float lK_O = lK_Ele_rel_max[Enumerations.M3ElementEnum.Si]
                    + lK_Ele_rel_max[Enumerations.M3ElementEnum.Mn]
                    + lK_Ele_rel_max[Enumerations.M3ElementEnum.P]
                    + lK_Ele_rel_max[Enumerations.M3ElementEnum.Al]
                    + lK_Ele_rel_max[Enumerations.M3ElementEnum.Cr]
                    + lK_Ele_rel_max[Enumerations.M3ElementEnum.V]
                    + lK_Ele_rel_max[Enumerations.M3ElementEnum.Ti]
                    + lK_Ele_rel[Enumerations.M3ElementEnum.Fe] * mCurrentStateData.c_Kov[32];

                // R 59
                Dictionary<Enumerations.M3ElementEnum, float> lO_for_Ele_wo_lim = new Dictionary<Enumerations.M3ElementEnum, float>();
                foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                {
                    Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                    lO_for_Ele_wo_lim[lEleIndex] = lK_Ele_rel[lEleIndex] / lK_O * (mCurrentStateData.c_Kov[nIndex] - mC_kov_min_p[lEleIndex]) * lm_O2_total;
                }
                lO_for_Ele_wo_lim[Enumerations.M3ElementEnum.Fe] = lK_Ele_rel[Enumerations.M3ElementEnum.Fe] / lK_O * mCurrentStateData.c_Kov[32] * lm_O2_total;

                // R 68
                float l_O_total = lO_for_Ele_wo_lim.Sum(aR => aR.Value);

                // R 69
                Dictionary<Enumerations.M3ElementEnum, float> lO_for_Ele_max = new Dictionary<Enumerations.M3ElementEnum, float>();
                foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                {
                    Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                    lO_for_Ele_max[lEleIndex] = mCurrentStateData.m_Kov * (mCurrentStateData.c_Kov[nIndex] - mC_kov_min_p[lEleIndex]) * MINP.Mm(28) / MINP.Mm(nIndex) * MINP.O2_Stechio(nIndex) / MINP.O2_Stechio(28);
                }
                #endregion

                #region 5.2.2 Mozne rozdeleni kysliku mezi jednotlive prvky
                // R 76
                mCurrentStateData.m_SlozkaKov[0] = mCurrentStateData.m_SlozkaKov[0] - lm_O2_blownC * MINP.Mm(0) / MINP.Mm(28) / (1 + Global.PostCombustion);
                float lPrebytek_O2_Last = 0;

                while (true)
                {
                    float lPrebytek_O2 = 0;

                    foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                    {
                        Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                        if (lEleIndex == Enumerations.M3ElementEnum.Fe) continue;

                        if (lO_for_Ele_wo_lim[lEleIndex] >= lO_for_Ele_max[lEleIndex])
                        {
                            mCurrentStateData.c_Kov[nIndex] = mC_kov_min_p[lEleIndex];
                            lPrebytek_O2 += lO_for_Ele_wo_lim[lEleIndex] - lO_for_Ele_max[lEleIndex];
                            mCurrentStateData.m_SlozkaKov[nIndex] = mCurrentStateData.c_Kov[nIndex] * mCurrentStateData.m_Kov;
                        }
                        else
                        {
                            mCurrentStateData.m_SlozkaKov[nIndex] -= lO_for_Ele_wo_lim[lEleIndex] * MINP.Mm(nIndex) / MINP.Mm(28) * MINP.O2_Stechio(28) / MINP.O2_Stechio(nIndex);
                        }

                    }
                    mCurrentStateData.m_SlozkaKov[32] -= lO_for_Ele_wo_lim[Enumerations.M3ElementEnum.Fe] * MINP.Mm(32) / MINP.Mm(28) * MINP.O2_Stechio(28) / MINP.O2_Stechio(32);

                    if (lPrebytek_O2 <= 0) break;
                    if (lPrebytek_O2 == lPrebytek_O2_Last) break;
                    lPrebytek_O2_Last = lPrebytek_O2;

                    // pokud prebytek pocitam znovu
                    // ****************************

                    // prepocet c_kov
                    float lSuma_Kov = mCurrentStateData.m_SlozkaKov.Sum();

                    for (int i = 0; i < Global.MATERIALELEMENTS_STEEL_COUNT; i++)
                    {
                        mCurrentStateData.c_Kov[i] = mCurrentStateData.m_SlozkaKov[i] / lSuma_Kov;
                    }

                    // R 58
                    foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                    {
                        Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                        lK_Ele_rel_max[lEleIndex] = lK_Ele_rel[lEleIndex] < 0 ? 0 : lK_Ele_rel[lEleIndex] * (mCurrentStateData.c_Kov[nIndex] - mC_kov_min_p[lEleIndex]);
                    }

                    lK_O = lK_Ele_rel_max[Enumerations.M3ElementEnum.Si]
                        + lK_Ele_rel_max[Enumerations.M3ElementEnum.Mn]
                        + lK_Ele_rel_max[Enumerations.M3ElementEnum.P]
                        + lK_Ele_rel_max[Enumerations.M3ElementEnum.Al]
                        + lK_Ele_rel_max[Enumerations.M3ElementEnum.Cr]
                        + lK_Ele_rel_max[Enumerations.M3ElementEnum.V]
                        + lK_Ele_rel_max[Enumerations.M3ElementEnum.Ti]
                        + lK_Ele_rel[Enumerations.M3ElementEnum.Fe] * mCurrentStateData.c_Kov[32];

                    // R 59
                    foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                    {
                        Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                        lO_for_Ele_wo_lim[lEleIndex] = lK_Ele_rel[lEleIndex] / lK_O * (mCurrentStateData.c_Kov[nIndex] - mC_kov_min_p[lEleIndex]) * lPrebytek_O2;
                    }
                    lO_for_Ele_wo_lim[Enumerations.M3ElementEnum.Fe] = lK_Ele_rel[Enumerations.M3ElementEnum.Fe] / lK_O * mCurrentStateData.c_Kov[32] * lPrebytek_O2;

                    // R 68
                    l_O_total = lO_for_Ele_wo_lim.Sum(aR => aR.Value);

                    // R 69
                    lO_for_Ele_max = new Dictionary<Enumerations.M3ElementEnum, float>();
                    foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                    {
                        Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                        lO_for_Ele_max[lEleIndex] = mCurrentStateData.m_Kov * (mCurrentStateData.c_Kov[nIndex] - mC_kov_min_p[lEleIndex]) * MINP.Mm(28) / MINP.Mm(nIndex) * MINP.O2_Stechio(nIndex) / MINP.O2_Stechio(28);
                    }
                }
                #endregion
                // Kyslik spotrebovan
            }

            // R 104
            for (int i = 0; i < Global.MATERIALELEMENTS_STEEL_COUNT; i++)
            {
                mCurrentStateData.FP_Kov[i] = mCurrentStateData.c_Kov[i] * MINP.ConversionVector(i);
            }

            // R 104.. R 113
            float lm_Delta_C = mCurrentStateData.m_SlozkaKov[0] - lm_C_Start;
            Dictionary<Enumerations.M3ElementEnum, float> lm_Delta_Ele = new Dictionary<Enumerations.M3ElementEnum, float>();
            foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
            {
                Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                lm_Delta_Ele[lEleIndex] = mCurrentStateData.m_SlozkaKov[nIndex] - lm_Ele_Start[lEleIndex];
            }

            // redukce pouze zeleza
            if (lm_O2_blown <= lm_O2_blownC)
            {
                foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                {
                    Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                    if (lEleIndex == Enumerations.M3ElementEnum.Fe) continue;
                    lm_Delta_Ele[lEleIndex] = 0;
                }
            }

            // R 114
            mCurrentStateData.m_Kov += lm_Delta_C;

            foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
            {
                Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                mCurrentStateData.m_Kov += lm_Delta_Ele[lEleIndex];
            }

            // R 115
            mCurrentStateData.m_Struska = mCurrentStateData.m_Struska
                - (lm_Delta_Ele[Enumerations.M3ElementEnum.Si] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Si) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Si))) * MINP.Mm(51) / MINP.Mm(1)
                - (lm_Delta_Ele[Enumerations.M3ElementEnum.Cr] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Cr) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Cr))) * MINP.Mm(52) / MINP.Mm(7) / 2
                - (lm_Delta_Ele[Enumerations.M3ElementEnum.Mn] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Mn) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Mn))) * MINP.Mm(53) / MINP.Mm(2)
                - (lm_Delta_Ele[Enumerations.M3ElementEnum.P] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.P) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.P))) * MINP.Mm(55) / MINP.Mm(3) / 2
                - (lm_Delta_Ele[Enumerations.M3ElementEnum.Ti] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Ti) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Ti))) * MINP.Mm(57) / MINP.Mm(11)
                - (lm_Delta_Ele[Enumerations.M3ElementEnum.Al] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Al) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Al))) * MINP.Mm(62) / MINP.Mm(5) / 2
                - (lm_Delta_Ele[Enumerations.M3ElementEnum.Fe] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Fe) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Fe))) * MINP.Mm(61) / MINP.Mm(32);


            // odprasky
            float lDeltam_Odprasky = Global.M_Odprasky / Global.TauTavby * mDeltaT_min;
            for (int j = 0; j < Global.MATERIALELEMENTS_COUNT; j++)
            {
                mCurrentStateData.FP_Tavby[j] = (mCurrentStateData.m_Tavby * mCurrentStateData.FP_Tavby[j] - lDeltam_Odprasky * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], j))
                    / (mCurrentStateData.m_Tavby + lDeltam_Odprasky);
            }

            // R 118
            mCurrentStateData.m_Tavby = mCurrentStateData.m_Kov + mCurrentStateData.m_Struska - lDeltam_Odprasky;

            // slozky tavby
            // R 117 .. R 124
            mCurrentStateData.m_SlozkaTavby[0] += lm_Delta_C;
            foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
            {
                Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                mCurrentStateData.m_SlozkaTavby[nIndex] += lm_Delta_Ele[lEleIndex];
            }

            // R 125 .. R 131
            mCurrentStateData.m_SlozkaTavby[51] -= (lm_Delta_Ele[Enumerations.M3ElementEnum.Si] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Si) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Si))) * MINP.Mm(51) / MINP.Mm(1);
            mCurrentStateData.m_SlozkaTavby[52] -= (lm_Delta_Ele[Enumerations.M3ElementEnum.Cr] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Cr) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Cr))) * MINP.Mm(52) / MINP.Mm(7) / 2;
            mCurrentStateData.m_SlozkaTavby[53] -= (lm_Delta_Ele[Enumerations.M3ElementEnum.Mn] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Mn) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Mn))) * MINP.Mm(53) / MINP.Mm(2);
            mCurrentStateData.m_SlozkaTavby[55] -= (lm_Delta_Ele[Enumerations.M3ElementEnum.P] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.P) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.P))) * MINP.Mm(55) / MINP.Mm(3) / 2;
            mCurrentStateData.m_SlozkaTavby[57] -= (lm_Delta_Ele[Enumerations.M3ElementEnum.Ti] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Ti) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Ti))) * MINP.Mm(57) / MINP.Mm(11);
            mCurrentStateData.m_SlozkaTavby[62] -= (lm_Delta_Ele[Enumerations.M3ElementEnum.Al] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Al) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Al))) * MINP.Mm(62) / MINP.Mm(5) / 2;
            mCurrentStateData.m_SlozkaTavby[61] -= (lm_Delta_Ele[Enumerations.M3ElementEnum.Fe] + (lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], (int)Enumerations.M3ElementEnum.Fe) / MINP.ConversionVector((int)Enumerations.M3ElementEnum.Fe))) * MINP.Mm(61) / MINP.Mm(32);

            if (mCurrentStateData.m_SlozkaTavby[61] < 0)
            {
                mCurrentStateData.m_SlozkaTavby[61] = 0;
            }

            // R 133    // +- * Conv
            for (int i = 0; i < Global.MATERIALELEMENTS_STEELANDSLAG_COUNT; i++)
            {
                mCurrentStateData.FP_Tavby[i] = mCurrentStateData.m_SlozkaTavby[i] / mCurrentStateData.m_Tavby * MINP.ConversionVector(i);
            }

            // TEPLOTA
            // *********************
            // R 130
            mCurrentStateData.FP_Tavby[70] =
                (MINP.FP(MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Steel], 70) * mCurrentStateData.m_Kov
                + MINP.FP(MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Slag], 70) * mCurrentStateData.m_Struska)
                / mCurrentStateData.m_Tavby;
            // R 131
            mCurrentStateData.FP_Tavby[71] =
                (MINP.FP(MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Steel], 71) * mCurrentStateData.m_Kov
                + MINP.FP(MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Slag], 71) * mCurrentStateData.m_Struska)
                / mCurrentStateData.m_Tavby;

            // R 132
            mCurrentStateData.E_C = (lm_Delta_C + lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], 0) / MINP.ConversionVector(0))
                * (MINP.E_Ox1(0) * MINP.Eta_Ox1(0) + Global.PostCombustion * MINP.E_Ox2(0) * MINP.Eta_Ox2(0));
            mCurrentStateData.E_Tavby -= mCurrentStateData.E_C;

            foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
            {
                Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;

                if (lEleIndex == Enumerations.M3ElementEnum.Fe)
                {
                    mCurrentStateData.E_Elements[Enumerations.M3ElementEnum.Fe] =
                        (lm_Delta_Ele[lEleIndex] +
                            lm_Odprasky_krok
                            * (
                                MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], 32)
                                + MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], 60)
                              ) / MINP.ConversionVector(nIndex)
                          )
                        * MINP.E_Ox1(nIndex) * MINP.Eta_Ox1(nIndex);
                }
                else
                {
                    mCurrentStateData.E_Elements[lEleIndex] =
                        (lm_Delta_Ele[lEleIndex] - lm_Odprasky_krok * MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], nIndex) / MINP.ConversionVector(nIndex))
                        * MINP.E_Ox1(nIndex) * MINP.Eta_Ox1(nIndex);
                }

                mCurrentStateData.E_Tavby -= mCurrentStateData.E_Elements[lEleIndex];
            }

            float lZtratovyVykonAkt = 0;

            if (Global.M3_Stat_T_ON)
            {
                float lTDiff = mCurrentStateData.T_Tavby - Global.M3_Stat_T_korekce;
                float lTAdapt =
                    Global.M3_Stat_Ztratovy_vykon_lin * lTDiff
                    + Global.M3_Stat_Ztratovy_vykon_kvad * lTDiff * lTDiff
                    + Global.M3_Stat_Ztratovy_vykon_kub * lTDiff * lTDiff * lTDiff;
                if (lTAdapt < 0) lTAdapt = 0;

                lZtratovyVykonAkt = lTAdapt;
            }

            mCurrentStateData.E_Tavby -= (Global.H_Akumulace + Global.TauTavby * Global.ZtratovyVykon / 60) / (Global.TauTavby * 60 / mDeltaT_s);
            mCurrentStateData.E_Tavby -= lZtratovyVykonAkt;

            mCurrentStateData.E_Tavby += (lm_Delta_Ele[Enumerations.M3ElementEnum.Fe] + lm_Odprasky_krok) * (MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], 71) / MINP.ConversionVector(71)
                * (Data.MINP.HeatAimData.FinalTemperature - MINP.FP(MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Steel], 72))
                + MINP.FP(Data.MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Odprasky], 70) / MINP.ConversionVector(70));

            // R 133 .. R 135   // +- kg -> t
            float lCalc = mCurrentStateData.E_Tavby / mCurrentStateData.m_Tavby * 1000;
            if (mCurrentStateData.FP_Tavby[70] > lCalc) // kg -> t
                mCurrentStateData.T_Tavby = mCurrentStateData.E_Tavby / mCurrentStateData.m_Tavby * 1000 / mCurrentStateData.FP_Tavby[70] * mCurrentStateData.FP_Tavby[72];
            else if (mCurrentStateData.FP_Tavby[70] == lCalc)
                mCurrentStateData.T_Tavby = MINP.FP(MINP.MINP_GD_ModelMaterials[Enumerations.MINP_GD_Material_ModelMaterial.Steel], 72);
            else if (mCurrentStateData.FP_Tavby[70] < lCalc)
                mCurrentStateData.T_Tavby = mCurrentStateData.FP_Tavby[72]
                    + (mCurrentStateData.E_Tavby / mCurrentStateData.m_Tavby * 1000 - mCurrentStateData.FP_Tavby[70]) / mCurrentStateData.FP_Tavby[71];

            // steel and slag amount recalculate
            RecalculateKovStruskaFromTavba();

            // copy to mCurrentOutputData
            mCurrentOutputData = ToDynamicOutput(mCurrentStateData);
            mCurrentOutputData.StartTime = Data.Clock.Current.StartTime;
            mCurrentOutputData.Duration = Data.Clock.Current.Duration;
            mCurrentOutputData.ActualTime = Data.Clock.Current.ActualTime;

            #region Output file
            if (Global.M3_GenerateOutputFile)
            {
                try
                {
                    DateTime lActualTime = Data.Clock.Current.ActualTime;
                    mCSVOutput.Append(mStepsCount);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lActualTime.Date);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lActualTime.TimeOfDay);
                    mCSVOutput.Append(';'); mCSVOutput.Append(Data.Clock.Current.Duration);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.OxygenConsumption_m3 > 0 ? lCyclicData.OxygenConsumption_m3.ToString() : "");
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.OxygenFlow_Nm3_min);
                    mCSVOutput.Append(';'); mCSVOutput.Append(""); // m_SŽ
                    mCSVOutput.Append(';'); mCSVOutput.Append("");
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCSVCoke != 0 ? mCSVCoke.ToString() : ""); // coke
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCSVLime != 0 ? mCSVLime.ToString() : ""); // lime
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCSVDolom != 0 ? mCSVDolom.ToString() : ""); // dolomit
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCSVFOM != 0 ? mCSVFOM.ToString() : ""); // fom
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCSVS1S2 != 0 ? mCSVS1S2.ToString() : ""); // s1, s2
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.WastegasFlow_Nm3_min);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.Wastegas_T_C);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.Wastegas_CO_p);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.Wastegas_CO2_p);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.Wastegas_O2_p);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.Wastegas_H2_p);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.Wastegas_N2_p);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lCyclicData.Wastegas_Ar_p);
                    mCSVOutput.Append(';'); mCSVOutput.Append("");    // T mer
                    mCSVOutput.Append(';'); mCSVOutput.Append("");    // C mer
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.T_Tavby);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_Tavby);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_Kov);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.m_Struska);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[0]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[1]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[2]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[3]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[7]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[10]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[11]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[5]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaKov[32]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaStruska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaStruska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaStruska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaStruska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaStruska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.m_SlozkaStruska[55 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.E_Tavby);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.E_C);
                    foreach (int nIndex in Enum.GetValues(typeof(Enumerations.M3ElementEnum)))
                    {
                        Enumerations.M3ElementEnum lEleIndex = (Enumerations.M3ElementEnum)nIndex;
                        mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentStateData.E_Elements[lEleIndex]);
                    }
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[0]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[1]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[2]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[3]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[7]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[10]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[11]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[5]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Kov[32]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Struska[63 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Struska[53 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Struska[61 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Struska[55 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCurrentOutputData.FP_Struska[50 - Global.MATERIALELEMENTS_SLAG_STARTINDEX] / mCurrentOutputData.FP_Struska[51 - Global.MATERIALELEMENTS_SLAG_STARTINDEX]);
                    double lDeCSpaliny = (lCyclicData.Wastegas_CO_p.HasValue && lCyclicData.Wastegas_CO2_p.HasValue && lCyclicData.WastegasFlow_Nm3_min.HasValue) ? (lCyclicData.Wastegas_CO_p.Value + lCyclicData.Wastegas_CO2_p.Value) / 100f * lCyclicData.WastegasFlow_Nm3_min.Value * 60 * 24 * 12 / 22.4 * mDeltaT_s / 24 / 60 : 0;
                    mCSVOutput.Append(';'); mCSVOutput.Append(lDeCSpaliny);
                    mCSVOutput.Append(';'); mCSVOutput.Append(lm_Delta_C / mDeltaT_s / 24 / 60);
                    if (Data.MINP.MINP_Cyclic.Count > 1)
                    {
                        DTO.MINP_CyclicDTO lCyclicDataPrevious = Data.MINP.MINP_Cyclic.OrderByDescending(aR => aR.C__Created).Skip(1).First();
                        mCSVOutput.Append(';'); mCSVOutput.Append(lDeCSpaliny / (lCyclicData.OxygenConsumption_m3 - lCyclicDataPrevious.OxygenConsumption_m3) / 11.2 * 12 * (1 + Global.PostCombustion) / 1000000);
                    }
                    else
                    {
                        mCSVOutput.Append(';'); mCSVOutput.Append(0);
                    }
                    mCSVOutput.Append(';'); mCSVOutput.Append(mCO2Buffer);
                    mCSVOutput.AppendLine();
                }
                catch { }
            }
            #endregion

            return mCurrentOutputData;
        }
        private Data.Model.DynamicOutput ToDynamicOutput(Data.Model.DynamicState aCurrentData)
        {
            Data.Model.DynamicOutput lResult = new Data.Model.DynamicOutput();
            lResult.E_Tavby = aCurrentData.E_Tavby;

            if (LastOutputData != null)
            {
                lResult.E_C_oxidace = LastOutputData.E_C_oxidace + aCurrentData.E_C;
                lResult.E_Si_oxidace = LastOutputData.E_Si_oxidace + aCurrentData.E_Elements[Enumerations.M3ElementEnum.Si];
                lResult.E_Mn_oxidace = LastOutputData.E_Mn_oxidace + aCurrentData.E_Elements[Enumerations.M3ElementEnum.Mn];
                lResult.E_Al_oxidace = LastOutputData.E_Al_oxidace + aCurrentData.E_Elements[Enumerations.M3ElementEnum.Al];
                lResult.E_Fe_oxidace = LastOutputData.E_Fe_oxidace + aCurrentData.E_Elements[Enumerations.M3ElementEnum.Fe];
            }
            else
            {
                lResult.E_C_oxidace = aCurrentData.E_C;
                lResult.E_Si_oxidace = aCurrentData.E_Elements[Enumerations.M3ElementEnum.Si];
                lResult.E_Mn_oxidace = aCurrentData.E_Elements[Enumerations.M3ElementEnum.Mn];
                lResult.E_Al_oxidace = aCurrentData.E_Elements[Enumerations.M3ElementEnum.Al];
                lResult.E_Fe_oxidace = aCurrentData.E_Elements[Enumerations.M3ElementEnum.Fe];
            }

            lResult.m_Tavby = aCurrentData.m_Tavby;
            lResult.E_Tavby = aCurrentData.E_Tavby;
            lResult.T_Tavby = aCurrentData.T_Tavby;
            lResult.FP_C = aCurrentData.FP_C;
            lResult.FP_Tavby = new float[aCurrentData.FP_Tavby.Length];
            Array.Copy(aCurrentData.FP_Tavby, lResult.FP_Tavby, aCurrentData.FP_Tavby.Length);
            lResult.m_SlozkaTavby = new float[aCurrentData.m_SlozkaTavby.Length];
            Array.Copy(aCurrentData.m_SlozkaTavby, lResult.m_SlozkaTavby, aCurrentData.m_SlozkaTavby.Length);

            lResult.m_Struska = aCurrentData.m_Struska;
            lResult.m_Kov = aCurrentData.m_Kov;

            lResult.FP_Struska = new float[aCurrentData.FP_Struska.Length];
            Array.Copy(aCurrentData.FP_Struska, lResult.FP_Struska, aCurrentData.FP_Struska.Length);
            lResult.FP_Kov = new float[aCurrentData.FP_Kov.Length];
            Array.Copy(aCurrentData.FP_Kov, lResult.FP_Kov, aCurrentData.FP_Kov.Length);
            lResult.m_SlozkaStruska = new float[aCurrentData.m_SlozkaStruska.Length];
            Array.Copy(aCurrentData.m_SlozkaStruska, lResult.m_SlozkaStruska, aCurrentData.m_SlozkaStruska.Length);
            lResult.m_SlozkaKov = new float[aCurrentData.m_SlozkaKov.Length];
            Array.Copy(aCurrentData.m_SlozkaKov, lResult.m_SlozkaKov, aCurrentData.m_SlozkaKov.Length);
            lResult.c_Struska = new float[aCurrentData.c_Struska.Length];
            Array.Copy(aCurrentData.c_Struska, lResult.c_Struska, aCurrentData.c_Struska.Length);
            lResult.c_Kov = new float[aCurrentData.c_Kov.Length];
            Array.Copy(aCurrentData.c_Kov, lResult.c_Kov, aCurrentData.c_Kov.Length);

            return lResult;
        }
        private void RecalculateKovStruskaFromTavba()
        {
            mCurrentStateData.FP_C = mCurrentStateData.FP_Kov[0];

            #region m, c - kov, struska R 16 .. R 25
            mCurrentStateData.m_Struska = 0;
            for (int i = 0; i < Global.MATERIALELEMENTS_SLAG_COUNT; i++)
            {
                int lIndex = i + Global.MATERIALELEMENTS_SLAG_STARTINDEX;
                mCurrentStateData.m_Struska += mCurrentStateData.m_Tavby * (mCurrentStateData.FP_Tavby[lIndex] / MINP.ConversionVector(lIndex));
            }
            mCurrentStateData.m_Kov = 0;
            for (int i = 0; i < Global.MATERIALELEMENTS_STEEL_COUNT; i++)
            {
                mCurrentStateData.m_Kov += mCurrentStateData.m_Tavby * (mCurrentStateData.FP_Tavby[i] / MINP.ConversionVector(i));
            }

            for (int i = 0; i < Global.MATERIALELEMENTS_SLAG_COUNT; i++)
            {
                int lIndex = i + Global.MATERIALELEMENTS_SLAG_STARTINDEX;
                mCurrentStateData.FP_Struska[i] = mCurrentStateData.FP_Tavby[lIndex] * mCurrentStateData.m_Tavby / mCurrentStateData.m_Struska;
                mCurrentStateData.m_SlozkaStruska[i] = mCurrentStateData.FP_Struska[i] * mCurrentStateData.m_Struska / MINP.ConversionVector(lIndex);
                mCurrentStateData.c_Struska[i] = mCurrentStateData.FP_Struska[i] / MINP.ConversionVector(lIndex);
            }
            for (int i = 0; i < Global.MATERIALELEMENTS_STEEL_COUNT; i++)
            {
                mCurrentStateData.FP_Kov[i] = mCurrentStateData.FP_Tavby[i] * mCurrentStateData.m_Tavby / mCurrentStateData.m_Kov;
                mCurrentStateData.m_SlozkaKov[i] = mCurrentStateData.FP_Kov[i] * mCurrentStateData.m_Kov / MINP.ConversionVector(i);
                mCurrentStateData.c_Kov[i] = mCurrentStateData.FP_Kov[i] / MINP.ConversionVector(i);
            }
            #endregion
        }

        /// <summary>
        /// Public for simulation from imported data only to run correction on temperature measurement time.
        /// </summary>
        private void Run_C_Correction()
        {
            float lC_ConversionVector = MINP.ConversionVector(0);
            // C correction from the beginning R 6-10 .. 6-12
            lock (mOutputData)
            {
                foreach (var nItem in mOutputData.OrderBy(aR => aR.Key))
                {
                    nItem.Value.FP_Kov[0] =
                        nItem.Value.FP_C +
                        (nItem.Value.FP_C - mC_kov_start)
                        * ((mC_kov_start - mC_kov_end) / (mC_kov_start - LastOutputData.FP_C) - 1);
                    nItem.Value.c_Kov[0] = nItem.Value.FP_Kov[0] / lC_ConversionVector;
                    nItem.Value.m_SlozkaKov[0] = nItem.Value.FP_Kov[0] * nItem.Value.m_Kov;
                }
            }
        }
        public void RealTimeSimulation_TemperatureMeasured()
        {
            mCurrentStateData.FP_Kov[0] = mC_kov_end;
            mCurrentStateData.m_SlozkaKov[0] = mC_kov_end * mCurrentStateData.m_Kov / MINP.ConversionVector(0);
            mCurrentStateData.FP_Tavby[0] = mCurrentStateData.FP_Kov[0] * mCurrentStateData.m_Kov / mCurrentStateData.m_Tavby;
            mCurrentStateData.m_SlozkaTavby[0] = mCurrentStateData.FP_Tavby[0] * mCurrentStateData.m_Kov / MINP.ConversionVector(0);
            LastOutputData.FP_Kov[0] = mC_kov_end;
            LastOutputData.m_SlozkaKov[0] = mC_kov_end * LastOutputData.m_Kov / MINP.ConversionVector(0);
            LastOutputData.FP_Tavby[0] = LastOutputData.FP_Kov[0] * LastOutputData.m_Kov / LastOutputData.m_Tavby;
            LastOutputData.m_SlozkaTavby[0] = LastOutputData.FP_Tavby[0] * LastOutputData.m_Kov / MINP.ConversionVector(0);

            Run_C_Correction();
        }

        private void SwitchToNextPhase()
        {
            // only oxygen blowing or material addition phases are allowed
            if (!(mCurrentPhase is Data.PhaseItemOxygenBlowing) && !(mCurrentPhase is Data.PhaseItemMatAdd))
                throw new ApplicationException("Only oxygen blowing or material addition phases are allowed in SwitchPhase method.");

            mCurrentPhase = mCurrentPhase.NextPhase;
            if (PhaseChanged != null) PhaseChanged(this, new Data.EventArgs.CurrentPhaseChangedEventArgs(mCurrentPhase.PreviousPhase, mCurrentPhase, mCurrentPhase));
        }
        private void SwitchPhaseToL1TemperatureMeasurement()
        {
            mCurrentPhaseState = ModelPhaseState.S25_Waiting4TemperatureMeasurement;
            mCurrentPhase = mInputData.OxygenBlowingPhases.Single(aR => aR is Data.PhaseItemL1Command && ((Data.PhaseItemL1Command)aR).L1Command == Enumerations.L2L1_Command.TemperatureMeasurement);
            if (PhaseChanged != null) PhaseChanged(this, new Data.EventArgs.CurrentPhaseChangedEventArgs(mCurrentPhase.PreviousPhase, mCurrentPhase, mCurrentPhase));
        }
        private void SwitchPhaseToO2Correction()
        {
            mCurrentPhaseState = ModelPhaseState.S30_Correction;
            mCurrentPhase = mCorrectionOxygenBlowingPhase;
            if (PhaseChanged != null) PhaseChanged(this, new Data.EventArgs.CurrentPhaseChangedEventArgs(mCurrentPhase.PreviousPhase, mCurrentPhase, mCurrentPhase));
        }
        public void SwitchPhaseToL1OxygenLanceParking()
        {
            mCurrentPhaseState = ModelPhaseState.S45_Waiting4LanceParking;
            mCurrentPhase = mInputData.OxygenBlowingPhases.Single(aR => aR is Data.PhaseItemL1Command && ((Data.PhaseItemL1Command)aR).L1Command == Enumerations.L2L1_Command.OxygenLanceToParkingPosition);
            if (PhaseChanged != null) PhaseChanged(this, new Data.EventArgs.CurrentPhaseChangedEventArgs(mCurrentPhase.PreviousPhase, mCurrentPhase, mCurrentPhase));
        }

        private void SaveData2CSV()
        {
            if (Global.M3_GenerateOutputFile && mCSVOutput != null)
            {
                try
                {
                    if (!Directory.Exists(Global.M3_GenerateOutputFileDirectory)) Directory.CreateDirectory(Global.M3_GenerateOutputFileDirectory);
                    try
                    {
                        if (!File.Exists(Path.Combine(Global.M3_GenerateOutputFileDirectory, "Template.xlsx")))
                            File.Copy("Template.xlsx", Path.Combine(Global.M3_GenerateOutputFileDirectory, "Template.xlsx"));
                    }
                    catch { }
                    
                    using (StreamWriter lOutputCsvFile = new StreamWriter(Path.Combine(Global.M3_GenerateOutputFileDirectory, String.Format("{1}_{0:yyyy_MM_dd HH_mm_ss}.csv", DateTime.Now, mHeatNumber))))
                    {
                        lOutputCsvFile.WriteLine("Krok;Datum;Cas;Doba tavby;O2 [m3];Vyska [cm];O2 intenzita [m3/min];m_SZ;m_SROT;m_KOKS;m_LIME;m_DOLOMIT;m_FOM;m_CaCO3;PRUTOK SPALIN;teplota spalin;CO;CO2;O2;H2;N2;Ar;T_MER;%C_MER;T;m_T;m_k;m_s;C;Si;Mn;P;Cr;V;Ti;Al;Fe;CaO;SiO2;MgO;MnO;FeO;P2O5;E_Tavby;E_C;E_Si;E_Mn;E_P;E_Al;E_Cr;E_V;E_Ti;E_Fe;C;Si;Mn;P;Cr;V;Ti;Al;Fe;CaO;SiO2;MgO;MnO;FeO;P2O5;B;rychlost deC spaliny;rychlost deC tavenina;Vyuziti O2 na deC;CO2 Buffer;m C Corr;% C Corr");

                        // extend StringBuilder about C_Corr statistical data
                        int lLastIndex = mCSVOutput.ToString().IndexOf(Environment.NewLine) + 1;

                        foreach (var nItem in mOutputData.OrderBy(aR => aR.Key))
                        {
                            string lAppendCorr = String.Format(";{0};{1}", nItem.Value.m_SlozkaKov[0], nItem.Value.FP_Kov[0]);
                            string lContent = mCSVOutput.ToString();
                            int lIndex = lContent.IndexOf(Environment.NewLine, lLastIndex);
                            if (lIndex >= 0)
                            {
                                mCSVOutput = mCSVOutput.Insert(lIndex, lAppendCorr);
                                lLastIndex = lIndex + lAppendCorr.Length + 1;
                            }
                        }

                        lOutputCsvFile.WriteLine(mCSVOutput);
                    }
                }
                catch { }
            }
        }

        // private members
        private Data.Model.DynamicInput mInputData;
        private Dictionary<DateTime, Data.Model.DynamicOutput> mOutputData;

        private Data.Model.DynamicState mCurrentStateData;
        private Data.Model.DynamicOutput mCurrentOutputData;

        public RunningType mRunningType;
        private Data.Clock mClock;
        private int mStepsCount;
        private int mDeltaT_s;
        private float mDeltaT_min;
        private System.Threading.Timer mTimer;
        private bool mPaused;

        private ModelPhaseState mCurrentPhaseState;
        private Data.PhaseItem mCurrentPhase;
        private Data.PhaseItemOxygenBlowing mLastMainOxygenBlowingPhase;
        private Data.PhaseItemOxygenBlowing mCorrectionOxygenBlowingPhase;

        private Queue<object> mRequestQueue;

        private Dictionary<Enumerations.M3ElementEnum, float> mC_kov_min_p;
        private float mT_SZ;
        private float mT_Other;
        private int mCurrentO2Amount;
        private int mFinalOxygenAmount;
        private int mCorrectionOxygenAmount;

        private float mSondaRemaining_s;
        private float mC_kov_end;
        private float mC_kov_start;
        private float mCO2Buffer;

        private int mCSVCoke;
        private int mCSVLime;
        private int mCSVDolom;
        private int mCSVFOM;
        private int mCSVS1S2;

        private string mHeatNumber;
        private StringBuilder mCSVOutput;
        private bool mRecalculateFromTheBeginning;

        // CHEREPOVETS ADDITIONS
        public ModelPhaseState State()
        {
            return mCurrentPhaseState;
        }
    }
}
