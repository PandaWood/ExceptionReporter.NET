using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class ExceptionReporter_Tests
	{
		[Test]
		public void Can_Init_AppAssembly()
		{
			var exceptionReporter = new Core.ExceptionReporter();
			Assert.That(exceptionReporter.Config.AppAssembly, Is.Not.Null);
			Assert.That(exceptionReporter.Config.AppAssembly.FullName, 
				Is.StringStarting("ExceptionReporter.Tests"));
		}
	}
}