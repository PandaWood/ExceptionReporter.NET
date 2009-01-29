using ExceptionReporting.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ExceptionController_Tests
	{
		//todo a more comprehensive suite of tests
		[Test]
		public void Controller_CanCreateExceptionReport_WithACoupleOfVagueBitsOfTextThatShouldBeThere()
		{
			var controller = new ExceptionReportController(new ExceptionReportInfo());
			var report = controller.CreateExceptionReport();

			Assert.That(report, Text.StartsWith("-"));
			Assert.That(report, Text.Contains("Application:"));
			Assert.That(report, Text.Contains("NumberOfUsers ="));
			Assert.That(report, Text.Contains("TotalPhysicalMemory ="));
		}
	}
}