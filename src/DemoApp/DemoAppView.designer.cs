namespace ExceptionReporting.DemoApp
{
	partial class DemoAppView
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
			this.btnShowConfiguredExceptionReport = new System.Windows.Forms.Button();
			this.btnShowNonConfiguredExceptionReport = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnShowConfiguredExceptionReport
			// 
			this.btnShowConfiguredExceptionReport.FlatAppearance.BorderSize = 2;
			this.btnShowConfiguredExceptionReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnShowConfiguredExceptionReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShowConfiguredExceptionReport.Location = new System.Drawing.Point(12, 27);
			this.btnShowConfiguredExceptionReport.Name = "btnShowConfiguredExceptionReport";
			this.btnShowConfiguredExceptionReport.Size = new System.Drawing.Size(249, 30);
			this.btnShowConfiguredExceptionReport.TabIndex = 1;
			this.btnShowConfiguredExceptionReport.Text = "Show Configured Exception Report";
			// 
			// btnShowNonConfiguredExceptionReport
			// 
			this.btnShowNonConfiguredExceptionReport.FlatAppearance.BorderSize = 2;
			this.btnShowNonConfiguredExceptionReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnShowNonConfiguredExceptionReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShowNonConfiguredExceptionReport.Location = new System.Drawing.Point(12, 73);
			this.btnShowNonConfiguredExceptionReport.Name = "btnShowNonConfiguredExceptionReport";
			this.btnShowNonConfiguredExceptionReport.Size = new System.Drawing.Size(249, 30);
			this.btnShowNonConfiguredExceptionReport.TabIndex = 2;
			this.btnShowNonConfiguredExceptionReport.Text = "Show Non Configured Exception Report";
			// 
			// DemoAppView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 132);
			this.Controls.Add(this.btnShowNonConfiguredExceptionReport);
			this.Controls.Add(this.btnShowConfiguredExceptionReport);
			this.Name = "DemoAppView";
			this.Text = "Exception Reporter Demo";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnShowConfiguredExceptionReport;
		private System.Windows.Forms.Button btnShowNonConfiguredExceptionReport;

	}
}