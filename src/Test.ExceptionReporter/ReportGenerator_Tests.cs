using System.Reflection;
using ExceptionReporting.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ReportGenerator_Tests
	{
		private ExceptionReportInfo _info;
		private ExceptionReportGenerator _controller;

		[SetUp]
		public void SetUp()
		{
			_info = new ExceptionReportInfo();
			_controller = new ExceptionReportGenerator(_info);

			_info.AppAssembly = Assembly.GetExecutingAssembly();
		}

		[TearDown]
		public void TearDown()
		{
			_info.Dispose();
		}

		[Test]
		public void Generator_CanCreateExceptionReport_WithACoupleOfVagueBitsOfTextThatShouldBeThere()
		{
			string report = _controller.CreateExceptionReport();

			Assert.That(report, Text.StartsWith("-"));
			Assert.That(report, Text.Contains("Application:"));
			Assert.That(report, Text.Contains("NumberOfUsers ="));
			Assert.That(report, Text.Contains("TotalPhysicalMemory ="));
		}

		[Test]
		public void Generator_CanCreateExceptionReport_WithSameResult_IfCalledTwice()
		{
			string report1 = _controller.CreateExceptionReport();
			string report2 = _controller.CreateExceptionReport();

			Assert.That(report1, Is.EqualTo(report2));
		}
	}
}