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
			pictureBox = new PictureBox();
			txtExceptionType = new TextBox();
			txtExceptionDetails = new TextBox();
			lblExceptionType = new Label();
			tpException = new TabPage();
			tpGeneral = new TabPage();
			lblGeneralMessage = new Label();
			lblExceptionMessage = new Label();
			tabControl = new TabControl();
			txtExceptionMessage = new TextBox();
			cmdOK = new Button();
			tpException.SuspendLayout();
			tpGeneral.SuspendLayout();
			tabControl.SuspendLayout();
			SuspendLayout();
			// 
			// pictureBox
			// 
			pictureBox.Location = new Point(8, 8);
			pictureBox.Name = "pictureBox";
			pictureBox.Size = new Size(48, 64);
			pictureBox.TabIndex = 5;
			pictureBox.TabStop = false;
			// 
			// txtExceptionType
			// 
			txtExceptionType.Location = new Point(56, 80);
			txtExceptionType.Name = "txtExceptionType";
			txtExceptionType.ReadOnly = true;
			txtExceptionType.Size = new Size(304, 21);
			txtExceptionType.TabIndex = 3;
			txtExceptionType.Text = "";
			// 
			// txtExceptionDetails
			// 
			txtExceptionDetails.Location = new Point(8, 8);
			txtExceptionDetails.Multiline = true;
			txtExceptionDetails.Name = "txtExceptionDetails";
			txtExceptionDetails.ReadOnly = true;
			txtExceptionDetails.Size = new Size(352, 176);
			txtExceptionDetails.TabIndex = 6;
			txtExceptionDetails.Text = "";
			// 
			// lblExceptionType
			// 
			lblExceptionType.Location = new Point(8, 80);
			lblExceptionType.Name = "lblExceptionType";
			lblExceptionType.Size = new Size(40, 16);
			lblExceptionType.TabIndex = 2;
			lblExceptionType.Text = "Type:";
			// 
			// tpException
			// 
			tpException.Controls.Add(txtExceptionDetails);
			tpException.Location = new Point(4, 22);
			tpException.Name = "tpException";
			tpException.Size = new Size(368, 190);
			tpException.TabIndex = 1;
			tpException.Text = "Exception";
			tpException.ToolTipText = "Displays detailed information about the error that occurred";
			// 
			// tpGeneral
			// 
			tpGeneral.Controls.Add(pictureBox);
			tpGeneral.Controls.Add(txtExceptionMessage);
			tpGeneral.Controls.Add(txtExceptionType);
			tpGeneral.Controls.Add(lblExceptionType);
			tpGeneral.Controls.Add(lblExceptionMessage);
			tpGeneral.Controls.Add(lblGeneralMessage);
			tpGeneral.Location = new Point(4, 22);
			tpGeneral.Name = "tpGeneral";
			tpGeneral.Size = new Size(368, 190);
			tpGeneral.TabIndex = 0;
			tpGeneral.Text = "General";
			tpGeneral.ToolTipText = "Displays general information about the error that occurred";
			// 
			// lblGeneralMessage
			// 
			lblGeneralMessage.Location = new Point(64, 8);
			lblGeneralMessage.Name = "lblGeneralMessage";
			lblGeneralMessage.Size = new Size(296, 64);
			lblGeneralMessage.TabIndex = 1;
			lblGeneralMessage.Text = "An error has occurred within the Exception Reporter";
			// 
			// lblExceptionMessage
			// 
			lblExceptionMessage.Location = new Point(8, 104);
			lblExceptionMessage.Name = "lblExceptionMessage";
			lblExceptionMessage.Size = new Size(56, 16);
			lblExceptionMessage.TabIndex = 4;
			lblExceptionMessage.Text = "Message:";
			// 
			// tabControl
			// 
			tabControl.Controls.Add(tpGeneral);
			tabControl.Controls.Add(tpException);
			tabControl.HotTrack = true;
			tabControl.Location = new Point(8, 8);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.ShowToolTips = true;
			tabControl.Size = new Size(376, 216);
			tabControl.TabIndex = 0;
			// 
			// txtExceptionMessage
			// 
			txtExceptionMessage.Location = new Point(56, 104);
			txtExceptionMessage.Multiline = true;
			txtExceptionMessage.Name = "txtExceptionMessage";
			txtExceptionMessage.ReadOnly = true;
			txtExceptionMessage.Size = new Size(304, 80);
			txtExceptionMessage.TabIndex = 5;
			txtExceptionMessage.Text = "";
			// 
			// cmdOK
			// 
			cmdOK.ImageAlign = ContentAlignment.MiddleLeft;
			cmdOK.Location = new Point(296, 232);
			cmdOK.Name = "cmdOK";
			cmdOK.Size = new Size(72, 24);
			cmdOK.TabIndex = 7;
			cmdOK.Text = "&OK";
			cmdOK.Click += cmdOK_Click;
			// 
			// SimpleExceptionView
			// 
			AutoScaleBaseSize = new Size(5, 14);
			ClientSize = new Size(386, 262);
			Controls.Add(tabControl);
			Controls.Add(cmdOK);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "SimpleExceptionView";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Exception Reporter: Problem Occurred";
			tpException.ResumeLayout(false);
			tpGeneral.ResumeLayout(false);
			tabControl.ResumeLayout(false);
			ResumeLayout(false);
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