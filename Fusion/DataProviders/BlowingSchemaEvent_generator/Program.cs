using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;
using ConnectionProvider;
using Core;
using Converter;
using Implements;




namespace BlowingSchemaEvent_generator
{
    class Program
    {
        static ConnectionProvider.Client mainGate;
        public static object consoleLock = new object();

        static void Main(string[] args)
        {
            Thread transmitter_thread = new Thread(transmitter);
            Thread receiver_thread = new Thread(receiver);
            transmitter_thread.Start();
            receiver_thread.Start();
            /*Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("BlowingSchemaEvent_generator..................................................................[started]\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n Нажмите <ENTER> для выхода.");*/
            InstantLogger.log("BlowingSchemaEvent_generator", "Started", InstantLogger.TypeMessage.important);
            InstantLogger.log("Нажмите <ENTER> для выхода.");
           /* int qq = 10;
            Type tt = qq.GetType();
            object oo = Activator.CreateInstance(tt);
            oo = qq;
            InstantLogger.msg(oo.ToString() + " -- " + tt.ToString());*/
            //int s = 10;
            //System.IO.Directory.CreateDirectory("qqq");
            //File.WriteAllText(@"qqq\www", "aaaaa\nbbbbb\nccccc\n");
            //Console.WriteLine("save");
            
            
            Console.ReadLine();
        }
        static void transmitter(object state)
        {
            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.ForegroundColor = ConsoleColor.White;
            mainGate = new ConnectionProvider.Client();
            
            CultureInfo culture = CultureInfo.InvariantCulture;
            System.Threading.Thread.Sleep(1000);
            //mainGate.PushEvent(new comBlowingSchemaEvent()); // после запуска программы листенер пропускает первое сообщение через раз, как временное решение проблемы сообщение с -1 
            //mainGate.PushEvent(new HeatChangeEvent() {HeatNumber = 1133});
            //mainGate.PushEvent(new BoundNameMaterialsEvent() {Bunker1MaterialName = "m1", Bunker2MaterialName = "m2", Bunker3MaterialName = "m3"});
            //mainGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(BoundNameMaterialsEvent).Name });
            //mainGate.PushEvent(new OPCDirectReadEvent() {EventName = typeof(WeighersStateEvent).Name});
            
            //mainGate.PushEvent(new comAdditionsSchemaEvent());
            //mainGate.PushEvent(new cntWeigher3JobReadyEvent() {Counter = 1137});
            //mainGate.PushEvent(new cntWeigher4JobReadyEvent() { Counter = 1137 });
            //mainGate.PushEvent(new cntWeigher5JobReadyEvent() { Counter = 1137 });
            //mainGate.PushEvent(new cntWeigher6JobReadyEvent() { Counter = 1137 });
            //mainGate.PushEvent(new cntWeigher7JobReadyEvent() { Counter = 1137 });
            //mainGate.PushEvent(new comBlowingSchemaEvent());

            //mainGate.PushEvent(new cntBlowingSchemaEvent() {Counter = 1137});
            //mainGate.PushEvent(new cntO2FlowRateEvent() { Counter = 1137 });
            //var be = new BlowingEvent() {O2TotalVol = 0};
            //int oxyRate = 100;
            //while (true)
            //{
            //    be.O2TotalVol += oxyRate;
            //    mainGate.PushEvent(be);
            //    Thread.Sleep(500);
            //}
            //int ii = 0;
            //while (true)
            //{
            //    mainGate.PushEvent(new HeatChangeEvent() {HeatNumber = ii++});
            //    Thread.Sleep(100);
            //}

            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.ForegroundColor = ConsoleColor.Green;
            /*lock (consoleLock)
            {
                Console.WriteLine("transmitter...................................................................................[started]\n");
            }*/

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(int[]));
            var dimm = new int[] { 1, 2, 3 };
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, dimm);
            ms.Close();
            var arr = ms.ToArray();
            var str = Encoding.UTF8.GetString(arr);
            Console.WriteLine(str);

            var fxe = new FlexHelper("dimm");

            fxe.AddArg("dd", str);
            fxe.Fire(mainGate);

