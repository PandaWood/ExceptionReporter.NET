using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using ExceptionReporting.Core;
using ExceptionReporting.Mail;
using ExceptionReporting.MVP.Views;
using ExceptionReporting.Network;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.MVP
{
	/// <summary>
	/// The Presenter in this MVP (Model-View-Presenter) implementation 
	/// </summary>
	internal class ExceptionReportPresenter
	{
		private readonly IExceptionReportView _view;
		private readonly IFileService _fileService;
		private readonly ExceptionReportGenerator _reportGenerator;

		/// <summary>
		/// constructor accepting a view and the data/config of the report
		/// </summary>
		public ExceptionReportPresenter(IExceptionReportView view, ExceptionReportInfo info)
		{
			_view = view;
			_reportGenerator = new ExceptionReportGenerator(info);
			_fileService = new FileService();
			ReportInfo = info;
		}

		/// <summary>
		/// The application assembly - ie the main application using the exception reporter assembly
		/// </summary>
		public Assembly AppAssembly
		{
			get { return ReportInfo.AppAssembly; }
		}

		/// <summary>
		/// Report configuration and data
		/// </summary>
		public ExceptionReportInfo ReportInfo { get; }

		private string CreateReport()
		{
			ReportInfo.UserExplanation = _view.UserExplanation;
			return _reportGenerator.Generate().ToString();
		}

		/// <summary>
		/// Save the exception report to file/disk
		/// </summary>
		/// <param name="fileName">the filename to save</param>
		public void SaveReportToFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) return;

			var report = CreateReport();
			var result = _fileService.Write(fileName, report);
			
			if (!result.Saved)
			{
				_view.ShowError(string.Format("Unable to save file '{0}'", fileName), result.Exception);
			}
		}

		/// <summary>
		/// Send the exception report using the configured send method
		/// </summary>
		public void SendReport()
		{
			_view.EnableEmailButton = false;
			_view.ShowProgressBar = true;
			
			var sender = new SenderFactory(ReportInfo, _view).Get();
			_view.ProgressMessage = sender.ConnectingMessage;
			
			try
			{
				var report = ReportInfo.IsSimpleMAPI() ? CreateMapiReport() : CreateReport();
				sender.Send(report);
			}
			catch (Exception exception)
			{		// most exceptions will be thrown in the Sender - this is just a backup
				_view.Completed(false);
				_view.ShowError(string.Format("Unable to setup {0}", sender.Description) + 
				                Environment.NewLine + exception.Message, exception);
			}
			finally
			{
				if (ReportInfo.IsSimpleMAPI())
				{
					_view.Mapi_Completed();
				}
			}
		}

		/// <summary>
		/// copy the report to the clipboard
		/// </summary>
		public void CopyReportToClipboard()
		{
			var report = CreateReport();
			WinFormsClipboard.CopyTo(report);
			_view.ProgressMessage = "Copied to clipboard";
		}

		/// <summary>
		/// toggle the detail between 'simple' (just message) and showFullDetail (ie normal)
		/// </summary>
		public void ToggleDetail()
		{
			_view.ShowFullDetail = !_view.ShowFullDetail;
			_view.ToggleShowFullDetail();
		}

		private string CreateMapiReport()
		{
			var emailTextBuilder = new EmailTextBuilder();
			var emailIntroString = emailTextBuilder.CreateIntro(ReportInfo.TakeScreenshot);
			var entireEmailText = new StringBuilder(emailIntroString);

			var report = CreateReport();
			entireEmailText.Append(report);

			return entireEmailText.ToString();
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

		private void ShellExecute(string executeString)
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
				if (ExceptionReporter.NotRunningMono())
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