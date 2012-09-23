// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:48
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_GD_MaterialElementDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Int32 Index { get; set; }

        public String Code { get; set; }

        public String Unit { get; set; }

        public Nullable<Double> Mm { get; set; }

        public Nullable<Double> O2 { get; set; }

        public Nullable<Double> E_Ox1 { get; set; }

        public Nullable<Double> E_Ox2 { get; set; }

        public Nullable<Double> Eta_Ox1 { get; set; }

        public Nullable<Double> Eta_Ox2 { get; set; }

        public Nullable<Double> Vector { get; set; }

        public Nullable<Boolean> O2Balance { get; set; }

        public List<MINP_GD_MaterialItemsDTO> MINP_GD_MaterialItems { get; set; }

        public List<MINP_GD_SteelGradeItemsDTO> MINP_GD_SteelGradeItems { get; set; }
    }
}
