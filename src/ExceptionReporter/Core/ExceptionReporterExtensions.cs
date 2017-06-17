using System.Text;

namespace ExceptionReporting.Extensions
{
	/// <summary>
	/// All extension methods for ExceptionReporter
	/// </summary>
	internal static class ExceptionReporterExtensions
	{
		/// <summary>
		/// Append a dotted line to the given string
		/// </summary>
		public static StringBuilder AppendDottedLine(this StringBuilder stringBuilder)
		{
			return stringBuilder.AppendLine("-----------------------------");
		}

		/// <summary>
		/// Return a string if not null, else the current value
		/// </summary>
		public static string GetString(this string newString, string currentString)
		{
			return string.IsNullOrEmpty(newString) ? currentString : newString.Trim();
		}

		/// <summary>
		/// Is empty.
		/// </summary>
		/// <returns><c>true</c>, if empty was ised, <c>false</c> otherwise.</returns>
		/// <param name="input">Input.</param>
		public static bool IsEmpty(this string input)
		{
			return string.IsNullOrEmpty(input);
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