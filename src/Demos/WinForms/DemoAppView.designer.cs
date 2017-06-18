namespace Demo.WinForms
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
			this.urlHideDetail = new System.Windows.Forms.LinkLabel();
			this.urlDefault = new System.Windows.Forms.LinkLabel();
			this.urlEmailTest = new System.Windows.Forms.LinkLabel();
			this.urlDialogless = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// urlDefault
			// 
			this.urlHideDetail.AutoSize = true;
			this.urlHideDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlHideDetail.Location = new System.Drawing.Point(12, 19);
			this.urlHideDetail.Name = "urlHideDetail";
			this.urlHideDetail.Size = new System.Drawing.Size(192, 16);
			this.urlHideDetail.TabIndex = 3;
			this.urlHideDetail.TabStop = true;
			this.urlHideDetail.Text = "Show More/Less Details Report";
			// 
			// urlConfigured
			// 
			this.urlDefault.AutoSize = true;
			this.urlDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlDefault.Location = new System.Drawing.Point(12, 54);
			this.urlDefault.Name = "urlDefault";
			this.urlDefault.Size = new System.Drawing.Size(215, 16);
			this.urlDefault.TabIndex = 4;
			this.urlDefault.TabStop = true;
			this.urlDefault.Text = "Show Default Exception Report";
			// 
			// urlCustomMessage
			// 
			this.urlEmailTest.AutoSize = true;
			this.urlEmailTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlEmailTest.Location = new System.Drawing.Point(12, 92);
			this.urlEmailTest.Name = "urlEmailTest";
			this.urlEmailTest.Size = new System.Drawing.Size(234, 16);
			this.urlEmailTest.TabIndex = 5;
			this.urlEmailTest.TabStop = true;
			this.urlEmailTest.Text = "Will Attach files to SMTP Email";
			// 
			// urlConfiguredMultiple
			// 
			this.urlDialogless.AutoSize = true;
			this.urlDialogless.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.urlDialogless.Location = new System.Drawing.Point(12, 128);
			this.urlDialogless.Name = "urlDialogless";
			this.urlDialogless.Size = new System.Drawing.Size(264, 16);
			this.urlDialogless.TabIndex = 6;
			this.urlDialogless.TabStop = true;
			this.urlDialogless.Text = "Invoke Dialogless Report";
			// 
			// DemoAppView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 170);
			this.Controls.Add(this.urlDialogless);
			this.Controls.Add(this.urlEmailTest);
			this.Controls.Add(this.urlDefault);
			this.Controls.Add(this.urlHideDetail);
			this.Name = "DemoAppView";
			this.Text = "Exception Reporter Demo";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel urlHideDetail;
        private System.Windows.Forms.LinkLabel urlDefault;
        private System.Windows.Forms.LinkLabel urlEmailTest;
        private System.Windows.Forms.LinkLabel urlDialogless;

    }
}