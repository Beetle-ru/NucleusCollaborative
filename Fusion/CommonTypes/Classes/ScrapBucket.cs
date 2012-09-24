using System;

namespace CommonTypes.Classes
{
    [Serializable]
    public class ScrapBucket
    {
        public int Id { get; set; }
        public int FusionId { get; set; }
        public int MaterialNumber { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int Weight { get; set; }
        public int Number { get; set; }
    }
}
