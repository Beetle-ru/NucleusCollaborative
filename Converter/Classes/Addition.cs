using System;

namespace Converter
{
    [Serializable]
    public class Addition
    {
        public int Id { get; set; }
        public int LancePosition { get; set; }
        public int O2TotalVol { get; set; }
        public int FusionId { get; set; }
        public DateTime Date { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string Destination { get; set; }
        public string DataSource { get; set; }
        public int PortionWeight { get; set; }
        public int TotalWeight { get; set; }
    }
}
