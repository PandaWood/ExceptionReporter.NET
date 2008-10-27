namespace ExceptionReporting.Views
{
	internal partial class ExceptionReportView
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
			this.listviewAssemblies = new System.Windows.Forms.ListView();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.picGeneral = new System.Windows.Forms.PictureBox();
			this.txtExceptionMessage = new System.Windows.Forms.TextBox();
			this.lblExplanation = new System.Windows.Forms.Label();
			this.txtUserExplanation = new System.Windows.Forms.TextBox();
			this.lblRegion = new System.Windows.Forms.Label();
			this.txtRegion = new System.Windows.Forms.TextBox();
			this.lblDate = new System.Windows.Forms.Label();
			this.txtDate = new System.Windows.Forms.TextBox();
			this.lblTime = new System.Windows.Forms.Label();
			this.txtTime = new System.Windows.Forms.TextBox();
			this.lblApplication = new System.Windows.Forms.Label();
			this.txtApplicationName = new System.Windows.Forms.TextBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.txtVersion = new System.Windows.Forms.TextBox();
			this.tabExceptions = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.txtExceptionTabStackTrace = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtExceptionTabMessage = new System.Windows.Forms.TextBox();
			this.listviewExceptions = new System.Windows.Forms.ListView();
			this.tabAssemblies = new System.Windows.Forms.TabPage();
			this.tabConfig = new System.Windows.Forms.TabPage();
			this.treeviewSettings = new System.Windows.Forms.TreeView();
			this.tabSysInfo = new System.Windows.Forms.TabPage();
			this.lblMachine = new System.Windows.Forms.Label();
			this.txtMachine = new System.Windows.Forms.TextBox();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.treeEnvironment = new System.Windows.Forms.TreeView();
			this.tabContact = new System.Windows.Forms.TabPage();
			this.lblContactMessageTop = new System.Windows.Forms.Label();
			this.txtFax = new System.Windows.Forms.TextBox();
			this.lblFax = new System.Windows.Forms.Label();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.lblPhone = new System.Windows.Forms.Label();
			this.lblWebSite = new System.Windows.Forms.Label();
			this.urlWeb = new System.Windows.Forms.LinkLabel();
			this.lblEmail = new System.Windows.Forms.Label();
			this.urlEmail = new System.Windows.Forms.LinkLabel();
			this.lblContactMessageBottom = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.btnEmail = new System.Windows.Forms.Button();
			this.lblProgressMessage = new System.Windows.Forms.Label();
			this.btnCopy = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picGeneral)).BeginInit();
			this.tabExceptions.SuspendLayout();
			this.tabAssemblies.SuspendLayout();
			this.tabConfig.SuspendLayout();
			this.tabSysInfo.SuspendLayout();
			this.tabContact.SuspendLayout();
			this.SuspendLayout();
			// 
			// listviewAssemblies
			// 
			this.listviewAssemblies.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listviewAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listviewAssemblies.FullRowSelect = true;
			this.listviewAssemblies.HotTracking = true;
			this.listviewAssemblies.HoverSelection = true;
			this.listviewAssemblies.Location = new System.Drawing.Point(0, 0);
			this.listviewAssemblies.Name = "listviewAssemblies";
			this.listviewAssemblies.Size = new System.Drawing.Size(497, 348);
			this.listviewAssemblies.TabIndex = 21;
			this.listviewAssemblies.UseCompatibleStateImageBehavior = false;
			this.listviewAssemblies.View = System.Windows.Forms.View.Details;
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabGeneral);
			this.tabControl.Controls.Add(this.tabExceptions);
			this.tabControl.Controls.Add(this.tabAssemblies);
			this.tabControl.Controls.Add(this.tabConfig);
			this.tabControl.Controls.Add(this.tabSysInfo);
			this.tabControl.Controls.Add(this.tabContact);
			this.tabControl.HotTrack = true;
			this.tabControl.Location = new System.Drawing.Point(6, 6);
			this.tabControl.MinimumSize = new System.Drawing.Size(200, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.ShowToolTips = true;
			this.tabControl.Size = new System.Drawing.Size(505, 374);
			this.tabControl.TabIndex = 51;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.picGeneral);
			this.tabGeneral.Controls.Add(this.txtExceptionMessage);
			this.tabGeneral.Controls.Add(this.lblExplanation);
			this.tabGeneral.Controls.Add(this.txtUserExplanation);
			this.tabGeneral.Controls.Add(this.lblRegion);
			this.tabGeneral.Controls.Add(this.txtRegion);
			this.tabGeneral.Controls.Add(this.lblDate);
			this.tabGeneral.Controls.Add(this.txtDate);
			this.tabGeneral.Controls.Add(this.lblTime);
			this.tabGeneral.Controls.Add(this.txtTime);
			this.tabGeneral.Controls.Add(this.lblApplication);
			this.tabGeneral.Controls.Add(this.txtApplicationName);
			this.tabGeneral.Controls.Add(this.lblVersion);
			this.tabGeneral.Controls.Add(this.txtVersion);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(497, 348);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// picGeneral
			// 
			this.picGeneral.Image = ((System.Drawing.Image)(resources.GetObject("picGeneral.Image")));
			this.picGeneral.Location = new System.Drawing.Point(8, 7);
			this.picGeneral.Name = "picGeneral";
			this.picGeneral.Size = new System.Drawing.Size(64, 64);
			this.picGeneral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picGeneral.TabIndex = 25;
			this.picGeneral.TabStop = false;
			// 
			// txtExceptionMessage
			// 
			this.txtExceptionMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtExceptionMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtExceptionMessage.Location = new System.Drawing.Point(78, 7);
			this.txtExceptionMessage.Multiline = true;
			this.txtExceptionMessage.Name = "txtExceptionMessage";
			this.txtExceptionMessage.ReadOnly = true;
			this.txtExceptionMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtExceptionMessage.Size = new System.Drawing.Size(407, 68);
			this.txtExceptionMessage.TabIndex = 24;
			this.txtExceptionMessage.Text = "No message";
			// 
			// lblExplanation
			// 
			this.lblExplanation.AutoSize = true;
			this.lblExplanation.Location = new System.Drawing.Point(5, 194);
			this.lblExplanation.Name = "lblExplanation";
			this.lblExplanation.Size = new System.Drawing.Size(334, 13);
			this.lblExplanation.TabIndex = 14;
			this.lblExplanation.Text = "Please enter a brief explanation of events leading up to this exception";
			// 
			// txtUserExplanation
			// 
			this.txtUserExplanation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserExplanation.BackColor = System.Drawing.Color.LemonChiffon;
			this.txtUserExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUserExplanation.Location = new System.Drawing.Point(8, 210);
			this.txtUserExplanation.Multiline = true;
			this.txtUserExplanation.Name = "txtUserExplanation";
			this.txtUserExplanation.Size = new System.Drawing.Size(481, 123);
			this.txtUserExplanation.TabIndex = 15;
			// 
			// lblRegion
			// 
			this.lblRegion.AutoSize = true;
			this.lblRegion.Location = new System.Drawing.Point(254, 127);
			this.lblRegion.Name = "lblRegion";
			this.lblRegion.Size = new System.Drawing.Size(41, 13);
			this.lblRegion.TabIndex = 7;
			this.lblRegion.Text = "Region";
			// 
			// txtRegion
			// 
			this.txtRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRegion.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.txtRegion.Location = new System.Drawing.Point(310, 124);
			this.txtRegion.Name = "txtRegion";
			this.txtRegion.ReadOnly = true;
			this.txtRegion.Size = new System.Drawing.Size(177, 20);
			this.txtRegion.TabIndex = 8;
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Location = new System.Drawing.Point(14, 159);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(30, 13);
			this.lblDate.TabIndex = 9;
			this.lblDate.Text = "Date";
			// 
			// txtDate
			// 
			this.txtDate.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.txtDate.Location = new System.Drawing.Point(78, 156);
			this.txtDate.Name = "txtDate";
			this.txtDate.ReadOnly = true;
			this.txtDate.Size = new System.Drawing.Size(152, 20);
			this.txtDate.TabIndex = 10;
			// 
			// lblTime
			// 
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(254, 159);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(30, 13);
			this.lblTime.TabIndex = 11;
			this.lblTime.Text = "Time";
			// 
			// txtTime
			// 
			this.txtTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtTime.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.txtTime.Location = new System.Drawing.Point(310, 156);
			this.txtTime.Name = "txtTime";
			this.txtTime.ReadOnly = true;
			this.txtTime.Size = new System.Drawing.Size(177, 20);
			this.txtTime.TabIndex = 12;
			// 
			// lblApplication
			// 
			this.lblApplication.AutoSize = true;
			this.lblApplication.Location = new System.Drawing.Point(14, 94);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(59, 13);
			this.lblApplication.TabIndex = 3;
			this.lblApplication.Text = "Application";
			// 
			// txtApplicationName
			// 
			this.txtApplicationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtApplicationName.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.txtApplicationName.Location = new System.Drawing.Point(78, 92);
			this.txtApplicationName.Name = "txtApplicationName";
			this.txtApplicationName.ReadOnly = true;
			this.txtApplicationName.Size = new System.Drawing.Size(409, 20);
			this.txtApplicationName.TabIndex = 4;
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(14, 127);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(48, 16);
			this.lblVersion.TabIndex = 5;
			this.lblVersion.Text = "Version";
			// 
			// txtVersion
			// 
			this.txtVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.txtVersion.Location = new System.Drawing.Point(78, 124);
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.ReadOnly = true;
			this.txtVersion.Size = new System.Drawing.Size(152, 20);
			this.txtVersion.TabIndex = 6;
			// 
			// tabExceptions
			// 
			this.tabExceptions.Controls.Add(this.label2);
			this.tabExceptions.Controls.Add(this.txtExceptionTabStackTrace);
			this.tabExceptions.Controls.Add(this.label1);
			this.tabExceptions.Controls.Add(this.txtExceptionTabMessage);
			this.tabExceptions.Controls.Add(this.listviewExceptions);
			this.tabExceptions.Location = new System.Drawing.Point(4, 22);
			this.tabExceptions.Name = "tabExceptions";
			this.tabExceptions.Size = new System.Drawing.Size(497, 348);
			this.tabExceptions.TabIndex = 1;
			this.tabExceptions.Text = "Exceptions";
			this.tabExceptions.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 219);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 13);
			this.label2.TabIndex = 28;
			this.label2.Text = "Stack Trace";
			// 
			// txtExceptionTabStackTrace
			// 
			this.txtExceptionTabStackTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtExceptionTabStackTrace.BackColor = System.Drawing.SystemColors.Window;
			this.txtExceptionTabStackTrace.Location = new System.Drawing.Point(8, 235);
			this.txtExceptionTabStackTrace.Multiline = true;
			this.txtExceptionTabStackTrace.Name = "txtExceptionTabStackTrace";
			this.txtExceptionTabStackTrace.ReadOnly = true;
			this.txtExceptionTabStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtExceptionTabStackTrace.Size = new System.Drawing.Size(481, 99);
			this.txtExceptionTabStackTrace.TabIndex = 26;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 131);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 13);
			this.label1.TabIndex = 27;
			this.label1.Text = "Message";
			// 
			// txtExceptionTabMessage
			// 
			this.txtExceptionTabMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtExceptionTabMessage.BackColor = System.Drawing.SystemColors.Window;
			this.txtExceptionTabMessage.Location = new System.Drawing.Point(8, 147);
			this.txtExceptionTabMessage.Multiline = true;
			this.txtExceptionTabMessage.Name = "txtExceptionTabMessage";
			this.txtExceptionTabMessage.ReadOnly = true;
			this.txtExceptionTabMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtExceptionTabMessage.Size = new System.Drawing.Size(481, 69);
			this.txtExceptionTabMessage.TabIndex = 24;
			// 
			// listviewExceptions
			// 
			this.listviewExceptions.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listviewExceptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listviewExceptions.FullRowSelect = true;
			this.listviewExceptions.HotTracking = true;
			this.listviewExceptions.HoverSelection = true;
			this.listviewExceptions.Location = new System.Drawing.Point(8, 8);
			this.listviewExceptions.Name = "listviewExceptions";
			this.listviewExceptions.Size = new System.Drawing.Size(481, 120);
			this.listviewExceptions.TabIndex = 22;
			this.listviewExceptions.UseCompatibleStateImageBehavior = false;
			this.listviewExceptions.View = System.Windows.Forms.View.Details;
			// 
			// tabAssemblies
			// 
			this.tabAssemblies.Controls.Add(this.listviewAssemblies);
			this.tabAssemblies.Location = new System.Drawing.Point(4, 22);
			this.tabAssemblies.Name = "tabAssemblies";
			this.tabAssemblies.Size = new System.Drawing.Size(497, 348);
			this.tabAssemblies.TabIndex = 6;
			this.tabAssemblies.Text = "Assemblies";
			this.tabAssemblies.UseVisualStyleBackColor = true;
			// 
			// tabConfig
			// 
			this.tabConfig.Controls.Add(this.treeviewSettings);
			this.tabConfig.Location = new System.Drawing.Point(4, 22);
			this.tabConfig.Name = "tabConfig";
			this.tabConfig.Size = new System.Drawing.Size(497, 348);
			this.tabConfig.TabIndex = 5;
			this.tabConfig.Text = "Configuration";
			this.tabConfig.UseVisualStyleBackColor = true;
			// 
			// treeviewSettings
			// 
			this.treeviewSettings.BackColor = System.Drawing.SystemColors.Window;
			this.treeviewSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeviewSettings.HotTracking = true;
			this.treeviewSettings.Location = new System.Drawing.Point(0, 0);
			this.treeviewSettings.Name = "treeviewSettings";
			this.treeviewSettings.Size = new System.Drawing.Size(497, 348);
			this.treeviewSettings.TabIndex = 20;
			// 
			// tabSysInfo
			// 
			this.tabSysInfo.Controls.Add(this.lblMachine);
			this.tabSysInfo.Controls.Add(this.txtMachine);
			this.tabSysInfo.Controls.Add(this.lblUsername);
			this.tabSysInfo.Controls.Add(this.txtUserName);
			this.tabSysInfo.Controls.Add(this.treeEnvironment);
			this.tabSysInfo.Location = new System.Drawing.Point(4, 22);
			this.tabSysInfo.Name = "tabSysInfo";
			this.tabSysInfo.Size = new System.Drawing.Size(497, 348);
			this.tabSysInfo.TabIndex = 3;
			this.tabSysInfo.Text = "System";
			this.tabSysInfo.UseVisualStyleBackColor = true;
			// 
			// lblMachine
			// 
			this.lblMachine.AutoSize = true;
			this.lblMachine.Location = new System.Drawing.Point(5, 15);
			this.lblMachine.Name = "lblMachine";
			this.lblMachine.Size = new System.Drawing.Size(48, 13);
			this.lblMachine.TabIndex = 16;
			this.lblMachine.Text = "Machine";
			// 
			// txtMachine
			// 
			this.txtMachine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMachine.BackColor = System.Drawing.SystemColors.Control;
			this.txtMachine.Location = new System.Drawing.Point(59, 12);
			this.txtMachine.Name = "txtMachine";
			this.txtMachine.ReadOnly = true;
			this.txtMachine.Size = new System.Drawing.Size(178, 20);
			this.txtMachine.TabIndex = 17;
			// 
			// lblUsername
			// 
			this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point(259, 15);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(55, 13);
			this.lblUsername.TabIndex = 18;
			this.lblUsername.Text = "Username";
			// 
			// txtUserName
			// 
			this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserName.BackColor = System.Drawing.SystemColors.Control;
			this.txtUserName.Location = new System.Drawing.Point(320, 12);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.ReadOnly = true;
			this.txtUserName.Size = new System.Drawing.Size(169, 20);
			this.txtUserName.TabIndex = 19;
			// 
			// treeEnvironment
			// 
			this.treeEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeEnvironment.BackColor = System.Drawing.SystemColors.Window;
			this.treeEnvironment.HotTracking = true;
			this.treeEnvironment.Location = new System.Drawing.Point(8, 40);
			this.treeEnvironment.Name = "treeEnvironment";
			this.treeEnvironment.Size = new System.Drawing.Size(481, 302);
			this.treeEnvironment.TabIndex = 24;
			// 
			// tabContact
			// 
			this.tabContact.Controls.Add(this.lblContactMessageTop);
			this.tabContact.Controls.Add(this.txtFax);
			this.tabContact.Controls.Add(this.lblFax);
			this.tabContact.Controls.Add(this.txtPhone);
			this.tabContact.Controls.Add(this.lblPhone);
			this.tabContact.Controls.Add(this.lblWebSite);
			this.tabContact.Controls.Add(this.urlWeb);
			this.tabContact.Controls.Add(this.lblEmail);
			this.tabContact.Controls.Add(this.urlEmail);
			this.tabContact.Controls.Add(this.lblContactMessageBottom);
			this.tabContact.Location = new System.Drawing.Point(4, 22);
			this.tabContact.Name = "tabContact";
			this.tabContact.Size = new System.Drawing.Size(497, 348);
			this.tabContact.TabIndex = 4;
			this.tabContact.Text = "Contact";
			this.tabContact.UseVisualStyleBackColor = true;
			// 
			// lblContactMessageTop
			// 
			this.lblContactMessageTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblContactMessageTop.Location = new System.Drawing.Point(8, 24);
			this.lblContactMessageTop.MinimumSize = new System.Drawing.Size(200, 0);
			this.lblContactMessageTop.Name = "lblContactMessageTop";
			this.lblContactMessageTop.Size = new System.Drawing.Size(441, 24);
			this.lblContactMessageTop.TabIndex = 27;
			this.lblContactMessageTop.Text = "The following details can be used to obtain support for this application.";
			// 
			// txtFax
			// 
			this.txtFax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFax.BackColor = System.Drawing.SystemColors.Control;
			this.txtFax.Location = new System.Drawing.Point(72, 168);
			this.txtFax.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtFax.Name = "txtFax";
			this.txtFax.Size = new System.Drawing.Size(249, 20);
			this.txtFax.TabIndex = 35;
			// 
			// lblFax
			// 
			this.lblFax.AutoSize = true;
			this.lblFax.Location = new System.Drawing.Point(18, 168);
			this.lblFax.Name = "lblFax";
			this.lblFax.Size = new System.Drawing.Size(24, 13);
			this.lblFax.TabIndex = 34;
			this.lblFax.Text = "Fax";
			// 
			// txtPhone
			// 
			this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPhone.BackColor = System.Drawing.SystemColors.Control;
			this.txtPhone.Location = new System.Drawing.Point(72, 142);
			this.txtPhone.MinimumSize = new System.Drawing.Size(200, 0);
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(249, 20);
			this.txtPhone.TabIndex = 33;
			// 
			// lblPhone
			// 
			this.lblPhone.AutoSize = true;
			this.lblPhone.Location = new System.Drawing.Point(16, 144);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(38, 13);
			this.lblPhone.TabIndex = 32;
			this.lblPhone.Text = "Phone";
			// 
			// lblWebSite
			// 
			this.lblWebSite.AutoSize = true;
			this.lblWebSite.Location = new System.Drawing.Point(16, 80);
			this.lblWebSite.Name = "lblWebSite";
			this.lblWebSite.Size = new System.Drawing.Size(30, 13);
			this.lblWebSite.TabIndex = 30;
			this.lblWebSite.Text = "Web";
			// 
			// urlWeb
			// 
			this.urlWeb.ActiveLinkColor = System.Drawing.Color.Orange;
			this.urlWeb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.urlWeb.AutoSize = true;
			this.urlWeb.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlWeb.Location = new System.Drawing.Point(72, 77);
			this.urlWeb.Margin = new System.Windows.Forms.Padding(5);
			this.urlWeb.MinimumSize = new System.Drawing.Size(200, 0);
			this.urlWeb.Name = "urlWeb";
			this.urlWeb.Size = new System.Drawing.Size(200, 18);
			this.urlWeb.TabIndex = 31;
			this.urlWeb.TabStop = true;
			this.urlWeb.Text = "NA";
			// 
			// lblEmail
			// 
			this.lblEmail.AutoSize = true;
			this.lblEmail.Location = new System.Drawing.Point(16, 56);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(32, 13);
			this.lblEmail.TabIndex = 28;
			this.lblEmail.Text = "Email";
			// 
			// urlEmail
			// 
			this.urlEmail.ActiveLinkColor = System.Drawing.Color.Orange;
			this.urlEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.urlEmail.AutoSize = true;
			this.urlEmail.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlEmail.Location = new System.Drawing.Point(72, 53);
			this.urlEmail.Margin = new System.Windows.Forms.Padding(5);
			this.urlEmail.MinimumSize = new System.Drawing.Size(200, 0);
			this.urlEmail.Name = "urlEmail";
			this.urlEmail.Size = new System.Drawing.Size(200, 18);
			this.urlEmail.TabIndex = 29;
			this.urlEmail.TabStop = true;
			this.urlEmail.Text = "NA";
			// 
			// lblContactMessageBottom
			// 
			this.lblContactMessageBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblContactMessageBottom.Location = new System.Drawing.Point(11, 238);
			this.lblContactMessageBottom.MinimumSize = new System.Drawing.Size(200, 0);
			this.lblContactMessageBottom.Name = "lblContactMessageBottom";
			this.lblContactMessageBottom.Size = new System.Drawing.Size(467, 110);
			this.lblContactMessageBottom.TabIndex = 36;
			this.lblContactMessageBottom.Text = "The information on this form describing the error and envrionment settings will b" +
				"e of use when contacting support.";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSave.Location = new System.Drawing.Point(358, 383);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(72, 32);
			this.btnSave.TabIndex = 56;
			this.btnSave.Text = "Save";
			this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.progressBar.Location = new System.Drawing.Point(12, 399);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(141, 16);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar.TabIndex = 53;
			// 
			// btnEmail
			// 
			this.btnEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnEmail.Image = ((System.Drawing.Image)(resources.GetObject("btnEmail.Image")));
			this.btnEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnEmail.Location = new System.Drawing.Point(436, 383);
			this.btnEmail.Name = "btnEmail";
			this.btnEmail.Size = new System.Drawing.Size(72, 32);
			this.btnEmail.TabIndex = 57;
			this.btnEmail.Text = "Email";
			this.btnEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblProgressMessage
			// 
			this.lblProgressMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblProgressMessage.Location = new System.Drawing.Point(12, 380);
			this.lblProgressMessage.Name = "lblProgressMessage";
			this.lblProgressMessage.Size = new System.Drawing.Size(262, 18);
			this.lblProgressMessage.TabIndex = 52;
			this.lblProgressMessage.Text = "Loading system information...";
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
			this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCopy.Location = new System.Drawing.Point(280, 383);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(72, 32);
			this.btnCopy.TabIndex = 55;
			this.btnCopy.Text = "Copy";
			this.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ExceptionReportView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(517, 419);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.btnEmail);
			this.Controls.Add(this.lblProgressMessage);
			this.Controls.Add(this.btnCopy);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ExceptionReportView";
			this.Text = "Exception Report";
			this.tabControl.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picGeneral)).EndInit();
			this.tabExceptions.ResumeLayout(false);
			this.tabExceptions.PerformLayout();
			this.tabAssemblies.ResumeLayout(false);
			this.tabConfig.ResumeLayout(false);
			this.tabSysInfo.ResumeLayout(false);
			this.tabSysInfo.PerformLayout();
			this.tabContact.ResumeLayout(false);
			this.tabContact.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listviewAssemblies;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.Label lblExplanation;
		private System.Windows.Forms.TextBox txtUserExplanation;
		private System.Windows.Forms.Label lblRegion;
		private System.Windows.Forms.TextBox txtRegion;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.TextBox txtDate;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.TextBox txtTime;
		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.TextBox txtApplicationName;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.TextBox txtVersion;
		private System.Windows.Forms.TabPage tabExceptions;
		private System.Windows.Forms.ListView listviewExceptions;
		private System.Windows.Forms.TextBox txtExceptionTabMessage;
		private System.Windows.Forms.TextBox txtExceptionTabStackTrace;
		private System.Windows.Forms.TabPage tabAssemblies;
		private System.Windows.Forms.TabPage tabConfig;
		private System.Windows.Forms.TreeView treeviewSettings;
		private System.Windows.Forms.TabPage tabSysInfo;
		private System.Windows.Forms.Label lblMachine;
		private System.Windows.Forms.TextBox txtMachine;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TreeView treeEnvironment;
		private System.Windows.Forms.TabPage tabContact;
		private System.Windows.Forms.Label lblContactMessageTop;
		private System.Windows.Forms.TextBox txtFax;
		private System.Windows.Forms.Label lblFax;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.Label lblPhone;
		private System.Windows.Forms.Label lblWebSite;
		private System.Windows.Forms.LinkLabel urlWeb;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.LinkLabel urlEmail;
		private System.Windows.Forms.Label lblContactMessageBottom;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button btnEmail;
		private System.Windows.Forms.Label lblProgressMessage;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.TextBox txtExceptionMessage;
		private System.Windows.Forms.PictureBox picGeneral;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}