using System;
using System.Collections.Generic;

namespace Converter
{
    [Serializable]
    public class HeatAttributes
    {
        public HeatAttributes()
        {
            Number = -1; // Плавка не началась
            Planned = new SteelAttributes();
            Actual = new SteelAttributes();
            HotMetalAttributes = new HotMetalAttributes();
            HotMetalAnalysyses = new List<HotMetalAnalysys>();
            Additions = new List<Addition>();
            ScrapBuckets = new List<ScrapBucket>();
            SlagAnalysys = new List<SlagAnalysys>();
            SteelAnalysys = new List<SteelAnalysys>();
            Sublances = new List<Sublance>();
        }

        public List<Addition> Additions { get; set; }
        public List<ScrapBucket> ScrapBuckets { get; set; }
        public List<SlagAnalysys> SlagAnalysys { get; set; }
        public List<SteelAnalysys> SteelAnalysys { get; set; }
        public List<Sublance> Sublances { get; set; }
        public List<HotMetalAnalysys> HotMetalAnalysyses { get; set; }
        public int CurrentBlowingScheme = -1;
        public int AggregateLifeTime { get; set; }
        public int ID { set; get; }
        public Int64 Number { get; set; }
        public DateTime StartDate { get; set; }
        public string Grade { get; set; }
        public SteelAttributes Planned { get; set; }
        public SteelAttributes Actual { get; set; }
        public DateTime EndDate { get; set; }
        public int AggregateNumber { get; set; }
        public int TeamNumber { get; set; }
        public HotMetalAttributes HotMetalAttributes { get; set; }

    }
}
