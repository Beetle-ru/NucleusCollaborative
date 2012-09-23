using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.Devices;
using OPC.Common;
using OPC.Data.Interface;
using OPC.Data;
using DirectOPCClient.MainGate;
using System.ServiceModel;
using Client;

namespace DirectOPCClient {
   public partial class MainForm : System.Windows.Forms.Form {
      #region Windows Form Designer generated code
      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.TextBox txtServerInfo;		// workaround to show SelServer Form on applic. start
      private System.Windows.Forms.StatusBar statusBarMain;
      private System.Windows.Forms.StatusBarPanel sbpTime;
      private System.Windows.Forms.StatusBarPanel sbpStatus;				// node in TreeView
      private System.Windows.Forms.ImageList opctreeicons;
      private System.Windows.Forms.ImageList opclisticons;
      private System.Windows.Forms.Timer timerLogg;
      private TabControl tabControl1;
      private TabPage tabPage1;
      private GroupBox groupBox2;
      private Label label5;
      private TextBox txbCtrlItemQual;
      private Label label6;
      private TextBox txbCtrlItemDataType;
      private TextBox txbCtrlItemValue;
      private Label label7;
      private TextBox txbCtrlItemTimeSt;
      private Label label8;
      private GroupBox groupBox1;
      private Label label3;
      private TextBox txtItemQual;
      private Label label2;
      private TextBox txtItemDataType;
      private TextBox txtItemValue;
      private Label label4;
      private TextBox txtItemTimeSt;
      private Label label1;
      private TextBox txbActPoint;
      private TabPage tabPage2;
      private ComboBox cbCtrlAliases;
      private Label label11;
      private Label label10;
      private TextBox txbCtrldtMax;
      private Label label9;
      private TextBox txbCtrlReadCntMax;
      private Label label12;
      private TextBox txbCtrlObjType;
      private TextBox txbCtrlItemValueUmc;
      private ListBox lbLogg;
      private TextBox txbCtrlReadCnt;
      private Button buttProbeSincRead;
      private Label label14;
      private TextBox txbCoreResult;
      private Label label13;
      private TextBox txbEventText;
      #endregion

      public class clsPointInfo {
         public string sGroup = string.Empty;
         public string sAlias = string.Empty;
         public string sAliasInfoTeil = string.Empty;
         public int iCnvNr;
         public string sPLC_Adr = string.Empty;
         public string sDesc = string.Empty;
         public bool bIsTakeOver = false;
         public bool bIsSetPoint = false;
         public int iIdxInGroupLi;
         public OPCItemState opcItem;
         public VarEnum CanonicalDataType;
         public string sCanonicalDataType;
         public OPCACCESSRIGHTS AccessRights;
         public TypeCode itmTypeCode;
         public int iDataLength;
         public int iDataValue;
         public double fDataValue;
         public string sDataValue;
         public DateTime dtDataValue;
         public int iReadCnt;          // ++ nach jeder Readaccess  :=0 wenn geschrieben in Oracle
         public int iReadCntMax;       // max von 'iReadCnt'
         public DateTime dtMax;        // wann 'iReadCntMax' fixiert ist
         }
      clsPointInfo itemclsPointInfo;
      List<clsPointInfo> pointLi;
      public class clsGroupInfo {
         public string sName = string.Empty;
         public int iItemsCnt;
         public List<int> iIdxInPointLi = new List<int>();
         }
      clsGroupInfo itemclsGroupInfo;
      List<clsGroupInfo> groupLi;
      public string sAliasToControl,sAliasInfoTeil;

      public delegate void AddListItem(String myString);
      public AddListItem lbLoggDelegate;
      public delegate void setTextInTxb(ref TextBox Txb,String sTxbText);
      public setTextInTxb setTextInTxbDelegate;
      public delegate void setTextInStatusBarPanel(int iPanelNo,String sTxbText);
      public setTextInStatusBarPanel setTextInStatusBarPanelDelegate;

      public Process thisprocess;			// running OS process
      public Thread tLoggToFile;
      public string sIP,sCompName;

      public string selectedOpcSrv;			// Name (ProgID) of selected OPC-server
      public OpcServer theSrv = null;		// root OPCDA object
      public OpcGroup theGrp = null;		// the only one OPC-Group in this example

      public string itmFullID;				// fully qualified OPC namespace path
      public int itmHandleClient;			// 0 if no current item selected
      public int itmHandleServer;
      public OPCACCESSRIGHTS itmAccessRights;
      public TypeCode itmTypeCode;				// saved data type of current item

      public bool first_activated = false;	// workaround to show SelServer Form on applic. start
      public bool opc_connected = false;		// flag if connected

      public string rootname = "Root";			// string of TreeView root (dummy)
      public string selectednode;
      public string selecteditem;				// item in ListView

      private Object lockThis = new Object();   // damit wird Eventbearbeitung geschutzt
      ASCIIEncoding ascii = new ASCIIEncoding();
      //UTF8Encoding  ascii = new UTF8Encoding();
      //UnicodeEncoding ascii = new UnicodeEncoding();

      OracleConnection OraConn = null;
      OracleDataReader myReader = null;
      OracleCommand OraCmd = null;

      MainGateClient mainGate = null;

      public static List<string> strLog1 = new List<string>();
      public static List<string> strLog2 = new List<string>();
      public static List<string> strItems_Groups = new List<string>();
      public static List<string> strItems_Alias = new List<string>();
      public static List<string> strItems_adrPLC = new List<string>();
      public static List<string> strItems_Desc = new List<string>();
      public static int iAcktLog = 0;
      public static int iCntLog1 = 0;
      public static int iCntLog2 = 0;
      public int iThreadCnt = 0;
      public string sP,sSp;
      public int i,j,k;

      public MainForm() {
         thisprocess = Process.GetCurrentProcess();		// see DoConnect for client-name
         Computer myComp = new Computer();
         sCompName = myComp.Name;                        // HOST-Name
         InitializeComponent();
         lbLoggDelegate = new AddListItem(AddlbLoggItemMethod);
         setTextInTxbDelegate = new setTextInTxb(setTextInTxbMethod);
         }

      protected void theSrv_ServerShutDown(object sender,ShutdownRequestEventArgs e) {					// event: the OPC server shuts down
         MessageBox.Show(this,"OPC server shuts down because:" + e.shutdownReason,"ServerShutDown",MessageBoxButtons.OK,MessageBoxIcon.Warning);
         }

