using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using ExceptionReporting.Core;
using ExceptionReporting.Mail;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.Views
{
	/// <summary>
	/// The Presenter in this MVP (Model-View-Presenter) implementation 
	/// </summary>
	public class ExceptionReportPresenter
	{
		readonly IExceptionReportView _view;
		readonly ExceptionReportGenerator _reportGenerator;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		/// <param name="info"></param>
		public ExceptionReportPresenter(IExceptionReportView view, ExceptionReportInfo info)
		{
			_view = view;
			ReportInfo = info;
			_reportGenerator = new ExceptionReportGenerator(info);
		}

		/// <summary>
		/// The application assembly - ie the main application using the exception reporter assembly
		/// </summary>
		public Assembly AppAssembly
		{
			get { return ReportInfo.AppAssembly; }
		}

		/// <summary>
		/// 
		/// </summary>
		public ExceptionReportInfo ReportInfo { get; private set; }

		private WinFormsClipboard _clipboard = new WinFormsClipboard();

		private ExceptionReport CreateExceptionReport()
		{
			ReportInfo.UserExplanation = _view.UserExplanation;
			return _reportGenerator.CreateExceptionReport();
		}

		/// <summary>
		/// Save the exception report to file/disk
		/// </summary>
		/// <param name="fileName">the filename to save</param>
		public void SaveReportToFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) return;

			var exceptionReport = CreateExceptionReport();

			try
			{
				using (var stream = File.OpenWrite(fileName))
				{
					var writer = new StreamWriter(stream);
					writer.Write(exceptionReport);
					writer.Flush();
				}
			}
			catch (Exception exception)
			{
				_view.ShowError(string.Format("Unable to save file '{0}'", fileName), exception);
			}
		}

		/// <summary>
		/// Send the exception report via email, using the configured email method/type
		/// </summary>
		public void SendReportByEmail()
		{
			switch (ReportInfo.MailMethod)
			{
				case ExceptionReportInfo.EmailMethod.SimpleMAPI:
					SendMapiEmail();
					break;
				case ExceptionReportInfo.EmailMethod.SMTP:
					SendSmtpMail();
					break;
				case ExceptionReportInfo.EmailMethod.WebService:
					SendToWebService();
					break;
				case ExceptionReportInfo.EmailMethod.None:
					break;
			}
		}

		/// <summary>
		/// copy the report to the clipboard, using the clipboard implementation supplied
		/// </summary>
		public void CopyReportToClipboard()
		{
			var exceptionReport = CreateExceptionReport();
			WinFormsClipboard.CopyTo(exceptionReport.ToString());
			_view.ProgressMessage = string.Format("{0} copied to clipboard", ReportInfo.TitleText);
		}

		/// <summary>
		/// toggle the detail between 'simple' (just message) and showFullDetail (ie normal)
		/// </summary>
		public void ToggleDetail()
		{
			_view.ShowFullDetail = !_view.ShowFullDetail;
			_view.ToggleShowFullDetail();
		}

		string BuildReportString()
		{
			var emailTextBuilder = new EmailTextBuilder();
			var emailIntroString = emailTextBuilder.CreateIntro(ReportInfo.TakeScreenshot);
			var entireEmailText = new StringBuilder(emailIntroString);

			var report = CreateExceptionReport();
			entireEmailText.Append(report);

			return entireEmailText.ToString();
		}

		void SendSmtpMail()
		{
			_view.ProgressMessage = "Sending email via SMTP...";
			_view.EnableEmailButton = false;
			_view.ShowProgressBar = true;

			try
			{
				var emailText = BuildReportString();
				var mailSender = new MailSender(ReportInfo, _view);
				mailSender.SendSmtp(emailText);
			}
			catch (Exception exception)
			{		// this would be an exception in the setup, the sending has it's own failure event
				_view.Completed(false);
				_view.ShowError("Unable to send email using SMTP" + Environment.NewLine + exception.Message, exception);
			}
		}

		private void SendToWebService()
		{
			_view.ProgressMessage = "Connecting to WebService...";
			_view.EnableEmailButton = false;

			try
			{
				var report = BuildReportString();
				var webService = new WebServiceSender(ReportInfo, _view);
				webService.Send(report);
			}
			catch (Exception exception)
			{
				_view.Completed(false);
				_view.ShowError("Unable to setup contact with WebService" + Environment.NewLine + exception.Message, exception);
			}
		}

		void SendMapiEmail()
		{

			_view.ProgressMessage = "Launching email program...";
			_view.EnableEmailButton = false;

			var success = false;

			try
			{
				var emailText = BuildReportString();
				var mailSender = new MailSender(ReportInfo, _view);
				mailSender.SendMapi(emailText);
				success = true;
			}
			catch (Exception exception)
			{
				success = false;
				_view.ShowError("Unable to connect to Email client\r\nPlease create an Email manually and use Copy Details", exception);
			}
			finally
			{
				_view.SetEmailCompletedState_WithMessageIfSuccess(success, string.Empty);
			}
		}

		/// <summary>
		/// Fetch the WMI system information
		/// </summary>
		public IEnumerable<SysInfoResult> GetSysInfoResults()
		{
			return _reportGenerator.GetOrFetchSysInfoResults();
		}

		/// <summary>
		/// Send email (using ShellExecute) to the configured contact email address
		/// </summary>
		public void SendContactEmail()
		{
			ShellExecute(string.Format("mailto:{0}", ReportInfo.ContactEmail));
		}

		/// <summary>
		/// Navigate to the website configured
		/// </summary>
		public void NavigateToWebsite()
		{
			ShellExecute(ReportInfo.WebUrl);
		}

		void ShellExecute(string executeString)
		{
			try
			{
				var psi = new ProcessStartInfo(executeString) { UseShellExecute = true };
				Process.Start(psi);
			}
			catch (Exception exception)
			{
				_view.ShowError(string.Format("Unable to (Shell) Execute '{0}'", executeString), exception);
			}
		}

		/// <summary>
		/// The main entry point, populates the report with everything it needs
		/// </summary>
		public void PopulateReport()
		{
			try
			{
				_view.SetInProgressState();

				_view.PopulateExceptionTab(ReportInfo.Exceptions);
				_view.PopulateAssembliesTab();
				if (!ExceptionReporter.IsRunningMono())
					_view.PopulateSysInfoTab();
			}
			finally
			{
				_view.SetProgressCompleteState();
			}
		}

		/// <summary>
		/// Close/cleanup
		/// </summary>
		public void Close()
		{
			_reportGenerator.Dispose();
		}
	}
}