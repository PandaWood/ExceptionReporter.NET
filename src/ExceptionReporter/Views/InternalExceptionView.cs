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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InternalExceptionView));
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
			this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
			this.pictureBox.Location = new System.Drawing.Point(3, 3);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(68, 86);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// txtExceptionType
			// 
			this.txtExceptionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtExceptionType.Location = new System.Drawing.Point(77, 74);
			this.txtExceptionType.Name = "txtExceptionType";
			this.txtExceptionType.ReadOnly = true;
			this.txtExceptionType.Size = new System.Drawing.Size(306, 20);
			this.txtExceptionType.TabIndex = 3;
			// 
			// txtExceptionDetails
			// 
			this.txtExceptionDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtExceptionDetails.Location = new System.Drawing.Point(0, 0);
			this.txtExceptionDetails.Multiline = true;
			this.txtExceptionDetails.Name = "txtExceptionDetails";
			this.txtExceptionDetails.ReadOnly = true;
			this.txtExceptionDetails.Size = new System.Drawing.Size(386, 175);
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
			this.tpException.Size = new System.Drawing.Size(386, 175);
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
			this.tpGeneral.Size = new System.Drawing.Size(386, 175);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = "General";
			this.tpGeneral.ToolTipText = "Displays general information about the error that occurred";
			// 
			// txtExceptionMessage
			// 
			this.txtExceptionMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtExceptionMessage.Location = new System.Drawing.Point(77, 100);
			this.txtExceptionMessage.Multiline = true;
			this.txtExceptionMessage.Name = "txtExceptionMessage";
			this.txtExceptionMessage.ReadOnly = true;
			this.txtExceptionMessage.Size = new System.Drawing.Size(306, 71);
			this.txtExceptionMessage.TabIndex = 5;
			// 
			// lblExceptionMessage
			// 
			this.lblExceptionMessage.Location = new System.Drawing.Point(15, 100);
			this.lblExceptionMessage.Name = "lblExceptionMessage";
			this.lblExceptionMessage.Size = new System.Drawing.Size(56, 14);
			this.lblExceptionMessage.TabIndex = 4;
			this.lblExceptionMessage.Text = "Message";
			// 
			// lblGeneralMessage
			// 
			this.lblGeneralMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblGeneralMessage.Location = new System.Drawing.Point(77, 7);
			this.lblGeneralMessage.Name = "lblGeneralMessage";
			this.lblGeneralMessage.Size = new System.Drawing.Size(306, 60);
			this.lblGeneralMessage.TabIndex = 1;
			this.lblGeneralMessage.Text = "An internal exception has occurred within Exception Reporter";
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tpGeneral);
			this.tabControl.Controls.Add(this.tpException);
			this.tabControl.HotTrack = true;
			this.tabControl.Location = new System.Drawing.Point(8, 7);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.ShowToolTips = true;
			this.tabControl.Size = new System.Drawing.Size(394, 201);
			this.tabControl.TabIndex = 0;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdOK.Location = new System.Drawing.Point(320, 212);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(80, 25);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "OK";
			// 
			// InternalExceptionView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(407, 241);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.cmdOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InternalExceptionView";
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