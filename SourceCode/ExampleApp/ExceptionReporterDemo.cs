// project created on 15/03/2004 at 20:15
using System;
using System.Windows.Forms;
using SLSExceptionReporter;

//-------------------------------------------------------------------------
// ExceptionReporter - Error Reporting Component for .Net
//
// Copyright (C) 2004  Phillip Pettit / Stratalogic Software
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//--------------------------------------------------------------------------


namespace Demo 
{
	class ExceptionReporterDemo: System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblExample3;
		private System.Windows.Forms.Label lblExample1;
		private System.Windows.Forms.Label lblExample2;
		private System.Windows.Forms.GroupBox grbExample2;
		private System.Windows.Forms.GroupBox grbExample3;
		private System.Windows.Forms.GroupBox grbExample1;
		private System.Windows.Forms.Button cmdExample3;
		private System.Windows.Forms.Button cmdExample2;
		private System.Windows.Forms.PictureBox picLogo;
		private System.Windows.Forms.Button cmdExample1;
		private SLSExceptionReporter.ExceptionReporter exceptionReporter;
		
		public ExceptionReporterDemo()
		{
			InitializeComponent();
		}
	
		// THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
		// DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
		void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExceptionReporterDemo));
			this.cmdExample1 = new System.Windows.Forms.Button();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.cmdExample2 = new System.Windows.Forms.Button();
			this.cmdExample3 = new System.Windows.Forms.Button();
			this.grbExample1 = new System.Windows.Forms.GroupBox();
			this.grbExample3 = new System.Windows.Forms.GroupBox();
			this.grbExample2 = new System.Windows.Forms.GroupBox();
			this.lblExample2 = new System.Windows.Forms.Label();
			this.lblExample1 = new System.Windows.Forms.Label();
			this.lblExample3 = new System.Windows.Forms.Label();
			this.grbExample1.SuspendLayout();
			this.grbExample3.SuspendLayout();
			this.grbExample2.SuspendLayout();
			this.SuspendLayout();
			//
			// exceptionReporter
			//
			exceptionReporter = new SLSExceptionReporter.ExceptionReporter();
			exceptionReporter.ContactEmail = "testEmail@testEmail.com";
			exceptionReporter.ContactWeb = "www.zzztestweb.com";
			exceptionReporter.ContactPhone = "7899 9877 7988";
			exceptionReporter.ContactFax = "7899 9877 7989";
			
			
			
			//
			// cmdExample1
			// 
			this.cmdExample1.Location = new System.Drawing.Point(16, 32);
			this.cmdExample1.Name = "cmdExample1";
			this.cmdExample1.Size = new System.Drawing.Size(88, 24);
			this.cmdExample1.TabIndex = 1;
			this.cmdExample1.Text = "Example 1";
			this.cmdExample1.Click += new System.EventHandler(this.Example1ButtonClick);
			// 
			// picLogo
			// 
			this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
			this.picLogo.Location = new System.Drawing.Point(168, 8);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(168, 80);
			this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picLogo.TabIndex = 8;
			this.picLogo.TabStop = false;
			// 
			// cmdExample2
			// 
			this.cmdExample2.Location = new System.Drawing.Point(16, 32);
			this.cmdExample2.Name = "cmdExample2";
			this.cmdExample2.Size = new System.Drawing.Size(88, 24);
			this.cmdExample2.TabIndex = 3;
			this.cmdExample2.Text = "Example 2";
			this.cmdExample2.Click += new System.EventHandler(this.Example2ButtonClick);
			// 
			// cmdExample3
			// 
			this.cmdExample3.Location = new System.Drawing.Point(16, 24);
			this.cmdExample3.Name = "cmdExample3";
			this.cmdExample3.Size = new System.Drawing.Size(88, 24);
			this.cmdExample3.TabIndex = 5;
			this.cmdExample3.Text = "Example 3";
			this.cmdExample3.Click += new System.EventHandler(this.Example3ButtonClick);
			// 
			// grbExample1
			// 
			this.grbExample1.Controls.Add(this.lblExample1);
			this.grbExample1.Controls.Add(this.cmdExample1);
			this.grbExample1.Location = new System.Drawing.Point(8, 88);
			this.grbExample1.Name = "grbExample1";
			this.grbExample1.Size = new System.Drawing.Size(496, 80);
			this.grbExample1.TabIndex = 0;
			this.grbExample1.TabStop = false;
			this.grbExample1.Text = "Example 1";
			// 
			// grbExample3
			// 
			this.grbExample3.Controls.Add(this.lblExample3);
			this.grbExample3.Controls.Add(this.cmdExample3);
			this.grbExample3.Location = new System.Drawing.Point(8, 256);
			this.grbExample3.Name = "grbExample3";
			this.grbExample3.Size = new System.Drawing.Size(496, 88);
			this.grbExample3.TabIndex = 4;
			this.grbExample3.TabStop = false;
			this.grbExample3.Text = "Example 3";
			// 
			// grbExample2
			// 
			this.grbExample2.Controls.Add(this.cmdExample2);
			this.grbExample2.Controls.Add(this.lblExample2);
			this.grbExample2.Location = new System.Drawing.Point(8, 168);
			this.grbExample2.Name = "grbExample2";
			this.grbExample2.Size = new System.Drawing.Size(496, 80);
			this.grbExample2.TabIndex = 2;
			this.grbExample2.TabStop = false;
			this.grbExample2.Text = "Example 2";
			// 
			// lblExample2
			// 
			this.lblExample2.Location = new System.Drawing.Point(120, 16);
			this.lblExample2.Name = "lblExample2";
			this.lblExample2.Size = new System.Drawing.Size(368, 56);
			this.lblExample2.TabIndex = 4;
			this.lblExample2.Text = "This example demonstrates the use of an Exception Reporter component created at r" +
