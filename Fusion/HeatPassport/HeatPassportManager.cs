using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.ServiceModel;
using System.Runtime.InteropServices;
using Core;
using CommonTypes;
using Converter;

namespace HeatPassport
{
    enum HeatPassportOperations
    {
        // Timashort
        AdditionsEvent = 10,
        BlowingEvent = 20,
        DeslaggingEvent = 30,
        HeatChangeEvent = 40,
        HeatingScrapEvent = 50,
        HotMetalLadleEvent = 60,
        HotMetalPouringEvent = 70,
        IgnitionEvent = 80,
        ResetO2TotalVolEvent = 90,
        ScrapChargingEvent = 100,
        ScrapEvent = 110,
        SlagBlowingEvent = 120,
        SublanceStartEvent = 130,
        SublanceCarbonEvent = 140,
        SublanceOxidationEvent = 150,
        SublanceTemperatureEvent = 160,
        TappingEvent = 170
        // Bart, pls forgive me
    }

    class HeatPassportManager
    {
        private bool m_WorkingFlag;
        private ConnectionProvider.Client m_MainGate;
        private EventsListener m_Events;
        private int m_ConverterNumber;
        private Int64 m_HeatNumber;
        private Int64 HeatNumber
        {
            get
            {
                if (m_HeatNumber != 0)
                {
                    return m_HeatNumber;
                }
                else
                {
                    try
                    {
                        m_HeatNumber = DBWorker.Instance.GetCurrentHeatNumber(m_ConverterNumber);
                        return m_HeatNumber;
                    }
                    catch
                    {
                        return 0;
                    }
                }

            }
        }
        private Dictionary<string, object> m_Repositary;
        private Dictionary<string, object> Repositary { get { return m_Repositary; } }
        private Dictionary<string, MethodInfo> m_Methods = new Dictionary<string, MethodInfo>();

        public HeatPassportManager(int ConverterNumber)
        {
            m_ConverterNumber = ConverterNumber;
            m_Repositary = new Dictionary<string, object>();
            var methods = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                if (method.Name == "ProcessEvent")
                {
                    m_Methods.Add(method.GetParameters()[0].ParameterType.Name, method);
                }
            }
        }

