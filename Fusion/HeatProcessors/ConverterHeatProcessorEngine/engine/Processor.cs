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
        public static int Processor(BaseEvent newEvent)
        {
            if (newEvent is HeatChangeEvent)
            {
                var hce = newEvent as HeatChangeEvent;
                if (m_heatNumber != hce.HeatNumber)
                {
                    SafeInit();
                    m_heatNumber = hce.HeatNumber;
                }
            }

            if (newEvent is LanceEvent)
            {
                var le = newEvent as LanceEvent;
                m_lanceHeight = le.LanceHeight;
            }

            if (newEvent is BlowingEvent)
            {
                var newBE = newEvent as BlowingEvent;

                if (m_oxigenCurrent != newBE.O2TotalVol && HeatOn && m_dataAvailable)
                {
                    m_oxigenCurrent = newBE.O2TotalVol;

                   // Logger.log(String.Format("current oxygen = {0}", m_oxigenCurrent),"receive current oxygen", Logger.TypeMessage.unimportant);
                    
                    
                    JobAllowToAddRefrash(); // проверяем задания на добавку для весов
                    

                    int cs = LanceCurrentStep();

                    var messag = String.Format("steep-{0} frame-{1} O2Current-{2} O2-{3} heigth-{4} flow-{5}",
                        m_oxigenCurrentStep,
                        m_lanceCurrentFrame,
                        m_oxigenCurrent,
                        SmPattern.steps[m_oxigenCurrentStep].O2Volume,
                        SmPattern.steps[m_oxigenCurrentStep].lance.LancePositin,
                        SmPattern.steps[m_oxigenCurrentStep].lance.O2Flow
                        );
                    InstantLogger.log(messag, "Processing", InstantLogger.TypeMessage.unimportant);
                    //InstantLogger.log(
                    //            "step-" + m_oxigenCurrentStep.ToString() + " frame-" +
                    //            m_lanceCurrentFrame.ToString() +
                    //            " m_oxigenCurrent -" + m_oxigenCurrent.ToString() + " o2-" +
                    //            SmPattern.steps[m_oxigenCurrentStep].O2Volume.ToString(), "Processing",
                    //            InstantLogger.TypeMessage.unimportant);

                    if (m_oxigenCurrentStep != cs)
                    {
                        m_oxigenCurrentStep = cs;
                        SenderCurrentStep(m_oxigenCurrentStep);

                        //  m_oxigenCurrentStep = LanceCurrentStep();
                        //  SenderCurrentStep(m_oxigenCurrentStep);

                        if (m_oxigenCurrentStep != -1)
                        {
                            InstantLogger.log(
                                "step - " + m_oxigenCurrentStep.ToString() + " frame - " +
                                m_lanceCurrentFrame.ToString() +
                                " m_oxigenCurrent  - " + m_oxigenCurrent.ToString() + " o2 - " +
                                SmPattern.steps[m_oxigenCurrentStep].O2Volume.ToString(), "Processing",
                                InstantLogger.TypeMessage.important);
                        }
                        else
                        {
                            InstantLogger.log(
                                "step - " + m_oxigenCurrentStep.ToString() + " frame - " +
                                m_lanceCurrentFrame.ToString() +
                                " m_oxigenCurrent  - " + m_oxigenCurrent.ToString()
                                , "Process complete",
                                InstantLogger.TypeMessage.caution);
                            m_dataAvailable = false;
                        }

                        if (m_lanceCurrentFrame != LanceGetFrameNumber() && m_oxigenCurrentStep != -1)
                        {
                           ComSender(LanceQuantizer()); // посылаем новое задание для фурмы
                            // LanceQuantizer();
                        }
                    }
                    //AdditionsQuantRefrash();                                          // посылаем новое задание для вертикального тракта
                    if (cs < 0) // если конец плавки обнуляем
                    {
                        //SafeInit(); // может перетирать задание окончания продувки
                    }
                    
                }
            }
            if (newEvent is SteelMakingPatternEvent)
            {
                var newSMPE = newEvent as SteelMakingPatternEvent;
                // сделать блок проверки корректности данных
                // здесь должен быть блок сравнения SmPattern с newSMPE 
                SmPattern = newSMPE;
                m_dataAvailable = true;                                               // данные пришли, начинем плавку
                if (HeatOn)
                {
                    ComSender(LanceQuantizer());                                      // шлем задание для фурмы
                   // LanceQuantizer();
                    //ComSenderMaterialNames(SmPattern);                                // шлем названия материалов
                    //ComSender(AdditionsQuantizerFr());                                // шлем задание для вертикального тракта --- отменено

                    SenderWeigherLoadMaterial(WeigherQuantizer());   // задание для вертикального тракта
                    //SenderWeigherLoadMaterial(WeigherQuantizerWE());   // задание для вертикального тракта с учетом пустых весов
                    SetControlMode(true); // отпираем визуху


                }
            }
            
            if (newEvent is WeighersStateEvent) // увеличение шага по освобождению весов
            {
                var newWSE = newEvent as WeighersStateEvent;
                for (int weigher = 0; weigher < m_weighersState.Count; weigher++)
                {
                    if (m_weighersState[weigher].GetState() != m_weighersStatePrevious[weigher].GetState())
                    {
                        m_weighersStatePrevious[weigher].WeigherEmpty = m_weighersState[weigher].WeigherEmpty;
                        m_weighersStatePrevious[weigher].WeigherLoadFree = m_weighersState[weigher].WeigherLoadFree;
                        m_weighersStatePrevious[weigher].WeigherUnLoadFree = m_weighersState[weigher].WeigherUnLoadFree;
                        m_weighersStatePrevious[weigher].Actual = m_weighersState[weigher].Actual;
                    }
                }
                //if (m_weighersStatePrevious != null && m_weighersStatePrevious != m_weighersState)
                //{
                //    m_weighersStatePrevious = m_weighersState; // сохраняем предыдущее значение
                //}

                int weigherCnt = 0; // W3
                m_weighersState[weigherCnt].WeigherEmpty = ConvertIntToBool(newWSE.Weigher3Empty);
                m_weighersState[weigherCnt].WeigherLoadFree = ConvertIntToBool(newWSE.Weigher3LoadFree);
                m_weighersState[weigherCnt].WeigherUnLoadFree = ConvertIntToBool(newWSE.Weigher3UnLoadFree);
                m_weighersState[weigherCnt].Actual = true;
                weigherCnt = 1; // W4
                m_weighersState[weigherCnt].WeigherEmpty = ConvertIntToBool(newWSE.Weigher4Empty);
                m_weighersState[weigherCnt].WeigherLoadFree = ConvertIntToBool(newWSE.Weigher4LoadFree);
                m_weighersState[weigherCnt].WeigherUnLoadFree = ConvertIntToBool(newWSE.Weigher4UnLoadFree);
                m_weighersState[weigherCnt].Actual = true;
                weigherCnt = 2; // W5
                m_weighersState[weigherCnt].WeigherEmpty = ConvertIntToBool(newWSE.Weigher5Empty);
                m_weighersState[weigherCnt].WeigherLoadFree = ConvertIntToBool(newWSE.Weigher5LoadFree);
                m_weighersState[weigherCnt].WeigherUnLoadFree = ConvertIntToBool(newWSE.Weigher5UnLoadFree);
                m_weighersState[weigherCnt].Actual = true;
                weigherCnt = 3; // W6
                m_weighersState[weigherCnt].WeigherEmpty = ConvertIntToBool(newWSE.Weigher6Empty);
                m_weighersState[weigherCnt].WeigherLoadFree = ConvertIntToBool(newWSE.Weigher6LoadFree);
                m_weighersState[weigherCnt].WeigherUnLoadFree = ConvertIntToBool(newWSE.Weigher6UnLoadFree);
                m_weighersState[weigherCnt].Actual = true;
                weigherCnt = 4; // W7
                m_weighersState[weigherCnt].WeigherEmpty = ConvertIntToBool(newWSE.Weigher7Empty);
                m_weighersState[weigherCnt].WeigherLoadFree = ConvertIntToBool(newWSE.Weigher7LoadFree);
                m_weighersState[weigherCnt].WeigherUnLoadFree = ConvertIntToBool(newWSE.Weigher7UnLoadFree);
                m_weighersState[weigherCnt].Actual = true;

                if (m_dataAvailable)
                {
                    for (int weigher = 0; weigher < WeightCounter; weigher++)
                    {
                        if ((m_weighersState[weigher].GetState() == WeigherState.State.Empty) && (m_weighersState[weigher].GetState() != m_weighersStatePrevious[weigher].GetState())) // весы пусты и состояние изменилось
                        {
                            m_weightCurrentSteps[weigher].Increase();
                            InstantLogger.msg("Weigher{0} empty", weigher);
                        }
                        if (m_weighersState[weigher].GetState() == WeigherState.State.Unload)
                        {
                            if (m_releaseWeighersState[weigher]) // выгрузка пошла из-за нажатия кнопки
                            {
                                m_releaseWeighersState[weigher] = false;
                                ComSendOxygenMode(weigher, false);
                                // переключаемся на реальный кислород - данная схема действительна пока работает по реальному кислороду
                                // при переходе на виртуальный кислород здесь нужно будет востанавливать значение виртуального кислорода
                                // которое было до нажатия кнопки
                            }
                        }
                    }

                    SenderWeigherLoadMaterial(WeigherQuantizer());
                    //SenderWeigherLoadMaterial(WeigherQuantizerWE());   // задание для вертикального тракта с учетом пустых весов
                }
            }

            if (newEvent is ReleaseWeigherEvent) // реакция на событие "Освободить весы"
            {
                // m_releaseWeigherState
                var rw = newEvent as ReleaseWeigherEvent;
               for (int weigher = 0; weigher < WeightCounter; weigher++)
               {
                   if ((rw.WeigherId == weigher) && ((m_weighersState[weigher].GetState() == WeigherState.State.Full) || (m_weighersState[weigher].GetState() == WeigherState.State.Load)))
                   {
                       m_releaseWeighersState[weigher] = true;
                       ComSendOxygenMode(weigher, true); // переключиться на виртуальный кислород пока он 32767
                       // при переходе на виртуальный кислород здесь нужно будет запоминать текущиее значение виртуального кислорода
                       // подсовывать новое значение после 30 000 и потом где нужно возвращать значение предыдущее
                       InstantLogger.msg("Release Weigher{0} Event", weigher);
                   }
               }
            }
            return 0;
        }
    }
}