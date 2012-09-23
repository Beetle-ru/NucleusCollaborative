using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonTypes;
using Converter;
using ConnectionProvider;
using Implements;

namespace ConverterHeatProcessorEngine
{
    static public class HeatEngine
    {
        public static bool HeatOn { set; get; }
        private static bool m_dataAvailable;
        private static int m_oxigenTotal;
        private static int m_oxigenCurrent;
        private static int m_oxigenCurrentStep;
        private static int m_lanceCurrentFrame;
        private static int m_additionsCurrentFrame;
        private static SteelMakingPatternEvent SmPattern { set; get; } // шаблон хранимый в классе 
        static List<AdditionsQuant> AdditionsQuantList { set; get; }  // уплотненная структура данных по добавокам
        private static ConnectionProvider.Client m_pushGate;
        private static ConnectionProvider.Client m_listenGate;
        private const int LanceMaxStepsFrame = 20; // количество шагов в кадре для фурмы по умолчанию 20
        private const int AdditionsMaxStepsFrame = 3; // количество шагов в кадре для добавок по умолчанию 3
        public static int Init()
        {
            HeatOn = true;
            m_dataAvailable = false;
            m_oxigenTotal = 0;
            m_oxigenCurrent = 0;
            m_oxigenCurrentStep = 0;
            SmPattern = new SteelMakingPatternEvent();
            AdditionsQuantList = new List<AdditionsQuant>();
            m_pushGate = new ConnectionProvider.Client();
            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();
           // Thread.Sleep(1000);
           // m_pushGate.PushEvent(new cntAdditionsEvent());
            return 0;

        }
        public static int Processor(BaseEvent newEvent)
        {
            if (newEvent is BlowingEvent)
            {
               BlowingEvent newBE = newEvent as BlowingEvent;

                if (m_oxigenCurrent != newBE.O2TotalVol && HeatOn && m_dataAvailable)
                {
                    m_oxigenCurrent = newBE.O2TotalVol;
                    m_oxigenCurrentStep = LanceCurrentStep();
                    //LanceCurrentStep();
                    if (m_oxigenCurrentStep != -1)
                    {
                        Logger.log(
                            "step - " + m_oxigenCurrentStep.ToString() + " frame - " + m_lanceCurrentFrame.ToString() +
                            " m_oxigenCurrent  - " + m_oxigenCurrent.ToString() + " o2 - " +
                            SmPattern.steps[m_oxigenCurrentStep].O2Volume.ToString(), "Processing",
                            Logger.TypeMessage.important);
                    }
                    else
                    {
                        Logger.log(
                            "step - " + m_oxigenCurrentStep.ToString() + " frame - " + m_lanceCurrentFrame.ToString() +
                            " m_oxigenCurrent  - " + m_oxigenCurrent.ToString()
                            , "Process complete",
                            Logger.TypeMessage.caution);
                        m_dataAvailable = false;
                    }

                    if (m_lanceCurrentFrame != LanceGetFrameNumber() && m_oxigenCurrentStep != -1)
                    {
                        //Logger.log("wwwwwwwwwwwwwwwwwww", "Processor", Logger.TypeMessage.terror);
                        ComSender(LanceQuantizer()); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                       // LanceQuantizer();
                    }
                  //!!!!!!!!!!!!!  AdditionsQuantRefrash();

                /*     for (int step = 0; step < SmPattern.steps.Count; step++)
                    {
                          if (SmPattern.steps[step].O2Volume > m_oxigenCurrent)
                        {
                            if (step + 1  >= SmPattern.steps.Count)
                            {
                                m_dataAvailable = false;
                                m_oxigenCurrentStep = step;
                            }
                            else
                                m_oxigenCurrentStep = step;

                            if (m_oxigenCurrentStep < 0)
                                m_oxigenCurrentStep = 0;
                            
                            break;
                        }
                    }*/
              /*      int stepMinus = 0;
                    stepMinus = m_oxigenCurrentStep - 1;
                        if (stepMinus < 0)
                                stepMinus = 0;
                    if (m_oxigenCurrentStep + 1 >= SmPattern.steps.Count)
                    {
                        Logger.log(
                            "step - " + m_oxigenCurrentStep.ToString() + " frame - " + m_lanceCurrentFrame.ToString() +
                            " m_oxigenCurrent  - " + m_oxigenCurrent.ToString() + " o2 - " +
                            SmPattern.steps[stepMinus].O2Volume.ToString(), "Process complete",
                            Logger.TypeMessage.caution);
                    }
                    else
                    {

                        Logger.log(
                            "step - " + m_oxigenCurrentStep.ToString() + " frame - " + m_lanceCurrentFrame.ToString() +
                            " m_oxigenCurrent  - " + m_oxigenCurrent.ToString() + " o2 - " +
                            SmPattern.steps[stepMinus].O2Volume.ToString(), "Processing",
                            Logger.TypeMessage.important);
                    }
                   
                    
                    if (m_lanceCurrentFrame != LanceGetFrameNumber())
                    {
                        //Logger.log("wwwwwwwwwwwwwwwwwww", "Processor", Logger.TypeMessage.terror);
                        ComSender(LanceQuantizer());
                    }
                    /*if (m_additionsCurrentFrame != AdditionsGetFrameNumber())
                    {
                        Logger.log("c " + m_additionsCurrentFrame.ToString() + " n " + AdditionsGetFrameNumber().ToString());
                        ComSender(AdditionsQuantizer());
                        
                    }*/
                    
                   
                }
           // m_oxigenCurrentStep = 0; // расчет текущего шага по событи изменения кислорода
            }
            if (newEvent is SteelMakingPatternEvent)
            {
                SteelMakingPatternEvent newSMPE = newEvent as SteelMakingPatternEvent;
                // здесь должен быть блок сравнения SmPattern с newSMPE 
                SmPattern = newSMPE;
                m_dataAvailable = true; // данные пришли, начинем плавку
                if (HeatOn)
                {
                    ComSender(LanceQuantizer());
                    //ComSender(AdditionsQuantizer());
                   /// LanceQuantizer();
                   //!!!!!!!!! ComSender(AdditionsQuantizerFr());
                   //!!!!!!!!!! ComSenderMaterialNames(SmPattern);
                    //Logger.log(SmPattern.steps[1].lance.LancePositin.ToString());
                   // Logger.log(SmPattern.steps[0].O2Volume.ToString());
                   // Logger.log(SmPattern.steps[0].lance.LancePositin.ToString());
                   // Logger.log(SmPattern.steps[23].lance.LancePositin.ToString());


                }
                //List<Converter.SteelMakingClasses.Step> lanceStepsFrame = Quantizer();
                //stepsFrame = Quantizer(); // получаем кадр из шагов для отправки

                //Logger.log(newSMPE.ToString(), "Processor", Logger.TypeMessage.terror);
            }
           
            //Logger.log(newEvent.ToString(), "Processor", Logger.TypeMessage.terror);
            return 0;
        }

