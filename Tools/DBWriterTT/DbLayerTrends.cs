using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommonTypes;
using Converter;
using NordSteel.Data;
using Oracle.DataAccess.Client;

namespace DBWriterTT
{
    internal class DbLayerTrends : DbLayer
    {

        private static readonly DbLayerTrends Instanc = new DbLayerTrends();

        public static DbLayerTrends Instance
        {
            get
            {
                return Instanc;
            }
        }

         public bool Insert(FlexEvent evt, int unit)
         {
             return evt.Operation.StartsWith("Model.Dynamic.Output.PerSecond") && InsertOrUpdateTrends(evt, unit);
         }

        public bool Insert(ConverterBaseEvent evt)
        {
            switch (evt.GetType().Name)
            {
                case "LanceEvent": { return InsertOrUpdateTrends((LanceEvent)evt); }
                case "OffGasAnalysisEvent": { return InsertOrUpdateTrends((OffGasAnalysisEvent) evt); }
                case "OffGasEvent": { return InsertOrUpdateTrends((OffGasEvent)evt); }
                case "BoilerWaterCoolingEvent": { return InsertOrUpdateTrends((BoilerWaterCoolingEvent)evt); }
                case "SlagOutburstEvent": { return InsertOrUpdateTrends((SlagOutburstEvent)evt); }
                case "IgnitionEvent": { return InsertOrUpdateTrends((IgnitionEvent)evt); }
                case "ModeLanceEvent": { return InsertOrUpdateTrends((ModeLanceEvent)evt); }
                case "ModeVerticalPathEvent": { return InsertOrUpdateTrends((ModeVerticalPathEvent)evt); }
                case "ConverterAngleEvent": { return InsertOrUpdateTrends((ConverterAngleEvent)evt); }
                case "CalculatedCarboneEvent": { return InsertOrUpdateTrends((CalculatedCarboneEvent)evt); }
            }
            return false;
        }

        #region InsetrTrends

