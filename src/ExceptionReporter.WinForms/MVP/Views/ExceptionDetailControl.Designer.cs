namespace ExceptionReporting.MVP.Views
{
	internal partial class ExceptionDetailControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionDetailControl));
            this.label2 = new System.Windows.Forms.Label();
            this.txtExceptionTabStackTrace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExceptionTabMessage = new System.Windows.Forms.TextBox();
            this.listviewExceptions = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtExceptionTabStackTrace
            // 
            resources.ApplyResources(this.txtExceptionTabStackTrace, "txtExceptionTabStackTrace");
            this.txtExceptionTabStackTrace.BackColor = System.Drawing.SystemColors.Window;
            this.txtExceptionTabStackTrace.Name = "txtExceptionTabStackTrace";
            this.txtExceptionTabStackTrace.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtExceptionTabMessage
            // 
            resources.ApplyResources(this.txtExceptionTabMessage, "txtExceptionTabMessage");
            this.txtExceptionTabMessage.BackColor = System.Drawing.SystemColors.Window;
            this.txtExceptionTabMessage.Name = "txtExceptionTabMessage";
            this.txtExceptionTabMessage.ReadOnly = true;
            // 
            // listviewExceptions
            // 
            resources.ApplyResources(this.listviewExceptions, "listviewExceptions");
            this.listviewExceptions.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listviewExceptions.FullRowSelect = true;
            this.listviewExceptions.HotTracking = true;
            this.listviewExceptions.HoverSelection = true;
            this.listviewExceptions.Name = "listviewExceptions";
            this.listviewExceptions.UseCompatibleStateImageBehavior = false;
            this.listviewExceptions.View = System.Windows.Forms.View.Details;
            // 
            // ExceptionDetailControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExceptionTabStackTrace);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExceptionTabMessage);
            this.Controls.Add(this.listviewExceptions);
            this.Name = "ExceptionDetailControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExceptionTabStackTrace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExceptionTabMessage;
        private System.Windows.Forms.ListView listviewExceptions;
    }
}