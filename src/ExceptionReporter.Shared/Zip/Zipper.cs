using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace ExceptionReporting.Zip
{
	internal interface IZipper
	{
		void Zip(string zipFile, IEnumerable<string> files);
	}

	internal class Zipper : IZipper
	{
		public void Zip(string zipFile, IEnumerable<string> files)
		{
			if (File.Exists(zipFile)) File.Delete(zipFile);

			using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
			{
				foreach (var file in files)
				{
					archive.CreateEntryFromFile(file, Path.GetFileName(file));
				}
			}
		}
	}
}
