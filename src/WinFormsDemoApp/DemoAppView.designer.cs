namespace WinFormsDemoApp
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
            this.urlDefault = new System.Windows.Forms.LinkLabel();
            this.urlConfigured = new System.Windows.Forms.LinkLabel();
            this.urlCustomMessage = new System.Windows.Forms.LinkLabel();
            this.urlConfiguredMultiple = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // urlDefault
            // 
            this.urlDefault.AutoSize = true;
            this.urlDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlDefault.Location = new System.Drawing.Point(12, 19);
            this.urlDefault.Name = "urlDefault";
            this.urlDefault.Size = new System.Drawing.Size(192, 16);
            this.urlDefault.TabIndex = 3;
            this.urlDefault.TabStop = true;
            this.urlDefault.Text = "Show Default Exception Report";
            // 
            // urlConfigured
            // 
            this.urlConfigured.AutoSize = true;
            this.urlConfigured.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlConfigured.Location = new System.Drawing.Point(12, 54);
            this.urlConfigured.Name = "urlConfigured";
            this.urlConfigured.Size = new System.Drawing.Size(215, 16);
            this.urlConfigured.TabIndex = 4;
            this.urlConfigured.TabStop = true;
            this.urlConfigured.Text = "Show Configured Exception Report";
            // 
            // urlCustomMessage
            // 
            this.urlCustomMessage.AutoSize = true;
            this.urlCustomMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlCustomMessage.Location = new System.Drawing.Point(12, 92);
            this.urlCustomMessage.Name = "urlCustomMessage";
            this.urlCustomMessage.Size = new System.Drawing.Size(149, 16);
            this.urlCustomMessage.TabIndex = 5;
            this.urlCustomMessage.TabStop = true;
            this.urlCustomMessage.Text = "Show Custom Message";
            // 
            // urlConfiguredMultiple
            // 
            this.urlConfiguredMultiple.AutoSize = true;
            this.urlConfiguredMultiple.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlConfiguredMultiple.Location = new System.Drawing.Point(12, 128);
            this.urlConfiguredMultiple.Name = "urlConfiguredMultiple";
            this.urlConfiguredMultiple.Size = new System.Drawing.Size(264, 16);
            this.urlConfiguredMultiple.TabIndex = 6;
            this.urlConfiguredMultiple.TabStop = true;
            this.urlConfiguredMultiple.Text = "Show Configured Multiple Exception Report";
            // 
            // DemoAppView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 170);
            this.Controls.Add(this.urlConfiguredMultiple);
            this.Controls.Add(this.urlCustomMessage);
            this.Controls.Add(this.urlConfigured);
            this.Controls.Add(this.urlDefault);
            this.Name = "DemoAppView";
            this.Text = "Exception Reporter Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel urlDefault;
        private System.Windows.Forms.LinkLabel urlConfigured;
        private System.Windows.Forms.LinkLabel urlCustomMessage;
        private System.Windows.Forms.LinkLabel urlConfiguredMultiple;

    }
}