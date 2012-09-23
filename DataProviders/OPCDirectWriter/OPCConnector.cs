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

namespace OPCDirectWriter
{
    class OpcConnector
    {
        private const int StrCount = 10;
        private static object locker = new object();
        private readonly OpcServer m_The_srv;
        private readonly OpcGroup m_The_grp;
        private readonly OPCItemDef[] m_Item_defs = new OPCItemDef[StrCount];
        private readonly int[] m_Handles_srv = new int[StrCount];

        public OpcConnector(string progId, string plcName, string opcAddressFmt)
        {
            m_The_srv = new OpcServer();
            m_The_srv.Connect(progId);
            Thread.Sleep(500); // we are faster then some servers!

            // add our only working group
            m_The_grp = m_The_srv.AddGroup(plcName + "-strings", false, 900);

            // add two items and save server handles
            for (int i = 0; i < StrCount; i++)
            {
                m_Item_defs[i] = new OPCItemDef(string.Format(opcAddressFmt, plcName, 272 + i * 6), true, i + 1, VarEnum.VT_EMPTY);
            }
            OPCItemResult[] rItm;
            m_The_grp.AddItems(m_Item_defs, out rItm);
            if (rItm == null) return;
            if (HRESULTS.Failed(rItm[0].Error) || HRESULTS.Failed(rItm[1].Error))
            {
                InstantLogger.msg("OPCDirectWriter: {0} -- AddItems - some failed", plcName);
                m_The_grp.Remove(true);
                m_The_srv.Disconnect();
                return;
            }
            for (int i = 0; i < StrCount; i++)
            {
                m_Handles_srv[i] = rItm[i].HandleServer;
            }
            m_The_grp.WriteCompleted += TheGrpWriteComplete;
        }
        private object conv(string str, int buflen = 6)
        {
            var res = new byte[buflen];

            for (int i = 0; i < buflen; i++)
            {
                res[i] = 0x20;
            }
            for (int i = 0; i < Math.Min(str.Length, buflen); i++)
            {
                int code = (int)str.ElementAt(i);
                if (code > 900) code -= 848;
                res[i] = (byte)code;
            }
            return res;
        }
        public void Send(Converter.comAdditionsEvent matEvent)
        {
            // asynch write
            int cancelId;
            int[] aE;
            var itemValues = new object[StrCount];
            itemValues[0] = conv(matEvent.Bunker1MaterialName);
            itemValues[1] = conv(matEvent.Bunker2MaterialName);
            itemValues[2] = conv(matEvent.Bunker3MaterialName);
            itemValues[3] = conv(matEvent.Bunker4MaterialName);
            itemValues[4] = conv(matEvent.Bunker5MaterialName);
            itemValues[5] = conv(matEvent.Bunker6MaterialName);
            itemValues[6] = conv(matEvent.Bunker7MaterialName);
            itemValues[7] = conv(matEvent.Bunker8MaterialName);
            itemValues[8] = conv(matEvent.Bunker9MaterialName);
            itemValues[9] = conv(matEvent.Bunker10MaterialName);
            m_The_grp.Write(m_Handles_srv, itemValues, 1133, out cancelId, out aE);

            // some delay for asynch write-complete callback (simplification)
            Thread.Sleep(500);
        }
        public void CloseConnection()
        {
            int[] aE;
            m_The_grp.WriteCompleted -= TheGrpWriteComplete;
            m_The_grp.RemoveItems(m_Handles_srv, out aE);
            m_The_grp.Remove(false);
            m_The_srv.Disconnect();
        }
        public void Work()
        {
            /*	try						// disabled for debugging
                {	

            m_The_srv = new OpcServer();
            m_The_srv.Connect(serverProgID);
            Thread.Sleep(500); // we are faster then some servers!

            // add our only working group
            m_The_grp = m_The_srv.AddGroup("OPCCSharp-Group", false, 900);

            // add two items and save server handles
            m_Item_defs[0] = new OPCItemDef(itemA, true, 1234, VarEnum.VT_EMPTY);
            m_Item_defs[1] = new OPCItemDef(itemB, true, 5678, VarEnum.VT_EMPTY);
            OPCItemResult[] rItm;
            m_The_grp.AddItems(m_Item_defs, out rItm);
            if (rItm == null)
                return;
            if (HRESULTS.Failed(rItm[0].Error) || HRESULTS.Failed(rItm[1].Error))
            {
                InstantLogger.msg("OPC Tester: AddItems - some failed");
                m_The_grp.Remove(true);
                m_The_srv.Disconnect();
                return;
            }
            ;

            m_Handles_srv[0] = rItm[0].HandleServer;
            m_Handles_srv[1] = rItm[1].HandleServer;

            // asynch read our two items
            m_The_grp.SetEnable(true);
            m_The_grp.Active = true;
            m_The_grp.DataChanged += new DataChangeEventHandler(this.theGrp_DataChange);
            m_The_grp.ReadCompleted += new ReadCompleteEventHandler(this.theGrp_ReadComplete);
            int CancelID;
            int[] aE;
            InstantLogger.msg("before read");
            m_The_grp.Read(m_Handles_srv, 55667788, out CancelID, out aE);

            // some delay for asynch read-complete callback (simplification)
            Thread.Sleep(500);


            // asynch write
            object[] itemValues = new object[2];
            itemValues[0] = (int)21133;
            itemValues[1] = "YANA-CC";
            m_The_grp.WriteCompleted += new WriteCompleteEventHandler(this.theGrp_WriteComplete);
            m_The_grp.Write(m_Handles_srv, itemValues, 99887766, out CancelID, out aE);

            // some delay for asynch write-complete callback (simplification)
            Thread.Sleep(1500);


            // disconnect and close
            //InstantLogger.msg("************************************** hit <return> to close...");
            //Console.ReadLine();
            m_The_grp.DataChanged -= new DataChangeEventHandler(this.theGrp_DataChange);
            m_The_grp.ReadCompleted -= new ReadCompleteEventHandler(this.theGrp_ReadComplete);
            m_The_grp.WriteCompleted -= new WriteCompleteEventHandler(this.theGrp_WriteComplete);
            m_The_grp.RemoveItems(m_Handles_srv, out aE);
            m_The_grp.Remove(false);
            m_The_srv.Disconnect();
            m_The_grp = null;
            m_The_srv = null;


            /*	}
            catch( Exception e )
                {
                InstantLogger.msg( "EXCEPTION : OPC Tester " + e.ToString() );
                return;
                }	*/
        }





