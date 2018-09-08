using System;
using System.Reflection;
using ExceptionReporting;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	public class ReportGenerator_Tests
	{
		private ExceptionReportInfo _info;
		private ReportGenerator _reportGenerator;

		[SetUp]
		public void SetUp()
		{
			_info = new ExceptionReportInfo { MainException = new Exception() };
			_reportGenerator = new ReportGenerator(_info);

			// set for testing because the AppAssembly filled by default, is null in a test environment
			_info.AppAssembly = Assembly.GetExecutingAssembly();
		}

		[Test]
		public void Can_Deal_With_Null_In_Constructor()
		{
			Assert.Throws<ArgumentNullException>(() => _reportGenerator = new ReportGenerator(null), "reportInfo cannot be null");
		}

		[Test]
		public void Can_Create_Report_With_A_Couple_Of_Minimal_Bits_That_Should_Exist()
		{
			if (ExceptionReporter.IsRunningMono()) return;
			var report = _reportGenerator.Generate();
			var reportString = report;

			Assert.That(reportString, Does.Contain("Application:"));
			Assert.That(reportString, Does.Contain("Version:"));
			Assert.That(reportString, Does.Contain("TotalPhysicalMemory ="));
		}

		[Test]
		public void Can_Create_Report_With_Local_Datetime()
		{
			var reportInfo = new ExceptionReportInfo
			{
				ExceptionDateKind = DateTimeKind.Local, 
				MainException = new Exception()
			};
			// ReSharper disable once ObjectCreationAsStatement
			new ReportGenerator(reportInfo);
			Assert.That(reportInfo.ExceptionDate.Kind, Is.EqualTo(DateTimeKind.Local));
		}
	}
}