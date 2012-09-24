using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTypes;
using System.Collections;

namespace NordSteel.Data
{
    public class LocalTrendProvider : BaseTrendProvider
    {
        public override ICollection<Trend> GetPoint(string pointName, string eventName, int unitNumber, object dataSource)
        {
            //if (!(dataSource is ICollection<BaseEvent>)) throw new Exception("Обьект dataSource не является коллекцией BaseEvent");

            List<Trend> trends = new List<Trend>();

            foreach (var item in (IEnumerable)dataSource)
            {
                trends.Add(new Trend()
                            {
                                Time = ((BaseEvent)item).Time,
                                Value = (double)item.GetType().GetProperty(pointName).GetValue(item, null)
                            });
            }

            return trends;
        }

        public override ICollection<Trend> GetPoint(string pointName, string eventName, int unitNumber, DateTime startTime, DateTime endTime, object dataSource)
        {
            //if (!(dataSource is ICollection<BaseEvent>)) throw new Exception("Обьект dataSource не является коллекцией BaseEvent");

            List<Trend> trends = new List<Trend>();
            foreach (var item in (IEnumerable)dataSource)
            {
                if (((BaseEvent)item).Time >= startTime && ((BaseEvent)item).Time <= endTime)
                {
                    trends.Add(new Trend()
                    {
                        Time = ((BaseEvent)item).Time,
                        Value = (double)item.GetType().GetProperty(pointName).GetValue(item, null)
                    });
                }
            }

            return trends;
        }

        public override ICollection<BaseEvent> GetEventsByType(Type eventType, int unitNumber, DateTime startTime, DateTime endTime, object dataSource, string propertyName)
        {
            return null;
        }

        public override ICollection<T> GetEvents<T>(int unitNumber, DateTime startTime, DateTime endTime, object dataSource,string propertyName)
        {
            return null;
        }

        public override T GetEvent<T>(int unitNumber, DateTime time, object dataSource, string propertyName)
        {
            return default(T);
        }
    }
}