      public bool DoInit() {
         string[] itemstrings = new string[5];
         string sPLC_Adr = "*UnDef*",sAlias = "*UnDef*",sGroup = "",sDesc = "";
         int iPLCDataLength;

         sAliasToControl = "ACT_C1_STAHLMARKE0";
         itemstrings[2] = itemstrings[3] = itemstrings[4] = "-";
         try {
            selectedOpcSrv = "OPC.SimaticNET.1";   // берем ЭТОТ протокол
            AddLogg("Работаю на " + sCompName);
            AddLogg("LoadDefinitions()-Start");
            LoadDefinitions();
            AddLogg("LoadDefinitions()-End");

            theSrv = new OpcServer();
            if (!DoConnect(selectedOpcSrv)) {
               AddLogg("DoConnect(" + selectedOpcSrv + ")=false");
               //////////return false;
               }
            // add event handler for server shutdown
            theSrv.ShutdownRequested += new ShutdownRequestEventHandler(this.theSrv_ServerShutDown);
            // precreate the only OPC group in this example
            if (!CreateGroup()) {
               AddLogg("CreateGroup()=false");
               //////////return false;
               }
            AddLogg("DoInit: Start");
            RemoveItem();		// first remove previous item if any
            cbCtrlAliases.Items.Clear();
            groupLi = new List<clsGroupInfo>();
            pointLi = new List<clsPointInfo>();
            j = 0;
            for (i = 0; i < strItems_Alias.Count; i++) {
               try {
                  itemclsPointInfo = new clsPointInfo();
                  itmHandleClient = j;
                  sPLC_Adr = strItems_adrPLC[i];   // "LOCATION0   PLC13:DB2,STRING66,8" // S7:[PLC13]DB2,STRING66,8
                  sAlias = strItems_Alias[i];
                  sGroup = strItems_Groups[i];
                  sDesc = strItems_Desc[i];
                  //k = sPLC_Adr.IndexOf("STRING");
                  k = sPLC_Adr.IndexOf("CHAR");
                  if (k < 0) {
                     itemclsPointInfo.iDataLength = 0;
                     }
                  else {
                     string[] sSplitted = sPLC_Adr.Trim().Split(',');
                     itemclsPointInfo.iDataLength = Convert.ToInt32(sSplitted[sSplitted.GetLength(0) - 1].Trim());
                     }
                  OPCItemDef[] aD = new OPCItemDef[1];
                  aD[0] = new OPCItemDef(sPLC_Adr,true,itmHandleClient,VarEnum.VT_EMPTY);
                  OPCItemResult[] arrRes;
                  theGrp.AddItems(aD,out arrRes);
                  if (arrRes == null) {
                     AddLogg("DoInit:AddItem" + i + " " + sPLC_Adr + " arrRes == null");
                     //continue;
                     }
                  else {
                     if (arrRes[0].Error != HRESULTS.S_OK) {
                        AddLogg("DoInit:AddItem" + i + " " + sPLC_Adr + " arrRes[0].Error != HRESULTS.S_OK");
                        //continue;
                        }
                     else {
                        itmHandleServer = arrRes[0].HandleServer;
                        itmAccessRights = arrRes[0].AccessRights;
                        itmTypeCode = VT2TypeCode(arrRes[0].CanonicalDataType);

                        txbActPoint.Text = sPLC_Adr;
                        txtItemDataType.Text = DUMMY_VARIANT.VarEnumToString(arrRes[0].CanonicalDataType);

                        //if ((itmAccessRights & OPCACCESSRIGHTS.OPC_READABLE) != 0) {
                        //   int cancelID;
                        //   theGrp.Refresh2(OPCDATASOURCE.OPC_DS_DEVICE,7788,out cancelID);
                        //   }
                        //else {
                        //   txtItemValue.Text = "no read access";
                        //   AddLogg("DoInit:AddItem" + i + " " + sPLC_Adr + " no read access");
                        //   }
                        if (itmTypeCode != TypeCode.Object) {
                           // Object=failed!
                           ////AddLogg("DoInit:AddItem" + i + " " + sPLC_Adr + " Object=failed");
                           // check if write is permitted
                           if ((itmAccessRights & OPCACCESSRIGHTS.OPC_WRITEABLE) != 0) {
                              //btnItemWrite.Enabled = true;
                              ////AddLogg("DoInit:AddItem" + i + " " + sPLC_Adr + " write is permitted");
                              }
                           }
                        itemstrings[0] = sAlias;
                        itemstrings[1] = sPLC_Adr;
                        //listOpcView.Items.Add(new ListViewItem(itemstrings,0));
                        itemclsPointInfo.sAlias = sAlias;
                        string[] sEvtNameSplitted = sAlias.Split('_');
                        //for (k = 0; k < sEvtNameSplitted.GetLength(0); k++){
                        //   AddLogg("DEBUG sEvtNameSplitted[" + k + "] = '" + sEvtNameSplitted[k]+"'");
                        //}
                        if (sEvtNameSplitted[0] == "ACT" || sEvtNameSplitted[0] == "SP") {   // Fool-Proof
                           if (sEvtNameSplitted[1] == "C1" || sEvtNameSplitted[1] == "C2" || sEvtNameSplitted[1] == "C3") {
                              itemclsPointInfo.iCnvNr = Convert.ToInt32(sEvtNameSplitted[1].Substring(1,1));
                              if (sEvtNameSplitted[0] == "SP"){
                                 itemclsPointInfo.sAliasInfoTeil = itemclsPointInfo.sAlias.Substring(6,itemclsPointInfo.sAlias.Length -6);
                              }
                              else{
                                 itemclsPointInfo.sAliasInfoTeil = itemclsPointInfo.sAlias.Substring(7,itemclsPointInfo.sAlias.Length - 7);
                                 }
                           }
                           else {
                              itemclsPointInfo.iCnvNr = 0;
                              if (sEvtNameSplitted[0] == "SP"){
                                 itemclsPointInfo.sAliasInfoTeil = itemclsPointInfo.sAlias.Substring(3,itemclsPointInfo.sAlias.Length -3);
                              }
                              else{
                                 itemclsPointInfo.sAliasInfoTeil = itemclsPointInfo.sAlias.Substring(4,itemclsPointInfo.sAlias.Length - 4);
                                 }
                           }
                           if (sEvtNameSplitted[0] == "SP"){
                              itemclsPointInfo.bIsSetPoint = true;
                           }
                        }
                        else {
                           AddLogg("DoInit:AddItem" + i + " " + sAlias + " не начинается с 'ACT_'/'SP_'");
                           }
                        itemclsPointInfo.sPLC_Adr = sPLC_Adr;
                        itemclsPointInfo.sDesc = sDesc;
                        itemclsPointInfo.CanonicalDataType = arrRes[0].CanonicalDataType;
                        itemclsPointInfo.sCanonicalDataType = DUMMY_VARIANT.VarEnumToString(arrRes[0].CanonicalDataType);
                        itemclsPointInfo.AccessRights = arrRes[0].AccessRights;
                        itemclsPointInfo.itmTypeCode = itmTypeCode;

                        pointLi.Add(itemclsPointInfo);
                        cbCtrlAliases.Items.Add(sAlias + " " + sPLC_Adr);
                        // Ведём и группы тоже
                        //k = groupLi.FindIndex(delegate(clsGroupInfo lfdGroupInfo) {  // ** это идентично
                        //                         return lfdGroupInfo.sName == sGroup;
                        //                         }
                        //                     );
                        string @group = sGroup;                                        // ** этому !!!
                        k = groupLi.FindIndex(lfdGroupInfo => lfdGroupInfo.sName == group);
                        if (k < 0) {                                           // это начало новой группы, соответственно, это TAKE_OVER !!
                           itemclsGroupInfo = new clsGroupInfo();
                           itemclsGroupInfo.sName = sGroup;
                           groupLi.Add(itemclsGroupInfo);
                           pointLi[pointLi.Count - 1].bIsTakeOver = true;  // rueckwerts in PointLi schreiben
                           }
                        k = pointLi.Count - 1;                             // wegen DEBUG
                        pointLi[k].iIdxInGroupLi = groupLi.Count - 1;      // gehoert zu dieser Gruppe;rueckwerts in PointLi schreiben
                        k = groupLi.Count - 1;                             // wegen DEBUG
                        groupLi[k].iItemsCnt++;                            // so viele Punkte in der Gruppe
                        groupLi[k].iIdxInPointLi.Add(pointLi.Count - 1);   // eigentlisch Punkte(als pointLi[Index])
                        DoStoreToOracle(j);                                // AnfangsZustand in Oracle generieren
                        AddLogg("Добавлен Pkt" + j + " group '" + groupLi[k].sName + "',Alias'" + sAlias + "' " + sPLC_Adr);
                        j++;
                        }
                     }
                  }
               catch (/*COMException*/ Exception comExc)
               {
                  //MessageBox.Show(this,"AddItem " + sPLC_Adr + " OPC error!","DoInit",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                  AddLogg("DoInit:AddItem" + i + " '" + sPLC_Adr + "' OPC exception:" + comExc.Message);
                  //return false;
                  continue;
                  }
               }
            int cancelID;
            theGrp.Refresh2(OPCDATASOURCE.OPC_DS_DEVICE,7788,out cancelID);
            }
         catch (Exception e) {		// exceptions MUST be handled
            //MessageBox.Show(this,"init error! " + e.ToString(),"Exception",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            AddLogg("DoInit:AddItem" + i + " '" + sPLC_Adr + "' init error! " + e.Message);
            AddLogg("DoInit: Ende - false");
            return false;
            }
         AddLogg("DoInit: Ende-true,в pointLi " + pointLi.Count + " в groupLi " + groupLi.Count);
         mainGate = new MainGateClient(new InstanceContext(new DummyListener()));

         return true;
         }

