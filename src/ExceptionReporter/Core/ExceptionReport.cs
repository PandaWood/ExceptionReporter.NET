using System.Text;

namespace ExceptionReporting.Core
{
	/// <summary>
	/// Encapsulates the concept of an ExceptionReport
	/// </summary>
	public class ExceptionReport
	{
		private readonly StringBuilder _reportString;

		/// <summary>
		/// Construct an ExceptionReport from a StringBuilder
		/// </summary>
		public ExceptionReport(StringBuilder stringBuilder)
		{
			_reportString = stringBuilder;
		}

        private static bool _isRunningMono = System.Type.GetType("Mono.Runtime") != null;

        /// <summary>
        /// Is running mono.
        /// </summary>
        /// <returns><c>true</c>, if running mono <c>false</c> otherwise.</returns>
        public static bool IsRunningMono() { return _isRunningMono; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:ExceptionReporting.Core.ExceptionReport"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:ExceptionReporting.Core.ExceptionReport"/>.</returns>
		public override string ToString()
		{
			return _reportString.ToString();
		}

		private bool Equals(ExceptionReport obj)
		{
			return Equals(obj._reportString.ToString(), _reportString.ToString());
		}

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:ExceptionReporting.Core.ExceptionReport"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:ExceptionReporting.Core.ExceptionReport"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:ExceptionReporting.Core.ExceptionReport"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			return Equals((ExceptionReport) obj);
		}

        /// <summary>
        /// Serves as a hash function for a <see cref="T:ExceptionReporting.Core.ExceptionReport"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
		public override int GetHashCode()
		{
			return _reportString.GetHashCode();
		}
	}
}