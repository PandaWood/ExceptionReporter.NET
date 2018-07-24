using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class ExceptionReporter_Tests
	{
		[Test]
		public void Can_Init_App_Assembly()
		{
			var exceptionReporter = new ExceptionReporter();
			Assert.That(exceptionReporter.Config.AppAssembly, Is.Not.Null);
			Assert.That(exceptionReporter.Config.AppAssembly.FullName, Does.Contain("ExceptionReporter.Tests"));
		}
	}
}