            var fex = new FlexHelper("OPC.Flex.Suka");
            fex.AddArg("val-i2", 1133);
            fex.AddArg("val-r4", -11.33);
            fex.AddArg("val-s", "Zalupa");
            fex.Fire(mainGate);
           //mainGate.PushEvent(new HeatChangeEvent() { HeatNumber = 221111 });

            //mainGate.PushEvent(new visSpectrluksEvent() { HeatNumber = 2201111, C = 0.05 });
            
            InstantLogger.log("transmitter", "Started", InstantLogger.TypeMessage.important);
            string[] strings;
            
            try
            {
                strings = File.ReadAllLines("BlowData.csv");
            }
            catch 
            {
                strings = new string[0];
                //Console.Write("cannot read the file");
                InstantLogger.log("Cannot read the file", "Error", InstantLogger.TypeMessage.error);
                return;
            }
            //Console.Write(strings.ToString() + "\n");


            comBlowingSchemaEvent blowingSchemaEvent = new comBlowingSchemaEvent();
            List<int> O2Vol = new List<int>();
            List<int> LancePosition = new List<int>();
            List<double> O2Flow = new List<double>();
            int O2SchemaNumber = 0;
            O2Vol.Add(-1);          // выравнивание чтобы совпадали номера
            LancePosition.Add(-1);  // выравнивание чтобы совпадали номера    
            O2Flow.Add(-1);         // выравнивание чтобы совпадали номера    

            for (int i = 1; i < strings.Count(); i++) // в нулевой строке заголовки
            {
                string[] values = strings[i].Split(' ');

                O2SchemaNumber = int.Parse(values[0], culture);
                O2Vol.Add(int.Parse(values[1], culture));
                LancePosition.Add(int.Parse(values[2], culture));
                O2Flow.Add(double.Parse(values[3], culture));
            }
           /* lock (consoleLock)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("transmitter...................................................................................[send]\n");
            }*/
            InstantLogger.log("transmitter", "Send", InstantLogger.TypeMessage.unimportant);

