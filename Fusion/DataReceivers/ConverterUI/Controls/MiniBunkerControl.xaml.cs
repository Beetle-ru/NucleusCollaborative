using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Reflection;

namespace ConverterUI.Controls
{
    /// <summary>
    /// Interaction logic for MiniBunkerControl.xaml
    /// </summary>
    public partial class MiniBunkerControl : UserControl, INotifyPropertyChanged
    {
        public MiniBunkerControl()
        {
            InitializeComponent();
        }

        public enum StatusEnum
        {
            NotActive = 0,
            Active = 1,
            Error = 2
        }

        private string m_MaterialName;
        private string m_BunkerName;
        private StatusEnum m_Status;

        public event PropertyChangedEventHandler PropertyChanged;

        public string MaterialName
        {
            get { return m_MaterialName; }
            set
            {
                m_MaterialName = value;
                OnPropertyChanged("MaterialName");
            }
        }

        public string BunkerName
        {
            get { return m_BunkerName; }
            set
            {
                m_BunkerName = value;
                OnPropertyChanged("BunkerName");
            }
        }

        public StatusEnum Status
        {
            get
            {
                switch (m_Status)
                {
                    case (StatusEnum.NotActive):
                        break;
                    case (StatusEnum.Active):
                        break;
                }
                return m_Status;
            }
            set
            {
                m_Status = value;
                OnPropertyChanged("Status");
                string path = "";
                switch (m_Status)
                {
                    case (StatusEnum.NotActive):
                        path = ";component/Images/button25-20110808211801-00001.png";
                        break;
                    case (StatusEnum.Active):
                        path = ";component/Images/button25-20110808211801-00010.png";
                        break;
                }
                iStatus.Source = new BitmapImage(new Uri(@"pack://application:,,,/"
                                                + Assembly.GetExecutingAssembly().GetName().Name
                                                + path, UriKind.Absolute));
            }
        }




        public static readonly DependencyProperty MaterialNameProperty = DependencyProperty.Register("MaterialName", typeof(string), typeof(MiniBunkerControl));
        public static readonly DependencyProperty BunkerNameProperty = DependencyProperty.Register("BunkerName", typeof(string), typeof(MiniBunkerControl));
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(StatusEnum), typeof(MiniBunkerControl));

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
