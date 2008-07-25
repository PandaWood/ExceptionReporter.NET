using System;
using System.Drawing;
using System.IO;
using System.Text;
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

namespace ExceptionReporting.Views
{
	internal class SimpleExceptionView : Form
	{
		private Button cmdOK;
		private TextBox txtExceptionMessage;
		private TabControl tabControl;
		private Label lblExceptionMessage;
		private Label lblGeneralMessage;
		private TabPage tpGeneral;
		private TabPage tpException;
		private Label lblExceptionType;
		private TextBox txtExceptionDetails;
		private TextBox txtExceptionType;
		private PictureBox pictureBox;
		
		public SimpleExceptionView()
		{
			InitializeComponent();
		}
		
		private void InitializeComponent()
		{
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.txtExceptionType = new System.Windows.Forms.TextBox();
			this.txtExceptionDetails = new System.Windows.Forms.TextBox();
			this.lblExceptionType = new System.Windows.Forms.Label();
			this.tpException = new System.Windows.Forms.TabPage();
			this.tpGeneral = new System.Windows.Forms.TabPage();
			this.txtExceptionMessage = new System.Windows.Forms.TextBox();
			this.lblExceptionMessage = new System.Windows.Forms.Label();
			this.lblGeneralMessage = new System.Windows.Forms.Label();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.cmdOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.tpException.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(58, 46);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// txtExceptionType
			// 
			this.txtExceptionType.Location = new System.Drawing.Point(56, 74);
			this.txtExceptionType.Name = "txtExceptionType";
			this.txtExceptionType.ReadOnly = true;
			this.txtExceptionType.Size = new System.Drawing.Size(304, 20);
			this.txtExceptionType.TabIndex = 3;
			// 
			// txtExceptionDetails
			// 
			this.txtExceptionDetails.Location = new System.Drawing.Point(8, 7);
			this.txtExceptionDetails.Multiline = true;
			this.txtExceptionDetails.Name = "txtExceptionDetails";
			this.txtExceptionDetails.ReadOnly = true;
			this.txtExceptionDetails.Size = new System.Drawing.Size(352, 164);
			this.txtExceptionDetails.TabIndex = 6;
			// 
			// lblExceptionType
			// 
			this.lblExceptionType.Location = new System.Drawing.Point(8, 74);
			this.lblExceptionType.Name = "lblExceptionType";
			this.lblExceptionType.Size = new System.Drawing.Size(40, 15);
			this.lblExceptionType.TabIndex = 2;
			this.lblExceptionType.Text = "Type:";
			// 
			// tpException
			// 
			this.tpException.Controls.Add(this.txtExceptionDetails);
			this.tpException.Location = new System.Drawing.Point(4, 22);
			this.tpException.Name = "tpException";
			this.tpException.Size = new System.Drawing.Size(368, 175);
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
			this.tpGeneral.Size = new System.Drawing.Size(368, 175);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = "General";
			this.tpGeneral.ToolTipText = "Displays general information about the error that occurred";
			// 
			// txtExceptionMessage
			// 
			this.txtExceptionMessage.Location = new System.Drawing.Point(56, 97);
			this.txtExceptionMessage.Multiline = true;
			this.txtExceptionMessage.Name = "txtExceptionMessage";
			this.txtExceptionMessage.ReadOnly = true;
			this.txtExceptionMessage.Size = new System.Drawing.Size(304, 74);
			this.txtExceptionMessage.TabIndex = 5;
			// 
			// lblExceptionMessage
			// 
			this.lblExceptionMessage.Location = new System.Drawing.Point(8, 97);
			this.lblExceptionMessage.Name = "lblExceptionMessage";
			this.lblExceptionMessage.Size = new System.Drawing.Size(56, 14);
			this.lblExceptionMessage.TabIndex = 4;
			this.lblExceptionMessage.Text = "Message:";
			// 
			// lblGeneralMessage
			// 
			this.lblGeneralMessage.Location = new System.Drawing.Point(64, 7);
			this.lblGeneralMessage.Name = "lblGeneralMessage";
			this.lblGeneralMessage.Size = new System.Drawing.Size(296, 60);
			this.lblGeneralMessage.TabIndex = 1;
			this.lblGeneralMessage.Text = "An error has occurred within the Exception Reporter";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tpGeneral);
			this.tabControl.Controls.Add(this.tpException);
			this.tabControl.HotTrack = true;
			this.tabControl.Location = new System.Drawing.Point(8, 7);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.ShowToolTips = true;
			this.tabControl.Size = new System.Drawing.Size(376, 201);
			this.tabControl.TabIndex = 0;
			// 
			// cmdOK
			// 
			this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdOK.Location = new System.Drawing.Point(308, 214);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(72, 23);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "OK";
			// 
			// SimpleExceptionView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(386, 241);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.cmdOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SimpleExceptionView";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Exception Reporter: Problem Occurred";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.tpException.ResumeLayout(false);
			this.tpException.PerformLayout();
			this.tpGeneral.ResumeLayout(false);
			this.tpGeneral.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		public void ShowException(string message, Exception ex)
		{

			lblGeneralMessage.Text = message;

			if (!(ex == null))
			{
				txtExceptionType.Text = ex.GetType().ToString();
				txtExceptionMessage.Text = ex.Message;
				txtExceptionDetails.Text = exceptionHeirarchyToString(ex);
			}

			ShowDialog();
		}


		private static string exceptionHeirarchyToString(Exception ex)
		{
			var sb = new StringBuilder();
			var swWriter = new StringWriter(sb);

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
					swWriter.WriteLine("Inner Exception " + intCount);
				}
				swWriter.WriteLine("Type:        " + current.GetType());
				swWriter.WriteLine("Message:     " + current.Message);
				swWriter.WriteLine("Source:      " + current.Source);
				swWriter.WriteLine("Stack Trace: " + current.StackTrace.Trim());
				swWriter.WriteLine((String) null);

				current = current.InnerException;
				intCount++;
			}
			return sb.ToString();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}