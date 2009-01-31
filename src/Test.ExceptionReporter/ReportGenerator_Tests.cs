using ExceptionReporting.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ReportGenerator_Tests
	{
		[Test]
		public void Generator_CanCreateExceptionReport_WithACoupleOfVagueBitsOfTextThatShouldBeThere()
		{
            using (var exceptionReportInfo = new ExceptionReportInfo())
            {
                var controller = new ExceptionReportGenerator(exceptionReportInfo);
                var report = controller.CreateExceptionReport();

                Assert.That(report, Text.StartsWith("-"));
                Assert.That(report, Text.Contains("Application:"));
                Assert.That(report, Text.Contains("NumberOfUsers ="));
                Assert.That(report, Text.Contains("TotalPhysicalMemory ="));
            }
		}

		[Test]
		public void Generator_CanCreateExceptionReport_WithSameResult_IfCalledTwice()
		{
			using (var exceptionReportInfo = new ExceptionReportInfo())
			{
				var controller = new ExceptionReportGenerator(exceptionReportInfo);
				var report1 = controller.CreateExceptionReport();
				var report2 = controller.CreateExceptionReport();

				Assert.That(report1, Is.EqualTo(report2));
			}
		}
	}
}