        private static List<LanceQuant> LanceQuantizer()
        {
            Logger.log(SmPattern.steps[0].O2Volume.ToString());
            int frameStart = 0;
            //int reminder = 0;

            //frameStart = Math.DivRem(m_oxigenCurrentStep, MaxStepsFrame, out reminder); //определяем номер кадра
            frameStart = LanceGetFrameNumber();
            m_lanceCurrentFrame = frameStart;
            frameStart *= LanceMaxStepsFrame;                                          //определяем начало кадра

            List<LanceQuant> LanceStepsFrame = new List<LanceQuant>();

            for (int step = 0; step < LanceMaxStepsFrame; step++) //выборка MaxStepsFrame шагов
            {
                int shiftStep = 0;
                shiftStep = frameStart + step;
                LanceStepsFrame.Add(new LanceQuant());
                if (shiftStep < SmPattern.steps.Count)
                {
                    //LanceStepsFrame[step] = SmPattern.steps[shiftStep].lance;
                    //Logger.log(shiftStep.ToString() + " --- " + SmPattern.steps[shiftStep].O2Volume.ToString());
                    LanceStepsFrame[step].O2Volume = SmPattern.steps[shiftStep].O2Volume;
                    LanceStepsFrame[step].O2Flow = SmPattern.steps[shiftStep].lance.O2Flow;
                    LanceStepsFrame[step].LancePositin = SmPattern.steps[shiftStep].lance.LancePositin;
                }
                else
                {
                    LanceStepsFrame[step] = new LanceQuant(); // забиваем -1, которые присваиваются в конструкторе
                }
           //     Logger.log(String.Format("step {0} ==>> {1}\n", step, stepsFrame[step].lance.LancePositin), "LancePosition", Logger.TypeMessage.danger);
                //Logger.log(String.Format("step {0} ==>> {1}\n", step, stepsFrame[step].lance.O2Flow), "O2Flow", Logger.TypeMessage.danger);
                //Logger.log(String.Format("step {0} ==>> {1}\n", step, stepsFrame[step].lance.O2Volume), "O2Volume", Logger.TypeMessage.danger);
            }
            //Logger.log(frameStart.ToString(), "frameStart", Logger.TypeMessage.important);
           // Logger.log(LanceStepsFrame[0].O2Volume.ToString());
            //Logger.log("wwwwwwwwwwwwwwwwwww", "Processor", Logger.TypeMessage.terror);
            return LanceStepsFrame;
        }

