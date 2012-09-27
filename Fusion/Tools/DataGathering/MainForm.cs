using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonTypes;
using Converter.Trends;
using System.IO;
using HeatInfo;
using System.Runtime.Serialization.Formatters.Binary;
using Converter;
using Core;
using System.Threading;
using DataGathering;
using System.Reflection;

namespace DataGathering
{
    public partial class Form1 : Form
    {
        ConverterDBLayer m_Db;
        public Fusion CurrentFussion { get; set; }
        public List<Fusion> Fusions { get; set; }


        List<SlagOutburstEvent> SummarySlagOutburst = new List<SlagOutburstEvent>();
        List<IgnitionEvent> SummaryIgnition = new List<IgnitionEvent>();
       
        List<OffGas> ListOfAllGas = new List<OffGas>();
        List<OffGas> CurrentListOfGas = new List<OffGas>();
        List<Lance> CurrentListLance = new List<Lance>();
        List<Addition> CurrentListAddition = new List<Addition>();
        List<BathLevel> CurrentListBathLevel = new List<BathLevel>();

        double Zoom = 1;//0.1;
        Heat _heat = new Heat();
        public string Path { get; set; }

        public BindingSource BSSteelAnalysys { get; set; }
        public BindingSource BSSublance { get; set; }
        public BindingSource BSSlagAnalysys { get; set; }
        public BindingSource BSBathLevel { get; set; }
        public BindingSource BSScrapBuckets { get; set; }
        public BindingSource BSFusion { get; set; }
        public BindingSource BSOffGas { get; set; }
        public BindingSource BSLance { get; set; }
        public BindingSource BSAddition { get; set; }
        public BindingSource BSHotMetal { get; set; }
        public BindingNavigator BNFusion { get; set; }
        public BindingNavigator BNOffGas { get; set; }

        private void InitializeBindings()
        {
            BSFusion = new BindingSource {DataSource = Fusions, AllowNew = true};
            BSOffGas = new BindingSource {AllowNew = true};
            BSLance = new BindingSource();
            BSAddition = new BindingSource();
        }

        public Form1()
        {
            InitializeComponent();
            m_Db = new ConverterDBLayer();
            InitializeBindings();
            button2.Enabled = false;
            button4.Enabled = false;
        }
 
        public void LoadFusionFromFile(string fileName)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileInfo file = new FileInfo(fileName);
                FileStream fs = file.OpenRead();

                string heatNumber = file.Name.Replace(".dat", "");
                var deserializedObject = new object();
                try
                {
                    deserializedObject = bf.Deserialize(fs);
                }
                catch
                {
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }
                
                
                _heat = new Heat {Number = int.Parse(heatNumber)};
                if (deserializedObject is Heat)
                {
                    _heat = deserializedObject as Heat;
                }
                if (deserializedObject is Dictionary<DateTime, BaseEvent>)
                {
                    _heat.LanceHistory.Clear();
                    _heat.OffGasAnalysisHistory.Clear();
                    _heat.OffGasHistory.Clear();
                    _heat.BoilerWaterCoolingHistory.Clear();
                    _heat.IgnitionHistory.Clear();
                    _heat.SlagOutburstHistory.Clear();
                    Dictionary<DateTime, BaseEvent> events = deserializedObject as Dictionary<DateTime, BaseEvent>;
                    foreach (DateTime key in events.Keys)
                    {

                        switch (events[key].GetType().ToString())
                        {
                            case "Converter.LanceEvent":
                                _heat.LanceHistory.Add((LanceEvent)events[key]);
                                break;
                            case "Converter.OffGasAnalysisEvent":
                                _heat.OffGasAnalysisHistory.Add((OffGasAnalysisEvent)events[key]);
                                break;
                            case "Converter.OffGasEvent":
                                _heat.OffGasHistory.Add((OffGasEvent)events[key]);
                                break;
                            case "Converter.BoilerWaterCoolingEvent":
                                _heat.BoilerWaterCoolingHistory.Add((BoilerWaterCoolingEvent)events[key]);
                                break;
                            case "Converter.IgnitionEvent":
                                _heat.IgnitionHistory.Add((IgnitionEvent)events[key]);
                                break;
                            case "Converter.SlagOutburstEvent":
                                _heat.SlagOutburstHistory.Add((SlagOutburstEvent)events[key]);
                                break;
                        }
                    }
                }
                else if ((deserializedObject is List<BaseEvent>))
                {
                    _heat.LanceHistory.Clear();
                    _heat.OffGasAnalysisHistory.Clear();
                    _heat.OffGasHistory.Clear();
                    _heat.BoilerWaterCoolingHistory.Clear();
                    _heat.IgnitionHistory.Clear();
                    _heat.SlagOutburstHistory.Clear();
                    List<BaseEvent> events = deserializedObject as List<BaseEvent>;
                    foreach (BaseEvent e in events)
                    {

                        switch (e.GetType().ToString())
                        {
                            case "Converter.LanceEvent":
                                _heat.LanceHistory.Add((LanceEvent)e);
                                break;
                            case "Converter.OffGasAnalysisEvent":
                                _heat.OffGasAnalysisHistory.Add((OffGasAnalysisEvent)e);
                                break;
                            case "Converter.OffGasEvent":
                                _heat.OffGasHistory.Add((OffGasEvent)e);
                                break;
                            case "Converter.BoilerWaterCoolingEvent":
                                _heat.BoilerWaterCoolingHistory.Add((BoilerWaterCoolingEvent)e);
                                break;
                            case "Converter.IgnitionEvent":
                                _heat.IgnitionHistory.Add((IgnitionEvent)e);
                                break;
                            case "Converter.SlagOutburstEvent":
                                _heat.SlagOutburstHistory.Add((SlagOutburstEvent)e);
                                break;
                        }
                    }

                }

                CurrentFussion = m_Db.GetFusion(_heat.Number.ToString().Insert(2, "0"));
                CurrentListOfGas.Clear();


                //if (false)
                //{
                //    CurrentListOfGas.Clear();
                //    DateTime? lastEventTime = null;
                //    double ArSum = 0;
                //    double COSum = 0;
                //    double CO2Sum = 0;
                //    double N2Sum = 0;
                //    double H2Sum = 0;
                //    double O2Sum = 0;
                //    int counter = 0;
                //    foreach (OffGasAnalysisEvent ogaEvent in _heat.OffGasAnalysisHistory)
                //    {
                //        ArSum += ogaEvent.Ar;
                //        COSum += ogaEvent.CO;
                //        CO2Sum += ogaEvent.CO2;
                //        H2Sum += ogaEvent.H2;
                //        N2Sum += ogaEvent.N2;
                //        O2Sum += ogaEvent.O2;
                //        counter++;
                //        if (lastEventTime == null || lastEventTime.Value.AddSeconds(1) < key)
                //        {
                //            OffGas og = new OffGas();
                //            og.Ar = ArSum / counter;
                //            og.CO = COSum / counter;
                //            og.CO2 = CO2Sum / counter;
                //            og.H2 = H2Sum / counter;
                //            og.N2 = N2Sum / counter;
                //            og.O2 = O2Sum / counter;
                //            og.Date = key;
                //            foreach (DateTime offGasHistoryKey in _heat.OffGasHistory.Keys)
                //            {
                //                if (offGasHistoryKey.Hour == key.Hour &&
                //                   offGasHistoryKey.Minute == key.Minute &&
                //                   offGasHistoryKey.Second == key.Second)
                //                {
                //                    og.Temp = _heat.OffGasHistory[offGasHistoryKey].OffGasTemp;
                //                    og.Flow = (int)_heat.OffGasHistory[offGasHistoryKey].OffGasFlow;
                //                    break;
                //                }
                //            }
                //            if (og.Flow != 0)
                //            {
                //                CurrentListOfGas.Add(og);
                //            }
                //            lastEventTime = key;
                //            ArSum = 0;
                //            COSum = 0;
                //            CO2Sum = 0;
                //            N2Sum = 0;
                //            H2Sum = 0;
                //            O2Sum = 0;
                //            counter = 0;
                //        }
                //    }

