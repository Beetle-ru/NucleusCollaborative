using System;
using CommonTypes;
using ConnectionProvider;
using Converter;
using Implements;
using Oracle.DataAccess.Client;

namespace DBWriterTT
{
    class EventsListener: IEventListener
    {
        private readonly int _unitNumber;

        public EventsListener(string unit)
        {
            _unitNumber = GetUnitNumber(unit);
        }

        private static int GetUnitNumber(string unit)
        {
            var unitNum = unit;
            for (var i = unit.Length - 1; i >= 0; i--)
            {
                int d;
                if (!int.TryParse(unit[i].ToString(), out d))
                {
                    unitNum = unitNum.Remove(i, 1);
                }
            }
            return int.Parse(unitNum);
        }

        public void OnEvent(BaseEvent newEvent)
        {
            using (var log = new Logger("DBWriterTT"))
            {
                try
                {
                    var evtFlex = newEvent as FlexEvent;
                    if (evtFlex != null)
                    {
                        try
                        {
                            if (DbLayerTrends.Instance.Insert(evtFlex, _unitNumber))
                            {
                                log.msg(string.Format("{0}: inserted trends for {1} from {2}", newEvent.Time, newEvent.GetType().Name, _unitNumber));
                            }
                        }
                        catch (OracleException exception)
                        {
                            log.msg(string.Format("{0}: Can't inserted trends for {1}  from {2}", newEvent.Time, newEvent.GetType().Name, _unitNumber));
                            log.err(exception.ToString());
                        }
                    }
                    else
                    {

                        var evt = (ConverterBaseEvent) newEvent;
                        evt.iCnvNr = _unitNumber;
                        if ((evt is LanceEvent) ||
                            (evt is OffGasAnalysisEvent) ||
                            (evt is OffGasEvent) ||
                            (evt is BoilerWaterCoolingEvent) ||
                            (evt is SlagOutburstEvent) ||
                            (evt is IgnitionEvent) ||
                            (evt is ModeLanceEvent) ||
                            (evt is ModeVerticalPathEvent) ||
                            (evt is CalculatedCarboneEvent) ||
                            (evt is ConverterAngleEvent))
                        {

                            try
                            {
                                if (DbLayerTrends.Instance.Insert(evt))
                                {
                                    log.msg(string.Format("{0}: inserted trends for {1} from {2}", evt.Time, evt.GetType().Name, evt.iCnvNr));
                                }
                            }
                            catch (OracleException exception)
                            {
                                log.msg(string.Format("{0}: Can't inserted trends for {1}  from {2}", evt.Time,
                                                      evt.GetType().Name, evt.iCnvNr));
                                log.err(exception.ToString());
                            }
                        }
                        
                        if ((evt is AdditionsEvent) ||
                            (evt is BlowingEvent) ||
                            (evt is ReBlowingEvent) ||
                            (evt is DeslaggingEvent) ||
                            (evt is HeatChangeEvent) ||
                            (evt is HeatingScrapEvent) ||
                            (evt is HotMetalLadleEvent) ||
                            (evt is HotMetalPouringEvent) ||
                            (evt is IgnitionEvent) ||
                            (evt is ResetO2TotalVolEvent) ||
                            (evt is ScrapChargingEvent) ||
                            (evt is ScrapEvent) ||
                            (evt is SlagBlowingEvent) ||
                            (evt is SublanceStartEvent) ||
                            (evt is SublanceCEvent) ||
                            (evt is SublanceOxidationEvent) ||
                            (evt is SublanceTemperatureEvent) ||
                            (evt is TappingEvent))
                        {
                            try
                            {
                                if (DbLayerTelegrams.Instance.Insert(evt))
                                {
                                    log.msg(string.Format("{0}: inserted telergams for {1} from {2}", evt.Time,
                                                          evt.GetType().Name, evt.iCnvNr));
                                }
                            }
                            catch (OracleException exception)
                            {
                                log.msg(string.Format("{0}: Can't inserted telergams trends for {1}  from {2}", evt.Time,
                                                      evt.GetType().Name, evt.iCnvNr));
                                log.err(exception.ToString());
                            }
                        }
                        
                    }
                }
                catch(Exception ex)
                {
                    log.err(ex.ToString());
                }
            }
        }
    }
}
