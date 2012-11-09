using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Data
{
    /// <summary>
    /// Defined phases for treatment based on process pattern.
    /// </summary>
    public class Phases
    {
        public event EventHandler<EventArgs.CurrentPhaseChangedEventArgs> CurrentPhaseChanged;

        public List<PhaseItem> Items
        {
            get
            {
                return mPhasesItems;
            }
        }
        public PhaseItem CurrentPhase
        {
            get
            {
                return mCurrentPhase;
            }
            private set
            {
                PhaseItem lPreviousPhase = mCurrentPhase;
                mCurrentPhase = value;
                if (CurrentPhaseChanged != null) CurrentPhaseChanged(this, new EventArgs.CurrentPhaseChangedEventArgs(lPreviousPhase, mCurrentPhase, CurrentVisiblePhase));
            }
        }
        public PhaseItem CurrentVisiblePhase
        {
            get
            {
                return mCurrentVisiblePhase;
            }
            private set
            {
                PhaseItem lPreviousPhase = mCurrentVisiblePhase;
                mCurrentVisiblePhase = value;
            }
        }

        private List<PhaseItem> mPhasesItems;
        private PhaseItem mCurrentPhase;
        private PhaseItem mCurrentVisiblePhase;

        public Phases()
        {
            mCurrentPhase = null;
            mCurrentVisiblePhase = null;
            mPhasesItems = new List<PhaseItem>();
        }
        public Phases(List<PhaseItem> aPhases)
        {
            mCurrentPhase = null;
            mCurrentVisiblePhase = null;
            mPhasesItems = aPhases;
            RefreshReferences();
        }
        public Phases(DTO.MINP_ProcessPatternDTO aProcessPattern, DTO.MINP_HeatAimDataDTO aHeatAimData)
        {
            mCurrentPhase = null;
            mCurrentVisiblePhase = null;
            mPhasesItems = new List<PhaseItem>();

            #region Charging
            // charging in 5 subphases (material, scrap, material, hot metal, material)
            IEnumerable<IGrouping<int, DTO.MINP_ProcessPatternSlagDTO>> lChargingMaterials = aProcessPattern.MINP_ProcessPatternSlags
                                            .Where(aR => aR.Prepairing)
                                            .OrderBy(aR => aR.Index)
                                            .GroupBy(aR => aR.Index);

            foreach (IGrouping<int, DTO.MINP_ProcessPatternSlagDTO> nItem in lChargingMaterials)
            {
                DTO.MOUT_AlloyRecipeDTO lAlloyingRecipe = GetAlloyingRecipe(nItem.Select(aValue => aValue), aHeatAimData);
                // Charging - scrap and hot metal charge - exceptions
                //if (lAlloyingRecipe.MOUT_AlloyRecipeItems.Count == 0) continue;

                if (nItem.Key == 1 || nItem.Key == 3 || nItem.Key == 5)
                {
                    if (lAlloyingRecipe.MOUT_AlloyRecipeItems.Count == 0) continue;
                }

                mPhasesItems.Add(new PhaseItemMatAdd()
                                {
                                    PhaseGroup = PhasePrimaryDivision.Charging,
                                    PhaseName = GetChargingPhaseName(nItem.Key),
                                    CanSkip = true,
                                    OperatorVisible = true,
                                    OperatorConfirmation = true,
                                    OperatorConfirmationText = GetChargingOperatorConfirmationText(nItem.Key),
                                    AlloyRecipe = lAlloyingRecipe,
                                    ChargingScrap = nItem.Key == 2,
                                    ChargingHotMetal = nItem.Key == 4,
                                });
            }
            #endregion
            #region Oxygen blowing start
            mPhasesItems.Add(new PhaseItemL1Command()
            {
                PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                PhaseName = Common.Properties.Resource_Data.Phases_L1_OxygenBlowingStart,
                CanSkip = false,
                OperatorConfirmation = true,
                OperatorConfirmationText = Common.Properties.Resource_Data.Phases_Confirmation_OxygenBlowingStart,
                OperatorVisible = true,
                L1Command = Common.Enumerations.L2L1_Command.OxygenBlowingStart
            });
            #endregion
            #region Oxgen blowing and material additions
            List<DTO.MINP_ProcessPatternOxygenDTO> lOxBlowingItems = aProcessPattern.MINP_ProcessPatternOxygens
                                        .Where(aR => !aR.Correction)
                                        .OrderBy(aR => aR.Index)
                                        .ToList();
            List<IGrouping<int, DTO.MINP_ProcessPatternSlagDTO>> lOxBlowingMaterials = aProcessPattern.MINP_ProcessPatternSlags
                                        .Where(aR => !aR.Prepairing)
                                        .GroupBy(aR => aR.OxygenAmount_m3.Value)
                                        .OrderBy(aR => aR.Key)
                                        .ToList();
            int lIndexOxBlowingItems = 0;
            int lIndexOxBlowingMaterials = 0;

            while (lIndexOxBlowingItems < lOxBlowingItems.Count || lIndexOxBlowingMaterials < lOxBlowingMaterials.Count)
            {
                if (lIndexOxBlowingItems < lOxBlowingItems.Count)
                {
                    // adding items from lists based on oxygen blowing amounts
                    if (lIndexOxBlowingMaterials < lOxBlowingMaterials.Count)
                    {
                        // material available
                        if (lOxBlowingItems[lIndexOxBlowingItems].OxygenAmount_m3.HasValue && lOxBlowingItems[lIndexOxBlowingItems].OxygenAmount_m3.Value > lOxBlowingMaterials[lIndexOxBlowingMaterials].Key)
                        {
                            #region Split ox blowing phase
                            // 1. Oxygen blowing until material addition
                            mPhasesItems.Add(new PhaseItemOxygenBlowing()
                            {
                                PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                                PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowing,
                                CanSkip = false,
                                OperatorConfirmation = false,
                                OperatorVisible = false,
                                O2Amount_Nm3 = lOxBlowingMaterials[lIndexOxBlowingMaterials].Key,
                                O2Flow_Nm3_min = lOxBlowingItems[lIndexOxBlowingItems].OxygenFlow_Nm3_min,
                                LanceDistance_mm = lOxBlowingItems[lIndexOxBlowingItems].LanceDistance_mm
                            });
                            // 2. Material addition
                            DTO.MOUT_AlloyRecipeDTO lAlloyingRecipe = GetAlloyingRecipe(lOxBlowingMaterials[lIndexOxBlowingMaterials].Select(aValue => aValue), aHeatAimData);
                            if (lAlloyingRecipe.MOUT_AlloyRecipeItems.Count > 0)
                            {
                                mPhasesItems.Add(new PhaseItemMatAdd()
                                {
                                    PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                                    PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowing_MatAdd,
                                    CanSkip = true,
                                    OperatorConfirmation = true,
                                    OperatorConfirmationText = Common.Properties.Resource_Data.Phases_L1_ChargeMaterials,
                                    OperatorVisible = true,
                                    O2Amount_Nm3 = lOxBlowingMaterials[lIndexOxBlowingMaterials].Key,
                                    AlloyRecipe = lAlloyingRecipe
                                });
                            }
                            #endregion
                            lIndexOxBlowingItems++;
                            lIndexOxBlowingMaterials++;
                        }
                        else if (lOxBlowingItems[lIndexOxBlowingItems].OxygenAmount_m3.HasValue && lOxBlowingMaterials[lIndexOxBlowingMaterials].Key == lOxBlowingItems[lIndexOxBlowingItems].OxygenAmount_m3.Value)
                        {
                            #region Add both
                            mPhasesItems.Add(new PhaseItemOxygenBlowing()
                            {
                                PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                                PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowing,
                                CanSkip = false,
                                OperatorConfirmation = false,
                                OperatorVisible = false,
                                O2Amount_Nm3 = lOxBlowingItems[lIndexOxBlowingItems].OxygenAmount_m3,
                                O2Flow_Nm3_min = lOxBlowingItems[lIndexOxBlowingItems].OxygenFlow_Nm3_min,
                                LanceDistance_mm = lOxBlowingItems[lIndexOxBlowingItems].LanceDistance_mm
                            });
                            DTO.MOUT_AlloyRecipeDTO lAlloyingRecipe = GetAlloyingRecipe(lOxBlowingMaterials[lIndexOxBlowingMaterials].Select(aValue => aValue), aHeatAimData);
                            if (lAlloyingRecipe.MOUT_AlloyRecipeItems.Count > 0)
                            {
                                mPhasesItems.Add(new PhaseItemMatAdd()
                                {
                                    PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                                    PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowing_MatAdd,
                                    CanSkip = true,
                                    OperatorConfirmation = true,
                                    OperatorConfirmationText = Common.Properties.Resource_Data.Phases_L1_ChargeMaterials,
                                    OperatorVisible = true,
                                    O2Amount_Nm3 = lOxBlowingMaterials[lIndexOxBlowingMaterials].Key,
                                    AlloyRecipe = lAlloyingRecipe
                                });
                            }
                            #endregion
                            lIndexOxBlowingItems++;
                            lIndexOxBlowingMaterials++;
                        }
                        else
                        {
                            #region Add ox blowing phase only
                            mPhasesItems.Add(new PhaseItemOxygenBlowing()
                            {
                                PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                                PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowing,
                                CanSkip = false,
                                OperatorConfirmation = false,
                                OperatorVisible = false,
                                O2Amount_Nm3 = lOxBlowingItems[lIndexOxBlowingItems].OxygenAmount_m3,
                                O2Flow_Nm3_min = lOxBlowingItems[lIndexOxBlowingItems].OxygenFlow_Nm3_min,
                                LanceDistance_mm = lOxBlowingItems[lIndexOxBlowingItems].LanceDistance_mm
                            });
                            #endregion
                            lIndexOxBlowingItems++;
                        }
                    }
                    else
                    {
                        #region Material NOT available => add oxygen blowing phase
                        mPhasesItems.Add(new PhaseItemOxygenBlowing()
                            {
                                PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                                PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowing,
                                CanSkip = false,
                                OperatorConfirmation = false,
                                OperatorVisible = false,
                                O2Amount_Nm3 = lOxBlowingItems[lIndexOxBlowingItems].OxygenAmount_m3,
                                O2Flow_Nm3_min = lOxBlowingItems[lIndexOxBlowingItems].OxygenFlow_Nm3_min,
                                LanceDistance_mm = lOxBlowingItems[lIndexOxBlowingItems].LanceDistance_mm
                            });
                        #endregion
                        lIndexOxBlowingItems++;
                    }
                }
                else
                {
                    #region Only material addition left
                    DTO.MOUT_AlloyRecipeDTO lAlloyingRecipe = GetAlloyingRecipe(lOxBlowingMaterials[lIndexOxBlowingMaterials].Select(aValue => aValue), aHeatAimData);
                    if (lAlloyingRecipe.MOUT_AlloyRecipeItems.Count > 0)
                    {
                        mPhasesItems.Add(new PhaseItemMatAdd()
                        {
                            PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                            PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowing_MatAdd,
                            CanSkip = true,
                            OperatorConfirmation = true,
                            OperatorConfirmationText = Common.Properties.Resource_Data.Phases_L1_ChargeMaterials,
                            OperatorVisible = true,
                            O2Amount_Nm3 = lOxBlowingMaterials[lIndexOxBlowingMaterials].Key,
                            AlloyRecipe = lAlloyingRecipe
                        });
                    }
                    lIndexOxBlowingMaterials++;
                    #endregion
                }
            }

            int lCount = aProcessPattern.MINP_ProcessPatternOxygens.Count(aR => !aR.Correction);
            // the last two lines operator visible
            if (lCount >= 2)
            {
                IEnumerable<PhaseItemOxygenBlowing> lLastPhases = mPhasesItems
                                    .Where(aR => aR is PhaseItemOxygenBlowing).Cast<PhaseItemOxygenBlowing>()
                                    .Skip(lCount - 2).Take(2);
                foreach (var nItem in lLastPhases) nItem.OperatorVisible = true;
            }
            #endregion
            #region Temperature Measurement
            mPhasesItems.Add(new PhaseItemL1Command()
            {
                PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection,
                PhaseName = Common.Properties.Resource_Data.Phases_Name_TemperatureMasurement,
                CanSkip = true,
                OperatorConfirmation = true,
                OperatorConfirmationText = Common.Properties.Resource_Data.Phases_L1_TemperatureMeasurement,
                OperatorVisible = true,
                L1Command = Common.Enumerations.L2L1_Command.TemperatureMeasurement
            });
            #endregion
            #region Correction, Parking
            DTO.MINP_ProcessPatternOxygenDTO lCorrectionPhase = aProcessPattern.MINP_ProcessPatternOxygens.SingleOrDefault(aR => aR.Correction);
            if (lCorrectionPhase != null)
            {
                mPhasesItems.Add(new PhaseItemOxygenBlowing()
                {
                    PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection,
                    PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowingCorrection,
                    CanSkip = true,
                    OperatorConfirmation = true,
                    OperatorVisible = true,
                    O2Amount_Nm3 = lCorrectionPhase.OxygenAmount_m3,
                    O2Flow_Nm3_min = lCorrectionPhase.OxygenFlow_Nm3_min,
                    LanceDistance_mm = lCorrectionPhase.LanceDistance_mm
                });
            }
            // Parking position
            mPhasesItems.Add(new PhaseItemL1Command()
            {
                PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection,
                PhaseName = Common.Properties.Resource_Data.Phases_L1_OxygenLanceParking,
                CanSkip = false,
                OperatorConfirmation = false,
                OperatorConfirmationText = Common.Properties.Resource_Data.Phases_L1_OxygenLanceParking,
                OperatorVisible = true,
                L1Command = Common.Enumerations.L2L1_Command.OxygenLanceToParkingPosition
            });
            #endregion

            RefreshReferences();
        }

        public void SwitchToNextPhase()
        {
            if (CurrentPhase != null)
            {
                CurrentPhase = CurrentPhase.NextPhase;
                if (CurrentPhase.OperatorVisible) CurrentVisiblePhase = CurrentPhase;
            }
            else
            {
                CurrentPhase = mPhasesItems.FirstOrDefault();
                CurrentVisiblePhase = mPhasesItems.FirstOrDefault(aR => aR.OperatorVisible);
            }
        }
        public void SwitchToPhase(Data.PhaseItem aPhase)
        {
            CurrentPhase = aPhase;
            CurrentVisiblePhase = aPhase;
        }
        public bool SwitchToPhaseNumber(int aPhaseNumber)
        {
            Data.PhaseItem lPhase = Items.SingleOrDefault(aR => aR.PhaseNumber == aPhaseNumber);
            if (lPhase == null) return false;
            CurrentPhase = lPhase;
            CurrentVisiblePhase = lPhase;
            return true;
        }
        public void AbortPhases()
        {
            CurrentPhase = null;
            CurrentVisiblePhase = null;
        }
        public void SetOxygenBlowingO2Amount(int aO2Amount_Nm3)
        {
            List<PhaseItemOxygenBlowing> lResult = mPhasesItems
                            .Where(aR => aR is PhaseItemOxygenBlowing && aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowing)
                            .Cast<PhaseItemOxygenBlowing>()
                            .Where(aR => !aR.O2Amount_Nm3.HasValue).ToList();
            foreach (var nItem in lResult)
            {
                nItem.O2Amount_Nm3 = aO2Amount_Nm3;
            }

            #region Re-order phases
            for (int i = 0; i < lResult.Count(); i++) Items.Remove(lResult[i]);

            int lInsertIndex = 0;
            foreach (PhaseItem nItem in Items)
            {
                if (nItem.PhaseGroup != PhasePrimaryDivision.OxygenBlowing) { lInsertIndex++; continue; }

                System.Reflection.PropertyInfo lProperty = nItem.GetType().GetProperty("O2Amount_Nm3");
                if (lProperty == null) { lInsertIndex++; continue; }
                object lValue = lProperty.GetValue(nItem, null);
                int lO2Amount = (int)lValue;
                if (aO2Amount_Nm3 <= lO2Amount) break;
                lInsertIndex++;
            }

            if (lInsertIndex <= Items.Count - 1 && Items[lInsertIndex].PhaseGroup == PhasePrimaryDivision.OxygenBlowing)
            {
                System.Reflection.PropertyInfo lProperty = Items[lInsertIndex].GetType().GetProperty("O2Amount_Nm3");
                object lValue = lProperty.GetValue(Items[lInsertIndex], null);
                int lO2Amount = (int)lValue;

                if (aO2Amount_Nm3 < lO2Amount) Items.InsertRange(lInsertIndex, lResult);
                else
                {
                    int lLastOxBlowingIndex = Items.IndexOf(Items.Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowing).Last()) + 1;
                    if (lLastOxBlowingIndex >= Items.Count) Items.AddRange(lResult); else Items.InsertRange(lLastOxBlowingIndex, lResult);
                }
            }
            else
            {
                // add to the end of oxygenblowing phases
                int lLastOxBlowingIndex = Items.IndexOf(Items.Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowing).Last()) + 1;
                if (lLastOxBlowingIndex >= Items.Count) Items.AddRange(lResult); else Items.InsertRange(lLastOxBlowingIndex, lResult);
            }
            #endregion

        }
        public void SetCorrectionO2Amount(int aO2Amount_Nm3)
        {
            IEnumerable<PhaseItemOxygenBlowing> lResult = mPhasesItems
                            .Where(aR => aR is PhaseItemOxygenBlowing && aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowingCorrection)
                            .Cast<PhaseItemOxygenBlowing>()
                            .Where(aR => !aR.O2Amount_Nm3.HasValue);
            foreach (var nItem in lResult) nItem.O2Amount_Nm3 = aO2Amount_Nm3;
        }
        /// <summary>
        /// Dolomit is replaced by Dolomit S.
        /// If the method is called again then Dolomit will not be found.
        /// </summary>
        /// <param name="aCoef"></param>
        public void ReplaceDolomit(float aCoef)
        {
            if (CurrentPhase == null) return;

            DTO.MINP_GD_MaterialDTO lDolom_DTO = Data.MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.Dolomite];
            DTO.MINP_GD_MaterialDTO lDolomS_DTO = Data.MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer1];

            PhaseItem lPhaseItem = CurrentPhase;
            while (lPhaseItem != null)
            {
                if (lPhaseItem is PhaseItemMatAdd)
                {
                    PhaseItemMatAdd lPhaseItemMatAdd = (PhaseItemMatAdd)lPhaseItem;

                    foreach (var nDolomit in lPhaseItemMatAdd.AlloyRecipe.MOUT_AlloyRecipeItems.Where(aR => aR.MINP_GD_Material.Code == lDolom_DTO.Code))
                    {
                        nDolomit.MINP_GD_Material = lDolomS_DTO;
                        if (nDolomit.Amount_kg.HasValue) nDolomit.Amount_kg = (int)Math.Round(nDolomit.Amount_kg.Value * aCoef);
                    }
                }

                lPhaseItem = lPhaseItem.NextPhase;
            }
        }
        public List<PhaseItem> GetPhasesForDynamicModel()
        {
            return Items.Where(aR => aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowing || aR.PhaseGroup == PhasePrimaryDivision.OxygenBlowingCorrection).ToList();
        }
        public List<PhaseItem> CreatePhasesForDynamicModelSimulation(int aOxygenAmount_Nm3)
        {
            // only main oxygen blowing phase
            mPhasesItems.Add(new PhaseItemOxygenBlowing()
            {
                PhaseGroup = PhasePrimaryDivision.OxygenBlowing,
                PhaseName = Common.Properties.Resource_Data.Phases_Name_OxygenBlowing,
                CanSkip = false,
                OperatorConfirmation = false,
                OperatorVisible = false,
                O2Amount_Nm3 = aOxygenAmount_Nm3,
                O2Flow_Nm3_min = 1200,
                LanceDistance_mm = 400
            });
            // Parking position
            mPhasesItems.Add(new PhaseItemL1Command()
            {
                PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection,
                PhaseName = Common.Properties.Resource_Data.Phases_L1_OxygenLanceParking,
                CanSkip = false,
                OperatorConfirmation = false,
                OperatorConfirmationText = Common.Properties.Resource_Data.Phases_L1_OxygenLanceParking,
                OperatorVisible = true,
                L1Command = Common.Enumerations.L2L1_Command.OxygenLanceToParkingPosition
            });

            return mPhasesItems;
        }

        private string GetChargingPhaseName(int aIndex)
        {
            switch (aIndex)
            {
                case 1: return Common.Properties.Resource_Data.Phases_Name_ChargingMaterials;
                case 2: return Common.Properties.Resource_Data.Phases_Name_ChargingScrap;
                case 3: return Common.Properties.Resource_Data.Phases_Name_ChargingMaterials;
                case 4: return Common.Properties.Resource_Data.Phases_Name_ChargingHotMetal;
                case 5: return Common.Properties.Resource_Data.Phases_Name_ChargingMaterials;
            }

            return "-";
        }
        private string GetChargingOperatorConfirmationText(int aIndex)
        {
            switch (aIndex)
            {
                case 1: return Common.Properties.Resource_Data.Phases_L1_ChargeMaterials;
                case 2: return Common.Properties.Resource_Data.Phases_Confirmation_ScrapCharged;
                case 3: return Common.Properties.Resource_Data.Phases_L1_ChargeMaterials;
                case 4: return Common.Properties.Resource_Data.Phases_Confirmation_HotMetalCharged;
                case 5: return Common.Properties.Resource_Data.Phases_L1_ChargeMaterials;
            }

            return "-";
        }
        private DTO.MOUT_AlloyRecipeDTO GetAlloyingRecipe(IEnumerable<DTO.MINP_ProcessPatternSlagDTO> aProcessPatternSlags, DTO.MINP_HeatAimDataDTO aHeatAimData)
        {
            DTO.MOUT_AlloyRecipeDTO lResult = new DTO.MOUT_AlloyRecipeDTO();
            lResult.MOUT_AlloyRecipeItems = new List<DTO.MOUT_AlloyRecipeItemsDTO>();

            foreach (var nItem in aProcessPatternSlags)
            {
                if (nItem.CaCO3_p.HasValue && aHeatAimData.S1_kg > 0)
                {
                    lResult.MOUT_AlloyRecipeItems.Add(new DTO.MOUT_AlloyRecipeItemsDTO()
                    {
                        MINP_GD_Material = MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer1],
                        Amount_kg = (int?)Math.Round((nItem.CaCO3_p.Value / 100f) * aHeatAimData.S1_kg)
                    });
                }
                // TODO: Pro S2
                /*
                if (nItem.CaCO3_p.HasValue && aHeatAimData.CaCO3_kg > 0)
                {
                    lResult.MOUT_AlloyRecipeItems.Add(new DTO.MOUT_AlloyRecipeItemsDTO()
                    {
                        MINP_GD_Material = MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.CaCO3],
                        Amount_kg = (int?)Math.Round((nItem.CaCO3_p.Value / 100f) * aHeatAimData.CaCO3_kg)
                    });
                }
                 * */
                if (nItem.Coke_p.HasValue && aHeatAimData.Coke_kg > 0)
                {
                    lResult.MOUT_AlloyRecipeItems.Add(new DTO.MOUT_AlloyRecipeItemsDTO()
                    {
                        MINP_GD_Material = MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.Coke],
                        Amount_kg = (int?)Math.Round((nItem.Coke_p.Value / 100f) * aHeatAimData.Coke_kg)
                    });
                }
                if (nItem.Dolomit_p.HasValue && aHeatAimData.Dolomit_kg > 0)
                {
                    lResult.MOUT_AlloyRecipeItems.Add(new DTO.MOUT_AlloyRecipeItemsDTO()
                    {
                        MINP_GD_Material = MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.Dolomite],
                        Amount_kg = (int?)Math.Round((nItem.Dolomit_p.Value / 100f) * aHeatAimData.Dolomit_kg)
                    });
                }
                if (nItem.Fom_p.HasValue && aHeatAimData.FOM_kg > 0)
                {
                    lResult.MOUT_AlloyRecipeItems.Add(new DTO.MOUT_AlloyRecipeItemsDTO()
                    {
                        MINP_GD_Material = MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.FOM],
                        Amount_kg = (int?)Math.Round((nItem.Fom_p.Value / 100f) * aHeatAimData.FOM_kg)
                    });
                }
                if (nItem.Lime_p.HasValue && aHeatAimData.Lime_kg > 0)
                {
                    lResult.MOUT_AlloyRecipeItems.Add(new DTO.MOUT_AlloyRecipeItemsDTO()
                    {
                        MINP_GD_Material = MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.CaO],
                        Amount_kg = (int?)Math.Round((nItem.Lime_p.Value / 100f) * aHeatAimData.Lime_kg)
                    });
                }
            }

            return lResult;
        }
        private void RefreshReferences()
        {
            PhaseItem lLastPhaseItem = null;
            int lPhaseNumber = 0;
            foreach (var nItem in mPhasesItems)
            {
                nItem.PhaseNumber = ++lPhaseNumber;
                nItem.PreviousPhase = lLastPhaseItem;
                if (lLastPhaseItem != null) lLastPhaseItem.NextPhase = nItem;
                lLastPhaseItem = nItem;
            }
        }
    }
}
