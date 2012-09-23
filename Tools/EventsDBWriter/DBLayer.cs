using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NordSteel.Data;
using System.Reflection;
using Oracle.DataAccess.Client;
using Core;
using CommonTypes;
namespace Tools.DB
{
    class DBLayer : OracleDBLayer
    {
        

        private List<PropertyInfo> GetDBPointProperties(Type eventType)
        {
            List<PropertyInfo> result = new List<PropertyInfo>();
            foreach (PropertyInfo property in eventType.GetProperties())
            {
                DBPoint dbPoint = (DBPoint)property.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBPoint").FirstOrDefault();

                if (dbPoint != null && dbPoint.IsStored)
                {
                    result.Add(property);
                }
            }
            return result;
        }
        private OracleDbType GetDBPointPropertiesType(PropertyInfo property)
        {
            switch (property.PropertyType.Name)
            {
                case "DateTime": return OracleDbType.Date;
                case "Boolean":
                case "Int32": return OracleDbType.Int32;
                case "Single": return OracleDbType.Single;
                case "Decimal": return OracleDbType.Decimal;
                case "String": return OracleDbType.Varchar2;
                default: return OracleDbType.Object;
            }
        }

        public bool Insert(BaseEvent _event, int unitNumber)
        {
            try
            {
                string sql = string.Format("INSERT INTO {0}{1} (Time", _event.GetType().Name, unitNumber);
                List<OracleParameter> parametres = new List<OracleParameter>();
                parametres.Add(new OracleParameter()
                                   {
                                       ParameterName = "Time",
                                       OracleDbType = OracleDbType.Date,
                                       Value = _event.Time,
                                       Direction = System.Data.ParameterDirection.Input
                                   });

                string dataSql = "";
                foreach (PropertyInfo property in GetDBPointProperties(_event.GetType()))
                {
                    sql += string.Format(",{0}", property.Name);
                    parametres.Add(new OracleParameter()
                                       {
                                           ParameterName = property.Name,
                                           OracleDbType = GetDBPointPropertiesType(property),
                                           Value =
                                               property.PropertyType.Name != "Boolean"
                                                   ? _event.GetType().GetProperty(property.Name).GetValue(_event,
                                                                                                          null)
                                                   : (bool)
                                                     _event.GetType().GetProperty(property.Name).GetValue(_event,
                                                                                                          null)
                                                         ? 1
                                                         : 0,
                                           Direction = System.Data.ParameterDirection.Input
                                       });
                    dataSql += string.Format(", :{0}", property.Name);
                }
                sql += string.Format(") VALUES (:Time{0})", dataSql);
                ExecuteNonQuery(sql, parametres.ToArray());
                return true;
            }
            catch (OracleException exception)
            {
                if (exception.Message.IndexOf("3114") > 0)
                {
                    try
                    {
                        Connection.Close();
                        Connection.Open();
                    }
                    catch
                    {
                    }
                    ;
                    return false;
                }

                if (exception.Message.IndexOf("unique constraint") > 0 || exception.Message.IndexOf("-00001") > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /*private string PropertyValueToSqlString(BaseEvent _event, string propertyName)
        {
            Type propertyType = _event.GetType().GetProperty(propertyName).PropertyType;
            object propertyValue = _event.GetType().GetProperty(propertyName).GetValue(_event, null);
            return (propertyType.Name.ToLower() == "datetime" || propertyType.Name.ToLower() == "string") ? string.Format("'{0}'", propertyValue) : string.Format("{0}", propertyValue);
        }*/

       /* public bool Insert(ICollection<BaseEvent> _events, int unitNumber)
        {
            try
            {
                string sql = "INSERT ALL ";
                foreach (BaseEvent _event in _events)
                {
                    sql += string.Format(" INTO {0}{1} (Time", _event.GetType().Name, unitNumber);
                    string dataSql = "";
                    foreach (PropertyInfo property in GetDBPointProperties(_event.GetType()))
                    {
                        sql += string.Format(",{0}", property.Name);
                        dataSql += string.Format(",{0}", PropertyValueToSqlString(_event, property.Name));
                    }
                    sql += string.Format(") VALUES ({0}{1})", this.OracleDate(_event.Time), dataSql);
                }
                sql += " select * from dual";
                Execute(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }*/

        public void CheckTables(Type[] types, int unitNumber)
        {
            foreach (Type type in types)
            {
                var tableName = type.Name + unitNumber.ToString();
                if (!TableExist(tableName))
                {
                    CreateTable(type);
                }
                else
                    AlterTable(tableName, type);
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

                    string sql = string.Format("CREATE TABLE {0}{1} (Time DATE NOT NULL", type.Name, ((DBGroup)item).UnitNumber);
                    foreach (PropertyInfo property in type.GetProperties().Where(p => p.Name != "Time"))
                    {
                        DBPoint dbPoint = (DBPoint)property.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBPoint").FirstOrDefault();

                        if (dbPoint == null || !dbPoint.IsStored) continue;

                        sql += string.Format(", {0} {1}", property.Name, ToOracleTypeString(property.PropertyType, dbPoint.MaxSize));
                    }
                    sql += ")";

                    ExecuteNonQuery(sql);
                    sql = string.Format("ALTER TABLE {0}{1} ADD CONSTRAINT {0}{1}_PK PRIMARY KEY (Time)", type.Name, ((DBGroup)item).UnitNumber);
                    ExecuteNonQuery(sql);
                }
            }
            catch
            { }
        }

      /*  public void DropTable(Type type)
        {
            try
            {
                var data = type.GetCustomAttributes(false).Where(x => x.GetType().Name == "DBGroup").ToArray();
                foreach (var item in data)
                {
                    if (item == null) continue;

                    if (!TableExist(type.Name + ((DBGroup)item).UnitNumber.ToString())) return;

                    string sql = string.Format("DROP TABLE {0}{1} PURGE", type.Name.ToUpper(), ((DBGroup)item).UnitNumber);

                    ExecuteNonQuery(sql);
                }
            }
            catch { }
        }*/

        private bool TableExist(string tableName)
        {
            try
            {
                bool ret = false;
                string sql = string.Format("SELECT count(*) from user_tables WHERE table_name='{0}'", tableName.ToUpper());
                OracleDataReader reader = Execute(sql);
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

        private void AlterTable(string tableName, Type type)
        {
            var fieldsList = new List<string>();
            var pointsList = new List<string>();
            var pointsNameList = new List<string>();
           

            foreach (var property in type.GetProperties().Where(p => p.Name != "Time"))
            {
                var dbPoint = (DBPoint)property.GetCustomAttributes(false).FirstOrDefault(p => p.GetType().Name == "DBPoint");
                if (dbPoint == null || !dbPoint.IsStored) continue;
                pointsList.Add(string.Format("{0} {1}", property.Name, ToOracleTypeString(property.PropertyType, dbPoint.MaxSize)));
                pointsNameList.Add(property.Name.ToUpper());
            }
            
            var sql = string.Format("select column_name from user_tab_columns where table_name ='{0}'", tableName.ToUpper());
            var reader = Execute(sql);
            while (reader.Read())
            {
                var field = reader[0].ToString();
                if (field.ToUpper() != "TIME")
                {
                    fieldsList.Add(field);
                }
            }
            reader.Close();

            if (pointsNameList.Count == fieldsList.Count) return;

            for (var i = 0; i < pointsList.Count; i++)
            {
                if (fieldsList.Any(field => field == pointsNameList[i])) continue;
                sql = string.Format("ALTER TABLE {0} ADD ({1})", tableName.ToUpper(), pointsList[i]);
                ExecuteNonQuery(sql);
            }
           
        }
    }
}
