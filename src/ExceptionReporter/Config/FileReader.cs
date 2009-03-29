using System.IO;

namespace ExceptionReporting.Config
{
	internal class FileReader : IFileReader
	{
		public string ReadAll(string fileName)
		{
			return File.ReadAllText(fileName);
		}
	}

	internal interface IFileReader
	{
		string ReadAll(string fileName);
	}
}