                //    lastEventTime = null;
                //    CurrentListLance.Clear();
                //    # region Объявление переменных
                //    int HeightSum = 0;
                //    double O2FlowSum = 0;
                //    double O2PressureSum = 0;
                //    double O2VolSum = 0;
                //    double O2LeftLanceGewBaerSum = 0;
                //    double O2LeftLanceGewWeightSum = 0;
                //    int O2LeftLanceLeckSum = 0;
                //    double O2LeftLanceWaterInputSum = 0;
                //    double O2LeftLanceWaterOutputSum = 0;
                //    double O2LeftLanceWaterPressureSum = 0;
                //    double O2LeftLanceWaterTempInputSum = 0;
                //    double O2LeftLanceWaterTempOutputSum = 0;
                //    double O2RightLanceGewBaerSum = 0;
                //    double O2RightLanceGewWeightSum = 0;
                //    int O2RightLanceLeckSum = 0;
                //    double O2RightLanceWaterInputSum = 0;
                //    double O2RightLanceWaterOutputSum = 0;
                //    double O2RightLanceWaterPressureSum = 0;
                //    double O2RightLanceWaterTempInputSum = 0;
                //    double O2RightLanceWaterTempOutputSum = 0;
                //    #endregion

                //    counter = 0;
                //    foreach (LanceEvent lEvent in  _heat.LanceHistory)
                //    {
                //        # region Суммирование
                //        HeightSum += lEvent.LanceHeight;
                //        O2FlowSum += lEvent.O2Flow;
                //        O2PressureSum += lEvent.O2Pressure;
                //        O2VolSum += lEvent.O2TotalVol;
                //        O2LeftLanceGewBaerSum += lEvent.O2LeftLanceGewBaer;
                //        O2LeftLanceGewWeightSum += lEvent.O2LeftLanceGewWeight;
                //        O2LeftLanceLeckSum += lEvent.O2LeftLanceLeck;
                //        O2LeftLanceWaterInputSum += lEvent.O2LeftLanceWaterInput;
                //        O2LeftLanceWaterOutputSum += lEvent.O2LeftLanceWaterOutput;
                //        O2LeftLanceWaterPressureSum += lEvent.O2LeftLanceWaterPressure;
                //        O2LeftLanceWaterTempInputSum += lEvent.O2LeftLanceWaterTempInput;
                //        O2LeftLanceWaterTempOutputSum += lEvent.O2LeftLanceWaterTempOutput;
                //        O2RightLanceGewBaerSum += lEvent.O2RightLanceGewBaer;
                //        O2RightLanceGewWeightSum += lEvent.O2RightLanceGewWeight;
                //        O2RightLanceLeckSum += lEvent.O2RightLanceLeck;
                //        O2RightLanceWaterInputSum += lEvent.O2RightLanceWaterInput;
                //        O2RightLanceWaterOutputSum += lEvent.O2RightLanceWaterOutput;
                //        O2RightLanceWaterPressureSum += lEvent.O2RightLanceWaterPressure;
                //        O2RightLanceWaterTempInputSum += lEvent.O2RightLanceWaterTempInput;
                //        O2RightLanceWaterTempOutputSum += lEvent.O2RightLanceWaterTempOutput;
                //        # endregion

                //        counter++;
                //        if (lastEventTime == null || lastEventTime.Value.AddSeconds(1) < key)
                //        {
                //            #region Создание объекта lance
                //            Lance lance = new Lance();
                //            lance.Date = key;
                //            lance.Height = HeightSum / counter;
                //            lance.O2Flow = O2FlowSum / counter;
                //            lance.O2Pressure = O2PressureSum / counter;
                //            lance.O2Vol = O2VolSum / counter;
                //            //lance.O2FlowMode = _heat.LanceHistory[key].O2FlowMode;
                //            lance.O2LeftLanceGewBaer = O2LeftLanceGewBaerSum / counter;
                //            lance.O2LeftLanceGewWeight = O2LeftLanceGewWeightSum / counter;
                //            lance.O2LeftLanceLeck = O2LeftLanceLeckSum / counter;
                //            lance.O2LeftLanceWaterInput = O2LeftLanceWaterInputSum / counter;
                //            lance.O2LeftLanceWaterOutput = O2LeftLanceWaterOutputSum / counter;
                //            lance.O2LeftLanceWaterPressure = O2LeftLanceWaterPressureSum / counter;
                //            lance.O2LeftLanceWaterTempInput = O2LeftLanceWaterTempInputSum / counter;
                //            lance.O2LeftLanceWaterTempOutput = O2LeftLanceWaterTempOutputSum / counter;
                //            lance.O2RightLanceGewBaer = O2RightLanceGewBaerSum / counter;
                //            lance.O2RightLanceGewWeight = O2RightLanceGewWeightSum / counter;
                //            lance.O2RightLanceLeck = O2RightLanceLeckSum / counter;
                //            lance.O2RightLanceWaterInput = O2RightLanceWaterInputSum / counter;
                //            lance.O2RightLanceWaterOutput = O2RightLanceWaterOutputSum / counter;
                //            lance.O2RightLanceWaterPressure = O2RightLanceWaterPressureSum / counter;
                //            lance.O2RightLanceWaterTempInput = O2RightLanceWaterTempInputSum / counter;
                //            lance.O2RightLanceWaterTempOutput = O2RightLanceWaterTempOutputSum / counter;
                //            #endregion
                //            //_heat.LanceHistory[key].O2LeftLanceWaterInput
                //            CurrentListLance.Add(lance);
                //            BathLevel bathLevel = new BathLevel();
                //            bathLevel.Date = key;
                //            bathLevel.Value = _heat.LanceHistory[key].BathLevel;
                //            CurrentListBathLevel.Add(bathLevel);
                //            lastEventTime = key;