        private static List<AdditionsQuant> AdditionsQuantizer()
        {
            int frameStart = 0;
            AdditionsQuantList = AdditionsTableCompressor(); // заполняем квант лист новыми данными
           // Logger.log("WWWWWWWWWWWWWWWWWWWWW", "Processor", Logger.TypeMessage.terror);
           
            frameStart = AdditionsGetFrameNumber();
            m_additionsCurrentFrame = frameStart;
            frameStart *= AdditionsMaxStepsFrame;                                          //определяем начало кадра

            List<AdditionsQuant> additionsStepsFrame = new List<AdditionsQuant>();

            for (int step = 0; step < AdditionsMaxStepsFrame; step++) //выборка MaxStepsFrame шагов
            {

                int shiftStep = 0;
                shiftStep = frameStart + step;
                additionsStepsFrame.Add(new AdditionsQuant());
                if (shiftStep < AdditionsQuantList.Count)
                {
                    additionsStepsFrame[step] = AdditionsQuantList[step];
                  //  if(step == 1)
                       // Logger.log(" == " + AdditionsQuantList[step].Addition[0].MaterialPortionWeight, "Processor", Logger.TypeMessage.terror);
                    //Logger.log("WWWWWWWWWWWWWWWWWWWWW", "Processor", Logger.TypeMessage.terror);
                }
                else
                {
                   // Logger.log("OOOOOOOOOOOOOOOOOOOOO", "Processor", Logger.TypeMessage.terror);
                   // Logger.log("AdditionsQuantList - " + AdditionsQuantList.Count.ToString() + " shiftStep - " + shiftStep.ToString() + " frameStart - " + frameStart.ToString(), "Processor", Logger.TypeMessage.terror);
                    additionsStepsFrame[step] = new AdditionsQuant(); // забиваем -1, которые присваиваются в конструкторе
                }
            }
            return additionsStepsFrame;
        }


        private static AdditionsQuant AdditionsQuantizerFr()
        {
            AdditionsQuant additionsFrame = new AdditionsQuant();

            AdditionsQuantList = AdditionsTableCompressor();

            for (int material = 0; material < AdditionsQuantList[0].Addition.Count; material++)
            {
                for (int step = 0; step < AdditionsQuantList.Count; step++)
                {
                    if (m_oxigenCurrent > AdditionsQuantList[step].Addition[material].O2VolPortionMaterial)
                    {
                        AdditionsQuantList[step].Addition[material].O2VolPortionMaterial = -1; //заполняем -1 чтобы выполненные шаги не мешались
                        AdditionsQuantList[step].Addition[material].MaterialPortionWeight = -1;
                        //выполненный шаг
                       // Logger.log("wwwwwwwwwwwwwwwwwww", "Processor", Logger.TypeMessage.terror);
                    }
                    else
                    {
                        additionsFrame.Addition[material] = AdditionsQuantList[step].Addition[material];
                        Logger.log("material - " + material.ToString() + " step - " + step.ToString() + " value - " + AdditionsQuantList[step].Addition[material].MaterialPortionWeight.ToString(), "Processor", Logger.TypeMessage.terror);
                        break;
                    }
                } 
             }
                

                return additionsFrame;
        }

        private  static int AdditionsQuantRefrash()
        {
            for (int step = 0; step < AdditionsQuantList.Count; step++)
            {
                for (int material = 0; material < AdditionsQuantList[step].Addition.Count; material++)
                {
                    if (m_oxigenCurrent > AdditionsQuantList[step].Addition[material].O2VolPortionMaterial)
                    {
                        if (AdditionsQuantList[step].Addition[material].O2VolPortionMaterial > 0)
                        {
                            ComSender(AdditionsQuantizerFr()); //если нашли выполненный шаг то обновляем кадр
                        }
                    }
                    
                }
            }
            return 0;
        }

