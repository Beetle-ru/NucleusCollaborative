using System;

namespace CommonTypes.Classes
{
    [Serializable]
    public class BathLevel
    {
        public int Id { get; set; }
        public int FusionId { get; set; }
        public DateTime Date { set; get; }
        public int Value  { set; get;}
    }
}
