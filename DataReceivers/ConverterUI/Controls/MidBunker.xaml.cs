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
    /// <summary>
    /// Interaction logic for Mid.xaml
    /// </summary>
    public partial class MidBunker : UserControl
    {

        public enum BunkerState
        {
            Opened = 0,
            Closed = 1
        }

        BunkerState m_State = BunkerState.Closed;

        string m_Name = "";
        string m_Material1Name = "";
        string m_Material2Name = "";
        string m_Material3Name = "";
        string m_Material4Name = "";
        int m_Material1Weight = 0;
        int m_Material2Weight = 0;
        int m_Material3Weight = 0;
        int m_Material4Weight = 0;

        public MidBunker()
        {
            InitializeComponent();
        }

        public string BunkerName
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

        public string Material4Name
        {
            get
            {
                return m_Material4Name;
            }
            set
            {
                m_Material4Name = value;
                lMaterial4Name.Content = m_Material4Name;
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

        public int Material4Weight
        {
            get
            {
                return m_Material4Weight;
            }
            set
            {
                m_Material4Weight = value;
                lMaterial4Weight.Content = m_Material4Weight;
            }
        }

        public BunkerState State
        {
            get { return m_State; }
            set
            {
                m_State = value;
                switch (m_State)
                {
                    case BunkerState.Closed:
                        iBunker.Source = new BitmapImage(new Uri(@"/Images/ПромБункер.png", UriKind.Relative));
                        break;
                    case BunkerState.Opened:
                        iBunker.Source = new BitmapImage(new Uri(@"/Images/ПромБункерОткрытый.png", UriKind.Relative));
                        break;
                }
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
         /*   if (this.State == BunkerState.Opened)
            {
                this.State = BunkerState.Closed;
            }
            else
            {
                this.State = BunkerState.Opened;
            }*/
        }
    }
}
