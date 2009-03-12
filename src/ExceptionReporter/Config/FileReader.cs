using System.IO;

#pragma warning disable 1591

namespace ExceptionReporter.Config
{
	public class FileReader : IFileReader
	{
		public string ReadAll(string fileName)
		{
			return File.ReadAllText(fileName);
		}
	}

	public interface IFileReader
	{
		string ReadAll(string fileName);
	}
}

#pragma warning restore 1591
