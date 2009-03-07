using System.IO;

#pragma warning disable 1591

namespace ExceptionReporter.Config
{
	public class ConfigParser : IConfigParser
	{
		public string ToString(string path)
		{
			return File.ReadAllText(ConfigReader.GetConfigFilePath());
		}
	}

	public interface IConfigParser
	{
		string ToString(string path);
	}
}

#pragma warning restore 1591

