using System;
using System.IO;
using System.Windows.Forms;
using ExceptionReporting;

namespace Demo.WinForms
{
	public partial class DemoAppView : Form
	{
		public DemoAppView()
		{
			InitializeComponent();

			urlDefault.Click += Show_Default_Report;
			urlHideDetail.Click += Show_HideDetailView_Click;
			urlSendEmail.Click += Send_Report;
			urlSilentReport.Click += Send_Silent_Report;
		}

		static void Show_Default_Report(object sender, EventArgs e)
		{
			ThrowAndShowExceptionReporter();
		}

		static void Show_HideDetailView_Click(object sender, EventArgs e)
		{
			ThrowAndShowExceptionReporter(detailView:true);
		}

		void Send_Silent_Report(object sender, EventArgs e)
		{
			try
			{
				SomeMethodThatThrows();
			}
			catch (Exception exception)
			{
				var er = new ExceptionReporter();

//				ConfigureSmtpEmail(er.Config);
				ConfigureWebService(er.Config);		//toggle which type to configure
				er.Send(exception);

				// don't really need ExceptionReportGenerator (as used below) because the ExceptionReporter Send()
				// method (above) wraps it
				// var exceptionReportGenerator = new ExceptionReportGenerator(er.Config);
				// exceptionReportGenerator.SendReportByEmail();
			}
		}

		private void Send_Report(object sender, EventArgs e)
		{
			try
			{
				SomeMethodThatThrows();
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

				exceptionReporter.Show(exception);
			}
		}

		ExceptionReporter CreateEmailReadyReporter() 
		{
			var exceptionReporter = new ExceptionReporter();
			//			ConfigureWebService(exceptionReporter.Config);
			//			ConfigureSmtpEmail(exceptionReporter.Config);		// comment any of these in/out to test
			ConfigureSimpleMAPI(exceptionReporter.Config);

			return exceptionReporter;
		}

		void ConfigureSimpleMAPI(ExceptionReportInfo config)
		{
			config.EmailReportAddress = "demo@exceptionreporter.com";
			config.SendMethod = ReportSendMethod.SimpleMAPI;
		}

		void ConfigureWebService(ExceptionReportInfo config)
		{
			config.SendMethod = ReportSendMethod.WebService;
			config.WebServiceUrl = "http://localhost:24513/api/er";
		}

		void ConfigureSmtpEmail(ExceptionReportInfo config) 
		{
			//--- Test SMTP - recommend using MailSlurper https://github.com/mailslurper
			config.MailMethod = ExceptionReportInfo.EmailMethod.SMTP;		// obsolete deprecated property used here, will be removed in later version
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
				SomeMethodThatThrows();
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

		static void SomeMethodThatThrows()
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