        // ------------------------------ events -----------------------------

        public void TheGrpDataChange(object sender, DataChangeEventArgs e)
        {
            lock (locker)
            {
                InstantLogger.msg("DataChange event: gh={0} id={1} me={2} mq={3}", e.groupHandleClient, e.transactionID,
                                  e.masterError, e.masterQuality);
                foreach (OPCItemState s in e.sts)
                {
                    if (HRESULTS.Succeeded(s.Error))
                        InstantLogger.msg(" ih={0} v={1} q={2} t={3}", s.HandleClient, s.DataValue, s.Quality, s.TimeStamp);
                    else
                        InstantLogger.msg(" ih={0}    ERROR=0x{1:x} !", s.HandleClient, s.Error);
                }
            }
        }

        public void TheGrpReadComplete(object sender, ReadCompleteEventArgs e)
        {
            lock (locker)
            {
                InstantLogger.msg("after read");
                InstantLogger.msg("ReadComplete event: gh={0} id={1} me={2} mq={3}", e.groupHandleClient, e.transactionID,
                           e.masterError, e.masterQuality);
                foreach (OPCItemState s in e.sts)
                {
                    if (HRESULTS.Succeeded(s.Error))
                        InstantLogger.msg(" ih={0} v={1} q={2} t={3}", s.HandleClient, s.DataValue, s.Quality, s.TimeStamp);
                    else
                        InstantLogger.msg(" ih={0}    ERROR=0x{1:x} !", s.HandleClient, s.Error);
                }
            }
        }

        public void TheGrpWriteComplete(object sender, WriteCompleteEventArgs e)
        {
            lock (locker)
            {
                InstantLogger.msg("WriteComplete event: gh={0} id={1} me={2}", e.groupHandleClient, e.transactionID,
                           e.masterError);
                foreach (OPCWriteResult r in e.res)
                {
                    if (HRESULTS.Succeeded(r.Error))
                        InstantLogger.msg(" ih={0} e={1}", r.HandleClient, r.Error);
                    else
                        InstantLogger.msg(" ih={0}    ERROR=0x{1:x} !", r.HandleClient, r.Error);
                }
            }
        }
    }
}
