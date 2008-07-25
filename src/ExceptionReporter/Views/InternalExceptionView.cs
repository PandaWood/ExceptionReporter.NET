using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ExceptionReporting.Views
{
	internal class InternalExceptionView : Form
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

		public InternalExceptionView()
		{
			InitializeComponent();

			WireUpEvents();
		}

		private void WireUpEvents()
		{
			cmdOK.Click += cmdOK_Click;
		}

		private void InitializeComponent()
		{
			pictureBox = new PictureBox();
			txtExceptionType = new TextBox();
			txtExceptionDetails = new TextBox();
			lblExceptionType = new Label();
			tpException = new TabPage();
			tpGeneral = new TabPage();
			txtExceptionMessage = new TextBox();
			lblExceptionMessage = new Label();
			lblGeneralMessage = new Label();
			tabControl = new TabControl();
			cmdOK = new Button();
			((ISupportInitialize) (pictureBox)).BeginInit();
			tpException.SuspendLayout();
			tpGeneral.SuspendLayout();
			tabControl.SuspendLayout();
			SuspendLayout();
			// 
			// pictureBox
			// 
			pictureBox.Location = new Point(0, 0);
			pictureBox.Name = "pictureBox";
			pictureBox.Size = new Size(58, 46);
			pictureBox.TabIndex = 0;
			pictureBox.TabStop = false;
			// 
			// txtExceptionType
			// 
			txtExceptionType.Location = new Point(56, 74);
			txtExceptionType.Name = "txtExceptionType";
			txtExceptionType.ReadOnly = true;
			txtExceptionType.Size = new Size(304, 20);
			txtExceptionType.TabIndex = 3;
			// 
			// txtExceptionDetails
			// 
			txtExceptionDetails.Location = new Point(8, 7);
			txtExceptionDetails.Multiline = true;
			txtExceptionDetails.Name = "txtExceptionDetails";
			txtExceptionDetails.ReadOnly = true;
			txtExceptionDetails.Size = new Size(352, 164);
			txtExceptionDetails.TabIndex = 6;
			// 
			// lblExceptionType
			// 
			lblExceptionType.Location = new Point(8, 74);
			lblExceptionType.Name = "lblExceptionType";
			lblExceptionType.Size = new Size(40, 15);
			lblExceptionType.TabIndex = 2;
			lblExceptionType.Text = "Type:";
			// 
			// tpException
			// 
			tpException.Controls.Add(txtExceptionDetails);
			tpException.Location = new Point(4, 22);
			tpException.Name = "tpException";
			tpException.Size = new Size(368, 175);
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
			tpGeneral.Size = new Size(368, 175);
			tpGeneral.TabIndex = 0;
			tpGeneral.Text = "General";
			tpGeneral.ToolTipText = "Displays general information about the error that occurred";
			// 
			// txtExceptionMessage
			// 
			txtExceptionMessage.Location = new Point(56, 97);
			txtExceptionMessage.Multiline = true;
			txtExceptionMessage.Name = "txtExceptionMessage";
			txtExceptionMessage.ReadOnly = true;
			txtExceptionMessage.Size = new Size(304, 74);
			txtExceptionMessage.TabIndex = 5;
			// 
			// lblExceptionMessage
			// 
			lblExceptionMessage.Location = new Point(8, 97);
			lblExceptionMessage.Name = "lblExceptionMessage";
			lblExceptionMessage.Size = new Size(56, 14);
			lblExceptionMessage.TabIndex = 4;
			lblExceptionMessage.Text = "Message:";
			// 
			// lblGeneralMessage
			// 
			lblGeneralMessage.Location = new Point(64, 7);
			lblGeneralMessage.Name = "lblGeneralMessage";
			lblGeneralMessage.Size = new Size(296, 60);
			lblGeneralMessage.TabIndex = 1;
			lblGeneralMessage.Text = "An error has occurred within the Exception Reporter";
			// 
			// tabControl
			// 
			tabControl.Controls.Add(tpGeneral);
			tabControl.Controls.Add(tpException);
			tabControl.HotTrack = true;
			tabControl.Location = new Point(8, 7);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.ShowToolTips = true;
			tabControl.Size = new Size(376, 201);
			tabControl.TabIndex = 0;
			// 
			// cmdOK
			// 
			cmdOK.ImageAlign = ContentAlignment.MiddleLeft;
			cmdOK.Location = new Point(308, 214);
			cmdOK.Name = "cmdOK";
			cmdOK.Size = new Size(72, 23);
			cmdOK.TabIndex = 7;
			cmdOK.Text = "OK";
			// 
			// SimpleExceptionView
			// 
			AutoScaleBaseSize = new Size(5, 13);
			ClientSize = new Size(386, 241);
			Controls.Add(tabControl);
			Controls.Add(cmdOK);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "SimpleExceptionView";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Exception Reporter: Problem Occurred";
			((ISupportInitialize) (pictureBox)).EndInit();
			tpException.ResumeLayout(false);
			tpException.PerformLayout();
			tpGeneral.ResumeLayout(false);
			tpGeneral.PerformLayout();
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