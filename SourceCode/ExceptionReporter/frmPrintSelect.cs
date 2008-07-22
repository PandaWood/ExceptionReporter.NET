// created on 20/03/2004 at 11:34
using System;
using System.Windows.Forms;

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
	internal class frmPrintSelect : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.CheckBox chkPrintContact;
		private System.Windows.Forms.CheckBox chkPrintEnvironment;
		private System.Windows.Forms.CheckBox chkPrintGeneral;
		private System.Windows.Forms.Label lblPrintSelect2;
		private System.Windows.Forms.CheckBox chkPrintSettings;
		private System.Windows.Forms.GroupBox grbLine;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.CheckBox chkPrintExceptions;
		private System.Windows.Forms.Label lblPrintSelect;
		private System.Windows.Forms.CheckBox chkPrintAssemblies;
		/**********************************************************
		 * Print Item Selector Class
		 * 
		 * 
		 * Developer		Date		Comment  
		 * Phillip Pettit	Mar/04		Initial Creation
		 **********************************************************/
		private bool ContinuePrint = false;
		
		public frmPrintSelect()
		{
			/**********************************************************
			 * Constructor
			 * 
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			InitializeComponent();
		}
		
		// THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
		// DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
		void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmER));
			this.chkPrintAssemblies = new System.Windows.Forms.CheckBox();
			this.lblPrintSelect = new System.Windows.Forms.Label();
			this.chkPrintExceptions = new System.Windows.Forms.CheckBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.grbLine = new System.Windows.Forms.GroupBox();
			this.chkPrintSettings = new System.Windows.Forms.CheckBox();
			this.lblPrintSelect2 = new System.Windows.Forms.Label();
			this.chkPrintGeneral = new System.Windows.Forms.CheckBox();
			this.chkPrintEnvironment = new System.Windows.Forms.CheckBox();
			this.chkPrintContact = new System.Windows.Forms.CheckBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.grbLine.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkPrintAssemblies
			// 
			this.chkPrintAssemblies.Checked = true;
			this.chkPrintAssemblies.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintAssemblies.Location = new System.Drawing.Point(248, 48);
			this.chkPrintAssemblies.Name = "chkPrintAssemblies";
			this.chkPrintAssemblies.Size = new System.Drawing.Size(88, 24);
			this.chkPrintAssemblies.TabIndex = 9;
			this.chkPrintAssemblies.Text = "&Assemblies";
			// 
			// lblPrintSelect
			// 
			this.lblPrintSelect.Location = new System.Drawing.Point(8, 24);
			this.lblPrintSelect.Name = "lblPrintSelect";
			this.lblPrintSelect.Size = new System.Drawing.Size(256, 16);
			this.lblPrintSelect.TabIndex = 13;
			this.lblPrintSelect.Text = "Please select the details you would like to Print:";
			// 
			// chkPrintExceptions
			// 
			this.chkPrintExceptions.Checked = true;
			this.chkPrintExceptions.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintExceptions.Location = new System.Drawing.Point(136, 48);
			this.chkPrintExceptions.Name = "chkPrintExceptions";
			this.chkPrintExceptions.Size = new System.Drawing.Size(96, 24);
			this.chkPrintExceptions.TabIndex = 8;
			this.chkPrintExceptions.Text = "&Exceptions";
			// 
			// cmdCancel
			// 
			this.cmdCancel.CausesValidation = false;
			this.cmdCancel.Location = new System.Drawing.Point(264, 176);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 10;
			this.cmdCancel.Text = "Cance&l";
			this.cmdCancel.Click += new System.EventHandler(this.CmdCancelClick);
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
			this.grbLine.Location = new System.Drawing.Point(8, 8);
			this.grbLine.Name = "grbLine";
			this.grbLine.Size = new System.Drawing.Size(352, 160);
			this.grbLine.TabIndex = 8;
			this.grbLine.TabStop = false;
			// 
			// chkPrintSettings
			// 
			this.chkPrintSettings.Checked = true;
			this.chkPrintSettings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintSettings.Location = new System.Drawing.Point(32, 80);
			this.chkPrintSettings.Name = "chkPrintSettings";
			this.chkPrintSettings.Size = new System.Drawing.Size(88, 24);
			this.chkPrintSettings.TabIndex = 10;
			this.chkPrintSettings.Text = "&Settings";
			// 
			// lblPrintSelect2
			// 
			this.lblPrintSelect2.Location = new System.Drawing.Point(8, 120);
			this.lblPrintSelect2.Name = "lblPrintSelect2";
			this.lblPrintSelect2.Size = new System.Drawing.Size(336, 32);
			this.lblPrintSelect2.TabIndex = 7;
			this.lblPrintSelect2.Text = "Note: Including Environment details will add a number of pages to the print.";
			// 
			// chkPrintGeneral
			// 
			this.chkPrintGeneral.Checked = true;
			this.chkPrintGeneral.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintGeneral.Location = new System.Drawing.Point(32, 48);
			this.chkPrintGeneral.Name = "chkPrintGeneral";
			this.chkPrintGeneral.Size = new System.Drawing.Size(72, 24);
			this.chkPrintGeneral.TabIndex = 7;
			this.chkPrintGeneral.Text = "&General";
			// 
			// chkPrintEnvironment
			// 
			this.chkPrintEnvironment.Location = new System.Drawing.Point(136, 80);
			this.chkPrintEnvironment.Name = "chkPrintEnvironment";
			this.chkPrintEnvironment.TabIndex = 11;
			this.chkPrintEnvironment.Text = "E&nvironment";
			// 
			// chkPrintContact
			// 
			this.chkPrintContact.Checked = true;
			this.chkPrintContact.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrintContact.Location = new System.Drawing.Point(248, 80);
			this.chkPrintContact.Name = "chkPrintContact";
			this.chkPrintContact.Size = new System.Drawing.Size(72, 24);
			this.chkPrintContact.TabIndex = 12;
			this.chkPrintContact.Text = "&Contact";
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(168, 176);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 24);
			this.cmdOK.TabIndex = 9;
			this.cmdOK.Text = "&OK";
			this.cmdOK.Click += new System.EventHandler(this.CmdOKClick);
			// 
			// frmER
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(370, 208);
			this.Controls.Add(this.grbLine);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmER";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Details for Print";
			this.grbLine.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		void CmdOKClick(object sender, System.EventArgs e)
		{
			/**********************************************************
			 * Handle OK button click
			 * 
			 * Pass:	sender - object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			ContinuePrint = true;
			this.Close();
		}
		
		public bool selectPrintDetails(ref bool blnGeneral, ref bool blnExceptions, ref bool blnAssemblies, ref bool blnSettings, ref bool blnEnvironment, ref bool blnContact) {
			
			/**********************************************************
			 * selectPrintDetails - allow user to select which details should be printed
			 * 
			 * Pass:	blnGeneral - include General info
			 * 			blnExceptions - include Exceptions Info
			 * 			blnAssemblies - include Assemblies Info
			 * 			blnSettings - include Settings Info
			 * 			blnEnvironment - include Environment info
			 * 			blnContact - include General info
			 *
			 * Returns: bool - true for success (false for cancel selected)
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			
			this.ShowDialog();
			
			blnGeneral = (chkPrintGeneral.CheckState == CheckState.Checked);
			blnExceptions = (chkPrintExceptions.CheckState == CheckState.Checked);
			blnAssemblies = (chkPrintAssemblies.CheckState == CheckState.Checked);
			blnSettings = (chkPrintSettings.CheckState == CheckState.Checked);
			blnEnvironment = (chkPrintEnvironment.CheckState == CheckState.Checked);
			blnContact = (chkPrintContact.CheckState == CheckState.Checked);
			
			return ContinuePrint;	
						
		}
		
		void CmdCancelClick(object sender, System.EventArgs e)
		{
			/**********************************************************
			 * Handle Cancel button click
			 * 
			 * Pass:	sender - object triggering event
			 * 			e - EventArgs
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/

			ContinuePrint = false;
			this.Close();
		}
		
	}
}
