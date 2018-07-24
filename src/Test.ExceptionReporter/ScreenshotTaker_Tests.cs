using ExceptionReporting.Core;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class ScreenshotTaker_Tests
	{
		[Test]
		public void Can_Take_Screenshot()
		{
			var screenshot = ScreenshotTaker.TakeScreenShot();
			if (ExceptionReporter.IsRunningMono())
				Assert.That(screenshot, Is.Null);
			else
				Assert.IsNotNull(screenshot);
		}
	}
}