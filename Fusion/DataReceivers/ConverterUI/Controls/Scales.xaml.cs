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

namespace ConverterUI
{
    public partial class Scales : UserControl
    {
        public Scales()
        {
            InitializeComponent();
        }

        string m_Name = "";
        int m_WeightReal = 0;
        int m_WeightPlan = 0;
        string m_Material1Name = "";
        string m_Material2Name = "";
        string m_Material3Name = "";
        int m_Material1Weight = 0;
        int m_Material2Weight = 0;
        int m_Material3Weight = 0;
        bool m_Material1Visible = false;
        bool m_Material2Visible = false;
        bool m_Material3Visible = false;

        public string ScalesName
        {
            get
            {
                if (string.IsNullOrEmpty(m_Name))
                {
                    m_Name = lName.Content.ToString();
                }
                return m_Name;
            }
            set
            {
                m_Name = value; lName.Content = m_Name;
            }
        }

        public int WeightReal
        {
            get
            {
                return m_WeightReal;
            }
            set
            {
                m_WeightReal = value;
                lWeightReal.Content = m_WeightReal.ToString();
            }
        }

        public int WeightPlan
        {
            get
            {
                return m_WeightPlan;
            }
            set
            {
                m_WeightPlan = value;
                lWeightPlan.Content = m_WeightPlan.ToString();
            }
        }

        public string Material1Name
        {
            get
            {
                return m_Material1Name;
            }
            set
            {
                m_Material1Name = value;
                lMaterial1Name.Content = m_Material1Name;
            }
        }

        public string Material2Name
        {
            get
            {
                return m_Material2Name;
            }
            set
            {
                m_Material2Name = value;
                lMaterial2Name.Content = m_Material2Name;
            }
        }

        public string Material3Name
        {
            get
            {
                return m_Material3Name;
            }
            set
            {
                m_Material3Name = value;
                lMaterial3Name.Content = m_Material3Name;
            }
        }

        public int Material1Weight
        {
            get
            {
                return m_Material1Weight;
            }
            set
            {
                m_Material1Weight = value;
                lMaterial1Weight.Content = m_Material1Weight;
            }
        }

        public int Material2Weight
        {
            get
            {
                return m_Material2Weight;
            }
            set
            {
                m_Material2Weight = value;
                lMaterial2Weight.Content = m_Material2Weight;
            }
        }

        public int Material3Weight
        {
            get
            {
                return m_Material3Weight;
            }
            set
            {
                m_Material3Weight = value;
                lMaterial3Weight.Content = m_Material3Weight;
            }
        }

        public bool Material1Visible
        {
            get
            {
                return m_Material1Visible;
            }
            set
            {
                m_Material1Visible = value;
                if (m_Material1Visible)
                {
                    lMaterial1Name.Visibility = System.Windows.Visibility.Visible;
                    lMaterial1Weight.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    lMaterial1Name.Visibility = System.Windows.Visibility.Hidden;
                    lMaterial1Weight.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        public bool Material2Visible
        {
            get
            {
                return m_Material2Visible;
            }
            set
            {
                m_Material2Visible = value;
                if (m_Material2Visible)
                {
                    lMaterial2Name.Visibility = System.Windows.Visibility.Visible;
                    lMaterial2Weight.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    lMaterial2Name.Visibility = System.Windows.Visibility.Hidden;
                    lMaterial2Weight.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        public bool Material3Visible
        {
            get
            {
                return m_Material3Visible;
            }
            set
            {
                m_Material3Visible = value;
                if (m_Material3Visible)
                {
                    lMaterial3Name.Visibility = System.Windows.Visibility.Visible;
                    lMaterial3Weight.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    lMaterial3Name.Visibility = System.Windows.Visibility.Hidden;
                    lMaterial3Weight.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }


    }
}
