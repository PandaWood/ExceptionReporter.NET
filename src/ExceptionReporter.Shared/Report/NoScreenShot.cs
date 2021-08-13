using ExceptionReporting.Core;

namespace ExceptionReporting.Report
{
	/// <summary>
	/// An implementation of IScreenshooter that does nothing
	/// </summary>
	public class NoScreenShot : IScreenShooter
	{
		/// <summary>
		/// Do nothing
		/// </summary>
		/// <returns>an empty string</returns>
		public string TakeScreenShot()
		{
			return "";
		}
	}
}