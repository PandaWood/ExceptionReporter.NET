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
		[ExpectedException(typeof(ExceptionReportGeneratorException), ExpectedMessage = "reportInfo cannot be null")]
		public void TestName()
		{
			_reportGenerator = new ExceptionReportGenerator(null);
		}

		[Test]
		public void Generator_CanCreateExceptionReport_WithACoupleOfMinimal_NotTooSpecificBits_ThatShouldExist()
		{
			var report = _reportGenerator.CreateExceptionReport();

			Assert.That(report.ToString(), Is.StringStarting("-"));
			Assert.That(report.ToString(), Is.StringContaining("Application:"));
			Assert.That(report.ToString(), Is.StringContaining("TotalPhysicalMemory ="));
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