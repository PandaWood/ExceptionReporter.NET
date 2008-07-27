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
			this.btnShowExceptionReport = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.btnShowExceptionReport.FlatAppearance.BorderSize = 2;
			this.btnShowExceptionReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowExceptionReport.Location = new System.Drawing.Point(12, 12);
			this.btnShowExceptionReport.Name = "btnShowExceptionReport";
			this.btnShowExceptionReport.Size = new System.Drawing.Size(267, 55);
			this.btnShowExceptionReport.TabIndex = 1;
			this.btnShowExceptionReport.Text = "Show Exception Report";
			// 
			// DemoAppView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(291, 79);
			this.Controls.Add(this.btnShowExceptionReport);
			this.Name = "DemoAppView";
			this.Text = "ExceptionReportView";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnShowExceptionReport;

	}
}