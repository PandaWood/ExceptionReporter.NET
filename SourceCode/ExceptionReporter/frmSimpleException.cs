// created on 23/03/2004 at 16:35
using System;
using System.Windows.Forms;
using System.Text;
using System.IO;

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
	internal class frmSimpleException : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.TextBox txtExceptionMessage;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.Label lblExceptionMessage;
		private System.Windows.Forms.Label lblGeneralMessage;
		private System.Windows.Forms.TabPage tpGeneral;
		private System.Windows.Forms.TabPage tpException;
		private System.Windows.Forms.Label lblExceptionType;
		private System.Windows.Forms.TextBox txtExceptionDetails;
		private System.Windows.Forms.TextBox txtExceptionType;
		private System.Windows.Forms.PictureBox pictureBox;
		/**********************************************************
		 * Simple Exception Display Class
		 * 
		 * 
		 * Developer		Date		Comment  
		 * Phillip Pettit	Mar/04		Initial Creation
		 **********************************************************/
		public frmSimpleException()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSimpleException));
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.txtExceptionType = new System.Windows.Forms.TextBox();
			this.txtExceptionDetails = new System.Windows.Forms.TextBox();
			this.lblExceptionType = new System.Windows.Forms.Label();
			this.tpException = new System.Windows.Forms.TabPage();
			this.tpGeneral = new System.Windows.Forms.TabPage();
			this.lblGeneralMessage = new System.Windows.Forms.Label();
			this.lblExceptionMessage = new System.Windows.Forms.Label();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.txtExceptionMessage = new System.Windows.Forms.TextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.tpException.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
			this.pictureBox.Location = new System.Drawing.Point(8, 8);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(48, 64);
			this.pictureBox.TabIndex = 5;
			this.pictureBox.TabStop = false;
			// 
			// txtExceptionType
			// 
			this.txtExceptionType.Location = new System.Drawing.Point(56, 80);
			this.txtExceptionType.Name = "txtExceptionType";
			this.txtExceptionType.ReadOnly = true;
			this.txtExceptionType.Size = new System.Drawing.Size(304, 21);
			this.txtExceptionType.TabIndex = 3;
			this.txtExceptionType.Text = "";
			// 
			// txtExceptionDetails
			// 
			this.txtExceptionDetails.Location = new System.Drawing.Point(8, 8);
			this.txtExceptionDetails.Multiline = true;
			this.txtExceptionDetails.Name = "txtExceptionDetails";
			this.txtExceptionDetails.ReadOnly = true;
			this.txtExceptionDetails.Size = new System.Drawing.Size(352, 176);
			this.txtExceptionDetails.TabIndex = 6;
			this.txtExceptionDetails.Text = "";
			// 
			// lblExceptionType
			// 
			this.lblExceptionType.Location = new System.Drawing.Point(8, 80);
			this.lblExceptionType.Name = "lblExceptionType";
			this.lblExceptionType.Size = new System.Drawing.Size(40, 16);
			this.lblExceptionType.TabIndex = 2;
			this.lblExceptionType.Text = "Type:";
			// 
			// tpException
			// 
			this.tpException.Controls.Add(this.txtExceptionDetails);
			this.tpException.Location = new System.Drawing.Point(4, 22);
			this.tpException.Name = "tpException";
			this.tpException.Size = new System.Drawing.Size(368, 190);
			this.tpException.TabIndex = 1;
			this.tpException.Text = "Exception";
			this.tpException.ToolTipText = "Displays detailed information about the error that occurred";
			// 
			// tpGeneral
			// 
			this.tpGeneral.Controls.Add(this.pictureBox);
			this.tpGeneral.Controls.Add(this.txtExceptionMessage);
			this.tpGeneral.Controls.Add(this.txtExceptionType);
			this.tpGeneral.Controls.Add(this.lblExceptionType);
			this.tpGeneral.Controls.Add(this.lblExceptionMessage);
			this.tpGeneral.Controls.Add(this.lblGeneralMessage);
			this.tpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Size = new System.Drawing.Size(368, 190);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = "General";
			this.tpGeneral.ToolTipText = "Displays general information about the error that occurred";
			// 
			// lblGeneralMessage
			// 
			this.lblGeneralMessage.Location = new System.Drawing.Point(64, 8);
			this.lblGeneralMessage.Name = "lblGeneralMessage";
			this.lblGeneralMessage.Size = new System.Drawing.Size(296, 64);
			this.lblGeneralMessage.TabIndex = 1;
			this.lblGeneralMessage.Text = "An error has occurred within the Exception Reporter";
			// 
			// lblExceptionMessage
			// 
			this.lblExceptionMessage.Location = new System.Drawing.Point(8, 104);
			this.lblExceptionMessage.Name = "lblExceptionMessage";
			this.lblExceptionMessage.Size = new System.Drawing.Size(56, 16);
			this.lblExceptionMessage.TabIndex = 4;
			this.lblExceptionMessage.Text = "Message:";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tpGeneral);
			this.tabControl.Controls.Add(this.tpException);
			this.tabControl.HotTrack = true;
			this.tabControl.Location = new System.Drawing.Point(8, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.ShowToolTips = true;
			this.tabControl.Size = new System.Drawing.Size(376, 216);
			this.tabControl.TabIndex = 0;
			// 
			// txtExceptionMessage
			// 
			this.txtExceptionMessage.Location = new System.Drawing.Point(56, 104);
			this.txtExceptionMessage.Multiline = true;
			this.txtExceptionMessage.Name = "txtExceptionMessage";
			this.txtExceptionMessage.ReadOnly = true;
			this.txtExceptionMessage.Size = new System.Drawing.Size(304, 80);
			this.txtExceptionMessage.TabIndex = 5;
			this.txtExceptionMessage.Text = "";
			// 
			// cmdOK
			// 
			this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdOK.Location = new System.Drawing.Point(296, 232);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(72, 24);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "&OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// frmSimpleException
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(386, 262);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSimpleException";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Exception Reporter: Problem Occurred";
			this.tpException.ResumeLayout(false);
			this.tpGeneral.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		
		public void ShowException(string message, Exception ex) {
			/**********************************************************
			 * ShowException - show the form
			 * 
			 * Pass:	message - message to show
			 * 			ex - Exception to show details of
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			 
			lblGeneralMessage.Text = message;
			
			if (!(ex==null)) {
				txtExceptionType.Text = ex.GetType().ToString();
				txtExceptionMessage.Text = ex.Message;
				txtExceptionDetails.Text = exceptionHeirarchyToString(ex);
			}
			
			this.ShowDialog();
			
		}
		
		
		private string exceptionHeirarchyToString(Exception ex) 
		{
			/**********************************************************
			 * exceptionHeirarchyToString - convert set of exceptions to string
			 * for display
			 * 
			 * Pass:	ex - Exception to show details of
			 *
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			StringBuilder sb = new StringBuilder();
			StringWriter swWriter = new StringWriter(sb);
			
			Int32 intCount = 0;
			Exception current = ex;

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
			return sb.ToString();

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
		
		
	}
}
