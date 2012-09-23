using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTypes;
using Converter;
using Implements;

namespace Bazooka
{
    internal class Bazooka
    {
        public static ConnectionProvider.Client m_pushGate;
        public static Type[] EventsList;
        public static BaseEvent EventToPush;
        public static Dictionary<string, string> ArgList = new Dictionary<string, string>();

        public static void Main(string[] args)
        {
            using (Logger l = new Logger("Bazooka"))
            {
                try
                {
                    for (int i = 0; i < args.Count(); i++)
                    {
                        l.msg("Command line argument {0} is <{1}>", i, args[i]);
                        if (i > 0)
                        {
                            string[] res = args[i].Split(new[] {'='}, 2);
                            ArgList.Add(res[0], res[1]);
                            l.msg("\tRecognized as <{0}>=<{1}> key-value pair", res[0], res[1]);
                        }
                    }
                    var o = new HeatChangeEvent(); /// нужно чтобы сборка загрузилась
                    EventsList = BaseEvent.GetEvents();
                    bool found = false;
                    for (int index = 0; index < EventsList.Length; index++)
                    {
                        if (EventsList[index].Name == args[0])
                        {
                            l.msg("Event {0} found -- preparing parameters:", args[0]);
                            EventToPush = (CommonTypes.BaseEvent) Activator.CreateInstance(EventsList[index]);
                            // Enum all the properties
                            foreach (var prop in EventsList[index].GetProperties())
                            {
                                l.msg("\tProperty {0} {1} == {2}", prop.PropertyType.ToString(), prop.Name,
                                      prop.GetValue(EventToPush, null));
                            }
                            // real processing
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        m_pushGate = new ConnectionProvider.Client();
                        m_pushGate.PushEvent(EventToPush);
                        System.Threading.Thread.Sleep(1000);
                        l.msg("Event fired>\n{0}", EventToPush.ToString());
                    }
                    else
                    {
                        l.err("Unknown event {0}", args[0]);
                    }
                }
                catch (Exception e)
                {
                    l.err("Exception caught:\n{0}", e.ToString());
                }
            }
        }
    }
}