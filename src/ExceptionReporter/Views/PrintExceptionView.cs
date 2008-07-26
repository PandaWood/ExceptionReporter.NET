using System;
using System.Windows.Forms;

namespace ExceptionReporting.Views
{
	internal class PrintExceptionView : Form
	{
		private Button cmdOK;
		private CheckBox chkPrintContact;
		private CheckBox chkPrintEnvironment;
		private CheckBox chkPrintGeneral;
		private Label lblPrintSelect2;
		private CheckBox chkPrintSettings;
		private GroupBox grbLine;
		private Button cmdCancel;
		private CheckBox chkPrintExceptions;
		private Label lblPrintSelect;
		private CheckBox chkPrintAssemblies;
		private bool ContinuePrint;

		public PrintExceptionView()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.chkPrintAssemblies = new System.Windows.Forms.CheckBox();
			this.lblPrintSelect = new System.Windows.Forms.Label();
			this.chkPrintExceptions = new System.Windows.Forms.CheckBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.grbLine = new System.Windows.Forms.GroupBox();
			this.chkPrintContact = new System.Windows.Forms.CheckBox();
			this.chkPrintEnvironment = new System.Windows.Forms.CheckBox();
			this.chkPrintSettings = new System.Windows.Forms.CheckBox();
			this.chkPrintGeneral = new System.Windows.Forms.CheckBox();
			this.lblPrintSelect2 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.grbLine.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkPrintAssemblies
			// 
			this.chkPrintAssemblies.Checked = true;
			this.chkPrintAssemblies.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintAssemblies.Location = new System.Drawing.Point(248, 45);
			this.chkPrintAssemblies.Name = "chkPrintAssemblies";
			this.chkPrintAssemblies.Size = new System.Drawing.Size(88, 22);
			this.chkPrintAssemblies.TabIndex = 9;
			this.chkPrintAssemblies.Text = "&Assemblies";
			// 
			// lblPrintSelect
			// 
			this.lblPrintSelect.Location = new System.Drawing.Point(8, 22);
			this.lblPrintSelect.Name = "lblPrintSelect";
			this.lblPrintSelect.Size = new System.Drawing.Size(256, 15);
			this.lblPrintSelect.TabIndex = 13;
			this.lblPrintSelect.Text = "Please select the details you would like to Print:";
			// 
			// chkPrintExceptions
			// 
			this.chkPrintExceptions.Checked = true;
			this.chkPrintExceptions.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintExceptions.Location = new System.Drawing.Point(136, 45);
			this.chkPrintExceptions.Name = "chkPrintExceptions";
			this.chkPrintExceptions.Size = new System.Drawing.Size(96, 22);
			this.chkPrintExceptions.TabIndex = 8;
			this.chkPrintExceptions.Text = "&Exceptions";
			// 
			// cmdCancel
			// 
			this.cmdCancel.CausesValidation = false;
			this.cmdCancel.Location = new System.Drawing.Point(264, 163);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 23);
			this.cmdCancel.TabIndex = 10;
			this.cmdCancel.Text = "Cance&l";
			// 
			// grbLine
			// 
			this.grbLine.Controls.Add(this.lblPrintSelect);
			this.grbLine.Controls.Add(this.chkPrintContact);
			this.grbLine.Controls.Add(this.chkPrintEnvironment);
			this.grbLine.Controls.Add(this.chkPrintSettings);
			this.grbLine.Controls.Add(this.chkPrintAssemblies);
			this.grbLine.Controls.Add(this.chkPrintExceptions);
			this.grbLine.Controls.Add(this.chkPrintGeneral);
			this.grbLine.Controls.Add(this.lblPrintSelect2);
			this.grbLine.Location = new System.Drawing.Point(8, 7);
			this.grbLine.Name = "grbLine";
			this.grbLine.Size = new System.Drawing.Size(352, 149);
			this.grbLine.TabIndex = 8;
			this.grbLine.TabStop = false;
			// 
			// chkPrintContact
			// 
			this.chkPrintContact.Checked = true;
			this.chkPrintContact.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintContact.Location = new System.Drawing.Point(248, 74);
			this.chkPrintContact.Name = "chkPrintContact";
			this.chkPrintContact.Size = new System.Drawing.Size(72, 23);
			this.chkPrintContact.TabIndex = 12;
			this.chkPrintContact.Text = "&Contact";
			// 
			// chkPrintEnvironment
			// 
			this.chkPrintEnvironment.Location = new System.Drawing.Point(136, 74);
			this.chkPrintEnvironment.Name = "chkPrintEnvironment";
			this.chkPrintEnvironment.Size = new System.Drawing.Size(104, 23);
			this.chkPrintEnvironment.TabIndex = 11;
			this.chkPrintEnvironment.Text = "E&nvironment";
			// 
			// chkPrintSettings
			// 
			this.chkPrintSettings.Checked = true;
			this.chkPrintSettings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintSettings.Location = new System.Drawing.Point(32, 74);
			this.chkPrintSettings.Name = "chkPrintSettings";
			this.chkPrintSettings.Size = new System.Drawing.Size(88, 23);
			this.chkPrintSettings.TabIndex = 10;
			this.chkPrintSettings.Text = "&Settings";
			// 
			// chkPrintGeneral
			// 
			this.chkPrintGeneral.Checked = true;
			this.chkPrintGeneral.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintGeneral.Location = new System.Drawing.Point(32, 45);
			this.chkPrintGeneral.Name = "chkPrintGeneral";
			this.chkPrintGeneral.Size = new System.Drawing.Size(72, 22);
			this.chkPrintGeneral.TabIndex = 7;
			this.chkPrintGeneral.Text = "&General";
			// 
			// lblPrintSelect2
			// 
			this.lblPrintSelect2.Location = new System.Drawing.Point(8, 111);
			this.lblPrintSelect2.Name = "lblPrintSelect2";
			this.lblPrintSelect2.Size = new System.Drawing.Size(336, 30);
			this.lblPrintSelect2.TabIndex = 7;
			this.lblPrintSelect2.Text = "Note: Including Environment details will add a number of pages to the print.";
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(168, 163);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 23);
			this.cmdOK.TabIndex = 9;
			this.cmdOK.Text = "OK";
			// 
			// PrintSelectionView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(370, 208);
			this.Controls.Add(this.grbLine);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrintSelectionView";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Details for Print";
			this.grbLine.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private void CmdOKClick(object sender, EventArgs e)
		{
			ContinuePrint = true;
			Close();
		}

		public bool selectPrintDetails(ref bool blnGeneral, ref bool blnExceptions, ref bool blnAssemblies,
		                               ref bool blnSettings, ref bool blnEnvironment, ref bool blnContact)
		{
			ShowDialog();

			blnGeneral = (chkPrintGeneral.CheckState == CheckState.Checked);
			blnExceptions = (chkPrintExceptions.CheckState == CheckState.Checked);
			blnAssemblies = (chkPrintAssemblies.CheckState == CheckState.Checked);
			blnSettings = (chkPrintSettings.CheckState == CheckState.Checked);
			blnEnvironment = (chkPrintEnvironment.CheckState == CheckState.Checked);
			blnContact = (chkPrintContact.CheckState == CheckState.Checked);

			return ContinuePrint;
		}

		private void CmdCancelClick(object sender, EventArgs e)
		{
			ContinuePrint = false;
			Close();
		}
	}
}