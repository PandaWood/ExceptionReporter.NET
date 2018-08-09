// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System;
using System.IO;

namespace ExceptionReporting.Core
{
	internal interface IFileService 
	{
		bool Exists(string file);
		string TempFile(string file);
	}

	internal class FileService : IFileService
	{
		public bool Exists(string file)
		{
			return File.Exists(file);
		}

		/// <summary>
		/// Returns a file with given name, in system TEMP path
		/// file is never deleted but reused (deleted before being reused)
		/// </summary>
		/// <returns>The filename, ready for use</returns>
		public string TempFile(string file)
		{
			var tempFile = Path.Combine(Path.GetTempPath(), file);
			DeleteIfExists(tempFile);
			return tempFile;
		}

		private static void DeleteIfExists(string file)
		{
			if (File.Exists(file)) 
			{ 
				File.Delete(file);
			}
		}
	}
}
