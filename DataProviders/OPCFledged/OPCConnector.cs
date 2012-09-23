using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using OPC.Data;
using OPC.Common;
using OPC.Data.Interface;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using CommonTypes;
using Implements;
using Core;
using System.Collections.Generic;
using Converter;

namespace OPCFledged
{
    public class FledgedItemDef
    {
        public FledgedItemDef(int _eventId, int _eventPropId, int _eventPlcpId)
        {
            eventId = _eventId;
            eventPropId = _eventPropId;
            eventPlcpId = _eventPlcpId;
        }
        public int eventId;
        public int eventPropId;
        public int eventPlcpId;
    }
    public partial class OpcConnector
    {
        public static string cnv(string arg, int opcConvSchema = 0)
        {
            var s = arg;
            switch (opcConvSchema)
            {
                case 1:
                    s = arg.Replace(',', '.');
                    var ix = s.LastIndexOf('.');
                    if (s.IndexOf('.') == ix) return s;
                    return s.Substring(0, ix);
                case 2:
                    s = arg.Replace("STRING", "B");
                    return s;
                default:
                    return s;
            }
        }

        //private const int maxItemCount = 36;
        private const int maxItemCount = 1000;
        private static object locker = new object();
        private static OpcServer m_The_srv;
        public static OpcGroup m_The_grp;
        public static readonly List<OPCItemDef> m_Item_defs = new List<OPCItemDef>();
        public static readonly List<FledgedItemDef> m_Item_props = new List<FledgedItemDef>();
        public static int[] m_Handles_srv;
        public static Type[] EventsList;
        private static List<CommonTypes.BaseEvent> EventStore = new List<CommonTypes.BaseEvent>();

        public OpcConnector(string progId, string opcDestination, string opcAddressFmt, int opcConvSchema = 0, int reqUpdateRate_ms = 500)
        {
            using (Logger l = new Logger("OpcConnector"))
            {
                string plcName = "NO-PLC";
                m_The_srv = new OpcServer();
                m_The_srv.Connect(progId);
                Thread.Sleep(500); // we are faster then some servers!

                // add our only working group
                m_The_grp = m_The_srv.AddGroup(opcDestination + "-items", false, reqUpdateRate_ms);

                // add all the items and save server handles
                int itemCounter = 0;

                EventsList = BaseEvent.GetEvents();
                for (int eventCounter = 0; eventCounter < EventsList.Length; eventCounter++)
                {
                    var heatEvent = EventsList[eventCounter];
                    l.msg(heatEvent.Name); // название события
                    EventStore.Add((CommonTypes.BaseEvent) Activator.CreateInstance(heatEvent));
                        // создаем экземпляр события
                    int plcpIndex = -1;
                    bool opcRelated = false;
                    for (int i = 0; i < heatEvent.GetCustomAttributes(false).Length; i++)
                    {
                        object x = heatEvent.GetCustomAttributes(false)[i];
                        if (x.GetType().Name == "PLCGroup")
                        {
                            PLCGroup p = (PLCGroup) x;
                            if (p.Destination == opcDestination)
                            {
                                plcName = p.Location;
                                l.msg("     {0} ==>> {1}", p.Location, p.Destination);
                                opcRelated = true;
                                break;
                            }
                        }
                    }
                    if (!opcRelated) continue;
                    for (int propertyCounter = 0; propertyCounter < heatEvent.GetProperties().Length; propertyCounter++)
                    {
                        var prop = heatEvent.GetProperties()[propertyCounter];
                        object first = null;
                        for (int index = 0; index < prop.GetCustomAttributes(false).Length; index++)
                        {
                            object x = prop.GetCustomAttributes(false)[index];
                            if (x.GetType().Name == "PLCPoint")
                            {
                                plcpIndex = index;
                                first = x;
                                break;
                            }
                        }
                        var plcp = (PLCPoint) first;
                        if (plcp == null) continue;
                        string s = string.Format(opcAddressFmt, plcName, cnv(plcp.Location, opcConvSchema));
                        // add new OPC Item using itemCounter as client handle
                        m_Item_defs.Add(new OPCItemDef(s, true, itemCounter, VarEnum.VT_EMPTY));
                        m_Item_props.Add(new FledgedItemDef(eventCounter, propertyCounter, plcpIndex));
                        l.msg("{0}:  {1}", itemCounter, s);
                        itemCounter++;
                    }
                    if (itemCounter >= maxItemCount) break;
                }
                l.msg("Counter is {0}", itemCounter);
                if (itemCounter == 0) throw new  Exception("No items found");
                // Validate Items (ignoring BLOBs
                OPCItemResult[] rItm;
                m_The_grp.ValidateItems(m_Item_defs.ToArray(), false, out rItm);
                if (rItm == null) throw new  Exception("OPC ValidateItems: -- system error: arrRes is null");
                List<int> itemExclude = new List<int>();
                for (int i = 0; i < itemCounter; i++)
                {
                    if (HRESULTS.Failed(rItm[i].Error))
                    {
                        l.err(
                            "Error 0x{1:x} while adding item {0} -- item EXCLUDED from monitoring",
                            i, rItm[i].Error);
                        itemExclude.Add(i);
                    }
                }
                if (itemCounter == itemExclude.Count) throw new Exception("No items passed validation");
                // Exclude invalid items
                // Add Items
                m_The_grp.AddItems(m_Item_defs.ToArray(), out rItm);
                if (rItm == null) return;
                for (int i = 0; i < itemCounter; i++)
                {
                    if (HRESULTS.Failed(rItm[i].Error))
                    {
                        rItm[i].HandleServer = -1;
                        
                    }
                }

                m_Handles_srv = new int[itemCounter];
                for (int i = 0; i < itemCounter; i++)
                {
                    m_Handles_srv[i] = rItm[i].HandleServer;
                }
                //m_The_grp.WriteCompleted += TheGrpWriteComplete;


                int cancelId;
                int[] aE;
                //  l.msg("start read");
                m_The_grp.SetEnable(true);
                m_The_grp.Active = true;
                m_The_grp.DataChanged += new DataChangeEventHandler(this.TheGrpDataChange);
                m_The_grp.ReadCompleted += new ReadCompleteEventHandler(this.TheGrpReadComplete);
                  //m_The_grp.Read(m_Handles_srv, 55667788, out cancelId, out aE);



                //Thread.Sleep(500);
                // l.msg("end read");
            }
        }
        private object conv(string str)
        {
            var res = new byte[6] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            for (int i = 0; i < Math.Min(str.Length, 6); i++)
            {
                int code = (int)str.ElementAt(i);
                if (code > 900) code -= 848;
                res[i] = (byte)code;
            }
            return res;
        }
        public void CloseConnection()
        {
            int[] aE;
            m_The_grp.WriteCompleted -= TheGrpWriteComplete;
            m_The_grp.RemoveItems(m_Handles_srv, out aE);
            m_The_grp.Remove(false);
            m_The_srv.Disconnect();
        }

