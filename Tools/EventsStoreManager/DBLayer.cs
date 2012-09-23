using System;
using System.Collections.Generic;
using System.Linq;
using NordSteel.Data;
using System.Reflection;
using Oracle.DataAccess.Client;
using Core;
using CommonTypes;
using Converter;


namespace EventsStoreManager
{
    class DBLayer : OracleDBLayer
    {
        private List<PropertyInfo> GetDBPointProperties(Type eventType)
        {
            var result = new List<PropertyInfo>();
            foreach (var property in eventType.GetProperties())
            {
                var dbPoint = (DBPoint)property.GetCustomAttributes(false).FirstOrDefault(p => p.GetType().Name == "DBPoint");

                if (dbPoint != null && dbPoint.IsStored)
                {
                    result.Add(property);
                }
            }
            return result;
        }

        private string PropertyValueToSqlString(BaseEvent _event, string propertyName)
        {
            var propertyType = _event.GetType().GetProperty(propertyName).PropertyType;
            var propertyValue = _event.GetType().GetProperty(propertyName).GetValue(_event,null);
            return (propertyType.Name.ToLower() == "datetime" || propertyType.Name.ToLower() == "string") ? string.Format("'{0}'", propertyValue) : string.Format("{0}", propertyValue);
        }

        public List<HeatChangeEvent> GetHeatList(DateTime startDate, DateTime endDate, int unitNumber)
        {
            var sql = string.Format("SELECT Time, HeatNumber FROM HeatChangeEvent{0} WHERE TIME BETWEEN {1} AND {2} ORDER BY Time", unitNumber, OracleDate(startDate), OracleDate(endDate));
            var reader = Execute(sql);
            var result = new List<HeatChangeEvent>();
            while (reader.Read())
            {
                result.Add(new HeatChangeEvent
                               {
                                   Time = Convert.ToDateTime(CheckDateForNull(reader[0].ToString())), 
                                   HeatNumber = int.Parse(CheckNubmerForNull(reader[1].ToString()))
                               });
            }
            return result;
        }

