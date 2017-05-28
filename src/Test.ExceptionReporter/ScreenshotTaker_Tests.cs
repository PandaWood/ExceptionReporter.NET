using ExceptionReporting.Core;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class ScreenshotTaker_Tests
	{
		[Test]
		public void TestName()
		{
			var screenshot = ScreenshotTaker.TakeScreenShot();
			if (ExceptionReport.IsRunningMono())			
				Assert.That(screenshot, Is.Null);
			else 
                Assert.IsNotNull(screenshot);
		}
	}
}