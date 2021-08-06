using System;
using System.Collections.Generic;
using System.Linq;
using ExceptionReporting.Core;
using ExceptionReporting.Network;
using ExceptionReporting.Properties;
using ExceptionReporting.Report;
using ExceptionReporting.SystemInfo;
using ExceptionReporting.Templates;
using ExceptionReporting.WinForms;

namespace ExceptionReporting.MVP.Presenters
{
	/// <summary>
	/// The Presenter in this MVP (Model-View-Presenter) implementation 
	/// </summary>
	internal class ExceptionReportPresenter
	{
		private readonly ReportGenerator _reportGenerator;
		private readonly ReportZipper _reportZipper;

		/// <summary>
		/// constructor accepting a view and the data/config of the report
		/// </summary>
		public ExceptionReportPresenter(IExceptionReportView view, ExceptionReportInfo info)
		{
			_reportGenerator = new ReportGenerator(info);
			_reportZipper = new ReportZipper(new FileService(), _reportGenerator, info);
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
		/// <param name="zipFilePath">the filename to save to</param>
		public void SaveZipReportToFile(string zipFilePath)
		{
			if (string.IsNullOrEmpty(zipFilePath)) return;
			_reportZipper.CreateReportZip(zipFilePath);
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
		/// copy the report to clipboard
		/// </summary>
		public void CopyReportToClipboard()
		{
			var report = CreateReport();
			View.ProgressMessage = WinFormsClipboard.CopyTo(report) ? Resources.Copied_to_clipboard : Resources.Failed_to_copy_to_clipboard;
		}

		/// <summary>
		/// toggle the detail between 'simple' (just message) and showFullDetail (ie normal)
		/// </summary>
		public void ToggleDetail()
		{
			View.ShowFullDetail = !View.ShowFullDetail;
			View.ToggleShowFullDetail();
		}

		//TODO move this out of presenter
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