using System.Text;

/// a hack (?) to ensure that we can use Extension Methods and still target .NET2
/// see http://www.danielmoth.com/Blog/2007/05/using-extension-methods-in-fx-20.html
namespace System.Runtime.CompilerServices
{
	public class ExtensionAttribute : Attribute { }
}

namespace ExceptionReporter.Extensions
{
	public static class ExceptionReporterExtensions
	{
		public static StringBuilder AppendDottedLine(this StringBuilder stringBuilder)
		{
			return stringBuilder.AppendLine("-----------------------------");
		}

		// TODO this is probably a dodgy use of extension methods, consider refactoring
		public static string ReturnStringIfNotNull_Else(this string newString, string currentString)
		{
			return string.IsNullOrEmpty(newString) ? currentString : newString.Trim();
		}

		/// <summary>
		/// Returns the boolean value of configString; where configString is null or empty, the current value is returned
		/// <remarks>all of (case insensitive) 'y' 'n' 'true' or 'false' are accepted as boolean indicators</remarks>
		/// </summary>
		public static bool ReturnBoolfNotNull_Else(this string configString, bool currentValue)
		{
			if (string.IsNullOrEmpty(configString)) return currentValue;

			switch (configString.ToLower())
			{
				case "y" : 
				case "true": 
					return true;

				case "n" :
				case "false" : 
					return false;
			}

			return currentValue;
		}
	}
}