            blowingSchemaEvent.O2SchemaNumber = O2SchemaNumber;
            blowingSchemaEvent.O2VolStep1 = O2Vol.ElementAt(1);
            blowingSchemaEvent.O2VolStep2 = O2Vol.ElementAt(2);
            blowingSchemaEvent.O2VolStep3 = O2Vol.ElementAt(3);
            blowingSchemaEvent.O2VolStep4 = O2Vol.ElementAt(4);
            blowingSchemaEvent.O2VolStep5 = O2Vol.ElementAt(5);
            blowingSchemaEvent.O2VolStep6 = O2Vol.ElementAt(6);
            blowingSchemaEvent.O2VolStep7 = O2Vol.ElementAt(7);
            blowingSchemaEvent.O2VolStep8 = O2Vol.ElementAt(8);
            blowingSchemaEvent.O2VolStep9 = O2Vol.ElementAt(9);
            blowingSchemaEvent.O2VolStep10 = O2Vol.ElementAt(10);
            blowingSchemaEvent.O2VolStep11 = O2Vol.ElementAt(11);
            blowingSchemaEvent.O2VolStep12 = O2Vol.ElementAt(12);
            blowingSchemaEvent.O2VolStep13 = O2Vol.ElementAt(13);
            blowingSchemaEvent.O2VolStep14 = O2Vol.ElementAt(14);
            blowingSchemaEvent.O2VolStep15 = O2Vol.ElementAt(15);
            blowingSchemaEvent.O2VolStep16 = O2Vol.ElementAt(16);
            blowingSchemaEvent.O2VolStep17 = O2Vol.ElementAt(17);
            blowingSchemaEvent.O2VolStep18 = O2Vol.ElementAt(18);
            blowingSchemaEvent.O2VolStep19 = O2Vol.ElementAt(19);
            blowingSchemaEvent.O2VolStep20 = O2Vol.ElementAt(20);
            blowingSchemaEvent.LancePositionStep1 = LancePosition.ElementAt(1);
            blowingSchemaEvent.LancePositionStep2 = LancePosition.ElementAt(2);
            blowingSchemaEvent.LancePositionStep3 = LancePosition.ElementAt(3);
            blowingSchemaEvent.LancePositionStep4 = LancePosition.ElementAt(4);
            blowingSchemaEvent.LancePositionStep5 = LancePosition.ElementAt(5);
            blowingSchemaEvent.LancePositionStep6 = LancePosition.ElementAt(6);
            blowingSchemaEvent.LancePositionStep7 = LancePosition.ElementAt(7);
            blowingSchemaEvent.LancePositionStep8 = LancePosition.ElementAt(8);
            blowingSchemaEvent.LancePositionStep9 = LancePosition.ElementAt(9);
            blowingSchemaEvent.LancePositionStep10 = LancePosition.ElementAt(10);
            blowingSchemaEvent.LancePositionStep11 = LancePosition.ElementAt(11);
            blowingSchemaEvent.LancePositionStep12 = LancePosition.ElementAt(12);
            blowingSchemaEvent.LancePositionStep13 = LancePosition.ElementAt(13);
            blowingSchemaEvent.LancePositionStep14 = LancePosition.ElementAt(14);
            blowingSchemaEvent.LancePositionStep15 = LancePosition.ElementAt(15);
            blowingSchemaEvent.LancePositionStep16 = LancePosition.ElementAt(16);
            blowingSchemaEvent.LancePositionStep17 = LancePosition.ElementAt(17);
            blowingSchemaEvent.LancePositionStep18 = LancePosition.ElementAt(18);
            blowingSchemaEvent.LancePositionStep19 = LancePosition.ElementAt(19);
            blowingSchemaEvent.LancePositionStep20 = LancePosition.ElementAt(20);
            blowingSchemaEvent.O2FlowStep1 = O2Flow.ElementAt(1);
            blowingSchemaEvent.O2FlowStep2 = O2Flow.ElementAt(2);
            blowingSchemaEvent.O2FlowStep3 = O2Flow.ElementAt(3);
            blowingSchemaEvent.O2FlowStep4 = O2Flow.ElementAt(4);
            blowingSchemaEvent.O2FlowStep5 = O2Flow.ElementAt(5);
            blowingSchemaEvent.O2FlowStep6 = O2Flow.ElementAt(6);
            blowingSchemaEvent.O2FlowStep7 = O2Flow.ElementAt(7);
            blowingSchemaEvent.O2FlowStep8 = O2Flow.ElementAt(8);
            blowingSchemaEvent.O2FlowStep9 = O2Flow.ElementAt(9);
            blowingSchemaEvent.O2FlowStep10 = O2Flow.ElementAt(10);
            blowingSchemaEvent.O2FlowStep11 = O2Flow.ElementAt(11);
            blowingSchemaEvent.O2FlowStep12 = O2Flow.ElementAt(12);
            blowingSchemaEvent.O2FlowStep13 = O2Flow.ElementAt(13);
            blowingSchemaEvent.O2FlowStep14 = O2Flow.ElementAt(14);
            blowingSchemaEvent.O2FlowStep15 = O2Flow.ElementAt(15);
            blowingSchemaEvent.O2FlowStep16 = O2Flow.ElementAt(16);
            blowingSchemaEvent.O2FlowStep17 = O2Flow.ElementAt(17);
            blowingSchemaEvent.O2FlowStep18 = O2Flow.ElementAt(18);
            blowingSchemaEvent.O2FlowStep19 = O2Flow.ElementAt(19);
            blowingSchemaEvent.O2FlowStep20 = O2Flow.ElementAt(20);

            SteelMakingPatternEvent steelMakingPatternEvent = new SteelMakingPatternEvent();
            steelMakingPatternEvent.materialsName[0] = "SUKA yana";
            steelMakingPatternEvent.materialsName[1] = "ДОЛОМС";
            steelMakingPatternEvent.materialsName[2] = "3material";
            steelMakingPatternEvent.materialsName[3] = "4material";
            steelMakingPatternEvent.materialsName[4] = "PULSE-CC";
            steelMakingPatternEvent.materialsName[5] = "6material";
            steelMakingPatternEvent.materialsName[6] = "7material";
            steelMakingPatternEvent.materialsName[7] = "8material";
            steelMakingPatternEvent.materialsName[8] = "9material";
            steelMakingPatternEvent.materialsName[9] = "ЯНА";

