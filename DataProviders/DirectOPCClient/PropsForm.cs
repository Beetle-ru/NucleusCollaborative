using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using OPC.Common;
using OPC.Data.Interface;
using OPC.Data;


namespace DirectOPCClient
{

/// <summary>
/// Summary description for PropsForm.
/// </summary>
public class PropsForm : System.Windows.Forms.Form
{
	private System.Windows.Forms.ListView listPropsView;
	private System.Windows.Forms.ColumnHeader colhdPrpID;
	private System.Windows.Forms.ColumnHeader colhdPrpName;
	private System.Windows.Forms.ColumnHeader colhdPrpType;
	private System.Windows.Forms.ColumnHeader colhdPrpValue;
	private System.Windows.Forms.ColumnHeader colhdPrpNewID;
	private System.Windows.Forms.Button btnPropClose;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.Container components = null;

	public PropsForm( ref OpcServer theSrv, string itemid )
		{
		InitializeComponent();
		ListAllProperties( ref theSrv, itemid );
		}

	public void ListAllProperties( ref OpcServer theSrv, string itemid )
		{
		listPropsView.Items.Clear();

		string[]			istrs = new string[ 5 ];
		OPCProperty[]		props;
		OPCPropertyData[]	propdata;
		int[]				propertyIDs = new int[1];
		OPCPropertyItem[]	propitm;

		try
			{
			theSrv.QueryAvailableProperties( itemid, out props );
			if( props == null )
				return;

			foreach( OPCProperty p in props )
				{
				istrs[0] = p.PropertyID.ToString();
				istrs[1] = p.Description;
				istrs[2] = DUMMY_VARIANT.VarEnumToString( p.DataType );
				istrs[3] = "";
				istrs[4] = "";

				propertyIDs[0] = p.PropertyID;
				theSrv.GetItemProperties( itemid, propertyIDs, out propdata );
				if( propdata != null )
					{
					if( propdata[0].Error != HRESULTS.S_OK )
						istrs[3] = "!Error 0x" + propdata[0].Error.ToString( "X" );
					else
						istrs[3] = propdata[0].Data.ToString();
					}

				if( p.PropertyID > 6 )
					{
					theSrv.LookupItemIDs( itemid, propertyIDs, out propitm );
					if( propitm != null )
						{
						if( propitm[0].Error != HRESULTS.S_OK )
							istrs[4] = "!Error 0x" + propitm[0].Error.ToString( "X" );
						else
							istrs[4] = propitm[0].newItemID;
						}
					}

				listPropsView.Items.Add( new ListViewItem( istrs ) );
				}
			
			}
		catch( COMException )
			{
			MessageBox.Show( this, "QueryAvailableProperties failed!", "Item Properties", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			return;
			}

		// preselect top item in ListView
		listPropsView.Items[0].Selected = true;
		}


	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	protected override void Dispose( bool disposing )
		{
		if( disposing )
			{
			if(components != null)
				{
				components.Dispose();
				}
			}
		base.Dispose( disposing );
		}

	#region Windows Form Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
		{
		this.btnPropClose = new System.Windows.Forms.Button();
		this.colhdPrpNewID = new System.Windows.Forms.ColumnHeader();
		this.colhdPrpType = new System.Windows.Forms.ColumnHeader();
		this.colhdPrpID = new System.Windows.Forms.ColumnHeader();
		this.colhdPrpName = new System.Windows.Forms.ColumnHeader();
		this.colhdPrpValue = new System.Windows.Forms.ColumnHeader();
		this.listPropsView = new System.Windows.Forms.ListView();
		this.SuspendLayout();
		// 
		// btnPropClose
		// 
		this.btnPropClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnPropClose.Location = new System.Drawing.Point(211, 216);
		this.btnPropClose.Name = "btnPropClose";
		this.btnPropClose.Size = new System.Drawing.Size(80, 24);
		this.btnPropClose.TabIndex = 1;
		this.btnPropClose.Text = "close";
		// 
		// colhdPrpNewID
		// 
		this.colhdPrpNewID.Text = "newID";
		this.colhdPrpNewID.Width = 128;
		// 
		// colhdPrpType
		// 
		this.colhdPrpType.Text = "Type";
		this.colhdPrpType.Width = 66;
		// 
		// colhdPrpID
		// 
		this.colhdPrpID.Text = "ID";
		this.colhdPrpID.Width = 40;
		// 
		// colhdPrpName
		// 
		this.colhdPrpName.Text = "Name";
		this.colhdPrpName.Width = 134;
		// 
		// colhdPrpValue
		// 
		this.colhdPrpValue.Text = "Value";
		this.colhdPrpValue.Width = 141;
		// 
		// listPropsView
		// 
		this.listPropsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.colhdPrpID,
																						this.colhdPrpName,
																						this.colhdPrpType,
																						this.colhdPrpValue,
																						this.colhdPrpNewID});
		this.listPropsView.FullRowSelect = true;
		this.listPropsView.GridLines = true;
		this.listPropsView.HideSelection = false;
		this.listPropsView.Location = new System.Drawing.Point(8, 8);
		this.listPropsView.Name = "listPropsView";
		this.listPropsView.Size = new System.Drawing.Size(480, 192);
		this.listPropsView.TabIndex = 0;
		this.listPropsView.View = System.Windows.Forms.View.Details;
		// 
		// PropsForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.CancelButton = this.btnPropClose;
		this.ClientSize = new System.Drawing.Size(504, 257);
		this.Controls.AddRange(new System.Windows.Forms.Control[] {
																	  this.btnPropClose,
																	  this.listPropsView});
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "PropsForm";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Item properties";
		this.ResumeLayout(false);

	}
	#endregion

}	// class PropsForm

}	// namespace DirectOPCClient
