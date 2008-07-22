using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;
using System.Management;
using System.Configuration;
using System.Text;
using System.IO;




namespace SLSExceptionReporter
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(SLSExceptionReporter),"MyBitmap.bmp")]
	[LicenseProvider(typeof(LicFileLicenseProvider))]
	public class SLSExceptionReporter : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label lblAdvancedSource;
		private System.Windows.Forms.TabPage tpGeneral;
		private System.Windows.Forms.GroupBox grbStackTrace;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lblGeneralType;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtGeneralMessage;
		private System.Windows.Forms.Label lblPhone;
		private System.Windows.Forms.TabControl tcTabs;
		private System.Windows.Forms.Button cmdCopy;
		private System.Windows.Forms.TreeView tvwExceptions;
		private System.Windows.Forms.Label lblContactMessageBottom;
		private System.Windows.Forms.TreeView tvwSettings;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.GroupBox gbLineGeneral1;
		private System.Windows.Forms.TextBox txtAdvancedType;
		private System.Windows.Forms.Label lblRegion;
		private System.Windows.Forms.TextBox txtApplication;
		private System.Windows.Forms.TabPage tpExplanation;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblGeneral;
		private System.Windows.Forms.TabPage tpAdvanced;
		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.PictureBox picGeneral;
		private System.Windows.Forms.LinkLabel lnkEmail;
		private System.Windows.Forms.Label lblContactMessageTop;
		private System.Windows.Forms.TabPage tpContact;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.TextBox txtMachine;
		private System.Windows.Forms.Label lblExplanation;
		private System.Windows.Forms.Label lblProcessingMessage;
		private System.Windows.Forms.TreeView tvwEnvironment;
		private System.Windows.Forms.Label lblWebSite;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblFax;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.LinkLabel lnkWeb;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.TextBox txtGeneralType;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.TabPage tpSettings;
		private System.Windows.Forms.TabPage tpEnvironment;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.Label lblAdvancedType;
		private System.Windows.Forms.TextBox txtTime;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.Button cmdPrint;
		private System.Windows.Forms.TextBox txtStackTrace;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtDate;
		private System.Windows.Forms.Button cmdEmail;
		private System.Windows.Forms.TextBox txtExplanation;
		private System.Windows.Forms.Label lblExceptionHeirarchy;
		private System.Windows.Forms.TextBox txtSource;
		private System.Windows.Forms.TextBox txtFax;
		private System.Windows.Forms.TextBox txtRegion;
		private System.Windows.Forms.TextBox txtVersion;
		private System.Windows.Forms.Label lblMachine;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Exception exSelected;
		private StringBuilder sbExceptionString;
        private StringBuilder sbPrintString;
		private StringReader sPrintReader;
		private int intCharactersLine = 0;
		private int intLinesPage = 0;
		private Font printFont;

		private int drawWidth = 0;
		private int drawHeight = 0;
		private int PageCount = 0;

		private License license = null;

		

		public SLSExceptionReporter()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			//license = LicenseManager.Validate(typeof(SLSExceptionReporter), this);
		
		}

		private bool setTabs() {
			
			tcTabs.TabPages.Clear();
			
			if (blnGeneralTab) {
				tcTabs.TabPages.Add(tpGeneral);
			}
			if (blnExplanationTab) {
				tcTabs.TabPages.Add(tpExplanation);
			}
			if (blnAdvancedTab) {
				tcTabs.TabPages.Add(tpAdvanced);
			}
			if (blnSettingsTab) {
				tcTabs.TabPages.Add(tpSettings);
			}
			if (blnContactTab) {
				tcTabs.TabPages.Add(tpContact);
			}
			if (blnEnvironmentTab) {
				tcTabs.TabPages.Add(tpEnvironment);
			}
			
			
			return true;
		
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (license != null) 
				{
					license.Dispose();
					license = null;
				}

				if( components != null )
					components.Dispose();
				
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SLSExceptionReporter));
			this.lblMachine = new System.Windows.Forms.Label();
			this.txtVersion = new System.Windows.Forms.TextBox();
			this.txtRegion = new System.Windows.Forms.TextBox();
			this.txtFax = new System.Windows.Forms.TextBox();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.lblExceptionHeirarchy = new System.Windows.Forms.Label();
			this.txtExplanation = new System.Windows.Forms.TextBox();
			this.cmdEmail = new System.Windows.Forms.Button();
			this.txtDate = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtStackTrace = new System.Windows.Forms.TextBox();
			this.cmdPrint = new System.Windows.Forms.Button();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.txtTime = new System.Windows.Forms.TextBox();
			this.lblAdvancedType = new System.Windows.Forms.Label();
			this.lblEmail = new System.Windows.Forms.Label();
			this.tpEnvironment = new System.Windows.Forms.TabPage();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.txtGeneralType = new System.Windows.Forms.TextBox();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.lnkWeb = new System.Windows.Forms.LinkLabel();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblFax = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblWebSite = new System.Windows.Forms.Label();
			this.tvwEnvironment = new System.Windows.Forms.TreeView();
			this.lblProcessingMessage = new System.Windows.Forms.Label();
			this.lblExplanation = new System.Windows.Forms.Label();
			this.txtMachine = new System.Windows.Forms.TextBox();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.tpContact = new System.Windows.Forms.TabPage();
			this.lblContactMessageTop = new System.Windows.Forms.Label();
			this.lnkEmail = new System.Windows.Forms.LinkLabel();
			this.picGeneral = new System.Windows.Forms.PictureBox();
			this.lblApplication = new System.Windows.Forms.Label();
			this.tpAdvanced = new System.Windows.Forms.TabPage();
			this.lblGeneral = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.tpExplanation = new System.Windows.Forms.TabPage();
			this.txtApplication = new System.Windows.Forms.TextBox();
			this.lblRegion = new System.Windows.Forms.Label();
			this.txtAdvancedType = new System.Windows.Forms.TextBox();
			this.gbLineGeneral1 = new System.Windows.Forms.GroupBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.tvwSettings = new System.Windows.Forms.TreeView();
			this.lblContactMessageBottom = new System.Windows.Forms.Label();
			this.tvwExceptions = new System.Windows.Forms.TreeView();
			this.cmdCopy = new System.Windows.Forms.Button();
			this.tcTabs = new System.Windows.Forms.TabControl();
			this.lblPhone = new System.Windows.Forms.Label();
			this.txtGeneralMessage = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblGeneralType = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lblMessage = new System.Windows.Forms.Label();
			this.grbStackTrace = new System.Windows.Forms.GroupBox();
			this.tpGeneral = new System.Windows.Forms.TabPage();
			this.lblAdvancedSource = new System.Windows.Forms.Label();
			this.tpEnvironment.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.tpContact.SuspendLayout();
			this.tpAdvanced.SuspendLayout();
			this.tpExplanation.SuspendLayout();
			this.tcTabs.SuspendLayout();
			this.grbStackTrace.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblMachine
			// 
			this.lblMachine.Location = new System.Drawing.Point(16, 192);
			this.lblMachine.Name = "lblMachine";
			this.lblMachine.Size = new System.Drawing.Size(56, 16);
			this.lblMachine.TabIndex = 20;
			this.lblMachine.Text = "Machine:";
			// 
			// txtVersion
			// 
			this.txtVersion.BackColor = System.Drawing.SystemColors.Control;
			this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtVersion.Location = new System.Drawing.Point(80, 120);
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.ReadOnly = true;
			this.txtVersion.Size = new System.Drawing.Size(128, 14);
			this.txtVersion.TabIndex = 17;
			this.txtVersion.Text = "";
			// 
			// txtRegion
			// 
			this.txtRegion.BackColor = System.Drawing.SystemColors.Control;
			this.txtRegion.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtRegion.Location = new System.Drawing.Point(312, 120);
			this.txtRegion.Name = "txtRegion";
			this.txtRegion.ReadOnly = true;
			this.txtRegion.Size = new System.Drawing.Size(144, 14);
			this.txtRegion.TabIndex = 24;
			this.txtRegion.Text = "";
			// 
			// txtFax
			// 
			this.txtFax.BackColor = System.Drawing.SystemColors.Control;
			this.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtFax.Location = new System.Drawing.Point(120, 152);
			this.txtFax.Name = "txtFax";
			this.txtFax.Size = new System.Drawing.Size(280, 14);
			this.txtFax.TabIndex = 7;
			this.txtFax.Text = "";
			// 
			// txtSource
			// 
			this.txtSource.BackColor = System.Drawing.SystemColors.Control;
			this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtSource.Location = new System.Drawing.Point(248, 24);
			this.txtSource.Name = "txtSource";
			this.txtSource.ReadOnly = true;
			this.txtSource.Size = new System.Drawing.Size(216, 14);
			this.txtSource.TabIndex = 1;
			this.txtSource.Text = "";
			// 
			// lblExceptionHeirarchy
			// 
			this.lblExceptionHeirarchy.Location = new System.Drawing.Point(8, 8);
			this.lblExceptionHeirarchy.Name = "lblExceptionHeirarchy";
			this.lblExceptionHeirarchy.Size = new System.Drawing.Size(160, 16);
			this.lblExceptionHeirarchy.TabIndex = 1;
			this.lblExceptionHeirarchy.Text = "Exception Heirarchy";
			// 
			// txtExplanation
			// 
			this.txtExplanation.Location = new System.Drawing.Point(12, 48);
			this.txtExplanation.Multiline = true;
			this.txtExplanation.Name = "txtExplanation";
			this.txtExplanation.Size = new System.Drawing.Size(444, 280);
			this.txtExplanation.TabIndex = 12;
			this.txtExplanation.Text = "";
			// 
			// cmdEmail
			// 
			this.cmdEmail.Location = new System.Drawing.Point(376, 376);
			this.cmdEmail.Name = "cmdEmail";
			this.cmdEmail.Size = new System.Drawing.Size(112, 32);
			this.cmdEmail.TabIndex = 5;
			this.cmdEmail.Text = "E-Mail";
			this.cmdEmail.Click += new System.EventHandler(this.cmdEmail_Click);
			// 
			// txtDate
			// 
			this.txtDate.BackColor = System.Drawing.SystemColors.Control;
			this.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtDate.Location = new System.Drawing.Point(80, 168);
			this.txtDate.Name = "txtDate";
			this.txtDate.ReadOnly = true;
			this.txtDate.Size = new System.Drawing.Size(128, 14);
			this.txtDate.TabIndex = 17;
			this.txtDate.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(8, 144);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(456, 8);
			this.groupBox2.TabIndex = 21;
			this.groupBox2.TabStop = false;
			// 
			// txtStackTrace
			// 
			this.txtStackTrace.BackColor = System.Drawing.SystemColors.Control;
			this.txtStackTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtStackTrace.Location = new System.Drawing.Point(4, 12);
			this.txtStackTrace.Multiline = true;
			this.txtStackTrace.Name = "txtStackTrace";
			this.txtStackTrace.ReadOnly = true;
			this.txtStackTrace.Size = new System.Drawing.Size(448, 96);
			this.txtStackTrace.TabIndex = 2;
			this.txtStackTrace.Text = "";
			// 
			// cmdPrint
			// 
			this.cmdPrint.Location = new System.Drawing.Point(16, 376);
			this.cmdPrint.Name = "cmdPrint";
			this.cmdPrint.Size = new System.Drawing.Size(112, 32);
			this.cmdPrint.TabIndex = 2;
			this.cmdPrint.Text = "Print";
			this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
			// 
			// txtPhone
			// 
			this.txtPhone.BackColor = System.Drawing.SystemColors.Control;
			this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtPhone.Location = new System.Drawing.Point(120, 120);
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(280, 14);
			this.txtPhone.TabIndex = 5;
			this.txtPhone.Text = "";
			// 
			// txtTime
			// 
			this.txtTime.BackColor = System.Drawing.SystemColors.Control;
			this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTime.Location = new System.Drawing.Point(312, 168);
			this.txtTime.Name = "txtTime";
			this.txtTime.ReadOnly = true;
			this.txtTime.Size = new System.Drawing.Size(128, 14);
			this.txtTime.TabIndex = 4;
			this.txtTime.Text = "";
			// 
			// lblAdvancedType
			// 
			this.lblAdvancedType.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblAdvancedType.Location = new System.Drawing.Point(192, 56);
			this.lblAdvancedType.Name = "lblAdvancedType";
			this.lblAdvancedType.Size = new System.Drawing.Size(40, 16);
			this.lblAdvancedType.TabIndex = 6;
			this.lblAdvancedType.Text = "Type:";
			// 
			// lblEmail
			// 
			this.lblEmail.Location = new System.Drawing.Point(64, 64);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(40, 24);
			this.lblEmail.TabIndex = 1;
			this.lblEmail.Text = "E-Mail:";
			// 
			// tpEnvironment
			// 
			this.tpEnvironment.Controls.Add(this.tvwEnvironment);
			this.tpEnvironment.Location = new System.Drawing.Point(4, 22);
			this.tpEnvironment.Name = "tpEnvironment";
			this.tpEnvironment.Size = new System.Drawing.Size(472, 334);
			this.tpEnvironment.TabIndex = 3;
			this.tpEnvironment.Text = "Environment";
			this.tpEnvironment.Click += new System.EventHandler(this.tpEnvironment_Click);
			// 
			// tpSettings
			// 
			this.tpSettings.Controls.Add(this.tvwSettings);
			this.tpSettings.Location = new System.Drawing.Point(4, 22);
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.Size = new System.Drawing.Size(472, 334);
			this.tpSettings.TabIndex = 5;
			this.tpSettings.Text = "Settings";
			this.tpSettings.Click += new System.EventHandler(this.tpSettings_Click);
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// txtGeneralType
			// 
			this.txtGeneralType.BackColor = System.Drawing.SystemColors.Control;
			this.txtGeneralType.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtGeneralType.Location = new System.Drawing.Point(80, 232);
			this.txtGeneralType.Name = "txtGeneralType";
			this.txtGeneralType.ReadOnly = true;
			this.txtGeneralType.Size = new System.Drawing.Size(248, 14);
			this.txtGeneralType.TabIndex = 15;
			this.txtGeneralType.Text = "";
			this.txtGeneralType.TextChanged += new System.EventHandler(this.txtGeneralType_TextChanged);
			// 
			// lnkWeb
			// 
			this.lnkWeb.Location = new System.Drawing.Point(120, 88);
			this.lnkWeb.Name = "lnkWeb";
			this.lnkWeb.Size = new System.Drawing.Size(280, 16);
			this.lnkWeb.TabIndex = 2;
			// 
			// lblTime
			// 
			this.lblTime.Location = new System.Drawing.Point(240, 168);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(40, 16);
			this.lblTime.TabIndex = 6;
			this.lblTime.Text = "Time:";
			// 
			// lblFax
			// 
			this.lblFax.Location = new System.Drawing.Point(64, 152);
			this.lblFax.Name = "lblFax";
			this.lblFax.Size = new System.Drawing.Size(48, 24);
			this.lblFax.TabIndex = 6;
			this.lblFax.Text = "Fax:";
			// 
			// lblDate
			// 
			this.lblDate.Location = new System.Drawing.Point(16, 168);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(40, 16);
			this.lblDate.TabIndex = 18;
			this.lblDate.Text = "Date:";
			// 
			// lblWebSite
			// 
			this.lblWebSite.Location = new System.Drawing.Point(64, 88);
			this.lblWebSite.Name = "lblWebSite";
			this.lblWebSite.Size = new System.Drawing.Size(40, 24);
			this.lblWebSite.TabIndex = 3;
			this.lblWebSite.Text = "Web:";
			// 
			// tvwEnvironment
			// 
			this.tvwEnvironment.ImageIndex = -1;
			this.tvwEnvironment.Location = new System.Drawing.Point(16, 8);
			this.tvwEnvironment.Name = "tvwEnvironment";
			this.tvwEnvironment.SelectedImageIndex = -1;
			this.tvwEnvironment.Size = new System.Drawing.Size(440, 320);
			this.tvwEnvironment.TabIndex = 24;
			this.tvwEnvironment.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwEnvironment_AfterSelect);
			// 
			// lblProcessingMessage
			// 
			this.lblProcessingMessage.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblProcessingMessage.Location = new System.Drawing.Point(80, 312);
			this.lblProcessingMessage.Name = "lblProcessingMessage";
			this.lblProcessingMessage.Size = new System.Drawing.Size(376, 16);
			this.lblProcessingMessage.TabIndex = 23;
			this.lblProcessingMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblExplanation
			// 
			this.lblExplanation.Location = new System.Drawing.Point(16, 11);
			this.lblExplanation.Name = "lblExplanation";
			this.lblExplanation.Size = new System.Drawing.Size(440, 32);
			this.lblExplanation.TabIndex = 13;
			this.lblExplanation.Text = "Please enter a brief explanation of the actions you took prior to this error occu" +
