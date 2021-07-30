using System;

namespace ExceptionReporting.Core
{
	internal class ConfigException : Exception
	{
		public ConfigException(string message) : base(message)
		{ }
	}

}