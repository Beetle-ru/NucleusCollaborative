using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DirectOPCClient
{

public class AboutForm : System.Windows.Forms.Form
	{
	private System.Windows.Forms.Button btnOk;
	private System.Windows.Forms.PictureBox picViscomLogo;
	private System.Windows.Forms.LinkLabel linkViscom;
	private System.Windows.Forms.Label labelViscom;
	private System.Windows.Forms.LinkLabel linkViscomNet;
	private System.Windows.Forms.LinkLabel linkViscomMail;

	private System.ComponentModel.Container components = null;

	public AboutForm()
		{
		InitializeComponent();
		linkViscom.Links.Add( 0, 100, "http://www.viscomvisual.com" );
		linkViscomNet.Links.Add( 0, 100, "http://www.viscomvisual.com/dotnet/" );
		linkViscomMail.Links.Add( 0, 100, "mailto:dotnet@viscomvisual.com?subject=Feedback&body=Great sample!" );
		}

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.btnOk = new System.Windows.Forms.Button();
			this.linkViscom = new System.Windows.Forms.LinkLabel();
			this.linkViscomNet = new System.Windows.Forms.LinkLabel();
			this.picViscomLogo = new System.Windows.Forms.PictureBox();
			this.labelViscom = new System.Windows.Forms.Label();
			this.linkViscomMail = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOk.Location = new System.Drawing.Point(109, 184);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 32);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			// 
			// linkViscom
			// 
			this.linkViscom.Location = new System.Drawing.Point(18, 111);
			this.linkViscom.Name = "linkViscom";
			this.linkViscom.Size = new System.Drawing.Size(272, 16);
			this.linkViscom.TabIndex = 2;
			this.linkViscom.TabStop = true;
			this.linkViscom.Text = "www.viscomvisual.com";
			this.linkViscom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkViscom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkViscom_LinkClicked);
			// 
			// linkViscomNet
			// 
			this.linkViscomNet.Location = new System.Drawing.Point(18, 133);
			this.linkViscomNet.Name = "linkViscomNet";
			this.linkViscomNet.Size = new System.Drawing.Size(272, 16);
			this.linkViscomNet.TabIndex = 2;
			this.linkViscomNet.TabStop = true;
			this.linkViscomNet.Text = "www.viscomvisual.com / dotnet";
			this.linkViscomNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkViscomNet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkViscom_LinkClicked);
			// 
			// picViscomLogo
			// 
			this.picViscomLogo.Image = ((System.Drawing.Bitmap)(resources.GetObject("picViscomLogo.Image")));
			this.picViscomLogo.Location = new System.Drawing.Point(29, 30);
			this.picViscomLogo.Name = "picViscomLogo";
			this.picViscomLogo.Size = new System.Drawing.Size(240, 72);
			this.picViscomLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picViscomLogo.TabIndex = 1;
			this.picViscomLogo.TabStop = false;
			// 
			// labelViscom
			// 
			this.labelViscom.Location = new System.Drawing.Point(65, 7);
			this.labelViscom.Name = "labelViscom";
			this.labelViscom.Size = new System.Drawing.Size(168, 16);
			this.labelViscom.TabIndex = 3;
			this.labelViscom.Text = "Copyright ©";
			this.labelViscom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkViscomMail
			// 
			this.linkViscomMail.Location = new System.Drawing.Point(18, 160);
			this.linkViscomMail.Name = "linkViscomMail";
			this.linkViscomMail.Size = new System.Drawing.Size(272, 16);
			this.linkViscomMail.TabIndex = 2;
			this.linkViscomMail.TabStop = true;
			this.linkViscomMail.Text = "feedback email: dotnet@viscomvisual.com";
			this.linkViscomMail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkViscomMail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkViscom_LinkClicked);
			// 
			// AboutForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(308, 223);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.labelViscom,
																		  this.linkViscom,
																		  this.picViscomLogo,
																		  this.btnOk,
																		  this.linkViscomNet,
																		  this.linkViscomMail});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.ResumeLayout(false);

		}
		#endregion

	private void linkViscom_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e )
		{
		e.Link.Visited = true;
		System.Diagnostics.Process.Start( e.Link.LinkData.ToString() );
		}

}

}