        // ------------------------------ events -----------------------------
        public static object conv(object obj, int buflen = 6)
        {
            var res = new char[buflen];

            for (int i = 0; i < buflen; i++)
            {
                res[i] = ' ';
            }
            byte[] barr = (byte[])obj;
            for (int i = 0; i < Math.Min(barr.Length, buflen); i++)
            {
                int code = (int)barr[i];
                if (code > 900) code -= 848;
                res[i] = (char)code;
            }
            return res;
        }
        public string convBack(byte[] barr)
        {
            string str = "";
            if (barr[0] == barr.Length)
            {
                for (int i = 0; i < Math.Min(barr[1], barr[0] - 2); i++)
                {
                    str += (char)barr[i + 2];
                }
                return str;
            }
            else
            {
                for (int i = 0; i < barr.Length; i++)
                {
                    str += (char)barr[i];
                }
                return str;
            }
        }
        public void CoreEventGenerator(object sender, EventArgs oEvent)
        {
            using (Logger l = new Logger("OnDataChange", ref locker))
            {
                int	transactionID;
	            int	groupHandleClient;
	            int	masterQuality;
	            int	masterError;
                OPCItemState[] sts;
                if (oEvent is DataChangeEventArgs)
                {
                    var dataE = oEvent as DataChangeEventArgs;
                    transactionID = dataE.transactionID;
                    groupHandleClient = dataE.groupHandleClient;
                    masterQuality = dataE.masterQuality;
                    masterError = dataE.masterError;
                    sts = dataE.sts;
                } else if (oEvent is ReadCompleteEventArgs)
                {
                    var dataE = oEvent as ReadCompleteEventArgs;
                    transactionID = dataE.transactionID;
                    groupHandleClient = dataE.groupHandleClient;
                    masterQuality = dataE.masterQuality;
                    masterError = dataE.masterError;
                    sts = dataE.sts;
                }
                else
                {
                    l.err("oEvent is no DataChangeEventArgs or no ReadCompleteEventArgs");
                    return;
                }
                l.msg("gh={0} id={1} me={2} mq={3}", groupHandleClient, transactionID, masterError, masterQuality);
                List<int> relatedEventsIdList = new List<int>();
                foreach (OPCItemState s in sts)
                {
                    if (HRESULTS.Succeeded(s.Error))
                    {
                        l.msg(" ih={0} ItemID={2} >> value={1}", s.HandleClient, s.DataValue, m_Item_defs.ElementAt(s.HandleClient).ItemID);
                        var _p_ = EventsList[m_Item_props[s.HandleClient].eventId].GetProperties()[
                            m_Item_props[s.HandleClient].eventPropId];
                        bool presenceFlag = false;
                        foreach (int evid in relatedEventsIdList)
                        {
                            if (evid == m_Item_props[s.HandleClient].eventId)
                                presenceFlag = true;
                        }
                        if (!presenceFlag)
                            relatedEventsIdList.Add(m_Item_props[s.HandleClient].eventId);
                        //l.msg(_p_.PropertyType.ToString() + " --- " + s.DataValue.GetType().ToString());
                        if (_p_.PropertyType == (typeof(System.Boolean)))
                        {
                            l.msg("boolean type");
                            var _nb_ = ((PLCPoint)_p_.GetCustomAttributes(false)[m_Item_props[s.HandleClient].eventPlcpId]).BitNumber;
                            _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], BoolExpressions.GetBit(Convert.ToByte(s.DataValue), _nb_), null);
                        }
                        else if (_p_.PropertyType == (typeof(System.Int32)))
                        {
                            l.msg("int32 type {0}", s.DataValue.GetType().ToString());
                            if (s.DataValue.GetType() == (typeof(System.Int16)))
                            {
                                _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], Convert.ToInt32(s.DataValue), null);
                            }
                            else
                            {
                                Int64 itmp = Convert.ToInt64(s.DataValue);
                                UInt64 tmp = (UInt64)itmp;
                                if (itmp != -1)
                                {
                                    if (tmp > 0xffffffff)
                                    {
                                        tmp = 32000;
                                    }
                                    if (tmp >= 0x7ffffff)
                                    {
                                        tmp = 31999;
                                    }
                                    _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], Convert.ToInt32(tmp), null);
                                }
                                else
                                    _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], -1, null);
                            }
                        }
                        else if (_p_.PropertyType == (typeof(System.Double)))
                        {
                            l.msg("double type");
                            _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], Convert.ToDouble(s.DataValue), null);
                        }
                        else if (_p_.PropertyType == (typeof(System.Char[])))
                        {
                            l.msg("char[]");
                            _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], conv(s.DataValue), null);
                        }
                        else if (_p_.PropertyType == (typeof(System.String)))
                        {
                            l.msg("string");
                            try
                            {
                                _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], convBack((byte[])s.DataValue), null);
                            }
                            catch (Exception)
                            {
                                _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], "err", null);
                                //throw;
                            }

                        }
                        else
                        {
                            l.msg("any type");
                            try
                            {
                                _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], s.DataValue, null);
                            }
                            catch (Exception)
                            {
                                l.msg("receive data type not support", "OPCConnector function TheGrpDataChange", InstantLogger.TypeMessage.death);
                                _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], 0, null);
                                throw;
                            }

                        }

                    }
                    else
                        l.msg(String.Format(" ih={0}    ERROR=0x{1:x} !", s.HandleClient, s.Error));
                }
                foreach (var idReceiveEvent in relatedEventsIdList)
                {
                    l.msg(EventStore[idReceiveEvent].ToString(), "OPCConnectore function TheGrpDataChange", InstantLogger.TypeMessage.unimportant);
                    Program.m_pushGate.PushEvent(EventStore[idReceiveEvent]);

                }

            }
        }
        public void TheGrpDataChange(object sender, DataChangeEventArgs e)
        {
            CoreEventGenerator(sender, e);
            //using (Logger l = new Logger("OnDataChange", ref locker))
            //{
            //    l.msg("gh={0} id={1} me={2} mq={3}", e.groupHandleClient, e.transactionID, e.masterError, e.masterQuality);
            //    List<int> relatedEventsIdList = new List<int>();
            //    foreach (OPCItemState s in e.sts)
            //    {
            //        if (HRESULTS.Succeeded(s.Error))
            //        {
            //            l.msg(" ih={0} ItemID={2} >> value={1}", s.HandleClient, s.DataValue, m_Item_defs.ElementAt(s.HandleClient).ItemID);
            //            var _p_ = EventsList[m_Item_props[s.HandleClient].eventId].GetProperties()[
            //                m_Item_props[s.HandleClient].eventPropId];
            //            bool presenceFlag = false;
            //            foreach (int evid in relatedEventsIdList)
            //            {
            //                if (evid == m_Item_props[s.HandleClient].eventId)
            //                    presenceFlag = true;
            //            }
            //            if (!presenceFlag)
            //                relatedEventsIdList.Add(m_Item_props[s.HandleClient].eventId);
            //            //l.msg(_p_.PropertyType.ToString() + " --- " + s.DataValue.GetType().ToString());
            //            if (_p_.PropertyType == (typeof(System.Boolean)))
            //            {
            //                l.msg("boolean type");
            //                var _nb_ = ((PLCPoint)_p_.GetCustomAttributes(false)[m_Item_props[s.HandleClient].eventPlcpId]).BitNumber;
            //                _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], BoolExpressions.GetBit(Convert.ToByte(s.DataValue), _nb_), null);
            //            }
            //            else if (_p_.PropertyType == (typeof(System.Int32)))
            //            {
            //                l.msg("int32 type {0}", s.DataValue.GetType().ToString());
            //                if (s.DataValue.GetType() == (typeof(System.Int16)))
            //                {
            //                    _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], Convert.ToInt32(s.DataValue), null);
            //                }
            //                else
            //                {
            //                    Int64 itmp = Convert.ToInt64(s.DataValue);
            //                    UInt64 tmp = (UInt64)itmp;
            //                    if (itmp != -1)
            //                    {
            //                        if (tmp > 0xffffffff)
            //                        {
            //                            tmp = 32000;
            //                        }
            //                        if (tmp >= 0x7ffffff)
            //                        {
            //                            tmp = 31999;
            //                        }
            //                        _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], Convert.ToInt32(tmp), null);
            //                    }
            //                    else
            //                        _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], -1, null);
            //                }
            //            }
            //            else if (_p_.PropertyType == (typeof(System.Double)))
            //            {
            //                l.msg("double type");
            //                _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], Convert.ToDouble(s.DataValue), null);
            //            }
            //            else if (_p_.PropertyType == (typeof(System.Char[])))
            //            {
            //                l.msg("char[]");
            //                _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], conv(s.DataValue), null);
            //            }
            //            else if (_p_.PropertyType == (typeof(System.String)))
            //            {
            //                l.msg("string");
            //                try
            //                {
            //                    _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], convBack((byte[])s.DataValue), null);
            //                }
            //                catch (Exception)
            //                {
            //                    _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], "err", null);
            //                   //throw;
            //                }

            //            }
            //            else
            //            {
            //                l.msg("any type");
            //                try
            //                {
            //                    _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], s.DataValue, null);
            //                }
            //                catch (Exception)
            //                {
            //                    l.msg("receive data type not support", "OPCConnector function TheGrpDataChange", InstantLogger.TypeMessage.death);
            //                    _p_.SetValue(EventStore[m_Item_props[s.HandleClient].eventId], 0, null);
            //                    throw;
            //                }

            //            }

            //        }
            //        else
            //            l.msg(String.Format(" ih={0}    ERROR=0x{1:x} !", s.HandleClient, s.Error));
            //    }
            //    foreach (var idReceiveEvent in relatedEventsIdList)
            //    {
            //        l.msg(EventStore[idReceiveEvent].ToString(), "OPCConnectore function TheGrpDataChange", InstantLogger.TypeMessage.unimportant);
            //       Program.m_pushGate.PushEvent(EventStore[idReceiveEvent]);

            //    }

            //}

        }

        public void TheGrpReadComplete(object sender, ReadCompleteEventArgs e)
        {
            
            CoreEventGenerator(sender, e);
            //using (Logger l = new Logger("OnReadComplete", ref locker))
            //{
            //    l.msg("after read");
            //    l.msg("ReadComplete event: gh={0} id={1} me={2} mq={3}", e.groupHandleClient, e.transactionID,
            //               e.masterError, e.masterQuality);
            //    foreach (OPCItemState s in e.sts)
            //    {
            //        if (HRESULTS.Succeeded(s.Error))
            //            l.msg(" ih={0} v={1} q={2} t={3}", s.HandleClient, s.DataValue, s.Quality, s.TimeStamp);
            //        else
            //            l.msg(" ih={0}    ERROR=0x{1:x} !", s.HandleClient, s.Error);
            //    }
            //}
        }

        public void TheGrpWriteComplete(object sender, WriteCompleteEventArgs e)
        {
            using (Logger l = new Logger("OnWriteComplete", ref locker))
            {
                l.msg("WriteComplete event: gh={0} id={1} me={2}", e.groupHandleClient, e.transactionID,
                           e.masterError);
                foreach (OPCWriteResult r in e.res)
                {
                    if (HRESULTS.Succeeded(r.Error))
                        l.msg(" ih={0} e={1}", r.HandleClient, r.Error);
                    else
                        l.msg(" ih={0} ERROR=0x{1:x} !", r.HandleClient, r.Error);
                }
            }
        }
    }
}
