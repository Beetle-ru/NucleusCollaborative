using System;
using System.Windows.Forms;
using ConnectionProvider;
using Converter;
using CommonTypes;
using System.Drawing;
using Implements;

namespace CarbonVisualizer
{
    class Listener : IEventListener
    {
        public double Carbon;
        public int LancePos;
        public double OxygenPercent;
        public static double CarbonMonoxideVolumePercent;
        public static RollingAverage CarbonMonoxideVolumePercentSmooth;
        public static double OxigenVoluemeRate;
        public Listener()
        {
            CarbonMonoxideVolumePercentSmooth = new RollingAverage(150);
        }
        public void OnEvent(BaseEvent newEvent)
        {
            if (Program.MainWindow.Curves != null)
            {
                if (newEvent is CalculatedCarboneEvent)
                {

                    var carbone = newEvent as CalculatedCarboneEvent;
                    //Carbon = carbone.CarbonePercent;
                }

                if (newEvent is LanceEvent)
                {
                    var lance = newEvent as LanceEvent;
                    LancePos = lance.LanceHeight;
                }

                if (newEvent is SublanceStartEvent)
                {
                    var sublancelance = newEvent as SublanceStartEvent;
                    if (sublancelance.SublanceStartFlag == 1)
                    {
                        Program.MainWindow.Invoke(new MethodInvoker(delegate() {
                            Program.MainWindow.Curves[2].AddPoint((float)OxygenPercent, 0);
                            Program.MainWindow.Curves[2].AddPoint((float)OxygenPercent, 100);
                            Program.MainWindow.Curves[2].AddPoint((float)OxygenPercent, 0);
                        }));

                    }
                }
                if (newEvent is FixDataMfactorModelEvent)
                {
                    Program.MainWindow.Invoke(new MethodInvoker(delegate() {
                        Program.MainWindow.Curves[3].AddPoint((float)OxygenPercent, 0);
                        Program.MainWindow.Curves[3].AddPoint((float)OxygenPercent, 100);
                        Program.MainWindow.Curves[3].AddPoint((float)OxygenPercent, 0);
                    }));
                }

                if (newEvent is OffGasAnalysisEvent)
                {
                    var offGas = newEvent as OffGasAnalysisEvent;
                    CarbonMonoxideVolumePercent = offGas.CO;
                    //CarbonMonoxideVolumePercentSmooth.Add(CarbonMonoxideVolumePercent);
                }

                if (newEvent is OffGasEvent)
                {

                }
                
                if (newEvent is BlowingEvent)
                {
                    var oxygenE = newEvent as BlowingEvent;
                    //oxygenPercent = (oxy * 0.00004); // /25000
                    if (oxygenE.O2TotalVol <= 0 )
                    {
                        Program.MainWindow.Invoke(new MethodInvoker(delegate() {
                            Program.MainWindow.Init();
                        }));
                    }
                    OxygenPercent = (oxygenE.O2TotalVol * 0.004); // /25000 * 100
                    Program.MainWindow.Invoke(new MethodInvoker(delegate() {
                        Program.MainWindow.Curves[0].AddPoint((float)OxygenPercent, (float)(Carbon*25));
                        Program.MainWindow.Curves[1].AddPoint((float)OxygenPercent, (float)(LancePos * 0.1)); // / 2000 * 100
                        Program.MainWindow.Curves[4].AddPoint((float)OxygenPercent, (float)(CarbonMonoxideVolumePercent));
                        //Program.MainWindow.Curves[4].AddPoint((float)OxygenPercent, (float)(CarbonMonoxideVolumePercentSmooth.Average(5)));
                        Program.MainWindow.CarbonCurrent = Carbon;
                        Program.MainWindow.LancePos = LancePos;
                        Program.MainWindow.CarbonMonoxideVolumePercent = CarbonMonoxideVolumePercent;
                        Program.MainWindow.Redraw();
                    }));
                }

                if (newEvent is FlexEvent)
                {
                    var fxe = newEvent as FlexEvent;
                    if (fxe.Operation.StartsWith("OffGasDecarbonater.Result"))
                    {
                        var key = "C";
                        InstantLogger.msg(fxe.ToString());
                        try
                        {
                            Carbon = (double)fxe.Arguments[key];
                        }
                        catch (Exception e)
                        {
                            InstantLogger.err("OffGasDecarbonater.Result - {1} : \n{0}", e.ToString(), key);
                        }
                    }
                }
            }
        }
    }
}
