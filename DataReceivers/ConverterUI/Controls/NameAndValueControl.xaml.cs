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
    /// Interaction logic for NameAndValueControl.xaml
    /// </summary>
    public partial class NameAndValueControl : UserControl, INotifyPropertyChanged
    {
        public NameAndValueControl()
        {
            InitializeComponent();
        }

        private string m_Name;
        private string m_Value;
        public event PropertyChangedEventHandler PropertyChanged;

        public string NameString 
        {
            get { return m_Name; }
            set { m_Name = value; OnPropertyChanged("NameString"); }
        }

        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; OnPropertyChanged("Value"); }
        }

        public static readonly DependencyProperty NameStringProperty = DependencyProperty.Register("NameString", typeof(string), typeof(NameAndValueControl));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(NameAndValueControl));

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