        public static int ComSender(List<LanceQuant> lanceStepsFrame)//(comAdditionsEvent prmComAdditionsEvent, comAdditionsSchemaEvent prmComAdditionsSchemaEvent, comBlowingSchemaEvent prmComBlowingSchemaEvent, comO2FlowRateEvent prmComO2FlowRateEvent)
        {
            //Logger.log("wwwwwwwwwwwwwwwwwww", "Processor", Logger.TypeMessage.terror);
            comBlowingSchemaEvent blowingSchemaEvent = new comBlowingSchemaEvent();
           
            blowingSchemaEvent.LancePositionStep1 = lanceStepsFrame[0].LancePositin;
            blowingSchemaEvent.O2FlowStep1 = lanceStepsFrame[0].O2Flow;
            blowingSchemaEvent.O2VolStep1 = lanceStepsFrame[0].O2Volume;
            blowingSchemaEvent.LancePositionStep2 = lanceStepsFrame[1].LancePositin;
            blowingSchemaEvent.O2FlowStep2 = lanceStepsFrame[1].O2Flow;
            blowingSchemaEvent.O2VolStep2 = lanceStepsFrame[1].O2Volume;
            blowingSchemaEvent.LancePositionStep3 = lanceStepsFrame[2].LancePositin;
            blowingSchemaEvent.O2FlowStep3 = lanceStepsFrame[2].O2Flow;
            blowingSchemaEvent.O2VolStep3 = lanceStepsFrame[2].O2Volume;
            blowingSchemaEvent.LancePositionStep4 = lanceStepsFrame[3].LancePositin;
            blowingSchemaEvent.O2FlowStep4 = lanceStepsFrame[3].O2Flow;
            blowingSchemaEvent.O2VolStep4 = lanceStepsFrame[3].O2Volume;
            blowingSchemaEvent.LancePositionStep5 = lanceStepsFrame[4].LancePositin;
            blowingSchemaEvent.O2FlowStep5 = lanceStepsFrame[4].O2Flow;
            blowingSchemaEvent.O2VolStep5 = lanceStepsFrame[4].O2Volume;
            blowingSchemaEvent.LancePositionStep6 = lanceStepsFrame[5].LancePositin;
            blowingSchemaEvent.O2FlowStep6 = lanceStepsFrame[5].O2Flow;
            blowingSchemaEvent.O2VolStep6 = lanceStepsFrame[5].O2Volume;
            blowingSchemaEvent.LancePositionStep7 = lanceStepsFrame[6].LancePositin;
            blowingSchemaEvent.O2FlowStep7 = lanceStepsFrame[6].O2Flow;
            blowingSchemaEvent.O2VolStep7 = lanceStepsFrame[6].O2Volume;
            blowingSchemaEvent.LancePositionStep8 = lanceStepsFrame[7].LancePositin;
            blowingSchemaEvent.O2FlowStep8 = lanceStepsFrame[7].O2Flow;
            blowingSchemaEvent.O2VolStep8 = lanceStepsFrame[7].O2Volume;
            blowingSchemaEvent.LancePositionStep9 = lanceStepsFrame[8].LancePositin;
            blowingSchemaEvent.O2FlowStep9 = lanceStepsFrame[8].O2Flow;
            blowingSchemaEvent.O2VolStep9 = lanceStepsFrame[8].O2Volume;
            blowingSchemaEvent.LancePositionStep10 = lanceStepsFrame[9].LancePositin;
            blowingSchemaEvent.O2FlowStep10 = lanceStepsFrame[9].O2Flow;
            blowingSchemaEvent.O2VolStep10 = lanceStepsFrame[9].O2Volume;
            blowingSchemaEvent.LancePositionStep11 = lanceStepsFrame[10].LancePositin;
            blowingSchemaEvent.O2FlowStep11 = lanceStepsFrame[10].O2Flow;
            blowingSchemaEvent.O2VolStep11 = lanceStepsFrame[10].O2Volume;
            blowingSchemaEvent.LancePositionStep12 = lanceStepsFrame[11].LancePositin;
            blowingSchemaEvent.O2FlowStep12 = lanceStepsFrame[11].O2Flow;
            blowingSchemaEvent.O2VolStep12 = lanceStepsFrame[11].O2Volume;
            blowingSchemaEvent.LancePositionStep13 = lanceStepsFrame[12].LancePositin;
            blowingSchemaEvent.O2FlowStep13 = lanceStepsFrame[12].O2Flow;
            blowingSchemaEvent.O2VolStep13 = lanceStepsFrame[12].O2Volume;
            blowingSchemaEvent.LancePositionStep14 = lanceStepsFrame[13].LancePositin;
            blowingSchemaEvent.O2FlowStep14 = lanceStepsFrame[13].O2Flow;
            blowingSchemaEvent.O2VolStep14 = lanceStepsFrame[13].O2Volume;
            blowingSchemaEvent.LancePositionStep15 = lanceStepsFrame[14].LancePositin;
            blowingSchemaEvent.O2FlowStep15 = lanceStepsFrame[14].O2Flow;
            blowingSchemaEvent.O2VolStep15 = lanceStepsFrame[14].O2Volume;
            blowingSchemaEvent.LancePositionStep16 = lanceStepsFrame[15].LancePositin;
            blowingSchemaEvent.O2FlowStep16 = lanceStepsFrame[15].O2Flow;
            blowingSchemaEvent.O2VolStep16 = lanceStepsFrame[15].O2Volume;
            blowingSchemaEvent.LancePositionStep17 = lanceStepsFrame[16].LancePositin;
            blowingSchemaEvent.O2FlowStep17 = lanceStepsFrame[16].O2Flow;
            blowingSchemaEvent.O2VolStep17 = lanceStepsFrame[16].O2Volume;
            blowingSchemaEvent.LancePositionStep18 = lanceStepsFrame[17].LancePositin;
            blowingSchemaEvent.O2FlowStep18 = lanceStepsFrame[17].O2Flow;
            blowingSchemaEvent.O2VolStep18 = lanceStepsFrame[17].O2Volume;
            blowingSchemaEvent.LancePositionStep19 = lanceStepsFrame[18].LancePositin;
            blowingSchemaEvent.O2FlowStep19 = lanceStepsFrame[18].O2Flow;
            blowingSchemaEvent.O2VolStep19 = lanceStepsFrame[18].O2Volume;
            blowingSchemaEvent.LancePositionStep20 = lanceStepsFrame[19].LancePositin;
            blowingSchemaEvent.O2FlowStep20 = lanceStepsFrame[19].O2Flow;
            blowingSchemaEvent.O2VolStep20 = lanceStepsFrame[19].O2Volume;

            m_pushGate.PushEvent(blowingSchemaEvent);
           // Logger.log("wwwwwwwwwwwwwwwwwww", "Processor", Logger.TypeMessage.terror);
            m_pushGate.PushEvent(new cntBlowingSchemaEvent());
            //Logger.log(lanceStepsFrame.ToString(), "ComSender", Logger.TypeMessage.important);
            //m_pushGate.PushEvent(new comO2FlowRateEvent());
           // Logger.log("ComSender - ok");
            return 0;
        }

