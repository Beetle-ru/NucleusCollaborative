using System;
using System.Collections.Generic;
using System.Threading;
using CommonTypes;
using ConnectionProvider;
using Converter;
using Implements;

namespace OPCEmu
{
    internal class Program
    {
        private static void Main()
        {
            var o = new HeatChangeEvent();
            var eListener = new Listener();
            var listener = new Client(eListener);
            listener.Subscribe();
            Console.WriteLine("Started");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Слушает события ядра и добавляет их в очередь
    /// </summary>
    internal class Listener : IEventListener
    {
        private readonly EventsStack _eventsStack;

        public Listener()
        {
            _eventsStack = new EventsStack();
        }

        #region IEventListener Members

        public void OnEvent(BaseEvent e)
        {
            _eventsStack.Add(e);
        }

        #endregion
    }

    /// <summary>
    /// Хранит очередь событий и постоянно проверяет ее на наличие какого-либо события
    /// Наядя событие, запускает эмулятор ответной реакции для тех типов событий, где это реализовано
    /// </summary>
    internal class EventsStack
    {
        private readonly Queue<BaseEvent> _eventsQueue;
        private readonly Client _gate;

        private bool _processingHeat;

        public EventsStack()
        {
            _processingHeat = false;
            _eventsQueue = new Queue<BaseEvent>();
            var t = new Thread(StartProcessingStack);
            t.Start();
            _gate = new Client();
            _gate.Subscribe();
        }

        public void Add(BaseEvent e)
        {
            lock (_eventsQueue)
            {
                _eventsQueue.Enqueue(e);
            }
        }

        private void StartProcessingStack(object state)
        {
            while (true)
            {
                lock (_eventsQueue)
                {
                    if (_eventsQueue.Count > 0)
                    {
                        var t = new Thread(React);
                        t.Start(_eventsQueue.Dequeue());
                    }
                    Thread.Sleep(10);
                }
            }
        }

        private void React(object data)
        {
            var e = data as BaseEvent;
            if (e != null)
            {
                Type eventType = e.GetType();
                Console.WriteLine("Incoming event. Event type is " + eventType);
                if (eventType == typeof(OPCDirectReadEvent))
                {
                    var opcDirectReadEvent = e as OPCDirectReadEvent;
                    if (opcDirectReadEvent != null)
                        Console.WriteLine("OPCDirectReadEvent asking for " + opcDirectReadEvent.EventName);
                    if (opcDirectReadEvent != null &&
                        (opcDirectReadEvent).EventName == typeof(BoundNameMaterialsEvent).Name)
                    {
                        var reaction = new BoundNameMaterialsEvent
                                           {
                                               Bunker5MaterialName = "DOLOMS",
                                               Bunker6MaterialName = "Alkonc",
                                               Bunker7MaterialName = "KOKS",
                                               Bunker8MaterialName = "IZVEST",
                                               Bunker9MaterialName = "IZVEST",
                                               Bunker10MaterialName = "FOM",
                                               Bunker11MaterialName = "DOLMIT",
                                               Bunker12MaterialName = "DOLOMS"
                                           };
                        _gate.PushEvent(reaction);
                        Console.WriteLine("BoundNameMaterialsEvent send");
                        for (int i = 0; i < 10; i++)
                        {
                            var realO2 = new BlowingEvent() { O2TotalVol = 1 };
                            _gate.PushEvent(realO2);                            
                            Thread.Sleep(1000);
                        }
                        var realzeroO2 = new BlowingEvent() { O2TotalVol = 0 };
                        _gate.PushEvent(realzeroO2);
                    }
                    if (opcDirectReadEvent != null &&
                        (opcDirectReadEvent).EventName == typeof(ModeVerticalPathEvent).Name)
                    {
                        var reaction = new ModeVerticalPathEvent
                        {
                            VerticalPathMode = 1
                        };
                        _gate.PushEvent(reaction);
                        Console.WriteLine("ModeVerticalPathEvent send");
                    }

                    if (opcDirectReadEvent != null &&
                        (opcDirectReadEvent).EventName == typeof(ModeLanceEvent).Name)
                    {
                        var reaction = new ModeLanceEvent
                        {
                            LanceMode = 3,
                            O2FlowMode = 3
                        };
                        _gate.PushEvent(reaction);
                        Console.WriteLine("ModeLanceEvent send");
                    }

                }
                if (eventType == typeof(SteelMakingPatternEvent))
                {
                    if (_processingHeat)
                    {
                        return;
                    }
                    _processingHeat = true;
                    var steelMakingPatternEvent = e as SteelMakingPatternEvent;
                    if (steelMakingPatternEvent != null)
                    {
                        int stepsCount = steelMakingPatternEvent.steps.Count;
                        for (int i = 0; i < stepsCount; i++)
                        {
                            var reaction = new HeatSchemaStepEvent
                                               {
                                                   Step = i
                                               };
                            var realO2 = new BlowingEvent() { O2TotalVol = 20000 * i / stepsCount };
                            var realLance = new LanceEvent() { LanceHeight = 700 - (700 * i / stepsCount) };
                            _gate.PushEvent(reaction);
                            Console.WriteLine("HeatSchemaStepEvent send");
                            InstantLogger.log(DateTime.Now.ToString() + " Новый шаг, HeatSchemaStepEvent send\r\n");
                            _gate.PushEvent(realO2);
                            Console.WriteLine("BlowingEvent send");
                            _gate.PushEvent(realLance);
                            Console.WriteLine("LanceEvent send");

                            Thread.Sleep(250);
                        }
                    }
                    var lastReaction = new HeatSchemaStepEvent
                                           {
                                               Step = -1
                                           };
                    _gate.PushEvent(lastReaction);
                    Console.WriteLine("HeatSchemaStepEvent with stop signal send");
                    _processingHeat = false;
                }
            }
        }
    }
}