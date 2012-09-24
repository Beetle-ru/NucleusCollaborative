using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CommonTypes;
using Core;

namespace NordSteel.Data
{
    public class TrendsManager
    {
        public object DataSource { get; set; }

        public BaseTrendProvider TrendProvider { get; set; }

        private Dictionary<string, TrendPoint> _Points;

        public void BindPoint(TrendPoint point)
        {
            if (DataSource == null) throw new Exception("Не указан источник данных (DataSource is null)");

            point.ParentGroup.DataSource = DataSource.GetType().GetProperty(point.ParentGroup.PropertyName).GetValue(DataSource, null);

            if (_Points.ContainsKey(point.PropertyName)) throw new Exception(string.Format("Точка с именем {0} уже содержится в коллекции", point.PropertyName));
            _Points.Add(point.PropertyName, point);
        }

        public void BindPoint(ICollection<TrendPoint> points)
        {
            if (DataSource == null) throw new Exception("Не указан источник данных (DataSource is null)");

            foreach (TrendPoint point in points)
            {
                point.ParentGroup.DataSource = DataSource.GetType().GetProperty(point.ParentGroup.PropertyName).GetValue(DataSource, null);

                if (_Points.ContainsKey(point.PropertyName)) throw new Exception(string.Format("Точка с именем {0} уже содержится в коллекции", point.PropertyName));
                _Points.Add(point.PropertyName, point);
            }
        }

        public int PointsCount { get { return _Points.Count; } }

        public TrendPoint GetTrendPoint(string pointName)
        {
            if (!_Points.ContainsKey(pointName)) throw new Exception(string.Format("Точка с именем {0} не найдена в коллекции") );

            return _Points[pointName];
        }

        public ICollection<TrendPoint> GetTrendPoints()
        {
            return _Points.Values.ToArray();
        }

        //public List<TrendPoint> Points { get { return _Points; } }

        private TrendsManager()
        {
            _Points = new Dictionary<string, TrendPoint>();
        }

        public ICollection<Trend> GetPoint(string pointName)
        {
            if (!_Points.ContainsKey(pointName)) throw new Exception(string.Format("Точка с именем {0} не найдена в коллекции") );
            TrendPoint tpoint = _Points[pointName];

            if (DataSource == null || TrendProvider == null) throw new Exception("Не указан источник данных (DataSource) или трендов (TrendProvider)");

            return TrendProvider.GetPoint(tpoint.PropertyName, tpoint.ParentGroup.EventType.Name, tpoint.ParentGroup.UnitNumber, tpoint.ParentGroup.DataSource); 
        }

        private static TrendsManager _Instance = new TrendsManager();

        public static TrendsManager Instance { get { return _Instance; } }

        public static ICollection<TrendGroup> GetGroupTree(int unitNumber)
        {
            List<TrendGroup> result = new List<TrendGroup>();
            Type[] eventTypes;
            try
            {
                eventTypes = BaseEvent.GetEvents();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Ошибка получения списка событий {0}", exc.Message));
            }

            if (eventTypes.Length == 0) throw new Exception("В сборке не найдено ни одного события");

            foreach (var item in eventTypes)
            {
                var data = item.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBGroup").ToArray();
                if (data == null || data.Length == 0) continue;

                foreach (var grp in data)
                {
                    if (((DBGroup)grp).IsTrendGroup && ((DBGroup)grp).UnitNumber == unitNumber)
                    {
                        TrendGroup trendGroup = new TrendGroup();
                        trendGroup.Name = ((DBGroup)grp).DisplayName;
                        trendGroup.PropertyName = ((DBGroup)grp).BindingPropertyName;
                        trendGroup.UnitNumber = unitNumber;
                        trendGroup.EventType = item;
                         
                        foreach (PropertyInfo property in item.GetProperties())
                        {
                            DBPoint point = (DBPoint)property.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBPoint").FirstOrDefault();

                            if (point != null && point.IsTrendPoint)
                            {
                                trendGroup.Points.Add(new TrendPoint()
                                    {
                                        MaxValue = point.MaxValue,
                                        MinValue = point.MinValue,
                                        Name = point.DisplayName,
                                        ShortName = point.DisplayShortName,
                                        ParentGroup = trendGroup,
                                        PropertyName = property.Name
                                    });
                            }
                        }

                        result.Add(trendGroup);
                    }
                }

            }

            return result;

        }
    }
}
