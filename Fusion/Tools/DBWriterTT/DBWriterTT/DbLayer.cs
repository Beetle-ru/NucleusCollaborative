using System;
using System.Collections.Generic;
using System.Data;
using NordSteel.Data;
using Oracle.DataAccess.Client;

namespace DBWriterTT
{
    class DbLayer : OracleDBLayer
    {
        internal enum OperationsId
        {
            AdditionsEvent = 1010,
            BlowingEvent = 1020,
            DeslaggingEvent = 1030,
            HeatChangeEvent = 1040,
            HeatingScrapEvent = 1050,
            HotMetalLadleEvent = 1060,
            HotMetalPouringEvent = 1070,
            IgnitionEvent = 1080,
            ResetO2TotalVolEvent = 1090,
            ScrapChargingEvent = 1100,
            ScrapEvent = 1110,
            SlagBlowingEvent = 1120,
            SublanceStartEvent = 1130,
            SublanceCarbonEvent = 1140,
            SublanceOxidationEvent = 1150,
            SublanceTemperatureEvent = 1160,
            TappingEvent = 1170
        }

        protected bool ExecuteNonQuery(string sql, List<OracleParameter> parameters)
        {
            return ExecuteNonQuery(sql, parameters.ToArray()) > -1;
        }

        protected bool CheckInsert(string tableName, int iCnvNo, DateTime time)
        {
            var sql = string.Format("SELECT CNV_NO, INSERTTIME FROM {0} WHERE CNV_NO = :CNV_NO AND INSERTTIME = :INSERTTIME", tableName);
            var parametres = MandatoryParams(iCnvNo, time);
            var res = Execute(sql, parametres.ToArray());
            var result = res.Read();
            res.Close();
            return result;
        }

        protected string GetLastHeatNumber(OperationsId operationId, int iCnvNo)
        {
            const string sql = "SELECT PAR1 FROM BOF_TELEGRAMS " +
                               "WHERE OPERATION_ID = :OPERATION_ID AND CV_NO = :CV_NO " +
                               "AND EVENTTIME = (SELECT MAX(EVENTTIME) FROM BOF_TELEGRAMS WHERE OPERATION_ID = :OPERATION_ID AND CV_NO = :CV_NO)";
            var parametres = MandatoryParams(operationId, iCnvNo);
            var res = Execute(sql, parametres.ToArray());
            var ret = res.Read() ? res[0].ToString() : "0";
            res.Close();
            return ret;
        }

        protected DateTime GetLastOperationTime(OperationsId operationId, int iCnvNo, string heatNumber, int par1, int par4)
        {
            const string sql = "SELECT MAX(EVENTTIME) FROM BOF_TELEGRAMS " +
                               "WHERE OPERATION_ID = :OPERATION_ID AND CV_NO = :CV_NO AND HEAT_NO = :HEAT_NO AND PAR1 = :PAR1 AND PAR4 = :PAR4 ";
            var parametres = MandatoryParams(operationId, iCnvNo, heatNumber, par1);
            parametres.Add(SetParams("PAR4", par4));
            var res = Execute(sql, parametres.ToArray());
            var ret = res.Read() ? DateTime.Parse(CheckDateForNull(res[0].ToString())) :DateTime.MinValue;
            res.Close();
            return ret;
        }

        protected DateTime GetLastOperationTime(OperationsId operationId, int iCnvNo, string heatNumber, int par1)
        {
            const string sql = "SELECT MAX(EVENTTIME) FROM BOF_TELEGRAMS " +
                               "WHERE OPERATION_ID = :OPERATION_ID AND CV_NO = :CV_NO AND HEAT_NO = :HEAT_NO AND PAR1 = :PAR1 ";
            var parametres = MandatoryParams(operationId, iCnvNo, heatNumber, par1);
            var res = Execute(sql, parametres.ToArray());
            var ret = res.Read() ? DateTime.Parse(CheckDateForNull(res[0].ToString())) : DateTime.MinValue;
            res.Close();
            return ret;
        }

       
        #region SetParams

        protected List<OracleParameter> MandatoryParams(int iCnvNo, DateTime time)
        {
            return new List<OracleParameter>
                       {
                           SetParams("CNV_NO", iCnvNo),
                           SetParams("INSERTTIME", time)
                       };
        }

        protected List<OracleParameter> MandatoryParams(OperationsId operationId, int iCnvNo)
        {
            return new List<OracleParameter>
                       {
                           SetParams("OPERATION_ID", operationId),
                           SetParams("CNV_NO", iCnvNo)
                       };
        }

        protected List<OracleParameter> MandatoryParams(OperationsId operationId, int iCnvNo, string heatNumber)
        {
            return new List<OracleParameter>
                       {
                           SetParams("OPERATION_ID", operationId),
                           SetParams("CNV_NO", iCnvNo),
                           SetParams("HEAT_NO", heatNumber)
                       };
        }

        protected List<OracleParameter> MandatoryParams(OperationsId operationId, int iCnvNo, string heatNumber, int par1)
        {
            return new List<OracleParameter>
                       {
                           SetParams("OPERATION_ID", operationId),
                           SetParams("CNV_NO", iCnvNo),
                           SetParams("HEAT_NO", heatNumber),
                           SetParams("PAR1", par1)
                       };
        }

        public static OracleParameter SetParams(string name, OperationsId value)
        {
            return new OracleParameter
            {
                ParameterName = name,
                Direction = ParameterDirection.Input,
                OracleDbType = OracleDbType.Int32,
                Value = value
            };
        }

        public static OracleParameter SetParams(string name, double value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Double,
                           Value = value
                       };
        }

        public static OracleParameter SetParams(string name, decimal value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Decimal,
                           Value = value
                       };
        }

        public static OracleParameter SetParams(string name, float value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Single,
                           Value = value
                       };
        }

        public static OracleParameter SetParams(string name, string value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Varchar2,
                           Value = value
                       };
        }

        public static OracleParameter SetParams(string name, Int16 value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Int16,
                           Value = value
                       };
        }

        public static OracleParameter SetParams(string name, Int32 value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Int32,
                           Value = value
                       };
        }

        public static OracleParameter SetParams(string name, Int64 value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Int64,
                           Value = value
                       };
        }

        public static OracleParameter SetParams(string name, DateTime value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Date,
                           Value = value
                       };
        }

        public static OracleParameter SetParams(string name, bool value)
        {
            return new OracleParameter
                       {
                           ParameterName = name,
                           Direction = ParameterDirection.Input,
                           OracleDbType = OracleDbType.Int32,
                           Value = value ? 1 : 0
                       };
        }

        # endregion

    }
}