            steelMakingPatternEvent.steps.Add(new Converter.SteelMakingClasses.Step());
            steelMakingPatternEvent.steps[0].lance.LancePositin = 700;
            steelMakingPatternEvent.steps[0].lance.O2Flow = 450;
            steelMakingPatternEvent.steps[0].O2Volume = 0;

       /*     for (int i = 0; i < 10; i++)
            {
                steelMakingPatternEvent.steps[0].additions.addition[i].MaterialPortionWeight = 0;
            }
         */   

            Random rnd1 = new Random();
            int qq = 0;
            for (int step = 1; step <= 25; step++)
            {
                steelMakingPatternEvent.steps.Add(new Converter.SteelMakingClasses.Step());
                steelMakingPatternEvent.steps[step].lance.LancePositin = steelMakingPatternEvent.steps[step-1].lance.LancePositin - 25;
                steelMakingPatternEvent.steps[step].lance.O2Flow = 1000;
                steelMakingPatternEvent.steps[step].O2Volume = steelMakingPatternEvent.steps[step - 1].O2Volume + 100;
                //qq = rnd1.Next(10);
                //steelMakingPatternEvent.steps[step].additions.addition[rnd1.Next(10)].MaterialPortionWeight = rnd1.Next(100,1000);
                int weigherid = rnd1.Next(5);
                steelMakingPatternEvent.steps[step].weigherLines[weigherid].PortionWeight = rnd1.Next(100, 1000);
                //steelMakingPatternEvent.steps[step].weigherLines[weigherid].BunkerId = rnd1.Next(8);
                if (weigherid == 0)
                    steelMakingPatternEvent.steps[step].weigherLines[weigherid].BunkerId = rnd1.Next(0, 2);
                if (weigherid == 1)
                    steelMakingPatternEvent.steps[step].weigherLines[weigherid].BunkerId = 2;
                if (weigherid == 2)
                    steelMakingPatternEvent.steps[step].weigherLines[weigherid].BunkerId = 3;
                if (weigherid == 3)
                    steelMakingPatternEvent.steps[step].weigherLines[weigherid].BunkerId = 4;
                if (weigherid == 4)
                    steelMakingPatternEvent.steps[step].weigherLines[weigherid].BunkerId = rnd1.Next(5, 8);

                /* for (int i = 0; i < 10; i++)
                {
                    steelMakingPatternEvent.steps[step].additions.addition[i].MaterialPortionWeight = 1000 + i*100 + i*10 + i;
                }*/

                /*steelMakingPatternEvent.steps[step].additions.addition[0].MaterialPortionWeight = 100;
                steelMakingPatternEvent.steps[step].additions.addition[0].O2VolPortionMateria =
                    steelMakingPatternEvent.steps[step].lance.O2Volume;*/
            }
           /* steelMakingPatternEvent.steps[5].additions.addition[0].MaterialPortionWeight = 50;
            steelMakingPatternEvent.steps[10].additions.addition[0].MaterialPortionWeight = 100;

            steelMakingPatternEvent.steps[7].additions.addition[1].MaterialPortionWeight = 71;
            steelMakingPatternEvent.steps[15].additions.addition[1].MaterialPortionWeight = 151;*/

           /* steelMakingPatternEvent.steps[0].additions.addition[0].MaterialPortionWeight = 100;
            steelMakingPatternEvent.steps[0].additions.addition[0].O2VolPortionMateria = 200;

            steelMakingPatternEvent.steps[0].additions.addition[1].MaterialPortionWeight = 100;
            steelMakingPatternEvent.steps[0].additions.addition[1].O2VolPortionMateria = 200;

            steelMakingPatternEvent.steps[0].additions.addition[2].MaterialPortionWeight = 200;
            steelMakingPatternEvent.steps[0].additions.addition[2].O2VolPortionMateria = 200;

            steelMakingPatternEvent.steps[1].additions.addition[3].MaterialPortionWeight = 200;
            steelMakingPatternEvent.steps[1].additions.addition[3].O2VolPortionMateria = 200;*/

