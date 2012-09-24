using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Core;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Converter;
using CommonTypes;
using System.Threading;
using NordSteel.Data;
namespace EventsStoreManager
{
    public partial class EventsStoreManagerForm : Form
    {
        public EventsStoreManagerForm()
        {
            InitializeComponent();
            GroupBoxDownLoad.Enabled = false;
            GroupBoxLoad.Enabled = false;
            DateTimePickerStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            DateTimePickerEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        }

        private readonly List<HeatChangeEvent> _heatList = new List<HeatChangeEvent>();
        private readonly List<HeatChangeEvent> _listSelectedHeats = new List<HeatChangeEvent>();
        private readonly DBLayer _db = new DBLayer();
        private readonly List<BaseEvent> _listFromDB = new List<BaseEvent>();
        private Type[] _eventTypes;
        private Dictionary<int, List<BaseEvent>> _dictionaryFromFile;

        private void ButtonGetModelClick(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() != DialogResult.OK) return;
                GroupBoxDownLoad.Enabled = true;
                GroupBoxLoad.Enabled = true;
                try
                {
                    Assembly.ReflectionOnlyLoadFrom(fileDialog.FileName);
                    _eventTypes = BaseEvent.GetEvents();
                    TextBoxModel.Text = fileDialog.SafeFileName;
                    GroupBoxModel.Enabled = false;
                }
                catch (Exception)
                {
                  
                }
            }
        }

        private void ButtonDownLoadClick(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath =  TextBoxDownLoadPath.Text;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    TextBoxDownLoadPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
            GroupBoxLoad.Enabled = false;
            _listSelectedHeats.Clear();
            foreach (var item in CheckedListBoxHeatNumber.CheckedItems)
            {
                _listSelectedHeats.Add(_heatList.Find(p => p.HeatNumber == Convert.ToInt32(item)));
            }
            ButtonDownLoad.Enabled = false;
            var worker = RadioButtonEventsForHeatInfo.Checked ? new Thread(GetHeatDataFromDB) : new Thread(GetDataFromDB);
            worker.Start();
        }

        private void CheckBoxAllHeatCheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < CheckedListBoxHeatNumber.Items.Count; i++)
            {
                CheckedListBoxHeatNumber.SetItemChecked(i, CheckBoxAllHeat.Checked);
            }

        }

        private IEnumerable<HeatChangeEvent> GetHeatList(DateTime startDate, DateTime endDate, int unitNumber)
        {
            var heatList = _db.GetHeatList(startDate, endDate, unitNumber);
            var heats = heatList.GroupBy(heat => heat.HeatNumber).Select(heatChange => new HeatChangeEvent {HeatNumber = heatChange.Key, Time = heatChange.Min(changeTime => changeTime.Time)});
            return heats.ToList();
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

            if (CheckBoxCN3.Checked)
            {
                _heatList.AddRange(GetHeatList(DateTimePickerStart.Value, DateTimePickerEnd.Value, 3));
            }
            CheckedListBoxHeatNumber.Items.Clear();
            foreach (var item in _heatList)
            {
                CheckedListBoxHeatNumber.Items.Add(item.HeatNumber);
            }
            _listFromDB.Clear();
        }

        #region DownLoad Events

        private void GetDataFromDB()
        {

            for (var i = 0; i < _listSelectedHeats.Count; i++)
            {
                var heatNumber = _listSelectedHeats[i].HeatNumber;
                var start = _listSelectedHeats[i].Time;
                var end = i < _listSelectedHeats.Count - 1 ? _listSelectedHeats[i + 1].Time : start.AddHours(3);
                _listFromDB.Clear();
                var n = 0;
                foreach (var type in _eventTypes)
                {
                    var data = type.GetCustomAttributes(false).Where(p => p.GetType().Name == "DBGroup").ToArray();
                    n++;
                    foreach (DBGroup group in data)
                    {
                        var unitNumber = int.Parse(_listSelectedHeats[i].HeatNumber.ToString()[0].ToString());
                        if (@group.UnitNumber != unitNumber) continue;
                        try
                        {
                            _listFromDB.AddRange(Trends.GetEventsByType(unitNumber, type, start, end));
                        }
                        catch { };
                        Invoke((Action)delegate
                        {
                            toolStripProgressBar1.Value = (int)(((double)n / (double)_eventTypes.Length) * 100);
                            toolStripStatusLabel1.Text = string.Format(" Подгружаем данные по событию: {0}. Всего загружено:{1}", type.Name, _listFromDB.Count);
                        });
                    }
                }
                Invoke((Action)delegate
                {
                    SaveToFile(heatNumber.ToString());
                    toolStripStatusLabel1.Text = string.Format(" Файл {0}.dat создан.", heatNumber);
                    toolStripProgressBar1.Value = 0;
                });
            }
            Invoke((Action)delegate
            {
                toolStripStatusLabel1.Text = string.Format(" Готово");
                toolStripProgressBar1.Value = 0;
            });
        }

        private void SaveToFile(string heatNumber)
        {
            FileInfo file;
            var binaryFormatter = new BinaryFormatter();
            if (string.IsNullOrEmpty(TextBoxDownLoadPath.Text))
            {
                if (!Directory.Exists(Application.StartupPath + "\\dat"))
                    Directory.CreateDirectory(Application.StartupPath + "\\dat");
                file = new FileInfo("dat\\" + heatNumber + ".dat");
            }
            else
            {
                if (!Directory.Exists(TextBoxDownLoadPath.Text))
                    Directory.CreateDirectory(TextBoxDownLoadPath.Text);
                file = new FileInfo(TextBoxDownLoadPath.Text + "\\" + heatNumber + ".dat");
            }
            var fileStream = file.Create();
            binaryFormatter.Serialize(fileStream, _listFromDB);
            fileStream.Close();
        }

        #endregion

        #region DownLoad HeatInfo

        private void SaveToFile(Heat heat)
        {
            FileInfo file;
            var binaryFormatter = new BinaryFormatter();
            if (string.IsNullOrEmpty(TextBoxDownLoadPath.Text))
            {
                if (!Directory.Exists(Application.StartupPath + "\\dat"))
                    Directory.CreateDirectory(Application.StartupPath + "\\dat");
                file = new FileInfo("dat\\" + heat.Number.ToString() + ".dat");
            }
            else
            {
                if (!Directory.Exists(TextBoxDownLoadPath.Text))
                    Directory.CreateDirectory(TextBoxDownLoadPath.Text);
                file = new FileInfo(TextBoxDownLoadPath.Text + "\\" + heat.Number.ToString() + ".dat");
            }
            var fileStream = file.Create();
            binaryFormatter.Serialize(fileStream, heat);
            fileStream.Close();
        }

        private void GetHeatDataFromDB()
        {
            Heat heat;
            for (var i = 0; i < _listSelectedHeats.Count; i++)
            {
                var lances = new List<LanceEvent>();
                var offgasanalysys = new List<OffGasAnalysisEvent>();
                var offgases = new List<OffGasEvent>();
                var outburst = new List<SlagOutburstEvent>();
                var boilerWaterCooling = new List<BoilerWaterCoolingEvent>();
                var ignition = new List<IgnitionEvent>();
                var heatNumber = _listSelectedHeats[i].HeatNumber;
                var start = _listSelectedHeats[i].Time;
                var end = i < _listSelectedHeats.Count - 1 ? _listSelectedHeats[i + 1].Time : start.AddHours(3);
                heat = new Heat();
                var unitNumber = int.Parse(_listSelectedHeats[i].HeatNumber.ToString()[0].ToString());
                if ((unitNumber == 1 || unitNumber == 2 || unitNumber == 3) && !File.Exists(TextBoxDownLoadPath.Text + "\\" + heatNumber.ToString() + ".dat"))
                {
                    try
                    {
                        Invoke((Action) delegate
                                            {
                                                toolStripProgressBar1.Value = 30;
                                                toolStripStatusLabel1.Text =
                                                    string.Format(
                                                        " Подгружаем данные по событию: SlagOutburstEvent. Всего загружено:{0}",
                                                        heat.SlagOutburstHistory.Count);
                                            });
                        outburst.AddRange((Trends.GetEvents<SlagOutburstEvent>(unitNumber, start, end)));
                    }
                    catch
                    {
                    }
                    try
                    {
                        Invoke((Action) delegate
                                            {
                                                toolStripProgressBar1.Value = 30;
                                                toolStripStatusLabel1.Text =
                                                    string.Format(
                                                        " Подгружаем данные по событию: IgnitionEvent. Всего загружено:{0}",
                                                        heat.IgnitionHistory.Count);
                                            });
                        ignition.AddRange((Trends.GetEvents<IgnitionEvent>(unitNumber, start, end)));
                    }
                    catch
                    {
                    }
                    try
                    {
                        Invoke((Action) delegate
                                            {
                                                toolStripProgressBar1.Value = 30;
                                                toolStripStatusLabel1.Text =
                                                    string.Format(
                                                        " Подгружаем данные по событию: BoilerWaterCoolingEvent. Всего загружено:{0}",
                                                        heat.BoilerWaterCoolingHistory.Count);
                                            });
                        boilerWaterCooling.AddRange((Trends.GetEvents<BoilerWaterCoolingEvent>(unitNumber, start, end)));
                    }
                    catch
                    {
                    }
                    try
                    {
                        Invoke((Action) delegate
                                            {
                                                toolStripProgressBar1.Value = 30;
                                                toolStripStatusLabel1.Text =
                                                    string.Format(
                                                        " Подгружаем данные по событию: LanceEvent. Всего загружено:{0}",
                                                        heat.LanceHistory.Count);
                                            });
                        lances.AddRange((Trends.GetEvents<LanceEvent>(unitNumber, start, end)));
                    }
                    catch
                    {
                    }
                    try
                    {
                        Invoke((Action) delegate
                                            {
                                                toolStripProgressBar1.Value = 60;
                                                toolStripStatusLabel1.Text =
                                                    string.Format(
                                                        " Подгружаем данные по событию: OffGasAnalysisEvent. Всего загружено:{0}",
                                                        heat.LanceHistory.Count);
                                            });
                        offgasanalysys.AddRange(Trends.GetEvents<OffGasAnalysisEvent>(unitNumber, start, end));
                    }
                    catch
                    {
                    }
                    try
                    {
                        Invoke((Action) delegate
                                            {
                                                toolStripProgressBar1.Value = 90;
                                                toolStripStatusLabel1.Text =
                                                    string.Format(
                                                        " Подгружаем данные по событию: OffGasEvent. Всего загружено:{0}",
                                                        heat.LanceHistory.Count + heat.OffGasAnalysisHistory.Count);
                                            });
                        offgases.AddRange(Trends.GetEvents<OffGasEvent>(unitNumber, start, end));
                        Invoke((Action) delegate
                                            {
                                                toolStripProgressBar1.Value = 100;
                                                toolStripStatusLabel1.Text = string.Format("Всего загружено:{0}",
                                                                                           heat.LanceHistory.Count +
                                                                                           heat.OffGasAnalysisHistory.
                                                                                               Count +
                                                                                           heat.OffGasHistory.Count);
                                            });
                    }
                    catch
                    {
                    }
                    Invoke((Action) delegate
                                        {
                                            _db.Reconnect("SMK", "smk");
                                            heat = _db.GetHeatInfo(heatNumber.ToString().Insert(2, "0"));
                                            if (heat.ID != 0)
                                            {
                                                toolStripStatusLabel1.Text = "Получаем данные по чугуну...";
                                                heat.HotMetalAttributes = _db.GetHotMetalAttributes(heat.ID);
                                                toolStripStatusLabel1.Text = "Получаем данные по добавочным...";
                                                toolStripProgressBar1.Value = 15;
                                                heat.Additions = _db.GetAdditions(heat.ID);
                                                toolStripStatusLabel1.Text = "Получаем данные по скрапу...";
                                                toolStripProgressBar1.Value = 25;
                                                heat.ScrapBuckets = _db.GetScrapBuckets(heat.ID);
                                                toolStripStatusLabel1.Text =
                                                    "Получаем данные по измерительному зонду...";
                                                toolStripProgressBar1.Value = 35;
                                                heat.Sublances = _db.GetSublance(heat.ID);
                                                _db.Reconnect("XIM", "xim");
                                                toolStripStatusLabel1.Text = "Получаем данные по анализу шлака...";
                                                toolStripProgressBar1.Value = 50;
                                                heat.SlagAnalysys = _db.GetSlagAnalysys(heat.Number);
                                                toolStripStatusLabel1.Text = "Получаем данные по анализу стали...";
                                                toolStripProgressBar1.Value = 70;
                                                heat.SteelAnalysys = _db.GetSteelAnalysys(heat.Number);
                                                toolStripStatusLabel1.Text = "Получаем данные по анализу чугуна...";
                                                toolStripProgressBar1.Value = 90;
                                                heat.HotMetalAnalysyses = _db.GetHotMetalAnalysys(heat.Number);
                                                toolStripProgressBar1.Value = 100;
                                                toolStripStatusLabel1.Text = "Сохранение в файл...";
                                                heat.LanceHistory = lances;
                                                heat.OffGasHistory = offgases;
                                                heat.OffGasAnalysisHistory = offgasanalysys;
                                                heat.IgnitionHistory = ignition;
                                                heat.BoilerWaterCoolingHistory = boilerWaterCooling;
                                                heat.SlagOutburstHistory = outburst;
                                                heat.Number = heatNumber;
                                                SaveToFile(heat);
                                            }
                                            _db.Reconnect("EVENTS", "events");
                                        });
                }
                Invoke((Action)(() =>
                {
                    var it = CheckedListBoxHeatNumber.FindString(heatNumber.ToString());
                    CheckedListBoxHeatNumber.SetItemCheckState(it, CheckState.Unchecked);
                }));
            }
            Invoke((Action)delegate
            {
                toolStripProgressBar1.Value = 0;
                toolStripStatusLabel1.Text = "Готово";
                GroupBoxLoad.Enabled = true;
                ButtonDownLoad.Enabled = true;
            });
        }

        #endregion

        #region Load

        private Dictionary<int, List<BaseEvent>> GetEventsFromFile(string fileName)
        {
            var binaryFormatter = new BinaryFormatter();
            var file = new FileInfo(fileName);
            var fileStream = file.OpenRead();
            listBox2.Items.Clear();
            listBox2.Items.Add(file.Name.Replace(".dat", ""));
            var deserializedObject = new object();
            try
            {
                deserializedObject = binaryFormatter.Deserialize(fileStream);
            }
            catch (Exception)
            {
            }
            finally
            {
                fileStream.Close();
                fileStream.Dispose();
            }
            var events = new List<BaseEvent>();
            if (deserializedObject is Dictionary<DateTime, BaseEvent>)
            {
                var dictionaryEvents = deserializedObject as Dictionary<DateTime, BaseEvent>;
                events = dictionaryEvents.Select(p => p.Value).ToList();
            }
            if ((deserializedObject is List<BaseEvent>))
            {
                events = deserializedObject as List<BaseEvent>;
            }
            var ret = new Dictionary<int, List<BaseEvent>> {{int.Parse(file.Name.Replace(".dat", "")), events}};
            return ret;
        }

        private void Button4Click(object sender, EventArgs e)
        {
            // Выбрать файл для записи в БД
            using (var fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    _dictionaryFromFile = GetEventsFromFile(fileDialog.FileName);
                    textBox3.Text = fileDialog.FileName;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count == 0) return;
            GroupBoxDownLoad.Enabled = false;
            button5.Enabled = false;
            List<string> items = new List<string>();
            foreach (var item in listBox2.SelectedItems)
            {

                items.Add((string)item);
            }

            new Thread(new ThreadStart((Action)delegate()
            {
                foreach (var item in items)
                {
                    WriteToDB(item);
                }
            }
                )).Start();
        }

        private void WriteToDB(string heatNumber)
        {
            var unitNumber = int.Parse(heatNumber[0].ToString());
            double n = 0;
            foreach (var item in _dictionaryFromFile[int.Parse(heatNumber)])
            {
                _db.Insert(item, unitNumber);
                n++;
                Invoke((Action)delegate { toolStripProgressBar1.Value = (int)(((double)n / (double)_dictionaryFromFile[int.Parse(heatNumber)].Count) * 100); toolStripStatusLabel1.Text = string.Format(" Пишем {0} {1}/{2}", item.GetType().Name, n, _dictionaryFromFile[int.Parse(heatNumber)].Count); });

            }
            Invoke((Action)delegate { toolStripProgressBar1.Value = 0; toolStripStatusLabel1.Text = "Запись плавки " + heatNumber + " завершена "; button5.Enabled = true; GroupBoxDownLoad.Enabled = true; });
        }

        #endregion



    }

}