        public static int ComSender(List<AdditionsQuant> additionsStepsFrame)
        {
            comAdditionsSchemaEvent comAdditionsSchema = new comAdditionsSchemaEvent();

            //порция 1
            comAdditionsSchema.Material1Portion1Weight = additionsStepsFrame[0].Addition[0].MaterialPortionWeight;  //материал 1
            comAdditionsSchema.O2VolPortion1Material1 = additionsStepsFrame[0].Addition[0].O2VolPortionMaterial;
            comAdditionsSchema.Material2Portion1Weight = additionsStepsFrame[0].Addition[1].MaterialPortionWeight;  //материал 2
            comAdditionsSchema.O2VolPortion1Material2 = additionsStepsFrame[0].Addition[1].O2VolPortionMaterial;
            comAdditionsSchema.Material3Portion1Weight = additionsStepsFrame[0].Addition[2].MaterialPortionWeight;  //материал 3
            comAdditionsSchema.O2VolPortion1Material3 = additionsStepsFrame[0].Addition[2].O2VolPortionMaterial;
            comAdditionsSchema.Material4Portion1Weight = additionsStepsFrame[0].Addition[3].MaterialPortionWeight;  //материал 4
            comAdditionsSchema.O2VolPortion1Material4 = additionsStepsFrame[0].Addition[3].O2VolPortionMaterial;
            comAdditionsSchema.Material5Portion1Weight = additionsStepsFrame[0].Addition[4].MaterialPortionWeight;  //материал 5
            comAdditionsSchema.O2VolPortion1Material5 = additionsStepsFrame[0].Addition[4].O2VolPortionMaterial;
            comAdditionsSchema.Material6Portion1Weight = additionsStepsFrame[0].Addition[5].MaterialPortionWeight;  //материал 6
            comAdditionsSchema.O2VolPortion1Material6 = additionsStepsFrame[0].Addition[5].O2VolPortionMaterial;
            comAdditionsSchema.Material7Portion1Weight = additionsStepsFrame[0].Addition[6].MaterialPortionWeight;  //материал 7
            comAdditionsSchema.O2VolPortion1Material7 = additionsStepsFrame[0].Addition[6].O2VolPortionMaterial;
            comAdditionsSchema.Material8Portion1Weight = additionsStepsFrame[0].Addition[7].MaterialPortionWeight;  //материал 8
            comAdditionsSchema.O2VolPortion1Material8 = additionsStepsFrame[0].Addition[7].O2VolPortionMaterial;
            comAdditionsSchema.Material9Portion1Weight = additionsStepsFrame[0].Addition[8].MaterialPortionWeight;  //материал 9
            comAdditionsSchema.O2VolPortion1Material9 = additionsStepsFrame[0].Addition[8].O2VolPortionMaterial;
            comAdditionsSchema.Material10Portion1Weight = additionsStepsFrame[0].Addition[9].MaterialPortionWeight; //материал 10
            comAdditionsSchema.O2VolPortion1Material10 = additionsStepsFrame[0].Addition[9].O2VolPortionMaterial;
            //порция 2
            comAdditionsSchema.Material1Portion2Weight = additionsStepsFrame[1].Addition[0].MaterialPortionWeight;  //материал 1
            comAdditionsSchema.O2VolPortion2Material1 = additionsStepsFrame[1].Addition[0].O2VolPortionMaterial;
            comAdditionsSchema.Material2Portion2Weight = additionsStepsFrame[1].Addition[1].MaterialPortionWeight;  //материал 2
            comAdditionsSchema.O2VolPortion2Material2 = additionsStepsFrame[1].Addition[1].O2VolPortionMaterial;
            comAdditionsSchema.Material3Portion2Weight = additionsStepsFrame[1].Addition[2].MaterialPortionWeight;  //материал 3
            comAdditionsSchema.O2VolPortion2Material3 = additionsStepsFrame[1].Addition[2].O2VolPortionMaterial;
            comAdditionsSchema.Material4Portion2Weight = additionsStepsFrame[1].Addition[3].MaterialPortionWeight;  //материал 4
            comAdditionsSchema.O2VolPortion2Material4 = additionsStepsFrame[1].Addition[3].O2VolPortionMaterial;
            comAdditionsSchema.Material5Portion2Weight = additionsStepsFrame[1].Addition[4].MaterialPortionWeight;  //материал 5
            comAdditionsSchema.O2VolPortion2Material5 = additionsStepsFrame[1].Addition[4].O2VolPortionMaterial;
            comAdditionsSchema.Material6Portion2Weight = additionsStepsFrame[1].Addition[5].MaterialPortionWeight;  //материал 6
            comAdditionsSchema.O2VolPortion2Material6 = additionsStepsFrame[1].Addition[5].O2VolPortionMaterial;
            comAdditionsSchema.Material7Portion2Weight = additionsStepsFrame[1].Addition[6].MaterialPortionWeight;  //материал 7
            comAdditionsSchema.O2VolPortion2Material7 = additionsStepsFrame[1].Addition[6].O2VolPortionMaterial;
            comAdditionsSchema.Material8Portion2Weight = additionsStepsFrame[1].Addition[7].MaterialPortionWeight;  //материал 8
            comAdditionsSchema.O2VolPortion2Material8 = additionsStepsFrame[1].Addition[7].O2VolPortionMaterial;
            comAdditionsSchema.Material9Portion2Weight = additionsStepsFrame[1].Addition[8].MaterialPortionWeight;  //материал 9
            comAdditionsSchema.O2VolPortion2Material9 = additionsStepsFrame[1].Addition[8].O2VolPortionMaterial;
            comAdditionsSchema.Material10Portion2Weight = additionsStepsFrame[1].Addition[9].MaterialPortionWeight; //материал 10
            comAdditionsSchema.O2VolPortion2Material10 = additionsStepsFrame[1].Addition[9].O2VolPortionMaterial;
            //порция 3
            comAdditionsSchema.Material1Portion3Weight = additionsStepsFrame[2].Addition[0].MaterialPortionWeight;  //материал 1
            comAdditionsSchema.O2VolPortion3Material1 = additionsStepsFrame[2].Addition[0].O2VolPortionMaterial;
            comAdditionsSchema.Material2Portion3Weight = additionsStepsFrame[2].Addition[1].MaterialPortionWeight;  //материал 2
            comAdditionsSchema.O2VolPortion3Material2 = additionsStepsFrame[2].Addition[1].O2VolPortionMaterial;
            comAdditionsSchema.Material3Portion3Weight = additionsStepsFrame[2].Addition[2].MaterialPortionWeight;  //материал 3
            comAdditionsSchema.O2VolPortion3Material3 = additionsStepsFrame[2].Addition[2].O2VolPortionMaterial;
            comAdditionsSchema.Material4Portion3Weight = additionsStepsFrame[2].Addition[3].MaterialPortionWeight;  //материал 4
            comAdditionsSchema.O2VolPortion3Material4 = additionsStepsFrame[2].Addition[3].O2VolPortionMaterial;
            comAdditionsSchema.Material5Portion3Weight = additionsStepsFrame[2].Addition[4].MaterialPortionWeight;  //материал 5
            comAdditionsSchema.O2VolPortion3Material5 = additionsStepsFrame[2].Addition[4].O2VolPortionMaterial;
            comAdditionsSchema.Material6Portion3Weight = additionsStepsFrame[2].Addition[5].MaterialPortionWeight;  //материал 6
            comAdditionsSchema.O2VolPortion3Material6 = additionsStepsFrame[2].Addition[5].O2VolPortionMaterial;
            comAdditionsSchema.Material7Portion3Weight = additionsStepsFrame[2].Addition[6].MaterialPortionWeight;  //материал 7
            comAdditionsSchema.O2VolPortion3Material7 = additionsStepsFrame[2].Addition[6].O2VolPortionMaterial;
            comAdditionsSchema.Material8Portion3Weight = additionsStepsFrame[2].Addition[7].MaterialPortionWeight;  //материал 8
            comAdditionsSchema.O2VolPortion3Material8 = additionsStepsFrame[2].Addition[7].O2VolPortionMaterial;
            comAdditionsSchema.Material9Portion3Weight = additionsStepsFrame[2].Addition[8].MaterialPortionWeight;  //материал 9
            comAdditionsSchema.O2VolPortion3Material9 = additionsStepsFrame[2].Addition[8].O2VolPortionMaterial;
            comAdditionsSchema.Material10Portion3Weight = additionsStepsFrame[2].Addition[9].MaterialPortionWeight; //материал 10
            comAdditionsSchema.O2VolPortion3Material10 = additionsStepsFrame[2].Addition[9].O2VolPortionMaterial;


            m_pushGate.PushEvent(comAdditionsSchema);
            m_pushGate.PushEvent(new cntAdditionsSchemaEvent());
            return 0;
        }
        public static int ComSender(AdditionsQuant additionsFrame)
        {
            comAdditionsSchemaEvent comAdditionsSchema = new comAdditionsSchemaEvent();

            //порция 1
            comAdditionsSchema.Material1Portion1Weight = additionsFrame.Addition[0].MaterialPortionWeight;  //материал 1
            comAdditionsSchema.O2VolPortion1Material1 = additionsFrame.Addition[0].O2VolPortionMaterial;
            comAdditionsSchema.Material2Portion1Weight = additionsFrame.Addition[1].MaterialPortionWeight;  //материал 2
            comAdditionsSchema.O2VolPortion1Material2 = additionsFrame.Addition[1].O2VolPortionMaterial;
            comAdditionsSchema.Material3Portion1Weight = additionsFrame.Addition[2].MaterialPortionWeight;  //материал 3
            comAdditionsSchema.O2VolPortion1Material3 = additionsFrame.Addition[2].O2VolPortionMaterial;
            comAdditionsSchema.Material4Portion1Weight = additionsFrame.Addition[3].MaterialPortionWeight;  //материал 4
            comAdditionsSchema.O2VolPortion1Material4 = additionsFrame.Addition[3].O2VolPortionMaterial;
            comAdditionsSchema.Material5Portion1Weight = additionsFrame.Addition[4].MaterialPortionWeight;  //материал 5
            comAdditionsSchema.O2VolPortion1Material5 = additionsFrame.Addition[4].O2VolPortionMaterial;
            comAdditionsSchema.Material6Portion1Weight = additionsFrame.Addition[5].MaterialPortionWeight;  //материал 6
            comAdditionsSchema.O2VolPortion1Material6 = additionsFrame.Addition[5].O2VolPortionMaterial;
            comAdditionsSchema.Material7Portion1Weight = additionsFrame.Addition[6].MaterialPortionWeight;  //материал 7
            comAdditionsSchema.O2VolPortion1Material7 = additionsFrame.Addition[6].O2VolPortionMaterial;
            comAdditionsSchema.Material8Portion1Weight = additionsFrame.Addition[7].MaterialPortionWeight;  //материал 8
            comAdditionsSchema.O2VolPortion1Material8 = additionsFrame.Addition[7].O2VolPortionMaterial;
            comAdditionsSchema.Material9Portion1Weight = additionsFrame.Addition[8].MaterialPortionWeight;  //материал 9
            comAdditionsSchema.O2VolPortion1Material9 = additionsFrame.Addition[8].O2VolPortionMaterial;
            comAdditionsSchema.Material10Portion1Weight = additionsFrame.Addition[9].MaterialPortionWeight; //материал 10
            comAdditionsSchema.O2VolPortion1Material10 = additionsFrame.Addition[9].O2VolPortionMaterial;

            m_pushGate.PushEvent(comAdditionsSchema);
            m_pushGate.PushEvent(new cntAdditionsSchemaEvent());
            return 0;
        }