        private bool InsertOrUpdateTrends(LanceEvent evt)
        {
            var sql = CheckInsert("TREND_LANCE", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_LANCE " +
                            "SET  O2TOTALVOL = :O2TOTALVOL, O2FLOW = :O2FLOW, O2PRESSURE = :O2PRESSURE, LANCEHEIGHT = :LANCEHEIGHT, LANCEMODE = :LANCEMODE, " +
                            "O2FLOWMODE = :O2FLOWMODE, LEFTWATERINP = :LEFTWATERINP, LEFTWATEROUT = :LEFTWATEROUT, LEFTWATERTEMPINP = :LEFTWATERTEMPINP, " +
                            "LEFTWATERTEMPOUTP = :LEFTWATERTEMPOUTP, LEFTLECK = :LEFTLECK, RIGHTLECK = :RIGHTLECK, LEFTWATERPRESS = :LEFTWATERPRESS, " +
                            "RIGHTWATERPRESS = :RIGHTWATERPRESS, RIGHTWATERINP = :RIGHTWATERINP, RIGHTWATEROUT = :RIGHTWATEROUT, " +
                            "RIGHTWATERTEMPINP = :RIGHTWATERTEMPINP, RIGHTWATERTEMPOUTP = :RIGHTWATERTEMPOUTP, LEFTWEIGHT = :LEFTWEIGHT, LEFTGOAT = :LEFTGOAT, " +
                            "RIGHTWEIGHT = :RIGHTWEIGHT, RIGHTGOAT = :RIGHTGOAT, BATHLEVEL = :BATHLEVEL " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_LANCE(O2TOTALVOL, O2FLOW, O2PRESSURE, LANCEHEIGHT, LANCEMODE, O2FLOWMODE, LEFTWATERINP, LEFTWATEROUT, " +
                            "LEFTWATERTEMPINP, LEFTWATERTEMPOUTP, LEFTLECK, RIGHTLECK, LEFTWATERPRESS, RIGHTWATERPRESS, RIGHTWATERINP, RIGHTWATEROUT, " +
                            "RIGHTWATERTEMPINP, RIGHTWATERTEMPOUTP, LEFTWEIGHT, LEFTGOAT, RIGHTWEIGHT, RIGHTGOAT, BATHLEVEL, CNV_NO, INSERTTIME) " +
                            "VALUES (:O2TOTALVOL, :O2FLOW, :O2PRESSURE, :LANCEHEIGHT, :LANCEMODE, :O2FLOWMODE, :LEFTWATERINP, :LEFTWATEROUT, " +
                            ":LEFTWATERTEMPINP, :LEFTWATERTEMPOUTP, :LEFTLECK, :RIGHTLECK, :LEFTWATERPRESS, :RIGHTWATERPRESS, :RIGHTWATERINP, :RIGHTWATEROUT, " +
                            ":RIGHTWATERTEMPINP, :RIGHTWATERTEMPOUTP, :LEFTWEIGHT, :LEFTGOAT, :RIGHTWEIGHT, :RIGHTGOAT, :BATHLEVEL, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter>
                                 {
                                     SetParams("O2TOTALVOL", evt.O2TotalVol),
                                     SetParams("O2FLOW", evt.O2Flow),
                                     SetParams("O2PRESSURE", evt.O2Pressure),
                                     SetParams("LANCEHEIGHT", evt.LanceHeight),
                                     SetParams("LANCEMODE", evt.LanceMode),
                                     SetParams("O2FLOWMODE", evt.O2FlowMode),
                                     SetParams("LEFTWATERINP", evt.O2LeftLanceWaterInput),
                                     SetParams("LEFTWATEROUT", evt.O2LeftLanceWaterOutput),
                                     SetParams("LEFTWATERTEMPINP", evt.O2LeftLanceWaterTempInput),
                                     SetParams("LEFTWATERTEMPOUTP", evt.O2LeftLanceWaterTempOutput),
                                     SetParams("LEFTLECK", evt.O2LeftLanceLeck),
                                     SetParams("RIGHTLECK", evt.O2RightLanceLeck),
                                     SetParams("LEFTWATERPRESS", evt.O2LeftLanceWaterPressure),
                                     SetParams("RIGHTWATERPRESS", evt.O2RightLanceWaterPressure),
                                     SetParams("RIGHTWATERINP", evt.O2RightLanceWaterInput),
                                     SetParams("RIGHTWATEROUT", evt.O2RightLanceWaterOutput),
                                     SetParams("RIGHTWATERTEMPINP", evt.O2RightLanceWaterTempInput),
                                     SetParams("RIGHTWATERTEMPOUTP", evt.O2LeftLanceWaterTempOutput),
                                     SetParams("LEFTWEIGHT", evt.O2LeftLanceGewWeight),
                                     SetParams("LEFTGOAT", evt.O2LeftLanceGewBaer),
                                     SetParams("RIGHTWEIGHT", evt.O2RightLanceGewWeight),
                                     SetParams("RIGHTGOAT", evt.O2RightLanceGewBaer),
                                     SetParams("BATHLEVEL", evt.BathLevel)
                                 };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(OffGasAnalysisEvent evt)
        {
            var sql = CheckInsert("TREND_OFFGASANALYSE", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_OFFGASANALYSE " +
                            "SET H2 = :H2, O2 = :O2, CO = :CO, CO2 = :CO2, N2 = :N2, AR = :AR " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_OFFGASANALYSE(H2, O2, CO, CO2, N2, AR, CNV_NO, INSERTTIME) " +
                            "VALUES (:H2, :O2, :CO, :CO2, :N2, :AR, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter>
                                 {
                                     SetParams("H2", evt.H2),
                                     SetParams("O2", evt.O2),
                                     SetParams("CO", evt.CO),
                                     SetParams("CO2", evt.CO2),
                                     SetParams("N2", evt.N2),
                                     SetParams("AR", evt.Ar)
                                 };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(OffGasEvent evt)
        {
            var sql = (CheckInsert("TREND_OFFGAS", evt.iCnvNr, evt.Time))
                          ? "UPDATE TREND_OFFGAS " +
                            "SET FLOW = :FLOW, TEMP = :TEMP, HOODPOS = :HOODPOS, FILTER = :FILTER, CNT = :CNT " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_OFFGAS(FLOW, TEMP, HOODPOS, FILTER, CNT, CNV_NO, INSERTTIME) " +
                            "VALUES (:FLOW, :TEMP, :HOODPOS, :FILTER, :CNT, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter>
                                 {
                                     SetParams("FLOW", evt.OffGasFlow),
                                     SetParams("TEMP", evt.OffGasTemp),
                                     SetParams("HOODPOS", evt.OffGasHoodPos),
                                     SetParams("FILTER", evt.OffGasFilterControlPos),
                                     SetParams("CNT", evt.OffGasCounter)
                                 };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(BoilerWaterCoolingEvent evt)
        {
            var sql = CheckInsert("TREND_OFFGAS", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_OFFGAS " +
                            "SET TEMPONEXIT = :TEMPONEXIT, TEMPPRECOLLING = :TEMPPRECOLLING, TEMPSTEP1 = :TEMPSTEP1, TEMPSTEP2 = :TEMPSTEP2 " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_OFFGAS(TEMPONEXIT, TEMPPRECOLLING, TEMPSTEP1, TEMPSTEP2, CNV_NO, INSERTTIME) " +
                            "VALUES (:TEMPONEXIT, :TEMPPRECOLLING, :TEMPSTEP1, :TEMPSTEP2, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter>
                                 {
                                     SetParams("TEMPONEXIT", evt.GasTemperatureOnExit),
                                     SetParams("TEMPPRECOLLING", evt.PrecollingGasTemperature),
                                     SetParams("TEMPSTEP1", evt.GasTemperatureAfter1Step),
                                     SetParams("TEMPSTEP2", evt.GasTemperatureAfter2Step)
                                 };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(IgnitionEvent evt)
        {
            var sql = CheckInsert("TREND_MODES", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_MODES " +
                            "SET IGNITION = :IGNITION " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_MODES(IGNITION, CNV_NO, INSERTTIME) " +
                            "VALUES (:IGNITION, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter> { SetParams("IGNITION", evt.FusionIgnition) };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(SlagOutburstEvent evt)
        {
            var sql = CheckInsert("TREND_MODES", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_MODES " +
                            "SET SLAGOUT = :SLAGOUT " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_MODES(SLAGOUT, CNV_NO, INSERTTIME) " +
                            "VALUES (:SLAGOUT, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter> { SetParams("SLAGOUT", evt.Counter) };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(ModeLanceEvent evt)
        {
            var sql = CheckInsert("TREND_MODES", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_MODES " +
                            "SET O2FLOW = :O2FLOW " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_MODES(O2FLOW, CNV_NO, INSERTTIME) " +
                            "VALUES (:O2FLOW, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter> { SetParams("O2FLOW", evt.O2FlowMode) };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(ModeVerticalPathEvent evt)
        {
            var sql = CheckInsert("TREND_MODES", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_MODES " +
                            "SET TRACT = :TRACT " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_MODES(TRACT, CNV_NO, INSERTTIME) " +
                            "VALUES (:TRACT, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter> { SetParams("TRACT", evt.VerticalPathMode) };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(ConverterAngleEvent evt)
        {
            var sql = CheckInsert("TREND_MODES", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_MODES " +
                            "SET ANGLE = :ANGLE " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_MODES(ANGLE, CNV_NO, INSERTTIME) " +
                            "VALUES (:ANGLE, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter> { SetParams("ANGLE", evt.Angle) };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(FlexEvent evt, int unit)
        {
            var sql = CheckInsert("TREND_BALANCE", unit, evt.Time)
                          ? "UPDATE TREND_BALANCE " +
                            "SET  C = :C, T = :T, SI = :SI, MN = :MN, AL = :AL, CR = :CR, P = :P, TI = :TI, V = :V, FE = :FE, " +
                            "CAO = :CAO, FEO = :FEO, SIO2 = :SIO2, MNO = :MNO, MGO = :MGO " +
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_BALANCE (C, T, SI, MN, AL, CR, P, TI, V, FE, CAO, FEO, SIO2, MNO, MGO, CNV_NO, INSERTTIME) " +
                            "VALUES (:C, :T, :SI, :MN, :AL, :CR, :P, :TI, :V, :FE, :CAO, :FEO, :SIO2, :MNO, :MGO, :CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter>(evt.Arguments.Keys.Select(key => SetParams(key.ToUpper(), (double) evt.Arguments[key])));
            parametres.AddRange(MandatoryParams(unit, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        private bool InsertOrUpdateTrends(CalculatedCarboneEvent evt)
        {
            var sql = CheckInsert("TREND_STATISTIC", evt.iCnvNr, evt.Time)
                          ? "UPDATE TREND_STATISTIC " +
                            "SET C = :C " + /*, T = :T, TEMPINBOILER = :TEMPINBOILER, DTEMPINBOILER = :DTEMPINBOILER, DTEMPINBLOW = :DTEMPINBLOW  " +*/
                            "WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME "
                          : "INSERT INTO TREND_STATISTIC (C, " + /*T, TEMPINBOILER, DTEMPINBOILER, DTEMPINBLOW, */"CNV_NO, INSERTTIME) " +
                            "VALUES (:C, " + /*:T, :TEMPINBOILER, :DTEMPINBOILER, :DTEMPINBLOW, */":CNV_NO, :INSERTTIME) ";
            var parametres = new List<OracleParameter>
                                  {
                                     SetParams("C", evt.CarbonePercent),
                                     /*Этих данных пока нет
                                     SetParams("T", events.iCnvNr),
                                     SetParams("TEMPINBOILER", events.iCnvNr),
                                     SetParams("DTEMPINBOILER", events.iCnvNr),
                                     SetParams("DTEMPINBLOW", events.iCnvNr),
                                     */
                                 };
            parametres.AddRange(MandatoryParams(evt.iCnvNr, evt.Time));
            return ExecuteNonQuery(sql, parametres);
        }

        #endregion

    }
}

