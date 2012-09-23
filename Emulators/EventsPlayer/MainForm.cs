using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Converter;
using Core;
using System.Threading;
using CommonTypes;

namespace EventsPlayer
{
    public partial class MainForm : Form
    {
        List<BaseEvent> allEvents = new List<BaseEvent>();
        ConnectionProvider.Client mainGate;
        public int speed = 1;
        Thread eventsThread;
        DateTime? PauseKey = null;
        String currentHeatNumber;
        bool PauseThread = false;

        public enum StatusEnum
        {
            Stopped = 0,
            Played = 1,
            Recorded = 2,
            Paused = 3
        }

        public StatusEnum Status = StatusEnum.Stopped;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(bool autoStart)
        {
            InitializeComponent();
            if (autoStart)
            {
                Record();
            }
            var o = new HeatChangeEvent();

        }

        void UpdateControls(String statusText)
        {
            switch (Status)
            {
                case StatusEnum.Played:
                    btRecord.Image = EventsPlayer.Properties.Resources.record_mini;
                    btPlay.Image = EventsPlayer.Properties.Resources.pause_mini;
                    btRefresh.Image = EventsPlayer.Properties.Resources.stop_mini;
                    btRecord.Enabled = false;

                    break;
                case StatusEnum.Recorded:
                    //btRecord.Image = EventsPlayer.Properties.Resources.stop_mini;
                    btPlay.Image = EventsPlayer.Properties.Resources.play_mini;
                    btRefresh.Image = EventsPlayer.Properties.Resources.stop_mini;
                    btPlay.Enabled = false;
                    btRecord.Enabled = false;
                    break;
                case StatusEnum.Stopped:
                    btPlay.Image = EventsPlayer.Properties.Resources.play_mini;
                    btRecord.Image = EventsPlayer.Properties.Resources.record_mini;
                    btRefresh.Image = EventsPlayer.Properties.Resources.refresh_mini1;
                    btPlay.Enabled = true;
                    btRecord.Enabled = true;
                    break;
                case StatusEnum.Paused:
                    btRecord.Image = EventsPlayer.Properties.Resources.record_mini;
                    btPlay.Image = EventsPlayer.Properties.Resources.play_mini;
                    btRefresh.Image = EventsPlayer.Properties.Resources.stop_mini;
                    btRecord.Enabled = false;
                    break;
            }
            StatusText.Text = statusText;
        }

        private void Record()
        {
            switch (Status)
            {
                case StatusEnum.Recorded:
                    if (mainGate != null)
                    {
                        mainGate.Unsubscribe();
                        Status = StatusEnum.Stopped;
                        UpdateControls("Запись приостановлена");
                    }
                    break;
                default:
                    try
                    {
                        Status = StatusEnum.Recorded;
                        var o = new HeatChangeEvent();
                        mainGate = new ConnectionProvider.Client(new EventsListener(this));
                        //mainGate = new ConnectionProvider.Client();
                        mainGate.Subscribe();
                        UpdateControls("Идёт запись");
                    }
                    catch
                    {
                        Status = StatusEnum.Stopped;
                        UpdateControls("Не могу подключиться для записи.");
                        return;
                    }
                    break;
            }

        }

        private void btRecord_Click(object sender, EventArgs e)
        {
            Record();
        }

        private void btPlay_Click(object sender, EventArgs e)
        {
            switch (Status)
            {
                case StatusEnum.Stopped:
                    if (lbHeatsList.SelectedItem != null)
                    {
                        PauseKey = null;
                        Status = StatusEnum.Played;
                        BinaryFormatter bf;
                        FileInfo file;
                        FileStream fs;
                        bf = new BinaryFormatter();
                        file = new FileInfo("dat\\" + lbHeatsList.SelectedItem.ToString());
                        fs = file.OpenRead();
                        allEvents = (List<BaseEvent>)bf.Deserialize(fs);
                        fs.Close();
                        if (allEvents.Count != 0)
                        {
                            currentHeatNumber = lbHeatsList.SelectedItem.ToString();
                            UpdateControls(String.Format("Файл {0}", currentHeatNumber));
                            eventsThread = new Thread(Play);
                            eventsThread.Start();
                        }
                    }
                    else
                    {
                        UpdateControls("Выберите файл для проигрывания");
                    }
                    break;
                case StatusEnum.Played:
                    if (allEvents != null && allEvents.Count > 0)
                    {
                        Status = StatusEnum.Paused;
                        PauseThread = true;
                        UpdateControls("Пауза");
                    }
                    break;
                case StatusEnum.Paused:
                    if (allEvents != null && allEvents.Count > 0)
                    {
                        Status = StatusEnum.Played;
                        PauseThread = false;
                        UpdateControls(String.Format("Файл {0}", currentHeatNumber));
                    }
                    break;
            }
        }

