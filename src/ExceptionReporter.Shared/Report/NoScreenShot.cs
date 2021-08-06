using ExceptionReporting.Core;

namespace ExceptionReporting.Report
{
	internal class NoScreenShot : IScreenShooter
	{
		public string TakeScreenShot()
		{
			return "";
		}
	}
}