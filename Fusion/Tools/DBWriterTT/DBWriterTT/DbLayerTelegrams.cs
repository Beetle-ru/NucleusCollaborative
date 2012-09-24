using System;
using System.Collections.Generic;
using System.Linq;
using Converter;
using Oracle.DataAccess.Client;

namespace DBWriterTT
{
    class DbLayerTelegrams : DbLayer
    {
        private static readonly DbLayerTelegrams Instanc = new DbLayerTelegrams();

        public static DbLayerTelegrams Instance
        {
            get
            {
                return Instanc;
            }
        }

        public bool Insert(ConverterBaseEvent evt)
        {
            var heatNumber = GetLastHeatNumber(OperationsId.HeatChangeEvent, evt.iCnvNr);
            if (heatNumber == "0" && evt.GetType().Name != "HeatChangeEvent") { return false;}
            var res = false;
            switch (evt.GetType().Name)
            {
                case "AdditionsEvent":
                    {
                        var e = (AdditionsEvent)evt;
                        return true;
                    }
                case "BlowingEvent":
                    {
                        var e = (BlowingEvent) evt;
                        var start = GetLastOperationTime(OperationsId.BlowingEvent, e.iCnvNr, heatNumber, 1, 1);
                        var end = GetLastOperationTime(OperationsId.BlowingEvent, e.iCnvNr, heatNumber, 0, 1);
                        var period = start != DateTime.MinValue ? (e.Time - start).TotalSeconds : 0;
                        if ((e.BlowingFlag == 1 && start == DateTime.MinValue) || (e.BlowingFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.BlowingFlag.ToString(), e.O2TotalVol.ToString(), period == 0 ? "" : period.ToString(), "1" };
                            res = InsertTelegrams(OperationsId.BlowingEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "ReBlowingEvent":
                    {
                        var e = (ReBlowingEvent)evt;
                        var start = GetLastOperationTime(OperationsId.BlowingEvent, e.iCnvNr, heatNumber, 1, 0);
                        var end = GetLastOperationTime(OperationsId.BlowingEvent, e.iCnvNr, heatNumber, 0, 0);
                        var period = start != DateTime.MinValue ? (e.Time - start).TotalSeconds : 0;
                        if ((e.BlowingFlag == 1 && start == DateTime.MinValue) || (e.BlowingFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.BlowingFlag.ToString(), e.O2TotalVol.ToString(), period == 0 ? "" : period.ToString(), "0" };
                            res = InsertTelegrams(OperationsId.BlowingEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "DeslaggingEvent":
                    {
                        var e = (DeslaggingEvent)evt;
                        var start = GetLastOperationTime(OperationsId.DeslaggingEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.DeslaggingEvent, e.iCnvNr, heatNumber, 0);
                        var period = start != DateTime.MinValue ? (e.Time - start).TotalSeconds : 0;
                        if ((e.DeslaggingFlag == 1 && start == DateTime.MinValue) || (e.DeslaggingFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.DeslaggingFlag.ToString(), period == 0 ? "" : period.ToString() };
                            res = InsertTelegrams(OperationsId.DeslaggingEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "HeatChangeEvent":
                    {
                        var e = (HeatChangeEvent)evt;
                        var param = new[] { e.HeatNumber.ToString().Insert(2, "0") };
                        res = InsertTelegrams(OperationsId.HeatChangeEvent, e.Time, e.iCnvNr, heatNumber, param);
                        break;
                    }
                case "HeatingScrapEvent":
                    {
                        var e = (HeatingScrapEvent)evt;
                        var start = GetLastOperationTime(OperationsId.HeatingScrapEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.HeatingScrapEvent, e.iCnvNr, heatNumber, 0);
                        if ((e.HeatingScrapFlag == 1 && start == DateTime.MinValue) || (e.HeatingScrapFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.HeatingScrapFlag.ToString() };
                            res = InsertTelegrams(OperationsId.HeatingScrapEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "HotMetalLadleEvent":
                    {
                        var e = (HotMetalLadleEvent)evt;
                        if (e.MixerNumberPortion1 != 0)
                        {
                            var param = new[] { e.LadleNumber.ToString(), e.HotMetalTemperature.ToString(), e.MixerNumberPortion1.ToString(), e.WeightPortion1.ToString() };
                            res = InsertTelegrams(OperationsId.HotMetalLadleEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.MixerNumberPortion2 != 0)
                        {
                            var param = new[] { e.LadleNumber.ToString(), e.HotMetalTemperature.ToString(), e.MixerNumberPortion2.ToString(), e.WeightPortion2.ToString() };
                            res = InsertTelegrams(OperationsId.HotMetalLadleEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.MixerNumberPortion3 != 0)
                        {
                            var param = new[] { e.LadleNumber.ToString(), e.HotMetalTemperature.ToString(), e.MixerNumberPortion3.ToString(), e.WeightPortion3.ToString() };
                            res = InsertTelegrams(OperationsId.HotMetalLadleEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        break;
                    }
                case "HotMetalPouringEvent":
                    {
                        var e = (HotMetalPouringEvent)evt;
                        var start = GetLastOperationTime(OperationsId.HotMetalPouringEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.HotMetalPouringEvent, e.iCnvNr, heatNumber, 0);
                        if ((e.HotMetalPouringFlag == 1 && start == DateTime.MinValue) || (e.HotMetalPouringFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.HotMetalPouringFlag.ToString() };
                            res = InsertTelegrams(OperationsId.HotMetalPouringEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "IgnitionEvent":
                    {
                        var e = (IgnitionEvent)evt;
                        if (e.O2IgnitionVol != 0)
                        {
                            var param = new[] { e.O2IgnitionVol.ToString() };
                            res = InsertTelegrams(OperationsId.IgnitionEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "ResetO2TotalVolEvent":
                    {
                        var e = (ResetO2TotalVolEvent)evt;
                        var param = new[] { e.O2TotalVol.ToString() };
                        res = InsertTelegrams(OperationsId.ResetO2TotalVolEvent, e.Time, e.iCnvNr, heatNumber, param);
                        break;
                    }
                case "ScrapChargingEvent":
                    {
                        var e = (ScrapChargingEvent)evt;
                        var start = GetLastOperationTime(OperationsId.ScrapChargingEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.ScrapChargingEvent, e.iCnvNr, heatNumber, 0);
                        if ((e.ScrapChargingFlag == 1 && start == DateTime.MinValue) || (e.ScrapChargingFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.ScrapChargingFlag.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapChargingEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "ScrapEvent":
                    {
                        var e = (ScrapEvent)evt;
                        if (e.ScrapType1 != 0)
                        {
                            var param = new[] { e.BucketNumber.ToString(), e.ScrapType1.ToString(), e.Weight1.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.ScrapType2 != 0)
                        {
                            var param = new[] { e.BucketNumber.ToString(), e.ScrapType2.ToString(), e.Weight2.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.ScrapType3 != 0)
                        {
                            var param = new[] { e.BucketNumber.ToString(), e.ScrapType3.ToString(), e.Weight3.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.ScrapType4 != 0)
                        {
                            var param = new[] { e.BucketNumber.ToString(), e.ScrapType4.ToString(), e.Weight4.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.ScrapType5 != 0)
                        {
                            var param = new[] { e.BucketNumber.ToString(), e.ScrapType5.ToString(), e.Weight5.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.ScrapType6 != 0)
                        {
                            var param = new[] { e.BucketNumber.ToString(), e.ScrapType6.ToString(), e.Weight6.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.ScrapType7 != 0)
                        {
                            var param = new[] { e.BucketNumber.ToString(), e.ScrapType7.ToString(), e.Weight7.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        if (e.ScrapType8 != 0)
                        {
                            var param = new[] { e.BucketNumber.ToString(), e.ScrapType8.ToString(), e.Weight8.ToString() };
                            res = InsertTelegrams(OperationsId.ScrapEvent, e.Time, e.iCnvNr, heatNumber, param) || res;
                        }
                        break;
                    }
                case "SlagBlowingEvent":
                    {
                        var e = (SlagBlowingEvent)evt;
                        var start = GetLastOperationTime(OperationsId.SlagBlowingEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.SlagBlowingEvent, e.iCnvNr, heatNumber, 0);
                        if ((e.SlagBlowingFlag == 1 && start == DateTime.MinValue) || (e.SlagBlowingFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.SlagBlowingFlag.ToString(), e.NVol.ToString() };
                            res = InsertTelegrams(OperationsId.SlagBlowingEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "SublanceStartEvent":
                    {
                        var e = (SublanceStartEvent)evt;
                        var start = GetLastOperationTime(OperationsId.SublanceStartEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.SublanceStartEvent, e.iCnvNr, heatNumber, 0);
                        if ((e.SublanceStartFlag == 1 && start == DateTime.MinValue) || (e.SublanceStartFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.SublanceStartFlag.ToString() };
                            res = InsertTelegrams(OperationsId.SublanceStartEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "SublanceCEvent":
                    {
                        var e = (SublanceCEvent)evt;
                        if (e.C != 0)
                        {
                            var param = new[] {e.C.ToString()};
                            res = InsertTelegrams(OperationsId.SublanceCarbonEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "SublanceOxidationEvent":
                    {
                        var e = (SublanceOxidationEvent)evt;
                        var start = GetLastOperationTime(OperationsId.SublanceStartEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.SublanceStartEvent, e.iCnvNr, heatNumber, 0);
                        if (e.O2InSteel != 0 && end == DateTime.MinValue && start != DateTime.MinValue)
                        {
                            var param = new[] { e.O2InSteel.ToString() };
                            res = InsertTelegrams(OperationsId.SublanceOxidationEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "SublanceTemperatureEvent":
                    {
                        var e = (SublanceTemperatureEvent)evt;
                        var start = GetLastOperationTime(OperationsId.SublanceStartEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.SublanceStartEvent, e.iCnvNr, heatNumber, 0);
                        if (e.SublanceTemperature != 0 && end == DateTime.MinValue && start != DateTime.MinValue)
                        {
                            var param = new[] { e.SublanceTemperature.ToString() };
                            res = InsertTelegrams(OperationsId.SublanceTemperatureEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
                case "TappingEvent":
                    {
                        var e = (TappingEvent)evt;
                        var start = GetLastOperationTime(OperationsId.TappingEvent, e.iCnvNr, heatNumber, 1);
                        var end = GetLastOperationTime(OperationsId.TappingEvent, e.iCnvNr, heatNumber, 0);
                        var period = start != DateTime.MinValue ? (e.Time - start).TotalSeconds : 0;
                        if ((e.TappingFlag == 1 && start == DateTime.MinValue) || (e.TappingFlag == 0 && end == DateTime.MinValue && start != DateTime.MinValue))
                        {
                            var param = new[] { e.TappingFlag.ToString(), period == 0 ? "" : period.ToString() };
                            res = InsertTelegrams(OperationsId.TappingEvent, e.Time, e.iCnvNr, heatNumber, param);
                        }
                        break;
                    }
           }
           return res;
        }

        private bool InsertTelegrams(OperationsId oparationId, DateTime eventTime, int iCnvNr, string heatNumber, ICollection<string> param)
        {
            const string sql = "INSERT INTO BOF_TELEGRAMS (OPERATION_ID, CV_NO, HEAT_NO, EVENTTIME, PAR1, PAR2, PAR3, PAR4, PAR5, PAR6, PAR7, PAR8, PAR9) " +
                               "VALUES (:OPERATION_ID, :CV_NO, :HEAT_NO, :EVENTTIME, :PAR1, :PAR2, :PAR3, :PAR4, :PAR5, :PAR6, :PAR7, :PAR8, :PAR9) ";
            var parametres = MandatoryParams(oparationId, iCnvNr, heatNumber);
            parametres.Add(SetParams("EVENTTIME", eventTime));
            parametres.AddRange(param.Select((t, i) => SetParams(string.Format("PAR{0}", i + 1), t)));
            for (var i = param.Count; i < 9; i++)
            {
                parametres.Add(SetParams(string.Format("PAR{0}", i + 1), ""));
            }
            return ExecuteNonQuery(sql, parametres.ToArray()) > -1;
        }


    }
}
