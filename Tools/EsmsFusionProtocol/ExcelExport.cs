using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Esms;
using Microsoft.Office.Interop.Excel;



namespace EsmsFusionProtocol
{
    class ExcelExport
    {
        public Worksheet ExcelWorkSheet { get; set; }
        public Workbook ExcelWorkBook { get; set; }
        public Application ExcelApp { get; set; }

        public void Save(string fileName)
        {
            ExcelWorkBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, false, Type.Missing, XlSaveAsAccessMode.xlNoChange, 2, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }

        public void ExcelFileClose()
        {
            ExcelWorkBook.Close(0);
            Marshal.ReleaseComObject(ExcelWorkBook);
            ExcelApp.Quit();
            Marshal.ReleaseComObject(ExcelApp);
        }

        public ExcelExport(string template)
        {
            ExcelApp = new Application();
            ExcelWorkBook = !File.Exists(template)
                ? ExcelApp.Workbooks.Add(System.Reflection.Missing.Value) 
                : ExcelApp.Workbooks.Open(template, 1, false, 5, "", "", true, XlPlatform.xlWindows, "", false, false, 0, true, false, XlCorruptLoad.xlNormalLoad);
            ExcelWorkSheet = (Worksheet)ExcelWorkBook.Worksheets.Item[1];
        }

