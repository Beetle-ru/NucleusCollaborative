using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using CommonTypes;
using ConnectionProvider;
using Converter;
using HeatCharge;
using Implements;

namespace AlgorithmsUI
{
    public class CoreListener : IEventListener
    {
        public static long HeatNumber = -1;
        public static long mixerCount;
        public static Queue<FlexHelper> mixers = new Queue<FlexHelper>(); 
        public static System.Timers.Timer mixerTimer = new System.Timers.Timer();
        public void Init()
        {
            mixerTimer.Interval = 1000;
            mixerTimer.AutoReset = false;
            mixerTimer.Enabled = true;
            mixerTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // список доступных миксеров
            var fex = new FlexHelper("OPC.Read-OPC.HM-Chemistry.Event.");
            fex.Fire(Program.MainGate);
            // текущий номер плавки
            Program.MainGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(HeatChangeEvent).Name });
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            lock (mixers)
            {
                var cnt = mixers.Count;
                if (cnt == 0) return;
                Program.face.Invoke(new MethodInvoker(delegate()
                {
                    Program.face.ironTable.dgw.Rows.Add(cnt);
                    for (var i = 0; i < cnt; i++)
                    {
                        var fex = mixers.Dequeue();
                        Program.face.ironTable.dgw.Rows[i].Cells[1].Value 
                            = fex.GetInt("Mixer");
                        Program.face.ironTable.dgw.Rows[i].Cells[2].Value 
                            = Math.Round(fex.GetDbl("HM-C"), 2);
                        Program.face.ironTable.dgw.Rows[i].Cells[3].Value
                            = Math.Round(fex.GetDbl("HM-Si"), 2);
                        Program.face.ironTable.dgw.Rows[i].Cells[4].Value
                            = Math.Round(fex.GetDbl("HM-Mn"), 2);
                        Program.face.ironTable.dgw.Rows[i].Cells[5].Value
                            = Math.Round(fex.GetDbl("HM-P"), 2);
                        Program.face.ironTable.dgw.Rows[i].Cells[6].Value
                            = Math.Round(fex.GetDbl("HM-S"), 2);
                    }
                }));
            }
        }
        public void OnEvent(BaseEvent evt)
        {
            using (Logger l = new Logger("OnEvent"))
            {
                if (evt is FlexEvent)
                {
                    var fex = new FlexHelper(evt as FlexEvent);
                    if (fex.evt.Operation.StartsWith("OPC.HM-Chemistry.Event."))
                    {
                        lock (mixers)
                        {
                            mixers.Enqueue(fex);
                            mixerTimer.Interval = 1000;
                        }
                    }
                    else if (fex.evt.Operation.StartsWith("Model.Shixta-I"))
                    {
                        l.msg("{0}", fex.evt);
                    }
                }
                else if (evt is HeatChangeEvent)
                {
                    var hce = evt as HeatChangeEvent;
                    if (HeatNumber == hce.HeatNumber) return;
                    Int64 rem;
                    Int64 res = Math.DivRem(hce.HeatNumber, 10000, out rem);
                    var newHeatNumber = res * 100000 + rem;
                    HeatNumber = newHeatNumber;
                    Program.face.Invoke(new MethodInvoker(delegate()
                    {
                        Program.face.txbHeatNum.Text = Convert.ToString(HeatNumber);
                        Program.face.txbSteelTemp.Text = "1650";
                        Program.face.txbCarbonPercent.Text = "0,03";
                        Program.face.txbMgO.Text = "10";
                        Program.face.txbFeO.Text = "27";
                        Program.face.txbBasiticy.Text = "2,7";
                        Program.face.txbIronTask.Text = "300";
                        Program.face.txbIronTemp.Text = "1380";

                    }));
                }
            }
        }

    }
}