using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;
using Core;
using OPC.Data;
using OPC.Common;

namespace OPCFledged
{
    internal class CoreListener : IEventListener
    {
        private object conv(string str)
        {
            var res = new byte[6] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20};

            for (int i = 0; i < Math.Min(str.Length, 6); i++)
            {
                int code = (int) str.ElementAt(i);
                if (code > 900) code -= 848;
                res[i] = (byte) code;
            }
            return res;
        }

        private string opcDestination_;
        private string opcAddressFmt_;
        private DateTime cTime = DateTime.Now;

        public CoreListener(string opcAddressFmt, string opcDestination)
        {
            using (Logger l = new Logger("CoreListener"))
            {
                opcAddressFmt_ = opcAddressFmt;
                opcDestination_ = opcDestination;
                l.msg("Started", "Listener", InstantLogger.TypeMessage.unimportant);
            }
        }

        public void OnEvent(BaseEvent newEvent)
        {
            using (Logger l = new Logger("OnEvent"))
            {
                if (cTime.Day != DateTime.Now.Day)
                {
                    cTime = DateTime.Now;
                    InstantLogger.log("", "To be continued...", InstantLogger.TypeMessage.important);
                    InstantLogger.LogFileInit();
                    InstantLogger.log("", "...Continuing", InstantLogger.TypeMessage.important);
                }
                l.msg(newEvent.GetType().FullName);
                if (newEvent is OPCDirectReadEvent)
                {
                    OpcConnector.ProcessEvent(newEvent as OPCDirectReadEvent);
                    return;
                }
                var plcg = new PLCGroup();
                bool found = false;
                foreach (object x in newEvent.GetType().GetCustomAttributes(false))
                {
                    if (x.GetType().Name == "PLCGroup")
                    {
                        plcg = (PLCGroup) x;
                        if (plcg.Destination == opcDestination_)
                        {
                            l.msg("    " + plcg.Location + " -- " + plcg.Destination);
                            found = true;
                            break;
                        }
                    }
                }
                if (!found) return;
                List<int> arrSrvH = new List<int>();
                List<object> arrItmV = new List<object>();

                var plcp = new PLCPoint();
                foreach (var prop in newEvent.GetType().GetProperties())
                {
                    foreach (object x in prop.GetCustomAttributes(false))
                    {
                        if (x.GetType().Name == "PLCPoint")
                        {
                            // InstantLogger.log(prop.GetCustomAttributesData()[1].ToString());
                            if (((PLCPoint) x).IsWritable)
                            {
                                plcp = (PLCPoint) x;
                                var s = string.Format(opcAddressFmt_, plcg.Location,
                                                      OPCFledged.OpcConnector.cnv(plcp.Location));
                                var sb = new StringBuilder();
                                sb.AppendFormat("        " + prop.Name + " = " +
                                      prop.GetValue(newEvent, null).ToString());
                                sb.AppendFormat("\n            IsWritable = " + plcp.IsWritable.ToString());
                                sb.AppendFormat("\n            " + plcp.Location + " % " + s);
                                sb.AppendFormat("\n            " + newEvent.GetType());
                                l.msg(sb.ToString());
                                int index = 0;
                                bool eureka = false;
                                while (index < OpcConnector.m_Item_defs.Count)
                                {
                                    OPCItemDef v = OpcConnector.m_Item_defs[index];
                                    //InstantLogger.msg("comparing <{0}> == <{1}>", v.ItemID, s);
                                    if (v.ItemID == s)
                                    {
                                        if (!v.Active)
                                        {
                                            l.err("Item <{0}> is excluded from monitoring", v.ItemID);
                                        }
                                        else
                                        {
                                            arrSrvH.Add(OpcConnector.m_Handles_srv[index]);
                                            l.msg("+++++ {0}", prop.PropertyType.ToString());
                                            if (prop.PropertyType == (typeof (System.String)))
                                            {
                                                string w = prop.GetValue(newEvent, null).ToString();
                                                List<byte> wb = new List<byte>();
                                                for (int i = 0; i < w.Length; i++)
                                                {
                                                    UInt16 b = (UInt16) w.ElementAt(i);
                                                    if (b > 900) b -= 848;
                                                    wb.Add((byte) b);
                                                }
                                                arrItmV.Add(wb.ToArray());
                                            }
                                            else if (prop.PropertyType == (typeof (System.Boolean)))
                                            {
                                                if (!OpcConnector.FlagStore.ContainsKey(v.ItemID))
                                                {
                                                    l.err("First reference to <{0}> found on writing!!!", v.ItemID);
                                                    OpcConnector.FlagStore.Add(v.ItemID, 0);
                                                }
                                                arrItmV.Add(BoolExpressions.SetBit(OpcConnector.FlagStore[v.ItemID], plcp.BitNumber, Convert.ToBoolean(prop.GetValue(newEvent, null))));
                                            }
                                            else arrItmV.Add(prop.GetValue(newEvent, null));
                                        }
                                        eureka = true;
                                        break;
                                    }
                                    index++;
                                }
                                if (!eureka) l.err("Item <{0}> is unknown to OPC Fledged", s);
                            }
                        }
                    }
                }
                int count = arrItmV.Count;
                if (count > 0)
                {
                    int[] aE;
                    if (OpcConnector.m_The_grp.Write(arrSrvH.ToArray(), arrItmV.ToArray(), out aE))
                    {
                        l.msg("Done processing event -- true");
                    }
                    else
                    {
                        for (int i = 0; i < aE.Length; i++)
                        {
                            if (HRESULTS.Failed(aE[i]))
                            {
                                l.err("Item srvH={0} failed with HRESULT=0x{1:x} -- value {2}",
                                      arrSrvH[i],
                                      aE[i],
                                      arrItmV[i]
                                    );
                            }
                            else
                            {
                                l.msg("Item srvH={0} succseeded -- value {1}", arrSrvH[i], arrItmV[i]);
                            }
                        }
                    }
                }
                l.msg("Done processing event");
            }
        }
    }
}