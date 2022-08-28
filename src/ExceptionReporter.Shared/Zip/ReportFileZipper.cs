using System.Collections.Generic;
using System.IO;
using ExceptionReporting.Core;
using ExceptionReporting.Report;

namespace ExceptionReporting.Zip
{
	internal class ReportFileZipper
	{
		private readonly IFileService _fileService;
		private readonly ReportGenerator _reportGenerator;
		private readonly ExceptionReportInfo _info;

		public ReportFileZipper(IFileService fileService, ReportGenerator reportGenerator, ExceptionReportInfo info)
		{
			_fileService = fileService;
			_reportGenerator = reportGenerator;
			_info = info;
		}

		public FileSaveResult Save(string zipFilePath)
		{
			var fileExtension = _info.ReportTemplateFormat.ToString().ToLower().Replace("text", "txt");
			var reportPath = Path.Combine(Path.GetTempPath(), $"ExceptionReporter{Path.DirectorySeparatorChar}report.{fileExtension}");
			if (!Directory.Exists(reportPath))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
			}

			var report = _reportGenerator.Generate();
			var saveResult = _fileService.Write(reportPath, report);
			if (!saveResult.Saved) return saveResult;

			var zipReport = new ZipAttachmentService(new Zipper(), new NoScreenShot(), _fileService);
			var savedPath = zipReport.CreateZipReport(_info, zipFilePath, new List<string> {reportPath});
			return File.Exists(savedPath) ? saveResult: new FileSaveResult
			{
				Exception = new IOException(savedPath)
			};
		}
	}
}