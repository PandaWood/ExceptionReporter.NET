using System;
using System.IO;
using System.Windows.Forms;
using ExceptionReporting;
using ExceptionReporting.Core;

namespace Demo.WinForms
{
	public partial class DemoAppView : Form
	{
		public DemoAppView()
		{
			InitializeComponent();

			urlConfigured.Click += Show_Default_Click;
			urlDefault.Click += Show_HideDetailView_Click;
			urlCustomMessage.Click += Throw_Email_Attachment_Test;
			urlConfiguredMultiple.Click += Throw_MultipleExceptions_Click;
		}

		private static void Show_Default_Click(object sender, EventArgs e)
		{
			ThrowAndShowExceptionReporter();
		}

		private static void Show_HideDetailView_Click(object sender, EventArgs e)
		{
			ThrowAndShowExceptionReporter(detailView:true);
		}

		private static void Throw_MultipleExceptions_Click(object sender, EventArgs e)
		{
			ShowMultipleExceptionReporter();
		}

		private static void Throw_Email_Attachment_Test(object sender, EventArgs e)
		{
			try
			{
				SomeMethod();
			}
			catch (Exception exception)
			{
				var file1 = Path.GetTempFileName() + "-file1.txt";	
				var file2 = Path.GetTempFileName() + "-file2.txt";

				File.WriteAllText(file1, "test text file 1");
				File.WriteAllText(file2, "test text file 2");

				var exceptionReporter = new ExceptionReporter();
				var config = exceptionReporter.Config;

				config.ShowAssembliesTab = false;
				config.FilesToAttach = new[] { file1, file2 };
				config.TakeScreenshot = true;

				//--- Test SMTP - recommend using MailSlurper https://github.com/mailslurper
				config.MailMethod = ExceptionReportInfo.EmailMethod.SMTP;
				config.SmtpServer= "10.0.2.2";
				config.SmtpPort = 2500;
				config.SmtpUsername = "";
				config.SmtpPassword = "";
				config.SmtpFromAddress = "test@test.com";
				config.EmailReportAddress = "support@support.com";
				config.SmtpUseSsl = false;     // NB you'll need to have "Allow less secure apps: ON" if using gmail for this
				//----

				exceptionReporter.Show("temp files will be attached to the email sent", exception);
			}
		}


		private static void ShowMultipleExceptionReporter()
		{
			Exception exception1 = null;
			Exception exception2 = null;
			try
			{
				SomeMethod();
			}
			catch (Exception exception)
			{
				exception1 = exception;
			}
			try
			{
				CallAnotherMethod();
			}
			catch (Exception exception)
			{
				exception2 = exception;
			}
			var exceptionReporter = new ExceptionReporter();

			exceptionReporter.Show(exception1, exception2);
		}

		private static void ThrowAndShowExceptionReporter(bool detailView = false) 
		{
			try
			{
				SomeMethod();
			}
			catch (Exception exception)
			{
				var exceptionReporter = new ExceptionReporter();

				if (detailView)
				{
					exceptionReporter.Config.ShowFullDetail = false;
					exceptionReporter.Config.ShowLessMoreDetailButton = true;
				}
				exceptionReporter.Show(exception);
			}
		}

		private static void SomeMethod()
		{
			CallAnotherMethod();
		}

		private static void CallAnotherMethod()
		{
			AndAnotherOne();
		}

		private static void AndAnotherOne()
		{
			var exception = new IOException(
				"Unable to establish a connection with the Foo bank account service. The error number is #FFF474678.",
				new Exception(
					"This is an Inner Exception message - with a message that is not too small but perhaps it should be smaller"));
			throw exception;
		}
	}
}