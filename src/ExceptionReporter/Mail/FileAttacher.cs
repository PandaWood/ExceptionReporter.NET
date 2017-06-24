using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExceptionReporting.Core;
using Ionic.Zip;

namespace ExceptionReporting.Mail
{
	class FileAttacher
	{
		ExceptionReportInfo _config;

		public FileAttacher(ExceptionReportInfo config)
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

			var filesThatExist = files.Where(File.Exists).ToList();

			foreach (var zf in filesThatExist.Where(f => f.EndsWith(".zip")))
			{
				attacher.Attach(zf);    // attach external zip files separately, admittedly weak detection using just file extension
			}

			var nonzipFilesToAttach = filesThatExist.Where(f => !f.EndsWith(".zip")).ToList();
			if (nonzipFilesToAttach.Any())
			{ // attach all other files (non zip) into our one zip file
				var zipFile = Path.Combine(Path.GetTempPath(), _config.AttachmentFilename);
				if (File.Exists(zipFile)) File.Delete(zipFile);

				using (var zip = new ZipFile(zipFile))
				{
					zip.AddFiles(nonzipFilesToAttach, "");
					zip.Save();
				}

				attacher.Attach(zipFile);
			}
		}
	}
}
