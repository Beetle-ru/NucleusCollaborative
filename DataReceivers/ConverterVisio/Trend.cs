using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;

namespace ConverterVisio
{
    class Trends
    {
        string m_Name;
        DateTime m_StartTime;
        Dictionary<double, float> m_Values = new Dictionary<double, float>();
        PointPairList m_Points = new PointPairList();
        System.Drawing.Color m_Color;
        bool m_IsVisible = true;
        double m_YZoom = 1;
        double m_XZoom = 1;
        System.Windows.Controls.Label m_NameLabel;
        System.Windows.Controls.Label m_ValueLabel;


        public string Name { get { return m_Name; } set { m_Name = value; } }
        public DateTime StartTime { get { return m_StartTime; } set { m_StartTime = value; } }
        public PointPairList Points { get { return m_Points; } set { m_Points = value; } }
        public System.Drawing.Color Color { get { return m_Color; } set { m_Color = value; } }
        public bool IsVisible { get { return m_IsVisible; } set { m_IsVisible = value; } }
        public Dictionary<double, float> Values { get { return m_Values; } set { m_Values = value; } }
        public double YZoom { get { return m_YZoom; } set { m_YZoom = value; } }
        public double XZoom { get { return m_XZoom; } set { m_XZoom = value; } }
        public System.Windows.Controls.Label NameLabel { get { return m_NameLabel; } set { m_NameLabel = value; } }
        public System.Windows.Controls.Label ValueLabel { get { return m_ValueLabel; } set { m_ValueLabel = value; } }

        public Trends() { }

        public Trends(string name) 
        {
            Name = name;
            if (NameLabel != null)
            {
                NameLabel.Content = name;
            }
        }

        public Trends(string name, DateTime startTime)
        {
            Name = name;
            if (NameLabel != null)
            {
                NameLabel.Content = name;
            }
            StartTime = startTime;
        }

        public Trends(string name, System.Drawing.Color color)
        {
            Name = name;
            Color = color;
            if (NameLabel != null)
            {
                NameLabel.Content = name;
            }
        }

        public Trends(string name, DateTime startTime, System.Drawing.Color color)
        {
            Name = name;
            StartTime = startTime;
            Color = color;
            if (NameLabel != null)
            {
                NameLabel.Content = name;
            }
        }

        public void Update(double timeSpanInSeconds, float value)
        {
            Values.Add(timeSpanInSeconds, value);
            PointPair pp = new PointPair(XZoom * timeSpanInSeconds, YZoom * value);
            Points.Add(pp);
            if (ValueLabel != null)
            {
                ValueLabel.Content = value;
            }
        }
    }
}
