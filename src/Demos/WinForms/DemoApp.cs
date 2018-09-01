using System;
using System.IO;
using System.Windows.Forms;
using ExceptionReporting;

namespace Demo.WinForms
{
	public partial class DemoApp : Form
	{
		public DemoApp()
		{
			InitializeComponent();

			urlDefault.Click += Show_Default_Report;
			urlSendEmail.Click += Send_Report;
			urlSilentReport.Click += Send_Silent_Report;
		}

		static void Show_Default_Report(object sender, EventArgs e)
		{
			ThrowAndShowExceptionReporter();
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

				//	ConfigureSmtpEmail(er.Config);
				ConfigureWebService(er.Config);		//toggle which type to configure
				er.Send(exception);
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
				// testing attaching files
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
						ConfigureWebService(exceptionReporter.Config);
			//	ConfigureSmtpEmail(exceptionReporter.Config);		// comment any of these in/out to test
			//	ConfigureSimpleMAPI(exceptionReporter.Config);

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
			config.SendMethod = ReportSendMethod.SMTP;		// obsolete deprecated property used here, will be removed in later version
			config.SmtpServer = "127.0.0.1";
			config.SmtpPort = 2500;
			config.SmtpUsername = "";
			config.SmtpPassword = "";
			config.SmtpFromAddress = "test@test.com";
			config.EmailReportAddress = "support@support.com";
			config.SmtpUseSsl = false;     // NB you'll need to have "Allow less secure apps: ON" if using Gmail for this
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
				var er = new ExceptionReporter();
				er.Config.SendMethod = ReportSendMethod.SimpleMAPI;
				er.Config.EmailReportAddress = "support@acme.com";
				er.Config.CompanyName = "Acme";   // this goes alongside email button text
				er.Config.TitleText = "Acme Error Report";
				
				// er.Config.ShowFullDetail = false;
				er.Config.ShowLessDetailButton = true;
				er.Config.ReportTemplateFormat = TemplateFormat.Text;
				// er.Config.ShowEmailButton = false;		// just for testing that removing email button works
				// er.Config.TemplateFormat = TemplateFormat.Markdown;
				
				er.Show(exception);
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
				"Unable to establish a connection with the Fizz photo service",
				new Exception(
					"This is an Inner Exception message - with a message that is not too small"));
			throw exception;
		}
		
		/// <summary>
		/// This is a demonstration of using a custom dialog/view - which implements IExceptionReportView
		/// And how the IViewReportMaker interface is used to set the custom view as the one to be instantiated
		/// </summary>
		void UseCustomReportView()
		{
			try
			{
				SomeMethodThatThrows();
			}
			catch (Exception exception)
			{
				var er = new ExceptionReporter
				{
					ViewMaker = new YourCustomViewMaker()
				};
				ConfigureWebService(er.Config);	
				er.Show(exception);
			}
		}

	}
}