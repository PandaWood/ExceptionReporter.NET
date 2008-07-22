#define SMTP_AUTH
// if defined, SMTP_AUTH allows support for SMTP authentication...note that this is not supported
// prior to .Net Framework 1.1  - code is conditionally excluded when compiling for 1.0 by undefining
// SMTP_AUTH


// created on 17/03/2004 at 07:24
using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Management;
using System.Configuration;
using System.Text;
using System.IO;
using System.Web.Mail;
using Win32Mapi;
using System.Reflection;
using System.Diagnostics;

//-------------------------------------------------------------------------
// ExceptionReporter - Error Reporting Component for .Net
//
// Copyright (C) 2004  Phillip Pettit / Stratalogic Software
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//--------------------------------------------------------------------------

namespace SLSExceptionReporter {
	
	
	
	internal class frmER : System.Windows.Forms.Form
	{
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.GroupBox grbStackTrace;
		private System.Windows.Forms.ProgressBar pbrProgress;
		private System.Windows.Forms.TabPage tpExceptions;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblWebSite;
		private System.Windows.Forms.Label lblPhone;
		private System.Windows.Forms.TabControl tcTabs;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.GroupBox grbMessage;
		private System.Windows.Forms.TabPage tpContact;
		private System.Windows.Forms.TabPage tpSettings;
		private System.Windows.Forms.LinkLabel lnkWeb;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.GroupBox gbLineGeneral1;
		private System.Windows.Forms.Label lblRegion;
		private System.Windows.Forms.TextBox txtApplication;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label lblContactMessageBottom;
		private System.Windows.Forms.Label lblGeneral;
		private System.Windows.Forms.TabPage tpAssemblies;
		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.TabPage tpEnvironment;
		private System.Windows.Forms.LinkLabel lnkEmail;
		private System.Windows.Forms.Label lblContactMessageTop;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.Label lblExplanation;
		private System.Windows.Forms.Button cmdCopy;
		private System.Windows.Forms.TreeView tvwEnvironment;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblFax;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.TextBox txtMachine;
		private System.Windows.Forms.TreeView tvwSettings;
		private System.Windows.Forms.ListView lstAssemblies;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.TabPage tpGeneral;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.TextBox txtTime;
		private System.Windows.Forms.ListView lstExceptions;
		private System.Windows.Forms.Button cmdPrint;
		private System.Windows.Forms.TextBox txtStackTrace;
		private System.Windows.Forms.TextBox txtDate;
		private System.Windows.Forms.Button cmdEmail;
		private System.Windows.Forms.PictureBox picGeneral;
		private System.Windows.Forms.TextBox txtExplanation;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel lnkTrial;
		private System.Windows.Forms.TextBox txtFax;
		private System.Windows.Forms.TextBox txtRegion;
		private System.Windows.Forms.TextBox txtVersion;
		private System.Windows.Forms.Label lblMachine;
		/**********************************************************
		 * Main Form of the Exception Reporter Class
		 * 
		 * Developer		Date		Comment  
		 * Phillip Pettit	Mar/04		Initial Creation
		 **********************************************************/
		
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
		private Font boldFont;

		private int drawWidth = 0;
		private int drawHeight = 0;
		private int PageCount = 0;

		private ExceptionReporter.slsMailType sendMailType = ExceptionReporter.slsMailType.SimpleMAPI;
		
		private License license = null;
		private bool blnLicensed = false;
		private Assembly cAssembly = null;
		
		private bool refreshData = false;
		
