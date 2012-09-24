using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Oracle.DataAccess.Client;
using System.Reflection;
using CommonTypes;
namespace NordSteel.Data
{
    class TrendsDBLayer : OracleDBLayer
    {
        public ICollection<BaseEvent> GetEventsByType(Type eventType,int unitNumber, DateTime startDate, DateTime endDate)
        {
            string sql = FormHeadSQL(eventType, unitNumber);
            List<OracleParameter> param = new List<OracleParameter>();
            param.Add(new OracleParameter() { Direction = System.Data.ParameterDirection.Input, ParameterName = "StartDate", OracleDbType = OracleDbType.Date, Value = startDate });
            param.Add(new OracleParameter() { Direction = System.Data.ParameterDirection.Input, ParameterName = "EndDate", OracleDbType = OracleDbType.Date, Value = endDate });
            sql += " WHERE Time BETWEEN :StartDate AND :EndDate ORDER BY Time";
            OracleDataReader reader = Execute(sql, param.ToArray());
            return ParseDatasReaderByType(reader, eventType);
        }

        public  ICollection<T> GetEvents<T>(int unitNumber, DateTime startDate, DateTime endDate)
        {
            Type eventType = typeof(T);
            string sql = FormHeadSQL(eventType, unitNumber);
            List<OracleParameter> param = new List<OracleParameter>();
            param.Add(new OracleParameter() { Direction = System.Data.ParameterDirection.Input, ParameterName = "StartDate", OracleDbType = OracleDbType.Date, Value = startDate });
            param.Add(new OracleParameter() { Direction = System.Data.ParameterDirection.Input, ParameterName = "EndDate", OracleDbType = OracleDbType.Date, Value = endDate });
            sql += " WHERE Time BETWEEN :StartDate AND :EndDate ORDER BY Time";
            OracleDataReader reader = Execute(sql, param.ToArray());
            return ParseDatasReader<T>(reader);
        }

        public T GetEvent<T>(int unitNumber, DateTime time)
        {
            Type eventType = typeof(T);
            string sql = FormHeadSQL(eventType, unitNumber);
            List<OracleParameter> param = new List<OracleParameter>();
            param.Add(new OracleParameter() { Direction = System.Data.ParameterDirection.Input, ParameterName = "Time", OracleDbType = OracleDbType.Date, Value = time });
            sql += " WHERE Time= "+ OracleDate(time);
            OracleDataReader reader = Execute(sql, param.ToArray());
            return ParseDataReader<T>(reader);
        }


        private T ParseDataReader<T>(OracleDataReader reader)
        {
            Type eventType = typeof(T);
            T _event = (T)Activator.CreateInstance(eventType);
            
            while (reader.Read())
            {
                
                _event.GetType().GetProperty("Time").SetValue(_event, reader["Time"], null);
                foreach (PropertyInfo property in eventType.GetProperties())
                {
                    DBPoint data = (DBPoint)property.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBPoint").FirstOrDefault();
                    if (data == null || !data.IsStored) continue;
                    _event.GetType().GetProperty(property.Name).SetValue(_event, reader[property.Name], null);
                }
            }
            reader.Close();
            return _event;
        }

        private ICollection<T> ParseDatasReader<T>(OracleDataReader reader)
        {
            Type eventType = typeof(T);
            
            List<T> result = new List<T>();
            while (reader.Read())
            {
                T _event = (T)Activator.CreateInstance(eventType);
                _event.GetType().GetProperty("Time").SetValue(_event, reader["Time"], null);
                foreach (PropertyInfo property in eventType.GetProperties())
                {
                    DBPoint data = (DBPoint)property.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBPoint").FirstOrDefault();
                    if (data == null || !data.IsStored || reader[property.Name].ToString() == "") continue;
                    _event.GetType().GetProperty(property.Name).SetValue(_event,
                                                                         property.PropertyType.Name == "Boolean"
                                                                             ? Convert.ChangeType(
                                                                                 int.Parse(
                                                                                     reader[property.Name].ToString()),
                                                                                 property.PropertyType)
                                                                             : Convert.ChangeType(
                                                                                 reader[property.Name],
                                                                                 property.PropertyType), null);
                }
                result.Add(_event);
            }
            reader.Close();
            return result;
        }

        private ICollection<BaseEvent> ParseDatasReaderByType(OracleDataReader reader, Type eventType)
        {

           
            List<BaseEvent> result = new List<BaseEvent>();
            while (reader.Read())
            {
                BaseEvent _event = (BaseEvent)Activator.CreateInstance(eventType);
                _event.GetType().GetProperty("Time").SetValue(_event, reader["Time"], null);
                foreach (PropertyInfo property in eventType.GetProperties())
                {
                    DBPoint data = (DBPoint)property.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBPoint").FirstOrDefault();
                    if (data == null || !data.IsStored || reader[property.Name].ToString() == "") continue;
                     
                    _event.GetType().GetProperty(property.Name).SetValue(_event,
                                                                         property.PropertyType.Name == "Boolean"
                                                                             ? Convert.ChangeType(
                                                                                 int.Parse(
                                                                                     reader[property.Name].ToString()),
                                                                                 property.PropertyType)
                                                                             : Convert.ChangeType(
                                                                                 reader[property.Name],
                                                                                 property.PropertyType), null);
                }
                result.Add(_event);
            }
            reader.Close();
            return result;
        }

        private string FormHeadSQL(Type eventType, int unitNumber)
        {
            string sql = "SELECT Time";
            foreach (PropertyInfo property in eventType.GetProperties())
            {
                DBPoint data = (DBPoint)property.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBPoint").FirstOrDefault();
                if (data == null || !data.IsStored) continue;
                sql += "," + property.Name;
            }
            sql += string.Format(" FROM {0}{1}", eventType.Name, unitNumber);
            return sql;
        }
    }
}
