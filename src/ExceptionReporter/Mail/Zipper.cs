using System;
using System.Collections.Generic;
using Ionic.Zip;

namespace ExceptionReporting.Mail
{
	interface IZipper 
	{
		void Zip(string zipFile, IEnumerable<string> files);
	}

	class Zipper : IZipper
	{
		public void Zip(string zipFile, IEnumerable<string> files)
		{
			using (var zip = new ZipFile(zipFile))
			{
				zip.AddFiles(files, directoryPathInArchive: "");
				zip.Save();
			}
		}
	}
}
