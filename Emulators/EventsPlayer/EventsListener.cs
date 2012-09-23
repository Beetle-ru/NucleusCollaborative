using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

using System.Windows.Forms;
using Converter;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CommonTypes;
using ConnectionProvider.MainGate;
using System.ServiceModel;
using ConnectionProvider;

namespace EventsPlayer
{
    [CallbackBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        UseSynchronizationContext = false)]
    class EventsListener : IEventListener
    {
        MainForm form;
        List<BaseEvent> AllEvents = new List<BaseEvent>();
        Int64 CurrentHeatNumber = 0;

        public EventsListener(MainForm mainForm)
        {
            form = mainForm;
        }

        public void OnEvent(BaseEvent newEvent)
        {
            try
            {
                if (form.Status == MainForm.StatusEnum.Recorded)
                {
                    (form.Controls["tbMessages"] as System.Windows.Forms.TextBox).AppendText(newEvent.ToString() + "\n");
                }
            }

            catch { }

            if (newEvent is ConverterBaseEvent)
            {
                ConverterBaseEvent cbEvent = newEvent as ConverterBaseEvent;
                if (cbEvent is HeatChangeEvent && CurrentHeatNumber != (cbEvent as HeatChangeEvent).HeatNumber)
                {
                    SaveEvents(CurrentHeatNumber, AllEvents);
                    CurrentHeatNumber = (cbEvent as HeatChangeEvent).HeatNumber;
                    (form.Controls["tbMessages"] as System.Windows.Forms.TextBox).Clear();
                }
                if (CurrentHeatNumber != 0)
                {
                    AllEvents.Add(cbEvent);
                    WriteLog(CurrentHeatNumber, cbEvent);
                }
            }
        }

        public void SaveEvents(Int64 HeatNumber, List<BaseEvent> events)
        {
            BinaryFormatter bf;
            FileInfo file;
            FileStream fs;
            bf = new BinaryFormatter();
            if (!Directory.Exists(Application.StartupPath + "\\dat"))
                Directory.CreateDirectory(Application.StartupPath + "\\dat");
            file = new FileInfo("dat\\" + HeatNumber.ToString() + ".dat");
            fs = file.Create();
            bf.Serialize(fs, events);
            fs.Close();
            events.Clear();
        }

        public void WriteLog(Int64 HeatNumber, ConverterBaseEvent cbEvent)
        {
            if (!Directory.Exists(Application.StartupPath + "\\log"))
                Directory.CreateDirectory(Application.StartupPath + "\\log");
            FileStream fs = new FileStream("log\\" + HeatNumber + ".log", FileMode.Append);
            string str = DateTime.Now.ToString("HH:mm:ss") + " " + cbEvent.ToString() + "\n";
            fs.Write(System.Text.Encoding.GetEncoding("x-cp1251").GetBytes(str), 0, str.Length);
            fs.Close();
            fs.Dispose();
        }
    }
}
