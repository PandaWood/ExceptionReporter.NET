using System;
using System.Reflection;
using ExceptionReporting.Core;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class ExceptionReportGenerator_Tests
	{
		private ExceptionReportInfo _info;
		private ExceptionReportGenerator _reportGenerator;

		[SetUp]
		public void SetUp()
		{
			_info = new ExceptionReportInfo { MainException = new Exception() };
			_reportGenerator = new ExceptionReportGenerator(_info);

			// set for testing because the AppAssembly filled by default, is null in a test environment
			_info.AppAssembly = Assembly.GetExecutingAssembly();
		}

		[Test]
		public void TestName()
		{
			Assert.Throws<ExceptionReportGeneratorException>(() => _reportGenerator = new ExceptionReportGenerator(null), "reportInfo cannot be null");
		}

		[Test]
		public void Generator_CanCreateExceptionReport_WithACoupleOfMinimal_NotTooSpecificBits_ThatShouldExist()
		{
			if (ExceptionReporter.IsRunningMono()) return;
			var report = _reportGenerator.CreateExceptionReport();
			var reportString = report.ToString();

			Assert.That(reportString, Does.StartWith("-"));
			Assert.That(reportString, Does.Contain("Application:"));
			Assert.That(reportString, Does.Contain("TotalPhysicalMemory ="));
		}

		[Test]
		public void Generator_CanCreateExceptionReport_WithSameResult_IfCalledTwice()
		{
			var report1 = _reportGenerator.CreateExceptionReport();
			var report2 = _reportGenerator.CreateExceptionReport();

			Assert.That(report1, Is.EqualTo(report2));
		}
	}
}