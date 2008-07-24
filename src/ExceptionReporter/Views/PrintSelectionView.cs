using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

//-------------------------------------------------------------------------
// ExceptionReporter - Error Reporting Component for .Net
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

namespace ExceptionReporting.Views
{
	internal class PrintSelectionView : Form
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

		public PrintSelectionView()
		{
			InitializeComponent();
		}

	
		private void InitializeComponent()
		{
			var resources = new ResourceManager(typeof (ExceptionReportView));
			chkPrintAssemblies = new CheckBox();
			lblPrintSelect = new Label();
			chkPrintExceptions = new CheckBox();
			cmdCancel = new Button();
			grbLine = new GroupBox();
			chkPrintSettings = new CheckBox();
			lblPrintSelect2 = new Label();
			chkPrintGeneral = new CheckBox();
			chkPrintEnvironment = new CheckBox();
			chkPrintContact = new CheckBox();
			cmdOK = new Button();
			grbLine.SuspendLayout();
			SuspendLayout();
			// 
			// chkPrintAssemblies
			// 
			chkPrintAssemblies.Checked = true;
			chkPrintAssemblies.CheckState = CheckState.Checked;
			chkPrintAssemblies.Location = new Point(248, 48);
			chkPrintAssemblies.Name = "chkPrintAssemblies";
			chkPrintAssemblies.Size = new Size(88, 24);
			chkPrintAssemblies.TabIndex = 9;
			chkPrintAssemblies.Text = "&Assemblies";
			// 
			// lblPrintSelect
			// 
			lblPrintSelect.Location = new Point(8, 24);
			lblPrintSelect.Name = "lblPrintSelect";
			lblPrintSelect.Size = new Size(256, 16);
			lblPrintSelect.TabIndex = 13;
			lblPrintSelect.Text = "Please select the details you would like to Print:";
			// 
			// chkPrintExceptions
			// 
			chkPrintExceptions.Checked = true;
			chkPrintExceptions.CheckState = CheckState.Checked;
			chkPrintExceptions.Location = new Point(136, 48);
			chkPrintExceptions.Name = "chkPrintExceptions";
			chkPrintExceptions.Size = new Size(96, 24);
			chkPrintExceptions.TabIndex = 8;
			chkPrintExceptions.Text = "&Exceptions";
			// 
			// cmdCancel
			// 
			cmdCancel.CausesValidation = false;
			cmdCancel.Location = new Point(264, 176);
			cmdCancel.Name = "cmdCancel";
			cmdCancel.Size = new Size(88, 24);
			cmdCancel.TabIndex = 10;
			cmdCancel.Text = "Cance&l";
			cmdCancel.Click += CmdCancelClick;
			// 
			// grbLine
			// 
			grbLine.Controls.Add(lblPrintSelect);
			grbLine.Controls.Add(chkPrintContact);
			grbLine.Controls.Add(chkPrintEnvironment);
			grbLine.Controls.Add(chkPrintSettings);
			grbLine.Controls.Add(chkPrintAssemblies);
			grbLine.Controls.Add(chkPrintExceptions);
			grbLine.Controls.Add(chkPrintGeneral);
			grbLine.Controls.Add(lblPrintSelect2);
			grbLine.Location = new Point(8, 8);
			grbLine.Name = "grbLine";
			grbLine.Size = new Size(352, 160);
			grbLine.TabIndex = 8;
			grbLine.TabStop = false;
			// 
			// chkPrintSettings
			// 
			chkPrintSettings.Checked = true;
			chkPrintSettings.CheckState = CheckState.Checked;
			chkPrintSettings.Location = new Point(32, 80);
			chkPrintSettings.Name = "chkPrintSettings";
			chkPrintSettings.Size = new Size(88, 24);
			chkPrintSettings.TabIndex = 10;
			chkPrintSettings.Text = "&Settings";
			// 
			// lblPrintSelect2
			// 
			lblPrintSelect2.Location = new Point(8, 120);
			lblPrintSelect2.Name = "lblPrintSelect2";
			lblPrintSelect2.Size = new Size(336, 32);
			lblPrintSelect2.TabIndex = 7;
			lblPrintSelect2.Text = "Note: Including Environment details will add a number of pages to the print.";
			// 
			// chkPrintGeneral
			// 
			chkPrintGeneral.Checked = true;
			chkPrintGeneral.CheckState = CheckState.Checked;
			chkPrintGeneral.Location = new Point(32, 48);
			chkPrintGeneral.Name = "chkPrintGeneral";
			chkPrintGeneral.Size = new Size(72, 24);
			chkPrintGeneral.TabIndex = 7;
			chkPrintGeneral.Text = "&General";
			// 
			// chkPrintEnvironment
			// 
			chkPrintEnvironment.Location = new Point(136, 80);
			chkPrintEnvironment.Name = "chkPrintEnvironment";
			chkPrintEnvironment.TabIndex = 11;
			chkPrintEnvironment.Text = "E&nvironment";
			// 
			// chkPrintContact
			// 
			chkPrintContact.Checked = true;
			chkPrintContact.CheckState = CheckState.Checked;
			chkPrintContact.Location = new Point(248, 80);
			chkPrintContact.Name = "chkPrintContact";
			chkPrintContact.Size = new Size(72, 24);
			chkPrintContact.TabIndex = 12;
			chkPrintContact.Text = "&Contact";
			// 
			// cmdOK
			// 
			cmdOK.Location = new Point(168, 176);
			cmdOK.Name = "cmdOK";
			cmdOK.Size = new Size(88, 24);
			cmdOK.TabIndex = 9;
			cmdOK.Text = "&OK";
			cmdOK.Click += CmdOKClick;
			// 
			// OldExceptionReportView
			// 
			AutoScaleBaseSize = new Size(5, 14);
			ClientSize = new Size(370, 208);
			Controls.Add(grbLine);
			Controls.Add(cmdCancel);
			Controls.Add(cmdOK);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = ((Icon) (resources.GetObject("$this.Icon")));
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "OldExceptionReportView";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Details for Print";
			grbLine.ResumeLayout(false);
			ResumeLayout(false);
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