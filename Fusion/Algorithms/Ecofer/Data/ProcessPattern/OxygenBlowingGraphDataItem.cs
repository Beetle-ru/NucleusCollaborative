using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data.ProcessPattern
{
    public class OxygenBlowingGraphDataItem
    {
        public int OxygenAmount { get; set; }
        public int OxygenFlow { get; set; }
        public int LanceDistance { get; set; }

        public static IEnumerable<OxygenBlowingGraphDataItem> GetData(IEnumerable<DTO.MINP_GD_ProcessPatternOxygenDTO> aInputData)
        {
            List<OxygenBlowingGraphDataItem> lItems = new List<OxygenBlowingGraphDataItem>();

            if (aInputData == null || aInputData.Count() == 0) return lItems;

            int lOxygenAmount = 0;
            int lDelta = 0;
            int lMaxIndex = aInputData.Where(aR => !aR.Correction).Max(aR => aR.Index);

            // all - foreach (var nItem in aInputData.OrderBy(aR => aR.Index))
            foreach (var nItem in aInputData.Where(aR => !aR.Correction).OrderBy(aR => aR.Index))
            {
                if (nItem.Index == lMaxIndex) break;

                if (!nItem.OxygenAmount_m3.HasValue)
                {
                    if (!nItem.Correction) lDelta = Global.ProcessPattern_OxygenAmount_Main_Delta;
                    else
                    {
                        if (nItem.Index == lMaxIndex)
                            lDelta = Global.ProcessPattern_OxygenAmount_End_Delta;
                        else
                            lDelta = Global.ProcessPattern_OxygenAmount_Correction_Delta;
                    }
                }
                else
                {
                    lDelta = 0;
                }

                lOxygenAmount = nItem.OxygenAmount_m3.HasValue ? nItem.OxygenAmount_m3.Value : lOxygenAmount + lDelta;

                lItems.Add(new OxygenBlowingGraphDataItem()
                {
                    OxygenAmount = lOxygenAmount,
                    OxygenFlow = nItem.OxygenFlow_Nm3_min,
                    LanceDistance = nItem.LanceDistance_mm
                });

                /*
                if (!nItem.OxygenAmount_m3.HasValue && !nItem.Correction)
                {
                    lOxygenAmount += Global.ProcessPattern_OxygenAmount_TempMeas_Delta;

                    // temperature measurement
                    lItems.Add(new OxygenBlowingGraphDataItem()
                    {
                        OxygenAmount = lOxygenAmount,
                        OxygenFlow = nItem.OxygenFlow_Nm3_min,
                        LanceDistance = nItem.LanceDistance_mm
                    });
                }
                */
            }

            return lItems;
        }
    }
}
