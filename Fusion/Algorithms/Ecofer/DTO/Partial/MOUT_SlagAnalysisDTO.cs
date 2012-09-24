// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:08
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MOUT_SlagAnalysisDTO
    {
        public double Basicity
        {
            get
            {
                if (!CaO.HasValue || !SiO2.HasValue || SiO2.Value == 0) return 0;
                return CaO.Value / SiO2.Value;
            }
        }
    }
}
