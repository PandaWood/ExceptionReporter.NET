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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoAppView));
            this.urlHideDetail = new System.Windows.Forms.LinkLabel();
            this.urlDefault = new System.Windows.Forms.LinkLabel();
            this.urlEmailTest = new System.Windows.Forms.LinkLabel();
            this.urlDialogless = new System.Windows.Forms.LinkLabel();
            this.comboBoxLang = new System.Windows.Forms.ComboBox();
            this.buttonLang = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelSentWebEmail = new System.Windows.Forms.LinkLabel();
            this.linkLabelSentWebGithub = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // urlHideDetail
            // 
            resources.ApplyResources(this.urlHideDetail, "urlHideDetail");
            this.urlHideDetail.Name = "urlHideDetail";
            this.urlHideDetail.TabStop = true;
            // 
            // urlDefault
            // 
            resources.ApplyResources(this.urlDefault, "urlDefault");
            this.urlDefault.Name = "urlDefault";
            this.urlDefault.TabStop = true;
            // 
            // urlEmailTest
            // 
            resources.ApplyResources(this.urlEmailTest, "urlEmailTest");
            this.urlEmailTest.Name = "urlEmailTest";
            this.urlEmailTest.TabStop = true;
            // 
            // urlDialogless
            // 
            resources.ApplyResources(this.urlDialogless, "urlDialogless");
            this.urlDialogless.Name = "urlDialogless";
            this.urlDialogless.TabStop = true;
            // 
            // comboBoxLang
            // 
            this.comboBoxLang.FormattingEnabled = true;
            this.comboBoxLang.Items.AddRange(new object[] {
            resources.GetString("comboBoxLang.Items"),
            resources.GetString("comboBoxLang.Items1")});
            resources.ApplyResources(this.comboBoxLang, "comboBoxLang");
            this.comboBoxLang.Name = "comboBoxLang";
            // 
            // buttonLang
            // 
            resources.ApplyResources(this.buttonLang, "buttonLang");
            this.buttonLang.Name = "buttonLang";
            this.buttonLang.UseVisualStyleBackColor = true;
            this.buttonLang.Click += new System.EventHandler(this.buttonLang_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // linkLabelSentWebEmail
            // 
            resources.ApplyResources(this.linkLabelSentWebEmail, "linkLabelSentWebEmail");
            this.linkLabelSentWebEmail.Name = "linkLabelSentWebEmail";
            this.linkLabelSentWebEmail.TabStop = true;
            this.linkLabelSentWebEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSentWebEmail_LinkClicked);
            // 
            // linkLabelSentWebGithub
            // 
            resources.ApplyResources(this.linkLabelSentWebGithub, "linkLabelSentWebGithub");
            this.linkLabelSentWebGithub.Name = "linkLabelSentWebGithub";
            this.linkLabelSentWebGithub.TabStop = true;
            this.linkLabelSentWebGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSentWebGithub_LinkClicked);
            // 
            // DemoAppView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabelSentWebGithub);
            this.Controls.Add(this.linkLabelSentWebEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLang);
            this.Controls.Add(this.comboBoxLang);
            this.Controls.Add(this.urlDialogless);
            this.Controls.Add(this.urlEmailTest);
            this.Controls.Add(this.urlDefault);
            this.Controls.Add(this.urlHideDetail);
            this.Name = "DemoAppView";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel urlHideDetail;
		private System.Windows.Forms.LinkLabel urlDefault;
		private System.Windows.Forms.LinkLabel urlEmailTest;
		private System.Windows.Forms.LinkLabel urlDialogless;
	private System.Windows.Forms.ComboBox comboBoxLang;
	private System.Windows.Forms.Button buttonLang;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.LinkLabel linkLabelSentWebEmail;
	private System.Windows.Forms.LinkLabel linkLabelSentWebGithub;
  }
}