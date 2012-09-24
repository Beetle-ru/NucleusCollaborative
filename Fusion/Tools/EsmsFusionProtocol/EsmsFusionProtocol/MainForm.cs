using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CommonTypes;
using Core;
using Esms;
using NordSteel.Data;

namespace EsmsFusionProtocol
{
    public partial class MainForm : Form
    {
        private const string Ext = ".xls";
        private Type[] _eventTypes;
        private readonly List<HeatCommon> _heatList = new List<HeatCommon>();
        private readonly List<HeatCommon> _listSelectedHeats = new List<HeatCommon>();
        private readonly DBLayer _db = new DBLayer();
        private string OutputFolder { get; set; }
        private bool IsStopeed { get; set; }
        private int StepTime { get; set; }
        private int Download { get; set; }

        public MainForm()
        {
            InitializeComponent();
            DateTimePickerStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            DateTimePickerEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            IsStopeed = false;
            ButtonStopProcess.Enabled = false;
            var text = TextBoxTemplate.Text;
            ButtonDownLoad.Enabled = (text != "") && (text.Substring(text.Length - 4, 4) == Ext);
            label6.Text = string.Format(" Выбрано: {0}", 0);
            Download = 0;
            label7.Text = string.Format(" Выгружено: {0}", Download);
        }

        private IEnumerable<HeatCommon> GetHeatList(DateTime startDate, DateTime endDate, int unitNumber)
        {
            var heatList = _db.GetHeatList(startDate, endDate, unitNumber);
            for (var i = 0; i < heatList.Count() - 1; i++)
            {
                heatList[i].NextHeatNumber = heatList[i + 1].HeatNumber;
                heatList[i].NextHeatStart = heatList[i + 1].HeatStart;
                heatList[i].NextHeatEnd = heatList[i + 1].HeatEnd;
            }
            heatList.RemoveAt(0);
            return heatList;
        }

        private void DownLoadHeatsClick(object sender, EventArgs e)
        {
            _heatList.Clear();
            if (CheckBoxCN1.Checked)
            {
                _heatList.AddRange(GetHeatList(DateTimePickerStart.Value, DateTimePickerEnd.Value, 1));
            }

            if (CheckBoxCN2.Checked)
            {
                _heatList.AddRange(GetHeatList(DateTimePickerStart.Value, DateTimePickerEnd.Value, 2));
            }
            CheckedListBoxHeatNumber.Items.Clear();
            foreach (var item in _heatList)
            {
                CheckedListBoxHeatNumber.Items.Add(item.HeatNumber);
            }
        }

        private void CheckBoxAllHeatCheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < CheckedListBoxHeatNumber.Items.Count; i++)
            {
                CheckedListBoxHeatNumber.SetItemChecked(i, CheckBoxAllHeat.Checked);
            }
        }

        private void ButtonDownLoadClick(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = OutputFolder;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    OutputFolder = folderBrowserDialog.SelectedPath;
                    if (OutputFolder.Substring(OutputFolder.Length-1) != "\\")
                    {
                        OutputFolder += "\\";
                    }
                }
                else
                {
                    return;
                }
            }

            _listSelectedHeats.Clear();
            foreach (var item in CheckedListBoxHeatNumber.CheckedItems)
            {
                _listSelectedHeats.Add(_heatList.Find(p => p.HeatNumber == (int)item));
            }
            ButtonDownLoad.Enabled = false;
            ButtonStopProcess.Enabled = true;
            TextBoxTemplate.Enabled = false;
            ButtonGetTemplate.Enabled = false;
            label6.Text = string.Format(" Выбрано: {0}", _listSelectedHeats.Count);
            Download = 0;
            label7.Text = string.Format(" Выгружено: {0}", Download);
            StepTime = int.Parse(СomboBoxStepTime.Text);
            var worker = new Thread(GetDataFromDB); 
            worker.Start();
        }

        private static Heat GetProtocol(IEnumerable<BaseEvent> baseEvent, Heat protocol)
        {
            var events = baseEvent as List<BaseEvent>;
            if (events != null)
                foreach (var e in events)
                {
                    switch (e.GetType().ToString())
                    {
                        case "Esms.ArCOSEvent":
                            protocol.ArCOSHistory.Add((ArCOSEvent)e);
                            break;
                        case "Esms.Burner1Event": 
                            protocol.Burner1History.Add((Burner1Event)e);
                            break;
                        case "Esms.Burner2Event": 
                            protocol.Burner2History.Add((Burner2Event)e);
                            break;
                        case "Esms.Burner3Event": 
                            protocol.Burner3History.Add((Burner3Event)e);
                            break;
                        case "Esms.Burner4Event": 
                            protocol.Burner4History.Add((Burner4Event)e);
                            break;
                        case "Esms.CeloxEvent":
                            protocol.CeloxHistory.Add((CeloxEvent) e);
                            break;
                        case "Esms.CoalInjectionEvent": 
                            protocol.CoalInjectionHistory.Add((CoalInjectionEvent)e); 
                            break;
                        case "Esms.EnergyEvent": 
                            protocol.EnergyHistory.Add((EnergyEvent)e);
                            break;
                        case "Esms.FurnaceEvent": 
                            protocol.FurnaceHistory.Add((FurnaceEvent)e); 
                            break;
                        case "Esms.FurnaceSwitch1Event": 
                            protocol.FurnaceSwitch1History.Add((FurnaceSwitch1Event)e); 
                            break;
                        case "Esms.FurnaceSwitch2Event": 
                            protocol.FurnaceSwitch2History.Add((FurnaceSwitch2Event)e); 
                            break;
                        case "Esms.FurnaceSwitchCommonEvent": 
                            protocol.FurnaceSwitchCommonHistory.Add((FurnaceSwitchCommonEvent)e); 
                            break;
                        case "Esms.GasWasteEvent": 
                            protocol.GasWasteHistory.Add((GasWasteEvent)e); 
                            break;
                        case "Esms.HeatPassportEvent": 
                            protocol.HeatPassportHistory.Add((HeatPassportEvent)e);
                            break;
                        case "Esms.Injector1Event": 
                            protocol.Injector1History.Add((Injector1Event)e);
                            break;
                        case "Esms.Injector2Event": 
                            protocol.Injector2History.Add((Injector2Event)e);
                            break;
                        case "Esms.Injector3Event": 
                            protocol.Injector3History.Add((Injector3Event)e);
                            break;
                        case "Esms.Injector4Event": 
                            protocol.Injector4History.Add((Injector4Event)e);
                            break;
                        case "Esms.LanceCrestEvent": 
                            protocol.LanceCrestHistory.Add((LanceCrestEvent)e);
                            break;
                        case "Esms.LevelBunkerEvent": 
                            protocol.LevelBunkerHistory.Add((LevelBunkerEvent)e);
                            break;
                        case "Esms.MaterialNamesEvent": 
                            protocol.MaterialNamesHistory.Add((MaterialNamesEvent)e);
                            break;
                        case "Esms.MaterialsBucketEvent": 
                            protocol.MaterialsBucketHistory.Add((MaterialsBucketEvent)e);
                            break;
                        case "Esms.MaterialsFurnaceEvent": 
                            protocol.MaterialsFurnaceHistory.Add((MaterialsFurnaceEvent)e);
                            break;
                        case "Esms.ReactorTransformerEvent": 
                            protocol.ReactorTransformerHistory.Add((ReactorTransformerEvent)e);
                            break;
                        case "Esms.SchieberEvent": 
                            protocol.SchieberHistory.Add((SchieberEvent)e);
                            break;
                        case "Esms.ScrapLoadEvent": 
                            protocol.ScrapLoadHistory.Add((ScrapLoadEvent)e);
                            break;
                        case "Esms.SteelOutletEvent": 
                            protocol.SteelOutletHistory.Add((SteelOutletEvent)e);
                            break;
                        case "Esms.SubmissionEvent": 
                            protocol.SubmissionHistory.Add((SubmissionEvent)e);
                            break;
                        case "Esms.TempHearthEvent": 
                            protocol.TempHearthHistory.Add((TempHearthEvent)e);
                            break;
                        case "Esms.VibratingChannel3Event": 
                            protocol.VibratingChannel3History.Add((VibratingChannel3Event)e);
                            break;
                        case "Esms.VibratingChannel4Event": 
                            protocol.VibratingChannel4History.Add((VibratingChannel4Event)e);
                            break;
                        case "Esms.WaterCoolingFlueEvent": 
                            protocol.WaterCoolingFlueHistory.Add((WaterCoolingFlueEvent)e); 
                            break;
                        case "Esms.WaterCoolingMineEvent": 
                            protocol.WaterCoolingMineHistory.Add((WaterCoolingMineEvent)e);
                            break;
                        case "Esms.WaterCoolingPanelEvent": 
                            protocol.WaterCoolingPanelHistory.Add((WaterCoolingPanelEvent)e);
                            break;
                        case "Esms.WeighBunkersEvent": 
                            protocol.WeighBunkersHistory.Add((WeighBunkersEvent)e);
                            break;
                        case "Esms.WorkWindowEvent":
                            protocol.WorkWindowHistory.Add((WorkWindowEvent)e);
                            break;
                     }
                }
            return protocol;
        }

        private void GetDataFromDB()
        {
            try
            {
                Assembly.ReflectionOnlyLoadFrom("Esms.dll");
                _eventTypes = BaseEvent.GetEvents();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            Invoke((Action) (() =>
            {
                toolStripProgressBar.Maximum = _eventTypes.Count() * _listSelectedHeats.Count;
                toolStripProgressBar.Value = 0;
            }));
            foreach (var heatCommon in _listSelectedHeats)
            {
                if (IsStopeed) break;
                var heatNumber = heatCommon.HeatNumber;
                var fileName = (string.Format("{0}{1}{2}", OutputFolder, heatNumber, Ext));
                if (!File.Exists(fileName))
                {
                    var heat = _db.GetHeatInfo(heatCommon);
                    var start = heat.HeatStart;
                    var end = heat.HeatEnd;
                    var scrapLoadNext = new List<ScrapLoadEvent>();
                    var unitNumber = int.Parse(heatNumber.ToString()[0].ToString());
                    var protocol = new Heat();
                    Invoke((Action)(() => toolStripStatusLabel.Text = string.Format(" Подгружаем данные по плавке: {0}. ", heatNumber)));
                    foreach (var type in _eventTypes)
                    {
                        Invoke((Action)(() => { toolStripProgressBar.Value++; }));
                        var data = type.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBGroup").Cast<DBGroup>().Where(x => x.UnitNumber == unitNumber);
                        if (data.Count(@group => @group.UnitNumber == unitNumber) == 0) continue;
                        try
                        {
                            if (type.FullName == "Esms.ScrapLoadEvent")
                            {
                                protocol.ScrapLoadHistory = _db.GetScrapLoad(heat.HeatNumber);
                                scrapLoadNext = _db.GetScrapLoad(heat.NextHeatNumber);
                            }
                            else
                            {
                                var ev = Trends.GetEventsByType(unitNumber, type, start, end);
                                if (ev != null)
                                {
                                    protocol = GetProtocol(ev, protocol);
                                }
                            }

                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.ToString());
                        }
                    }
                    var additons = _db.GetAdditions(heat.HeatId);
                    var hotMetal = _db.GetHotMetal(heat.HeatId);
                    var excel = new ExcelExport(TextBoxTemplate.Text);
                    try
                    {    
                        Invoke((Action)(() => toolStripStatusLabel.Text = string.Format(" Формируем протокол плавки: {0}. ", heatNumber)));
                        if (excel.DoCommon(protocol, hotMetal, additons, scrapLoadNext, StepTime))
                        {
                            excel.Save(fileName);
                        }
                    }
                    finally
                    {
                        excel.ExcelFileClose();
                    }
                }
                else
                {
                    Invoke((Action)(() => { toolStripProgressBar.Value = toolStripProgressBar.Value + _eventTypes.Count(); }));
                }
                Download++;
                Invoke((Action)(() => 
                { 
                    toolStripStatusLabel.Text = string.Format(" Протокол плавки {0} сформирован. ", heatNumber);
                    var it = CheckedListBoxHeatNumber.FindString(heatNumber.ToString());
                    CheckedListBoxHeatNumber.SetItemCheckState(it, CheckState.Unchecked);
                    label7.Text = string.Format(" Выгружено: {0}", Download);
                }));
            }
            Invoke((Action) (() =>
            {
                toolStripStatusLabel.Text = string.Format(" Готово");
                toolStripProgressBar.Value = toolStripProgressBar.Maximum;
                ButtonDownLoad.Enabled = true;
                ButtonStopProcess.Enabled = false;
                TextBoxTemplate.Enabled = true;
                ButtonGetTemplate.Enabled = true;
                IsStopeed = false;
            }) );

        }

        private void ButtonStopProcessClick(object sender, EventArgs e)
        {
            var box = MessageBox.Show("Остановка будет выполнена после окончания выгрузки текущей плавки. Остановить?",
                                      "Остановка процесса выгрузки", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (box != DialogResult.Yes) return;
            IsStopeed = true;
            ButtonStopProcess.Enabled = false;
        }

        private void GetTemplateClick(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() != DialogResult.OK) return;
                TextBoxTemplate.Text = fileDialog.FileName;
            }
        }

        private void TextBoxTemplateTextChanged(object sender, EventArgs e)
        {
            var text = TextBoxTemplate.Text;
            ButtonDownLoad.Enabled = (text != "") && (text.Substring(text.Length - 4, 4) == Ext);
        }

    }
}
