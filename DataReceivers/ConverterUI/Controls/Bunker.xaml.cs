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
    /// Interaction logic for Bunker.xaml
    /// </summary>
    public partial class Bunker : UserControl
    {
        public Bunker()
        {
            InitializeComponent();
        }

        public enum BunkerState
        {
            Opened = 0,
            Closed = 1
        }

        BunkerState m_State = BunkerState.Closed;
        //string m_Material = "";
        string m_Name = "";

        public string Material
        {
            get { return (string)this.GetValue(MaterialProperty); }
            set
            {
                this.SetValue(MaterialProperty, value);
                lMaterialName.Content = value;
            }
        }

        public static readonly DependencyProperty MaterialProperty = DependencyProperty.Register("Material", typeof(string), typeof(Bunker));


        public string BunkerName
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                lBunkerName.Content = m_Name;
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
                        iBunker.Source = new BitmapImage(new Uri(@"/Images/бункерЗакр.png", UriKind.Relative));
                        break;
                    case BunkerState.Opened:
                        iBunker.Source = new BitmapImage(new Uri(@"/Images/бункер.png", UriKind.Relative));
                        break;
                }
            }
        }

        private void lMaterialName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /* cbMaterials.Visibility = System.Windows.Visibility.Visible;
             cbMaterials.IsDropDownOpen = true;*/
        }

        private void cbMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*  cbMaterials.Visibility = System.Windows.Visibility.Hidden;
              lMaterialName.Content = ((System.Windows.Controls.ComboBoxItem)cbMaterials.SelectedItem).Content;*/
        }

        private void iBunker_MouseDown(object sender, MouseButtonEventArgs e)
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
