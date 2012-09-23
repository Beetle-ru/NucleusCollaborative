using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    /// <summary>
    /// Basic phase division into 5 parts.
    /// Part 1 is not used in operator application. It is covered by Batch preparation.
    /// </summary>
    public enum PhasePrimaryDivision : int
    {
        BatchPreparation = 1,
        Charging = 2,
        OxygenBlowing = 3,
        OxygenBlowingCorrection = 4,
        Tapping = 5,
        HeatRelease = 6
    }

    /// <summary>
    /// Phase definition.
    /// </summary>
    public abstract class PhaseItem
    {
        /// <summary>
        /// Phase name.
        /// </summary>
        public int PhaseNumber { get; set; }
        public string PhaseName { get; set; }
        public PhasePrimaryDivision PhaseGroup { get; set; }
        public bool OperatorVisible { get; set; }
        public bool OperatorConfirmation { get; set; }
        public string OperatorConfirmationText { get; set; }
        public bool CanSkip { get; set; }
        public PhaseItem NextPhase { get; set; }
        public PhaseItem PreviousPhase { get; set; }
        
        public virtual TimeSpan SimulationDuration
        {
            get
            {
                return TimeSpan.Zero;
            }
        }
        public virtual TimeSpan SimulationTotalDuration
        {
            get
            {
                if (PreviousPhase == null) return SimulationDuration;
                return PreviousPhase.SimulationTotalDuration + SimulationDuration;
            }
        }

        public PhaseItemOxygenBlowing PreviousOxygenBlowingPhase
        {
            get
            {
                if (this.PreviousPhase == null) return null;
                if (this.PreviousPhase is PhaseItemOxygenBlowing) return (PhaseItemOxygenBlowing)this.PreviousPhase;
                return this.PreviousPhase.PreviousOxygenBlowingPhase;
            }
        }
    }

    /// <summary>
    /// Phases for oxygen blowing used in PhasePrimaryDivision:
    /// - OxygenBlowing
    /// - OxygenBlowingCorrection
    /// O2Amount_Nm3 - Oxygen amount when phase become active.
    /// O2Flow_Nm3_min - new setpoint for oxygen flow.
    /// LanceDistance_mm - new setpoint for oxygen lance.
    /// </summary>
    public class PhaseItemOxygenBlowing : PhaseItem
    {
        public int? O2Amount_Nm3 { get; set; }
        public int O2Flow_Nm3_min { get; set; }
        public int LanceDistance_mm { get; set; }
        
        public override TimeSpan SimulationDuration
        {
            get
            {
                Data.PhaseItemOxygenBlowing lPreviousOxygenBlowingPhase = PreviousOxygenBlowingPhase;
                if (lPreviousOxygenBlowingPhase == null)
                {
                    if (O2Amount_Nm3.HasValue) return TimeSpan.FromMinutes((float)O2Amount_Nm3.Value / O2Flow_Nm3_min);
                    return TimeSpan.Zero;
                }

                // only with amount
                while (!lPreviousOxygenBlowingPhase.O2Amount_Nm3.HasValue)
                {
                    lPreviousOxygenBlowingPhase = lPreviousOxygenBlowingPhase.PreviousOxygenBlowingPhase;
                    if (lPreviousOxygenBlowingPhase == null) return TimeSpan.Zero;
                }

                if (!O2Amount_Nm3.HasValue) return TimeSpan.Zero;

                return TimeSpan.FromMinutes((float)(O2Amount_Nm3.Value - lPreviousOxygenBlowingPhase.O2Amount_Nm3.Value) / O2Flow_Nm3_min);
            }
        }

        public override string ToString()
        {
            return String.Format("[{0}] [{3} | {4}] [PhaseGroup: {1}] PhaseName: {2} ", O2Amount_Nm3, PhaseGroup, PhaseName,
                SimulationDuration.ToString("g"), SimulationTotalDuration.ToString("g"));
        }
    }

    /// <summary>
    /// Phases for material addition used in PhasePrimaryDivision:
    /// - Charging
    /// - Tapping
    /// O2Amount_Nm3 - Oxygen amount when phase become active.
    /// O2Flow_Nm3_min - new setpoint for oxygen flow.
    /// LanceDistance_mm - new setpoint for oxygen lance.
    /// </summary>
    public class PhaseItemMatAdd : PhaseItem
    {
        public int? O2Amount_Nm3 { get; set; }
        public bool ChargingScrap { get; set; }
        public bool ChargingHotMetal { get; set; }
        public DTO.MOUT_AlloyRecipeDTO AlloyRecipe { get; set; }

        public override string ToString()
        {
            return String.Format("[{0}] [{3} | {4}] [PhaseGroup: {1}] {2}", O2Amount_Nm3, PhaseGroup, String.Join(", ", AlloyRecipe.MOUT_AlloyRecipeItems.Select(aR => String.Format("{0} [{1}]", aR.MINP_GD_Material.ShortCode, aR.Amount_kg))),
                SimulationDuration.ToString("g"), SimulationTotalDuration.ToString("g"));
        }
    }

    public class PhaseItemL1Command : PhaseItem
    {
        public Common.Enumerations.L2L1_Command L1Command { get; set; }

        public override string ToString()
        {
            return String.Format("[{2} | {3}] [PhaseGroup: {0}] {1}", PhaseGroup, L1Command,
                SimulationDuration.ToString("g"), SimulationTotalDuration.ToString("g"));
        }
    }
}