      // connect to OPC server via ProgID
      public bool DoConnect(string progid) {
         try {
            theSrv.Connect(progid);
            Thread.Sleep(100);
            theSrv.SetClientName("DirectOPC " + thisprocess.Id);	// set my client name (exe+process no)

            SERVERSTATUS sts;
            theSrv.GetStatus(out sts);

            // get infos about OPC server
            StringBuilder sb = new StringBuilder(sts.szVendorInfo,200);
            sb.AppendFormat(" ver:{0}.{1}.{2}",sts.wMajorVersion,sts.wMinorVersion,sts.wBuildNumber);
            txtServerInfo.Text = sb.ToString();
            AddLogg(sts.eServerState.ToString());
            }
         catch (COMException exc) {
            AddLogg("connect error!Exception: " + exc.Message);
            MessageBox.Show(this,"connect error!","Exception",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return false;
            }
         return true;
         }


      public bool CreateGroup() {
         try {
            // add our only working group
            theGrp = theSrv.AddGroup("OPCdotNET-Group",true,500);
            
            // add event handler for data changes
            theGrp.DataChanged += new DataChangeEventHandler(this.theGrp_DataChange);
            theGrp.WriteCompleted += new WriteCompleteEventHandler(this.theGrp_WriteComplete);
            }
         catch (COMException exc) {
            AddLogg("connect error!Exception: " + exc.Message);
            MessageBox.Show(this,"create group error!","Exception",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return false;
            }
         return true;
         }

      // event handler: called if any item in group has changed values
      protected void theGrp_DataChange(object sender,DataChangeEventArgs e) {
         string sPLC_Adr,sAlias,sP;

         lock (lockThis) {             // Иначе может начаться новый поток и рассинхронизируются типы и вообще всё
           
             iThreadCnt++;
            DateTime dtBeg = DateTime.Now;
            //Trace.WriteLine("theGrp_DataChange  id=" + e.transactionID.ToString() + " me=0x" + e.masterError.ToString("X"));
            //listOpcView.BeginUpdate(); 
            //AddLogg("iThreadCnt=" + iThreadCnt + " Id " + Process.GetCurrentProcess().Id + ",в событии " + e.sts.GetLength(0) +
            //        " элементов");
            foreach (OPCItemState s in e.sts) {
               try {
                  if (HRESULTS.Succeeded(s.Error)) {
                     //Trace.WriteLine("  val=" + s.DataValue.ToString());
                     if (s.HandleClient < pointLi.Count) {
                        if (s.DataValue != null) {
                           setTextInTxbViaDelegate(ref txtItemValue,s.DataValue.ToString());
                           setTextInTxbViaDelegate(ref txtItemQual,OpcGroup.QualityToString(s.Quality));
                           setTextInTxbViaDelegate(ref txtItemTimeSt,DateTime.FromFileTime(s.TimeStamp).ToString());
                           pointLi[s.HandleClient].opcItem = s;
                           if (s.DataValue.GetType() == typeof(SByte[])) {
                              pointLi[s.HandleClient].sDataValue = ConvertToRus((SByte[])s.DataValue,pointLi[s.HandleClient].iDataLength);
                              }
                           else {
                              if (s.DataValue.GetType() == typeof(DateTime)) {
                                 pointLi[s.HandleClient].dtDataValue = Convert.ToDateTime(s.DataValue);
                                 }
                              else {
                                 pointLi[s.HandleClient].iDataValue = Convert.ToInt32(s.DataValue);
                                 if (pointLi[s.HandleClient].CanonicalDataType == VarEnum.VT_R4) {
                                    pointLi[s.HandleClient].fDataValue = Convert.ToDouble(s.DataValue);
                                    }
                                 }
                              }
                           pointLi[s.HandleClient].iReadCnt++;
                           if (Math.Max(pointLi[s.HandleClient].iReadCnt,pointLi[s.HandleClient].iReadCntMax) >
                               pointLi[s.HandleClient].iReadCntMax) {
                              pointLi[s.HandleClient].iReadCntMax = pointLi[s.HandleClient].iReadCnt;
                              pointLi[s.HandleClient].dtMax = DateTime.Now;
                              }

                           DoStoreToOracle(s.HandleClient);    // das Gelesene rausschmeissen
                           DoStoreToCore(s.HandleClient); // dem Core senden

                           if (sAliasToControl == pointLi[s.HandleClient].sAlias) {    // der Punkt ist controlled
                              setTextInTxbViaDelegate(ref txbCtrlItemDataType,pointLi[s.HandleClient].sCanonicalDataType);
                              setTextInTxbViaDelegate(ref txbCtrlObjType,
                                                      pointLi[s.HandleClient].opcItem.DataValue.GetType().ToString());
                              setTextInTxbViaDelegate(ref txbCtrlItemTimeSt,LoggDateToString());
                              setTextInTxbViaDelegate(ref txbCtrlItemValue,
                                                      pointLi[s.HandleClient].opcItem.DataValue.ToString());
                              if (s.DataValue.GetType() == typeof(SByte[])) {
                                 setTextInTxbViaDelegate(ref txbCtrlItemValueUmc,pointLi[s.HandleClient].sDataValue);
                                 sP = pointLi[s.HandleClient].sDataValue;
                                 }
                              else {
                                 if (s.DataValue.GetType() == typeof(DateTime)) {
                                    setTextInTxbViaDelegate(ref txbCtrlItemValueUmc,
                                                            pointLi[s.HandleClient].dtDataValue.ToString());
                                    sP = pointLi[s.HandleClient].dtDataValue.ToString();
                                    }
                                 else {
                                    setTextInTxbViaDelegate(ref txbCtrlItemValueUmc,
                                                            pointLi[s.HandleClient].iDataValue.ToString());
                                    sP = pointLi[s.HandleClient].iDataValue.ToString();
                                    }
                                 }
                              AddLogg("Pkt" + s.HandleClient + "," + s.DataValue.GetType() + " контроль: '" + sP + "'");
                              setTextInTxbViaDelegate(ref txbCtrlItemQual,
                                                      OpcGroup.QualityToString(pointLi[s.HandleClient].opcItem.Quality));
                              setTextInTxbViaDelegate(ref txbCtrlReadCnt,pointLi[s.HandleClient].iReadCnt.ToString());
                              setTextInTxbViaDelegate(ref txbCtrlReadCntMax,pointLi[s.HandleClient].iReadCntMax.ToString());
                              setTextInTxbViaDelegate(ref txbCtrldtMax,LoggDateToString(pointLi[s.HandleClient].dtMax));
                              }
                           }
                        else {
                           AddLogg("s.HandleClient{" + s.HandleClient + "} s.DataValue == null");
                           }
                        }
                     else {
                        AddLogg("s.HandleClient{" + s.HandleClient + "}>=pointLi.Count{" + pointLi.Count + "}");
                        }
                     }
                  else {
                     AddLogg("s.HandleClient{" + s.HandleClient + "} ERROR 0x" + s.Error.ToString("X"));
                     setTextInTxbViaDelegate(ref txtItemTimeSt,DateTime.FromFileTime(s.TimeStamp).ToString());
                     setTextInTxbViaDelegate(ref txtItemQual,"error");
                     setTextInTxbViaDelegate(ref txtItemValue,"ERROR 0x" + s.Error.ToString("X"));
                     }
                  }
               catch (Exception exc) {
                  AddLogg("Pkt" + s.HandleClient + "," + s.DataValue.GetType() + " " + exc.Message);
                  //listOpcView.EndUpdate();
                  }
               }
            //listOpcView.EndUpdate();

            //AddLogg("iThreadCnt=" + iThreadCnt + ",событие отработано " + e.sts.GetLength(0) + " элементов " +
            //        (DateTime.Now - dtBeg).Milliseconds.ToString() + " ms");
            iThreadCnt--;
            }
         }
      // das Gelesene rausschmeissen
      private bool DoStoreToOracle(int iIdxInPointLi) {
         bool bErg = true;

         try {
            if (pointLi[iIdxInPointLi].bIsTakeOver) {  // jetzt: Action!
               foreach (var iLfdIdxInPointLi in groupLi[pointLi[iIdxInPointLi].iIdxInGroupLi].iIdxInPointLi) {
                  try {
                     itemclsPointInfo = pointLi[iLfdIdxInPointLi];   // wegen DEBUG
                     pointLi[iLfdIdxInPointLi].iReadCnt = 0;         // merker: jetzt wurde gespeichert
                     if (OraConn == null) {
                        OraConn = new OracleConnection { ConnectionString = "data source=SMKBOFnew;user id=SMK;password=SMK;" };
                        }
                     else {
                        OraConn.Close();
                        }
                     OraCmd = OraConn.CreateCommand();
                     OraCmd.CommandText = "pck_opc.StorePLC_Value";
                     OraCmd.CommandType = CommandType.StoredProcedure;
                     OraCmd.Parameters.Add(new OracleParameter("sGroupInp",OracleType.VarChar)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("sAliasInp",OracleType.VarChar)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("sPLC_AdrInp",OracleType.VarChar)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("iIsTakeOverInp",OracleType.Number)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("iTypeCodeInp",OracleType.Number)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("sDataValueInp",OracleType.VarChar)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("dtDataValueInp",OracleType.DateTime)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("iDataValueInp",OracleType.Number)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("dtReadingDateInp",OracleType.DateTime)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("iReadCntInp",OracleType.Number)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("iReadCntMaxInp",OracleType.Number)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("dtMaxInp",OracleType.DateTime)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters.Add(new OracleParameter("sDescInp",OracleType.VarChar)).Direction = ParameterDirection.Input;
                     OraCmd.Parameters["sGroupInp"].Value = groupLi[itemclsPointInfo.iIdxInGroupLi].sName;
                     OraCmd.Parameters["sAliasInp"].Value = itemclsPointInfo.sAlias;
                     OraCmd.Parameters["sPLC_AdrInp"].Value = itemclsPointInfo.sPLC_Adr;
                     OraCmd.Parameters["iIsTakeOverInp"].Value = (itemclsPointInfo.bIsTakeOver) ? 1 : 0;
                     OraCmd.Parameters["sDescInp"].Value = itemclsPointInfo.sDesc;

                     OraCmd.Parameters["iTypeCodeInp"].Value = 2;
                     OraCmd.Parameters["sDataValueInp"].Value = string.Empty;
                     OraCmd.Parameters["dtDataValueInp"].Value = DateTime.MinValue;
                     OraCmd.Parameters["iDataValueInp"].Value = int.MinValue;
                     OraCmd.Parameters["dtReadingDateInp"].Value = DateTime.MinValue;
                     OraCmd.Parameters["iReadCntInp"].Value = int.MinValue;
                     OraCmd.Parameters["iReadCntMaxInp"].Value = int.MinValue;
                     OraCmd.Parameters["dtMaxInp"].Value = DateTime.MinValue;

                     if (itemclsPointInfo.opcItem != null) {
                        if (itemclsPointInfo.opcItem.DataValue != null) {
                           if (itemclsPointInfo.opcItem.DataValue.GetType() == typeof(SByte[])) {
                              OraCmd.Parameters["iTypeCodeInp"].Value = 0;
                              OraCmd.Parameters["sDataValueInp"].Value = itemclsPointInfo.sDataValue;
                              }
                           else {
                              if (itemclsPointInfo.opcItem.DataValue.GetType() == typeof(DateTime)) {
                                 OraCmd.Parameters["iTypeCodeInp"].Value = 1;
                                 OraCmd.Parameters["dtDataValueInp"].Value = itemclsPointInfo.dtDataValue;
                                 }
                              else {
                                 OraCmd.Parameters["iTypeCodeInp"].Value = 2;
                                 OraCmd.Parameters["iDataValueInp"].Value = itemclsPointInfo.iDataValue;
                                 }
                              }
                           OraCmd.Parameters["dtReadingDateInp"].Value = DateTime.FromFileTime(itemclsPointInfo.opcItem.TimeStamp);
                           OraCmd.Parameters["iReadCntInp"].Value = itemclsPointInfo.iReadCnt;
                           OraCmd.Parameters["iReadCntMaxInp"].Value = itemclsPointInfo.iReadCntMax;
                           OraCmd.Parameters["dtMaxInp"].Value = itemclsPointInfo.dtMax;
                           }
                        }
                     OraCmd.Connection.Open();
                     OraCmd.ExecuteNonQuery();
                     }
                  catch (Exception ex) {
                     bErg = false;
                     sP = "Ошибка доступа: "
                        + ((OraCmd.CommandText == null) ? "OraCmd.CommandText==null" : OraCmd.CommandText)
                        + " ,Exc: " + ex.Message;
                     AddLogg(sP);
                     }
                  if (OraConn != null) OraConn.Close();

                  }
               }
            }
         catch (Exception ex) {
            bErg = false;
            sP = "Ошибка записи pointLi[" + iIdxInPointLi + "],Exc: " + ex.Message;
            AddLogg(sP);
            }
         return bErg;
         }

      private void cbCtrlAliases_SelectedValueChanged(object sender,EventArgs e) {

         string[] sSel = cbCtrlAliases.SelectedItem.ToString().Split(' ');
         sAliasToControl = sSel[0].Trim();
         AddLogg("Выбрана точка " + sAliasToControl);
         txbCtrlItemDataType.Text = "";
         txbCtrlObjType.Text = "";
         txbCtrlItemTimeSt.Text = "";
         txbCtrlItemValue.Text = "";
         txbCtrlItemValueUmc.Text = "";
         txbCtrlItemQual.Text = "";
         txbCtrlReadCnt.Text = "";
         txbCtrlReadCntMax.Text = "";
         txbCtrldtMax.Text = "";
         txbEventText.Text = "";
         txbCoreResult.Text = "";
         }

      // event handler: called if asynch write finished
      protected void theGrp_WriteComplete(object sender,WriteCompleteEventArgs e) {

         foreach (OPCWriteResult w in e.res) {
            //if (HRESULTS.Failed(w.Error))
            //   //txtItemWriteRes.Text = "ERROR 0x" + w.Error.ToString("X");
            //else
            //   //txtItemWriteRes.Text = "ok";
            }
         }

      //------------------------ это только если 1 точка в группе !!!
      // opcid    "S7:[PLC31]db2,int12"
      public bool ViewItem(string opcid) {

         try {
            RemoveItem();		// first remove previous item if any

            itmHandleClient = 1234;
            OPCItemDef[] aD = new OPCItemDef[1];
            aD[0] = new OPCItemDef(opcid,true,itmHandleClient,VarEnum.VT_EMPTY);
            OPCItemResult[] arrRes;
            theGrp.AddItems(aD,out arrRes);
            if (arrRes == null)
               return false;
            if (arrRes[0].Error != HRESULTS.S_OK)
               return false;

            itmHandleServer = arrRes[0].HandleServer;
            itmAccessRights = arrRes[0].AccessRights;
            itmTypeCode = VT2TypeCode(arrRes[0].CanonicalDataType);
            txtItemDataType.Text = DUMMY_VARIANT.VarEnumToString(arrRes[0].CanonicalDataType);

            if ((itmAccessRights & OPCACCESSRIGHTS.OPC_READABLE) != 0) {
               int cancelID;
               theGrp.Refresh2(OPCDATASOURCE.OPC_DS_DEVICE,7788,out cancelID);
               }
            else
               txtItemValue.Text = "no read access";

            if (itmTypeCode != TypeCode.Object)				// Object=failed!
				{
               // check if write is premitted
               //if ((itmAccessRights & OPCACCESSRIGHTS.OPC_WRITEABLE) != 0)
               //btnItemWrite.Enabled = true;
               }
            }
         catch (COMException) {
            MessageBox.Show(this,"AddItem " + opcid + " OPC error!","ViewItem",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return false;
            }
         return true;
         }
      // remove previous OPC item if any
      public bool RemoveItem() {
         try {
            if (itmHandleClient != 0) {
               itmHandleClient = 0;
               //txtItemID.Text = "";		// clear screen texts
               txtItemValue.Text = "";
               txtItemDataType.Text = "";
               txtItemQual.Text = "";
               txtItemTimeSt.Text = "";
               //txtItemSendValue.Text = "";
               //txtItemWriteRes.Text = "";
               //btnItemWrite.Enabled = false;
               //btnItemMore.Enabled = false;

               int[] serverhandles = new int[1] { itmHandleServer };
               int[] remerrors;
               theGrp.RemoveItems(serverhandles,out remerrors);
               itmHandleServer = 0;
               }
            }
         catch (COMException) {
            MessageBox.Show(this,"RemoveItem OPC error!","RemoveItem",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return false;
            }
         return true;
         }
      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (disposing) {
            if (components != null) {
               components.Dispose();
               }
            }
         base.Dispose(disposing);
         }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
         this.opclisticons = new System.Windows.Forms.ImageList(this.components);
         this.txtServerInfo = new System.Windows.Forms.TextBox();
         this.sbpStatus = new System.Windows.Forms.StatusBarPanel();
         this.opctreeicons = new System.Windows.Forms.ImageList(this.components);
         this.statusBarMain = new System.Windows.Forms.StatusBar();
         this.sbpTime = new System.Windows.Forms.StatusBarPanel();
         this.timerLogg = new System.Windows.Forms.Timer(this.components);
         this.tabControl1 = new System.Windows.Forms.TabControl();
         this.tabPage1 = new System.Windows.Forms.TabPage();
         this.buttProbeSincRead = new System.Windows.Forms.Button();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.cbCtrlAliases = new System.Windows.Forms.ComboBox();
         this.label11 = new System.Windows.Forms.Label();
         this.label10 = new System.Windows.Forms.Label();
         this.txbCtrldtMax = new System.Windows.Forms.TextBox();
         this.label9 = new System.Windows.Forms.Label();
         this.txbCtrlReadCntMax = new System.Windows.Forms.TextBox();
         this.label5 = new System.Windows.Forms.Label();
         this.txbCtrlReadCnt = new System.Windows.Forms.TextBox();
         this.txbCtrlItemQual = new System.Windows.Forms.TextBox();
         this.label12 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.txbCtrlObjType = new System.Windows.Forms.TextBox();
         this.txbCtrlItemDataType = new System.Windows.Forms.TextBox();
         this.txbCtrlItemValueUmc = new System.Windows.Forms.TextBox();
         this.txbCtrlItemValue = new System.Windows.Forms.TextBox();
         this.label7 = new System.Windows.Forms.Label();
         this.txbCtrlItemTimeSt = new System.Windows.Forms.TextBox();
         this.label8 = new System.Windows.Forms.Label();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.label3 = new System.Windows.Forms.Label();
         this.txtItemQual = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.txtItemDataType = new System.Windows.Forms.TextBox();
         this.txtItemValue = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.txtItemTimeSt = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.txbActPoint = new System.Windows.Forms.TextBox();
         this.tabPage2 = new System.Windows.Forms.TabPage();
         this.lbLogg = new System.Windows.Forms.ListBox();
         this.label13 = new System.Windows.Forms.Label();
         this.txbEventText = new System.Windows.Forms.TextBox();
         this.label14 = new System.Windows.Forms.Label();
         this.txbCoreResult = new System.Windows.Forms.TextBox();
         ((System.ComponentModel.ISupportInitialize)(this.sbpStatus)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.sbpTime)).BeginInit();
         this.tabControl1.SuspendLayout();
         this.tabPage1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox1.SuspendLayout();
         this.tabPage2.SuspendLayout();
         this.SuspendLayout();
         // 
         // opclisticons
         // 
         this.opclisticons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("opclisticons.ImageStream")));
         this.opclisticons.TransparentColor = System.Drawing.Color.Transparent;
         this.opclisticons.Images.SetKeyName(0,"");
         this.opclisticons.Images.SetKeyName(1,"");
         // 
         // txtServerInfo
         // 
         this.txtServerInfo.Location = new System.Drawing.Point(6,6);
         this.txtServerInfo.Name = "txtServerInfo";
         this.txtServerInfo.ReadOnly = true;
         this.txtServerInfo.Size = new System.Drawing.Size(440,20);
         this.txtServerInfo.TabIndex = 1;
         this.txtServerInfo.Text = "serverinfo";
         // 
         // sbpStatus
         // 
         this.sbpStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
         this.sbpStatus.MinWidth = 32;
         this.sbpStatus.Name = "sbpStatus";
         this.sbpStatus.Text = "1";
         this.sbpStatus.Width = 478;
         // 
         // opctreeicons
         // 
         this.opctreeicons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("opctreeicons.ImageStream")));
         this.opctreeicons.TransparentColor = System.Drawing.Color.Transparent;
         this.opctreeicons.Images.SetKeyName(0,"");
         this.opctreeicons.Images.SetKeyName(1,"");
         // 
         // statusBarMain
         // 
         this.statusBarMain.Location = new System.Drawing.Point(0,491);
         this.statusBarMain.Name = "statusBarMain";
         this.statusBarMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpTime,
            this.sbpStatus});
         this.statusBarMain.ShowPanels = true;
         this.statusBarMain.Size = new System.Drawing.Size(608,16);
         this.statusBarMain.SizingGrip = false;
         this.statusBarMain.TabIndex = 9;
         // 
         // sbpTime
         // 
         this.sbpTime.MinWidth = 20;
         this.sbpTime.Name = "sbpTime";
         this.sbpTime.Text = "0";
         this.sbpTime.Width = 130;
         // 
         // timerLogg
         // 
         this.timerLogg.Tick += new System.EventHandler(this.timerLogg_Tick);
         // 
         // tabControl1
         // 
         this.tabControl1.Controls.Add(this.tabPage1);
         this.tabControl1.Controls.Add(this.tabPage2);
         this.tabControl1.Location = new System.Drawing.Point(6,30);
         this.tabControl1.Name = "tabControl1";
         this.tabControl1.SelectedIndex = 0;
         this.tabControl1.Size = new System.Drawing.Size(597,455);
         this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
         this.tabControl1.TabIndex = 20;
         // 
         // tabPage1
         // 
         this.tabPage1.BackColor = System.Drawing.Color.White;
         this.tabPage1.Controls.Add(this.buttProbeSincRead);
         this.tabPage1.Controls.Add(this.groupBox2);
         this.tabPage1.Controls.Add(this.groupBox1);
         this.tabPage1.Location = new System.Drawing.Point(4,22);
         this.tabPage1.Name = "tabPage1";
         this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
         this.tabPage1.Size = new System.Drawing.Size(589,429);
         this.tabPage1.TabIndex = 0;
         this.tabPage1.Text = "Опросы";
         // 
         // buttProbeSincRead
         // 
         this.buttProbeSincRead.Location = new System.Drawing.Point(361,13);
         this.buttProbeSincRead.Name = "buttProbeSincRead";
         this.buttProbeSincRead.Size = new System.Drawing.Size(222,23);
         this.buttProbeSincRead.TabIndex = 17;
         this.buttProbeSincRead.Text = "Синхронное чтение";
         this.buttProbeSincRead.UseVisualStyleBackColor = true;
         this.buttProbeSincRead.Visible = false;
         this.buttProbeSincRead.Click += new System.EventHandler(this.buttProbeSincRead_Click);
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.label14);
         this.groupBox2.Controls.Add(this.txbCoreResult);
         this.groupBox2.Controls.Add(this.label13);
         this.groupBox2.Controls.Add(this.txbEventText);
         this.groupBox2.Controls.Add(this.cbCtrlAliases);
         this.groupBox2.Controls.Add(this.label11);
         this.groupBox2.Controls.Add(this.label10);
         this.groupBox2.Controls.Add(this.txbCtrldtMax);
         this.groupBox2.Controls.Add(this.label9);
         this.groupBox2.Controls.Add(this.txbCtrlReadCntMax);
         this.groupBox2.Controls.Add(this.label5);
         this.groupBox2.Controls.Add(this.txbCtrlReadCnt);
         this.groupBox2.Controls.Add(this.txbCtrlItemQual);
         this.groupBox2.Controls.Add(this.label12);
         this.groupBox2.Controls.Add(this.label6);
         this.groupBox2.Controls.Add(this.txbCtrlObjType);
         this.groupBox2.Controls.Add(this.txbCtrlItemDataType);
         this.groupBox2.Controls.Add(this.txbCtrlItemValueUmc);
         this.groupBox2.Controls.Add(this.txbCtrlItemValue);
         this.groupBox2.Controls.Add(this.label7);
         this.groupBox2.Controls.Add(this.txbCtrlItemTimeSt);
         this.groupBox2.Controls.Add(this.label8);
         this.groupBox2.Location = new System.Drawing.Point(11,155);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(572,268);
         this.groupBox2.TabIndex = 16;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Контролируем точку...";
         // 
         // cbCtrlAliases
         // 
         this.cbCtrlAliases.FormattingEnabled = true;
         this.cbCtrlAliases.Location = new System.Drawing.Point(5,18);
         this.cbCtrlAliases.Name = "cbCtrlAliases";
         this.cbCtrlAliases.Size = new System.Drawing.Size(561,21);
         this.cbCtrlAliases.TabIndex = 21;
         this.cbCtrlAliases.SelectedValueChanged += new System.EventHandler(this.cbCtrlAliases_SelectedValueChanged);
         // 
         // label11
         // 
         this.label11.Location = new System.Drawing.Point(6,170);
         this.label11.Name = "label11";
         this.label11.Size = new System.Drawing.Size(86,28);
         this.label11.TabIndex = 20;
         this.label11.Text = "время мах незап.чтений";
         this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label10
         // 
         this.label10.Location = new System.Drawing.Point(1,158);
         this.label10.Name = "label10";
         this.label10.Size = new System.Drawing.Size(101,16);
         this.label10.TabIndex = 20;
         this.label10.Text = "max.незап.чтений";
         this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txbCtrldtMax
         // 
         this.txbCtrldtMax.Location = new System.Drawing.Point(117,179);
         this.txbCtrldtMax.Name = "txbCtrldtMax";
         this.txbCtrldtMax.ReadOnly = true;
         this.txbCtrldtMax.Size = new System.Drawing.Size(449,20);
         this.txbCtrldtMax.TabIndex = 19;
         // 
         // label9
         // 
         this.label9.Location = new System.Drawing.Point(1,138);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(78,16);
         this.label9.TabIndex = 20;
         this.label9.Text = "незап.чтений";
         this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txbCtrlReadCntMax
         // 
         this.txbCtrlReadCntMax.Location = new System.Drawing.Point(117,159);
         this.txbCtrlReadCntMax.Name = "txbCtrlReadCntMax";
         this.txbCtrlReadCntMax.ReadOnly = true;
         this.txbCtrlReadCntMax.Size = new System.Drawing.Size(449,20);
         this.txbCtrlReadCntMax.TabIndex = 19;
         // 
         // label5
         // 
         this.label5.Location = new System.Drawing.Point(1,118);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(40,16);
         this.label5.TabIndex = 20;
         this.label5.Text = "quality";
         this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txbCtrlReadCnt
         // 
         this.txbCtrlReadCnt.Location = new System.Drawing.Point(117,139);
         this.txbCtrlReadCnt.Name = "txbCtrlReadCnt";
         this.txbCtrlReadCnt.ReadOnly = true;
         this.txbCtrlReadCnt.Size = new System.Drawing.Size(449,20);
         this.txbCtrlReadCnt.TabIndex = 19;
         // 
         // txbCtrlItemQual
         // 
         this.txbCtrlItemQual.Location = new System.Drawing.Point(117,119);
         this.txbCtrlItemQual.Name = "txbCtrlItemQual";
         this.txbCtrlItemQual.ReadOnly = true;
         this.txbCtrlItemQual.Size = new System.Drawing.Size(449,20);
         this.txbCtrlItemQual.TabIndex = 19;
         // 
         // label12
         // 
         this.label12.Location = new System.Drawing.Point(2,59);
         this.label12.Name = "label12";
         this.label12.Size = new System.Drawing.Size(48,16);
         this.label12.TabIndex = 18;
         this.label12.Text = "Obj-type";
         this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // label6
         // 
         this.label6.Location = new System.Drawing.Point(-1,43);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(58,16);
         this.label6.TabIndex = 18;
         this.label6.Text = "OPC type";
         this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txbCtrlObjType
         // 
         this.txbCtrlObjType.Location = new System.Drawing.Point(117,59);
         this.txbCtrlObjType.Name = "txbCtrlObjType";
         this.txbCtrlObjType.ReadOnly = true;
         this.txbCtrlObjType.Size = new System.Drawing.Size(449,20);
         this.txbCtrlObjType.TabIndex = 17;
         // 
         // txbCtrlItemDataType
         // 
         this.txbCtrlItemDataType.Location = new System.Drawing.Point(117,39);
         this.txbCtrlItemDataType.Name = "txbCtrlItemDataType";
         this.txbCtrlItemDataType.ReadOnly = true;
         this.txbCtrlItemDataType.Size = new System.Drawing.Size(449,20);
         this.txbCtrlItemDataType.TabIndex = 17;
         // 
         // txbCtrlItemValueUmc
         // 
         this.txbCtrlItemValueUmc.Location = new System.Drawing.Point(309,99);
         this.txbCtrlItemValueUmc.Name = "txbCtrlItemValueUmc";
         this.txbCtrlItemValueUmc.ReadOnly = true;
         this.txbCtrlItemValueUmc.Size = new System.Drawing.Size(257,20);
         this.txbCtrlItemValueUmc.TabIndex = 15;
         // 
         // txbCtrlItemValue
         // 
         this.txbCtrlItemValue.Location = new System.Drawing.Point(117,99);
         this.txbCtrlItemValue.Name = "txbCtrlItemValue";
         this.txbCtrlItemValue.ReadOnly = true;
         this.txbCtrlItemValue.Size = new System.Drawing.Size(186,20);
         this.txbCtrlItemValue.TabIndex = 15;
         // 
         // label7
         // 
         this.label7.Location = new System.Drawing.Point(1,100);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(35,16);
         this.label7.TabIndex = 16;
         this.label7.Text = "value";
         this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txbCtrlItemTimeSt
         // 
         this.txbCtrlItemTimeSt.Location = new System.Drawing.Point(117,79);
         this.txbCtrlItemTimeSt.Name = "txbCtrlItemTimeSt";
         this.txbCtrlItemTimeSt.ReadOnly = true;
         this.txbCtrlItemTimeSt.Size = new System.Drawing.Size(449,20);
         this.txbCtrlItemTimeSt.TabIndex = 14;
         // 
         // label8
         // 
         this.label8.Location = new System.Drawing.Point(5,80);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(26,16);
         this.label8.TabIndex = 13;
         this.label8.Text = "time";
         this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.label3);
         this.groupBox1.Controls.Add(this.txtItemQual);
         this.groupBox1.Controls.Add(this.label2);
         this.groupBox1.Controls.Add(this.txtItemDataType);
         this.groupBox1.Controls.Add(this.txtItemValue);
         this.groupBox1.Controls.Add(this.label4);
         this.groupBox1.Controls.Add(this.txtItemTimeSt);
         this.groupBox1.Controls.Add(this.label1);
         this.groupBox1.Controls.Add(this.txbActPoint);
         this.groupBox1.Location = new System.Drawing.Point(6,13);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(290,129);
         this.groupBox1.TabIndex = 15;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Текущий опрос";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(1,99);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(40,16);
         this.label3.TabIndex = 20;
         this.label3.Text = "quality";
         this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txtItemQual
         // 
         this.txtItemQual.Location = new System.Drawing.Point(97,99);
         this.txtItemQual.Name = "txtItemQual";
         this.txtItemQual.ReadOnly = true;
         this.txtItemQual.Size = new System.Drawing.Size(187,20);
         this.txtItemQual.TabIndex = 19;
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(1,43);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(54,16);
         this.label2.TabIndex = 18;
         this.label2.Text = "OPC type";
         this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txtItemDataType
         // 
         this.txtItemDataType.Location = new System.Drawing.Point(97,39);
         this.txtItemDataType.Name = "txtItemDataType";
         this.txtItemDataType.ReadOnly = true;
         this.txtItemDataType.Size = new System.Drawing.Size(187,20);
         this.txtItemDataType.TabIndex = 17;
         // 
         // txtItemValue
         // 
         this.txtItemValue.Location = new System.Drawing.Point(97,79);
         this.txtItemValue.Name = "txtItemValue";
         this.txtItemValue.ReadOnly = true;
         this.txtItemValue.Size = new System.Drawing.Size(187,20);
         this.txtItemValue.TabIndex = 15;
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(1,81);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(35,16);
         this.label4.TabIndex = 16;
         this.label4.Text = "value";
         this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txtItemTimeSt
         // 
         this.txtItemTimeSt.Location = new System.Drawing.Point(97,59);
         this.txtItemTimeSt.Name = "txtItemTimeSt";
         this.txtItemTimeSt.ReadOnly = true;
         this.txtItemTimeSt.Size = new System.Drawing.Size(187,20);
         this.txtItemTimeSt.TabIndex = 14;
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(3,61);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(26,16);
         this.label1.TabIndex = 13;
         this.label1.Text = "time";
         this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // txbActPoint
         // 
         this.txbActPoint.Location = new System.Drawing.Point(4,19);
         this.txbActPoint.Name = "txbActPoint";
         this.txbActPoint.ReadOnly = true;
         this.txbActPoint.Size = new System.Drawing.Size(280,20);
         this.txbActPoint.TabIndex = 5;
         // 
         // tabPage2
         // 
         this.tabPage2.Controls.Add(this.lbLogg);
         this.tabPage2.Location = new System.Drawing.Point(4,22);
         this.tabPage2.Name = "tabPage2";
         this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
         this.tabPage2.Size = new System.Drawing.Size(589,422);
         this.tabPage2.TabIndex = 1;
         this.tabPage2.Text = "Logg";
         this.tabPage2.UseVisualStyleBackColor = true;
         // 
         // lbLogg
         // 
         this.lbLogg.FormattingEnabled = true;
         this.lbLogg.Location = new System.Drawing.Point(0,3);
         this.lbLogg.Name = "lbLogg";
         this.lbLogg.Size = new System.Drawing.Size(586,368);
         this.lbLogg.TabIndex = 0;
         // 
         // label13
         // 
         this.label13.Location = new System.Drawing.Point(4,201);
         this.label13.Name = "label13";
         this.label13.Size = new System.Drawing.Size(101,16);
         this.label13.TabIndex = 23;
         this.label13.Text = "Event-Text";
         // 
         // txbEventText
         // 
         this.txbEventText.Location = new System.Drawing.Point(117,199);
         this.txbEventText.Name = "txbEventText";
         this.txbEventText.ReadOnly = true;
         this.txbEventText.Size = new System.Drawing.Size(449,20);
         this.txbEventText.TabIndex = 22;
         // 
         // label14
         // 
         this.label14.Location = new System.Drawing.Point(4,220);
         this.label14.Name = "label14";
         this.label14.Size = new System.Drawing.Size(101,16);
         this.label14.TabIndex = 25;
         this.label14.Text = "Core-Result";
         // 
         // txbCoreResult
         // 
         this.txbCoreResult.Location = new System.Drawing.Point(117,219);
         this.txbCoreResult.Name = "txbCoreResult";
         this.txbCoreResult.ReadOnly = true;
         this.txbCoreResult.Size = new System.Drawing.Size(449,20);
         this.txbCoreResult.TabIndex = 24;
         // 
         // MainForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
         this.BackColor = System.Drawing.Color.White;
         this.ClientSize = new System.Drawing.Size(608,507);
         this.Controls.Add(this.tabControl1);
         this.Controls.Add(this.txtServerInfo);
         this.Controls.Add(this.statusBarMain);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "MainForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "OPC.NET: Переработка интерфейса PLC(DB1/2)";
         this.Activated += new System.EventHandler(this.MainForm_Activated);
         this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
         ((System.ComponentModel.ISupportInitialize)(this.sbpStatus)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.sbpTime)).EndInit();
         this.tabControl1.ResumeLayout(false);
         this.tabPage1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.tabPage2.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

         }
      #endregion

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main() {
         Application.Run(new MainForm());
         }

      private void MainForm_Activated(object sender,System.EventArgs e) {

         if (!first_activated) {		   // workaround to show SelServer form !after! MainForm is visible
            first_activated = true;
            //Top += 200;
            Left = 0;
            Top = 0;
            tLoggToFile = new Thread(new ThreadStart(ThreadProc));
            tLoggToFile.Start();
            //txtItemWriteRes.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() +
            //                       ":tLoggToFile.ManagedThreadId " + tLoggToFile.ManagedThreadId.ToString();
            Thread.Sleep(0);           // damit Thread wenigstens 1 mal durchlaeufe
            InitAdditionsLists();
            opc_connected = DoInit();
            }
         }


      public TypeCode VT2TypeCode(VarEnum vevt) {
         switch (vevt) {
            case VarEnum.VT_I1:
               return TypeCode.SByte;
            case VarEnum.VT_I2:
               return TypeCode.Int16;
            case VarEnum.VT_I4:
               return TypeCode.Int32;
            case VarEnum.VT_I8:
               return TypeCode.Int64;

            case VarEnum.VT_UI1:
               return TypeCode.Byte;
            case VarEnum.VT_UI2:
               return TypeCode.UInt16;
            case VarEnum.VT_UI4:
               return TypeCode.UInt32;
            case VarEnum.VT_UI8:
               return TypeCode.UInt64;

            case VarEnum.VT_R4:
               return TypeCode.Single;
            case VarEnum.VT_R8:
               return TypeCode.Double;

            case VarEnum.VT_BSTR:
               return TypeCode.String;
            case VarEnum.VT_BOOL:
               return TypeCode.Boolean;
            case VarEnum.VT_DATE:
               return TypeCode.DateTime;
            case VarEnum.VT_DECIMAL:
               return TypeCode.Decimal;
            case VarEnum.VT_CY:				// not supported
               return TypeCode.Double;
            }

         return TypeCode.Object;
         }

      private void MainForm_Closing(object sender,System.ComponentModel.CancelEventArgs e) {
         if (!opc_connected)
            return;

         if (theGrp != null) {
            theGrp.DataChanged -= new DataChangeEventHandler(this.theGrp_DataChange);
            theGrp.WriteCompleted -= new WriteCompleteEventHandler(this.theGrp_WriteComplete);
            RemoveItem();
            theGrp.Remove(false);
            theGrp = null;
            }

         if (theSrv != null) {
            theSrv.Disconnect();				// should clean up
            theSrv = null;
            }

         opc_connected = false;
         }

      private void btnItemWrite_Click(object sender,System.EventArgs e) {
         try {
            //txtItemWriteRes.Text = "";

            // convert the user text to OPC data type of item
            object[] arrVal = new Object[1];
            //arrVal[0] = Convert.ChangeType(txtItemSendValue.Text,itmTypeCode);

            int[] serverhandles = new int[1] { itmHandleServer };
            int cancelID;
            int[] arrErr;
            theGrp.Write(serverhandles,arrVal,9988,out cancelID,out arrErr);

            GC.Collect();		// just for fun
            }
         catch (FormatException) {
            MessageBox.Show(this,"Invalid data format!","opcItemDoWrite_Click",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return;
            }
         catch (OverflowException) {
            MessageBox.Show(this,"Invalid data range/overflow!","opcItemDoWrite_Click",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return;
            }
         catch (COMException) {
            MessageBox.Show(this,"OPC Write Item error!","opcItemDoWrite_Click",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return;
            }
         }

      private void btnItemMore_Click(object sender,System.EventArgs e) {
         PropsForm frmProps = new PropsForm(ref theSrv,itmFullID);		// create item properties form
         frmProps.ShowDialog(this);
         }

      private void btnAbout_Click(object sender,System.EventArgs e) {
         AboutForm frmAbout = new AboutForm();			// show about dialog
         frmAbout.ShowDialog(this);
         }

      private bool LoadDefinitions() {
         //string[] itemstrings = new string[5];
         //itemstrings[0] = "DB2,INT12";
         //itemstrings[1] = "S7:[PLC11]db2,int12";
         string sPLC_Adr,sAlias = "",sGroup = "",sDesc = "";
         int iLocationsCnt;
         bool bErg = true;

         //itemstrings[2] = itemstrings[3] = itemstrings[4] = "-";
         try {
            //using (StreamReader sr = new StreamReader("ODB_POINTDEF_AutoCreation.txt",System.Text.Encoding.ASCII)) {
            using (StreamReader sr = new StreamReader("ODB_POINTDEF_AutoCreation.txt",Encoding.Default)) {
               String line;
               iLocationsCnt = 0;
               while ((line = sr.ReadLine()) != null) {
                  int iPos = line.IndexOf("! ");
                  if (iPos == 0) {
                     sDesc = line.Substring(iPos + 2).Trim();
                     }
                  iPos = line.IndexOf("POINT");
                  if (iPos == 0) {
                     sAlias = line.Substring(iPos + 6).Trim();
                     iLocationsCnt = 0;
                     string[] sAli = sAlias.Split('[');
                     if (sAli.Length > 1) {
                        sAlias = sAli[0].Trim();
                        string[] sAli1 = sAli[1].Split(']');
                        if (!Int32.TryParse(sAli1[0],out iLocationsCnt)) {
                           iLocationsCnt = 0;
                           }
                        }
                     }
                  iPos = line.IndexOf(" LOCATION");   // LOCATION PLC01:DB2,W0 GroupName ACT_TOHMWGT
                  if (iPos >= 0) {
                     iPos = line.IndexOf(" GroupName");
                     if (iPos >= 0) {
                        sGroup = line.Substring(iPos + 10).Trim();
                        line = line.Substring(0,iPos);
                        }
                     else {
                        sGroup = "";
                        }
                     string[] sLoc = line.Trim().Split(' ');
                     //sPLC_Adr = line.Substring(iPos + 9).Trim();  // "PLC01:DB2,W0"
                     sPLC_Adr = sLoc[sLoc.GetLength(0) - 1].Trim().ToUpper();
                     sPLC_Adr = sPLC_Adr.Replace(":","]");
                     sPLC_Adr = sPLC_Adr.Replace("W","int");
                     sPLC_Adr = sPLC_Adr.Replace("STRING","CHAR");
                     sPLC_Adr = "S7:[" + sPLC_Adr;
                     // erst die Adresse auf Plausibilitaet priefen !
                     //listOpcView.Items.Add(new ListViewItem(itemstrings,0));
                     sP = sAlias;
                     if (iLocationsCnt > 0) {
                        sP += sLoc[0].Substring(8,sLoc[0].Length - 8);
                        }
                     strItems_Alias.Add(sP);
                     strItems_adrPLC.Add(sPLC_Adr);
                     strItems_Groups.Add(sGroup);
                     strItems_Desc.Add(sDesc);
                     //AddLogg("Добавлено " + listOpcView.Items.Count + " " + itemstrings[0] + " " + itemstrings[1]);
                     //cbCtrlAliases.Items.Add(sAlias + " " + sPLC_Adr);

                     }
                  }
               }
            }
         catch (Exception ex) {
            MessageBox.Show(this,"Чтой-то с файликом " + ex,"Exception browsing namespace",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            bErg = false;
            }
         return bErg;
         }

      private string LoggDateToString() {
         return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":" +
                String.Format("{0:D3}",DateTime.Now.Millisecond);
         }

      private string LoggDateToString(DateTime dt) {
         return dt.ToShortDateString() + " " + dt.ToLongTimeString() + ":" +
                   String.Format("{0:D3}",dt.Millisecond);
         }

      private void AddLoggA(string sMsg) {


         //String line = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":" +
         //                 String.Format("{0:D3}",DateTime.Now.Millisecond) + " " + sMsg;
         String line = LoggDateToString() + " " + sMsg;
         /*
                           if (iAcktLog == 0) {
                              strLog1.Add(line);
                              iCntLog1++;
                              txtItemSendValue.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":" +
                                                      String.Format("{0:D3}",DateTime.Now.Millisecond) + " strLog1.Count=" + strLog1.Count.ToString();
                              }

                           else {
                              strLog2.Add(line);
                              iCntLog2++;
                              txtItemSendValue.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":" +
                                                      String.Format("{0:D3}",DateTime.Now.Millisecond) + " strLog2.Count=" + strLog2.Count.ToString();
                              }
                           //if (!timerLogg.Enabled)
                           //timerLogg.Enabled = true;
                           txtItemID.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":" +
                                            String.Format("{0:D3}",DateTime.Now.Millisecond) + " strLog1.Count=" + strLog1.Count +
                                            " strLog2.Count=" + strLog2.Count;
                  */
         setTextInStatusBarPanelViaDelegate(0,LoggDateToString());
         setTextInStatusBarPanelViaDelegate(1,sMsg);
         //if (!lbLogg.InvokeRequired) {
         //   lbLogg.Items.Add(LoggDateToString() + " " + sMsg);
         //   }
         //else {
         //   lbLogg.Invoke(lbLoggDelegate,new Object[] { sMsg });
         //   }
         }
      // специально для Delegate "lbLogg" чтобы работать через Invoke
      public void AddlbLoggItemMethod(String myString) {
         lbLogg.Items.Add(myString);
         }

      public void setTextInTxbViaDelegate(ref TextBox Txb,String sTxbText) {

         try {
            if (!Txb.InvokeRequired) {
               Txb.Text = sTxbText;
               }
            else {         // setTextInTxb(ref TextBox Txb, String sTxbText)
               Txb.Invoke(setTextInTxbDelegate,new Object[] { Txb,sTxbText });
               }
            }
         catch (Exception eXc) {       // unterdruecken moeglische Exceptions !

            }
         }
      // специально для Delegate "setTextInTxb" чтобы работать через Invoke
      public void setTextInTxbMethod(ref TextBox Txb,String sTxbText) {

         Txb.Text = sTxbText;
         }

      public void setTextInStatusBarPanelViaDelegate(int iPanelNo,String sTxbText) {

         try {                         // statusBarMain.Panels[0]
            if (!statusBarMain.InvokeRequired) {
               statusBarMain.Panels[iPanelNo].Text = sTxbText;
               }
            else {
               statusBarMain.Invoke(setTextInStatusBarPanelDelegate,new Object[] { iPanelNo,sTxbText });
               }
            }
         catch (Exception eXc) {       // unterdruecken moeglische Exceptions !

            }
         }
      // специально для Delegate "setTextInStatusBarPanel" чтобы работать через Invoke
      public void setTextInStatusBarPanelMethod(int iPanelNo,String sTxbText) {

         statusBarMain.Panels[iPanelNo].Text = sTxbText;
         }

      [HostProtectionAttribute(SecurityAction.LinkDemand,Synchronization = true)]
      private void AddLoggB(string sMsg) {


         String line = LoggDateToString() + " " + sMsg;
         using (StreamWriter sr = new StreamWriter("DirecktOPCClient.log",true)) {
            sr.WriteLine(line);
            }
         setTextInStatusBarPanelViaDelegate(0,LoggDateToString());
         setTextInStatusBarPanelViaDelegate(1,sMsg);
         }

      private void AddLogg(string sMsg) {

         String line = LoggDateToString() + " " + sMsg;
         try {
            if (OraConn == null) {
               OraConn = new OracleConnection { ConnectionString = "data source=SMKBOFnew;user id=SMK;password=SMK;" };
               }
            else {
               OraConn.Close();
               }
            OraCmd = OraConn.CreateCommand();
            OraCmd.CommandText = "TRACE.TRACE_MES";
            OraCmd.CommandType = CommandType.StoredProcedure;
            OraCmd.Parameters.Add(new OracleParameter("vctraceinfo",OracleType.VarChar)).Direction = ParameterDirection.Input;
            OraCmd.Parameters.Add(new OracleParameter("vctracefile",OracleType.VarChar)).Direction = ParameterDirection.Input;
            OraCmd.Parameters.Add(new OracleParameter("iError",OracleType.Number)).Direction = ParameterDirection.Input;
            OraCmd.Parameters["vctraceinfo"].Value = sMsg;
            OraCmd.Parameters["vctracefile"].Value = "DirectOPCClient";
            OraCmd.Parameters["iError"].Value = 0;
            OraCmd.Connection.Open();

            //myReader = OraCmd.ExecuteReader();
            OraCmd.ExecuteNonQuery();
            }
         catch (Exception ex) {
            setTextInStatusBarPanelViaDelegate(0,LoggDateToString());
            sP = "Ошибка доступа: "
               + ((OraCmd.CommandText == null) ? "OraCmd.CommandText==null" : OraCmd.CommandText)
               + " ,Exc: " + ex.Message;
            setTextInStatusBarPanelViaDelegate(1,sP);
            }
         if (OraConn != null) OraConn.Close();
         }

      //private void timerLogg_Tick(object sender,EventArgs e) {

      //   using (StreamWriter sr = new StreamWriter("DirecktOPCClient.log",true)) {
      //      while (true) {
      //         if (iAcktLog == 0) {
      //            iAcktLog = 1;             // auf Zwischenspeicherung (waehrend Fileausgabe) umleiten
      //            if (iCntLog1 > 0) {
      //               iCntLog1 = 0;
      //               while (strLog1.Count > 0) {
      //                  sr.WriteLine(strLog1[0]);
      //                  strLog1.Remove(strLog1[0]);
      //                  }
      //               }
      //            iAcktLog = 0;             // Puffer frei: zurueckschalten
      //            if (iCntLog2 > 0) {           // waehrend AusgabeZeit haette was zwischengespeichert koennen...
      //               iCntLog2 = 0;          // das Zwischengespeichert ausgeben 
      //               while (strLog2.Count > 0) {
      //                  sr.WriteLine(strLog2[0]);
      //                  strLog2.Remove(strLog2[0]);
      //                  }
      //               }
      //            }
      //         if (iCntLog1 == 0) break;    // beide Puffers sind ausgegeben
      //         }
      //      }
      //   timerLogg.Enabled = false;
      //   }

      private void timerLogg_Tick(object sender,EventArgs e) {

         timerLogg.Enabled = false;
         return;                       // -------------------!!!!-----------------
         ////using (StreamWriter sr = new StreamWriter("DirecktOPCClient.log",true)) {
         ////   //            if (iCntLog1 > 0) {
         ////   iCntLog1 = 0;
         ////   while (strLog1.Count > 0) {
         ////      txtItemWriteRes.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":" +
         ////                                 String.Format("{0:D3}",DateTime.Now.Millisecond) + " strLog1.Count=" + strLog1.Count + " strLog2.Count=" + strLog2.Count;
         ////      sr.WriteLine(strLog1[0]);
         ////      strLog1.Remove(strLog1[0]);
         ////      }
         ////   //}
         ////   }
         ////timerLogg.Enabled = false;
         }

      public static void ThreadProc() {

         //txtItemID.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ":" +
         //                 DateTime.Now.Millisecond + " strLog1.Count=" + strLog1.Count +
         //                 DateTime.Now.Millisecond + " strLog2.Count=" + strLog2.Count;

         //using (StreamWriter sr = new StreamWriter("DirecktOPCClient.log",true)) {
         //while (true) {
         if (iAcktLog == 0) {
            iAcktLog = 1;             // auf Zwischenspeicherung (waehrend Fileausgabe) umleiten
            if (iCntLog1 > 0) {
               iCntLog1 = 0;
               while (strLog1.Count > 0) {
                  //sr.WriteLine(strLog1[0]);
                  strLog1.Remove(strLog1[0]);
                  }
               }
            iAcktLog = 0;             // Puffer frei: zurueckschalten
            if (iCntLog2 > 0) {           // waehrend AusgabeZeit haette was zwischengespeichert koennen...
               iCntLog2 = 0;          // das Zwischengespeichert ausgeben 
               while (strLog2.Count > 0) {
                  //sr.WriteLine(strLog2[0]);
                  strLog2.Remove(strLog2[0]);
                  }
               }
            }
         //if (iCntLog1 == 0) break;    // beide Puffers sind ausgegeben
         //}
         //}
         Thread.Sleep(1000);
         }

      private void MainForm_FormClosing(object sender,FormClosingEventArgs e) {

         //t.Join();                      // Call Join(), to wait until ThreadProc ends
         }

      public string ConvertToRus(SByte[] sBytes,int iLength) {
         string sResult = "";

         Byte[] bString = new Byte[iLength];
         for (int i = 0; i < iLength; i++) {
            int iS = Convert.ToInt16(sBytes[i]);
            if (iS < 0)
               iS = 256 + iS;
            bString[i] = (Byte)iS;
            }
         //sResult = ascii.GetString(bString);
         sResult = System.Text.Encoding.GetEncoding("x-cp1251").GetString(bString);
         return sResult;
         }

      public string ConvertToRusA(SByte[] sBytes,int iLength) {
         const string sASCII = "??????????"    // 0-9
                              + "??????????"    // 10-19
                              + "??????????"    // 20-29
                              + "?? !\"#$%&'"    // 30-39 здесь \"
                              + "()*+,-./01"    // 40-49
                              + "23456789:;"    // 50-59
                              + "<=>?@ABCDE"    // 60-69
                              + "FGHIJKLMNO"    // 70-79
                              + "PQRSTUVWXY"    // 80-89
                              + "Z[\\]^_`abc"    // 90-99  здесь \\
                              + "defghijklm"    // 100-109
                              + "nopqrstuvw"    // 110-119
                              + "xyz{|}~?АБ"    // 120-129
                              + "ВГДЕЖЗИЙКЛ"    // 130-139
                              + "МНОПРСТУФХ"    // 140-149
                              + "ЦЧШЩЪЫЬЭЮЯ"    // 150-159
                              + "абвгдежзий"    // 160-169
                              + "клмноп????"    // 170-179
                              + "??????????"    // 180-189
                              + "??????????"    // 190-199
                              + "??????????"    // 200-209
                              + "??????????"    // 210-219
                              + "????рстуфх"    // 220-229
                              + "цчшщъыьэюя"    // 230-239
                              + "ЁёЄє??????"    // 240-249
                              + "???????№??"    // 250-259
;
         string sResult = "";

         for (int i = 0; i < iLength; i++) {
            int iS = Convert.ToInt16(sBytes[i]);
            if (iS < 0)
               iS = 256 + iS;
            sResult += sASCII[iS];
            }
         return sResult;
         }

      private void buttProbeSincRead_Click(object sender,EventArgs e) {
         OpcGroup theSinchroGrp;    // спецГруппа для тех, кого читаем
         OPCItemDef[] aD;           // те(несколько)точек, кого читаем
         OPCItemResult[] arrRes;    // результатик считывания
         OPCItemState[] sts;        // статусы(несколько)по каждой считанной точке
         int[] serverhandles;       // для каждой точки м.б.свой уникальный
         string sPLC_Adr = "S7:[PLC01]DB2,int102";
         string[] sPLC_AdrLi = { "S7:[PLC01]DB2,int102"  // ACT_WATCHDOG01_PLC
                               ,"S7:[PLC11]DB2,int48"    // ACT_C1_WATCHDOG1_PLC
                               ,"S7:[PLC21]DB2,int48"    // ACT_C2_WATCHDOG1_PLC
                               ,"S7:[PLC31]DB2,int48"    // ACT_C3_WATCHDOG1_PLC
                               };
         string sErgValue = "";
         bool bErg = true;
         clsPointInfo[] probePointLi = new clsPointInfo[sPLC_AdrLi.GetLength(0)];

         try {
            theSinchroGrp = theSrv.AddGroup("OPCdotNET-Sinchro",true,60000);
            aD = new OPCItemDef[1];

            aD[0] = new OPCItemDef(sPLC_Adr,true,itmHandleClient,VarEnum.VT_EMPTY);
            theGrp.AddItems(aD,out arrRes);
            if (arrRes == null) {
               MessageBox.Show(this,"AddItems" + i,"arrRes == null",MessageBoxButtons.OK,MessageBoxIcon.Warning);
               }
            else {
               if (arrRes[0].Error != HRESULTS.S_OK) {
                  MessageBox.Show(this,"AddItems" + i,"arrRes[0].Error != HRESULTS.S_OK",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                  }
               }

            //// add event handler for data changes
            //theSinchroGrp.DataChanged += new DataChangeEventHandler(this.theGrp_DataChange);
            //theSinchroGrp.WriteCompleted += new WriteCompleteEventHandler(this.theGrp_WriteComplete);
            sts = new OPCItemState[1];       // не знаю, нужна ли инициализация
            itmHandleServer = 0;             // как бы 0. элемент в "sPLC_AdrLi"
            serverhandles = new int[1] { itmHandleServer };
            theSinchroGrp.Read(OPCDATASOURCE.OPC_DS_DEVICE,serverhandles,out sts);  // должны считаться сразу несколько точек
            foreach (OPCItemState s in sts) {
               try {
                  if (HRESULTS.Succeeded(s.Error)) {
                     if (s.HandleClient < sPLC_AdrLi.GetLength(0)) {
                        if (s.DataValue != null) {
                           setTextInTxbViaDelegate(ref txtItemQual,OpcGroup.QualityToString(s.Quality));
                           setTextInTxbViaDelegate(ref txtItemTimeSt,DateTime.FromFileTime(s.TimeStamp).ToString());
                           probePointLi[s.HandleClient].opcItem = s;
                           if (s.DataValue.GetType() == typeof(SByte[])) {
                              probePointLi[s.HandleClient].sDataValue = ConvertToRus((SByte[])s.DataValue,probePointLi[s.HandleClient].iDataLength);
                              setTextInTxbViaDelegate(ref txtItemValue,probePointLi[s.HandleClient].sDataValue);
                              sErgValue = probePointLi[s.HandleClient].sDataValue;
                              }
                           else {
                              if (s.DataValue.GetType() == typeof(DateTime)) {
                                 probePointLi[s.HandleClient].dtDataValue = Convert.ToDateTime(s.DataValue);
                                 setTextInTxbViaDelegate(ref txtItemValue,LoggDateToString(probePointLi[s.HandleClient].dtDataValue));
                                 sErgValue = LoggDateToString(probePointLi[s.HandleClient].dtDataValue);
                                 }
                              else {
                                 probePointLi[s.HandleClient].iDataValue = Convert.ToInt32(s.DataValue);
                                 setTextInTxbViaDelegate(ref txtItemValue,probePointLi[s.HandleClient].iDataValue.ToString());
                                 sErgValue = probePointLi[s.HandleClient].iDataValue.ToString();
                                 }
                              }
                           AddLogg("Pkt" + s.HandleClient + "," + s.DataValue.GetType() + " контроль: '" + sErgValue + "'");
                           }
                        else {
                           AddLogg("s.HandleClient{" + s.HandleClient + "}>=probePointLi.Count{" + sPLC_AdrLi.GetLength(0) + "}");
                           }
                        }
                     }
                  else {
                     setTextInTxbViaDelegate(ref txtItemTimeSt,DateTime.FromFileTime(s.TimeStamp).ToString());
                     setTextInTxbViaDelegate(ref txtItemQual,"error");
                     setTextInTxbViaDelegate(ref txtItemValue,"ERROR 0x" + s.Error.ToString("X"));
                     AddLogg("s.HandleClient{" + s.HandleClient + "} ERROR 0x" + s.Error.ToString("X"));
                     }
                  }
               catch (Exception exc) {
                  AddLogg("Pkt" + s.HandleClient + ","
                     + ((s.DataValue == null) ? " DataValue==null" : s.DataValue.GetType().ToString())
                     + " " + exc.Message);
                  }
               }
            GC.Collect(); // just for fun
            }
         catch (Exception eXc) {
            MessageBox.Show(this,"OPC Exception!",eXc.Message,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return;
            }
         }

      //string getOPCError(int Error){
      //const int S_OK = 0x00000000;
      //const int S_FALSE = 0x00000001;

      //const int E_NOTIMPL = unchecked((int)0x80004001);		// winerror.h
      //const int E_NOINTERFACE = unchecked((int)0x80004002);
      //const int E_ABORT = unchecked((int)0x80004004);
      //const int E_FAIL = unchecked((int)0x80004005);
      //const int E_OUTOFMEMORY = unchecked((int)0x8007000E);
      //const int E_INVALIDARG = unchecked((int)0x80070057);

      //const int CONNECT_E_NOCONNECTION = unchecked((int)0x80040200);		// olectl.h
      //const int CONNECT_E_ADVISELIMIT = unchecked((int)0x80040201);

      //const int OPC_E_INVALIDHANDLE = unchecked((int)0xC0040001);		// opcerror.h
      //const int OPC_E_BADTYPE = unchecked((int)0xC0040004);
      //const int OPC_E_PUBLIC = unchecked((int)0xC0040005);
      //const int OPC_E_BADRIGHTS = unchecked((int)0xC0040006);
      //const int OPC_E_UNKNOWNITEMID = unchecked((int)0xC0040007);
      //const int OPC_E_INVALIDITEMID = unchecked((int)0xC0040008);
      //const int OPC_E_INVALIDFILTER = unchecked((int)0xC0040009);
      //const int OPC_E_UNKNOWNPATH = unchecked((int)0xC004000A);
      //const int OPC_E_RANGE = unchecked((int)0xC004000B);
      //const int OPC_E_DUPLICATENAME = unchecked((int)0xC004000C);
      //const int OPC_S_UNSUPPORTEDRATE = unchecked((int)0x0004000D);
      //const int OPC_S_CLAMP = unchecked((int)0x0004000E);
      //const int OPC_S_INUSE = unchecked((int)0x0004000F);
      //const int OPC_E_INVALIDCONFIGFILE = unchecked((int)0xC0040010);
      //const int OPC_E_NOTFOUND = unchecked((int)0xC0040011);
      //const int OPC_E_INVALID_PID = unchecked((int)0xC0040203);

      //   }
      }	// class MainForm

   }	// namespace DirectOPCClient