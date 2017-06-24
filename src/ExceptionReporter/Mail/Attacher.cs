using System.Collections.Generic;
using System.Linq;
using ExceptionReporting.Core;

namespace ExceptionReporting.Mail
{
	class Attacher
	{
		protected const string ZIP = ".zip";
		public IFileService File { get; set; } = new FileService();
		public IZipper Zipper { get; set; } = new Zipper();
		readonly ExceptionReportInfo _config;

		public Attacher(ExceptionReportInfo config)
		{
			_config = config;
		}

		public void AttachFiles(IAttach attacher)
		{
			var files = new List<string>();
			if (_config.FilesToAttach.Length > 0)
			{
				files.AddRange(_config.FilesToAttach);
			}
			if (_config.ScreenshotAvailable)
			{
				files.Add(ScreenshotTaker.GetImageAsFile(_config.ScreenshotImage));
			}

			var filesThatExist = files.Where(f => File.Exists(f));

			// attach external zip files separately, admittedly weak detection using just file extension
			filesThatExist.Where(f => f.EndsWith(ZIP)).ToList().ForEach(attacher.Attach);

			// now zip & attach all specified files (ie config FilesToAttach) plus screenshot, if taken
			var nonzipFilesToAttach = filesThatExist.Where(f => !f.EndsWith(ZIP)).ToList();
			if (nonzipFilesToAttach.Any())
			{
				var zipFile = File.TempFile(_config.AttachmentFilename);
				Zipper.Zip(zipFile, nonzipFilesToAttach);
				attacher.Attach(zipFile);
			}
		}
	}
}
