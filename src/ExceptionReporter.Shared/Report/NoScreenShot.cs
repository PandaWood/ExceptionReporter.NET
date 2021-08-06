using ExceptionReporting.Core;

namespace ExceptionReporting.Report
{
	internal class NoScreenShot : IScreenshotTaker
	{
		public string TakeScreenShot()
		{
			return "";
		}
	}
}