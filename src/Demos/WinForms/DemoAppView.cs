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

			urlDefault.Click += Show_Default_Report;
			urlHideDetail.Click += Show_HideDetailView_Click;
			urlEmailTest.Click += Show_Email_Attachment_Test;
			urlDialogless.Click += Do_Dialogless_Report;
		}

		static void Show_Default_Report(object sender, EventArgs e)
		{
			ThrowAndShowExceptionReporter();
		}

		static void Show_HideDetailView_Click(object sender, EventArgs e)
		{
			ThrowAndShowExceptionReporter(detailView:true);
		}

		void Do_Dialogless_Report(object sender, EventArgs e)
		{
			try
			{
				SomeMethod();
			}
			catch (Exception exception)
			{
				var config = new ExceptionReportInfo
				{
					MainException = exception
				};

				ConfigureSmtpEmail(config);
				var exceptionReportGenerator = new ExceptionReportGenerator(config);
				exceptionReportGenerator.SendReportByEmail();
			}
		}

		private void Show_Email_Attachment_Test(object sender, EventArgs e)
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

				var exceptionReporter = CreateEmailReadyReporter();

				exceptionReporter.Config.FilesToAttach = new[] { file1, file2 };
				exceptionReporter.Config.TakeScreenshot = true;

				exceptionReporter.Show("temp files will be attached to the email sent", exception);
			}
		}

		ExceptionReporter CreateEmailReadyReporter() 
		{
			var exceptionReporter = new ExceptionReporter();
//			ConfigureSmtpEmail(exceptionReporter.Config);		// comment one in/out to test SMPT or WebService
			ConfigureWebService(exceptionReporter.Config);
			return exceptionReporter;
		}

		void ConfigureWebService(ExceptionReportInfo config)
		{
			config.MailMethod = ExceptionReportInfo.EmailMethod.WebService;
			config.WebServiceUrl = "http://localhost:24513/api/er";
		}

		void ConfigureSmtpEmail(ExceptionReportInfo config) 
		{
			//--- Test SMTP - recommend using MailSlurper https://github.com/mailslurper
			config.MailMethod = ExceptionReportInfo.EmailMethod.SMTP;
			//config.SmtpServer = "10.0.2.2";
			config.SmtpServer = "127.0.0.1";
			config.SmtpPort = 2500;
			config.SmtpUsername = "";
			config.SmtpPassword = "";
			config.SmtpFromAddress = "test@test.com";
			config.EmailReportAddress = "support@support.com";
			config.SmtpUseSsl = false;     // NB you'll need to have "Allow less secure apps: ON" if using gmail for this
			//---
		}

		static void ThrowAndShowExceptionReporter(bool detailView = false) 
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
//					exceptionReporter.Config.ShowEmailButton = false;		// just for testing that removing email button works well positioning etc
				}
				exceptionReporter.Show(exception);
			}
		}

		static void SomeMethod()
		{
			CallAnotherMethod();
		}

		static void CallAnotherMethod()
		{
			AndAnotherOne();
		}

		static void AndAnotherOne()
		{
			var exception = new IOException(
				"Unable to establish a connection with the Foo bank account service: " + Guid.NewGuid().ToString(),
				new Exception(
					"This is an Inner Exception message - with a message that is not too small but perhaps it should be smaller"));
			throw exception;
		}
	}
}