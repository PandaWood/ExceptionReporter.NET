namespace ExceptionReporting.MVP.Views
{
	public partial class ExceptionReportView
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
            this.tabAssemblies = new System.Windows.Forms.TabPage();
            this.tabSysInfo = new System.Windows.Forms.TabPage();
            this.treeEnvironment = new System.Windows.Forms.TreeView();
            this.btnSave = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnEmail = new System.Windows.Forms.Button();
            this.lblProgressMessage = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnDetailToggle = new System.Windows.Forms.Button();
            this.txtExceptionMessageLarge = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lessDetailPanel = new System.Windows.Forms.Panel();
            this.lessDetail_optionsPanel = new System.Windows.Forms.Panel();
            this.lblContactCompany = new System.Windows.Forms.Label();
            this.btnSimpleEmail = new System.Windows.Forms.Button();
            this.btnSimpleDetailToggle = new System.Windows.Forms.Button();
            this.btnSimpleCopy = new System.Windows.Forms.Button();
            this.txtExceptionMessageLarge2 = new System.Windows.Forms.TextBox();
            this.lessDetail_alertIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).BeginInit();
            this.tabAssemblies.SuspendLayout();
            this.tabSysInfo.SuspendLayout();
            this.lessDetailPanel.SuspendLayout();
            this.lessDetail_optionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lessDetail_alertIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // listviewAssemblies
            // 
            resources.ApplyResources(this.listviewAssemblies, "listviewAssemblies");
            this.listviewAssemblies.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listviewAssemblies.FullRowSelect = true;
            this.listviewAssemblies.HotTracking = true;
            this.listviewAssemblies.HoverSelection = true;
            this.listviewAssemblies.Name = "listviewAssemblies";
            this.listviewAssemblies.UseCompatibleStateImageBehavior = false;
            this.listviewAssemblies.View = System.Windows.Forms.View.Details;
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabExceptions);
            this.tabControl.Controls.Add(this.tabAssemblies);
            this.tabControl.Controls.Add(this.tabSysInfo);
            this.tabControl.HotTrack = true;
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
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
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // picGeneral
            // 
            resources.ApplyResources(this.picGeneral, "picGeneral");
            this.picGeneral.Name = "picGeneral";
            this.picGeneral.TabStop = false;
            // 
            // txtExceptionMessage
            // 
            resources.ApplyResources(this.txtExceptionMessage, "txtExceptionMessage");
            this.txtExceptionMessage.BackColor = System.Drawing.Color.White;
            this.txtExceptionMessage.Name = "txtExceptionMessage";
            this.txtExceptionMessage.ReadOnly = true;
            // 
            // lblExplanation
            // 
            resources.ApplyResources(this.lblExplanation, "lblExplanation");
            this.lblExplanation.Name = "lblExplanation";
            // 
            // txtUserExplanation
            // 
            resources.ApplyResources(this.txtUserExplanation, "txtUserExplanation");
            this.txtUserExplanation.BackColor = System.Drawing.Color.Cornsilk;
            this.txtUserExplanation.Name = "txtUserExplanation";
            // 
            // lblRegion
            // 
            resources.ApplyResources(this.lblRegion, "lblRegion");
            this.lblRegion.Name = "lblRegion";
            // 
            // txtRegion
            // 
            resources.ApplyResources(this.txtRegion, "txtRegion");
            this.txtRegion.BackColor = System.Drawing.Color.Snow;
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.ReadOnly = true;
            // 
            // lblDate
            // 
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.Name = "lblDate";
            // 
            // txtDate
            // 
            resources.ApplyResources(this.txtDate, "txtDate");
            this.txtDate.BackColor = System.Drawing.Color.Snow;
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // txtTime
            // 
            resources.ApplyResources(this.txtTime, "txtTime");
            this.txtTime.BackColor = System.Drawing.Color.Snow;
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            // 
            // lblApplication
            // 
            resources.ApplyResources(this.lblApplication, "lblApplication");
            this.lblApplication.Name = "lblApplication";
            // 
            // txtApplicationName
            // 
            resources.ApplyResources(this.txtApplicationName, "txtApplicationName");
            this.txtApplicationName.BackColor = System.Drawing.Color.Snow;
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.ReadOnly = true;
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // txtVersion
            // 
            resources.ApplyResources(this.txtVersion, "txtVersion");
            this.txtVersion.BackColor = System.Drawing.Color.Snow;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            // 
            // tabExceptions
            // 
            resources.ApplyResources(this.tabExceptions, "tabExceptions");
            this.tabExceptions.Name = "tabExceptions";
            this.tabExceptions.UseVisualStyleBackColor = true;
            // 
            // tabAssemblies
            // 
            resources.ApplyResources(this.tabAssemblies, "tabAssemblies");
            this.tabAssemblies.Controls.Add(this.listviewAssemblies);
            this.tabAssemblies.Name = "tabAssemblies";
            this.tabAssemblies.UseVisualStyleBackColor = true;
            // 
            // tabSysInfo
            // 
            resources.ApplyResources(this.tabSysInfo, "tabSysInfo");
            this.tabSysInfo.Controls.Add(this.treeEnvironment);
            this.tabSysInfo.Name = "tabSysInfo";
            this.tabSysInfo.UseVisualStyleBackColor = true;
            // 
            // treeEnvironment
            // 
            resources.ApplyResources(this.treeEnvironment, "treeEnvironment");
            this.treeEnvironment.BackColor = System.Drawing.SystemColors.Window;
            this.treeEnvironment.HotTracking = true;
            this.treeEnvironment.Name = "treeEnvironment";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // btnEmail
            // 
            resources.ApplyResources(this.btnEmail, "btnEmail");
            this.btnEmail.Name = "btnEmail";
            // 
            // lblProgressMessage
            // 
            resources.ApplyResources(this.lblProgressMessage, "lblProgressMessage");
            this.lblProgressMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblProgressMessage.Name = "lblProgressMessage";
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            // 
            // btnDetailToggle
            // 
            resources.ApplyResources(this.btnDetailToggle, "btnDetailToggle");
            this.btnDetailToggle.Name = "btnDetailToggle";
            // 
            // txtExceptionMessageLarge
            // 
            resources.ApplyResources(this.txtExceptionMessageLarge, "txtExceptionMessageLarge");
            this.txtExceptionMessageLarge.BackColor = System.Drawing.Color.White;
            this.txtExceptionMessageLarge.Name = "txtExceptionMessageLarge";
            this.txtExceptionMessageLarge.ReadOnly = true;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            // 
            // lessDetailPanel
            // 
            resources.ApplyResources(this.lessDetailPanel, "lessDetailPanel");
            this.lessDetailPanel.BackColor = System.Drawing.Color.White;
            this.lessDetailPanel.Controls.Add(this.lessDetail_optionsPanel);
            this.lessDetailPanel.Controls.Add(this.txtExceptionMessageLarge2);
            this.lessDetailPanel.Controls.Add(this.lessDetail_alertIcon);
            this.lessDetailPanel.Controls.Add(this.label1);
            this.lessDetailPanel.Name = "lessDetailPanel";
            // 
            // lessDetail_optionsPanel
            // 
            resources.ApplyResources(this.lessDetail_optionsPanel, "lessDetail_optionsPanel");
            this.lessDetail_optionsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lessDetail_optionsPanel.Controls.Add(this.lblContactCompany);
            this.lessDetail_optionsPanel.Controls.Add(this.btnSimpleEmail);
            this.lessDetail_optionsPanel.Controls.Add(this.btnSimpleDetailToggle);
            this.lessDetail_optionsPanel.Controls.Add(this.btnSimpleCopy);
            this.lessDetail_optionsPanel.Name = "lessDetail_optionsPanel";
            // 
            // lblContactCompany
            // 
            resources.ApplyResources(this.lblContactCompany, "lblContactCompany");
            this.lblContactCompany.ForeColor = System.Drawing.Color.SlateGray;
            this.lblContactCompany.Name = "lblContactCompany";
            // 
            // btnSimpleEmail
            // 
            resources.ApplyResources(this.btnSimpleEmail, "btnSimpleEmail");
            this.btnSimpleEmail.FlatAppearance.BorderSize = 0;
            this.btnSimpleEmail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(230)))));
            this.btnSimpleEmail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.btnSimpleEmail.Name = "btnSimpleEmail";
            // 
            // btnSimpleDetailToggle
            // 
            resources.ApplyResources(this.btnSimpleDetailToggle, "btnSimpleDetailToggle");
            this.btnSimpleDetailToggle.FlatAppearance.BorderSize = 0;
            this.btnSimpleDetailToggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(230)))));
            this.btnSimpleDetailToggle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.btnSimpleDetailToggle.Name = "btnSimpleDetailToggle";
            // 
            // btnSimpleCopy
            // 
            resources.ApplyResources(this.btnSimpleCopy, "btnSimpleCopy");
            this.btnSimpleCopy.FlatAppearance.BorderSize = 0;
            this.btnSimpleCopy.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(230)))));
            this.btnSimpleCopy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.btnSimpleCopy.Name = "btnSimpleCopy";
            // 
            // txtExceptionMessageLarge2
            // 
            resources.ApplyResources(this.txtExceptionMessageLarge2, "txtExceptionMessageLarge2");
            this.txtExceptionMessageLarge2.BackColor = System.Drawing.Color.White;
            this.txtExceptionMessageLarge2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExceptionMessageLarge2.Name = "txtExceptionMessageLarge2";
            this.txtExceptionMessageLarge2.ReadOnly = true;
            // 
            // lessDetail_alertIcon
            // 
            resources.ApplyResources(this.lessDetail_alertIcon, "lessDetail_alertIcon");
            this.lessDetail_alertIcon.Name = "lessDetail_alertIcon";
            this.lessDetail_alertIcon.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ExceptionReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lessDetailPanel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDetailToggle);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnEmail);
            this.Controls.Add(this.lblProgressMessage);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtExceptionMessageLarge);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionReportView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).EndInit();
            this.tabAssemblies.ResumeLayout(false);
            this.tabSysInfo.ResumeLayout(false);
            this.lessDetailPanel.ResumeLayout(false);
            this.lessDetailPanel.PerformLayout();
            this.lessDetail_optionsPanel.ResumeLayout(false);
            this.lessDetail_optionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lessDetail_alertIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TabPage tabAssemblies;
        private System.Windows.Forms.TabPage tabSysInfo;
        private System.Windows.Forms.TreeView treeEnvironment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.Label lblProgressMessage;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtExceptionMessage;
        private System.Windows.Forms.PictureBox picGeneral;
        private System.Windows.Forms.Button btnDetailToggle;
        private System.Windows.Forms.TextBox txtExceptionMessageLarge;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel lessDetailPanel;
        private System.Windows.Forms.PictureBox lessDetail_alertIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel lessDetail_optionsPanel;
        private System.Windows.Forms.TextBox txtExceptionMessageLarge2;
        private System.Windows.Forms.Label lblContactCompany;
        private System.Windows.Forms.Button btnSimpleEmail;
        private System.Windows.Forms.Button btnSimpleCopy;
        private System.Windows.Forms.Button btnSimpleDetailToggle;
    }
}