"ring.  This information will assist the investigation into the cause of the erro" +
"r.";
			// 
			// txtMachine
			// 
			this.txtMachine.BackColor = System.Drawing.SystemColors.Control;
			this.txtMachine.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMachine.Location = new System.Drawing.Point(80, 192);
			this.txtMachine.Name = "txtMachine";
			this.txtMachine.ReadOnly = true;
			this.txtMachine.Size = new System.Drawing.Size(144, 14);
			this.txtMachine.TabIndex = 19;
			this.txtMachine.Text = "";
			// 
			// lblUsername
			// 
			this.lblUsername.Location = new System.Drawing.Point(240, 192);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(64, 16);
			this.lblUsername.TabIndex = 9;
			this.lblUsername.Text = "Username:";
			// 
			// txtMessage
			// 
			this.txtMessage.BackColor = System.Drawing.SystemColors.Control;
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMessage.Location = new System.Drawing.Point(248, 104);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new System.Drawing.Size(216, 96);
			this.txtMessage.TabIndex = 1;
			this.txtMessage.Text = "";
			// 
			// tpContact
			// 
			this.tpContact.Controls.Add(this.lblContactMessageTop);
			this.tpContact.Controls.Add(this.txtFax);
			this.tpContact.Controls.Add(this.lblFax);
			this.tpContact.Controls.Add(this.txtPhone);
			this.tpContact.Controls.Add(this.lblPhone);
			this.tpContact.Controls.Add(this.lblWebSite);
			this.tpContact.Controls.Add(this.lnkWeb);
			this.tpContact.Controls.Add(this.lblEmail);
			this.tpContact.Controls.Add(this.lnkEmail);
			this.tpContact.Controls.Add(this.lblContactMessageBottom);
			this.tpContact.Location = new System.Drawing.Point(4, 22);
			this.tpContact.Name = "tpContact";
			this.tpContact.Size = new System.Drawing.Size(472, 334);
			this.tpContact.TabIndex = 4;
			this.tpContact.Text = "Contact";
			this.tpContact.Click += new System.EventHandler(this.tpContact_Click);
			// 
			// lblContactMessageTop
			// 
			this.lblContactMessageTop.Location = new System.Drawing.Point(16, 16);
			this.lblContactMessageTop.Name = "lblContactMessageTop";
			this.lblContactMessageTop.Size = new System.Drawing.Size(432, 24);
			this.lblContactMessageTop.TabIndex = 15;
			this.lblContactMessageTop.Text = "The following details can be used to obtain support for this application..";
			// 
			// lnkEmail
			// 
			this.lnkEmail.Location = new System.Drawing.Point(120, 64);
			this.lnkEmail.Name = "lnkEmail";
			this.lnkEmail.Size = new System.Drawing.Size(280, 16);
			this.lnkEmail.TabIndex = 0;
			this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// picGeneral
			// 
			this.picGeneral.Location = new System.Drawing.Point(16, 8);
			this.picGeneral.Name = "picGeneral";
			this.picGeneral.Size = new System.Drawing.Size(48, 48);
			this.picGeneral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picGeneral.TabIndex = 22;
			this.picGeneral.TabStop = false;
			// 
			// lblApplication
			// 
			this.lblApplication.Location = new System.Drawing.Point(16, 88);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(64, 16);
			this.lblApplication.TabIndex = 18;
			this.lblApplication.Text = "Application:";
			this.lblApplication.Click += new System.EventHandler(this.lblApplication_Click);
			// 
			// tpAdvanced
			// 
			this.tpAdvanced.Controls.Add(this.grbStackTrace);
			this.tpAdvanced.Controls.Add(this.groupBox3);
			this.tpAdvanced.Controls.Add(this.lblAdvancedType);
			this.tpAdvanced.Controls.Add(this.txtAdvancedType);
			this.tpAdvanced.Controls.Add(this.lblAdvancedSource);
			this.tpAdvanced.Controls.Add(this.lblExceptionHeirarchy);
			this.tpAdvanced.Controls.Add(this.tvwExceptions);
			this.tpAdvanced.Controls.Add(this.txtSource);
			this.tpAdvanced.Controls.Add(this.txtMessage);
			this.tpAdvanced.Controls.Add(this.label1);
			this.tpAdvanced.Location = new System.Drawing.Point(4, 22);
			this.tpAdvanced.Name = "tpAdvanced";
			this.tpAdvanced.Size = new System.Drawing.Size(472, 334);
			this.tpAdvanced.TabIndex = 1;
			this.tpAdvanced.Text = "Advanced";
			this.tpAdvanced.Click += new System.EventHandler(this.tabPage2_Click);
			// 
			// lblGeneral
			// 
			this.lblGeneral.Location = new System.Drawing.Point(80, 16);
			this.lblGeneral.Name = "lblGeneral";
			this.lblGeneral.Size = new System.Drawing.Size(384, 48);
			this.lblGeneral.TabIndex = 14;
			this.lblGeneral.Text = "An error has occured in this application.";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(192, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Message";
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(16, 120);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(48, 16);
			this.lblVersion.TabIndex = 18;
			this.lblVersion.Text = "Version:";
			// 
			// txtUserName
			// 
			this.txtUserName.BackColor = System.Drawing.SystemColors.Control;
			this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtUserName.Location = new System.Drawing.Point(312, 192);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.ReadOnly = true;
			this.txtUserName.Size = new System.Drawing.Size(144, 14);
			this.txtUserName.TabIndex = 8;
			this.txtUserName.Text = "";
			// 
			// tpExplanation
			// 
			this.tpExplanation.Controls.Add(this.lblExplanation);
			this.tpExplanation.Controls.Add(this.txtExplanation);
			this.tpExplanation.Location = new System.Drawing.Point(4, 22);
			this.tpExplanation.Name = "tpExplanation";
			this.tpExplanation.Size = new System.Drawing.Size(472, 334);
			this.tpExplanation.TabIndex = 2;
			this.tpExplanation.Text = "Explanation";
			this.tpExplanation.Click += new System.EventHandler(this.tpExplanation_Click);
			// 
			// txtApplication
			// 
			this.txtApplication.BackColor = System.Drawing.SystemColors.Control;
			this.txtApplication.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtApplication.Location = new System.Drawing.Point(80, 88);
			this.txtApplication.Name = "txtApplication";
			this.txtApplication.ReadOnly = true;
			this.txtApplication.Size = new System.Drawing.Size(376, 14);
			this.txtApplication.TabIndex = 17;
			this.txtApplication.Text = "";
			// 
			// lblRegion
			// 
			this.lblRegion.Location = new System.Drawing.Point(240, 120);
			this.lblRegion.Name = "lblRegion";
			this.lblRegion.Size = new System.Drawing.Size(64, 16);
			this.lblRegion.TabIndex = 25;
			this.lblRegion.Text = "Region:";
			// 
			// txtAdvancedType
			// 
			this.txtAdvancedType.BackColor = System.Drawing.SystemColors.Control;
			this.txtAdvancedType.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtAdvancedType.Location = new System.Drawing.Point(248, 56);
			this.txtAdvancedType.Name = "txtAdvancedType";
			this.txtAdvancedType.ReadOnly = true;
			this.txtAdvancedType.Size = new System.Drawing.Size(216, 14);
			this.txtAdvancedType.TabIndex = 5;
			this.txtAdvancedType.Text = "";
			// 
			// gbLineGeneral1
			// 
			this.gbLineGeneral1.Location = new System.Drawing.Point(8, 64);
			this.gbLineGeneral1.Name = "gbLineGeneral1";
			this.gbLineGeneral1.Size = new System.Drawing.Size(456, 8);
			this.gbLineGeneral1.TabIndex = 21;
			this.gbLineGeneral1.TabStop = false;
			// 
			// cmdSave
			// 
			this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
			this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.cmdSave.Location = new System.Drawing.Point(256, 376);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Size = new System.Drawing.Size(112, 32);
			this.cmdSave.TabIndex = 2;
			this.cmdSave.Text = "Save";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// tvwSettings
			// 
			this.tvwSettings.ImageIndex = -1;
			this.tvwSettings.Location = new System.Drawing.Point(16, 8);
			this.tvwSettings.Name = "tvwSettings";
			this.tvwSettings.SelectedImageIndex = -1;
			this.tvwSettings.Size = new System.Drawing.Size(440, 320);
			this.tvwSettings.TabIndex = 0;
			this.tvwSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSettings_AfterSelect);
			// 
			// lblContactMessageBottom
			// 
			this.lblContactMessageBottom.Location = new System.Drawing.Point(16, 200);
			this.lblContactMessageBottom.Name = "lblContactMessageBottom";
			this.lblContactMessageBottom.Size = new System.Drawing.Size(432, 112);
			this.lblContactMessageBottom.TabIndex = 15;
			this.lblContactMessageBottom.Text = "The information on this form describing the error and envrionment settings will b" +
