namespace Demo.WinForms
{
	partial class DemoApp
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.urlDefault = new System.Windows.Forms.LinkLabel();
			this.urlSendEmail = new System.Windows.Forms.LinkLabel();
			this.urlSilentReport = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// urlDefault
			// 
			this.urlDefault.AutoSize = true;
			this.urlDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlDefault.Location = new System.Drawing.Point(12, 23);
			this.urlDefault.Name = "urlDefault";
			this.urlDefault.Size = new System.Drawing.Size(140, 24);
			this.urlDefault.TabIndex = 4;
			this.urlDefault.TabStop = true;
			this.urlDefault.Text = "Show - Defaults";
			// 
			// urlSendEmail
			// 
			this.urlSendEmail.AutoSize = true;
			this.urlSendEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlSendEmail.Location = new System.Drawing.Point(12, 67);
			this.urlSendEmail.Name = "urlSendEmail";
			this.urlSendEmail.Size = new System.Drawing.Size(237, 24);
			this.urlSendEmail.TabIndex = 5;
			this.urlSendEmail.TabStop = true;
			this.urlSendEmail.Text = "Show - Configured to Send";
			// 
			// urlSilentReport
			// 
			this.urlSilentReport.AutoSize = true;
			this.urlSilentReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlSilentReport.Location = new System.Drawing.Point(12, 111);
			this.urlSilentReport.Name = "urlSilentReport";
			this.urlSilentReport.Size = new System.Drawing.Size(261, 24);
			this.urlSilentReport.TabIndex = 6;
			this.urlSilentReport.TabStop = true;
			this.urlSilentReport.Text = "Send Report (no dialog show)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 169);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(249, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "see DemoAppView to modify config and experiment";
			// 
			// DemoAppView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(362, 191);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.urlSilentReport);
			this.Controls.Add(this.urlSendEmail);
			this.Controls.Add(this.urlDefault);
			this.Name = "DemoApp";
			this.Text = "Exception Reporter Demo";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.LinkLabel urlDefault;
		private System.Windows.Forms.LinkLabel urlSendEmail;
		private System.Windows.Forms.LinkLabel urlSilentReport;
		private System.Windows.Forms.Label label1;
	}
}