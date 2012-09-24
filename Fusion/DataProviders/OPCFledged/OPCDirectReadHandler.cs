using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTypes;
using Converter;
using Implements;

namespace OPCFledged
{
    public partial class OpcConnector
    {
        //public static Type[] EventsList;
        public static void ProcessEvent(OPCDirectReadEvent ev)
        {
            using (Logger l = new Logger("OPCDirectReader"))
            {
                bool foundEvent = false;
                bool foundToAddedEvent = false;
                List<int> readHsrv = new List<int>();
                for (int index = 0; index < EventsList.Length; index++)
                {
                    if (EventsList[index].Name == ev.EventName)
                    {
                        
                        for (int i = 0; i < m_Item_props.Count; i++)
                        {
                            //InstantLogger.log(m_Item_props[i].eventId.ToString() + "  " + m_Item_props[i].eventPlcpId.ToString() + "  " + m_Item_props[i].eventPropId.ToString());
                            if (index == m_Item_props[i].eventId)
                            {
                                //l.msg("OPC Related event found: {0}", evt.EventName);
                                foundToAddedEvent = true;
                                //processing

                                //m_Item_defs[0]
                                //InstantLogger.log(m_Item_props[i].eventId.ToString() + "  " + m_Item_props[i].eventPlcpId.ToString() + "  " + m_Item_props[i].eventPropId.ToString() + " -- " + m_Handles_srv[m_Item_props[i].eventPropId]);
                                /*for (int j = 0; j < m_Handles_srv.Length; j++)
                                {
                                    InstantLogger.log(m_Handles_srv[j].ToString());
                                }*/
                                
                               //!!! readHsrv.Add(m_Handles_srv[m_Item_props[i].eventPropId]);
                                //int[] m_readHsrv;
                                //m_readHsrv.
                                /*int[] aE;
                                int cancelId;
                                int[] m_readHsrv = new int[];
                                m_The_grp.Read(m_readHsrv, 55667788, out cancelId, out aE);*/
                                //break;
                            }
                            

                        }
                        foundEvent = true;
                        Program.m_pushGate.PushEvent(EventStore[index]);
                        l.msg(EventStore[index].ToString());
                        break;
                    }
                }
                int[] aE;
                int cancelId;
                if (!foundEvent)
                {
                    l.err("Unknown event {0}", ev.EventName);
                }
                else if (!foundToAddedEvent)
                {
                    l.err("OPC group have no event {0}", ev.EventName);
                }
                else
                {
                    l.msg("OPC Related event found and read");
                    m_The_grp.Read(readHsrv.ToArray(), 55667788, out cancelId, out aE);
                }
            }
        }
    }
}
