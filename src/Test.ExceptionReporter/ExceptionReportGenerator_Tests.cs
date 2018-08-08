using System;
using System.Reflection;
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
		public void Can_Deal_With_Null_In_Constructor()
		{
			Assert.Throws<ExceptionReportGeneratorException>(() => _reportGenerator = new ExceptionReportGenerator(null), "reportInfo cannot be null");
		}

		[Test]
		public void Can_Create_Report_With_A_Couple_Of_Minimal_Bits_That_Should_Exist()
		{
			if (ExceptionReporter.IsRunningMono()) return;
			var report = _reportGenerator.CreateExceptionReport();
			var reportString = report.ToString();

			Assert.That(reportString, Does.StartWith("-"));
			Assert.That(reportString, Does.Contain("Application:"));
			Assert.That(reportString, Does.Contain("TotalPhysicalMemory ="));
		}

		[Test]
		public void Can_Create_Report_With_Same_Result_If_Called_Twice()
		{
			var report1 = _reportGenerator.CreateExceptionReport();
			var report2 = _reportGenerator.CreateExceptionReport();

			Assert.That(report1, Is.EqualTo(report2));
		}

		[Test]
		public void Can_Create_Report_With_Local_Datetime()
		{
			var config = new ExceptionReportInfo { ExceptionDateKind = DateTimeKind.Local, MainException = new Exception() };
			var report = new ExceptionReportGenerator(config);
			Assert.That(config.ExceptionDate.Kind == DateTimeKind.Local);
		}
	}
}