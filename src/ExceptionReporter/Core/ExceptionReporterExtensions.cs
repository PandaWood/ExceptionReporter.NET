using System.Text;

namespace ExceptionReporting.Core
{
	/// <summary>
	/// All extension methods for ExceptionReporter
	/// It's important this class is internal/not public else it will pollute user extensions
	/// </summary>
	static class ExceptionReporterExtensions
	{
		/// <summary>
		/// Append a dotted line to the given string
		/// </summary>
		public static StringBuilder AppendDottedLine(this StringBuilder stringBuilder)
		{
			return stringBuilder.AppendLine("-----------------------------");
		}

		public static bool IsEmpty(this string input)
		{
			return string.IsNullOrWhiteSpace(input);
		}

		/// <summary>
		/// Truncate the specified value and maxLength.
		/// </summary>
		public static string Truncate(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) return value;
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}

	}
}