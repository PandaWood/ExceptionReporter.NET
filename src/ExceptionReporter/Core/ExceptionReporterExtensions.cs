using System.Text;

namespace ExceptionReporting.Extensions
{
	/// <summary>
	/// All extension methods for ExceptionReporter
	/// </summary>
	public static class ExceptionReporterExtensions
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
		/// Returns the boolean value of configString; where configString is null or empty, the current value is returned
		/// <remarks>all of (case insensitive) 'y' 'n' 'true' or 'false' are accepted as boolean indicators</remarks>
		/// </summary>
		public static bool GetBool(this string configString, bool currentValue)
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

        /// <summary>
        /// Is empty.
        /// </summary>
        /// <returns><c>true</c>, if empty was ised, <c>false</c> otherwise.</returns>
        /// <param name="input">Input.</param>
		public static bool IsEmpty(this string input)
		{
			return string.IsNullOrEmpty(input);
		}
	}
}