                //            #region Обнуление суммарных значений и счетчика
                //            HeightSum = 0;
                //            O2FlowSum = 0;
                //            O2PressureSum = 0;
                //            O2VolSum = 0;
                //            O2LeftLanceGewBaerSum = 0;
                //            O2LeftLanceGewWeightSum = 0;
                //            O2LeftLanceLeckSum = 0;
                //            O2LeftLanceWaterInputSum = 0;
                //            O2LeftLanceWaterOutputSum = 0;
                //            O2LeftLanceWaterPressureSum = 0;
                //            O2LeftLanceWaterTempInputSum = 0;
                //            O2LeftLanceWaterTempOutputSum = 0;
                //            O2RightLanceGewBaerSum = 0;
                //            O2RightLanceGewWeightSum = 0;
                //            O2RightLanceLeckSum = 0;
                //            O2RightLanceWaterInputSum = 0;
                //            O2RightLanceWaterOutputSum = 0;
                //            O2RightLanceWaterPressureSum = 0;
                //            O2RightLanceWaterTempInputSum = 0;
                //            O2RightLanceWaterTempOutputSum = 0;
                //            counter = 0;
                //            #endregion
                //        }
                //    }
                //}
                //else
                {
                    DateTime first = _heat.OffGasAnalysisHistory.First().Time;
                    DateTime last = _heat.OffGasAnalysisHistory.Last().Time;

                    DateTime current = first.AddMilliseconds(-first.Millisecond).AddSeconds(-first.Second);
                    OffGas og = new OffGas();

                    SummaryIgnition = _heat.IgnitionHistory;
                    SummarySlagOutburst = _heat.SlagOutburstHistory;

                    
                    foreach (OffGasAnalysisEvent ogaEvent in _heat.OffGasAnalysisHistory)
                    {
                        og = new OffGas();
                        og.Ar = ogaEvent.Ar;
                        og.CO = ogaEvent.CO;
                        og.CO2 = ogaEvent.CO2;
                        og.H2 = ogaEvent.H2;
                        og.N2 = ogaEvent.N2;
                        og.O2 = ogaEvent.O2;
                        og.Date = ogaEvent.Time;
                        ListOfAllGas.Add(og);
                    }

                    for (int i = 1; i < (last - first).TotalMinutes * 4; i++)
                    {
                        og.Date = current.AddSeconds(7);
                        var gasArray = _heat.OffGasAnalysisHistory.Where(x => x.Time >= current && x.Time < current.AddSeconds(15)).Select(x => x).ToArray();
                        if (gasArray.Length > 0)
                        {
                            og = new OffGas();
                            og.Ar = gasArray.Average(x => x.Ar);
                            og.CO = gasArray.Average(x => x.CO);
                            og.CO2 = gasArray.Average(x => x.CO2);
                            og.H2 = gasArray.Average(x => x.H2);
                            og.N2 = gasArray.Average(x => x.N2);
                            og.O2 = gasArray.Average(x => x.O2);
                            var gasFlowAndTempArray = _heat.OffGasHistory.Where(x => x.Time >= current && x.Time < current.AddSeconds(15)).Select(x => x).ToArray();
                            if (gasFlowAndTempArray.Length > 0)
                            {
                                og.Temperature = (int)gasFlowAndTempArray.Average(x => x.OffGasTemp);
                                og.Flow = (int)gasFlowAndTempArray.Average(x => x.OffGasFlow);
                            }
                            var gasTempsArray = _heat.BoilerWaterCoolingHistory.Where(x => x.Time >= current && x.Time < current.AddSeconds(15)).Select(x => x).ToArray();
                            if (gasTempsArray.Length > 0)
                            {
                                og.TemperatureOnExit = gasTempsArray.Average(x => x.GasTemperatureOnExit);
                                og.PrecollingTemperature = gasTempsArray.Average(x => x.PrecollingGasTemperature);
                                og.TemperatureAfter1Step = gasTempsArray.Average(x => x.GasTemperatureAfter1Step);
                                og.TemperatureAfter2Step = gasTempsArray.Average(x => x.GasTemperatureAfter2Step);
                            }
                        }
                        if (og.Flow != 0)
                        {
                            CurrentListOfGas.Add(og);
                        }
                        current = current.AddSeconds(15);
                    }

                    CurrentListLance.Clear();
                    CurrentListOfGas.Remove(CurrentListOfGas.Last());
                    current = CurrentListOfGas.First().Date.AddMilliseconds(-CurrentListOfGas.First().Date.Millisecond).AddSeconds(-CurrentListOfGas.First().Date.Second);
                    Lance lance = new Lance();
                    for (int i = 1; i < (CurrentListOfGas.Last().Date - CurrentListOfGas.First().Date).TotalMinutes * 4; i++)
                    {
                        current = current.AddSeconds(15);
                        var LanceEventArray = _heat.LanceHistory.Where(x => x.Time >= current && x.Time < current.AddSeconds(15)).Select(x => x).ToArray();
                        if (LanceEventArray.Length > 0)
                        {
                            lance = new Lance();
                            lance.Date = current.AddSeconds(7);
                            lance.Height = (int)LanceEventArray.Average(x => x.LanceHeight);
                            lance.O2Flow = LanceEventArray.Average(x => x.O2Flow);
                            lance.O2Pressure = LanceEventArray.Average(x => x.O2Pressure);
                            lance.O2Vol = LanceEventArray.Average(x => x.O2TotalVol);
                            lance.O2LeftLanceGewBaer = LanceEventArray.Average(x => x.O2LeftLanceGewBaer);
                            lance.O2LeftLanceGewWeight = LanceEventArray.Average(x => x.O2LeftLanceGewWeight);
                            lance.O2LeftLanceLeck = LanceEventArray.Average(x => x.O2LeftLanceLeck);
                            lance.O2LeftLanceWaterInput = LanceEventArray.Average(x => x.O2LeftLanceWaterInput);
                            lance.O2LeftLanceWaterOutput = LanceEventArray.Average(x => x.O2LeftLanceWaterOutput);
                            lance.O2LeftLanceWaterPressure = LanceEventArray.Average(x => x.O2LeftLanceWaterPressure);
                            lance.O2LeftLanceWaterTempInput = LanceEventArray.Average(x => x.O2LeftLanceWaterTempInput);
                            lance.O2LeftLanceWaterTempOutput = LanceEventArray.Average(x => x.O2LeftLanceWaterTempOutput);
                            lance.O2RightLanceGewBaer = LanceEventArray.Average(x => x.O2RightLanceGewBaer);
                            lance.O2RightLanceGewWeight = LanceEventArray.Average(x => x.O2RightLanceGewWeight);
                            lance.O2RightLanceLeck = LanceEventArray.Average(x => x.O2RightLanceLeck);
                            lance.O2RightLanceWaterInput = LanceEventArray.Average(x => x.O2RightLanceWaterInput);
                            lance.O2RightLanceWaterOutput = LanceEventArray.Average(x => x.O2RightLanceWaterOutput);
                            lance.O2RightLanceWaterPressure = LanceEventArray.Average(x => x.O2RightLanceWaterPressure);
                            lance.O2RightLanceWaterTempInput = LanceEventArray.Average(x => x.O2RightLanceWaterTempInput);
                            lance.O2RightLanceWaterTempOutput = LanceEventArray.Average(x => x.O2RightLanceWaterTempOutput);
                            if (CurrentListBathLevel.Count == 0)
                            {
                                BathLevel level = new BathLevel();
                                level.Value = (int)LanceEventArray.Average(x => x.BathLevel);
                                CurrentListBathLevel.Add(level);
                            }
                        }
                        if (lance.O2Flow > 0)
                        {
                            CurrentListLance.Add(lance);
                        }
                    }
                }
            }
            catch (Exception) { }
            CurrentListAddition.Clear();
            UpdateFusion();

        }


        private void открытьФайлToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadFusionFromFile(openDialog.FileName);
            }
            button2.Enabled = true;
            button4.Enabled = true;
        }


        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      public void UpdateFusion()
        {
            try
            {
                BSFusion.DataSource = CurrentFussion;

                dataGridViewFusion.DataSource = null;
                dataGridViewFusion.DataSource = BSFusion;
                dataGridViewFusion.Columns["EndDate"].HeaderText = "Конец";
                dataGridViewFusion.Columns["PlannedTemperature"].HeaderText = "Тзадан";
                dataGridViewFusion.Columns["FactTemperature"].HeaderText = "Тфакт";
                dataGridViewFusion.Columns["PlannedCarbon"].HeaderText = "Сзадан";
                dataGridViewFusion.Columns["FactCarbon"].HeaderText = "Сфакт";
                dataGridViewFusion.Columns["AggregateNumber"].HeaderText = "Номер конвертера";
                dataGridViewFusion.Columns["TeamNumber"].HeaderText = "Бригада";
                dataGridViewFusion.Columns["Grade"].HeaderText = "Марка стали";
                dataGridViewFusion.Columns["Number"].HeaderText = "Номер плавки";
                dataGridViewFusion.Columns["StartDate"].HeaderText = "Начало плавки";
                dataGridViewFusion.Columns["CastIronWeight"].HeaderText = "Вес чугуна";
                dataGridViewFusion.Columns["CastIronTemp"].HeaderText = "Температура чугуна";
                dataGridViewFusion.Columns["EndDate"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";
                dataGridViewFusion.Columns["StartDate"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";
                dataGridViewFusion.Columns["ID"].Visible = false;
                dataGridViewFusion.Columns["Planned"].Visible = false;
                dataGridViewFusion.Columns["Actual"].Visible = false;
                dataGridViewFusion.Columns["HotMetalAttributes"].Visible = false;
                dataGridViewFusion.Columns["AggregateLifeTime"].Visible = false;
                dataGridViewFusion.AutoResizeColumns();
                dataGridViewFusion.Update();

                BSOffGas.DataSource = CurrentListOfGas;
                dataGridViewOffGas.DataSource = null;
                dataGridViewOffGas.DataSource = BSOffGas;
                //dataGridViewOffGas.Columns["Date"].Visible = false;
                dataGridViewOffGas.Columns["ID"].Visible = false;
                dataGridViewOffGas.Columns["FusionID"].Visible = false;
                dataGridViewOffGas.Columns["PhaseNo"].Visible = false;
                dataGridViewOffGas.Columns["Date"].HeaderText = "Время";
                dataGridViewOffGas.Columns["Temperature"].HeaderText = "Температура";
                dataGridViewOffGas.Columns["Flow"].HeaderText = "Объем";
                dataGridViewOffGas.Columns["TemperatureOnExit"].HeaderText = "Tемпература газов на выходе из котла-охладителя";
                dataGridViewOffGas.Columns["PrecollingTemperature"].HeaderText = "Tемпература отходящих газов после предварительного охлаждения";
                dataGridViewOffGas.Columns["TemperatureAfter1Step"].HeaderText = "Tемпература отходящих газов после первой ступени";
                dataGridViewOffGas.Columns["TemperatureAfter2Step"].HeaderText = "Tемпература отходящих газов после второй ступени";
                dataGridViewOffGas.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";
                dataGridViewOffGas.Update();

                BSLance.DataSource = CurrentListLance;
                dataGridViewLance.DataSource = null;
                dataGridViewLance.DataSource = BSLance;
                dataGridViewLance.Columns["ID"].Visible = false;
                dataGridViewLance.Columns["FusionId"].Visible = false;
                dataGridViewLance.Columns["PhaseNo"].Visible = false;
                //dataGridViewLance.Columns["Date"].Visible = false;
                dataGridViewLance.Columns["Date"].HeaderText = "Время";
                dataGridViewLance.Columns["O2Vol"].HeaderText = "Объем";
                dataGridViewLance.Columns["O2Flow"].HeaderText = "Интенсивность";
                dataGridViewLance.Columns["O2Pressure"].HeaderText = "Давление";
                dataGridViewLance.Columns["Height"].HeaderText = "Положение";
                dataGridViewLance.Columns["O2LeftLanceGewBaer"].HeaderText = "Левая-настыль";
                dataGridViewLance.Columns["O2LeftLanceGewWeight"].HeaderText = "Левая-вес";
                dataGridViewLance.Columns["O2LeftLanceLeck"].HeaderText = "Левая-течь";
                dataGridViewLance.Columns["O2LeftLanceWaterInput"].HeaderText = "Левая-Q воды вход";
                dataGridViewLance.Columns["O2LeftLanceWaterOutput"].HeaderText = "Левая-Q воды слив";
                dataGridViewLance.Columns["O2LeftLanceWaterPressure"].HeaderText = "Левая-давление";
                dataGridViewLance.Columns["O2LeftLanceWaterTempInput"].HeaderText = "Левая-Т вход";
                dataGridViewLance.Columns["O2LeftLanceWaterTempOutput"].HeaderText = "Левая- Т слив";
                dataGridViewLance.Columns["O2RightLanceGewBaer"].HeaderText = "Правая-настыль";
                dataGridViewLance.Columns["O2RightLanceGewWeight"].HeaderText = "Правая-вес";
                dataGridViewLance.Columns["O2RightLanceLeck"].HeaderText = "Правая-течь";
                dataGridViewLance.Columns["O2RightLanceWaterInput"].HeaderText = "Правая-Q воды вход";
                dataGridViewLance.Columns["O2RightLanceWaterOutput"].HeaderText = "Правая-Q воды слив";
                dataGridViewLance.Columns["O2RightLanceWaterPressure"].HeaderText = "Правая-Давление";
                dataGridViewLance.Columns["O2RightLanceWaterTempInput"].HeaderText = "Правая-Т вход";
                dataGridViewLance.Columns["O2RightLanceWaterTempOutput"].HeaderText = "Правая-Т слив";
                dataGridViewLance.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";
                dataGridViewLance.Update();

                BSAddition.DataSource = m_Db.GetAdditions(CurrentFussion.ID);
                dataGridViewTrakt.DataSource = null;
                dataGridViewTrakt.DataSource = BSAddition;
                dataGridViewTrakt.Columns["ID"].Visible = false;
                dataGridViewTrakt.Columns["FusionId"].Visible = false;
                dataGridViewTrakt.Columns["MaterialId"].Visible = false;
                dataGridViewTrakt.Columns["LancePosition"].Visible = false;
                dataGridViewTrakt.Columns["O2TotalVol"].Visible = false;
                dataGridViewTrakt.Columns["DataSource"].Visible = false;
                dataGridViewTrakt.Columns["Date"].HeaderText = "Время";
                dataGridViewTrakt.Columns["MaterialName"].HeaderText = "Материал";
                dataGridViewTrakt.Columns["PortionWeight"].HeaderText = "Вес порции";
                dataGridViewTrakt.Columns["TotalWeight"].HeaderText = "Общий вес";
                dataGridViewTrakt.Columns["Destination"].HeaderText = "Получатель";
                dataGridViewTrakt.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";
                dataGridViewTrakt.Update();

                BSBathLevel = new BindingSource();
                BSBathLevel.DataSource = CurrentListBathLevel;
                dataGridViewMetalMirror.DataSource = null;
                dataGridViewMetalMirror.DataSource = BSBathLevel;
                dataGridViewMetalMirror.Columns["FusionID"].Visible = false;
                dataGridViewMetalMirror.Columns["ID"].Visible = false;
                dataGridViewMetalMirror.Columns["Date"].HeaderText = "Время";
                dataGridViewMetalMirror.Columns["Value"].HeaderText = "Значение";
                dataGridViewMetalMirror.Update();

                BSScrapBuckets = new BindingSource();
                BSScrapBuckets.DataSource = m_Db.GetScrapBuckets(CurrentFussion.ID);
                dataGridViewScrapBuckets.DataSource = null;
                dataGridViewScrapBuckets.DataSource = BSScrapBuckets;
                dataGridViewScrapBuckets.Columns["ID"].Visible = false;
                dataGridViewScrapBuckets.Columns["FusionId"].Visible = false;
                dataGridViewScrapBuckets.Columns["MaterialId"].Visible = false;
                dataGridViewScrapBuckets.Columns["MaterialNumber"].Visible = false;
                dataGridViewScrapBuckets.Columns["Number"].Visible = false;
                dataGridViewScrapBuckets.Columns["MaterialName"].HeaderText = "Материал";
                dataGridViewScrapBuckets.Columns["Weight"].HeaderText = "Вес";
                dataGridViewScrapBuckets.Update();


                BSHotMetal = new BindingSource();
                BSHotMetal.DataSource = m_Db.GetHotMetal(CurrentFussion.ID);
                dataGridViewHotMetal.DataSource = null;
                dataGridViewHotMetal.DataSource = BSHotMetal;
                dataGridViewHotMetal.Columns["Temperature"].HeaderText = "Температура";
                dataGridViewHotMetal.Columns["Weight"].HeaderText = "Вес";
                dataGridViewHotMetal.Update();

                BSSlagAnalysys = new BindingSource();
                BSSlagAnalysys.DataSource = m_Db.GetSlagAnalysys(CurrentFussion.Number);
                dataGridViewSlagAnalysys.DataSource = null;
                dataGridViewSlagAnalysys.DataSource = BSSlagAnalysys;
                dataGridViewSlagAnalysys.Columns["Time"].HeaderText = "Время пробы";
                dataGridViewSlagAnalysys.Columns["ProbeNumber"].HeaderText = "№ пробы";
                dataGridViewSlagAnalysys.Update();

                BSSteelAnalysys = new BindingSource();
                BSSteelAnalysys.DataSource = m_Db.GetSteelAnalysys(CurrentFussion.Number);
                dataGridViewSteelAnalysys.DataSource = null;
                dataGridViewSteelAnalysys.DataSource = BSSteelAnalysys;
                dataGridViewSteelAnalysys.Columns["Time"].HeaderText = "Время пробы";
                dataGridViewSteelAnalysys.Columns["ProbeNumber"].HeaderText = "№ пробы";
                dataGridViewSteelAnalysys.Update();


                BSSublance = new BindingSource();
                BSSublance.DataSource = m_Db.GetSublance(CurrentFussion.ID);
                dataGridViewSublance.DataSource = null;
                dataGridViewSublance.DataSource = BSSublance;
                dataGridViewSublance.Columns["StartDate"].HeaderText = "Старт зонда";
                dataGridViewSublance.Columns["Temperature"].HeaderText = "Tемпература (град)";
                dataGridViewSublance.Columns["Oxigen"].HeaderText = "Окисленность";
                dataGridViewSublance.Columns["C"].HeaderText = "Углерод, % [C]";
                dataGridViewSublance.Update();

                CreatePassport();

                UpdateGraph();
            }
            catch { }
        }


        public void CreatePassport()
        {
            int row = 0;
            string[] passportStrings = FillBySpace(250, 50);

            string newline = "";
            textBoxPassport.Clear();
            PrintXY(ref passportStrings, 0, 0, string.Format("                                                          ПАСПОРТ ПЛАВКИ №{0}", CurrentFussion.Number));
            PrintXY(ref passportStrings, 0, 1, string.Format("                                                          ======================="));
            PrintXY(ref passportStrings, 0, 2, string.Format("Выплавка... {0} - {1}    Марка стали задано...{2,-35}        Стойкость футеровки /{3,4:D}/",
                CurrentFussion.StartDate.ToString(), CurrentFussion.EndDate.ToString(), CurrentFussion.Grade, CurrentFussion.AggregateLifeTime));
            PrintXY(ref passportStrings, 0, 3, "ЗАДАНО НА ПЛАВКУ ВСЕГО ============================================================================================================================");
            PrintXY(ref passportStrings, 0, 4, "ЧУГУН       ЛОМ           По сортам                                                      Отдано на плавку                            Ферросплавы");

            // Рисуем данные по чугуну. Загрузка по миксерам.
            HotMetalLadle hotMetalLadle = m_Db.GetHotMetalLadle(CurrentFussion.Number);
            PrintXY(ref passportStrings, 0, 5, "Ном Вес(т)"); // Совок Вес(т) Сорт";
            row = 0;
            string[] newlines = new string[hotMetalLadle.Torpedes.Count];
            foreach (HotMetalTorpedo torpedo in hotMetalLadle.Torpedes)
            {
                newlines[row++] = string.Format("{0,-2:D} / {1,-3:D}", torpedo.Number, torpedo.Weight);
            }
            PrintXY(ref passportStrings, 0, 6, newlines);

            // Рисуем данные по скрапу. Загрузка по сортам.
            List<ScrapBucket> scrapBuckets = (List<ScrapBucket>)BSScrapBuckets.DataSource;
            PrintXY(ref passportStrings, 11, 5, "  Совок Вес(т) Сорт");
            if (scrapBuckets.Count > 0)
                PrintXY(ref passportStrings, 11, 6, string.Format("  {0,2:D}(6) / {1,-5:F1}Вес", scrapBuckets[0].Number, scrapBuckets.Sum(p => p.Weight) / 1000));
            newlines = new string[2];
            for (int i = 0; i < 8; i++)
            {
                newlines[0] += " ";
                newlines[1] += " ";
                if (i < scrapBuckets.Count)
                {
                    newlines[0] += string.Format("{0,4:D}", scrapBuckets[i].MaterialNumber);
                    newlines[1] += string.Format("{0,4:F1}", scrapBuckets[i].Weight / 1000);
                }
                else
                {
                    newlines[0] += "    ";
                    newlines[1] += "    ";
                }
                newlines[0] += "/";
                newlines[1] += "/";
            }
            PrintXY(ref passportStrings, 31, 5, newlines);


            // Рисуем блок под чугуном и скрапом. Итоговые данные.
            row = 9;
            passportStrings[row++] = string.Format("{0,2:D}*/ {1,-4:F1} Всего {2,-4:F1} (Черный лом)", hotMetalLadle.Number, hotMetalLadle.Weight, scrapBuckets.Sum(p => p.Weight) / 1000);
            EventDuration scrapDuration = m_Db.GetScrapHeatingDuration(CurrentFussion.ID);
            passportStrings[row++] = "Время прогрева лома";
            passportStrings[row++] = string.Format("c {0} по {1} ({2})", scrapDuration.StartDate, scrapDuration.EndDate, scrapDuration.Value.ToString());
            passportStrings[row++] = "Материал под конвертер/";
            passportStrings[row] = "Материал c раб. площадки/";
            List<Addition> additions = (List<Addition>)BSAddition.DataSource;
            var data = additions.Where(p => p.DataSource == "MAN" && p.Destination == "LDL").ToList();
            newline = " ";
            foreach (Addition addition in data)
            {
                newline += string.Format(" {0} - {1}/", addition.MaterialName, addition.PortionWeight);
            }
            PrintXY(ref passportStrings, 25, 13, newline);
            passportStrings[14] = "ПРИСАДКИ ПО ХОДУ ПЛАВКИ ===========================================================================================================================";
            data = additions.Where(p => p.Destination == "CV").ToList();
            passportStrings[15] = string.Format("{0,-14}", "Время (Час Мин)");
            DateTime[] additionsDates = data.Select(p => p.Date).Distinct().ToArray();
            newline = "";
            foreach (DateTime date in additionsDates)
            {
                newline += string.Format(" {0}", date.TimeOfDay.ToString("hh':'mm"));
            }
            PrintXY(ref passportStrings, 15, 15, newline);
            newline = "";
            // Отдано на плавку 
            string[] additionsOnHeat = new string[2];
            foreach (string add in data.Select(p => p.MaterialName).Distinct().ToList())
            {
                additionsOnHeat[0] += string.Format(" {0,-6}", add);
                additionsOnHeat[1] += string.Format(" {0,-6}", data.Find(p => p.MaterialName == add).TotalWeight);
            }
            PrintXY(ref passportStrings, 79, 5, additionsOnHeat);

            string[] additionNames = data.Select(p => p.MaterialName).Distinct().ToArray(); ;
            string[] additionsLanceO2 = new string[2 + additionNames.Length];

            additionsLanceO2[0] = string.Format("{0,-14} ", "Пол фурмы (см)");
            additionsLanceO2[1] = string.Format("{0,-14} ", "Кислород (м3)");
            for (int i = 2; i < additionsLanceO2.Length; i++)
            {
                additionsLanceO2[i] = string.Format("{0,-14} ", additionNames[i - 2]);
            }
            foreach (DateTime dat in additionsDates)
            {
                var sel = data.Where(p => p.Date == dat).ToList();
                for (int i = 2; i < additionsLanceO2.Length; i++)
                {
                    Addition add = sel.Find(p => p.MaterialName == additionNames[i - 2]);
                    additionsLanceO2[i] += add != null ? string.Format(" {0,-5}", add.PortionWeight) : string.Format(" {0,-5}", " ");

                }
                additionsLanceO2[0] += string.Format(" {0,-5}", sel[0].LancePosition);
                additionsLanceO2[1] += sel[0].O2TotalVol > 0 ? string.Format(" {0,-5}", sel[0].O2TotalVol) : string.Format(" {0,-5}", " ");
            }
            //MessageBox.Show(passportStrings[4].IndexOf("Отдано").ToString());
            row = 16;
            foreach (string str in additionsLanceO2)
            {
                passportStrings[row] = "";
                passportStrings[row] = passportStrings[row].Insert(passportStrings[row++].Length, str);
            }
            // Отдано на выпуске 
            PrintXY(ref passportStrings, 89, 7, "Отдано на выпуске");
            newlines = new string[2];
            foreach (Addition add in m_Db.GetAdditionsDozen(CurrentFussion.ID))
            {
                newlines[0] += string.Format(" {0,-6}", add.MaterialName);
                newlines[1] += string.Format(" {0,-6}", add.PortionWeight);
            }
            PrintXY(ref passportStrings, 79, 8, newlines);
            // Продолжительность оперций
            passportStrings[row] = "ПРОДОЛЖИТЕЛЬНОСТЬ ОПЕРАЦИЙ ";
            passportStrings[row] += (new StringBuilder().Insert(0, "=", 147 - passportStrings[row++].Length)).ToString();
            passportStrings[row++] = string.Format("{0,-9} Плавка   Завалка  Залив    Допрод.             Продувки            Выпуск   Слив     Вып с ***Общ. длит. прод. (мин. сек)//", " ", "ПРОДУВКИ");
            passportStrings[row++] = string.Format("{0,-19}лома     чугуна   операц.     1     2     3     4     5   стали    шлака    ОТК   ***Расход О2 общий (нм3)      //", " ");
            // 19
            newlines = new string[4];
            newlines[0] = string.Format("{0,-10}", "Начало");
            newlines[1] = string.Format("{0,-10}", "Конец");
            newlines[2] = string.Format("{0,-10}", "Длит(мин)");
            newlines[3] = string.Format("Расход О2 по циклам (нм3)");
            PrintXY(ref passportStrings, 0, 27, newlines);

            // Продолжительность плавки
            newlines = new string[3];
            newlines[0] = CurrentFussion.StartDate.TimeOfDay.ToString();
            newlines[1] = CurrentFussion.EndDate.TimeOfDay.ToString();
            newlines[2] = (CurrentFussion.EndDate - CurrentFussion.StartDate).ToString();
            PrintXY(ref passportStrings, 10, 27, newlines);

            // Продолжительность завалки лома
            newlines = new string[3];
            EventDuration scrap = m_Db.GetScrapChargingDuration(CurrentFussion.ID);
            newlines[0] = scrap.StartDate.TimeOfDay.ToString();
            newlines[1] = scrap.EndDate.TimeOfDay.ToString();
            newlines[2] = (scrap.EndDate - scrap.StartDate).ToString();
            PrintXY(ref passportStrings, 19, 27, newlines);

            // Продолжительность заливки чугуна
            newlines = new string[3];
            EventDuration hotmetal = m_Db.GetHotMetalChargingDuration(CurrentFussion.ID);
            newlines[0] = hotmetal.StartDate.TimeOfDay.ToString();
            newlines[1] = hotmetal.EndDate.TimeOfDay.ToString();
            newlines[2] = (hotmetal.EndDate - hotmetal.StartDate).ToString();
            PrintXY(ref passportStrings, 28, 27, newlines);

            // Продувки
            List<EventDuration> blowings = m_Db.GetBlowings(CurrentFussion.ID);
            for (int i = 0; i < blowings.Count; i++)
            {
                newlines = new string[4];
                newlines[0] = blowings[i].StartDate.TimeOfDay.ToString("hh':'mm");
                newlines[1] = blowings[i].EndDate.TimeOfDay.ToString("hh':'mm");
                newlines[2] = (blowings[i].EndDate - blowings[i].StartDate).ToString("mm':'ss");
                newlines[3] = blowings[i].Tag.ToString();
                PrintXY(ref passportStrings, 47 + i * 6, 27, newlines);
            }

            // Продолжительность слива стали
            newlines = new string[3];
            EventDuration tap = m_Db.GetTapsDuration(CurrentFussion.ID);
            newlines[0] = tap.StartDate.TimeOfDay.ToString();
            newlines[1] = tap.EndDate.TimeOfDay.ToString();
            newlines[2] = (tap.EndDate - tap.StartDate).ToString();
            PrintXY(ref passportStrings, 77, 27, newlines);

            // Продолжительность слива шлака
            newlines = new string[3];
            EventDuration deslag = m_Db.GetDeslagingDuration(CurrentFussion.ID);
            newlines[0] = deslag.StartDate.TimeOfDay.ToString();
            newlines[1] = deslag.EndDate.TimeOfDay.ToString();
            newlines[2] = (deslag.EndDate - deslag.StartDate).ToString();
            PrintXY(ref passportStrings, 86, 27, newlines);

            // Общ длительность продувки.
            TimeSpan sumBlowingDuration = new TimeSpan();
            foreach (EventDuration blow in blowings)
            {
                sumBlowingDuration += blow.Value;
            }
            PrintXY(ref passportStrings, 132, 25, sumBlowingDuration.ToString("mm':'ss"));

            // Общий расход О2
            PrintXY(ref passportStrings, 132, 26, blowings.Sum(p => (int)p.Tag).ToString());

            // Химия чугуна и шлака
            passportStrings[31] = "ХИМИЯ ЧУГУНА И ШЛАКА ";
            passportStrings[31] += (new StringBuilder().Insert(0, "=", 147 - passportStrings[31].Length)).ToString();
            PrintXY(ref passportStrings, 0, 32,
                string.Format("Чугун Микс   Ковш  Время"));

            // Химия чугуна по миксерам
            newlines = new string[hotMetalLadle.Torpedes.Count];
            string[] chemlines = new string[hotMetalLadle.Torpedes.Count + 1];

            Type t = typeof(HotMetalAnalysys);
            PropertyInfo[] pi = t.GetProperties();
            foreach (PropertyInfo property in pi)
            {
                if (property.Name == "Time" || property.Name == "ProbeNumber" || property.Name == "TorpedoNumber" || property.Name == "LadleNumber") continue;
                chemlines[0] += string.Format("{0,-5} ", property.Name);
            }

            for (int i = 0; i < hotMetalLadle.Torpedes.Count; i++)
            {
                newlines[i] = string.Format("      {0,-6} {1,-5} /{2}", hotMetalLadle.Torpedes[i].Number, "/", hotMetalLadle.Torpedes[i].ChargeTime.ToString("hh':'mm"));
                foreach (PropertyInfo property in pi)
                {
                    if (property.Name == "Time" || property.Name == "ProbeNumber" || property.Name == "TorpedoNumber" || property.Name == "LadleNumber") continue;
                    chemlines[i + 1] += string.Format("{0,-5:F2} ", property.GetValue(hotMetalLadle.Torpedes[i].Analysys, null));
                }
            }
            PrintXY(ref passportStrings, 0, 33, newlines);
            PrintXY(ref passportStrings, 30, 32, chemlines);
            newline = string.Format("      {0,-6} {1,-5} /{2}", " ", hotMetalLadle.Number, hotMetalLadle.ChargeTime.ToString("hh':'mm"));
            PrintXY(ref passportStrings, 0, 35, newline);
            newline = "";
            foreach (PropertyInfo property in pi)
            {
                if (property.Name == "Time" || property.Name == "ProbeNumber" || property.Name == "TorpedoNumber" || property.Name == "LadleNumber") continue;
                newline += string.Format("{0,-5:F2} ", property.GetValue(hotMetalLadle.Analysys, null));
            }
            PrintXY(ref passportStrings, 30, 35, newline);
            newline = string.Format("Температура чугуна (град С) /{0}/", ((HotMetal)BSHotMetal.DataSource).Temperature);
            PrintXY(ref passportStrings, 30, 36, newline);
            // Шлак
            PrintXY(ref passportStrings, 65, 32, "Шлак");
            pi = typeof(SlagAnalysys).GetProperties();
            List<SlagAnalysys> slagAnalysys = m_Db.GetSlagAnalysys(CurrentFussion.Number);
            newlines = new string[slagAnalysys.Count + 1];
            foreach (PropertyInfo property in pi)
            {
                switch (property.Name)
                {
                    case "Time": newlines[0] += string.Format(" {0,-5}", "Bремя"); break;
                    case "ProbeNumber": newlines[0] += string.Format(" {0,-5}", "№пр"); break;
                    default: newlines[0] += string.Format(" {0,-5}", property.Name); break;
                }
                for (int i = 0; i < slagAnalysys.Count; i++)
                {
                    if (property.Name == "Time")
                    {
                        newlines[i + 1] += string.Format(" {0,-5}", slagAnalysys[i].Time.ToString("hh':'mm"));
                    }
                    else
                    {
                        newlines[i + 1] += string.Format(" {0,-5}", property.GetValue(slagAnalysys[i], null));
                    }
                }
            }
            PrintXY(ref passportStrings, 70, 32, newlines);
           

            // Сталь на повалке
            passportStrings[37] = "СТАЛЬ НА ПОВАЛКЕ ";
            passportStrings[37] += (new StringBuilder().Insert(0, "=", 147 - passportStrings[37].Length)).ToString();
            List<SteelAnalysys> steelAnalysys = m_Db.GetSteelAnalysys(CurrentFussion.Number);
            pi = typeof(SteelAnalysys).GetProperties();
            newlines = new string[steelAnalysys.Count + 1];
            foreach (PropertyInfo property in pi)
            {
                switch (property.Name)
                {
                    case "Time": newlines[0] += string.Format(" {0,-5}", "Bремя"); break;
                    case "ProbeNumber": newlines[0] += string.Format(" {0,-5}", "№пр"); break;
                    default: newlines[0] += string.Format(" {0,-5}", property.Name); break;
                }
                for (int i = 0; i < steelAnalysys.Count; i++)
                {
                    if (property.Name == "Time")
                    {
                        newlines[i + 1] += string.Format(" {0,-5}", steelAnalysys[i].Time.ToString("hh':'mm"));
                    }
                    else
                    {
                        newlines[i + 1] += string.Format(" {0,-5}", property.GetValue(steelAnalysys[i], null));
                    }
                }
            }
            PrintXY(ref passportStrings, 0, 38, newlines);

            // Зонд и заданные значения стали
            PrintXY(ref passportStrings, 0, 41, string.Format("             {0,-19}   Задан.  Tемпература /{1,-4}/  %C /{2,-5:F3}/", " ", CurrentFussion.PlannedTemperature, CurrentFussion.PlannedCarbon));
            List<Sublance> sublances = m_Db.GetSublance(CurrentFussion.Number);
            for (var i = 0; i < sublances.Count; i++)
            {
                PrintXY(ref passportStrings, 0, 42 + i, string.Format("Старт зонда /{0}/  Факт.   Tемпература /{1,-4}/  %C /{2,-5:F3}/ Окисленность /{3}/", sublances[i].StartDate, sublances[i].Temperature, sublances[i].C, sublances[i].Oxigen));
            }
         

            WriteToPassport(passportStrings);
        }

        private string[] FillBySpace(int countX, int countY)
        {
            string[] resultLines = new string[countY];

            for (int i = 0; i < countY; i++)
            {
                resultLines[i] = "";
                resultLines[i] = new StringBuilder().Insert(0, " ", countX).ToString();
            }
            return resultLines;
        }

        private void PrintXY(ref string[] passport, int X, int Y, string[] lines)
        {
            foreach (string line in lines)
            {
                if (X > passport[Y].Length)
                {
                    passport[Y] = passport[Y].Insert(passport[Y].Length, new StringBuilder().Insert(0, " ", X - passport[Y].Length).ToString());
                }
                passport[Y] = passport[Y++].Insert(X, line);
            }
        }

        private void PrintXY(ref string[] passport, int X, int Y, string line)
        {
            PrintXY(ref passport, X, Y, new string[] { line });
        }

        void WriteToPassport(string[] passportStrings)
        {
            //foreach (string line in passportStrings)
            //{
            //    tbPassport.AppendText(String.Format("{0}{1}", line, Environment.NewLine));
            //}

            textBoxPassport.Lines = passportStrings.Select(x => x.TrimEnd()).ToArray();
        }


       private void UpdateGraph()
        {

            zGraph.GraphPane.CurveList.Clear();
            zGraph.RestoreScale(zGraph.GraphPane);

            PointPairList pointsH2 = new PointPairList();
            PointPairList pointsO2 = new PointPairList();

            PointPairList pointsCO = new PointPairList();
            PointPairList pointsCO2 = new PointPairList();
            PointPairList pointsN2 = new PointPairList();
            PointPairList pointsAr = new PointPairList();
            PointPairList pointsTemp = new PointPairList();
            PointPairList pointsFlow = new PointPairList();
            PointPairList pointsRealCO = new PointPairList();
            PointPairList pointsRealCO2 = new PointPairList();
            PointPairList pointsRealFlow = new PointPairList();

            double time;
            foreach (OffGas og in CurrentListOfGas)
            {
                time = (og.Date - CurrentFussion.StartDate).TotalSeconds;
                pointsH2.Add(time, og.H2);
                pointsO2.Add(time, og.O2);
                pointsCO.Add(time, og.CO);
                pointsCO2.Add(time, og.CO2);
                pointsN2.Add(time, og.N2);
                pointsAr.Add(time, og.Ar);
                pointsTemp.Add(time, og.Temperature);
                pointsFlow.Add(time, og.Flow / 60);
            }

            foreach (OffGasAnalysisEvent ogaEvent in _heat.OffGasAnalysisHistory)
            {
                time = (ogaEvent.Time - CurrentFussion.StartDate).TotalSeconds;
                pointsRealCO.Add(time, ogaEvent.CO);
                pointsRealCO2.Add(time, ogaEvent.CO2);
            }

            foreach (OffGasEvent ogEvent in _heat.OffGasHistory)
            {
                time = (ogEvent.Time - CurrentFussion.StartDate).TotalSeconds;
                pointsRealFlow.Add(time, ogEvent.OffGasFlow / 60);
            }

            if (cbH2.Checked)
            {
                zGraph.GraphPane.AddCurve("H2", pointsH2, Color.Green, SymbolType.None);
            }
            if (cbO2.Checked)
            {
                zGraph.GraphPane.AddCurve("O2", pointsO2, Color.Blue, SymbolType.None);
            }
            if (cbCO.Checked)
            {
                zGraph.GraphPane.AddCurve("CO", pointsCO, Color.Red, SymbolType.None);
            }
            if (cbCO2.Checked)
            {
                zGraph.GraphPane.AddCurve("CO2", pointsCO2, Color.Orange, SymbolType.None);
            }
            if (cbN2.Checked)
            {
                zGraph.GraphPane.AddCurve("N2", pointsN2, Color.Black, SymbolType.None);
            }
            if (cbAr.Checked)
            {
                zGraph.GraphPane.AddCurve("Ar", pointsAr, Color.Turquoise, SymbolType.None);
            }
            if (cbFlow.Checked)
            {
                zGraph.GraphPane.AddCurve("Flow", pointsFlow, Color.SaddleBrown, SymbolType.None);
            }
            if (cbTemp.Checked)
            {
                zGraph.GraphPane.AddCurve("Temp", pointsTemp, Color.OliveDrab, SymbolType.None);
            }
            if (cbRealCO.Checked)
            {
                zGraph.GraphPane.AddCurve("RealCO", pointsRealCO, Color.DarkGray, SymbolType.None);
            }
            if (cbRealCO2.Checked)
            {
                zGraph.GraphPane.AddCurve("RealCO2", pointsRealCO2, Color.Fuchsia, SymbolType.None);
            }
            if (cbRealFlow.Checked)
            {
                zGraph.GraphPane.AddCurve("RealFlow", pointsRealFlow, Color.MidnightBlue, SymbolType.None);
            }

            PointPairList pointsLance = new PointPairList();
            PointPairList pointsOFlow = new PointPairList();

            List<Lance> lanceHeights = CurrentListLance;//m_Db.GetLance(CurrentFussion.Id);
            if (lanceHeights.Count > 0)
            {
                int lanceHeight = lanceHeights.First().Height;

                for (double second = (lanceHeights.First().Date - CurrentFussion.StartDate).TotalSeconds; second < (lanceHeights.Last().Date - CurrentFussion.StartDate).TotalSeconds; second++)
                {
                    foreach (Lance lance in lanceHeights)
                    {
                        if (second > 0)
                        {
                            if ((int)(lance.Date - CurrentFussion.StartDate).TotalSeconds == (int)second)
                            {
                                pointsLance.Add(second, lance.Height * Zoom);
                                pointsOFlow.Add(second, lance.O2Flow * Zoom);
                                lanceHeight = lance.Height;
                                continue;
                            }
                            pointsLance.Add(second, lanceHeight);
                        }
                    }
                }
            }
            if (cbLance.Checked)
            {
                zGraph.GraphPane.AddCurve("Фурма", pointsLance, Color.Lime, SymbolType.None);
            }
            if (cbOFlow.Checked)
            {
                zGraph.GraphPane.AddCurve("OFlow", pointsOFlow, Color.Magenta, SymbolType.None);
            }

            //PointPairList pointsAdditions = new PointPairList();

            if (gbVars.Controls.Count > 0)
            {
                MathParser mathParser = new MathParser();
                int varsCount = gbVars.Controls.Count / 3;
                for (int i = 0; i < varsCount; i++)
                {
                    if ((gbVars.Controls.Find(string.Format("cb{0}", i), true)[0] as System.Windows.Forms.CheckBox).Checked)
                    {
                        PointPairList points = new PointPairList();
                        foreach (OffGas og in CurrentListOfGas)
                        {
                            mathParser.CreateVar("H2", og.H2, null);
                            mathParser.CreateVar("O2", og.O2, null);
                            mathParser.CreateVar("CO", og.CO, null);
                            mathParser.CreateVar("CO2", og.CO2, null);
                            mathParser.CreateVar("N2", og.N2, null);
                            mathParser.CreateVar("Ar", og.Ar, null);
                            mathParser.Expression = (gbVars.Controls.Find(string.Format("tb{0}", i), true)[0] as System.Windows.Forms.TextBox).Text;
                            try
                            {
                                points.Add((og.Date - CurrentFussion.StartDate).TotalSeconds, mathParser.ValueAsDouble * Zoom);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(string.Format("Переменная {0} задана неверно, ошибка в формуле", (gbVars.Controls.Find(string.Format("lb{0}", i), true)[0] as System.Windows.Forms.Label).Text));
                                return;
                            }
                        }
                        mathParser.CreateVar((gbVars.Controls.Find(string.Format("lb{0}", i), true)[0] as System.Windows.Forms.Label).Text,
                                              mathParser.ValueAsString,
                                              null);
                        if ((gbVars.Controls.Find(string.Format("cb{0}", i), true)[0] as System.Windows.Forms.CheckBox).Checked)
                        {
                            zGraph.GraphPane.AddCurve((gbVars.Controls.Find(string.Format("lb{0}", i), true)[0] as System.Windows.Forms.Label).Text, points, Color.DeepPink, SymbolType.None);
                        }
                    }
                }
            }

            zGraph.GraphPane.Title.IsVisible = false;
            this.Text = string.Format("Плавка: {0}  Марка: {1}  Начало: {2}({3})  Бригада: {4}  Конвертер:{5}  Т зад={6}  Т факт={7}  С зад={8}  С факт={9}",
                                                     CurrentFussion.Number, CurrentFussion.Grade, ((HeatAttributes) CurrentFussion).StartDate, CurrentFussion.StartDate,
                                                     CurrentFussion.TeamNumber, CurrentFussion.AggregateNumber, CurrentFussion.PlannedTemperature,
                                                     CurrentFussion.FactTemperature, CurrentFussion.PlannedCarbon, CurrentFussion.FactCarbon);

            zGraph.AxisChange();
            zGraph.Invalidate();
        }

        private void numericUpDownConverterNumber_ValueChanged(object sender, EventArgs e)
        {
            buttonGet_Click(null, null);
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
        }

        private void SaveToExcel(string directoryName)
        {
            ExcelExport exc = new ExcelExport(CurrentFussion.Number.ToString());
            exc.Do(dataGridViewScrapBuckets, "Шихта");
            exc.Do(dataGridViewMetalMirror, "Зеркало металла");
            exc.Do(dataGridViewOffGas, "Отходящие газы");
            exc.Do(dataGridViewLance, "Фурма");
            exc.Do(dataGridViewTrakt, "Тракт сыпучих");
            exc.Do(dataGridViewHotMetal, "Чугун");
            exc.Do(dataGridViewSteelAnalysys, "Сталь на повалке");
            exc.Do(dataGridViewSlagAnalysys, "Шлак");
            exc.Do(dataGridViewSublance, "Измерительный зонд");
            exc.Do(textBoxPassport.Lines, "Паспорт плавки");
            exc.Do(dataGridViewFusion, "Плавка");
            exc.Save(directoryName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wait = new HeatPassport.FormProggress(this, 100);
                wait.Show();
                textBox1.Text = fb.SelectedPath;
                wait.progressBar1.Value = 50;
                SaveToExcel(fb.SelectedPath);
                wait.progressBar1.Value = 90;
                wait.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = fb.SelectedPath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //zGraph.IsEnableHZoom = false;
            //zGraph.IsEnableVZoom = false;
            zGraph.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zGraph.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zGraph.GraphPane.XAxis.Title.IsVisible = false;
            zGraph.GraphPane.YAxis.Title.IsVisible = false;
            zGraph.GraphPane.Title.FontSpec.Size = 12;
            zGraph.GraphPane.XAxis.Scale.FontSpec.Size = 10;
            zGraph.GraphPane.YAxis.Scale.FontSpec.Size = 10;
            zGraph.GraphPane.XAxis.Scale.MajorStep = 300;
            zGraph.GraphPane.XAxis.Scale.MinorStep = 60;
            zGraph.GraphPane.Legend.IsVisible = false;
            zGraph.GraphPane.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler(XAxis_ScaleFormatEvent);
        }

        string XAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            return (string.Format("{0}:{1}", (int)(val / 60), (val % 60).ToString("00")));
        }

        private void cbH2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbO2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbCO_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbCO2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbN2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbAr_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private bool zGraph_MouseMoveEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            double x, y;
            zGraph.GraphPane.ReverseTransform(e.Location, out x, out y);
            if (zGraph.GraphPane.CurveList.Count > 0)
            {
                for (int curveCount = 0; curveCount < zGraph.GraphPane.CurveList.Count; curveCount++)
                {
                    for (int i = 0; i < zGraph.GraphPane.CurveList[curveCount].Points.Count; i++)
                    {
                        if (zGraph.GraphPane.CurveList[curveCount].Points[i].X == (int)x)
                        {
                            double time = zGraph.GraphPane.CurveList[curveCount].Points[i].X;
                            lbTimeValue.Text = string.Format("{0}:{1}", (int)(time / 60), (time % 60).ToString("00"));
                            switch (zGraph.GraphPane.CurveList[curveCount].Label.Text)
                            {
                                case "H2":
                                    lbH2Value.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "O2":
                                    lbO2Value.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "CO":
                                    lbCOValue.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "CO2":
                                    lbCO2Value.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "N2":
                                    lbN2Value.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "Ar":
                                    lbArValue.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "Фурма":
                                    lbLanceValue.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y).ToString();
                                    break;
                                case "OFlow":
                                    lbOFlowValue.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y).ToString();
                                    break;
                            }
                        }
                    }
                }
            }
            return default(bool);

        }

        private void cbLance_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbOFlow_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

       
        private void SummaryToExcel (string directoryName)
        {
            ExcelExport excel = new ExcelExport(CurrentFussion.Number.ToString());
            excel.DoCommon(CurrentFussion, CurrentListOfGas, CurrentListLance, m_Db.GetAdditions(CurrentFussion.ID), m_Db.GetSublance(CurrentFussion.ID), SummarySlagOutburst, SummaryIgnition);
            excel.Save(directoryName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wait = new HeatPassport.FormProggress(this, 100);
                wait.Show();
                textBox1.Text = fb.SelectedPath;
                wait.progressBar1.Value = 50;
                SummaryToExcel(fb.SelectedPath);
                wait.progressBar1.Value = 90;
                wait.Close();
            }        
        }

        private void gbValues_Enter(object sender, EventArgs e)
        {

        }

        private void cbTemp_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbFlow_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbRealCO_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbRealCO2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbRealFlow_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private string[] GetDatFiles(string path)
        {
            return new DirectoryInfo(path).GetFiles("*.dat").Select(p => p.FullName).ToArray();
        }


        private void EcxelProcess(string path)
        {
            var proggres = new HeatPassport.FormProggress(this);
            proggres.Show();
            m_currentFile = 0;
            foreach (string file in m_datFiles)
            {
                LoadFusionFromFile(file);
                SaveToExcel(path);
                m_currentFile++;
                proggres.NewValue();
            }
            proggres.Close();
        }

        private void ПапкуToolStripMenuItemClick(object sender, EventArgs e)
        {
            var folderDialogFrom = new FolderBrowserDialog();
            if (folderDialogFrom.ShowDialog() != DialogResult.OK) return;
            m_datFiles = GetDatFiles(folderDialogFrom.SelectedPath);
            var folderDialogTo = new FolderBrowserDialog();
            if (folderDialogTo.ShowDialog() != DialogResult.OK) return;
            EcxelProcess(folderDialogTo.SelectedPath);
        }

        internal string[] m_datFiles;
        internal int m_currentFile;


        private void EcxelProcessCommon(string path)
        {
            var proggres = new HeatPassport.FormProggress(this);
            proggres.Show();
            m_currentFile = 0;
            foreach (string file in m_datFiles)
            {
                LoadFusionFromFile(file);
                SummaryToExcel(path);
                m_currentFile++;
                proggres.NewValue();
            }
            proggres.Close();

        }

        private void сводногоОтчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderDialogFrom = new FolderBrowserDialog();
            if (folderDialogFrom.ShowDialog() != DialogResult.OK) return;
            m_datFiles = GetDatFiles(folderDialogFrom.SelectedPath);
            var folderDialogTo = new FolderBrowserDialog();
            if (folderDialogTo.ShowDialog() != DialogResult.OK) return;
            EcxelProcessCommon(folderDialogTo.SelectedPath);
        }






    }
}
