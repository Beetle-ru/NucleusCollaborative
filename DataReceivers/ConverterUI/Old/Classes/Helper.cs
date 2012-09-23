using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTypes.Classes;
using Converter;
using System.Windows;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace ConverterUI
{
    static class Helper
    {
        static HeatInfo m_HeatInfo;
        public static HeatScript CurrentHeatScript = new HeatScript();
        public static HeatStage CurrentHeatStage = new HeatStage();


        public static HeatInfo HeatInfo
        {
            get
            {
                if (m_HeatInfo == null)
                {
                    m_HeatInfo= new HeatInfo();
                    m_HeatInfo.StartDate = DateTime.Now;                    
                }
                return m_HeatInfo;
            }
            set
            {
                m_HeatInfo= value;
            }
        }

        //static HeatScript m_HeatInfo.HeatScript;
        //public static HeatScript HeatInfo.HeatScript
        //{
        //    get
        //    {
        //        if (m_HeatInfo.HeatScript == null)
        //        {
        //            m_HeatInfo.HeatScript = new HeatScript();
        //        }
        //        return m_HeatInfo.HeatScript;
        //    }
        //    set
        //    {
        //        m_HeatInfo.HeatScript = value;
        //    }
        //}

        public static ObservableDataSource<Point> O2Points = new ObservableDataSource<Point>();
        public static ObservableDataSource<Point> LancePoints = new ObservableDataSource<Point>();
        public static ObservableDataSource<Point> COPoints = new ObservableDataSource<Point>();
        public static ObservableDataSource<Point> CO2Points = new ObservableDataSource<Point>();
        public static ObservableDataSource<Point> H2Points = new ObservableDataSource<Point>();
        public static ObservableDataSource<Point> N2Points = new ObservableDataSource<Point>();
        public static ObservableDataSource<Point> ArPoints = new ObservableDataSource<Point>();

        public static void RunScript()
        {
             
        }

        public static void UpdateTrends(ConverterBaseEvent newEvent)
        {
            if (newEvent is OffGasAnalysisEvent)
            {
                OffGasAnalysisEvent ogaEvent = newEvent as OffGasAnalysisEvent;
                O2Points.Collection.Add(new Point((newEvent.Time - HeatInfo.StartDate).TotalSeconds, ogaEvent.O2 / 12));
                COPoints.Collection.Add(new Point((newEvent.Time - HeatInfo.StartDate).TotalSeconds, ogaEvent.CO));
                CO2Points.Collection.Add(new Point((newEvent.Time - HeatInfo.StartDate).TotalSeconds, ogaEvent.CO2));
                H2Points.Collection.Add(new Point((newEvent.Time - HeatInfo.StartDate).TotalSeconds, ogaEvent.H2));
                N2Points.Collection.Add(new Point((newEvent.Time - HeatInfo.StartDate).TotalSeconds, ogaEvent.N2));
                ArPoints.Collection.Add(new Point((newEvent.Time - HeatInfo.StartDate).TotalSeconds, ogaEvent.Ar));
            }
            else if (newEvent is LanceEvent)
            {
                LanceEvent lEvent = newEvent as LanceEvent;
                LancePoints.Collection.Add(new Point((newEvent.Time - HeatInfo.StartDate).TotalSeconds, lEvent.LanceHeight / 3));
            }
        }


        public static void NewHeat(int heatNumber, int converterNumber, DateTime startDate)
        {
            //HeatInfo.Heat = null;

            HeatInfo.CurrentNumber = heatNumber;
            HeatInfo.AggregateNumber = converterNumber;
            HeatInfo.StartDate = startDate;
            HeatInfo.AggregateAngle = new AggregateAngle();

            O2Points.Collection.Clear();
            LancePoints.Collection.Clear();
            COPoints.Collection.Clear();
            CO2Points.Collection.Clear();
            H2Points.Collection.Clear();
            N2Points.Collection.Clear();
            ArPoints.Collection.Clear();

        }
    }
}
