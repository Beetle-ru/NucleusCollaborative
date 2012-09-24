// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:57
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L1L2_BunkerMaterialDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Int32 ConverterNo { get; set; }

        public DateTime TimeProcessed { get; set; }

        public Int32 Destination { get; set; }

        public String BunkerNo { get; set; }

        public String Code { get; set; }

        public String ShortCode { get; set; }

        public Nullable<Int32> Amount { get; set; }

        public Nullable<Boolean> Available { get; set; }
    }
}
