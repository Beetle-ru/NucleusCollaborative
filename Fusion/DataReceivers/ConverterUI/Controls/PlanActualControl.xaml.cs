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

namespace ConverterUI.Controls
{
    /// <summary>
    /// Interaction logic for PlanActualControl.xaml
    /// </summary>
    public partial class PlanActualControl : UserControl, INotifyPropertyChanged
    {
        public PlanActualControl()
        {
            InitializeComponent();
        }

        private string m_Name;
        private string m_ActualValue;
        private string m_PlannedValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public string NameString
        {
            get { return m_Name; }
            set { m_Name = value; OnPropertyChanged("NameString"); }
        }

        public string ValueActual
        {
            get { return m_ActualValue; }
            set { m_ActualValue = value; OnPropertyChanged("ValueActual"); }
        }

        public string ValuePlanned
        {
            get { return m_PlannedValue; }
            set { m_PlannedValue = value; OnPropertyChanged("ValuePlanned"); }
        }

        public static readonly DependencyProperty NameStringProperty = DependencyProperty.Register("NameString", typeof(string), typeof(PlanActualControl));
        public static readonly DependencyProperty ValuePlannedProperty = DependencyProperty.Register("ValuePlanned", typeof(string), typeof(PlanActualControl));
        public static readonly DependencyProperty ValueActualProperty = DependencyProperty.Register("ValueActual", typeof(string), typeof(PlanActualControl));

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
