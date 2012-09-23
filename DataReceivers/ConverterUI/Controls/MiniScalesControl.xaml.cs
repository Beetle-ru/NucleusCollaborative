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
    /// Interaction logic for MiniScalesControl.xaml
    /// </summary>
    public partial class MiniScalesControl : UserControl, INotifyPropertyChanged
    {
        public MiniScalesControl()
        {
            InitializeComponent();
        }

        private string m_ScalesName;
        private int m_WeightCurrentReal;
        private int m_WeightCurrentPlanned;

        private string m_Material1Name;
        private int m_Material1Weight;
        private bool m_Material1Visibility;

        private string m_Material2Name;
        private int m_Material2Weight;
        private bool m_Material2Visibility;

        private string m_Material3Name;
        private int m_Material3Weight;
        private bool m_Material3Visibility;


        public event PropertyChangedEventHandler PropertyChanged;

        public string ScalesName
        {
            get { return m_ScalesName; }
            set
            {
                m_ScalesName = value;
                OnPropertyChanged("ScalesName");
            }
        }

        public int WeightCurrentReal
        {
            get { return m_WeightCurrentReal; }
            set
            {
                m_WeightCurrentReal = value;
                OnPropertyChanged("WeightCurrentReal");
            }
        }

        public int WeightCurrentPlanned
        {
            get { return m_WeightCurrentPlanned; }
            set
            {
                m_WeightCurrentPlanned = value;
                OnPropertyChanged("WeightCurrentPlanned");
            }
        }

        public string Material1Name
        {
            get { return m_Material1Name; }
            set
            {
                m_Material1Name = value;
                OnPropertyChanged("Material1Name");
            }
        }

        public int Material1Weight
        {
            get { return m_Material1Weight; }
            set
            {
                m_Material1Weight = value;
                OnPropertyChanged("Material1Weight");
            }
        }

        public bool Material1Visibility
        {
            get
            {
                return m_Material1Visibility; 
            }
            set
            {
                m_Material1Visibility = value;
                if (value)
                {
                    tName1.Visibility = System.Windows.Visibility.Visible;
                    tValue1.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    tName1.Visibility = System.Windows.Visibility.Hidden;
                    tValue1.Visibility = System.Windows.Visibility.Hidden;
                }
                OnPropertyChanged("Material1Visibility");
            }
        }

        public string Material2Name
        {
            get { return m_Material2Name; }
            set
            {
                m_Material2Name = value;
                OnPropertyChanged("Material2Name");
            }
        }

        public int Material2Weight
        {
            get { return m_Material2Weight; }
            set
            {
                m_Material2Weight = value;
                OnPropertyChanged("Material2Weight");
            }
        }

        public bool Material2Visibility
        {
            get { return m_Material2Visibility; }
            set
            {
                m_Material2Visibility = value;
                if (value)
                {
                    tName2.Visibility = System.Windows.Visibility.Visible;
                    tValue2.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    tName2.Visibility = System.Windows.Visibility.Hidden;
                    tValue2.Visibility = System.Windows.Visibility.Hidden;
                }
                OnPropertyChanged("Material2Visibility");
            }
        }

        public string Material3Name
        {
            get { return m_Material3Name; }
            set
            {
                m_Material3Name = value;
                OnPropertyChanged("Material3Name");
            }
        }

        public int Material3Weight
        {
            get { return m_Material3Weight; }
            set
            {
                m_Material3Weight = value;
                OnPropertyChanged("Material3Weight");
            }
        }

        public bool Material3Visibility
        {
            get { return m_Material3Visibility; }
            set
            {
                m_Material3Visibility = value;
                if (value)
                {
                    tName3.Visibility = System.Windows.Visibility.Visible;
                    tValue3.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    tName3.Visibility = System.Windows.Visibility.Hidden;
                    tValue3.Visibility = System.Windows.Visibility.Hidden;
                }
                OnPropertyChanged("Material3Visibility");
            }
        }

        public static readonly DependencyProperty ScalesNameProperty = DependencyProperty.Register("ScalesName", typeof(string), typeof(MiniScalesControl));
        public static readonly DependencyProperty WeightCurrentRealProperty = DependencyProperty.Register("WeightCurrentReal", typeof(int), typeof(MiniScalesControl));
        public static readonly DependencyProperty WeightCurrentPlannedProperty = DependencyProperty.Register("WeightCurrentPlanned", typeof(int), typeof(MiniScalesControl));

        public static readonly DependencyProperty Material1NameProperty = DependencyProperty.Register("Material1Name", typeof(string), typeof(MiniScalesControl));
        public static readonly DependencyProperty Material1WeightProperty = DependencyProperty.Register("Material1Weight", typeof(int), typeof(MiniScalesControl));
        public static readonly DependencyProperty Material1VisibilityProperty = DependencyProperty.Register("Material1Visibility", typeof(bool), typeof(MiniScalesControl));

        public static readonly DependencyProperty Material2NameProperty = DependencyProperty.Register("Material2Name", typeof(string), typeof(MiniScalesControl));
        public static readonly DependencyProperty Material2WeightProperty = DependencyProperty.Register("Material2Weight", typeof(int), typeof(MiniScalesControl));
        public static readonly DependencyProperty Material2VisibilityProperty = DependencyProperty.Register("Material2Visibility", typeof(bool), typeof(MiniScalesControl));

        public static readonly DependencyProperty Material3NameProperty = DependencyProperty.Register("Material3Name", typeof(string), typeof(MiniScalesControl));
        public static readonly DependencyProperty Material3WeightProperty = DependencyProperty.Register("Material3Weight", typeof(int), typeof(MiniScalesControl));
        public static readonly DependencyProperty Material3VisibilityProperty = DependencyProperty.Register("Material3Visibility", typeof(bool), typeof(MiniScalesControl));

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
