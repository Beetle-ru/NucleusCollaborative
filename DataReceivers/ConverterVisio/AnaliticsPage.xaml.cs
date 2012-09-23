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

namespace ConverterVisio
{
    /// <summary>
    /// Interaction logic for AnaliticsPage.xaml
    /// </summary>
    public partial class AnaliticsPage : Page
    {
        ZedGraph.ZedGraphControl zGraph = new ZedGraph.ZedGraphControl();
        String[] menuText = { "F1-Помощь", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10-Пред экран", "F11", "F12-Главный экран" };
        String[] menuUris = { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "MainPage.xaml" };

        public void InitZedGraph()
        {
            zGraph.IsEnableHZoom = false;
            zGraph.IsEnableVZoom = false;
            zGraph.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zGraph.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zGraph.GraphPane.XAxis.Title.IsVisible = false;
            zGraph.GraphPane.YAxis.Title.IsVisible = false;
            zGraph.GraphPane.Title.IsVisible = false;
            zGraph.GraphPane.XAxis.Scale.FontSpec.Size = 10;
            zGraph.GraphPane.YAxis.Scale.FontSpec.Size = 10;
            zGraph.GraphPane.XAxis.Scale.MajorStep = 300;
            zGraph.GraphPane.XAxis.Scale.MinorStep = 60;
            zGraph.GraphPane.Legend.IsVisible = false;
        }

        public AnaliticsPage()
        {
            InitializeComponent();
            InitZedGraph();
            wfhZedGraph.Child = zGraph;

            for (int i=0; i<12; i++)
            {
                mainMenu.AddItem(i, menuText[i], menuUris[i]);
            }
            mainMenu.parentPage = this;
            //mainMenu.UpdateMenu();
        }
    }
}