        private void ProccesAssync()
        {
            while (m_WorkingFlag)
            {
                while (m_Events.Queue.Count > 0)
                {
                    BaseEvent _event;
                    lock (m_Events.Queue)
                    {
                        _event = m_Events.Queue.Peek();
                    }
                    if (ProcessEvents(_event))
                    {
                        lock (m_Events.Queue)
                        {
                            m_Events.Queue.Dequeue();
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(3000);
                    }
                }
                System.Threading.Thread.Sleep(300);
            }
        }

        public void Start()
        {
            if (m_Events == null)
            {
                m_Events = new EventsListener();
            }

            if (m_MainGate == null)
            {
                m_MainGate = new ConnectionProvider.Client("Converter" + m_ConverterNumber.ToString(), m_Events);
                m_MainGate.Subscribe();
            }


            m_WorkingFlag = true;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ProccesAssync));
            thread.Start();
        }

        private bool ProcessEvents(BaseEvent _event)
        {
            if (m_Methods.ContainsKey(_event.GetType().Name))
            {
                return (bool)m_Methods[_event.GetType().Name].Invoke(this, new object[] { _event });
            }

            return true;
        }

        private bool ProcessEvent(BlowingEvent _event)
        {
            // Начало главной продувки 
            if (_event.BlowingFlag == 1 && !Repositary.ContainsKey("BlowingStartDate"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.BlowingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.BlowingFlag, _event.O2TotalVol, 0, 1);
                    Repositary.Add("BlowingStartDate", _event.Time);
                }
                catch { return false; }
            }

            // Конец главной продувки
            if (_event.BlowingFlag == 0 && Repositary.ContainsKey("BlowingStartDate"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.BlowingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.BlowingFlag, _event.O2TotalVol,
                        (_event.Time - (DateTime)Repositary["BlowingStartDate"]).TotalSeconds, 1);
                    Repositary.Remove("BlowingStartDate");
                }
                catch { return false; }
            }
            return true;
        }

        private bool ProcessEvent(ReBlowingEvent _event)
        {
            // Начало повторной продувки
            // Тоже самое, что и BlowingEvent, только флаг главной продувки установлен в 0
            if (_event.BlowingFlag == 1 && !Repositary.ContainsKey("ReBlowingStartDate"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.BlowingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.BlowingFlag, _event.O2TotalVol, 0, 0);
                    Repositary.Add("ReBlowingStartDate", _event.Time);
                }
                catch { return false; }
            }
            // Конец повторной продувки
            if (_event.BlowingFlag == 0 && Repositary.ContainsKey("ReBlowingStartDate"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.BlowingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.BlowingFlag, _event.O2TotalVol,
                          (_event.Time - (DateTime)Repositary["ReBlowingStartDate"]).TotalSeconds, 0);
                    Repositary.Remove("ReBlowingStartDate");
                }
                catch { return false; }
            }
            return true;
        }

        private bool ProcessEvent(HeatChangeEvent _event)
        {

            _event.HeatNumber = int.Parse(_event.HeatNumber.ToString().Insert(2, "0"));
            int prevHeatNumber;
            if (!Repositary.ContainsKey("PrevHeatNumber"))
            {
                Repositary.Add("PrevHeatNumber", HeatNumber);
                prevHeatNumber = 0;
            }
            else
            {
                prevHeatNumber = (int)Repositary["PrevHeatNumber"];
            }

            if (prevHeatNumber != _event.HeatNumber)
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.HeatChangeEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.HeatNumber);
                    m_Repositary = new Dictionary<string, object>();
                    Repositary.Add("PrevHeatNumber", _event.HeatNumber);
                }
                catch { return false; }
            }
            m_HeatNumber = _event.HeatNumber;
            Repositary["PrevHeatNumber"] = _event.HeatNumber;
            return true;
        }

        private bool ProcessEvent(HeatingScrapEvent _event)
        {
            // Начало сушки скрапа
            if (_event.HeatingScrapFlag == 1 && !Repositary.ContainsKey("HeatingScrap"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.HeatingScrapEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.HeatingScrapFlag);
                    Repositary.Add("HeatingScrap", true);
                    return true;
                }
                catch { return false; }
            }

            // Конец сушки скрапа
            if (_event.HeatingScrapFlag == 0 && Repositary.ContainsKey("HeatingScrap"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.HeatingScrapEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.HeatingScrapFlag);
                    Repositary.Remove("HeatingScrap");
                    return true;
                }
                catch { return false; }
            }
            return true;
        }

        private bool ProcessEvent(HotMetalLadleEvent _event)
        {
            int hotMetalLastSendPortion = Repositary.ContainsKey("HotMetalLastSendPortion") ? (int)Repositary["HotMetalLastSendPortion"] : 0;

            for (int i = hotMetalLastSendPortion; i < 3; i++)
            {
                try
                {
                    if ((int)_event.GetType().GetProperty("MixerNumberPortion" + (i + 1).ToString()).GetValue(_event, null) > 0)
                    {
                        DBWorker.Instance.Insert((int)HeatPassportOperations.HotMetalLadleEvent, _event.Time, m_ConverterNumber, HeatNumber,
                            _event.LadleNumber, _event.HotMetalTemperature, _event.GetType().GetProperty("MixerNumberPortion" + (i + 1).ToString()).GetValue(_event, null),
                            _event.GetType().GetProperty("WeightPortion" + (i + 1).ToString()).GetValue(_event, null));
                    }

                }
                catch { return false; }
                finally
                {
                    if (!Repositary.ContainsKey("HotMetalLastSendPortion"))
                    {
                        Repositary.Add("HotMetalLastSendPortion", i);
                    }
                    else
                    {
                        Repositary["HotMetalLastSendPortion"] = i;
                    }
                }
            }
            Repositary.Remove("HotMetalLastSendPortion");
            return true;
        }

        private bool ProcessEvent(IgnitionEvent _event)
        {
            try
            {
                DBWorker.Instance.Insert((int)HeatPassportOperations.IgnitionEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.O2IgnitionVol);
            }
            catch { return false; }

            return true;
        }

        private bool ProcessEvent(ResetO2TotalVolEvent _event)
        {
            try
            {
                DBWorker.Instance.Insert((int)HeatPassportOperations.ResetO2TotalVolEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.O2TotalVol);
            }
            catch { return false; }
            return true;

        }

        private bool ProcessEvent(ScrapEvent _event)
        {
            int scrapPortionLastSend = Repositary.ContainsKey("ScrapPortionLastSend") ? (int)Repositary["ScrapPortionLastSend"] : 0;

            for (int i = scrapPortionLastSend; i < 8; i++)
            {

                try
                {
                    if ((int)_event.GetType().GetProperty("ScrapType" + (i + 1).ToString()).GetValue(_event, null) > 0)
                    {
                        DBWorker.Instance.Insert((int)HeatPassportOperations.ScrapEvent, _event.Time, m_ConverterNumber, HeatNumber,
                            _event.BucketNumber, _event.GetType().GetProperty("ScrapType" + (i + 1).ToString()).GetValue(_event, null),
                            _event.GetType().GetProperty("Weight" + (i + 1).ToString()).GetValue(_event, null));
                    }

                }
                catch { return false; }
                finally
                {
                    if (!Repositary.ContainsKey("ScrapPortionLastSend"))
                    {
                        Repositary.Add("ScrapPortionLastSend", i);
                    }
                    else
                    {
                        Repositary["ScrapPortionLastSend"] = i;
                    }
                }
            }
            Repositary.Remove("ScrapPortionLastSend");
            return true;
        }

        private bool ProcessEvent(SlagBlowingEvent _event)
        {
            // Начало раздувки шлака
            if (_event.SlagBlowingFlag == 1 && !Repositary.ContainsKey("SlagBlowing"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.SlagBlowingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.SlagBlowingFlag, _event.NVol);
                    Repositary.Add("SlagBlowing", true);
                }
                catch { return false; }
            }
            // Конец раздувки шлака
            if (_event.SlagBlowingFlag == 0 && Repositary.ContainsKey("SlagBlowing"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.SlagBlowingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.SlagBlowingFlag, _event.NVol);
                    Repositary.Remove("SlagBlowing");
                }
                catch { return false; }
            }
            return true;
        }

        private bool ProcessEvent(DeslaggingEvent _event)
        {
            // Начало слива шлака
            if (_event.DeslaggingFlag == 1 && !Repositary.ContainsKey("DeslaggingStartDate"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.DeslaggingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.DeslaggingFlag, 0);
                    Repositary.Add("DeslaggingStartDate", _event.Time);
                }
                catch { return false; }
            }

            // Конец слива шлака
            if (_event.DeslaggingFlag == 0 && Repositary.ContainsKey("DeslaggingStartDate"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.DeslaggingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.DeslaggingFlag,
                        (_event.Time - (DateTime)Repositary["DeslaggingStartDate"]).TotalSeconds);
                    Repositary.Remove("DeslaggingStartDate");
                }
                catch { return false; }
            }
            return true;
        }

        private bool ProcessEvent(HotMetalPouringEvent _event)
        {
            // Начало заливки чугуна
            if (_event.HotMetalPouringFlag == 1 && !Repositary.ContainsKey("HotMetalPouring"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.HotMetalPouringEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.HotMetalPouringFlag);
                    Repositary.Add("HotMetalPouring", true);
                }
                catch { return false; }
            }

            // Начало заливки чугуна
            if (_event.HotMetalPouringFlag == 0 && Repositary.ContainsKey("HotMetalPouring"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.HotMetalPouringEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.HotMetalPouringFlag);
                    Repositary.Remove("HotMetalPouring");
                }
                catch { return false; }
            }

            return true;
        }

        private bool ProcessEvent(ScrapChargingEvent _event)
        {
            // Начало загрузки скрапа 
            if (_event.ScrapChargingFlag == 1 && !Repositary.ContainsKey("ScrapCharging"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.ScrapChargingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.ScrapChargingFlag);
                    Repositary.Add("ScrapCharging", true);
                }
                catch { return false; }
            }

            // Конец загрузки скрапа 
            if (_event.ScrapChargingFlag == 0 && Repositary.ContainsKey("ScrapCharging"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.ScrapChargingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.ScrapChargingFlag);
                    Repositary.Remove("ScrapCharging");
                }
                catch { return false; }
            }


            return true;
        }

        private bool ProcessEvent(SublanceStartEvent _event)
        {
            try
            {
                DBWorker.Instance.Insert((int)HeatPassportOperations.SublanceStartEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.SublanceStartFlag);
            }
            catch { return false; }
            return true;
        }

        private bool ProcessEvent(SublanceCEvent _event)
        {
            try
            {
                DBWorker.Instance.Insert((int)HeatPassportOperations.SublanceCarbonEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.C);
            }
            catch { return false; }
            return true;
        }

        private bool ProcessEvent(SublanceOxidationEvent _event)
        {
            try
            {
                DBWorker.Instance.Insert((int)HeatPassportOperations.SublanceOxidationEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.O2InSteel);
            }
            catch { return false; }
            return true;
        }

        private bool ProcessEvent(SublanceTemperatureEvent _event)
        {
            try
            {
                DBWorker.Instance.Insert((int)HeatPassportOperations.SublanceTemperatureEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.SublanceTemperature);
            }
            catch { return false; }
            return true;
        }

        private bool ProcessEvent(TappingEvent _event)
        {
            // Начало слива стали 
            if (_event.TappingFlag == 1 && !Repositary.ContainsKey("TappingStartDate"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.TappingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.TappingFlag, 0);
                    Repositary.Add("TappingStartDate", _event.Time);
                }
                catch { return false; }
            }

            // Конец слива стали
            if (_event.TappingFlag == 0 && Repositary.ContainsKey("TappingStartDate"))
            {
                try
                {
                    DBWorker.Instance.Insert((int)HeatPassportOperations.TappingEvent, _event.Time, m_ConverterNumber, HeatNumber, _event.TappingFlag,
                        (_event.Time - (DateTime)Repositary["TappingStartDate"]).TotalSeconds);
                    Repositary.Remove("TappingStartDate");
                }
                catch { return false; }
            }
            return true;
        }

        
    }



}


