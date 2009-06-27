using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ExceptionReporting.WinForms.Views
{
	/// <summary>
	/// For exceptions within the ExceptionReporter - considering removing altogether
	/// </summary>
	public class InternalExceptionView : Form, IInternalExceptionView
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
			var resources = new ComponentResourceManager(typeof(InternalExceptionView));
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
			((ISupportInitialize)(pictureBox)).BeginInit();
			tpException.SuspendLayout();
			tpGeneral.SuspendLayout();
			tabControl.SuspendLayout();
			SuspendLayout();
			// 
			// pictureBox
			// 
			pictureBox.Image = ((Image)(resources.GetObject("pictureBox.Image")));
			pictureBox.Location = new Point(3, 3);
			pictureBox.Name = "pictureBox";
			pictureBox.Size = new Size(68, 86);
			pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
			pictureBox.TabIndex = 0;
			pictureBox.TabStop = false;
			// 
			// txtExceptionType
			// 
			txtExceptionType.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
			                               | AnchorStyles.Right;
			txtExceptionType.Location = new Point(77, 74);
			txtExceptionType.Name = "txtExceptionType";
			txtExceptionType.ReadOnly = true;
			txtExceptionType.Size = new Size(306, 20);
			txtExceptionType.TabIndex = 3;
			// 
			// txtExceptionDetails
			// 
			txtExceptionDetails.BorderStyle = BorderStyle.None;
			txtExceptionDetails.Dock = DockStyle.Fill;
			txtExceptionDetails.Location = new Point(0, 0);
			txtExceptionDetails.Multiline = true;
			txtExceptionDetails.Name = "txtExceptionDetails";
			txtExceptionDetails.ReadOnly = true;
			txtExceptionDetails.Size = new Size(386, 175);
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
			tpException.Size = new Size(386, 175);
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
			tpGeneral.Size = new Size(386, 175);
			tpGeneral.TabIndex = 0;
			tpGeneral.Text = "General";
			tpGeneral.ToolTipText = "Displays general information about the error that occurred";
			// 
			// txtExceptionMessage
			// 
			txtExceptionMessage.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
			                                   | AnchorStyles.Left)
			                                  | AnchorStyles.Right;
			txtExceptionMessage.Location = new Point(77, 100);
			txtExceptionMessage.Multiline = true;
			txtExceptionMessage.Name = "txtExceptionMessage";
			txtExceptionMessage.ReadOnly = true;
			txtExceptionMessage.Size = new Size(306, 71);
			txtExceptionMessage.TabIndex = 5;
			// 
			// lblExceptionMessage
			// 
			lblExceptionMessage.Location = new Point(15, 100);
			lblExceptionMessage.Name = "lblExceptionMessage";
			lblExceptionMessage.Size = new Size(56, 14);
			lblExceptionMessage.TabIndex = 4;
			lblExceptionMessage.Text = "Message";
			// 
			// lblGeneralMessage
			// 
			lblGeneralMessage.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
			                                | AnchorStyles.Right;
			lblGeneralMessage.Location = new Point(77, 7);
			lblGeneralMessage.Name = "lblGeneralMessage";
			lblGeneralMessage.Size = new Size(306, 60);
			lblGeneralMessage.TabIndex = 1;
			lblGeneralMessage.Text = "An internal exception has occurred within Exception Reporter";
			// 
			// tabControl
			// 
			tabControl.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
			                          | AnchorStyles.Left)
			                         | AnchorStyles.Right;
			tabControl.Controls.Add(tpGeneral);
			tabControl.Controls.Add(tpException);
			tabControl.HotTrack = true;
			tabControl.Location = new Point(8, 7);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.ShowToolTips = true;
			tabControl.Size = new Size(394, 201);
			tabControl.TabIndex = 0;
			// 
			// cmdOK
			// 
			cmdOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			cmdOK.ImageAlign = ContentAlignment.MiddleLeft;
			cmdOK.Location = new Point(320, 212);
			cmdOK.Name = "cmdOK";
			cmdOK.Size = new Size(80, 25);
			cmdOK.TabIndex = 7;
			cmdOK.Text = "OK";
			// 
			// InternalExceptionView
			// 
			AutoScaleBaseSize = new Size(5, 13);
			ClientSize = new Size(407, 241);
			Controls.Add(tabControl);
			Controls.Add(cmdOK);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "InternalExceptionView";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Exception Reporter: Problem Occurred";
			((ISupportInitialize)(pictureBox)).EndInit();
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
				txtExceptionDetails.Text = ExceptionHeirarchyToString(ex);
			}

			ShowDialog();
		}


		private static string ExceptionHeirarchyToString(Exception exception)
		{
			var stringBuilder = new StringBuilder();
		    using (var stringWriter = new StringWriter(stringBuilder))
		    {
		        var intCount = 0;
		        var current = exception;

		        while (current != null)
		        {
		            if (intCount == 0)
		            {
		                stringWriter.WriteLine("Top Level Exception");
		            }
		            else
		            {
		                stringWriter.WriteLine("Inner Exception " + intCount);
		            }
		            stringWriter.WriteLine("Type:        " + current.GetType());
		            stringWriter.WriteLine("Message:     " + current.Message);
		            stringWriter.WriteLine("Source:      " + current.Source);
		            stringWriter.WriteLine("Stack Trace: " + current.StackTrace.Trim());
		            stringWriter.WriteLine((String) null);

		            current = current.InnerException;
		            intCount++;
		        }
		    }
		    return stringBuilder.ToString();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
