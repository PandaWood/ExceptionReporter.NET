// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System;
using System.Collections.Generic;
// using Ionic.Zip;

/// <summary>
/// TODO: update to native .net 4.5 zipper 
/// https://msdn.microsoft.com/en-us/library/system.io.compression.ziparchive(v=vs.110).aspx
/// </summary>
namespace ExceptionReporting.Mail
{
	internal interface IZipper 
	{
		void Zip(string zipFile, IEnumerable<string> files);
	}

	internal class Zipper : IZipper
	{
		public void Zip(string zipFile, IEnumerable<string> files)
		{
			/*
			using (var zip = new ZipFile(zipFile))
			{
				zip.AddFiles(files, directoryPathInArchive: "");
				zip.Save();
			}
			*/
		}
	}
}
