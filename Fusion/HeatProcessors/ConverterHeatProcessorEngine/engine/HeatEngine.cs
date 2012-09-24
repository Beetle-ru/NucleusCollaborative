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
    
    static public partial class HeatEngine
    {
        public static bool HeatOn { set; get; }
        private static bool m_dataAvailable;
        private static int m_oxigenTotal;
        private static int m_oxigenCurrent;
        private static int m_oxigenCurrentStep;
        private static int m_lanceCurrentFrame;
        private static int m_additionsCurrentFrame;
        private static SteelMakingPatternEvent SmPattern { set; get; }                // шаблон хранимый в классе 
        static List<AdditionsQuant> AdditionsQuantList { set; get; }                  // уплотненная структура данных по добавокам
        private static ConnectionProvider.Client m_pushGate;
        private static ConnectionProvider.Client m_listenGate;
        private const int LanceMaxStepsFrame = 20;                                    // количество шагов в кадре для фурмы по умолчанию 20
        private const int AdditionsMaxStepsFrame = 3;                                 // количество шагов в кадре для добавок по умолчанию 3
        private const int WeightCounter = 5;                                          // количество весов - 5 шт
        private const int DelayRefrashData = 500;                                     // задержка при обновлении данных ms
        private const int KeepAlivePeriod = 60000;                                     // период посылов KeepAlive ms
        private static List<WatchSteps> m_weightCurrentSteps;                         // счетчики текущего шага для каждых весов
        private static int m_counterNotToGive;                                        // счетчик NotToGive за плавку
        private static int m_counterAllowToAdd;                                       // счетчик AllowToAdd за плавку
        private const int m_startNotToGive = 30000;                                   // значение с которого пишем кислород в случае NotToGive
        private const int m_startAllowToAdd = 31000;                                  // значение с которого пишем кислород в случае AllowToAdd
        private static List<int> m_jobAllowToAdd;                                     // задания AllowToAdd на добавку для весов 5 шт
        private static List<WeigherState> m_weighersState;                            // Состояния весов 5 шт 
        private static List<WeigherState> m_weighersStatePrevious;                    // Состояния весов 5 шт для хранения предыдущего значения 
        private static List<bool> m_releaseWeighersState;                             // Идет процесс выгрузки по кнопке весов 5 шт 
        private static List<int> m_cntWeighersJobReady;                               // Счетчики готовности заданий для весов

        public static int Init()
        {
            SafeInit();
            var listenThread = new Thread(ListenThread);
            listenThread.IsBackground = true;
            listenThread.Start();

            var keepAliveThread = new Thread(KeepAliveThread);
            keepAliveThread.IsBackground = true;
            keepAliveThread.Start();

            InstantLogger.log("Initialization complete");
            return 0;
        }

        public static void SafeInit()
        {
            try
            {
                HeatOn = true;
                m_dataAvailable = false;
                m_oxigenTotal = 0;
                m_oxigenCurrent = 0;
                m_oxigenCurrentStep = 0;
                m_counterNotToGive = 0;
                m_counterAllowToAdd = 0;
                m_lanceCurrentFrame = 0;

                SmPattern = new SteelMakingPatternEvent();
                AdditionsQuantList = new List<AdditionsQuant>();
                m_weightCurrentSteps = new List<WatchSteps>();
                m_pushGate = new ConnectionProvider.Client();
                m_jobAllowToAdd = new List<int>();
                m_weighersState = new List<WeigherState>();
                m_weighersStatePrevious = new List<WeigherState>();
                m_releaseWeighersState = new List<bool>();
                m_cntWeighersJobReady = new List<int>();

                for (int i = 0; i < WeightCounter; i++)
                {
                    m_weightCurrentSteps.Add(new WatchSteps());
                    m_weighersState.Add(new WeigherState());
                    m_weighersStatePrevious.Add(new WeigherState());
                    m_jobAllowToAdd.Add(-1);
                    m_releaseWeighersState.Add(false);
                    m_cntWeighersJobReady.Add(0);

                    //ComSendOxygenMode(i, false); // очищаем задания по вирт кислороде с предыдущей плавки
                    // сделать всу остальную очистку

                }

                ResetAllState(1133); // обнуляем все задания в контроллере
                ResetAllState(1135); // повторяем чтоб убедиться в том что счетчики точно переключились

                InstantLogger.log("Safe initialization complete");
            }
            catch (Exception e)
            {
                InstantLogger.log("Safe initialization complete with result: {0}", e.ToString());
                //throw;
            }
         }
    }
}