		public frmER()
		{
			/**********************************************************
			 * Constructor
			 * 
			 * Pass:	
			 * Returns: 
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			InitializeComponent();
		}
		

		private bool setTabs() {
			/**********************************************************
			 * setTabs (hide or show tabs as appropriate)
			 * 
			 *  Pass: None
			 *  Return: True for success, false otherwise
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
		
			try {
				// remove all the tabs to start with
				tcTabs.TabPages.Clear();
				
				// add back the tabs one by one that have the appropriate
				// property set
				if (blnGeneralTab) {
					tcTabs.TabPages.Add(tpGeneral);
				}
				if (blnExceptionsTab) {
					tcTabs.TabPages.Add(tpExceptions);
				}
				if (blnAssembliesTab) {
					tcTabs.TabPages.Add(tpAssemblies);
				}
				if (blnSettingsTab) {
					tcTabs.TabPages.Add(tpSettings);
				}
				if (blnEnvironmentTab) {
					tcTabs.TabPages.Add(tpEnvironment);
				}
				if (blnContactTab) {
					tcTabs.TabPages.Add(tpContact);
				}
			} catch (Exception ex) {
				handleError("There has been a problem configuring tab page display within the Exception Reporter",ex);
				
			}
			return true;
		
		}

		private bool setButtons() {
			/**********************************************************
			 * setButtons (place buttons based on visibility settings)
			 * 
			 *  Pass: None
			 *  Return: True for success, false otherwise
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			int intX2Pos = 0;
			int intXSpacer = 10;
			
			try {
				intX2Pos = this.Width - 30;
				
				if (cmdEmail.Visible) {
					cmdEmail.Left = intX2Pos - cmdEmail.Width;
					intX2Pos -= cmdEmail.Width;
					intX2Pos -= intXSpacer;
				}
				if (cmdSave.Visible) {
					cmdSave.Left = intX2Pos - cmdSave.Width;
					intX2Pos -= cmdSave.Width;
					intX2Pos -= intXSpacer;
				}
				if (cmdCopy.Visible) {
					cmdCopy.Left = intX2Pos - cmdCopy.Width;
					intX2Pos -= cmdCopy.Width;
					intX2Pos -= intXSpacer;
				}
				if (cmdPrint.Visible) {
					cmdPrint.Left = intX2Pos - cmdPrint.Width;
					intX2Pos -= cmdPrint.Width;
					intX2Pos -= intXSpacer;
				}
			} catch (Exception ex) {
				handleError("There has been a problem configuring command button display within the Exception Reporter",ex);
				
			}
			return true;
			
			
		}
		
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			/**********************************************************
			 * Dispose any resources - object closing
			 * 
			 *  Pass: disposing
			 *  Return: 
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmER));
			this.lblMachine = new System.Windows.Forms.Label();
			this.txtVersion = new System.Windows.Forms.TextBox();
			this.txtRegion = new System.Windows.Forms.TextBox();
			this.txtFax = new System.Windows.Forms.TextBox();
			this.lnkTrial = new System.Windows.Forms.LinkLabel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtExplanation = new System.Windows.Forms.TextBox();
			this.picGeneral = new System.Windows.Forms.PictureBox();
			this.cmdEmail = new System.Windows.Forms.Button();
			this.txtDate = new System.Windows.Forms.TextBox();
			this.txtStackTrace = new System.Windows.Forms.TextBox();
			this.cmdPrint = new System.Windows.Forms.Button();
			this.lstExceptions = new System.Windows.Forms.ListView();
			this.txtTime = new System.Windows.Forms.TextBox();
			this.lblProgress = new System.Windows.Forms.Label();
			this.tpGeneral = new System.Windows.Forms.TabPage();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.lstAssemblies = new System.Windows.Forms.ListView();
			this.tvwSettings = new System.Windows.Forms.TreeView();
			this.txtMachine = new System.Windows.Forms.TextBox();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblFax = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.tvwEnvironment = new System.Windows.Forms.TreeView();
			this.cmdCopy = new System.Windows.Forms.Button();
			this.lblExplanation = new System.Windows.Forms.Label();
			this.lblEmail = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.lblContactMessageTop = new System.Windows.Forms.Label();
			this.lnkEmail = new System.Windows.Forms.LinkLabel();
			this.tpEnvironment = new System.Windows.Forms.TabPage();
			this.lblApplication = new System.Windows.Forms.Label();
			this.tpAssemblies = new System.Windows.Forms.TabPage();
			this.lblGeneral = new System.Windows.Forms.Label();
			this.lblContactMessageBottom = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.txtApplication = new System.Windows.Forms.TextBox();
			this.lblRegion = new System.Windows.Forms.Label();
			this.gbLineGeneral1 = new System.Windows.Forms.GroupBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.lnkWeb = new System.Windows.Forms.LinkLabel();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.tpContact = new System.Windows.Forms.TabPage();
			this.grbMessage = new System.Windows.Forms.GroupBox();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.tcTabs = new System.Windows.Forms.TabControl();
			this.lblPhone = new System.Windows.Forms.Label();
			this.lblWebSite = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.tpExceptions = new System.Windows.Forms.TabPage();
			this.pbrProgress = new System.Windows.Forms.ProgressBar();
			this.grbStackTrace = new System.Windows.Forms.GroupBox();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.tpGeneral.SuspendLayout();
			this.tpEnvironment.SuspendLayout();
			this.tpAssemblies.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.tpContact.SuspendLayout();
			this.grbMessage.SuspendLayout();
			this.tcTabs.SuspendLayout();
			this.tpExceptions.SuspendLayout();
			this.grbStackTrace.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblMachine
			// 
			this.lblMachine.Location = new System.Drawing.Point(16, 15);
			this.lblMachine.Name = "lblMachine";
			this.lblMachine.Size = new System.Drawing.Size(56, 16);
			this.lblMachine.TabIndex = 16;
			this.lblMachine.Text = "&Machine:";
			// 
			// txtVersion
			// 
			this.txtVersion.BackColor = System.Drawing.SystemColors.Control;
			this.txtVersion.Location = new System.Drawing.Point(80, 120);
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.ReadOnly = true;
			this.txtVersion.Size = new System.Drawing.Size(152, 20);
			this.txtVersion.TabIndex = 6;
			this.txtVersion.Text = "";
			// 
			// txtRegion
			// 
			this.txtRegion.BackColor = System.Drawing.SystemColors.Control;
			this.txtRegion.Location = new System.Drawing.Point(312, 120);
			this.txtRegion.Name = "txtRegion";
			this.txtRegion.ReadOnly = true;
			this.txtRegion.Size = new System.Drawing.Size(168, 20);
			this.txtRegion.TabIndex = 8;
			this.txtRegion.Text = "";
			// 
			// txtFax
			// 
			this.txtFax.BackColor = System.Drawing.SystemColors.Control;
			this.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtFax.Location = new System.Drawing.Point(72, 168);
			this.txtFax.Name = "txtFax";
			this.txtFax.Size = new System.Drawing.Size(240, 13);
			this.txtFax.TabIndex = 35;
			this.txtFax.Text = "";
			// 
			// lnkTrial
			// 
			this.lnkTrial.Location = new System.Drawing.Point(8, 296);
			this.lnkTrial.Name = "lnkTrial";
			this.lnkTrial.Size = new System.Drawing.Size(472, 32);
			this.lnkTrial.TabIndex = 37;
			this.lnkTrial.TabStop = true;
			this.lnkTrial.Text = "For information on how to purchase this product visit www.stratalogic.com";
			this.lnkTrial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lnkTrial.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkTrialLinkClicked);
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(8, 176);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(472, 8);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			// 
			// txtExplanation
			// 
			this.txtExplanation.Location = new System.Drawing.Point(8, 224);
			this.txtExplanation.Multiline = true;
			this.txtExplanation.Name = "txtExplanation";
			this.txtExplanation.Size = new System.Drawing.Size(472, 104);
			this.txtExplanation.TabIndex = 15;
			this.txtExplanation.Text = "";
			// 
			// picGeneral
			// 
			this.picGeneral.Image = ((System.Drawing.Image)(resources.GetObject("picGeneral.Image")));
			this.picGeneral.Location = new System.Drawing.Point(15, 5);
			this.picGeneral.Name = "picGeneral";
			this.picGeneral.Size = new System.Drawing.Size(64, 64);
			this.picGeneral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picGeneral.TabIndex = 22;
			this.picGeneral.TabStop = false;
			// 
			// cmdEmail
			// 
			this.cmdEmail.Image = ((System.Drawing.Image)(resources.GetObject("cmdEmail.Image")));
			this.cmdEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdEmail.Location = new System.Drawing.Point(424, 370);
			this.cmdEmail.Name = "cmdEmail";
			this.cmdEmail.Size = new System.Drawing.Size(72, 32);
			this.cmdEmail.TabIndex = 43;
			this.cmdEmail.Text = "&E-Mail";
			this.cmdEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdEmail.Click += new System.EventHandler(this.cmdEmail_Click);
			// 
			// txtDate
			// 
			this.txtDate.BackColor = System.Drawing.SystemColors.Control;
			this.txtDate.Location = new System.Drawing.Point(80, 152);
			this.txtDate.Name = "txtDate";
			this.txtDate.ReadOnly = true;
			this.txtDate.Size = new System.Drawing.Size(152, 20);
			this.txtDate.TabIndex = 10;
			this.txtDate.Text = "";
			// 
			// txtStackTrace
			// 
			this.txtStackTrace.BackColor = System.Drawing.SystemColors.Control;
			this.txtStackTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtStackTrace.Location = new System.Drawing.Point(4, 12);
			this.txtStackTrace.Multiline = true;
			this.txtStackTrace.Name = "txtStackTrace";
			this.txtStackTrace.ReadOnly = true;
			this.txtStackTrace.Size = new System.Drawing.Size(460, 84);
			this.txtStackTrace.TabIndex = 26;
			this.txtStackTrace.Text = "";
			// 
			// cmdPrint
			// 
			this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
			this.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdPrint.Location = new System.Drawing.Point(184, 370);
			this.cmdPrint.Name = "cmdPrint";
			this.cmdPrint.Size = new System.Drawing.Size(72, 32);
			this.cmdPrint.TabIndex = 40;
			this.cmdPrint.Text = "&Print";
			this.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
			// 
			// lstExceptions
			// 
			this.lstExceptions.FullRowSelect = true;
			this.lstExceptions.Location = new System.Drawing.Point(8, 8);
			this.lstExceptions.Name = "lstExceptions";
			this.lstExceptions.Size = new System.Drawing.Size(472, 120);
			this.lstExceptions.TabIndex = 22;
			this.lstExceptions.View = System.Windows.Forms.View.Details;
			this.lstExceptions.SelectedIndexChanged += new System.EventHandler(this.LstExceptionsSelectedIndexChanged);
			// 
			// txtTime
			// 
			this.txtTime.BackColor = System.Drawing.SystemColors.Control;
			this.txtTime.Location = new System.Drawing.Point(312, 152);
			this.txtTime.Name = "txtTime";
			this.txtTime.ReadOnly = true;
			this.txtTime.Size = new System.Drawing.Size(168, 20);
			this.txtTime.TabIndex = 12;
			this.txtTime.Text = "";
			// 
			// lblProgress
			// 
			this.lblProgress.Location = new System.Drawing.Point(16, 368);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(128, 16);
			this.lblProgress.TabIndex = 38;
			this.lblProgress.Text = "Loading System Info...";
			// 
			// tpGeneral
			// 
			this.tpGeneral.Controls.Add(this.lblExplanation);
			this.tpGeneral.Controls.Add(this.txtExplanation);
			this.tpGeneral.Controls.Add(this.lblRegion);
			this.tpGeneral.Controls.Add(this.txtRegion);
			this.tpGeneral.Controls.Add(this.picGeneral);
			this.tpGeneral.Controls.Add(this.gbLineGeneral1);
			this.tpGeneral.Controls.Add(this.lblDate);
			this.tpGeneral.Controls.Add(this.txtDate);
			this.tpGeneral.Controls.Add(this.lblGeneral);
			this.tpGeneral.Controls.Add(this.lblTime);
			this.tpGeneral.Controls.Add(this.txtTime);
			this.tpGeneral.Controls.Add(this.groupBox2);
			this.tpGeneral.Controls.Add(this.lblApplication);
			this.tpGeneral.Controls.Add(this.txtApplication);
			this.tpGeneral.Controls.Add(this.lblVersion);
			this.tpGeneral.Controls.Add(this.txtVersion);
			this.tpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Size = new System.Drawing.Size(488, 334);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = "General";
			this.tpGeneral.ToolTipText = "Displays General Information about the application error";
			this.tpGeneral.Click += new System.EventHandler(this.tabPage1_Click);
			// 
			// lstAssemblies
			// 
			this.lstAssemblies.FullRowSelect = true;
			this.lstAssemblies.Location = new System.Drawing.Point(8, 8);
			this.lstAssemblies.Name = "lstAssemblies";
			this.lstAssemblies.Size = new System.Drawing.Size(472, 320);
			this.lstAssemblies.TabIndex = 21;
			this.lstAssemblies.View = System.Windows.Forms.View.Details;
			// 
			// tvwSettings
			// 
			this.tvwSettings.ImageIndex = -1;
			this.tvwSettings.Location = new System.Drawing.Point(8, 8);
			this.tvwSettings.Name = "tvwSettings";
			this.tvwSettings.SelectedImageIndex = -1;
			this.tvwSettings.Size = new System.Drawing.Size(472, 320);
			this.tvwSettings.TabIndex = 20;
			this.tvwSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSettings_AfterSelect);
			// 
			// txtMachine
			// 
			this.txtMachine.BackColor = System.Drawing.SystemColors.Control;
			this.txtMachine.Location = new System.Drawing.Point(80, 12);
			this.txtMachine.Name = "txtMachine";
			this.txtMachine.ReadOnly = true;
			this.txtMachine.Size = new System.Drawing.Size(144, 20);
			this.txtMachine.TabIndex = 17;
			this.txtMachine.Text = "";
			// 
			// lblTime
			// 
			this.lblTime.Location = new System.Drawing.Point(256, 152);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(40, 16);
			this.lblTime.TabIndex = 11;
			this.lblTime.Text = "&Time:";
			// 
			// lblFax
			// 
			this.lblFax.Location = new System.Drawing.Point(16, 168);
			this.lblFax.Name = "lblFax";
			this.lblFax.Size = new System.Drawing.Size(48, 24);
			this.lblFax.TabIndex = 34;
			this.lblFax.Text = "Fax:";
			// 
			// lblDate
			// 
			this.lblDate.Location = new System.Drawing.Point(16, 152);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(40, 16);
			this.lblDate.TabIndex = 9;
			this.lblDate.Text = "&Date:";
			// 
			// tvwEnvironment
			// 
			this.tvwEnvironment.ImageIndex = -1;
			this.tvwEnvironment.Location = new System.Drawing.Point(8, 40);
			this.tvwEnvironment.Name = "tvwEnvironment";
			this.tvwEnvironment.SelectedImageIndex = -1;
			this.tvwEnvironment.Size = new System.Drawing.Size(472, 288);
			this.tvwEnvironment.TabIndex = 24;
			this.tvwEnvironment.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwEnvironment_AfterSelect);
			// 
			// cmdCopy
			// 
			this.cmdCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopy.Image")));
			this.cmdCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdCopy.Location = new System.Drawing.Point(264, 370);
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Size = new System.Drawing.Size(72, 32);
			this.cmdCopy.TabIndex = 41;
			this.cmdCopy.Text = "&Copy";
			this.cmdCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
			// 
			// lblExplanation
			// 
			this.lblExplanation.Location = new System.Drawing.Point(16, 192);
			this.lblExplanation.Name = "lblExplanation";
			this.lblExplanation.Size = new System.Drawing.Size(464, 32);
			this.lblExplanation.TabIndex = 14;
			this.lblExplanation.Text = "Please enter a brief explanation detailing the actions and events leading up to t" +
"he occurrence of this error.";
			// 
			// lblEmail
			// 
			this.lblEmail.Location = new System.Drawing.Point(16, 56);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(40, 24);
			this.lblEmail.TabIndex = 28;
			this.lblEmail.Text = "E-Mail:";
			// 
			// lblUsername
			// 
			this.lblUsername.Location = new System.Drawing.Point(256, 15);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(64, 16);
			this.lblUsername.TabIndex = 18;
			this.lblUsername.Text = "&Username:";
			// 
			// txtMessage
			// 
			this.txtMessage.BackColor = System.Drawing.SystemColors.Control;
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMessage.Location = new System.Drawing.Point(8, 16);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new System.Drawing.Size(456, 64);
			this.txtMessage.TabIndex = 24;
			this.txtMessage.Text = "";
			// 
			// lblContactMessageTop
			// 
			this.lblContactMessageTop.Location = new System.Drawing.Point(8, 24);
			this.lblContactMessageTop.Name = "lblContactMessageTop";
			this.lblContactMessageTop.Size = new System.Drawing.Size(432, 24);
			this.lblContactMessageTop.TabIndex = 27;
			this.lblContactMessageTop.Text = "The following details can be used to obtain support for this application....";
			// 
			// lnkEmail
			// 
			this.lnkEmail.Location = new System.Drawing.Point(72, 56);
			this.lnkEmail.Name = "lnkEmail";
			this.lnkEmail.Size = new System.Drawing.Size(400, 16);
			this.lnkEmail.TabIndex = 29;
			this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEmail_LinkClicked);
			// 
			// tpEnvironment
			// 
			this.tpEnvironment.Controls.Add(this.lblMachine);
			this.tpEnvironment.Controls.Add(this.txtMachine);
			this.tpEnvironment.Controls.Add(this.lblUsername);
			this.tpEnvironment.Controls.Add(this.txtUserName);
			this.tpEnvironment.Controls.Add(this.tvwEnvironment);
			this.tpEnvironment.Location = new System.Drawing.Point(4, 22);
			this.tpEnvironment.Name = "tpEnvironment";
			this.tpEnvironment.Size = new System.Drawing.Size(488, 334);
			this.tpEnvironment.TabIndex = 3;
			this.tpEnvironment.Text = "Environment";
			this.tpEnvironment.ToolTipText = "Displays information about the environment in which the error occurred";
			this.tpEnvironment.Click += new System.EventHandler(this.tpEnvironment_Click);
			// 
			// lblApplication
			// 
			this.lblApplication.Location = new System.Drawing.Point(16, 90);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(64, 16);
			this.lblApplication.TabIndex = 3;
			this.lblApplication.Text = "&Application:";
			this.lblApplication.Click += new System.EventHandler(this.lblApplication_Click);
			// 
			// tpAssemblies
			// 
			this.tpAssemblies.Controls.Add(this.lstAssemblies);
			this.tpAssemblies.Location = new System.Drawing.Point(4, 22);
			this.tpAssemblies.Name = "tpAssemblies";
			this.tpAssemblies.Size = new System.Drawing.Size(488, 334);
			this.tpAssemblies.TabIndex = 6;
			this.tpAssemblies.Text = "Assemblies";
			this.tpAssemblies.ToolTipText = "Displays information about the assemblies referenced by this application";
			// 
			// lblGeneral
			// 
			this.lblGeneral.Location = new System.Drawing.Point(96, 16);
			this.lblGeneral.Name = "lblGeneral";
			this.lblGeneral.Size = new System.Drawing.Size(384, 48);
			this.lblGeneral.TabIndex = 1;
			this.lblGeneral.Text = "An error has occured in this application.";
			// 
			// lblContactMessageBottom
			// 
			this.lblContactMessageBottom.Location = new System.Drawing.Point(8, 200);
			this.lblContactMessageBottom.Name = "lblContactMessageBottom";
			this.lblContactMessageBottom.Size = new System.Drawing.Size(472, 96);
			this.lblContactMessageBottom.TabIndex = 36;
			this.lblContactMessageBottom.Text = "The information on this form describing the error and envrionment settings will b" +
"e of use when contacting support.";
			// 
			// txtUserName
			// 
			this.txtUserName.BackColor = System.Drawing.SystemColors.Control;
			this.txtUserName.Location = new System.Drawing.Point(328, 12);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.ReadOnly = true;
			this.txtUserName.Size = new System.Drawing.Size(152, 20);
			this.txtUserName.TabIndex = 19;
			this.txtUserName.Text = "";
			// 
			// txtApplication
			// 
			this.txtApplication.BackColor = System.Drawing.SystemColors.Control;
			this.txtApplication.Location = new System.Drawing.Point(80, 88);
			this.txtApplication.Name = "txtApplication";
			this.txtApplication.ReadOnly = true;
			this.txtApplication.Size = new System.Drawing.Size(400, 20);
			this.txtApplication.TabIndex = 4;
			this.txtApplication.Text = "";
			// 
			// lblRegion
			// 
			this.lblRegion.Location = new System.Drawing.Point(256, 120);
			this.lblRegion.Name = "lblRegion";
			this.lblRegion.Size = new System.Drawing.Size(48, 16);
			this.lblRegion.TabIndex = 7;
			this.lblRegion.Text = "&Region:";
			// 
			// gbLineGeneral1
			// 
			this.gbLineGeneral1.Location = new System.Drawing.Point(8, 64);
			this.gbLineGeneral1.Name = "gbLineGeneral1";
			this.gbLineGeneral1.Size = new System.Drawing.Size(472, 8);
			this.gbLineGeneral1.TabIndex = 2;
			this.gbLineGeneral1.TabStop = false;
			// 
			// cmdSave
			// 
			this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
			this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdSave.Location = new System.Drawing.Point(344, 370);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Size = new System.Drawing.Size(72, 32);
			this.cmdSave.TabIndex = 42;
			this.cmdSave.Text = "&Save";
			this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// lnkWeb
			// 
			this.lnkWeb.Location = new System.Drawing.Point(72, 80);
			this.lnkWeb.Name = "lnkWeb";
			this.lnkWeb.Size = new System.Drawing.Size(400, 56);
			this.lnkWeb.TabIndex = 31;
			this.lnkWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkWebLinkClicked);
			// 
			// tpSettings
			// 
			this.tpSettings.Controls.Add(this.tvwSettings);
			this.tpSettings.Location = new System.Drawing.Point(4, 22);
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.Size = new System.Drawing.Size(488, 334);
			this.tpSettings.TabIndex = 5;
			this.tpSettings.Text = "Settings";
			this.tpSettings.ToolTipText = "Displays Application Settings";
			this.tpSettings.Click += new System.EventHandler(this.tpSettings_Click);
			// 
			// tpContact
			// 
			this.tpContact.Controls.Add(this.lnkTrial);
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
			this.tpContact.Size = new System.Drawing.Size(488, 334);
			this.tpContact.TabIndex = 4;
			this.tpContact.Text = "Contact";
			this.tpContact.ToolTipText = "Contact details for this application";
			this.tpContact.Click += new System.EventHandler(this.tpContact_Click);
			// 
			// grbMessage
			// 
			this.grbMessage.Controls.Add(this.txtMessage);
			this.grbMessage.Location = new System.Drawing.Point(8, 136);
			this.grbMessage.Name = "grbMessage";
			this.grbMessage.Size = new System.Drawing.Size(472, 88);
			this.grbMessage.TabIndex = 23;
			this.grbMessage.TabStop = false;
			this.grbMessage.Text = "Message";
			// 
			// txtPhone
			// 
			this.txtPhone.BackColor = System.Drawing.SystemColors.Control;
			this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtPhone.Location = new System.Drawing.Point(72, 144);
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(240, 13);
			this.txtPhone.TabIndex = 33;
			this.txtPhone.Text = "";
			// 
			// tcTabs
			// 
			this.tcTabs.Controls.Add(this.tpGeneral);
			this.tcTabs.Controls.Add(this.tpExceptions);
			this.tcTabs.Controls.Add(this.tpAssemblies);
			this.tcTabs.Controls.Add(this.tpSettings);
			this.tcTabs.Controls.Add(this.tpEnvironment);
			this.tcTabs.Controls.Add(this.tpContact);
			this.tcTabs.HotTrack = true;
			this.tcTabs.Location = new System.Drawing.Point(8, 8);
			this.tcTabs.Name = "tcTabs";
			this.tcTabs.SelectedIndex = 0;
			this.tcTabs.ShowToolTips = true;
			this.tcTabs.Size = new System.Drawing.Size(496, 360);
			this.tcTabs.TabIndex = 0;
			this.tcTabs.SelectedIndexChanged += new System.EventHandler(this.tcTabs_SelectedIndexChanged);
			// 
			// lblPhone
			// 
			this.lblPhone.Location = new System.Drawing.Point(16, 144);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(48, 24);
			this.lblPhone.TabIndex = 32;
			this.lblPhone.Text = "Phone:";
			// 
			// lblWebSite
			// 
			this.lblWebSite.Location = new System.Drawing.Point(16, 80);
			this.lblWebSite.Name = "lblWebSite";
			this.lblWebSite.Size = new System.Drawing.Size(40, 24);
			this.lblWebSite.TabIndex = 30;
			this.lblWebSite.Text = "Web:";
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(16, 120);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(48, 16);
			this.lblVersion.TabIndex = 5;
			this.lblVersion.Text = "&Version:";
			// 
			// tpExceptions
			// 
			this.tpExceptions.Controls.Add(this.lstExceptions);
			this.tpExceptions.Controls.Add(this.grbMessage);
			this.tpExceptions.Controls.Add(this.grbStackTrace);
			this.tpExceptions.Location = new System.Drawing.Point(4, 22);
			this.tpExceptions.Name = "tpExceptions";
			this.tpExceptions.Size = new System.Drawing.Size(488, 334);
			this.tpExceptions.TabIndex = 1;
			this.tpExceptions.Text = "Exceptions";
			this.tpExceptions.ToolTipText = "Displays details information about the exception(s) that occurred";
			this.tpExceptions.Click += new System.EventHandler(this.tabPage2_Click);
			// 
			// pbrProgress
			// 
			this.pbrProgress.Location = new System.Drawing.Point(16, 384);
			this.pbrProgress.Name = "pbrProgress";
			this.pbrProgress.Size = new System.Drawing.Size(128, 16);
			this.pbrProgress.TabIndex = 39;
			// 
			// grbStackTrace
			// 
			this.grbStackTrace.Controls.Add(this.txtStackTrace);
			this.grbStackTrace.Location = new System.Drawing.Point(8, 224);
			this.grbStackTrace.Name = "grbStackTrace";
			this.grbStackTrace.Size = new System.Drawing.Size(472, 104);
			this.grbStackTrace.TabIndex = 25;
			this.grbStackTrace.TabStop = false;
			this.grbStackTrace.Text = "Stack Trace";
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// frmER
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(514, 408);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.pbrProgress);
			this.Controls.Add(this.cmdEmail);
			this.Controls.Add(this.tcTabs);
			this.Controls.Add(this.cmdPrint);
			this.Controls.Add(this.cmdSave);
			this.Controls.Add(this.cmdCopy);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmER";
			this.Text = "Exception Reporter: An Error has occured in this application";
			this.Load += new System.EventHandler(this.UserControl1_Load);
			this.tpGeneral.ResumeLayout(false);
			this.tpEnvironment.ResumeLayout(false);
			this.tpAssemblies.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.tpContact.ResumeLayout(false);
			this.grbMessage.ResumeLayout(false);
			this.tcTabs.ResumeLayout(false);
			this.tpExceptions.ResumeLayout(false);
			this.grbStackTrace.ResumeLayout(false);
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
		

		public void sendMAPIEmail()
		{
			/**********************************************************
			 * sendMAPIEmail - send an e-mail using the SimpleMAPI component
			 * 
			 *  Pass: 
			 *  Return:  void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			try{
				
				Mapi ma = new Mapi();
				ma.Logon(this.Handle);
				
				ma.Reset();
				if (strSendEmailAddress != null) {
					if (strSendEmailAddress.Length > 0) {
						ma.AddRecip(strSendEmailAddress,null,false);		
					}
				}
				
				ma.Send("An Exception has occured",sbExceptionString.ToString(),true);
				ma.Logoff();
				
			} catch (Exception ex) {
				handleError("There has been a problem sending e-mail through Simple MAPI. A suitable mail client or required protocols may not be configured on the machine. Instead, you can use the copy button to place details of the error onto the clipboard, you can then paste this information directly into your mail client",ex);				
			}
		}
		
		public void sendSMTPEmail(){
			/**********************************************************
			 * sendSMTPEmail - send an e-mail using SMTP
			 *  - makes use of SMTP settings passed to the object
			 * 
			 *  Pass: 
			 *  Return:  void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			try {
				
				MailMessage objMyMessage;
				
				objMyMessage = new MailMessage();
				            
	            objMyMessage.To = strSendEmailAddress;
				objMyMessage.From = strSMTPFromAddress;
	            objMyMessage.Subject = "An Error has occured";
	            objMyMessage.Body =sbExceptionString.ToString(); 
	            objMyMessage.BodyFormat = MailFormat.Text;
				
				#if SMTP_AUTH
					// conditionally include support for SMTP authentication
					if (strSMTPUsername != null) {
						objMyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");	//basic authentication
						objMyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendUSERNAME", strSMTPUsername); //set your USERNAME here
						
						if (strSMTPPassword != null) {
							objMyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", strSMTPPassword);	//set your password here
						}
					}
				#endif
				
				if (strSMTPServer != null) {
					SmtpMail.SmtpServer = strSMTPServer;
				}
	          
	        	SmtpMail.Send(objMyMessage);	
        	} catch (Exception ex) {
				handleError("There has been a problem sending e-mail through SMTP. Suitable configuration details or required protocols may not be configured on the machine. Instead, you can use the copy button to place details of the error onto the clipboard, you can then paste this information directly into your mail client",ex);				
			}

		}
		// pivate boolean variable to store state of tab page
		// default to true
		private bool blnGeneralTab = true;
		private bool blnEnvironmentTab = true;
		private bool blnSettingsTab = true;
		private bool blnContactTab = true;
		private bool blnExceptionsTab = true;
		private bool blnAssembliesTab = true;
		private bool blnEnumeratePrinters = true;

		// public property used to set/get visibility of Tab
		public bool ShowGeneralTab
		{
			/**********************************************************
			 * Property: ShowGeneralTab
			 * 
			 *  If true the General Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
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
		
		// public property used to set/get printer enumeration setting
		public bool EnumeratePrinters
		{
			/**********************************************************
			 * Property: EnumeratePrinters
			 * 
			 *  If true the General Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/05		Initial Creation
			 **********************************************************/
			get
			{
				//return settings
				return blnEnumeratePrinters;
			}
			set
			{
				// set settings
				blnEnumeratePrinters = value;
			}
		}
		
