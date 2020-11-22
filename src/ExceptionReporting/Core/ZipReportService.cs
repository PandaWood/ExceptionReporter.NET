using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExceptionReporting.Mail;

namespace ExceptionReporting.Core
{
	internal class ZipReportService : IZipReportService
	{
		private IZipper Zipper { get; } = new Zipper();
		private IScreenshotTaker ScreenshotTaker { get; } = new ScreenshotTaker();

		public ZipReportService()
		{
			Zipper = new Zipper();
			ScreenshotTaker = new ScreenshotTaker();
		}

		public ZipReportService(IZipper zipper, IScreenshotTaker screenshotTaker)
		{
			Zipper = zipper;
			ScreenshotTaker = screenshotTaker;
		}

		public string CreateZipReport(ExceptionReportInfo reportInfo)
		{
			string zipFilePath = Path.Combine(Path.GetTempPath(), reportInfo.AttachmentFilename);
			return CreateZipReport(reportInfo, zipFilePath);
		}

		public string CreateZipReport(ExceptionReportInfo reportInfo, string zipFilePath)
		{
			if (string.IsNullOrWhiteSpace(zipFilePath)) return string.Empty;
			if (File.Exists(zipFilePath)) File.Delete(zipFilePath);

			var files = new List<string>();
			if (reportInfo.FilesToAttach.Length > 0) files.AddRange(reportInfo.FilesToAttach);
			try
			{
				if (reportInfo.TakeScreenshot) files.Add(ScreenshotTaker.TakeScreenShot());
			}
			catch { /* ignored */ }

			var filesThatExist = files.Where(f => File.Exists(f)).ToList();
			var filesToZip = filesThatExist;
			if (filesToZip.Any())
				Zipper.Zip(zipFilePath, filesToZip);
			else
				return string.Empty;

			return zipFilePath;
		}
	}
}
