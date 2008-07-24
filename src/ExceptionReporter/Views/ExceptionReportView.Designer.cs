namespace ExceptionReporting.Views
{
	partial class ExceptionReportView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionReportView));
			this.lstAssemblies = new System.Windows.Forms.ListView();
			this.tcTabs = new System.Windows.Forms.TabControl();
			this.tpGeneral = new System.Windows.Forms.TabPage();
			this.lblExplanation = new System.Windows.Forms.Label();
			this.txtExplanation = new System.Windows.Forms.TextBox();
			this.lblRegion = new System.Windows.Forms.Label();
			this.txtRegion = new System.Windows.Forms.TextBox();
			this.picGeneral = new System.Windows.Forms.PictureBox();
			this.gbLineGeneral1 = new System.Windows.Forms.GroupBox();
			this.lblDate = new System.Windows.Forms.Label();
			this.txtDate = new System.Windows.Forms.TextBox();
			this.lblGeneral = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.txtTime = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblApplication = new System.Windows.Forms.Label();
			this.txtApplication = new System.Windows.Forms.TextBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.txtVersion = new System.Windows.Forms.TextBox();
			this.tpExceptions = new System.Windows.Forms.TabPage();
			this.lstExceptions = new System.Windows.Forms.ListView();
			this.grbMessage = new System.Windows.Forms.GroupBox();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.grbStackTrace = new System.Windows.Forms.GroupBox();
			this.txtStackTrace = new System.Windows.Forms.TextBox();
			this.tpAssemblies = new System.Windows.Forms.TabPage();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.tvwSettings = new System.Windows.Forms.TreeView();
			this.tpEnvironment = new System.Windows.Forms.TabPage();
			this.lblMachine = new System.Windows.Forms.Label();
			this.txtMachine = new System.Windows.Forms.TextBox();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.tvwEnvironment = new System.Windows.Forms.TreeView();
			this.tpContact = new System.Windows.Forms.TabPage();
			this.lblContactMessageTop = new System.Windows.Forms.Label();
			this.txtFax = new System.Windows.Forms.TextBox();
			this.lblFax = new System.Windows.Forms.Label();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.lblPhone = new System.Windows.Forms.Label();
			this.lblWebSite = new System.Windows.Forms.Label();
			this.lnkWeb = new System.Windows.Forms.LinkLabel();
			this.lblEmail = new System.Windows.Forms.Label();
			this.lnkEmail = new System.Windows.Forms.LinkLabel();
			this.lblContactMessageBottom = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.btnEmail = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.lblProgress = new System.Windows.Forms.Label();
			this.btnCopy = new System.Windows.Forms.Button();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.tcTabs.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picGeneral)).BeginInit();
			this.tpExceptions.SuspendLayout();
			this.grbMessage.SuspendLayout();
			this.grbStackTrace.SuspendLayout();
			this.tpAssemblies.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.tpEnvironment.SuspendLayout();
			this.tpContact.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstAssemblies
			// 
			this.lstAssemblies.FullRowSelect = true;
			this.lstAssemblies.Location = new System.Drawing.Point(8, 8);
			this.lstAssemblies.Name = "lstAssemblies";
			this.lstAssemblies.Size = new System.Drawing.Size(472, 320);
			this.lstAssemblies.TabIndex = 21;
			this.lstAssemblies.UseCompatibleStateImageBehavior = false;
			this.lstAssemblies.View = System.Windows.Forms.View.Details;
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
			this.tcTabs.Location = new System.Drawing.Point(12, 12);
			this.tcTabs.Name = "tcTabs";
			this.tcTabs.SelectedIndex = 0;
			this.tcTabs.ShowToolTips = true;
			this.tcTabs.Size = new System.Drawing.Size(496, 360);
			this.tcTabs.TabIndex = 51;
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
			// 
			// lblExplanation
			// 
			this.lblExplanation.Location = new System.Drawing.Point(12, 238);
			this.lblExplanation.Name = "lblExplanation";
			this.lblExplanation.Size = new System.Drawing.Size(464, 32);
			this.lblExplanation.TabIndex = 14;
			this.lblExplanation.Text = "Please enter a brief explanation detailing the actions and events leading up to t" +
				"he occurrence of this error.";
			// 
			// txtExplanation
			// 
			this.txtExplanation.Location = new System.Drawing.Point(8, 273);
			this.txtExplanation.Multiline = true;
			this.txtExplanation.Name = "txtExplanation";
			this.txtExplanation.Size = new System.Drawing.Size(472, 55);
			this.txtExplanation.TabIndex = 15;
			// 
			// lblRegion
			// 
			this.lblRegion.Location = new System.Drawing.Point(255, 158);
			this.lblRegion.Name = "lblRegion";
			this.lblRegion.Size = new System.Drawing.Size(48, 16);
			this.lblRegion.TabIndex = 7;
			this.lblRegion.Text = "&Region:";
			// 
			// txtRegion
			// 
			this.txtRegion.BackColor = System.Drawing.SystemColors.Control;
			this.txtRegion.Location = new System.Drawing.Point(311, 158);
			this.txtRegion.Name = "txtRegion";
			this.txtRegion.ReadOnly = true;
			this.txtRegion.Size = new System.Drawing.Size(168, 20);
			this.txtRegion.TabIndex = 8;
			// 
			// picGeneral
			// 
			this.picGeneral.Image = ((System.Drawing.Image)(resources.GetObject("picGeneral.Image")));
			this.picGeneral.Location = new System.Drawing.Point(3, 3);
			this.picGeneral.Name = "picGeneral";
			this.picGeneral.Size = new System.Drawing.Size(87, 83);
			this.picGeneral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picGeneral.TabIndex = 22;
			this.picGeneral.TabStop = false;
			// 
			// gbLineGeneral1
			// 
			this.gbLineGeneral1.Location = new System.Drawing.Point(8, 103);
			this.gbLineGeneral1.Name = "gbLineGeneral1";
			this.gbLineGeneral1.Size = new System.Drawing.Size(472, 8);
			this.gbLineGeneral1.TabIndex = 2;
			this.gbLineGeneral1.TabStop = false;
			// 
			// lblDate
			// 
			this.lblDate.Location = new System.Drawing.Point(15, 190);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(40, 16);
			this.lblDate.TabIndex = 9;
			this.lblDate.Text = "&Date:";
			// 
			// txtDate
			// 
			this.txtDate.BackColor = System.Drawing.SystemColors.Control;
			this.txtDate.Location = new System.Drawing.Point(79, 190);
			this.txtDate.Name = "txtDate";
			this.txtDate.ReadOnly = true;
			this.txtDate.Size = new System.Drawing.Size(152, 20);
			this.txtDate.TabIndex = 10;
			// 
			// lblGeneral
			// 
			this.lblGeneral.Location = new System.Drawing.Point(96, 3);
			this.lblGeneral.Name = "lblGeneral";
			this.lblGeneral.Size = new System.Drawing.Size(380, 83);
			this.lblGeneral.TabIndex = 1;
			this.lblGeneral.Text = "An error has occured in this application.";
			// 
			// lblTime
			// 
			this.lblTime.Location = new System.Drawing.Point(255, 190);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(40, 16);
			this.lblTime.TabIndex = 11;
			this.lblTime.Text = "&Time:";
			// 
			// txtTime
			// 
			this.txtTime.BackColor = System.Drawing.SystemColors.Control;
			this.txtTime.Location = new System.Drawing.Point(311, 190);
			this.txtTime.Name = "txtTime";
			this.txtTime.ReadOnly = true;
			this.txtTime.Size = new System.Drawing.Size(168, 20);
			this.txtTime.TabIndex = 12;
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(7, 214);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(472, 8);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			// 
			// lblApplication
			// 
			this.lblApplication.Location = new System.Drawing.Point(15, 128);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(64, 16);
			this.lblApplication.TabIndex = 3;
			this.lblApplication.Text = "&Application:";
			// 
			// txtApplication
			// 
			this.txtApplication.BackColor = System.Drawing.SystemColors.Control;
			this.txtApplication.Location = new System.Drawing.Point(79, 126);
			this.txtApplication.Name = "txtApplication";
			this.txtApplication.ReadOnly = true;
			this.txtApplication.Size = new System.Drawing.Size(400, 20);
			this.txtApplication.TabIndex = 4;
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(15, 158);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(48, 16);
			this.lblVersion.TabIndex = 5;
			this.lblVersion.Text = "&Version:";
			// 
			// txtVersion
			// 
			this.txtVersion.BackColor = System.Drawing.SystemColors.Control;
			this.txtVersion.Location = new System.Drawing.Point(79, 158);
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.ReadOnly = true;
			this.txtVersion.Size = new System.Drawing.Size(152, 20);
			this.txtVersion.TabIndex = 6;
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
			// 
			// lstExceptions
			// 
			this.lstExceptions.FullRowSelect = true;
			this.lstExceptions.Location = new System.Drawing.Point(8, 8);
			this.lstExceptions.Name = "lstExceptions";
			this.lstExceptions.Size = new System.Drawing.Size(472, 120);
			this.lstExceptions.TabIndex = 22;
			this.lstExceptions.UseCompatibleStateImageBehavior = false;
			this.lstExceptions.View = System.Windows.Forms.View.Details;
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
			// tpSettings
			// 
			this.tpSettings.Controls.Add(this.tvwSettings);
			this.tpSettings.Location = new System.Drawing.Point(4, 22);
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.Size = new System.Drawing.Size(488, 334);
			this.tpSettings.TabIndex = 5;
			this.tpSettings.Text = "Settings";
			this.tpSettings.ToolTipText = "Displays Application Settings";
			// 
			// tvwSettings
			// 
			this.tvwSettings.Location = new System.Drawing.Point(8, 8);
			this.tvwSettings.Name = "tvwSettings";
			this.tvwSettings.Size = new System.Drawing.Size(472, 320);
			this.tvwSettings.TabIndex = 20;
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
			// 
			// lblMachine
			// 
			this.lblMachine.Location = new System.Drawing.Point(16, 15);
			this.lblMachine.Name = "lblMachine";
			this.lblMachine.Size = new System.Drawing.Size(56, 16);
			this.lblMachine.TabIndex = 16;
			this.lblMachine.Text = "&Machine:";
			// 
			// txtMachine
			// 
			this.txtMachine.BackColor = System.Drawing.SystemColors.Control;
			this.txtMachine.Location = new System.Drawing.Point(80, 12);
			this.txtMachine.Name = "txtMachine";
			this.txtMachine.ReadOnly = true;
			this.txtMachine.Size = new System.Drawing.Size(144, 20);
			this.txtMachine.TabIndex = 17;
			// 
			// lblUsername
			// 
			this.lblUsername.Location = new System.Drawing.Point(256, 15);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(64, 16);
			this.lblUsername.TabIndex = 18;
			this.lblUsername.Text = "&Username:";
			// 
			// txtUserName
			// 
			this.txtUserName.BackColor = System.Drawing.SystemColors.Control;
			this.txtUserName.Location = new System.Drawing.Point(328, 12);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.ReadOnly = true;
			this.txtUserName.Size = new System.Drawing.Size(152, 20);
			this.txtUserName.TabIndex = 19;
			// 
			// tvwEnvironment
			// 
			this.tvwEnvironment.Location = new System.Drawing.Point(8, 40);
			this.tvwEnvironment.Name = "tvwEnvironment";
			this.tvwEnvironment.Size = new System.Drawing.Size(472, 288);
			this.tvwEnvironment.TabIndex = 24;
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
			this.tpContact.Size = new System.Drawing.Size(488, 334);
			this.tpContact.TabIndex = 4;
			this.tpContact.Text = "Contact";
			this.tpContact.ToolTipText = "Contact details for this application";
			// 
			// lblContactMessageTop
			// 
			this.lblContactMessageTop.Location = new System.Drawing.Point(8, 24);
			this.lblContactMessageTop.Name = "lblContactMessageTop";
			this.lblContactMessageTop.Size = new System.Drawing.Size(432, 24);
			this.lblContactMessageTop.TabIndex = 27;
			this.lblContactMessageTop.Text = "The following details can be used to obtain support for this application....";
			// 
			// txtFax
			// 
			this.txtFax.BackColor = System.Drawing.SystemColors.Control;
			this.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtFax.Location = new System.Drawing.Point(72, 168);
			this.txtFax.Name = "txtFax";
			this.txtFax.Size = new System.Drawing.Size(240, 13);
			this.txtFax.TabIndex = 35;
			// 
			// lblFax
			// 
			this.lblFax.Location = new System.Drawing.Point(16, 168);
			this.lblFax.Name = "lblFax";
			this.lblFax.Size = new System.Drawing.Size(48, 24);
			this.lblFax.TabIndex = 34;
			this.lblFax.Text = "Fax:";
			// 
			// txtPhone
			// 
			this.txtPhone.BackColor = System.Drawing.SystemColors.Control;
			this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtPhone.Location = new System.Drawing.Point(72, 144);
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(240, 13);
			this.txtPhone.TabIndex = 33;
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
			// lnkWeb
			// 
			this.lnkWeb.Location = new System.Drawing.Point(72, 80);
			this.lnkWeb.Name = "lnkWeb";
			this.lnkWeb.Size = new System.Drawing.Size(400, 56);
			this.lnkWeb.TabIndex = 31;
			// 
			// lblEmail
			// 
			this.lblEmail.Location = new System.Drawing.Point(16, 56);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(40, 24);
			this.lblEmail.TabIndex = 28;
			this.lblEmail.Text = "E-Mail:";
			// 
			// lnkEmail
			// 
			this.lnkEmail.Location = new System.Drawing.Point(72, 56);
			this.lnkEmail.Name = "lnkEmail";
			this.lnkEmail.Size = new System.Drawing.Size(400, 16);
			this.lnkEmail.TabIndex = 29;
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
			// btnSave
			// 
			this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSave.Location = new System.Drawing.Point(356, 378);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(72, 32);
			this.btnSave.TabIndex = 56;
			this.btnSave.Text = "Save";
			this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(28, 392);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(128, 16);
			this.progressBar.TabIndex = 53;
			// 
			// btnEmail
			// 
			this.btnEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnEmail.Location = new System.Drawing.Point(436, 378);
			this.btnEmail.Name = "btnEmail";
			this.btnEmail.Size = new System.Drawing.Size(72, 32);
			this.btnEmail.TabIndex = 57;
			this.btnEmail.Text = "Email";
			this.btnEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnPrint
			// 
			this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPrint.Location = new System.Drawing.Point(196, 378);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(72, 32);
			this.btnPrint.TabIndex = 54;
			this.btnPrint.Text = "Print";
			this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblProgress
			// 
			this.lblProgress.Location = new System.Drawing.Point(28, 376);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(128, 16);
			this.lblProgress.TabIndex = 52;
			this.lblProgress.Text = "Loading System Info...";
			// 
			// btnCopy
			// 
			this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCopy.Location = new System.Drawing.Point(276, 378);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(72, 32);
			this.btnCopy.TabIndex = 55;
			this.btnCopy.Text = "Copy";
			this.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ExceptionReportView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 419);
			this.Controls.Add(this.tcTabs);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.btnEmail);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.btnCopy);
			this.Name = "ExceptionReportView";
			this.Text = "ExceptionReportView";
			this.tcTabs.ResumeLayout(false);
			this.tpGeneral.ResumeLayout(false);
			this.tpGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picGeneral)).EndInit();
			this.tpExceptions.ResumeLayout(false);
			this.grbMessage.ResumeLayout(false);
			this.grbMessage.PerformLayout();
			this.grbStackTrace.ResumeLayout(false);
			this.grbStackTrace.PerformLayout();
			this.tpAssemblies.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.tpEnvironment.ResumeLayout(false);
			this.tpEnvironment.PerformLayout();
			this.tpContact.ResumeLayout(false);
			this.tpContact.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lstAssemblies;
		private System.Windows.Forms.TabControl tcTabs;
		private System.Windows.Forms.TabPage tpGeneral;
		private System.Windows.Forms.Label lblExplanation;
		private System.Windows.Forms.TextBox txtExplanation;
		private System.Windows.Forms.Label lblRegion;
		private System.Windows.Forms.TextBox txtRegion;
		private System.Windows.Forms.PictureBox picGeneral;
		private System.Windows.Forms.GroupBox gbLineGeneral1;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.TextBox txtDate;
		private System.Windows.Forms.Label lblGeneral;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.TextBox txtTime;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.TextBox txtApplication;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.TextBox txtVersion;
		private System.Windows.Forms.TabPage tpExceptions;
		private System.Windows.Forms.ListView lstExceptions;
		private System.Windows.Forms.GroupBox grbMessage;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.GroupBox grbStackTrace;
		private System.Windows.Forms.TextBox txtStackTrace;
		private System.Windows.Forms.TabPage tpAssemblies;
		private System.Windows.Forms.TabPage tpSettings;
		private System.Windows.Forms.TreeView tvwSettings;
		private System.Windows.Forms.TabPage tpEnvironment;
		private System.Windows.Forms.Label lblMachine;
		private System.Windows.Forms.TextBox txtMachine;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TreeView tvwEnvironment;
		private System.Windows.Forms.TabPage tpContact;
		private System.Windows.Forms.Label lblContactMessageTop;
		private System.Windows.Forms.TextBox txtFax;
		private System.Windows.Forms.Label lblFax;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.Label lblPhone;
		private System.Windows.Forms.Label lblWebSite;
		private System.Windows.Forms.LinkLabel lnkWeb;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.LinkLabel lnkEmail;
		private System.Windows.Forms.Label lblContactMessageBottom;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.Button btnEmail;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Button btnCopy;
		private System.Drawing.Printing.PrintDocument printDocument1;
	}
}