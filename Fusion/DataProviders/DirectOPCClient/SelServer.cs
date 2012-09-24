using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using OPC.Common;

namespace DirectOPCClient {

   /// <summary>
   /// Summary description for SelServer.
   /// </summary>
   public class SelServer : System.Windows.Forms.Form {
      public string selectedOpcSrv = null;

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.ListView OpcDaSrvList;
      private System.Windows.Forms.ColumnHeader colHeadName;
      private System.Windows.Forms.ColumnHeader colHeadID;
      private System.Windows.Forms.ColumnHeader colHeadCLS;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public SelServer() {
         InitializeComponent();
         ListAllServers();
         }

      public void ListAllServers() {
         OpcDaSrvList.Items.Clear();

         OpcServerList lst = new OpcServerList();
         OpcServers[] svs = null;
         try {
            lst.ListAllData20(out svs);
            }
         catch (COMException) {
            MessageBox.Show(this,"Enum OPC servers failed!","Select Server",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return;
            }

         if (svs == null)
            return;

         string[] itemstrings = new string[3];
         foreach (OpcServers l in svs) {
            itemstrings[0] = l.ServerName;
            itemstrings[1] = l.ProgID;
            itemstrings[2] = l.ClsID.ToString();
            OpcDaSrvList.Items.Add(new ListViewItem(itemstrings));
            }

         // preselect top item in ListView
         OpcDaSrvList.Items[0].Selected = true;
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
         this.colHeadCLS = new System.Windows.Forms.ColumnHeader();
         this.btnOK = new System.Windows.Forms.Button();
         this.OpcDaSrvList = new System.Windows.Forms.ListView();
         this.colHeadName = new System.Windows.Forms.ColumnHeader();
         this.colHeadID = new System.Windows.Forms.ColumnHeader();
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // colHeadCLS
         // 
         this.colHeadCLS.Text = "CLSID";
         this.colHeadCLS.Width = 208;
         // 
         // btnOK
         // 
         this.btnOK.Location = new System.Drawing.Point(208,194);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(96,32);
         this.btnOK.TabIndex = 0;
         this.btnOK.Text = "Ok";
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // OpcDaSrvList
         // 
         this.OpcDaSrvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.colHeadName,
																					   this.colHeadID,
																					   this.colHeadCLS});
         this.OpcDaSrvList.FullRowSelect = true;
         this.OpcDaSrvList.GridLines = true;
         this.OpcDaSrvList.HideSelection = false;
         this.OpcDaSrvList.Location = new System.Drawing.Point(8,32);
         this.OpcDaSrvList.MultiSelect = false;
         this.OpcDaSrvList.Name = "OpcDaSrvList";
         this.OpcDaSrvList.Size = new System.Drawing.Size(496,152);
         this.OpcDaSrvList.TabIndex = 2;
         this.OpcDaSrvList.View = System.Windows.Forms.View.Details;
         this.OpcDaSrvList.DoubleClick += new System.EventHandler(this.btnOK_Click);
         // 
         // colHeadName
         // 
         this.colHeadName.Text = "Name";
         this.colHeadName.Width = 280;
         // 
         // colHeadID
         // 
         this.colHeadID.Text = "ProgID";
         this.colHeadID.Width = 232;
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8,8);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(312,16);
         this.label1.TabIndex = 1;
         this.label1.Text = "select OPC DA 2.0 server:";
         // 
         // SelServer
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
         this.ClientSize = new System.Drawing.Size(516,236);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
																	  this.OpcDaSrvList,
																	  this.label1,
																	  this.btnOK});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "SelServer";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "OPC Server";
         this.ResumeLayout(false);

         }
      #endregion

      private void btnOK_Click(object sender,System.EventArgs e) {
         if (OpcDaSrvList.SelectedItems.Count == 1) {
            ListViewItem selitem = OpcDaSrvList.SelectedItems[0];
            selectedOpcSrv = selitem.SubItems[1].Text;
            }

         // exit this form
         Close();
         }
      }
   }
