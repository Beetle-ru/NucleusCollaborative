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
    public static partial class HeatEngine
    {
        private static List<LanceQuant> LanceQuantizer()
        {
           // Logger.log(SmPattern.steps[0].O2Volume.ToString());
            int frameStart = 0;

            frameStart = LanceGetFrameNumber();                                        //определяем номер кадра
            m_lanceCurrentFrame = frameStart;
            frameStart *= LanceMaxStepsFrame;                                          //определяем начало кадра

            List<LanceQuant> LanceStepsFrame = new List<LanceQuant>();

            for (int step = 0; step < LanceMaxStepsFrame; step++)                     //выборка MaxStepsFrame шагов
            {
                int shiftStep = 0;
                shiftStep = frameStart + step;
                LanceStepsFrame.Add(new LanceQuant());
                if (shiftStep < SmPattern.steps.Count)
                {
                    LanceStepsFrame[step].O2Volume = SmPattern.steps[shiftStep].O2Volume;
                    LanceStepsFrame[step].O2Flow = SmPattern.steps[shiftStep].lance.O2Flow;
                    LanceStepsFrame[step].LancePositin = SmPattern.steps[shiftStep].lance.LancePositin;
                }
                else
                {
                    LanceStepsFrame[step] = new LanceQuant();                         // забиваем -1, которые присваиваются в конструкторе
                }
            }
            return LanceStepsFrame;
        }
       /* private static List<AdditionsQuant> AdditionsQuantizer()
        {
            int frameStart = 0;
            AdditionsQuantList = AdditionsTableCompressor();                          // заполняем квант лист новыми данными

            frameStart = AdditionsGetFrameNumber();
            m_additionsCurrentFrame = frameStart;
            frameStart *= AdditionsMaxStepsFrame;                                     //определяем начало кадра

            List<AdditionsQuant> additionsStepsFrame = new List<AdditionsQuant>();

            for (int step = 0; step < AdditionsMaxStepsFrame; step++)                 //выборка MaxStepsFrame шагов
            {
                int shiftStep = 0;
                shiftStep = frameStart + step;
                additionsStepsFrame.Add(new AdditionsQuant());
                if (shiftStep < AdditionsQuantList.Count)
                {
                    additionsStepsFrame[step] = AdditionsQuantList[step];
                }
                else
                {
                    additionsStepsFrame[step] = new AdditionsQuant();                 // забиваем -1, которые присваиваются в конструкторе
                }
            }
            return additionsStepsFrame;
        }
        */
    /*    private static AdditionsQuant AdditionsQuantizerFr()
        {
            AdditionsQuant additionsFrame = new AdditionsQuant();

            AdditionsQuantList = AdditionsTableCompressor();

            for (int material = 0; material < AdditionsQuantList[0].Addition.Count; material++)
            {
                for (int step = 0; step < AdditionsQuantList.Count; step++)
                {
                    if (m_oxigenCurrent > AdditionsQuantList[step].Addition[material].O2VolPortionMaterial)
                    {
                        //заполняем -1 чтобы выполненные шаги не мешались
                        AdditionsQuantList[step].Addition[material].O2VolPortionMaterial = -1; 
                        AdditionsQuantList[step].Addition[material].MaterialPortionWeight = -1;
                        //выполненный шаг
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
        }*/
        private static List<WeigherQuant> WeigherQuantizer() // возвращает лист потому, что может быть задание одновременно для нескольких весов
        {
            var weigherQuant = new List<WeigherQuant>();

            //var stepsWeigherQuant = new List<StepWeigherQuant>();
            var stepsWeigherQuant = WeigherTableCompressor();                        // получаем подготовленную структуру шагов
            
            for (int i = 0; i < WeightCounter; i++)
            {
                weigherQuant.Add(new WeigherQuant());
            }
            bool endJob = false;
            for (int weigh = 0; weigh < WeightCounter; weigh++)
            {
                if (!m_weightCurrentSteps[weigh].GetCurrentStepCompleteStatus())
                {
                   // Logger.log(m_weightCurrentSteps[weigh].GetCurrentStep().ToString() + "  " + stepsWeigherQuant.Count.ToString());
                    if (m_weightCurrentSteps[weigh].GetCurrentStep() < stepsWeigherQuant.Count)
                    {
                       // Logger.log("wwwwwwwwwwwww");
                        weigherQuant[weigh] =
                            stepsWeigherQuant[m_weightCurrentSteps[weigh].GetCurrentStep()].weigherQuant[weigh];
                       
                        if (weigherQuant[weigh].PortionWeight > 0) // если реально что-то нагрузили
                        {
                            m_weightCurrentSteps[weigh].SetCurrentStepCompleteStatus(true);// метим текущий шаг как отданный(завершенный) !!!
                        }
                        
                        if (weigherQuant[weigh].AllowToAdd) // если стоит флаг разрешено добавлять, то устанавливаем кислород которого не достигнем за всю плавку
                        {
                            m_jobAllowToAdd[weigh] = weigherQuant[weigh].OxygenTreshold; // запоминаем кислород на шаг
                            weigherQuant[weigh].OxygenTreshold = m_startAllowToAdd + m_counterAllowToAdd;
                            m_counterAllowToAdd++;
                        }
                        else
                        {
                            if (weigherQuant[weigh].NotToGive)
                                // если стоит флаг не отдавать, то устанавливаем кислород которого не достигнем за всю плавку
                            {
                                weigherQuant[weigh].OxygenTreshold = m_startNotToGive + m_counterNotToGive;
                                m_counterNotToGive++;
                            }
                        }

                    }
                    else
                    {
                        endJob = true;
                    }
                }
            }
            if (endJob)
            {
                InstantLogger.log("For Weighers", "End of job", InstantLogger.TypeMessage.unimportant);
            }
            
            return weigherQuant;
        }

        private static List<WeigherQuant> WeigherQuantizerWE() // возвращает квант только если весы свободны
        {
            var weigherQuant = new List<WeigherQuant>();

            var stepsWeigherQuant = WeigherTableCompressor();                        // получаем подготовленную структуру шагов

            for (int i = 0; i < WeightCounter; i++)
            {
                weigherQuant.Add(new WeigherQuant());
            }
            bool endJob = false;
            for (int weigher = 0; weigher < WeightCounter; weigher++)
            {
                if (m_weighersState[weigher].GetState() == WeigherState.State.Empty)
                {
                    if (!m_weightCurrentSteps[weigher].GetCurrentStepCompleteStatus())
                    {
                        if (m_weightCurrentSteps[weigher].GetCurrentStep() < stepsWeigherQuant.Count)
                        {
                            weigherQuant[weigher] =
                                stepsWeigherQuant[m_weightCurrentSteps[weigher].GetCurrentStep()].weigherQuant[weigher];
                            m_weightCurrentSteps[weigher].SetCurrentStepCompleteStatus(true);
                                // метим текущий шаг как отданный(завершенный)
                            if (weigherQuant[weigher].AllowToAdd)
                                // если стоит флаг разрешено добавлять, то устанавливаем кислород которого не достигнем за всю плавку
                            {
                                m_jobAllowToAdd[weigher] = weigherQuant[weigher].OxygenTreshold;
                                    // запоминаем кислород на шаг
                                weigherQuant[weigher].OxygenTreshold = m_startAllowToAdd + m_counterAllowToAdd;
                                m_counterAllowToAdd++;
                            }
                            else
                            {
                                if (weigherQuant[weigher].NotToGive)
                                    // если стоит флаг не отдавать, то устанавливаем кислород которого не достигнем за всю плавку
                                {
                                    weigherQuant[weigher].OxygenTreshold = m_startNotToGive + m_counterNotToGive;
                                    m_counterNotToGive++;
                                }
                            }

                        }
                        else
                        {
                            endJob = true;
                        }
                    }
                }
            }
            if (endJob)
            {
                InstantLogger.log("For Weighers", "End of job", InstantLogger.TypeMessage.unimportant);
            }

            return weigherQuant;
        }
    }
}