        public static int ComSenderMaterialNames(SteelMakingPatternEvent smPatt)
        {
            comAdditionsEvent comANames = new comAdditionsEvent();
            for (int i = 0; i < smPatt.materialsName.Count; i++)
            {
                if (smPatt.materialsName[i] == null || smPatt.materialsName[i] == "")
                {
                    smPatt.materialsName[i] = "ИЗВЕСТ";
                }
                while (smPatt.materialsName[i].Length < 6)
                {
                    smPatt.materialsName[i] += " ";
                }
                if (smPatt.materialsName[i].Length > 6)
                {
                    //int extra = smPatt.materialsName[i].Length - 6;;
                    smPatt.materialsName[i] = smPatt.materialsName[i].Remove(6);

                }
            }
            
            comANames.Bunker1MaterialName = smPatt.materialsName[0];
            Logger.log("Material name loaded = " + smPatt.materialsName[0],"Material 1", Logger.TypeMessage.normal);
            comANames.Bunker2MaterialName = smPatt.materialsName[1];
            Logger.log("Material name loaded = " + smPatt.materialsName[1], "Material 2", Logger.TypeMessage.normal);
            comANames.Bunker3MaterialName = smPatt.materialsName[2];
            Logger.log("Material name loaded = " + smPatt.materialsName[2], "Material 3", Logger.TypeMessage.normal);
            comANames.Bunker4MaterialName = smPatt.materialsName[3];
            Logger.log("Material name loaded = " + smPatt.materialsName[3], "Material 4", Logger.TypeMessage.normal);
            comANames.Bunker5MaterialName = smPatt.materialsName[4];
            Logger.log("Material name loaded = " + smPatt.materialsName[4], "Material 5", Logger.TypeMessage.normal);
            comANames.Bunker6MaterialName = smPatt.materialsName[5];
            Logger.log("Material name loaded = " + smPatt.materialsName[5], "Material 6", Logger.TypeMessage.normal);
            comANames.Bunker7MaterialName = smPatt.materialsName[6];
            Logger.log("Material name loaded = " + smPatt.materialsName[6], "Material 7", Logger.TypeMessage.normal);
            comANames.Bunker8MaterialName = smPatt.materialsName[7];
            Logger.log("Material name loaded = " + smPatt.materialsName[7], "Material 8", Logger.TypeMessage.normal);
            comANames.Bunker9MaterialName = smPatt.materialsName[8];
            Logger.log("Material name loaded = " + smPatt.materialsName[8], "Material 9", Logger.TypeMessage.normal);
            comANames.Bunker10MaterialName = smPatt.materialsName[9];
            Logger.log("Material name loaded = " + smPatt.materialsName[9], "Material 10", Logger.TypeMessage.normal);

            m_pushGate.PushEvent(comANames);
            m_pushGate.PushEvent(new cntAdditionsEvent());
            return 0;
        }

