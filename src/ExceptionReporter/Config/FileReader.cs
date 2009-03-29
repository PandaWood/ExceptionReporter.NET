using System.IO;

namespace ExceptionReporting.Config
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