        public bool DoCommon(Heat protocol, List<HotMetal> hotMetal, List<Additions> additions, List<ScrapLoadEvent> scrapLoadNext, int stepTime)
        {
            var start = protocol.HeatPassportHistory.Min(x => x.Time);
            var end = protocol.HeatPassportHistory.Max(x => x.Time);
            var step = Math.Round((end - start).TotalSeconds/stepTime) + 1;
            var fingerOpen = (int)Math.Round((protocol.FingersHistory.Where(x => x.FingersOpen).Min(x => x.Time) - start).TotalSeconds / stepTime);
            var fingerClose = (int)Math.Round((protocol.FingersHistory.Where(x => x.FingersClose).Min(x => x.Time) - start).TotalSeconds / stepTime);
            var hib = (int)Math.Round((protocol.PouringHotMetalHistory.Where(x => x.IsPouring).Min(x => x.Time) - start).TotalSeconds / stepTime);
            var hie =(int)Math.Round((protocol.PouringHotMetalHistory.Where(x => x.IsPouring).Max(x => x.Time) - start).TotalSeconds / stepTime);
            var gateOpen = (int)Math.Round((protocol.SteelOutletHistory.Where(x => x.GateOpen).Min(x => x.Time) - start).TotalSeconds / stepTime);
            var gateClose =(int)Math.Round((protocol.SteelOutletHistory.Where(x => x.GateClose).Min(x => x.Time) - start).TotalSeconds / stepTime);
            var outerb = (int)Math.Round((protocol.SteelOutletHistory.Where(x => x.Output).Min(x => x.Time) - start).TotalSeconds / stepTime);
            var outere = (int)Math.Round((protocol.SteelOutletHistory.Where(x => x.Output).Max(x => x.Time) - start).TotalSeconds / stepTime);
            var tubeOpen = TubOpen(protocol.FingersHistory, start, stepTime);
            var tubeClose = TubClose(protocol.FingersHistory, start, stepTime);

            ExcelApp.Cells[3, 3] = protocol.HeatPassportHistory.Select(x => x.HeatNumber).FirstOrDefault();
            ExcelApp.Cells[3, 5] = start;

            //Данные по прелдылущей плавке фигугруют в протоколе один раз.
            //Т.к. нет пока возможности получать реальные данные, то по умолчанию используем типовые данные
            //mStH (масса болота), кг - данных нет - торетические 
            ExcelApp.Cells[6, 48] = 15000;
            //mSlH (масса шлака болота), кг -данных нет - торетические
            ExcelApp.Cells[6, 49] = 5000;
            //(TH-273,15) (температура болота), ºС - данных нет - торетические
            ExcelApp.Cells[6, 50] = 1620;
            //WCh1SP (теплосодержание лома в шахте), МДж - данных нет - торетические
            ExcelApp.Cells[6, 51] = 17600;
            
            for (var i = 0; i < step; i++)
            {
                var current = start.AddSeconds(stepTime);
                var row = i + 6;
                //i (шаг вычисления)
                ExcelApp.Cells[row, 2] = i;
                //t(i) (время) - секунды
                ExcelApp.Cells[row, 3] = i * stepTime;

                //mSlTip(i) (скорость слива шлака), кг/с - Нет данных
                ExcelApp.Cells[row, 23] = i < outerb && protocol.HeatPassportHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.HeatElectricityFlow).FirstOrDefault() >= 10000 ? 8 : 0;
                //(Tenv(i)-273,15) (температура окр. воздуха) - нет данных, берём среднесуточную для данного дня
                ExcelApp.Cells[row, 24] = 25;
                //(TFout(i)-273,15) (температура на выходе из пальцев), ºС - нет данных 
                ExcelApp.Cells[row, 28] = 29;
                //(TFin(i)-273,15) (температура на входе в пальцы), ºС  - нет данных
                ExcelApp.Cells[row, 29] = 26;
                //mAir(i) (подсос воздуха), кг/с - теоретические данные
                ExcelApp.Cells[row, 38] = 8.8;
                //(TS(i)-273,15) (температура подины), ºС - PLC1.Температура подины.Температура подины1-8. Здесь по-видимому средняя температура в 8 точках 
                ExcelApp.Cells[row, 37] = 200;
                /* это вычисление температуры подины включить после ремонта датчиков
                Math.Round((protocol.TempHearthHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempHearth1).FirstOrDefault()  +
                protocol.TempHearthHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempHearth2).FirstOrDefault()  +
                protocol.TempHearthHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempHearth3).FirstOrDefault() +
                protocol.TempHearthHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempHearth4).FirstOrDefault()  +
                protocol.TempHearthHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempHearth5).FirstOrDefault()  +
                protocol.TempHearthHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempHearth6).FirstOrDefault()  +
                protocol.TempHearthHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempHearth7).FirstOrDefault() +
                protocol.TempHearthHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempHearth8).FirstOrDefault()) / 8, 1);
                 */

                //We(i) (активная мощность трансформатора), МВт - PLC1.ArCos.Активная энергия, МВт
                ExcelApp.Cells[row, 4] = Math.Round(protocol.ArCOSHistory.Where(x => x.Time >= start && x.Time < current).Select(x => x.ActiveEnergy).FirstOrDefault(), 1);
                //UCH4B(i) (интенсиность вдувания CH4) - PLC2.Горелка 1.Текущий расход природного газа, м3/ч+Горелка 2.Текущий расход природного газа, м3/ч+Горелка 3.Текущий расход природного газа, м3/ч+Горелка 4.Текущий расход природного газа, м3/ч
                ExcelApp.Cells[row, 5] = Math.Round(protocol.Burner1History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentNaturalGasFlow).FirstOrDefault() +
                     protocol.Burner2History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentNaturalGasFlow).FirstOrDefault() +
                     protocol.Burner3History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentNaturalGasFlow).FirstOrDefault() +
                     protocol.Burner4History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentNaturalGasFlow).FirstOrDefault(), 0);
                //UO2B(i) (интенсивность вдувания O2) - PLC2.Горелка 1.Текущий расход кислорода, м3/ч+Горелка 2.Текущий расход кислорода, м3/ч+Горелка 3.Текущий расход кислорода, м3/ч+Горелка 4.Текущий расход кислорода, м3/ч
                ExcelApp.Cells[row, 6] = Math.Round(protocol.Burner1History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlow).FirstOrDefault() +
                     protocol.Burner2History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlow).FirstOrDefault() +
                     protocol.Burner3History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlow).FirstOrDefault() +
                     protocol.Burner4History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlow).FirstOrDefault(), 0);
                //UCH4RB(i) (интенсиность вдувания CH4 в режиме горелки) - PLC2.Инжектор 1.Текущий расход газа, м3/ч+Инжектор 2.Текущий расход газа, м3/ч+Инжектор 3.Текущий расход газа, м3/ч+Инжектор 4.Текущий расход газа, м3/ч
                ExcelApp.Cells[row, 7] = Math.Round(protocol.Injector1History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentGasFlow).FirstOrDefault() +
                     protocol.Injector2History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentGasFlow).FirstOrDefault() +
                     protocol.Injector3History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentGasFlow).FirstOrDefault() +
                     protocol.Injector4History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentGasFlow).FirstOrDefault(), 0);
                //UO2RB(i) (интенсивность вдувания O2 в режиме горелки) - PLC2.Инжектор 1.Текущий расход кислорода (режим горелки), м3/ч+Инжектор 2.Текущий расход кислорода (режим горелки), м3/ч+Инжектор 3.Текущий расход кислорода (режим горелки), м3/ч+Инжектор 4.Текущий расход кислорода (режим горелки), м3/ч
                ExcelApp.Cells[row, 8] = Math.Round(protocol.Injector1History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlowBurner).FirstOrDefault() +
                     protocol.Injector2History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlowBurner).FirstOrDefault() +
                     protocol.Injector3History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlowBurner).FirstOrDefault() +
                     protocol.Injector4History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlowBurner).FirstOrDefault(), 0);
                //UO2RL(i) (интенсивность вдувания O2 в режиме фурмы) - PLC2.Инжектор 1.Текущий расход кислорода (режим инжектора), м3/ч+Инжектор 2.Текущий расход кислорода (режим инжектора), м3/ч+Инжектор 3.Текущий расход кислорода (режим инжектора), м3/ч+Инжектор 4.Текущий расход кислорода (режим инжектора), м3/ч
                ExcelApp.Cells[row, 9] = Math.Round(protocol.Injector1History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlowInjector).FirstOrDefault() +
                    protocol.Injector2History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlowInjector).FirstOrDefault() +
                    protocol.Injector3History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlowInjector).FirstOrDefault() +
                    protocol.Injector4History.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlowInjector).FirstOrDefault(), 0);
                //UO2L(i) (режим работы сводовой фурмы), м3/ч - PLC1. Сводовая фурма.Текущий расход кислорода, м3/ч
                ExcelApp.Cells[row, 10] = Math.Round(protocol.LanceCrestHistory.Where(x => x.Time >= start && x.Time < current).Select(x => x.CurrentOxygenFlow).FirstOrDefault(),0);
                //mCP(i)*60 (режим работы угольного инжектора), кг/мин - PLC4. Интенсивность вдувания кокса, кг/мин
                ExcelApp.Cells[row, 11] = Math.Round(protocol.CoalInjectionHistory.Where(x => x.Time >= start && x.Time < current).Select(x => x.CokeBlowing).FirstOrDefault() , 1);

                //mCh1/1000 (завалка с пальцев) - Вес завалки на плавку в тоннах - брать с провайдера MS SQL (скрап) - 
                //Ch1 (открытие пальцев) - Данные пока непишутся в БД, поэтом тому определяем как приблизительно 40 секунд от включения горелок
                // врямы = времени открытия пальцев, но данных пока нет, поэтому определяем как приблизительно 40 секунд от включения горелок
                if (fingerOpen == i)
                {
                    ExcelApp.Cells[row, 12] = Math.Round(protocol.ScrapLoadHistory.Where(x => x.TaskNumber == 1).Sum(x => x.Weight), 0);
                    ExcelApp.Cells[row, 41] = i;
                }
               
                //mCh2S/1000 (подвалка в шахту) - Вес подвалки на плавку в тоннах - брать с провайдера MS SQL (скрап)
                //Ch2S (подвалка в шахту) - Данные по времени завалки и подвалки находятся в базе MS SQL -  определяем как приблизительно 30 секунд от открытия пальцев
                // врямы = подвалки  (открытие бадьи, пальцы открыты)
                if (i >= fingerOpen && i < fingerClose && tubeOpen.FirstOrDefault(x => x == i) == i)
                {
                    ExcelApp.Cells[row, 13] = Math.Round(protocol.ScrapLoadHistory.Where(x => x.TaskNumber == 2).Sum(x => x.Weight), 0);
                    ExcelApp.Cells[row, 42] = i;
                }
                
                //mHI/1000 (жидкий чугун) PLC1. Паспорт плавки. Вес жидкого чугуна, т.
                //mHISl/1000 (шлак чугуновозного ковша) (5% от чугуна)
                //(THI(i)-273,15) (температура жидкого чугуна в ковше), ºС 
                //HIB (начало заливки чугуна)
                if (hib == i)
                {
                    var weight = Math.Round(hotMetal.Sum(x => x.Weight), 0);
                    ExcelApp.Cells[row, 14] = weight;
                    ExcelApp.Cells[row, 15] = weight/100*5;
                    ExcelApp.Cells[row, 17] = Math.Round(hotMetal.Select(x => x.Temperature).FirstOrDefault(), 0);
                    ExcelApp.Cells[row, 39] = i;
                }

                //mCh1SN/1000 (завалка на следующую плавку) - Вес завалки на плавку в тоннах - брать с провайдера MS SQL (скрап)
                //Ch1SN (завалка на следующую плавку) - Данные по времени завалки и подвалки находятся в базе MS SQL 
                // врямы = времени закрытия пальцев, но данных пока нет, попробуем взять с паспорта плавки там вроде что-то было
                if (i >= fingerClose && tubeOpen.FirstOrDefault(x => x == i) == i)
                {
                    ExcelApp.Cells[row, 16] = Math.Round(scrapLoadNext.Where(x => x.TaskNumber == 1).Sum(x => x.Weight), 0);
                    ExcelApp.Cells[row, 43] = i;
                }

                //mCk (кокс) Вес сыпучих на плавку в кг
                var temp = Math.Round(additions.Where(x => (x.Time >= start && x.Time < current) && x.Index == 1).Select(x => x.Weight).FirstOrDefault(), 0);
                if (temp > 0)
                {
                    ExcelApp.Cells[row, 18] = temp;
                }
                //mLmAdd (известь) Вес сыпучих на плавку в кг 
                temp = Math.Round(additions.Where(x => (x.Time >= start && x.Time < current) && x.Index == 2).Select(x => x.Weight).FirstOrDefault(), 0);
                if (temp > 0)
                {
                    ExcelApp.Cells[row, 19] = temp;
                }
                //mDlmtAdd (доломит) Вес сыпучих на плавку в кг
                temp = Math.Round(additions.Where(x => (x.Time >= start && x.Time < current) && x.Index == 3).Select(x => x.Weight).FirstOrDefault(), 0);
                if (temp > 0)
                {
                    ExcelApp.Cells[row, 20] = temp;
                }
                //mStTap(i) (скорость выпуска металла), кг/с  - вычисляется как вес выпуска/время выпуска металла. В данный момент время выпуска не фиксируется
                //mSlTap(i) (скорость слива шлака во время выпуска), кг/с - вычисляется как вес выпуска/время выпуска шлака. В данный момент время выпуска не фиксируется
                if (i >= outerb && i < outere)
                {
                    ExcelApp.Cells[row, 21] = Math.Round(protocol.HeatPassportHistory.Select(x => x.ReleaseWeight).FirstOrDefault()*1000/(outere - outerb), 1);
                    ExcelApp.Cells[row, 22] = Math.Round((double) (1000 / (outere - outerb)), 1);
                }
                else
                {
                    ExcelApp.Cells[row, 21] = 0;
                    ExcelApp.Cells[row, 22] = 0;
                }
                //(TWout(i)-273,15) (температура на выходе из стены), ºС - PLC1.Охлаждающая вода на панели (кожух). Температура охлаждающей воды на выходе кожуха (панелей)
                ExcelApp.Cells[row, 25] = Math.Round(protocol.WaterCoolingPanelHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempWaterOutputCover).FirstOrDefault(), 1);
                //(TWin(i)-273,15) (температура на входе в стену), ºС - PLC1.Охлаждающая вода на панели (кожух). Температура охлаждающей воды на входе
                ExcelApp.Cells[row, 26] = Math.Round(protocol.WaterCoolingPanelHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempWaterInput).FirstOrDefault(), 1);
                //UCWW(i) (расход воды в стене), м3/ч - PLC1.Охлаждающая вода на панели (кожух). Расход охлаждающей воды на выходе кожуха 
                ExcelApp.Cells[row, 27] = Math.Round(protocol.WaterCoolingPanelHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.FlowWaterOutputCover).FirstOrDefault(), 0);
                //UCWF(i) (расход воды в пальцах), м3/ч - PLC1.Охлаждающая вода на свод,шахту. Расход охлаждающей воды на выходе пальцев
                ExcelApp.Cells[row, 30] = Math.Round(protocol.WaterCoolingMineHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.FlowWaterOutputFingers).FirstOrDefault(), 0);
                //(TRout(i)-273,15) (температура на выходе из свода), ºС - PLC1.Охлаждающая вода на свод,шахту. Температура охлаждающей воды на выходе свода
                ExcelApp.Cells[row, 31] = Math.Round(protocol.WaterCoolingMineHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempWaterOutputCrest).FirstOrDefault(), 1);
                //(TRin(i)-273,15) (температура на входе в свод), ºС - PLC1.Охлаждающая вода на свод,шахту. Температура охлаждающей воды на входе свода
                ExcelApp.Cells[row, 32] = Math.Round(protocol.WaterCoolingMineHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempWaterInputCrest).FirstOrDefault(), 1);
                //UCWR(i) (расход воды в своде), м3/ч - PLC1.Охлаждающая вода на свод,шахту. Расход охлаждающей воды на выходе шахты и свода
                ExcelApp.Cells[row, 33] = Math.Round(protocol.WaterCoolingMineHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.FlowWaterOutputMineCrest).FirstOrDefault(), 0);
                //(TSout(i)-273,15) (температура на выходе из шахты), ºС - PLC1.Охлаждающая вода на свод,шахту. Температура охлаждающей воды на выходе шахты и колпака
                ExcelApp.Cells[row, 34] = Math.Round(protocol.WaterCoolingMineHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempWaterOutputMineHoodToo).FirstOrDefault(), 1);
                //(TSin(i)-273,15) (температура на входе в шахту), ºС - PLC1.Охлаждающая вода на свод,шахту. Температура охлаждающей воды на входе шахты и колпака
                ExcelApp.Cells[row, 35] = Math.Round(protocol.WaterCoolingMineHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.TempWaterOutputMineHood).FirstOrDefault(), 1);
                //UCWS(i) (расход воды в шахте), м3/ч - PLC1.Охлаждающая вода на свод,шахту. Расход охлаждающей воды на выходе шахты и свода
                ExcelApp.Cells[row, 36] = Math.Round(protocol.WaterCoolingMineHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.FlowWaterOutputMineCrest).FirstOrDefault(), 0);

                //HIE (окончание заливки чугуна) - Необходимо будет добавить на рабочей станции InTouch кнопку "Конец заливка чугуна" - и требовать, чтобы сталевар её нажимал при заливке. Сейчас данных нет
                // на данные момент вычисляем как время начала заливки + время затрациваемое на завивку (~ 5т в минуту)
                if (hie == i)
                {
                    ExcelApp.Cells[row, 40] = i;
                }
               
                //StTapB (Шибер сталевыпускного отверстия открыт)	
                if (gateOpen == i)
                {
                    ExcelApp.Cells[row, 44] = i;
                }
                //StTapE (Шибер сталевыпускного отверстия закрыты)	
                if (gateClose == i)
                {
                    ExcelApp.Cells[row, 45] = i;
                }

               
                //SlAddB (открытие загрузочного бункера)
                if (tubeOpen.FirstOrDefault(x => x==i) == i && i != 0)
                {
                    ExcelApp.Cells[row, 46] = i;
                }
                //SlAddE (закрытие загрузочного бункера)
                if (tubeClose.FirstOrDefault(x => x == i) == i && i != 0)
                {
                    ExcelApp.Cells[row, 47] = i;
                }

                //OCSD(i) (рабочее окно: 1-открыто; 0-закрыто) - PLC1.Рабочее окно. Рабочее окно открыто
                var open = protocol.WorkWindowHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.Open).FirstOrDefault();
                var close = protocol.WorkWindowHistory.Where(x => (x.Time >= start && x.Time < current)).Select(x => x.Close).FirstOrDefault();
                if (open && close) { temp = 0.5; }
                if (open && !close) { temp = 1; }
                if (!open && close) { temp = 0; }
                ExcelApp.Cells[row, 52] = temp;
                
                start = current;
            }
            return true;
        }


        private static List<int> TubOpen(IEnumerable<FingersEvent> figers, DateTime start, int stepTime)
        {
            var res = new List<int>();
            var open = false;
            foreach (var fingersEvent in figers)
            {
                if (fingersEvent.TubOpen && !open)
                {
                    open = true;
                    res.Add((int) Math.Round((fingersEvent.Time - start).TotalSeconds / stepTime));
                }
                if (!fingersEvent.TubOpen && open)
                {
                    open = false;
                }
            }
            return res;
        }

        private static List<int> TubClose(IEnumerable<FingersEvent> figers, DateTime start, int stepTime)
        {
            var res = new List<int>();
            var close = false;
            foreach (var fingersEvent in figers)
            {
                if (fingersEvent.TubClose && !close)
                {
                    close = true;
                    res.Add((int)Math.Round((fingersEvent.Time - start).TotalSeconds / stepTime));
                }
                if (!fingersEvent.TubClose && close)
                {
                    close = false;
                }
            }
            return res;
        } 

    }
}
