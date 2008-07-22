// created on 23/03/2004 at 12:25
using System;
using System.Windows.Forms;
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
	internal class frmRegistrationReminder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.LinkLabel lnkTrial;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.GroupBox grpBorder;
		/**********************************************************
		 * Registration Reminder Class
		 * 
		 * 
		 * Developer		Date		Comment  
		 * Phillip Pettit	Mar/04		Initial Creation
		 **********************************************************/
		public frmRegistrationReminder()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmRegistrationReminder));
			this.grpBorder = new System.Windows.Forms.GroupBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.lnkTrial = new System.Windows.Forms.LinkLabel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.grpBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBorder
			// 
			this.grpBorder.Controls.Add(this.lnkTrial);
			this.grpBorder.Controls.Add(this.pictureBox);
			this.grpBorder.Location = new System.Drawing.Point(8, 8);
			this.grpBorder.Name = "grpBorder";
			this.grpBorder.Size = new System.Drawing.Size(520, 104);
			this.grpBorder.TabIndex = 0;
			this.grpBorder.TabStop = false;
			// 
			// cmdOK
			// 
			this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdOK.Location = new System.Drawing.Point(440, 120);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(72, 24);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "&OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// lnkTrial
			// 
			this.lnkTrial.Location = new System.Drawing.Point(216, 32);
			this.lnkTrial.Name = "lnkTrial";
			this.lnkTrial.Size = new System.Drawing.Size(296, 56);
			this.lnkTrial.TabIndex = 1;
			this.lnkTrial.TabStop = true;
			this.lnkTrial.Text = "This is a trial version of the Exception Reporter.  If you would like to use this" +
" product within your application(s) please visit www.stratalogic.com for further" +
" information.";
			this.lnkTrial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkTrial.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkTrialLinkClicked);
			// 
			// pictureBox
			// 
			this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
			this.pictureBox.Location = new System.Drawing.Point(8, 16);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(192, 80);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 46;
			this.pictureBox.TabStop = false;
			// 
			// frmRegistrationReminder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(538, 152);
			this.ControlBox = false;
			this.Controls.Add(this.grpBorder);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmRegistrationReminder";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Registration Reminder";
			this.grpBorder.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		
		public void ShowReminder() {
			/**********************************************************
			 * Display the Reminder message
			 * 
			 * Pass:	
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			 
			this.ShowDialog();
			
		}
		void cmdOK_Click(object sender, System.EventArgs e)
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
			this.Close();
		}
		void LnkTrialLinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			/**********************************************************
			 * Handle the click of the trial link (open the link)
			 * 
			 * Pass:	
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/

			Process.Start("http://www.stratalogic.com");
		}
		
	}
}
