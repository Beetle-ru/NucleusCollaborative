using System;

namespace CommonTypes.Classes
{
    [Serializable]
    class Blowing
    {
        public DateTime Date { set; get; }
        public int O2TotalVol { set; get; }               // общий O2 расход                   # ACT_CX_O2VOL_TOTAL
        public int BlowingInterruptFlag { set; get; }     // прерывание продувки нач(1)/кон(0) # ACT_CX_BLOWINTERRUPT
    }
}
