using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConnectionProvider;
using Core;
using Converter;
using CommonTypes;
using ConnectionProvider.MainGate;
using Implements;

namespace CoreMeter
{
    class Listener : IEventListener
    {

        public Listener()
        {
            InstantLogger.log("Listener", "Started\n", InstantLogger.TypeMessage.important);
        }

        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("Listener"))
            {
                if (evt is FlexEvent)
                {
                    lock (Program.ResultList)
                    {
                        var last = Program.ResultList.Count - 1;
                        var fxe = evt as FlexEvent;
                        if (fxe.Operation.StartsWith("CoreMeteringEvent"))
                        {
                            for (int i = 0; i < Program.FlexList.Count; i++)
                            {
                                if (fxe.Id == Program.FlexList[i].Id)
                                {
                                    var fxhReceived = new FlexHelper(fxe);
                                    var fxhSended = new FlexHelper(Program.FlexList[i]);

                                    var timeReceive = DateTime.Now;
                                    var timeSend = (DateTime) fxhSended.GetArg("SendTime");
                                    var deltaTime = timeReceive - timeSend;
                                    var delayMs = deltaTime.TotalMilliseconds;
                                                  
                                    delayMs = delayMs > 0 ? delayMs : 0;
                                    Program.ResultList[last].TotalDelayMs += delayMs;
                                    var previousDelay = Program.ResultList[last].MaxDelayMs;
                                    Program.ResultList[last].MaxDelayMs = delayMs > previousDelay
                                                                              ? delayMs
                                                                              : previousDelay;
                                    if (Program.ResultList[last].ReceivedEvents > 0)
                                    {
                                        Program.ResultList[last].AverageDelayMs =
                                                   (Program.ResultList[last].TotalDelayMs/
                                                   Program.ResultList[last].ReceivedEvents);
                                    }
                                    //Console.WriteLine(Program.ResultList[last].AverageDelayMs);
                                    //Console.WriteLine(delayMs);
                                    //Console.WriteLine(Program.ResultList[last].MaxDelayMs);
                                    //Console.WriteLine("send time = {0}; receive time = {1}", fxhSended.GetArg("SendTime"), fxhReceived.GetArg("SendTime"));
                                    Program.FlexList.RemoveAt(i);
                                    break;
                                }
                            }

                            Program.ResultList[last].ReceivedEvents++;
                            //Program.ResultList[last].LostEvents = Program.ResultList[last].FieredEvents -
                            //                                      Program.ResultList[last].ReceivedEvents;
                            //Console.WriteLine(Program.ResultList[last].LostEvents);
                        }
                    }
                }
            }
        }
    }
}
