using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using CommonTypes;
using ConnectionProvider;
using Converter;
using Implements;

namespace HeatControl {
    public class CoreListener : IEventListener {
        public ConnectionProvider.Client MainGate;
        private MixCalculator ClientFace;
        public String ClientName;

        public CoreListener(String _Name, MixCalculator _Face) {
            ClientName = _Name;
            ClientFace = _Face;
        }

        public long HeatNumber = -1;
        public long mixerCount;
        public Queue<FlexHelper> mixers = new Queue<FlexHelper>();
        public System.Timers.Timer mixerTimer = new System.Timers.Timer();

        public void Init() {
            MainGate = new Client(ClientName, this);
            MainGate.Subscribe();
            mixerTimer.Interval = 1000;
            mixerTimer.AutoReset = false;
            mixerTimer.Enabled = true;
            mixerTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // список доступных миксеров
            var fex = new FlexHelper("OPC.Read-OPC.HM-Chemistry.Event.");
            ClientFace.ironTable.dgw.RowCount = 0;
            fex.Fire(MainGate);
            // текущий номер плавки
            MainGate.PushEvent(new OPCDirectReadEvent() {EventName = typeof (HeatChangeEvent).Name});
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e) {
            lock (mixers) {
                var cnt = mixers.Count;
                if (cnt == 0) return;
                ClientFace.Invoke(new MethodInvoker(delegate()
                {
                    ClientFace.ironTable.dgw.Rows.Add(cnt);
                    for (var i = 0; i < cnt; i++)
                    {
                        var fex = mixers.Dequeue();
                        ClientFace.ironTable.dgw.Rows[i].Cells[1].Value
                            = fex.GetInt("Mixer");
                        ClientFace.ironTable.dgw.Rows[i].Cells[2].Value
                            = Math.Round(fex.GetDbl("HM-C"), 2);
                        ClientFace.ironTable.dgw.Rows[i].Cells[3].Value
                            = Math.Round(fex.GetDbl("HM-Si"), 2);
                        ClientFace.ironTable.dgw.Rows[i].Cells[4].Value
                            = Math.Round(fex.GetDbl("HM-Mn"), 2);
                        ClientFace.ironTable.dgw.Rows[i].Cells[5].Value
                            = Math.Round(fex.GetDbl("HM-P"), 2);
                        ClientFace.ironTable.dgw.Rows[i].Cells[6].Value
                            = Math.Round(fex.GetDbl("HM-S"), 2);
                    }
                }));
            }
        }

        public void OnEvent(BaseEvent evt) {
            using (Logger l = new Logger("OnEvent")) {
                if (evt is FlexEvent) {
                    var fex = new FlexHelper(evt as FlexEvent);
                    if (fex.evt.Operation.StartsWith("OPC.HM-Chemistry.Event.")) {
                        lock (mixers) {
                            mixers.Enqueue(fex);
                            mixerTimer.Interval = 1000;
                            ClientFace.ironTable.dgw.RowCount = 0;
                        }
                    }
                    else if (fex.evt.Operation.StartsWith("Model.Shixta-I")) {
                        l.msg("{0}", fex.evt);
                    }
                }
                else if (evt is HeatChangeEvent) {
                    var hce = evt as HeatChangeEvent;
                    Int64 rem;
                    Int64 res = Math.DivRem(hce.HeatNumber, 10000, out rem);
                    var newHeatNumber = res*100000 + rem;
                    HeatNumber = newHeatNumber + 1;
                    ClientFace.Invoke(new MethodInvoker(delegate() {
                        ClientFace.txbHeatNum.Text = Convert.ToString(HeatNumber);
                        if (ClientFace.m_cn != ClientFace.txbHeatNum.Text.Substring(0, 1)) {
                            ClientFace.m_cn = ClientFace.txbHeatNum.Text.Substring(0, 1);
                            ClientFace.lblTitleHeading.Text += ClientFace.m_cn;
                        }
                    }));
                }
            }
        }
    }
}