            //steelMakingPatternEvent.steps[0].additions.
            BlowingEvent blowingEvent = new BlowingEvent();
            blowingEvent.O2TotalVol = 0;
        //    mainGate.PushEvent(blowingEvent);
           
         //   mainGate.PushEvent(steelMakingPatternEvent);
            
            //blowingEvent.O2TotalVol = 200;
       /*     ComName1MatEvent ssss =new ComName1MatEvent();
            ssss.Name = "TEST";
           
            ComName2MatEvent cccc = new ComName2MatEvent();
            cccc.Name[0] = (char) 6;
            cccc.Name[1] = (char) 1;
            cccc.Name[3] = (char) 32;*/
            var WSE = new WeighersStateEvent();
            WSE.Weigher3Empty = 1;
            WSE.Weigher3LoadFree = 1;
            WSE.Weigher3UnLoadFree = 1;
            WSE.Weigher4Empty = 1;
            WSE.Weigher4LoadFree = 1;
            WSE.Weigher4UnLoadFree = 1;
            WSE.Weigher5Empty = 1;
            WSE.Weigher5LoadFree = 1;
            WSE.Weigher5UnLoadFree = 1;
            WSE.Weigher6Empty = 1;
            WSE.Weigher6LoadFree = 1;
            WSE.Weigher6UnLoadFree = 1;
            WSE.Weigher7Empty = 1;
            WSE.Weigher7LoadFree = 1;
            WSE.Weigher7UnLoadFree = 1;
            
       //!!!!!     mainGate.PushEvent(steelMakingPatternEvent);
            
            
            //mainGate.PushEvent(new OPCDirectReadEvent() { EventName = "comAdditionsEvent" });
            //int currentStep = 0;
            //while (true)
            //{
            //    if (currentStep > 15)
            //    {
            //        currentStep = 0;
            //    }
            //    else
            //    {
            //        currentStep++;
            //        mainGate.PushEvent(new HeatSchemaStepEvent {Step = currentStep});
            //        Thread.Sleep(1000);
            //    }
            //}

            int cnt = 0;
            int step2 = 0;
            bool stepChange = false;
         /*   while (true)
            {
                         blowingEvent.O2TotalVol +=10;
                mainGate.PushEvent(blowingEvent);
                //mainGate.PushEvent(WSE);
                cnt++;
                if (cnt > 10)
                {
                    //mainGate.PushEvent(steelMakingPatternEvent);
                    //Thread.Sleep(3000);
                    cnt = 0;
                }
               // mainGate.PushEvent(new cntWatchDogPLC01Event());
               
                //mainGate.PushEvent(WSE);
                if (steelMakingPatternEvent.steps.Count > step2)
                {
                    if (steelMakingPatternEvent.steps[step2].O2Volume < blowingEvent.O2TotalVol)
                    {
                        step2++;
                        stepChange = true;
                    }
                }
                if (steelMakingPatternEvent.steps.Count > step2)
                {
                    if (stepChange)
                    {
                        if (steelMakingPatternEvent.steps[step2].weigherLines[0].PortionWeight > 0)
                        {
                            mainGate.PushEvent(new WeighersStateEvent()
                                                   {Weigher3Empty = 1, Weigher3LoadFree = 1, Weigher3UnLoadFree = 1});
                            stepChange = false;
                            InstantLogger.log("W3 - " +
                                       steelMakingPatternEvent.steps[step2].weigherLines[0].PortionWeight.ToString());
                        }
                        if (steelMakingPatternEvent.steps[step2].weigherLines[1].PortionWeight > 0)
                        {
                            mainGate.PushEvent(new WeighersStateEvent()
                                                   {Weigher4Empty = 1, Weigher4LoadFree = 1, Weigher4UnLoadFree = 1});
                            stepChange = false;
                            InstantLogger.log("W4 - " +
                                       steelMakingPatternEvent.steps[step2].weigherLines[1].PortionWeight.ToString());
                        }
                        if (steelMakingPatternEvent.steps[step2].weigherLines[2].PortionWeight > 0)
                        {
                            mainGate.PushEvent(new WeighersStateEvent()
                                                   {Weigher5Empty = 1, Weigher5LoadFree = 1, Weigher5UnLoadFree = 1});
                            stepChange = false;
                            InstantLogger.log("W5 - " +
                                       steelMakingPatternEvent.steps[step2].weigherLines[2].PortionWeight.ToString());
                        }
                        if (steelMakingPatternEvent.steps[step2].weigherLines[3].PortionWeight > 0)
                        {
                            mainGate.PushEvent(new WeighersStateEvent()
                                                   {Weigher6Empty = 1, Weigher6LoadFree = 1, Weigher6UnLoadFree = 1});
                            stepChange = false;
                            InstantLogger.log("W6 - " +
                                       steelMakingPatternEvent.steps[step2].weigherLines[3].PortionWeight.ToString());
                        }
                        if (steelMakingPatternEvent.steps[step2].weigherLines[4].PortionWeight > 0)
                        {
                            mainGate.PushEvent(new WeighersStateEvent()
                                                   {Weigher7Empty = 1, Weigher7LoadFree = 1, Weigher7UnLoadFree = 1});
                            stepChange = false;
                            InstantLogger.log("W7 - " +
                                       steelMakingPatternEvent.steps[step2].weigherLines[4].PortionWeight.ToString());
                        }
                    }
                }

                Thread.Sleep(1000);
            }*/

