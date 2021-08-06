using System.Collections.Generic;
using System.IO;
using ExceptionReporting.Core;
using ExceptionReporting.Mail;

namespace ExceptionReporting.Report
{
	internal class NoScreenShot : IScreenshotTaker
	{
		public string TakeScreenShot()
		{
			return "";
		}
	}

	internal class ReportZipper
	{
		private readonly IFileService _fileService;
		private readonly ReportGenerator _reportGenerator;
		private readonly ExceptionReportInfo _info;

		public ReportZipper(IFileService fileService, ReportGenerator reportGenerator, ExceptionReportInfo info)
		{
			_fileService = fileService;
			_reportGenerator = reportGenerator;
			_info = info;
		}

		public void CreateReportZip(string zipFilePath)
		{
			var extension = _info.ReportTemplateFormat.ToString().ToLower().Replace("text", "txt");
			var reportPath = Path.Combine(Path.GetTempPath(), $"ExceptionReporter{Path.DirectorySeparatorChar}report.{extension}");
			if (!Directory.Exists(reportPath))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
			}

			var report = _reportGenerator.Generate();
			var textFileSaveResult = _fileService.Write(reportPath, report);
			if (textFileSaveResult.Saved)
			{
				var zipReport = new ZipAttachmentService(new Zipper(), new NoScreenShot(), _fileService);
				var result = zipReport.CreateZipReport(_info, zipFilePath, new List<string> {reportPath});
				if (!File.Exists(result))
				{
					// _exceptionReportPresenter.View.ShowError(Resources.Unable_to_save_file + $" '{result}'", new IOException());
				}
			}
			else
			{
				//return $"'{textReportPath}' {textFileSaveResult.Exception}";
				// _exceptionReportPresenter.View.ShowError(Resources.Unable_to_save_file + $" '{textReportPath}'", textFileSaveResult.Exception);
			}
		}
	}
}