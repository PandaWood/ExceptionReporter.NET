// created on 15/03/2004 at 20:56
using System;
using System.Windows.Forms;

namespace MyUserControl {
	public class CreatedUserControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label;
		public CreatedUserControl()
		{
			InitializeComponent();
		}
		
		// THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
		// DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
		void InitializeComponent() {
			this.label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label
			// 
			this.label.Location = new System.Drawing.Point(40, 56);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(80, 40);
			this.label.TabIndex = 0;
			this.label.Text = "label";
			// 
			// CreatedUserControl
			// 
			this.Controls.Add(this.label);
			this.Name = "CreatedUserControl";
			this.ResumeLayout(false);
		}
	}
}