            //comO2FlowRateEvent _comO2FlowRateEvent = new comO2FlowRateEvent();
            //BlowingEvent blowingEvent = new BlowingEvent();
            //blowingEvent.O2TotalVol = 1000;

            //_comO2FlowRateEvent.O2TotalVol = 1000;
            //mainGate.PushEvent(_comO2FlowRateEvent);
            // mainGate.PushEvent(new cntO2FlowRateEvent());
            //mainGate.PushEvent(blowingSchemaEvent);
            //mainGate.PushEvent(new cntBlowingSchemaEvent());
            //_comO2FlowRateEvent.iCnvNr = 1;
            /*  while (true)
            {
                _comO2FlowRateEvent.O2TotalVol++;
                blowingEvent.O2TotalVol++;
                mainGate.PushEvent(blowingEvent);
                //mainGate.PushEvent(_comO2FlowRateEvent);
                mainGate.PushEvent(new cntWatchDogPLC01Event());
              //  mainGate.PushEvent(new cntBlowingSchemaEvent());
                //mainGate.PushEvent(new cntWatchDogPLC1Event());
                mainGate.PushEvent(_comO2FlowRateEvent);
                //mainGate.PushEvent(new cntWatchDogPLC1Event());
                Thread.Sleep(1000);
            }*/
            //SteelMakingPatternEvent ss = new SteelMakingPatternEvent();
            //ss.steps.Add(new Converter.SteelMakingClasses.Step() { Period = 777});
            //ss.steps.Add(new Converter.SteelMakingClasses.Step());
            //Console.Write(ss.ToString());
            //Console.Write(ss.steps[0].Period.ToString());
            // mainGate.PushEvent(ss);
            /* while (true)
            {
                lock (mainGate)
                {
                    mainGate.PushEvent(blowingSchemaEvent);
                    blowingSchemaEvent.O2FlowStep20++;
                }
                Thread.Sleep(1000);
            }*/
        }

        static void receiver(object state)
        {
            var o = new HeatChangeEvent();
            mainGate = new ConnectionProvider.Client(new Listener());
            mainGate.Subscribe();
            
            //Thread.Sleep(5000);
            //mainGate.Unsubscribe();
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.ForegroundColor = ConsoleColor.Green;
            /*lock (consoleLock)
            {
                Console.WriteLine("receiver......................................................................................[started]\n");
            }*/
            InstantLogger.log("receiver", "Started", InstantLogger.TypeMessage.important);
        }
    }
}