"un-time and configured by loading property settings from ExceptionReporterDemo.c" +
"onfig.  Refer to ExceptionReporterDemo.cs to examine the code.";
			// 
			// lblExample1
			// 
			this.lblExample1.Location = new System.Drawing.Point(120, 24);
			this.lblExample1.Name = "lblExample1";
			this.lblExample1.Size = new System.Drawing.Size(368, 48);
			this.lblExample1.TabIndex = 3;
			this.lblExample1.Text = "This example demonstrates the use of an Exception Reporter component placed and c" +
"onfigured on a form at design time.  Refer to ExceptionReporterDemo.cs to examin" +
"e the code.";
			// 
			// lblExample3
			// 
			this.lblExample3.Location = new System.Drawing.Point(120, 12);
			this.lblExample3.Name = "lblExample3";
			this.lblExample3.Size = new System.Drawing.Size(360, 72);
			this.lblExample3.TabIndex = 5;
			this.lblExample3.Text = @"This example also demonstrates the use of an Exception Reporter component created at run-time.  The communication with the Exception Reporter is handled by a static method in a seperate class.  Refer to ExceptionReporterDemo.cs AND HandleException.cs to examine the code.";
			// 
			// ExceptionReporterDemo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 358);
			this.Controls.Add(this.picLogo);
			this.Controls.Add(this.grbExample3);
			this.Controls.Add(this.grbExample2);
			this.Controls.Add(this.grbExample1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExceptionReporterDemo";
			this.Text = "Exception Reporter Example";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.grbExample1.ResumeLayout(false);
			this.grbExample3.ResumeLayout(false);
			this.grbExample2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
			
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new ExceptionReporterDemo());
		}
		void MainFormLoad(object sender, System.EventArgs e)
		{
			
			this.Show();
		}
		
		void Example1ButtonClick(object sender, System.EventArgs e)
		{
			// setup a situation where we force a simple exception
			int j = 0;
			try {
				int i = 5/j;
			} catch (Exception ex) {
				// use the already existing object
				// it's properties have already been set in the designer
				exceptionReporter.DisplayException(ex);
			}
		}
		
		void Example2ButtonClick(object sender, System.EventArgs e)
		{
			try {
				// call a sub that will cause an exception
				someSubWithError();
			} catch (Exception ex) {
				// Create an Exception Reporter at run-time
				ExceptionReporter er = new ExceptionReporter();
				// load properties from the applications config file
				er.LoadPropertiesFromConfig();
				// Show the exception details through use of the Exception Reporter
				er.DisplayException(ex);
			}
		}
		
		void Example3ButtonClick(object sender, System.EventArgs e)
		{
			try {
				// call a sub that will cause an exception
				someSubWithError();
			} catch (Exception ex) {
				// Call a static method of another class to handle the exception
				// the static method will make use of an Exception Reporter object
				HandleError.HandleAnError(ex);
			}
		}
		
		private void someSubWithError(){
			
			try {
				innerSubWithError();
			} catch (Exception ex) {
				throw new ApplicationException("Problem in someSub",ex);
			}
			
			
		}

		private void innerSubWithError(){
			int j = 0;
			try {
				int k = 5/j;
			} catch (Exception ex) {
				throw new ApplicationException("Problem in InnerSub",ex);
			}
		}
		
	}			
}