        public static int LanceGetFrameNumber() // возвращает номер текущего кадра для фурмы
        {
            int reminder = 0;
            return Math.DivRem(m_oxigenCurrentStep, LanceMaxStepsFrame, out reminder);
        }

        public static int AdditionsGetFrameNumber() // возвращает номер текущего кадра для добавок
        {
            int reminder = 0;
            return Math.DivRem(m_oxigenCurrentStep, AdditionsMaxStepsFrame, out reminder);
        }

        public static List<AdditionsQuant> AdditionsTableCompressor() // уплотнитель структуры данных для добавок
        {
            //int reminder = 0;
            //return Math.DivRem(m_oxigenCurrentStep, AdditionsMaxStepsFrame, out reminder);
            //Logger.log("WWWWWWWWWWWWWWWWWWWWW", "Processor", Logger.TypeMessage.terror);

            List < AdditionsQuant > additionsQuantList = new List<AdditionsQuant>(); 
     /*       bool itemCreated = false;
            bool keep = false;
            int skipped = 0;
            for (int step = 0; step < SmPattern.steps.Count; step++ )
            {
               // Logger.log("--" + step.ToString(), "Processor", Logger.TypeMessage.terror);
                itemCreated = false;
                keep = false;
                //Logger.log("qqqqqqqqqqqqqqqqqqq", "Processor", Logger.TypeMessage.terror);
                for (int material = 0; material < SmPattern.steps[step].additions.addition.Count; material++)
                {
                    //Logger.log("step - " + step.ToString() + " material - " + material.ToString() + " | SmPattern.steps.Count - " + SmPattern.steps.Count.ToString() + " addition.Count - " + SmPattern.steps[step].additions.addition.Count.ToString() + " additionsQuantList[step].Addition - " + additionsQuantList[step].Addition.Count.ToString(), "Processor", Logger.TypeMessage.terror);
                    if(SmPattern.steps[step].additions.addition[material].MaterialPortionWeight > 0)
                    {
                        keep = true;
                        if(!itemCreated)
                        {
                            additionsQuantList.Add(new AdditionsQuant());
                            itemCreated = true;
                        }
                       // Logger.log("step - " + step.ToString() + " skipped - " + skipped.ToString() + " material - " + material.ToString(), "Processor", Logger.TypeMessage.terror);

                      //  Logger.log("--" + step.ToString(), "Processor", Logger.TypeMessage.terror);
                       // Logger.log(additionsQuantList[step-1].Addition.Count.ToString(), "Processor", Logger.TypeMessage.terror);
                        //additionsQuantList[step].Addition[0].MaterialPortionWeight = 0;
                        additionsQuantList[step - skipped].Addition[material].MaterialPortionWeight =
                            SmPattern.steps[step].additions.addition[material].MaterialPortionWeight;

                        additionsQuantList[step - skipped].Addition[material].O2VolPortionMaterial =
                            SmPattern.steps[step].O2Volume;
                     //   if (step < 3 && material == 0)
                       // {
                         //   Logger.log(">>> " + SmPattern.steps[step].additions.addition[0].ToString(), "Processor", Logger.TypeMessage.terror);
                        //}
                    }
                    
                }
                if (!keep)
                {
                    skipped++;
                }
                
            }*/
            additionsQuantList.Add(new AdditionsQuant());
            int stepTo = 0;
            for (int material = 0; material < 10; material++)
            {
                stepTo = 0;
                for (int stepFrom = 0; stepFrom < SmPattern.steps.Count; stepFrom++)
                {
                    if (SmPattern.steps[stepFrom].additions.addition[material].MaterialPortionWeight > 0)
                    {
                        if(stepFrom > additionsQuantList.Count)
                        {
                            additionsQuantList.Add(new AdditionsQuant());
                        }
                        //Logger.log("from " + stepFrom.ToString() + " to " + stepTo.ToString() + " max " + additionsQuantList.Count.ToString(), "Processor", Logger.TypeMessage.terror);
                       // Logger.log(" -- " + SmPattern.steps[stepFrom].additions.addition[material].MaterialPortionWeight.ToString(), "Processor", Logger.TypeMessage.terror);
                        additionsQuantList[stepTo].Addition[material].MaterialPortionWeight =
                            SmPattern.steps[stepFrom].additions.addition[material].MaterialPortionWeight;

                        additionsQuantList[stepTo].Addition[material].O2VolPortionMaterial =
                            SmPattern.steps[stepFrom].O2Volume;
                        stepTo++;
                    }
                }
            }

                // Logger.log(SmPattern.steps[0].ToString(), "Processor", Logger.TypeMessage.terror);
                // Logger.log(additionsQuantList[70].Addition.Count.ToString(), "Processor", Logger.TypeMessage.terror);
                //Logger.log("additionsQuantList - " + additionsQuantList.Count.ToString() + " SmPattern - " + SmPattern.steps.Count.ToString(), "Processor", Logger.TypeMessage.terror);
                //SmPattern;
                return additionsQuantList; 
        }
        private static int LanceCurrentStep()
        {
            int currentStep = 0;
          //  Logger.log("tep max" + SmPattern.steps.Count);
           // while (SmPattern.steps[currentStep].O2Volume < m_oxigenCurrent)
            while (true)
            {

                if (currentStep >= SmPattern.steps.Count)
                {
                    //currentStep--;
                    return -1;
                }
                else
                {
                    if (SmPattern.steps[currentStep].O2Volume == null || SmPattern.steps[currentStep].O2Volume == -1)
                    {
                        currentStep--;
                        return -1;
                    }

                    if (!(SmPattern.steps[currentStep].O2Volume < m_oxigenCurrent))
                    {
                        //currentStep--;
                        return currentStep;
                    }
                    currentStep++;
                }
                
           // Logger.log("s - " + currentStep + " O2 - " + m_oxigenCurrent + " O2vol - " + SmPattern.steps[currentStep].O2Volume);
            }
            return currentStep;
        }

        private static void ListenThread()
        {
            var o = new HeatChangeEvent();
            m_listenGate = new ConnectionProvider.Client(new Listener());
            m_listenGate.Subscribe();
           // Logger.log("Listener subscribe");
        }
    }
}
