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
       /* private static int AdditionsQuantRefrash()
        {
            for (int step = 0; step < AdditionsQuantList.Count; step++)
            {
                for (int material = 0; material < AdditionsQuantList[step].Addition.Count; material++)
                {
                    if (m_oxigenCurrent > AdditionsQuantList[step].Addition[material].O2VolPortionMaterial)
                    {
                        if (AdditionsQuantList[step].Addition[material].O2VolPortionMaterial > 0)
                        {
                            ComSender(AdditionsQuantizerFr());                        //если нашли выполненный шаг то обновляем кадр
                        }
                    }
                }
            }
            return 0;
        }
        */
        public static int LanceGetFrameNumber()                                       // возвращает номер текущего кадра для фурмы
        {
            int reminder = 0;
            return Math.DivRem(m_oxigenCurrentStep, LanceMaxStepsFrame, out reminder);
        }

        public static int AdditionsGetFrameNumber()                                   // возвращает номер текущего кадра для добавок
        {
            int reminder = 0;
            return Math.DivRem(m_oxigenCurrentStep, AdditionsMaxStepsFrame, out reminder);
        }

       /* public static List<AdditionsQuant> AdditionsTableCompressor()                 // уплотнитель структуры данных для добавок
        {
            List<AdditionsQuant> additionsQuantList = new List<AdditionsQuant>();

            additionsQuantList.Add(new AdditionsQuant());
            int stepTo = 0;
            for (int material = 0; material < 10; material++)
            {
                stepTo = 0;
                for (int stepFrom = 0; stepFrom < SmPattern.steps.Count; stepFrom++)
                {
                    if (SmPattern.steps[stepFrom].additions.addition[material].MaterialPortionWeight > 0)
                    {
                        if (stepFrom > additionsQuantList.Count)
                        {
                            additionsQuantList.Add(new AdditionsQuant());
                        }
                        additionsQuantList[stepTo].Addition[material].MaterialPortionWeight =
                            SmPattern.steps[stepFrom].additions.addition[material].MaterialPortionWeight;
                        additionsQuantList[stepTo].Addition[material].O2VolPortionMaterial =
                            SmPattern.steps[stepFrom].O2Volume;
                        stepTo++;
                    }
                }
            }
            return additionsQuantList;
        }
        */
        public static List<StepWeigherQuant> WeigherTableCompressor()                 // уплотнитель структуры данных для весов
        {
            List<StepWeigherQuant> stepsWeigherQuant = new List<StepWeigherQuant>();
            List<StepWeigherQuant> stepsWeigherQuantforSort = new List<StepWeigherQuant>();

            stepsWeigherQuantforSort.Add(new StepWeigherQuant());
            int weigherCount = stepsWeigherQuantforSort[0].weigherQuant.Count;
            
            for (int weigher = 0; weigher < weigherCount; weigher++)
            {
                for (int step = 0; step < SmPattern.steps.Count; step++)
                {
                    if (step >= stepsWeigherQuantforSort.Count)
                    {
                        stepsWeigherQuantforSort.Add(new StepWeigherQuant());
                    }
                    stepsWeigherQuantforSort[step].weigherQuant[weigher].PortionWeight = SmPattern.steps[step].weigherLines[weigher].PortionWeight;
                    stepsWeigherQuantforSort[step].weigherQuant[weigher].BunkerId = SmPattern.steps[step].weigherLines[weigher].BunkerId;
                    stepsWeigherQuantforSort[step].weigherQuant[weigher].OxygenTreshold = SmPattern.steps[step].O2Volume;

                    stepsWeigherQuantforSort[step].weigherQuant[weigher].AllowToAdd = SmPattern.steps[step].weigherLines[weigher].AllowToAdd;
                    stepsWeigherQuantforSort[step].weigherQuant[weigher].NotToGive = SmPattern.steps[step].weigherLines[weigher].NotToGive;
                }
            }
            stepsWeigherQuantforSort = SortStepsWeigherQuant(stepsWeigherQuantforSort); // сортируем для каждых весов    
                
            
            stepsWeigherQuant.Add(new StepWeigherQuant());
            weigherCount = stepsWeigherQuant[0].weigherQuant.Count;

            for (int weigher = 0; weigher < weigherCount; weigher++)
            {
                int stepTo = 0;
                for (int stepFrom = 0; stepFrom < stepsWeigherQuantforSort.Count; stepFrom++)
                {
                    // Logger.log(SmPattern.steps[stepFrom].weigherLines[weigher].PortionWeight.ToString());
                    if (stepsWeigherQuantforSort[stepFrom].weigherQuant[weigher].PortionWeight > 0)
                    {
                        if (stepTo >= stepsWeigherQuant.Count)
                        {
                            stepsWeigherQuant.Add(new StepWeigherQuant());
                        }
                        stepsWeigherQuant[stepTo].weigherQuant[weigher] =
                            stepsWeigherQuantforSort[stepFrom].weigherQuant[weigher];
                        stepTo++;
                    }

                }
            }
            return stepsWeigherQuant;
        }

        public static List<StepWeigherQuant> SortStepsWeigherQuant(List<StepWeigherQuant> stepsWeigherQuant)
        {
            List<WeigherQuant> weigherQuantList;
            int weigherCount = stepsWeigherQuant[0].weigherQuant.Count;
            for (int weigher = 0; weigher < weigherCount; weigher++)
            {
                weigherQuantList = new List<WeigherQuant>();
                for (int step = 0; step < stepsWeigherQuant.Count; step++)
                {
                    weigherQuantList.Add(stepsWeigherQuant[step].weigherQuant[weigher]);
                }
                weigherQuantList.Sort();
                for (int step = 0; step < stepsWeigherQuant.Count; step++)
                {
                    stepsWeigherQuant[step].weigherQuant[weigher] = weigherQuantList[step];
                }
            }
            return stepsWeigherQuant;
        }

        private static int LanceCurrentStep()
        {
            int currentStep = 0;

            while (true)
            {

                if (currentStep >= SmPattern.steps.Count)
                {
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
                        return currentStep;
                    }
                    currentStep++;
                }
            }
            return currentStep;
        }

        public static void JobAllowToAddRefrash()// проверяем задания на добавку для весов
        {
            bool weigherJob = false;
            for (int weigher = 0; weigher < WeightCounter; weigher++) 
            {
                if (m_jobAllowToAdd[weigher] > 0)
                {
                    if ((m_jobAllowToAdd[weigher] <= m_oxigenCurrent) && m_weightCurrentSteps[weigher].GetCurrentStepCompleteStatus())
                    {
                        m_weightCurrentSteps[weigher].Increase();
                        weigherJob = true;
                        m_jobAllowToAdd[weigher] = -1; // !!! проверить, возможно из-за отсутствия обнуления наблюдается бага с навесками
                    }
                }
            }
            if (weigherJob)
            {
                SenderWeigherLoadMaterial(WeigherQuantizer());
            }
        }

        /// <summary>
        /// Конвертирует int в bool, если integer > 0 возвращает True
        /// </summary>
        /// <param name="?"></param>
        /// <param name="value"> целое число </param>
        /// <returns></returns>
        public static bool ConvertIntToBool(int value)
        {
            if (value > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Конвертирует bool в int, если True возвращает 1
        /// </summary>
        /// <param name="?"></param>
        /// <param name="value"> бул </param>
        /// <returns></returns>
        public static int ConvertBoolToInt(bool value)
        {
            if (value)
            {
                return 1;
            }
            return 0;
        }
        private static void ListenThread()
        {
            var o = new HeatChangeEvent();
            m_listenGate = new ConnectionProvider.Client(new Listener());
            m_listenGate.Subscribe();
        }

        private static void KeepAliveThread()
        {
            while (true)
            {
                if (m_pushGate != null)
                {
                    m_pushGate.PushEvent(new TestEvent());
                    Console.Write(".");
                }
                else
                {
                    Console.Write("#");
                }
                Thread.Sleep(KeepAlivePeriod);
            }
        }
    }
}