"e of use when contacting support.";
			// 
			// tvwExceptions
			// 
			this.tvwExceptions.ImageIndex = -1;
			this.tvwExceptions.Location = new System.Drawing.Point(8, 24);
			this.tvwExceptions.Name = "tvwExceptions";
			this.tvwExceptions.SelectedImageIndex = -1;
			this.tvwExceptions.Size = new System.Drawing.Size(176, 176);
			this.tvwExceptions.TabIndex = 0;
			this.tvwExceptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwExceptions_AfterSelect);
			// 
			// cmdCopy
			// 
			this.cmdCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopy.Image")));
			this.cmdCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdCopy.Location = new System.Drawing.Point(136, 376);
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Size = new System.Drawing.Size(112, 32);
			this.cmdCopy.TabIndex = 4;
			this.cmdCopy.Text = "Copy";
			this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
			// 
			// tcTabs
			// 
			this.tcTabs.Controls.Add(this.tpGeneral);
			this.tcTabs.Controls.Add(this.tpExplanation);
			this.tcTabs.Controls.Add(this.tpEnvironment);
			this.tcTabs.Controls.Add(this.tpSettings);
			this.tcTabs.Controls.Add(this.tpContact);
			this.tcTabs.Controls.Add(this.tpAdvanced);
			this.tcTabs.Location = new System.Drawing.Point(8, 8);
			this.tcTabs.Name = "tcTabs";
			this.tcTabs.SelectedIndex = 0;
			this.tcTabs.Size = new System.Drawing.Size(480, 360);
			this.tcTabs.TabIndex = 3;
			this.tcTabs.SelectedIndexChanged += new System.EventHandler(this.tcTabs_SelectedIndexChanged);
			// 
			// lblPhone
			// 
			this.lblPhone.Location = new System.Drawing.Point(64, 120);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(48, 24);
			this.lblPhone.TabIndex = 4;
			this.lblPhone.Text = "Phone:";
			// 
			// txtGeneralMessage
			// 
			this.txtGeneralMessage.BackColor = System.Drawing.SystemColors.Control;
			this.txtGeneralMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtGeneralMessage.Location = new System.Drawing.Point(80, 256);
			this.txtGeneralMessage.Multiline = true;
			this.txtGeneralMessage.Name = "txtGeneralMessage";
			this.txtGeneralMessage.ReadOnly = true;
			this.txtGeneralMessage.Size = new System.Drawing.Size(376, 48);
			this.txtGeneralMessage.TabIndex = 5;
			this.txtGeneralMessage.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(8, 216);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 8);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// lblGeneralType
			// 
			this.lblGeneralType.Location = new System.Drawing.Point(16, 232);
			this.lblGeneralType.Name = "lblGeneralType";
			this.lblGeneralType.Size = new System.Drawing.Size(40, 16);
			this.lblGeneralType.TabIndex = 16;
			this.lblGeneralType.Text = "Type:";
			// 
			// groupBox3
			// 
			this.groupBox3.Location = new System.Drawing.Point(192, 80);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(280, 8);
			this.groupBox3.TabIndex = 22;
			this.groupBox3.TabStop = false;
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(16, 256);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(56, 16);
			this.lblMessage.TabIndex = 7;
			this.lblMessage.Text = "Message:";
			this.lblMessage.Click += new System.EventHandler(this.lblMessage_Click);
			// 
			// grbStackTrace
			// 
			this.grbStackTrace.Controls.Add(this.txtStackTrace);
			this.grbStackTrace.Location = new System.Drawing.Point(8, 208);
			this.grbStackTrace.Name = "grbStackTrace";
			this.grbStackTrace.Size = new System.Drawing.Size(456, 120);
			this.grbStackTrace.TabIndex = 23;
			this.grbStackTrace.TabStop = false;
			this.grbStackTrace.Text = "Stack Trace";
			// 
			// tpGeneral
			// 
			this.tpGeneral.Controls.Add(this.lblRegion);
			this.tpGeneral.Controls.Add(this.txtRegion);
			this.tpGeneral.Controls.Add(this.lblProcessingMessage);
			this.tpGeneral.Controls.Add(this.picGeneral);
			this.tpGeneral.Controls.Add(this.gbLineGeneral1);
			this.tpGeneral.Controls.Add(this.lblMachine);
			this.tpGeneral.Controls.Add(this.txtMachine);
			this.tpGeneral.Controls.Add(this.lblDate);
			this.tpGeneral.Controls.Add(this.txtDate);
			this.tpGeneral.Controls.Add(this.lblGeneralType);
			this.tpGeneral.Controls.Add(this.txtGeneralType);
			this.tpGeneral.Controls.Add(this.lblGeneral);
			this.tpGeneral.Controls.Add(this.lblUsername);
			this.tpGeneral.Controls.Add(this.txtUserName);
			this.tpGeneral.Controls.Add(this.lblMessage);
			this.tpGeneral.Controls.Add(this.lblTime);
			this.tpGeneral.Controls.Add(this.txtTime);
			this.tpGeneral.Controls.Add(this.txtGeneralMessage);
			this.tpGeneral.Controls.Add(this.groupBox1);
			this.tpGeneral.Controls.Add(this.groupBox2);
			this.tpGeneral.Controls.Add(this.lblApplication);
			this.tpGeneral.Controls.Add(this.txtApplication);
			this.tpGeneral.Controls.Add(this.lblVersion);
			this.tpGeneral.Controls.Add(this.txtVersion);
			this.tpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Size = new System.Drawing.Size(472, 334);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = "General";
			this.tpGeneral.Click += new System.EventHandler(this.tabPage1_Click);
			// 
			// lblAdvancedSource
			// 
			this.lblAdvancedSource.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblAdvancedSource.Location = new System.Drawing.Point(192, 24);
			this.lblAdvancedSource.Name = "lblAdvancedSource";
			this.lblAdvancedSource.Size = new System.Drawing.Size(40, 16);
			this.lblAdvancedSource.TabIndex = 3;
			this.lblAdvancedSource.Text = "Source:";
			// 
			// SLSExceptionReporter
			// 
			this.Controls.Add(this.cmdEmail);
			this.Controls.Add(this.tcTabs);
			this.Controls.Add(this.cmdPrint);
			this.Controls.Add(this.cmdSave);
			this.Controls.Add(this.cmdCopy);
			this.Name = "SLSExceptionReporter";
			this.Size = new System.Drawing.Size(496, 416);
			this.Load += new System.EventHandler(this.UserControl1_Load);
			this.tpEnvironment.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.tpContact.ResumeLayout(false);
			this.tpAdvanced.ResumeLayout(false);
			this.tpExplanation.ResumeLayout(false);
			this.tcTabs.ResumeLayout(false);
			this.grbStackTrace.ResumeLayout(false);
			this.tpGeneral.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion

		private void tabPage2_Click(object sender, System.EventArgs e)
		{
		
		}

		private void UserControl1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void lblMessage_Click(object sender, System.EventArgs e)
		{
		
		}

		private void tabPage1_Click(object sender, System.EventArgs e)
		{
			
		}
		

		protected void sendEmail()
		{
			
			/*Outlook.Application oApp;
			Outlook._NameSpace oNameSpace;
			oApp = new Outlook.Application();      
			oNameSpace= oApp.GetNamespace("MAPI");      
			oNameSpace.Logon("","",true,true);

			Outlook._MailItem oMailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);                     
			oMailItem.To = lnkEmail.Text;      
			oMailItem.Subject = "Error Occured in Application ";      
			oMailItem.Body = sbExceptionString.ToString();   

			oMailItem.Display(this);
			*/

		}
		// pivate boolean variable to store state of tab page
		// default to true
		private bool blnGeneralTab = true;
		private bool blnExplanationTab = true;
		private bool blnEnvironmentTab = true;
		private bool blnSettingsTab = true;
		private bool blnContactTab = true;
		private bool blnAdvancedTab = true;

		// public property used to set/get visibility of Tab
		public bool ShowGeneralTab
		{
			get
			{
				//return visibility
				return blnGeneralTab;
			}
			set
			{
				// set visibility
				blnGeneralTab = value;
				setTabs();
				
				
			}
		}

		
		// public property used to set/get visibility of Tab
		public bool ShowExplanationTab
		{
			get
			{
				//return visibility
				return blnExplanationTab;
			}
			set
			{
				// set visibility
				blnExplanationTab = value;
				// hide or show as appropriate
				setTabs();
				
			}
		}
		// public property used to set/get visibility of Tab
		public bool ShowEnvironmentTab
		{
			get
			{
				//return visibility
				return blnEnvironmentTab;
			}
			set
			{
				// set visibility
				blnEnvironmentTab= value;
				// hide or show as appropriate
				setTabs();
				
			}
		}
		// public property used to set/get visibility of Tab
		public bool ShowSettingsTab
		{
			get
			{
				//return visibility
				return blnSettingsTab;
			}
			set
			{
				// set visibility
				blnSettingsTab = value;
				// hide or show as appropriate
				setTabs();
				
			}
		}
		// public property used to set/get visibility of Tab
		public bool ShowContactTab
		{
			get
			{
				//return visibility
				return blnContactTab;
			}
			set
			{
				// set visibility
				blnContactTab = value;
				// hide or show as appropriate
				setTabs();
				
			}
		}

		// public property used to set/get visibility of Tab
		public bool ShowAdvancedTab
		{
			get
			{
				//return visibility
				return blnAdvancedTab;
			}
			set
			{
				// set visibility
				blnAdvancedTab = value;
				// hide or show as appropriate
				setTabs();
				
			}
		}

		public bool ShowCopyButton
		{
			get
			{
				//return visibility
				return cmdCopy.Visible;
			}
			set
			{
				// set visibility
				cmdCopy.Visible = value;
			}
		}
		public bool ShowEmailButton
		{
			get
			{
				//return visibility
				return cmdEmail.Visible;
			}
			set
			{
				// set visibility
				cmdEmail.Visible = value;
			}
		}
		public bool ShowSaveButton
		{
			get
			{
				//return visibility
				return cmdSave.Visible;
			}
			set
			{
				// set visibility
				cmdSave.Visible = value;
			}
		}
		public bool ShowPrintButton
		{
			get
			{
				//return visibility
				return cmdPrint.Visible;
			}
			set
			{
				// set visibility
				cmdPrint.Visible = value;
			}
		}

		// public property used to set/get contact email
		public String ContactEmail
		{
			get
			{
				//return email
				return lnkEmail.Text;
			}
			set
			{
				// set email
				lnkEmail.Text = value;
				
			}
		}

		// public property used to set/get contact email
		public String ContactWeb
		{
			get
			{
				//return Web
				return lnkWeb.Text;
			}
			set
			{
				// set Web
				lnkWeb.Text = value;
				
			}
		}

		// public property used to set/get contact email
		public String ContactPhone
		{
			get
			{
				//return Phone
				return txtPhone.Text;
			}
			set
			{
				// set Phone
				txtPhone.Text = value;
				
			}
		}


		// public property used to set/get contact email
		public String ContactFax
		{
			get
			{
				//return Fax
				return txtFax.Text;
			}
			set
			{
				// set Fax
				txtFax.Text = value;
				
			}
		}

		public String ContactMessageTop
		{
			get
			{
				//return top contact message
				return lblContactMessageTop.Text;
			}
			set
			{
				// set top contact message
				lblContactMessageTop.Text = value;
				
			}
		}

		public String ContactMessageBottom
		{
			get
			{
				//return bottom contact message
				return lblContactMessageBottom.Text;
			}
			set
			{
				// set bottom contact message
				lblContactMessageBottom.Text = value;
				
			}
		}

		public String GeneralMessage
		{
			get
			{
				//return General Message
				return lblGeneral.Text;
			}
			set
			{
				// set General Message
				lblGeneral.Text = value;
				
			}
		}

		public String ExplanationMessage
		{
			get
			{
				//return General Message
				return lblExplanation.Text;
			}
			set
			{
				// set General Message
				lblExplanation.Text = value;
				
			}
		}


		private bool buildExceptionString() 
		{

			sbExceptionString = new StringBuilder();

			
			StringWriter swWriter = new StringWriter(sbExceptionString);
			
			swWriter.WriteLine(lblGeneral.Text);
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);
			swWriter.WriteLine(lblApplication.Text + " " + txtApplication.Text);
			swWriter.WriteLine(lblVersion.Text + " " + txtVersion.Text);
			swWriter.WriteLine(lblRegion.Text + " " + txtRegion.Text);
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);
			swWriter.WriteLine(lblDate.Text + " " + txtDate.Text);
			swWriter.WriteLine(lblTime.Text + " " + txtTime.Text);
			swWriter.WriteLine(lblMachine.Text + " " + txtMachine.Text);
			swWriter.WriteLine(lblUsername.Text + " " + txtUserName.Text);
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);
			swWriter.WriteLine(lblGeneralType.Text + " " + txtGeneralType.Text);
			swWriter.WriteLine(lblMessage.Text + " " + txtGeneralMessage.Text);
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);
			
			swWriter.WriteLine("Explanation:");
			swWriter.WriteLine(txtExplanation.Text.Trim());
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);
			
			exceptionHeirarchyToString(swWriter);
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);

			swWriter.WriteLine("Contact Details");
			swWriter.WriteLine(lblEmail.Text + " " + lnkEmail.Text);
			swWriter.WriteLine(lblWebSite.Text + " " + lnkWeb.Text);
			swWriter.WriteLine(lblPhone.Text + " " + txtPhone.Text);
			swWriter.WriteLine(lblFax.Text + " " + txtFax.Text);
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);

			treeToString(tvwSettings,swWriter);
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);
			
			treeToString(tvwEnvironment,swWriter);
			swWriter.WriteLine((String)null);
			swWriter.WriteLine("-----------------------------");
			swWriter.WriteLine((String)null);

			return true;
		}

		private bool treeToString(TreeView tvConvert, StringWriter swTreeWriter) 
		{
			
			treeNodeToString(tvConvert.TopNode,swTreeWriter,0);

			return true;

		}
		private bool treeNodeToString(TreeNode tnNode, StringWriter swWriter, Int32 intLevel) 
		{
			String space = "";
			
			for (Int32 intCount = 0; intCount < (intLevel * 4); intCount++) {
				space = space + " ";
			}
			if (intLevel <=2) 
			{
				swWriter.WriteLine((String)null);
			}
			swWriter.WriteLine(space + tnNode.Text);
			foreach (TreeNode tnChild in tnNode.Nodes) 
			{
				treeNodeToString(tnChild,swWriter, intLevel + 1);
			}
			return true;
			
		}

		private bool exceptionHeirarchyToString(StringWriter swWriter) 
		{
			Int32 intCount = 0;
			Exception current = exSelected;

			while (current != null) 
			{
				if (intCount == 0) 
				{
					swWriter.WriteLine("Top Level Exception");
				} 
				else 
				{
					swWriter.WriteLine("Inner Exception " + intCount.ToString());
				}
				swWriter.WriteLine("Exception Type: " + current.GetType().ToString());
				swWriter.WriteLine("Exception Message: " + current.Message);
				swWriter.WriteLine("Exception Source: " + current.Source);
				swWriter.WriteLine("Exception Stack Trace: " + current.StackTrace);
				swWriter.WriteLine((String)null);

				current = current.InnerException;
				intCount++;
			}
			return true;

		}

		private bool wrapText(StringReader sr, StringWriter sw, Int32 intMaxLineChars) 
		{
			
			String strLine;
			String strSubLine;
			int intPos;

			strLine = sr.ReadLine();
			while (strLine != null) 
			{
				while (strLine.Length > intMaxLineChars) 
				{
					strSubLine = strLine.Substring(1,intMaxLineChars);
					intPos = strSubLine.LastIndexOf(" ");
					if (intPos > intMaxLineChars - 7) 
					{
						// ie if space occurs within last set of characters
						// then wrap at the space (not in the middle of a word)
						strSubLine = strSubLine.Substring(1,intPos);
					}
					sw.WriteLine(strSubLine);
					strLine = strLine.Substring(strSubLine.Length);
                }
				
				// now just add remaining chars if there are any
				if (strLine.Length > 0) 
				{
					sw.WriteLine(strLine);
				}

				// get the next line
				strLine = sr.ReadLine();
			}
			return true;

		}

		public bool displayException(Exception ex) 
		{
			
			Cursor = Cursors.WaitCursor;
			Application.DoEvents();

			
			
            // general tab
			txtDate.Text = System.DateTime.Now.ToShortDateString();
			txtTime.Text = System.DateTime.Now.ToLongTimeString();
			txtGeneralMessage.Text = ex.Message;
			txtUserName.Text = Environment.UserName;
			txtMachine.Text = Environment.MachineName;
			txtGeneralType.Text = ((Object)ex.GetType()).ToString();
		
			txtRegion.Text = Application.CurrentCulture.DisplayName;
			txtApplication.Text = Application.ProductName;
			txtVersion.Text = Application.ProductVersion;

			// explanation tab
		

			//fill the environment tree 
			lblProcessingMessage.Text = "Retrieving Environment Details...Please Wait";
			Application.DoEvents();



			TreeNode root = new TreeNode("Environment");
		
			addEnvironmentNode2("Operating System","Win32_OperatingSystem",root,false,"");
			addEnvironmentNode2(".Net Framework Version(s)","Win32_Product",root,true,"WHERE Caption like 'Microsoft .Net Framework%'");
			addEnvironmentNode2("CPU","Win32_Processor",root,true,"");
			addEnvironmentNode2("Memory","Win32_PhysicalMemory",root,true,"");
			addEnvironmentNode2("Drives","Win32_DiskDrive",root,true,"");
			addEnvironmentNode2("Environment Variables","Win32_Environment",root,true,"");
			addEnvironmentNode2("Printers","Win32_Printer",root,true,"");
			addEnvironmentNode2("System","Win32_ComputerSystem",root,true,"");
			
			addEnvironmentNode2("TimeZone","Win32_TimeZone",root,true,"");
			
			tvwEnvironment.Nodes.Add(root);
			root.Expand();
            

			lblProcessingMessage.Text = "Retrieving Application Settings...Please Wait";
			Application.DoEvents();

			// fill the settings tab
			TreeNode settingsRoot = new TreeNode("Application Settings");
			
			IEnumerator ienum = ConfigurationSettings.AppSettings.GetEnumerator();

			while(ienum.MoveNext())
			{
				settingsRoot.Nodes.Add(new TreeNode(ienum.Current.ToString() + " : " + ConfigurationSettings.AppSettings.Get(ienum.Current.ToString())));
			}

			tvwSettings.Nodes.Add(settingsRoot);
			settingsRoot.Expand();


			TreeNode tnExceptionRoot = buildExceptionHeirarchy(ex);

			tvwExceptions.Nodes.Add(tnExceptionRoot);
            tnExceptionRoot.Expand();
			

			lblProcessingMessage.Text = "";

			exSelected = ex;


			Cursor = Cursors.Default;

			return true;

		}

		private TreeNode buildExceptionHeirarchy(Exception e) 
		{

			TreeNode tnRoot = new TreeNode(e.GetType().ToString());
			tnRoot.Tag = "0";
			TreeNode tnParent = tnRoot;
			TreeNode tnCurrent;
			Exception exCurrent;
			bool blnContinue = true;
			Int32 intIndex=0;

			exCurrent = e;
			blnContinue = (exCurrent.InnerException != null);
			while (blnContinue) 
			{	
				intIndex++;
				exCurrent= exCurrent.InnerException;
				tnCurrent = new TreeNode(exCurrent.GetType().ToString());
				tnCurrent.Tag = intIndex.ToString();
				tnParent.Nodes.Add(tnCurrent);
				tnParent = tnCurrent;
				blnContinue = (exCurrent.InnerException != null);
			}

			return tnRoot;
		}


		private bool addEnvironmentNode2(String strCaption, String strClass , TreeNode parentNode, Boolean blnUseName, String strWhere) 
		{
			try 
			{
				String strDisplayField = blnUseName?"Name":"Caption";

				TreeNode tn = new TreeNode(strCaption);
				Int32 iCount = 0;

				TreeNode propertyNode;

				ManagementObjectSearcher searcher  = new ManagementObjectSearcher("SELECT * FROM " + strClass + " " + strWhere);
			 
				foreach(ManagementObject mo in searcher.Get()) 
				{
					
					TreeNode tn2 = new TreeNode(mo.GetPropertyValue(strDisplayField).ToString().Trim());
					tn.Nodes.Add(tn2);
					foreach(System.Management.PropertyData iPropData in mo.Properties) 
					{
						propertyNode = new TreeNode(iPropData.Name + ':' + System.Convert.ToString(iPropData.Value) );
						tn2.Nodes.Add(propertyNode);
					}
					iCount++;
					
				}
				parentNode.Nodes.Add(tn);
			}
			catch (Exception e) 
			{
				return false;
			}
			
				
			return true;
		}

		
		private void cmdPrint_Click(object sender, System.EventArgs e)
		{
			buildExceptionString();
			// add the delegate

			PrintEventHandler peHandler = new PrintEventHandler(this.printDocument1_BeginPrint);

			printDocument1.BeginPrint += peHandler;


			

			DialogResult dr;
		    printDialog1.Document = printDocument1;
			dr = printDialog1.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				txtGeneralMessage.Text = "Printing";

				printDocument1.Print();
			}
			else 
			{
				txtGeneralMessage.Text = "Cancelled Print";
			}




		}

		private void tvwEnvironment_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
		
		}

		private void tvwSettings_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
		
		}

		private void tpSettings_Click(object sender, System.EventArgs e)
		{
		
		}

		private void txtGeneralType_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void tvwExceptions_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			Exception displayException;
			
			displayException = exSelected;
			


			for (Int32 intCount = 0; intCount < Int32.Parse(e.Node.Tag.ToString());intCount++) 
			{
				displayException = displayException.InnerException;
			}

			txtSource.Text = displayException.Source;
			txtAdvancedType.Text = displayException.GetType().ToString();
			txtStackTrace.Text = displayException.StackTrace;
			txtMessage.Text = displayException.Message;
			



		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
		
		}

		private void tpContact_Click(object sender, System.EventArgs e)
		{
		
		}

		private void tcTabs_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void cmdCopy_Click(object sender, System.EventArgs e)
		{
			buildExceptionString();
			Clipboard.SetDataObject(sbExceptionString.ToString(),true);
		}

		private void cmdEmail_Click(object sender, System.EventArgs e)
		{
			buildExceptionString();
			sendEmail();
		}


		private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			
			PageCount = 0;

			
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			
			
			int leftMargin = e.MarginBounds.Left;
			int topMargin = e.MarginBounds.Top;
			int intCount = 1;


			PageCount ++;
			if (PageCount == 1) 
			{
				printFont = new Font("Courier New", 12);
			}

			SizeF fSize = e.Graphics.MeasureString("W",printFont);

			if (PageCount == 1) 
			{
				// setup for first page
				drawWidth = e.PageBounds.Width; //- (e.MarginBounds.Left + e.MarginBounds.Right);
				drawHeight= e.PageBounds.Height;//- (e.MarginBounds.Top+ e.MarginBounds.Bottom);
		
				intCharactersLine =(int) ((float)drawWidth/fSize.Width);
				intLinesPage = (int)((float)drawHeight/fSize.Height);
            
				sbPrintString = new StringBuilder();
				StringWriter swPrint = new StringWriter(sbPrintString);
				StringReader srException = new StringReader(sbExceptionString.ToString());
				wrapText(srException,swPrint,intCharactersLine);
				sPrintReader = new StringReader(sbPrintString.ToString());

			}
			
			//loop for the number of lines a page
			while (intCount <= intLinesPage)
			{
				// read the line
				String strLine = sPrintReader.ReadLine();
				if (strLine == null) 
				{
					intCount = intLinesPage + 1; //exit the loop
				} 
				else 
				{
					// print the line
					e.Graphics.DrawString(strLine, printFont, Brushes.Black, leftMargin, topMargin + ((intCount - 1) * fSize.Height));
				}
				intCount++;
			}
			
			if (sPrintReader.Peek() == -1) 
			{
				// there is no more for the string reader to read
				e.HasMorePages=false;
			} 
			else 
			{
				// we have more to print
				e.HasMorePages = true;
			}
			
			                       
		}


		~SLSExceptionReporter() 
		{
			Dispose();
		}

		private void tpExplanation_Click(object sender, System.EventArgs e)
		{
		
		}

		private void tpEnvironment_Click(object sender, System.EventArgs e)
		{
		
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			buildExceptionString();
		}

		private void lblApplication_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
