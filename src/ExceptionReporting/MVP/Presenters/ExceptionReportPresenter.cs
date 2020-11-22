using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExceptionReporting.Core;
using ExceptionReporting.Mail;
using ExceptionReporting.MVP.Views;
using ExceptionReporting.Network;
using ExceptionReporting.Properties;
using ExceptionReporting.Report;
using ExceptionReporting.SystemInfo;
using ExceptionReporting.Templates;

namespace ExceptionReporting.MVP.Presenters
{
	/// <summary>
	/// The Presenter in this MVP (Model-View-Presenter) implementation 
	/// </summary>
	internal class ExceptionReportPresenter
	{
		private readonly IFileService _fileService;
		private readonly ReportGenerator _reportGenerator;

		/// <summary>
		/// constructor accepting a view and the data/config of the report
		/// </summary>
		public ExceptionReportPresenter(IExceptionReportView view, ExceptionReportInfo info)
		{
			_reportGenerator = new ReportGenerator(info);
			_fileService = new FileService();
			View = view;
			ReportInfo = info;
		}

		/// <summary> Report configuration and data  </summary>
		public ExceptionReportInfo ReportInfo { get; }

		/// <summary> The main dialog/view  </summary>
		private IExceptionReportView View { get; }

		private string CreateReport()
		{
			ReportInfo.UserExplanation = View.UserExplanation;
			return _reportGenerator.Generate();
		}

		/// <summary>
		/// Save the exception report to file/disk
		/// </summary>
		/// <param name="fileName">the filename to save to</param>
		public void SaveTextReportToFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) return;

			var report = CreateReport();
			var result = _fileService.Write(fileName, report);
			
			if (!result.Saved)
			{
				//View.ShowError(string.Format("Unable to save file '{0}'", zipFilePath), result.Exception);
				View.ShowError(Resources.Unable_to_save_file + $" '{fileName}'", result.Exception);
			}
		}

		/// <summary>
		/// Save the exception report to file/disk
		/// </summary>
		/// <param name="zipFilePath">the filename to save to</param>
		public void SaveZipReportToFile(string zipFilePath)
		{
			if (string.IsNullOrEmpty(zipFilePath)) return;

			//TODO: select extension by ReportTemplateFormat
			var textReportPath = Path.Combine(Path.GetTempPath(), @"ExceptionReporter\report.txt");
			if (!Directory.Exists(textReportPath)) Directory.CreateDirectory(Path.GetDirectoryName(textReportPath));
			var report = CreateReport();
			var textFileSaveResult = _fileService.Write(textReportPath, report);
			if (!textFileSaveResult.Saved)
			{
				View.ShowError(Resources.Unable_to_save_file + $" '{textReportPath}'", textFileSaveResult.Exception);
			}
			else
			{
				var zipReport = new ZipReportService(new Zipper(), new ScreenshotTaker());
				var result = zipReport.CreateZipReport(ReportInfo, zipFilePath);
				if (!File.Exists(result))
				{
					//View.ShowError(string.Format("Unable to save file '{0}'", zipFilePath), result.Exception);
					View.ShowError(Resources.Unable_to_save_file + $" '{result}'", new IOException());
				}
			}
		}

		/// <summary>
		/// Send the exception report using the configured send method
		/// </summary>
		public void SendReport()
		{
			View.EnableEmailButton = false;
			View.ShowProgressBar = true;
			
			var sender = new SenderFactory(ReportInfo, View).Get();
			View.ProgressMessage = sender.ConnectingMessage;
			
			try
			{
				var report = ReportInfo.IsSimpleMAPI() ? CreateEmailReport() : CreateReport();
				sender.Send(report);
			}
			catch (Exception exception)
			{		// most exceptions will be thrown in the Sender - this is just a backup
				View.Completed(false);
				//View.ShowError($"Unable to setup" + $" {sender.Description}" + 
				//               Environment.NewLine + exception.Message, exception);
				View.ShowError(Resources.Unable_to_setup + $" {sender.Description}" + 
				               Environment.NewLine + exception.Message, exception);
			}
			finally
			{
				if (ReportInfo.IsSimpleMAPI())
				{
					View.MapiSendCompleted();
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
			//View.ProgressMessage = "Copied to clipboard";
			View.ProgressMessage = Resources.Copied_to_clipboard;
		}

		/// <summary>
		/// toggle the detail between 'simple' (just message) and showFullDetail (ie normal)
		/// </summary>
		public void ToggleDetail()
		{
			View.ShowFullDetail = !View.ShowFullDetail;
			View.ToggleShowFullDetail();
		}

		private string CreateEmailReport()
		{
			var template = new TemplateRenderer(new EmailIntroModel
			{
				ScreenshotTaken = ReportInfo.TakeScreenshot
			});
			var emailIntro = template.RenderPreset();
			var report = CreateReport();

			return emailIntro + report;
		}

		/// <summary>
		/// Fetch the WMI system information
		/// </summary>
		public IEnumerable<SysInfoResult> GetSysInfoResults()
		{
			return _reportGenerator.GetOrFetchSysInfoResults();
		}

		/// <summary>
		/// The main entry point, populates the report with everything it needs
		/// </summary>
		public void PopulateReport()
		{
			try
			{
				View.SetInProgressState();

				View.PopulateExceptionTab(ReportInfo.Exceptions);
				View.PopulateAssembliesTab();
				if (ExceptionReporter.NotRunningMono())
					View.PopulateSysInfoTab();
			}
			finally
			{
				View.SetProgressCompleteState();
			}
		}

		public List<AssemblyRef> GetReferencedAssemblies()
		{
			return new AssemblyDigger(ReportInfo.AppAssembly).GetAssemblyRefs().ToList();
		}
	}
}