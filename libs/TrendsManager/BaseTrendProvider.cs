using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTypes;

namespace NordSteel.Data
{
    public abstract class BaseTrendProvider
    {
        public abstract ICollection<Trend> GetPoint(string pointName, string eventName, int unitNumber, object dataSource);
        public abstract ICollection<Trend> GetPoint(string pointName, string eventName, int unitNumber, DateTime startTime, DateTime endTime, object dataSource);

        public abstract ICollection<BaseEvent> GetEventsByType(Type eventType, int unitNumber, DateTime startTime, DateTime endTime, object dataSource, string propertyName);
        public abstract ICollection<T> GetEvents<T>(int unitNumber, DateTime startTime, DateTime endTime, object dataSource, string propertyName);
        public abstract T GetEvent<T>(int unitNumber, DateTime time, object dataSource, string propertyName);
    }
}
