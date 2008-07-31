using System.Text;

/// a hack to ensure that we can use Extension Methods and still target .NET2
/// see http://www.danielmoth.com/Blog/2007/05/using-extension-methods-in-fx-20.html
namespace System.Runtime.CompilerServices
{
	public class ExtensionAttribute : Attribute { }
}

namespace ExceptionReporting.Extensions
{
	public static class ExceptionReporterExtensions
	{
		public static StringBuilder AppendDottedLine(this StringBuilder stringBuilder)
		{
			return stringBuilder.AppendLine("-----------------------------");
		}

		public static string ReturnStringOr(this string newString, string currentString)
		{
			return string.IsNullOrEmpty(newString) ? currentString : newString;
		}

		/// <summary>
		/// Returns the true if strNew is a string representing Yes (Y) or false if it represents No (N)
		/// if strNew is NULL or zero length the current value is returned
		/// </summary>
		public static bool ReturnBoolOr(this string configString, bool currentValue)
		{
			if (string.IsNullOrEmpty(configString)) return currentValue;

			if (configString.ToUpper().Equals("Y")) return true;
			if (configString.ToUpper().Equals("N")) return false;

			return currentValue;
		}
	}
}