		// public property used to set/get visibility of Tab
		public bool ShowEnvironmentTab
		{
			/**********************************************************
			 * Property: ShowEnvironmentTab
			 * 
			 *  If true the Environment Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
		public bool ShowAssembliesTab
		{
			/**********************************************************
			 * Property: ShowAssembliesTab
			 * 
			 *  If true the Assemblies Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return visibility
				return blnAssembliesTab;
			}
			set
			{
				// set visibility
				blnAssembliesTab= value;
				// hide or show as appropriate
				setTabs();
				
			}
		}
		private String strSMTPServer = null;
		// public property used to set/get SMTP Server
		public String SMTPServer
		{
			/**********************************************************
			 * Property: SMTPServer
			 * 
			 * Relevant for SMTP mail only..the server to send e-mail
			 * through..if none is specified the local machine is used
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			get
			{
				//return SMTP Server
				return strSMTPServer;
			}
			set
			{
				// set SMTP Server
				strSMTPServer = value;
				
			}
		}
		private String strSMTPUsername = null;
		// public property used to set/get UserName
		public String SMTPUsername
		{
			/**********************************************************
			 * Property: SMTPUsername
			 * 
			 * Relevant for SMTP mail only..the username used for
			 *  SMTP authentication. Supported in .Net 1.1 + only
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return UserName
				return strSMTPUsername;
			}
			set
			{
				// set UserName
				strSMTPUsername = value;
				
			}
		}
		private String strSMTPPassword = null;
		// public property used to set/get Password
		public String SMTPPassword
		{
			/**********************************************************
			 * Property: SMTPPassword
			 * 
			 * Relevant for SMTP mail only..the password used for
			 *  SMTP authentication. Supported in .Net 1.1 + only
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return Password
				return strSMTPPassword;
			}
			set
			{
				// set Password
				strSMTPPassword = value;
				
			}
		}
		private String strSMTPFromAddress = null;
		// public property used to set/get From Address
		public String SMTPFromAddress
		{
			/**********************************************************
			 * Property: SMTPFromAddress
			 * 
			 * Relevant for SMTP mail only..the address from which it will
			 * appear that Exception Reporter e-mail has come from
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return From Address
				return strSMTPFromAddress;
			}
			set
			{
				// set From Address
				strSMTPFromAddress = value;
				
			}
		}
		private String strSendEmailAddress = null;
		// public property used to set/get send email address
		public String SendEmailAddress
		{
			/**********************************************************
			 * Property: SendEmailAddress
			 * 
			 * The e-mail address to which Email will be sent...if not specified
			 * then e-mail is sent to the ContactEmail address
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return send email address
				return strSendEmailAddress;
			}
			set
			{
				// set send email address
				strSendEmailAddress = value;
				
			}
		}
		
		// public property used to set/get visibility of Tab
		public bool ShowSettingsTab
		{
			/**********************************************************
			 * Property: ShowSettingsTab
			 * 
			 *  If true the Settings Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
			/**********************************************************
			 * Property: ShowContactTab
			 * 
			 *  If true the Contact Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
		public bool ShowExceptionsTab
		{
			/**********************************************************
			 * Property: ShowExceptionsTab
			 * 
			 *  If true the Exceptions Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return visibility
				return blnExceptionsTab;
			}
			set
			{
				// set visibility
				blnExceptionsTab = value;
				// hide or show as appropriate
				setTabs();
				
			}
		}

		public bool ShowCopyButton
		{
			/**********************************************************
			 * Property: ShowCopyButton
			 * 
			 *  If true the Copy (to clipboard) Button is displayed on the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return visibility
				return cmdCopy.Visible;
			}
			set
			{
				// set visibility
				cmdCopy.Visible = value;
				setButtons();
			}
		}
		public bool ShowEmailButton
		{
			/**********************************************************
			 * Property: ShowEmailButton
			 * 
			 *  If true the E-Mail Button is displayed on the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return visibility
				return cmdEmail.Visible;
			}
			set
			{
				// set visibility
				cmdEmail.Visible = value;
				setButtons();
			}
		}
		public bool ShowSaveButton
		{
			/**********************************************************
			 * Property: ShowSaveButton
			 * 
			 *  If true the Save Button is displayed on the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return visibility
				return cmdSave.Visible;
			}
			set
			{
				// set visibility
				cmdSave.Visible = value;
				setButtons();
			}
		}
		public bool ShowPrintButton
		{
			/**********************************************************
			 * Property: ShowPrintButton
			 * 
			 *  If true the Print Button is displayed on the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return visibility
				return cmdPrint.Visible;
			}
			set
			{
				// set visibility
				cmdPrint.Visible = value;
				setButtons();
			}
		}

		// public property used to set/get contact email
		public String ContactEmail
		{
			/**********************************************************
			 * Property: ContactEmail
			 * 
			 *  The Contact Email address displayed by the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
			/**********************************************************
			 * Property: ContactWeb
			 * 
			 *  The Contact Web address displayed by the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
			/**********************************************************
			 * Property: ContactPhone
			 * 
			 *  The Contact Phone displayed by the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
			/**********************************************************
			 * Property: ContactFax
			 * 
			 *  The Contact Fax displayed by the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
			/**********************************************************
			 * Property: ContactMessageTop
			 * 
			 *  The Contact Message displayed above the contact details
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
			/**********************************************************
			 * Property: ContactMessageBottom
			 * 
			 *  The Contact Message displayed below the contact details
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
			/**********************************************************
			 * Property: GeneralMessage
			 * 
			 *  The General Message displayed at the top of the General Tab
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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
		
		public ExceptionReporter.slsMailType MailType
		{
			/**********************************************************
			 * Property: MailType
			 * 
			 *  Specifies wether SMTP or SimpleMAPI is used
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			get
			{
				//return Mail Type
				return sendMailType;
			}
			set
			{
				// set Mail Type
				sendMailType = value;
				
			}
		}

		public String ExplanationMessage
		{
			/**********************************************************
			 * Property: ExplanationMessage
			 * 
			 * The message displayed above the text box for entering
			 * User Explanation
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
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

		private bool buildExceptionString() {
			/**********************************************************
			 * buildExceptionString - sets an internal field with a string
			 * representation of the info shown by the Exception Reporter
			 * 
			 * Pass: none
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			return buildExceptionString(true,true,true,true,true,true,false);
		}
                                  
		private bool buildExceptionString(bool blnGeneral, bool blnExceptions, bool blnAssemblies, bool blnSettings, bool blnEnvironment, bool blnContact, bool blnForPrint) 
		{
			/**********************************************************
			 * buildExceptionString - sets an internal field with a string
			 * representation of the info shown by the Exception Reporter
			 * 
			 * Pass:	blnGeneral		- include General Information
			 * 			blnExceptions	- include Exception Information
			 * 			blnAssemblies	- include Assemblies Information
			 * 			blnSettings		- include Settings Information
			 * 			blnEnvironment	- include Environment Information
			 * 			blnContact		- include Contact Information
			 * 
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/

			try {
				
				sbExceptionString = new StringBuilder();
				StringWriter swWriter = new StringWriter(sbExceptionString);
	
	
				if (blnGeneral)	{
					
					if (!blnForPrint) {
						swWriter.WriteLine(lblGeneral.Text);
						swWriter.WriteLine((String)null);
						swWriter.WriteLine("-----------------------------");
						swWriter.WriteLine((String)null);
					}
					swWriter.WriteLine("General");
					swWriter.WriteLine((String)null);
					swWriter.WriteLine("Application: " + txtApplication.Text);
					swWriter.WriteLine("Version:     " + txtVersion.Text);
					swWriter.WriteLine("Region:      " + txtRegion.Text);
					swWriter.WriteLine("Machine:     " + " " + txtMachine.Text);
					swWriter.WriteLine("User:        " + txtUserName.Text);
					swWriter.WriteLine("-----------------------------");
					if (!blnForPrint) {
						swWriter.WriteLine((String)null);
						swWriter.WriteLine("Date: " + txtDate.Text);
						swWriter.WriteLine("Time: " + txtTime.Text);
						swWriter.WriteLine("-----------------------------");
					}
					swWriter.WriteLine((String)null);
					swWriter.WriteLine("Explanation");
					swWriter.WriteLine(txtExplanation.Text.Trim());
					swWriter.WriteLine((String)null);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String)null);
				}
				
				if (blnExceptions)	{
					swWriter.WriteLine("Exceptions");
					swWriter.WriteLine((String)null);
					exceptionHeirarchyToString(swWriter);
					swWriter.WriteLine((String)null);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String)null);
				}
				
				if (blnAssemblies)
				{
					swWriter.WriteLine("Assemblies");
					swWriter.WriteLine((String)null);
					referencedAssembliesToString(swWriter);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String)null);
				}
				
				if (blnSettings)
				{
					treeToString(tvwSettings,swWriter);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String)null);	
				}
				
				if (blnEnvironment)
				{
					treeToString(tvwEnvironment,swWriter);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String)null);
				}
				
				if (blnContact)	{
					swWriter.WriteLine("Contact");
					swWriter.WriteLine((String)null);			
					swWriter.WriteLine("E-Mail: " + lnkEmail.Text);
					swWriter.WriteLine("Web:    " + lnkWeb.Text);
					swWriter.WriteLine("Phone:  " + txtPhone.Text);
					swWriter.WriteLine("Fax:    " + txtFax.Text);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String)null);
				}
			} catch (Exception ex) {
				handleError("There has been a problem building exception details into a string for printing, copying, saving or e-mailing",ex);				
			}

			return true;
		}

		private bool treeToString(TreeView tvConvert, StringWriter swTreeWriter) 
		{
			/**********************************************************
			 * treeToString - convert a tree representation to text
			 * for use within the Exception String (for printing etc)
			 * 
			 * Pass:	tvConvert - the tree to convert
			 * 			swTreeWriter - the String Writer object
			 * 
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			treeNodeToString(tvConvert.Nodes[0],swTreeWriter,0);

			// bubble error back
			return true;

		}
		private bool treeNodeToString(TreeNode tnNode, StringWriter swWriter, Int32 intLevel) 
		{
			/**********************************************************
			 * treeNodeToString - convert a tree node
			 * for use within the Exception String (for printing etc)
			 * 
			 * Pass:	tnNode - the node within a tree to convert
			 * 			swTreeWriter - the String Writer object
			 * 			intLevel - indicates how far down the tree hierarchy we are
			 * 
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			 
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
			//bubble error back
			return true;
			
		}

		private bool referencedAssembliesToString(StringWriter swWriter) {
			/**********************************************************
			 * referencedAssembliesToString - convert a set of referenced
			 * assemblies to a string for use within the Exception String (for printing etc)
			 * 
			 * Pass:	swWriter - the String Writer object
			 * 
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			if (cAssembly == null) {
				return false;
			}
			foreach (AssemblyName a in
				cAssembly.GetReferencedAssemblies()) {
					
				swWriter.WriteLine(a.FullName);
				swWriter.WriteLine((String)null);
			}
			//bubble error back
			
			return true;
		}
		private bool exceptionHeirarchyToString(StringWriter swWriter) 
		{
			/**********************************************************
			 * exceptionHeirarchyToString - convert a an exception and it's
			 * hierarchy of inner exceptions to a string for use within the 
			 * Exception String (for printing etc)
			 * 
			 * Pass:	swWriter - the String Writer object
			 * 
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
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
				swWriter.WriteLine("Type:        " + current.GetType().ToString());
				swWriter.WriteLine("Message:     " + current.Message);
				swWriter.WriteLine("Source:      " + current.Source);
				swWriter.WriteLine("Stack Trace: " + current.StackTrace.ToString().Trim());
				swWriter.WriteLine((String)null);

				current = current.InnerException;
				intCount++;
			}
			// bubble error back
			return true;

		}

		private bool wrapText(StringReader sr, StringWriter sw, Int32 intMaxLineChars) 
		{
			/**********************************************************
			 * wrapText - convert a string containing long lines to a string
			 * with lines wrapped at a maximum character length
			 * 
			 * Pass:	sr - A reader for the source string
			 * 			sw - the String Writer object
			 * 			intMaxLineChars - max chars in any one line
			 * 
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			String strLine;
			String strSubLine;
			int intPos;

			strLine = sr.ReadLine();
			while (strLine != null) 
			{
				// handle blank lines
				if (strLine.Length == 0) 
				{
					sw.WriteLine(strLine);
				}
				
				// handle long lines
				while (strLine.Length > intMaxLineChars) 
				{
					strSubLine = strLine.Substring(0,intMaxLineChars);
					intPos = strSubLine.LastIndexOf(" ");
					if (intPos > intMaxLineChars - 7) 
					{
						// ie if space occurs within last set of characters
						// then wrap at the space (not in the middle of a word)
						strSubLine = strSubLine.Substring(0,intPos);
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
			// bubble error back
			return true;

		}
		protected override void OnActivated(System.EventArgs e)
		{
			/**********************************************************
			 * OnActivated - handle the Activate event of the form
			 * set the forms controls to have relevant details
			 * 
			 * Pass:	e - EventArgs
			 * 
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			try {
				
				base.OnActivated(e);
				// only refresh when we need to
				if (!refreshData) {
				 return;
				}
				// next time we won't refresh unless this flag is set back to true
				refreshData = false;
				
				setButtons();
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
	
				lblProgress.Visible = true;
				pbrProgress.Visible = true;
				pbrProgress.Maximum = 14;
				
				pbrProgress.Value = 0;
				
	            // general tab
				txtDate.Text = System.DateTime.Now.ToShortDateString();
				txtTime.Text = System.DateTime.Now.ToLongTimeString();
				txtUserName.Text = Environment.UserName;
				txtMachine.Text = Environment.MachineName;
				
				txtRegion.Text = Application.CurrentCulture.DisplayName;
				txtApplication.Text = Application.ProductName;
				txtVersion.Text = Application.ProductVersion;
	
				pbrProgress.Value = pbrProgress.Value + 1;
				Application.DoEvents();
	
				TreeNode root = new TreeNode("Environment");
			
				try {
					// Environment Tab
					addEnvironmentNode2("Operating System","Win32_OperatingSystem",root,false,"");
					
				} catch (Exception ex) {
					// do nothing, some environment nodes aren't available on all machines
				} finally {
					pbrProgress.Value = pbrProgress.Value + 1;
					Application.DoEvents();
				}
				try {
					addEnvironmentNode2("CPU","Win32_Processor",root,true,"");
				} catch (Exception ex) {
					// do nothing, some environment nodes aren't available on all machines
				} finally {
					pbrProgress.Value = pbrProgress.Value + 1;
					Application.DoEvents();
				}
				try {
					addEnvironmentNode2("Memory","Win32_PhysicalMemory",root,true,"");
				} catch (Exception ex) {
					// do nothing, some environment nodes aren't available on all machines
				} finally {
					pbrProgress.Value = pbrProgress.Value + 1;
					Application.DoEvents();
				}
				
				try {
					addEnvironmentNode2("Drives","Win32_DiskDrive",root,true,"");
				} catch (Exception ex) {
					// do nothing, some environment nodes aren't available on all machines
				} finally {
					pbrProgress.Value = pbrProgress.Value + 1;
					Application.DoEvents();
				}
				
				try {
					addEnvironmentNode2("Environment Variables","Win32_Environment",root,true,"");
				} catch (Exception ex) {
					// do nothing, some environment nodes aren't available on all machines
				} finally {
					pbrProgress.Value = pbrProgress.Value + 1;
					Application.DoEvents();
				}
	
				try {
					if (this.EnumeratePrinters) {
						addEnvironmentNode2("Printers","Win32_Printer",root,true,"");
					}
				} catch (Exception ex) {
					// do nothing, some environment nodes aren't available on all machines
				} finally {
					pbrProgress.Value = pbrProgress.Value + 1;
					Application.DoEvents();
				}
	
				try {
					addEnvironmentNode2("System","Win32_ComputerSystem",root,true,"");
				} catch (Exception ex) {
					// do nothing, some environment nodes aren't available on all machines
				} finally {
					pbrProgress.Value = pbrProgress.Value + 1;
					Application.DoEvents();
				}
				
				tvwEnvironment.Nodes.Add(root);
				root.Expand();
				
				// fill the settings tab
				TreeNode settingsRoot = new TreeNode("Application Settings");
				
				IEnumerator ienum = ConfigurationSettings.AppSettings.GetEnumerator();
	
				while(ienum.MoveNext())
				{
					settingsRoot.Nodes.Add(new TreeNode(ienum.Current.ToString() + " : " + ConfigurationSettings.AppSettings.Get(ienum.Current.ToString())));
				}
	
				tvwSettings.Nodes.Add(settingsRoot);
				settingsRoot.Expand();
				pbrProgress.Value = pbrProgress.Value + 1;
				Application.DoEvents();
	
				buildExceptionHeirarchy(exSelected);
				pbrProgress.Value = pbrProgress.Value + 1;
				Application.DoEvents();
	
				lstAssemblies.Clear();
				lstAssemblies.Columns.Add("Name",100,HorizontalAlignment.Left);
				lstAssemblies.Columns.Add("Version",150,HorizontalAlignment.Left);
				lstAssemblies.Columns.Add("Culture",150,HorizontalAlignment.Left);
				
				foreach (AssemblyName a in
					cAssembly.GetReferencedAssemblies()) {
						
					ListViewItem lv;
					lv = new ListViewItem();
					lv.Text= a.Name;
					lv.SubItems.Add(a.Version.ToString());
					lv.SubItems.Add(a.CultureInfo.EnglishName);
					lstAssemblies.Items.Add(lv);	
				}
	
				pbrProgress.Value = pbrProgress.Maximum;
				Application.DoEvents();
	
				lblProgress.Visible = false;
				pbrProgress.Visible = false;
				
				
				setTabs();
				
				Cursor = Cursors.Default;
			} catch (Exception ex) {
				handleError("There has been a problem setting up the Exception Reporter display",ex);				
			}

			if (!blnLicensed) displayNoLicenseMessage();
		}
		public bool displayException(Exception ex, Assembly callingAssembly, bool licensed) 
		{
			/**********************************************************
			 * displayException - entry point to the form, shows the form modally
			 * 
			 * Pass:	ex - The exception object to display
			 * 			callingAssembly - object representing calling assembly
			 * 			licensed - if true, registration popups are disabled
			 * 
			 * Returns: bool - true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			 
			blnLicensed = licensed;
			lnkTrial.Visible = !licensed;
			cAssembly = callingAssembly;
			exSelected = ex;
			refreshData = true;
			
			this.ShowDialog();
			
			return true;

		}

		private void buildExceptionHeirarchy(Exception e) 
		{
			/**********************************************************
			 * buildExceptionHeirarchy - fills the exception tree with details
			 * of the passed in exception
			 * 
			 * Pass:	e - The exception object to display
			 * 
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			try {
				
				lstExceptions.Clear();
				
				lstExceptions.Columns.Add("Level",100,HorizontalAlignment.Left);
				lstExceptions.Columns.Add("Exception Type",150,HorizontalAlignment.Left);
				lstExceptions.Columns.Add("Target Site / Method",150,HorizontalAlignment.Left);
				
				
				ListViewItem lv;
				lv = new ListViewItem();
				lv.Text= "Top Level";
				lv.SubItems.Add(e.GetType().ToString());
				lv.SubItems.Add(e.TargetSite.ToString());
				lv.Tag = "0";
				lstExceptions.Items.Add(lv);
				lv.Selected=true;
				
				Exception exCurrent;
				bool blnContinue = true;
				Int32 intIndex=0;
	
				exCurrent = e;
				blnContinue = (exCurrent.InnerException != null);
				while (blnContinue) 
				{	
					intIndex++;
					exCurrent= exCurrent.InnerException;
					lv = new ListViewItem();
					lv.Text= "Inner Exception " + intIndex.ToString();
					lv.SubItems.Add(exCurrent.GetType().ToString());
					lv.SubItems.Add(exCurrent.TargetSite.ToString());
					lv.Tag = intIndex.ToString();
					lstExceptions.Items.Add(lv);
					
					blnContinue = (exCurrent.InnerException != null);
				}
				txtStackTrace.Text = e.StackTrace;
				txtMessage.Text = e.Message;
				
			} catch (Exception ex) {
				handleError("There has been a problem building the Exception Heirarchy list",ex);				
			}

		}

		private void displayNoLicenseMessage(){
			/**********************************************************
			 * displayNoLicenseMessage - display a registration reminder
			 * 
			 * Pass:	
			 * 
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			try {
				frmRegistrationReminder frm = new frmRegistrationReminder();
				frm.ShowReminder();
			} catch (Exception ex) {
				handleError("There has been a problem displaying the registration reminder",ex);				
			}
		}


		private bool addEnvironmentNode2(String strCaption, String strClass , TreeNode parentNode, Boolean blnUseName, String strWhere) 
		{
			/**********************************************************
			 * addEnvironmentNode2 - add a set of environment display nodes
			 * to an environment tree
			 * 
			 * Pass:	strCaption - top level caption for the nodes
			 * 			strClass - WMI class to display
			 * 			parentNode - node in tree to add children under
			 * 			blnUseName - if true use name instead of caption properties for display
			 * 			strWhere - if specified limits nodes to those that match a where clause
			 * Returns: true for success
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
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
						propertyNode = new TreeNode(iPropData.Name + ':' + System.Convert.ToString(iPropData.Value));
						tn2.Nodes.Add(propertyNode);
					}
					iCount++;
					
				}
				parentNode.Nodes.Add(tn);
			}
			catch (Exception ex) 
			{
				handleError("There has been a problem adding an Environment node.",ex);
				return false;
			}
			
				
			return true;
		}

		
		private void cmdPrint_Click(object sender, System.EventArgs e)
		{
			
			/**********************************************************
			 * cmdPrint_Click - Intiate Printing
			 * 
			 * Pass:	e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			frmPrintSelect frmPS = new frmPrintSelect();
			bool blnGeneral = false;
			bool blnExceptions = false;
			bool blnAssemblies = false;
			bool blnSettings = false;
			bool blnEnvironment = false;
			bool blnContact = false;
			
			
			if (!blnLicensed) displayNoLicenseMessage();
			
			try {
				
			
				if (!frmPS.selectPrintDetails(ref blnGeneral,ref blnExceptions,ref blnAssemblies,ref blnSettings,ref blnEnvironment,ref blnContact)) {
					//user has cancelled print
					return;
				}
				//otherwise continue on			
				
				if (blnGeneral == false && blnExceptions == false && blnAssemblies == false && blnSettings == false && blnEnvironment == false && blnContact == false) {
					MessageBox.Show("No items have been selected for print. Printing has been cancelled.","Printing Cancelled");
					return;
				}
				
				buildExceptionString(blnGeneral, blnExceptions, blnAssemblies, blnSettings, blnEnvironment, blnContact,true);
			
				// add the delegate
				PrintEventHandler peHandler = new PrintEventHandler(this.printDocument1_BeginPrint);
				printDocument1.BeginPrint += peHandler;
				
			} catch (Exception ex) {
				handleError("There has been a problem preparing to Print",ex);				
			}
			
			



			DialogResult dr;
		    printDialog1.Document = printDocument1;
			dr = printDialog1.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				try {
					printDocument1.Print();
				} catch (Exception ex) {
					handleError("There has been a problem printing",ex);				
				}
			}
			else 
			{
				
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

		private void lnkEmail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			/**********************************************************
			 * lnkEmail_LinkClicked - Intiate email to contact address
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			try {
				
				String strLink = lnkEmail.Text;
				
				//strMessage = strMessage.Replace(" ","%20");
				//strMessage = strMessage.Replace("\n\r","%0D%0A");
				
				if (!strLink.Substring(0,7).ToUpper().Equals("MAILTO:")) {
					strLink = "MailTo:" + strLink;
				}
				
				Process.Start(strLink);
				
				
			} catch (Exception ex) {
					handleError("There has been a problem handling the e-mail link",ex);				
			}
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
			/**********************************************************
			 * cmdCopy_Click - Copy Exception String to clipboard
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			if (!blnLicensed) displayNoLicenseMessage();
			try {
				buildExceptionString();
				Clipboard.SetDataObject(sbExceptionString.ToString(),true);
			} catch (Exception ex) {
				handleError("There has been a problem copying to clipboard",ex);				
			}
		}

		private void cmdEmail_Click(object sender, System.EventArgs e)
		{
			/**********************************************************
			 * cmdEmail_Click - Email Exception string to recipient
			 * uses either SimpleMAPI of SMTP based on settings
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			if (!blnLicensed) displayNoLicenseMessage();
			buildExceptionString();
			try {
				if (strSendEmailAddress == null) {
					strSendEmailAddress = lnkEmail.Text;
				}
			
				if (sendMailType == ExceptionReporter.slsMailType.SimpleMAPI) {
					sendMAPIEmail();
				}
				if (sendMailType == ExceptionReporter.slsMailType.SMTP) {
					if (strSendEmailAddress != null) {
						sendSMTPEmail();
					} else {
						MessageBox.Show("It is not possible to send e-mail as a recipient address has not been configured by the application.","To Address Missing");
					}
				}
			}  catch (Exception ex) {
				handleError("There has been a problem sending e-mail",ex);				
			}
			
			
		}


		private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			/**********************************************************
			 * printDocument1_BeginPrint - reset page count prior to print
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			PageCount = 0;

			
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			/**********************************************************
			 * printDocument1_PrintPage - handle printing of one page
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			int leftMargin = e.MarginBounds.Left;
			int rightMargin = e.MarginBounds.Right;
			int topMargin = e.MarginBounds.Top;
			bool blnSkip = false;		
			int intCount = 0;
			float fltFontWidth = 0;
			Font currentFont;
			
			String strLine = null;
			
			PageCount ++;
			if (PageCount == 1) 
			{
				printFont = new Font("Courier New", 12);
				boldFont = new Font("Courier New", 12,FontStyle.Bold);
				
			}

			SizeF fSize = e.Graphics.MeasureString("WWWWWWWWWWWWWWWWWWWWWWWWWWWWWW",printFont);
			fltFontWidth = fSize.Width/30;
						
			if (PageCount == 1) 
			{
				// setup for first page
				drawWidth = e.MarginBounds.Size.Width; //- (e.MarginBounds.Left + e.MarginBounds.Right);
				drawHeight= e.MarginBounds.Size.Height;//- (e.MarginBounds.Top+ e.MarginBounds.Bottom);
		
				intCharactersLine =(int) ((float)drawWidth/fltFontWidth); //fSize.ToSize().Width;
				intLinesPage = (int)((float)drawHeight/printFont.GetHeight());
            
            	
            
				sbPrintString = new StringBuilder();
				StringWriter swPrint = new StringWriter(sbPrintString);
				StringReader srException = new StringReader(sbExceptionString.ToString());
				wrapText(srException,swPrint,intCharactersLine);
				sPrintReader = new StringReader(sbPrintString.ToString());

			}
			// draw the border
			Rectangle rect = new Rectangle(leftMargin,topMargin,drawWidth,drawHeight);
			e.Graphics.DrawRectangle(Pens.Black,rect);
			
			//draw the header
			strLine = "Error Report: " + txtApplication.Text;
			e.Graphics.DrawString(strLine, boldFont, Brushes.Black, leftMargin, topMargin + ((intCount) * printFont.GetHeight()));
			intCount++;
			strLine = "Date/Time:    " + txtDate.Text + " " + txtTime.Text;
			e.Graphics.DrawString(strLine, boldFont, Brushes.Black, leftMargin, topMargin + ((intCount) * printFont.GetHeight()));
			intCount++;
			e.Graphics.DrawLine(Pens.Black,leftMargin, topMargin + ((intCount) * printFont.GetHeight()),rightMargin,topMargin + ((intCount) * printFont.GetHeight()));
			intCount++; // leave a space from header
			
			
			// draw the footer
			strLine = "Page: " + PageCount.ToString();
			e.Graphics.DrawLine(Pens.Black,leftMargin, topMargin + ((intLinesPage-2) * printFont.GetHeight()),rightMargin,topMargin + ((intLinesPage-2) * printFont.GetHeight()));
			e.Graphics.DrawString(strLine, boldFont, Brushes.Black, leftMargin, topMargin + ((intLinesPage-1) * printFont.GetHeight()));
			
			
			//loop for the number of lines a page
			while (intCount <= (intLinesPage - 3)) // - 1 because of footer
			{
				currentFont = printFont;
				blnSkip = false;
				// read the line
				strLine = sPrintReader.ReadLine();
				if (strLine == null) 
				{
					intCount = intLinesPage + 1; //exit the loop
				} 
				else 
				{
					if (strLine.Length >= 5) {
						if (strLine.Substring(1,4).Equals("----")){
							//draw a seperator line
							e.Graphics.DrawLine(Pens.Black,leftMargin, topMargin + ((intCount) * printFont.GetHeight()),rightMargin,topMargin + ((intCount) * printFont.GetHeight()));
							blnSkip = true;
						}	
					}
					if (!blnSkip) {
						// check if the line should be bold
						if (strLine.Equals("General") || strLine.Equals("Exceptions") || strLine.Equals("Explanation") || strLine.Equals("Assemblies") || strLine.Equals("Application Settings") || strLine.Equals("Environment") || strLine.Equals("Contact")) {
							currentFont = boldFont;
						}
						
						// output the text line
						e.Graphics.DrawString(strLine, currentFont, Brushes.Black, leftMargin, topMargin + ((intCount) * printFont.GetHeight()));
					}
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
			// let error bubble back
			                       
		}


		~frmER() 
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
			/**********************************************************
			 * cmdSave_Click - Save exception string to a file
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			try {
				
				if (!blnLicensed) displayNoLicenseMessage();
				buildExceptionString();
			
				Stream strStream ; 
	            SaveFileDialog dlgSave = new SaveFileDialog(); 
	
	            dlgSave.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*" ; 
	            dlgSave.FilterIndex = 1; 
	            dlgSave.RestoreDirectory = true; 
	
	            if(dlgSave.ShowDialog() == DialogResult.OK) 
	            { 
	                  if((strStream = dlgSave.OpenFile()) != null) 
	                  { 
	                        StreamWriter strWriter =new StreamWriter(strStream); 
	                         
	                        strWriter.Write(sbExceptionString.ToString()); 
	
	                        strStream.Close(); 
	                  } 
	            } 
			}  catch (Exception ex) {
				handleError("There has been a problem saving error details to file",ex);				
			}
		}

		
		
		void LstExceptionsSelectedIndexChanged(object sender, System.EventArgs e)
		{
			/**********************************************************
			 * LstExceptionsSelectedIndexChanged - handle selection of a 
			 * exception in the Exceptions list
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			Exception displayException = exSelected;
			try {
				
				foreach(ListViewItem lvi in lstExceptions.Items) {
					if (lvi.Selected) {
						// work out which exception to display
						for (Int32 intCount = 0; intCount < Int32.Parse(lvi.Tag.ToString());intCount++) 
						{
							displayException = displayException.InnerException;
						}
					}
				}
				
				txtStackTrace.Text = "";
				txtMessage.Text = "";
				if (displayException == null) displayException = exSelected;
				if (!(displayException==null)) {
					txtStackTrace.Text = displayException.StackTrace;
					txtMessage.Text = displayException.Message;
				}
			}  catch (Exception ex) {
				handleError("There has been a problem handling the change of selected exception",ex);				
			}
		}
		
		void lblApplication_Click(object sender, System.EventArgs e)
		{
			
		}
		
		void LnkWebLinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			/**********************************************************
			 * LnkWebLinkClicked - handle click of the web link (open the link)
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			try {
				String strLink = lnkWeb.Text;
				Process.Start(strLink);
			}  catch (Exception ex) {
				handleError("There has been a problem handling the web link click",ex);				
			}

		}
		
		void LnkTrialLinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			/**********************************************************
			 * LnkTrialLinkClicked - handle click of the trial link (open the link)
			 * 
			 * Pass:	sender - sender object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			String strLink = lnkWeb.Text;
			try {
				Process.Start("http://www.stratalogic.com");
			}  catch (Exception ex) {
				handleError("There has been a problem handling the stratalogic link click",ex);				
			}
		}
		private void handleError(string strMessage,Exception ex) {
			/**********************************************************
			 * handleError - handle error by showing a simple error handler
			 * form
			 * 
			 * Pass:	strMessage - message to display
			 * 			ex - The exception to display
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			frmSimpleException frm = new frmSimpleException();
			frm.ShowException(strMessage,ex);
		}


		

	}
}