        public void Play()
        {
            try
            {
                EventsListener el = new EventsListener(this);
                mainGate = new ConnectionProvider.Client(el);
                DateTime? lastTime = null;
                this.Invoke((Action)delegate { pbProgress.Step = 1; });
                this.Invoke((Action)delegate { tbMessages.Clear(); });

                int ProgressBarUpdatePeriod = (int)(allEvents.Last().Time - allEvents.First().Time).TotalSeconds / 100;
                DateTime LastProgressbarUpdate = allEvents.First().Time;

                foreach (BaseEvent _event in allEvents)
                {
                    while (PauseThread)
                    {
                        System.Threading.Thread.Sleep(1);
                    }

                    if (lastTime != null)
                    {
                        try
                        {
                            Thread.Sleep((int)((_event.Time - lastTime.Value).TotalMilliseconds) / speed);
                        }
                        catch (Exception e)
                        {
                            Thread.Sleep(0);
                        }
                    }
                    
                    //this.Invoke((Action)delegate { tbMessages.AppendText(string.Format("{0}\n\n", allEvents[time].ToString())); });
                    lastTime = _event.Time;

                    mainGate.PushEvent(_event);
                    if ((_event.Time - LastProgressbarUpdate).TotalSeconds > ProgressBarUpdatePeriod)
                    {
                        this.Invoke((Action)delegate { pbProgress.PerformStep(); });
                        LastProgressbarUpdate = _event.Time;
                    }
                    //this.Invoke((InvokeDelegate)((String s) => { tbMessages.AppendText(s + "\n"); }), allEvents[time].ToString());
                }
                this.Invoke((Action)delegate { pbProgress.Value = 0; });
                Status = StatusEnum.Stopped;
                this.Invoke((Action)delegate { UpdateControls(""); });

            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            switch (Status)
            {
                case StatusEnum.Played:
                case StatusEnum.Recorded:
                case StatusEnum.Paused:
                    Status = StatusEnum.Stopped;
                    eventsThread.Abort();
                    allEvents.Clear();
                    currentHeatNumber = "";
                    PauseKey = null;
                    tbMessages.Clear();
                    pbProgress.Value = 0;
                    UpdateControls("");
                    break;
                case StatusEnum.Stopped:
                    DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\dat");
                    FileInfo[] files = di.GetFiles("*.dat");
                    lbHeatsList.Items.Clear();
                    foreach (FileInfo fi in files)
                    {
                        lbHeatsList.Items.Add(fi.Name);
                    }
                    lbHeatsList.Update();
                    UpdateControls("Обновлены файлы из директории");
                    break;
            }
        }

        private void btSpeedX1_Click(object sender, EventArgs e)
        {
            ChangeSpeed(1);

        }

        private void btSpeedX2_Click(object sender, EventArgs e)
        {
            ChangeSpeed(2);
        }

        private void btSpeedx5_Click(object sender, EventArgs e)
        {
            ChangeSpeed(5);
        }

        private void btSpeedX10_Click(object sender, EventArgs e)
        {
            ChangeSpeed(10);
        }

        public void ChangeSpeed(int newSpeed)
        {
            speed = newSpeed;
            /*SpeedChangeEvent scEvent = new SpeedChangeEvent();
            scEvent.Speed = speed;
            mainGate.PushEvent(scEvent);*/
        }
    }
}
