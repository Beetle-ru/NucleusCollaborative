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

namespace ConverterUI.Controls
{
    /// <summary>
    /// Interaction logic for TractMiniControl.xaml
    /// </summary>
    public partial class TractMiniControl : UserControl
    {
        public TractMiniControl()
        {
            InitializeComponent();
            miniBunkerControl12.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl11.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl10.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl92.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl91.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl82.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl81.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl7.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl6.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniBunkerControl5.Status = MiniBunkerControl.StatusEnum.NotActive;
            miniScalesControl3.Material3Visibility = false;
            miniScalesControl4.Material2Visibility = false;
            miniScalesControl4.Material3Visibility = false;
            miniScalesControl5.Material2Visibility = false;
            miniScalesControl5.Material3Visibility = false;
            miniScalesControl6.Material2Visibility = false;
            miniScalesControl6.Material3Visibility = false;
        }
    }
}
