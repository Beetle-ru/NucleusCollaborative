using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Reflection;
using CommonTypes;

namespace NordSteel.Data
{
    public class Trends
    {
        private static TrendsDBLayer m_DB = new TrendsDBLayer();

        public static ICollection<BaseEvent> GetEventsByType(int unitNumber, Type eventType, DateTime startDate, DateTime endDate)
        {
            List<BaseEvent> allEvents = new List<BaseEvent>();

            var data = eventType.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBGroup").ToArray();
            //int n = ((DBGroup[])data).Where(p => p.UnitNumber == unitNumber).Select(p => p.UnitNumber).FirstOrDefault();
            //var group = (((DBGroup[])data).FirstOrDefault(p => p.UnitNumber == unitNumber));

            //if (n == unitNumber)
            //{
            return m_DB.GetEventsByType(eventType, unitNumber, startDate, endDate);
            //}
            //else
            //{
            //    throw new Nordsteel.Data.Exceptions.NoDBGroupAttributesException();
            //}


        }

        public static T GetEvent<T>(int unitNumber, DateTime time)
        {
            return m_DB.GetEvent<T>(unitNumber, time);
        }

        public static ICollection<T> GetEvents<T>(int unitNumber, DateTime startDate, DateTime endDate)
        {
            return m_DB.GetEvents<T>(unitNumber, startDate, endDate);
        }




    }
}