        public bool Insert(BaseEvent _event, int unitNumber)
        {
            try
            {
                var sql = string.Format("INSERT INTO {0}{1} (Time", _event.GetType().Name, unitNumber);
                var dataSql = "";
                foreach (var property in GetDBPointProperties(_event.GetType()))
                {
                    sql += string.Format(",{0}", property.Name);
                    dataSql += string.Format(",{0}", PropertyValueToSqlString(_event, property.Name));
                }
                sql += string.Format(") VALUES ({0}{1})", OracleDate(_event.Time), dataSql);
                ExecuteNonQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Insert(ICollection<BaseEvent> events, int unitNumber)
        {
            try
            {
                var sql = "INSERT ALL ";
                foreach (var _event in events)
                {
                    sql += string.Format(" INTO {0}{1} (Time", _event.GetType().Name, unitNumber);
                    var dataSql = "";
                    foreach (var property in GetDBPointProperties(_event.GetType()))
                    {
                        sql += string.Format(",{0}", property.Name);
                        dataSql += string.Format(",{0}", PropertyValueToSqlString(_event, property.Name));
                    }
                    sql += string.Format(") VALUES ({0}{1})", OracleDate(_event.Time), dataSql);
                }
                sql += " SELECT * FROM DUAL";
                Execute(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CheckTables(Type[] types, int unitNumber)
        {
            foreach (var type in types.Where(type => !TableExist(type.Name + unitNumber.ToString())))
            {
                CreateTable(type);
            }
        }

        public void CreateTable(Type type)
        {
            try
            {
                var data = type.GetCustomAttributes(false).Where(x => x.GetType().Name == "DBGroup").ToArray();
                foreach (var item in data)
                {
                    if (item == null) continue;
                    if (TableExist(type.Name + ((DBGroup)item).UnitNumber.ToString())) return;
                    var sql = string.Format("CREATE TABLE {0}{1} (Time DATE NOT NULL", type.Name, ((DBGroup)item).UnitNumber);
                    foreach (var property in type.GetProperties().Where(p => p.Name != "Time"))
                    {
                        var dbPoint = (DBPoint)property.GetCustomAttributes(false).FirstOrDefault(p => p.GetType().Name == "DBPoint");
                        if (dbPoint == null || !dbPoint.IsStored) continue;
                        sql += string.Format(", {0} {1}", property.Name, ToOracleTypeString(property.PropertyType, dbPoint.MaxSize));
                    }
                    sql += ")";
                    ExecuteNonQuery(sql);
                }
            }
            catch (Exception)
            {
            }
        }

        public void DropTable(Type type)
        {
            try
            {
                var data = type.GetCustomAttributes(false).Where(x => x.GetType().Name == "DBGroup").ToArray();
                foreach (var item in data)
                {
                    if (item == null) continue;
                    if (!TableExist(type.Name + ((DBGroup)item).UnitNumber.ToString())) return;
                    var sql = string.Format("DROP TABLE {0}{1} PURGE", type.Name.ToUpper(), ((DBGroup)item).UnitNumber);
                    ExecuteNonQuery(sql);
                }
            }
            catch { }
        }

        private bool TableExist(string tableName)
        {
            try
            {
                var ret = false;
                var sql = string.Format("SELECT COUNT(*) FROM user_tables WHERE table_name='{0}'", tableName.ToUpper());
                var reader = Execute(sql);
                if (reader.Read())
                {
                    ret = int.Parse((CheckNubmerForNull(reader[0].ToString()))) > 0;
                }
                reader.Close();
                return ret;
            }
            catch 
            {
                return false;
            }
        }

        public List<Addition> GetAdditions(int heatID)
        {
            var result = new List<Addition>();
            var sql = "SELECT HA.HTADDACT_ID, HA.HEAT_ID, HA.MAT_ID, HA.PHASE_NO, HA.DESTINATION_AGGNO, HA.DATA_SOURCE, ";
            sql += "HA.INSERTTIME, HA.PORTION_WGT, HA.TOTAL_WGT, MS.NAME_ENGLISH, HA.LANZE_POS, HA.O2VOL_TOTAL ";
            sql += "FROM SMK.HEAT_ADDITIONS_ACT HA, SMK.MATERIAL_SPEC MS ";
            sql += "WHERE MS.MAT_ID = HA.MAT_ID AND HA.HEAT_ID=:HeatID ORDER BY INSERTTIME ";
            var reader = Execute(sql, new[] { 
                new OracleParameter { 
                    OracleDbType = OracleDbType.Int32, ParameterName = "HeatID", 
                    Direction= System.Data.ParameterDirection.Input, Value = heatID 
                } 
            });
            while (reader.Read())
            {
                result.Add(new Addition
                {
                    Id = int.Parse(CheckNubmerForNull(reader[0].ToString())),
                    FusionId = int.Parse(CheckNubmerForNull(reader[1].ToString())),
                    MaterialId = int.Parse(CheckNubmerForNull(reader[2].ToString())),
                    Destination = reader[4].ToString(),
                    DataSource = reader[5].ToString(),
                    Date = DateTime.Parse(CheckDateForNull(reader[6].ToString())),
                    PortionWeight = int.Parse(CheckNubmerForNull(reader[7].ToString())),
                    TotalWeight = int.Parse(CheckNubmerForNull(reader[8].ToString())),
                    MaterialName = reader[9].ToString(),
                    LancePosition = int.Parse(CheckNubmerForNull(reader[10].ToString())),
                    O2TotalVol = int.Parse(CheckNubmerForNull(reader[11].ToString()))
                });
            }
            reader.Close();
            return result;
        }

        public HotMetalAttributes GetHotMetalAttributes(int heatID)
        {
            var sql = "SELECT HML.HM_TEMP, HML.HM_WEIGHT ";
            sql += "FROM SMK.HEATS H, SMK.HEAT_HOTMETAL HHM, SMK.HOTMETAL_LADLES HML ";
            sql += "WHERE HHM.HEAT_ID = H.HEAT_ID AND HML.CHGL_ID = HHM.CHGL_ID AND H.HEAT_ID = :HeatID ";
            var reader = Execute(sql, new[] { 
                new OracleParameter { 
                    OracleDbType = OracleDbType.Int32, ParameterName = "HeatID", Direction= System.Data.ParameterDirection.Input, Value = heatID 
                } 
            });
            var hotMetal = new HotMetalAttributes();
            if (reader.Read())
            {
                hotMetal.Temperature = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                hotMetal.Weight = int.Parse(CheckNubmerForNull(reader[1].ToString()));
            }
            reader.Close();
            return hotMetal;
        }

        public List<HotMetalAnalysys> GetHotMetalAnalysys(Int64 heatNumber)
        {
            var result = new List<HotMetalAnalysys>();
            var sql = "SELECT XIM_DT, XIM_NUM, XIM_MIX, XIM_PFANNE, XIM_C, XIM_MN, XIM_P, XIM_S, XIM_SI, XIM_TI ";
            sql += "FROM XIM.XIM_ANAL ";
            sql += "WHERE XIM_VID = 3 AND XIM_PLAVKA = :HeatNumber ";
            var reader = Execute(sql, new[] { 
                new OracleParameter { 
                    OracleDbType = OracleDbType.Int32, ParameterName = "HeatNumber", Direction= System.Data.ParameterDirection.Input, Value = heatNumber 
                } 
            });
            while (reader.Read())
            {
                result.Add(new HotMetalAnalysys
                {
                    Time = DateTime.Parse((CheckDateForNull(reader[0].ToString()))),
                    ProbeNumber = int.Parse(CheckNubmerForNull(reader[1].ToString())),
                    TorpedoNumber = int.Parse(CheckNubmerForNull(reader[2].ToString())),
                    LadleNumber = int.Parse(CheckNubmerForNull(reader[3].ToString())),
                    C = float.Parse(CheckNubmerForNull(reader[4].ToString())),
                    Mn = float.Parse(CheckNubmerForNull(reader[5].ToString())),
                    P = float.Parse(CheckNubmerForNull(reader[6].ToString())),
                    S = float.Parse(CheckNubmerForNull(reader[7].ToString())),
                    Si = float.Parse(CheckNubmerForNull(reader[8].ToString())),
                    Ti = float.Parse(CheckNubmerForNull(reader[9].ToString()))
                });
            }
            reader.Close();
            return result;
        }

        public List<ScrapBucket> GetScrapBuckets(int heatID)
        {
            var result = new List<ScrapBucket>();
            var sql = "SELECT SB.BUCKET_ID, SB.HEAT_ID, SBM.WEIGHT, MS.NAME_ENGLISH, SBM.MAT_ID, MS.MAT_NO, SB.BUCKET_NO ";
            sql += "FROM SMK.SCRAP_BUCKETMATS SBM, SMK.SCRAP_BUCKETS SB, SMK.MATERIAL_SPEC MS ";
            sql += "WHERE SB.HEAT_ID = :HeatID AND SBM.BUCKET_ID = SB.BUCKET_ID AND MS.MAT_ID = SBM.MAT_ID ";
            var reader = Execute(sql, new[] {
                new OracleParameter { OracleDbType = OracleDbType.Int32, ParameterName = "HeatID", Direction= System.Data.ParameterDirection.Input, Value = heatID 
                } 
            });
            while (reader.Read())
            {
                result.Add(new ScrapBucket
                {
                    Id = int.Parse(CheckNubmerForNull(reader[0].ToString())),
                    FusionId = int.Parse(CheckNubmerForNull(reader[1].ToString())),
                    Weight = int.Parse(CheckNubmerForNull(reader[2].ToString())),
                    MaterialName = reader[3].ToString(),
                    MaterialId = int.Parse(CheckNubmerForNull(reader[4].ToString())),
                    MaterialNumber =
                        int.Parse(
                            reader[5].ToString().Split(new[] {'-'},
                                                        StringSplitOptions.RemoveEmptyEntries)[2]),
                    Number = int.Parse(CheckNubmerForNull(reader[6].ToString()))
                });
            }
            reader.Close();
            return result;
        }

        public List<SlagAnalysys> GetSlagAnalysys(Int64 heatNumber)
        {
            var result = new List<SlagAnalysys>();
            var sql = "SELECT XIM_DT, XIM_NUM, XIM_CAO, XIM_SIO2, XIM_FEO, XIM_MGO, XIM_MNO, XIM_S, XIM_AL2O3, XIM_P2O5 ";
            sql += "FROM XIM.XIM_ANAL WHERE XIM_VID = 2 AND XIM_PLAVKA = :HeatNumber";
            var reader = Execute(sql,  new[] {  
                new OracleParameter { OracleDbType = OracleDbType.Int32, ParameterName = "HeatNumber", Direction= System.Data.ParameterDirection.Input, Value = heatNumber }
            });
            while  (reader.Read())
            {
                result.Add(new SlagAnalysys
                {
                    CaO = double.Parse(CheckNubmerForNull(reader[2].ToString())),
                    SiO2 = double.Parse(CheckNubmerForNull(reader[3].ToString())),
                    FeO = double.Parse(CheckNubmerForNull(reader[4].ToString())),
                    MgO = double.Parse(CheckNubmerForNull(reader[5].ToString())),
                    MnO = double.Parse(CheckNubmerForNull(reader[6].ToString())),
                    S = double.Parse(CheckNubmerForNull(reader[7].ToString())),
                    Al2O3 = double.Parse(CheckNubmerForNull(reader[8].ToString())),
                    P2O5 = double.Parse(CheckNubmerForNull(reader[9].ToString()))
                });
            }
            reader.Close();
            return result;
        }

        public List<SteelAnalysys> GetSteelAnalysys(Int64 heatNumber)
        {
            var result = new List<SteelAnalysys>();
            var sql = "SELECT XIM_DT, XIM_NUM, XIM_C, XIM_SI, XIM_MN, XIM_P, XIM_S, XIM_CR, XIM_NI, XIM_CU, XIM_AL, XIM_N, XIM_V, XIM_TI, ";
            sql += "XIM_AS, XIM_NB, XIM_MO, XIM_ZR, XIM_B, XIM_SN, XIM_AL_KR, XIM_W, XIM_CO, XIM_CA ";
            sql += "FROM XIM.XIM_ANAL WHERE XIM_VID = 1 AND XIM_PLAVKA = :HeatNumber AND (XIM_PLACE >= 20 AND XIM_PLACE < 30) ";
            var reader = Execute(sql,new[] {  
                new OracleParameter { OracleDbType = OracleDbType.Int32, ParameterName = "HeatNumber", Direction= System.Data.ParameterDirection.Input, Value = heatNumber }});
            if (reader.Read())
            {
                result.Add(new SteelAnalysys
                {
                    Time = DateTime.Parse(CheckDateForNull(reader[0].ToString())),
                    ProbeNumber = int.Parse(CheckNubmerForNull(reader[1].ToString())),
                    C = double.Parse(CheckNubmerForNull(reader[2].ToString())),
                    Si = double.Parse(CheckNubmerForNull(reader[3].ToString())),
                    Mn = double.Parse(CheckNubmerForNull(reader[4].ToString())),
                    P = double.Parse(CheckNubmerForNull(reader[5].ToString())),
                    S = double.Parse(CheckNubmerForNull(reader[6].ToString())),
                    Cr = double.Parse(CheckNubmerForNull(reader[7].ToString())),
                    Ni = double.Parse(CheckNubmerForNull(reader[8].ToString())),
                    Cu = double.Parse(CheckNubmerForNull(reader[9].ToString())),
                    Al = double.Parse(CheckNubmerForNull(reader[10].ToString())),
                    N = double.Parse(CheckNubmerForNull(reader[11].ToString())),
                    V = double.Parse(CheckNubmerForNull(reader[12].ToString())),
                    Ti = double.Parse(CheckNubmerForNull(reader[13].ToString())),
                    As = double.Parse(CheckNubmerForNull(reader[14].ToString())),
                    Nb = double.Parse(CheckNubmerForNull(reader[15].ToString())),
                    Mo = double.Parse(CheckNubmerForNull(reader[16].ToString())),
                    Zr = double.Parse(CheckNubmerForNull(reader[17].ToString())),
                    B = double.Parse(CheckNubmerForNull(reader[18].ToString())),
                    Sn = double.Parse(CheckNubmerForNull(reader[19].ToString())),
                    Al_kp = double.Parse(CheckNubmerForNull(reader[20].ToString())),
                    W = double.Parse(CheckNubmerForNull(reader[21].ToString())),
                    Co = double.Parse(CheckNubmerForNull(reader[22].ToString())),
                    Ca = double.Parse(CheckNubmerForNull(reader[23].ToString()))
                });
            }
            reader.Close();
            return result;
        }

        public List<Sublance> GetSublance(int heatID)
        {
            var result = new List<Sublance>();
            var sql = "SELECT INSERTTIME, TEMP, OXYGEN, CARBON ";
            sql += "FROM SMK.HEAT_MEASUREMENTS ";
            sql += "WHERE TYPE = 'AUTO' AND HEAT_ID = :HeatID ";
            sql += "ORDER BY INSERTTIME";
            var reader = Execute(sql, new[] { 
                new OracleParameter { 
                    OracleDbType = OracleDbType.Int32, ParameterName = "HeatID", Direction= System.Data.ParameterDirection.Input, Value = heatID 
                } 
            });
            while (reader.Read())
            {
                result.Add(new Sublance
                               {
                                   StartDate = DateTime.Parse(CheckDateForNull(reader["INSERTTIME"].ToString())),
                                   Temperature = int.Parse(CheckNubmerForNull(reader["TEMP"].ToString())),
                                   Oxigen = int.Parse(CheckNubmerForNull(reader["OXYGEN"].ToString())),
                                   C = double.Parse(CheckNubmerForNull(reader["CARBON"].ToString())),
                                   
                               });
               
            }
            reader.Close();
            return result;
        }

        public Heat GetHeatInfo(string heatNumber)
        {
            var heat = new Heat();
            var sql = "SELECT H.HEAT_ID, H.HEAT_NO, H.RPT_TSOLL, H.RPT_TIST, H.RPT_CSOLL, H.RPT_CIST, ";
            sql += "to_char(h.DT_BEGIN,'dd.mm.yy HH24:mi:ss'),to_char(h.DT_END,'dd.mm.yy HH24:mi:ss'), ";
            sql += "H.TEAM_NAME, G.NAME_ENGLISH, H.CHGD_HMWEIGHT, H.HM_TEMP, H.CONVERTER_LIFE ";
            sql += "FROM SMK.HEATS H, SMK.GRADE_SPEC G WHERE H.HEAT_NO = '" + heatNumber + "' AND H.GRADE_ID = G.GRADE_ID";
            var reader = Execute(sql);
            if (reader.Read())
            {
                heat.ID = int.Parse(reader[0].ToString());
                heat.Number = int.Parse(reader[1].ToString());
                heat.Planned.Temperature = int.Parse(!string.IsNullOrEmpty(reader[2].ToString()) ? reader[2].ToString() : "0");
                heat.Actual.Temperature = int.Parse(!string.IsNullOrEmpty(reader[3].ToString()) ? reader[3].ToString() : "0");
                heat.Planned.Carbon = double.Parse(!string.IsNullOrEmpty(reader[4].ToString()) ? reader[4].ToString() : "0");
                heat.Actual.Carbon = double.Parse(!string.IsNullOrEmpty(reader[5].ToString()) ? reader[5].ToString() : "0");
                heat.StartDate = DateTime.Parse(reader[6].ToString());
                heat.EndDate = DateTime.Parse(!string.IsNullOrEmpty(reader[7].ToString()) ? reader[7].ToString() : "01.01.01");
                heat.TeamNumber = int.Parse(!string.IsNullOrEmpty(reader[8].ToString()) ? reader[8].ToString() : "0");
                heat.Grade = reader[9].ToString();
                heat.HotMetalAttributes.Weight = int.Parse(CheckNubmerForNull(reader[10].ToString()));
                heat.HotMetalAttributes.Temperature = int.Parse(CheckNubmerForNull(reader[11].ToString()));
                heat.AggregateLifeTime = int.Parse(CheckNubmerForNull(reader[12].ToString()));
                heat.AggregateNumber = int.Parse(heatNumber.Substring(0, 1));
            }
            reader.Close();
            return heat;
        }
    }
}
