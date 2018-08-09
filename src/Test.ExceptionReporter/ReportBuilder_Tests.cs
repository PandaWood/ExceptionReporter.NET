using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ExceptionReporting.Core;
using ExceptionReporting.SystemInfo;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	public class ReportBuilder_Tests
	{
		[Test]
		public void Can_Build_Referenced_Assemblies_Section()
		{
			using (var reportInfo = CreateReportInfo())
			{
				reportInfo.ShowAssembliesTab = true;
				reportInfo.AppAssembly = Assembly.GetExecutingAssembly();

				var builder = new ReportBuilder(reportInfo, new List<SysInfoResult>());

				var exceptionReport = builder.Build();

				Assert.That(exceptionReport, Is.Not.Null);

				var exceptionReportString = exceptionReport.ToString();
				Assert.That(exceptionReportString.Length, Is.GreaterThan(0));
				if (!ExceptionReporter.IsRunningMono())
					Assert.That(exceptionReportString, Does.Contain("System.Core, Version="));
				Assert.That(exceptionReportString, Does.Contain(Environment.NewLine));
			}
		}

		[Test]
		public void Can_Build_SysInfo_Section()
		{
			var sysInfoResults = CreateSysInfoResult();
			var expectedExceptionReport = CreateExpectedReport();

			using (var reportInfo = CreateReportInfo())
			{
				reportInfo.ShowSysInfoTab = true;

				var builder = new ReportBuilder(reportInfo, sysInfoResults);
				var exceptionReport = builder.Build();

				if (!ExceptionReporter.IsRunningMono())
					Assert.That(exceptionReport.ToString(), Is.EqualTo(expectedExceptionReport.ToString()));
			}
		}

		[Test]
		public void Can_Build_Hierarchy_String_With_Root_And_Inner_Exception()
		{
			using (var reportInfo = CreateReportInfo())
			{
				reportInfo.ShowExceptionsTab = true;
				reportInfo.SetExceptions(new List<Exception>
																						{
																								new ArgumentOutOfRangeException("OuterException",
																								new ArgumentNullException("Inner" + "Exception"))
																						});

				var expectedExceptionReportString = new StringBuilder().AppendDottedLine()
						.AppendLine("[Exception Info 1]").AppendLine()
						.AppendLine("Top-level Exception")
						.AppendLine("Type:        System.ArgumentOutOfRangeException")
						.AppendLine("Message:     OuterException")
						.AppendLine("Source:      ")
						.AppendLine()
						.AppendLine("Inner Exception 1")
						.AppendLine("Type:        System.ArgumentNullException")
						.AppendLine("Message:     Value cannot be null.")
						.AppendLine("Parameter name: InnerException")
						.AppendLine("Source:")
						.AppendLine().AppendDottedLine().AppendLine();

				var builder = new ReportBuilder(reportInfo, new List<SysInfoResult>());
				var exceptionReport = builder.Build();

				Assert.That(exceptionReport.ToString(), Is.EqualTo(expectedExceptionReportString.ToString()));
			}
		}

		private static ExceptionReportInfo CreateReportInfo()
		{
			return new ExceptionReportInfo
			{
				ShowAssembliesTab = false,
				ShowGeneralTab = false,
				ShowSysInfoTab = false,
				ShowExceptionsTab = false,
			};
		}

		private static StringBuilder CreateExpectedReport()
		{
			var expectedString = new StringBuilder().AppendDottedLine();
			expectedString.AppendLine("[System Info]").AppendLine();
			expectedString.AppendLine("Memory");
			expectedString.AppendLine("-Physical Memory");
			expectedString.AppendLine("--Version:2.66");
			expectedString.AppendLine().AppendDottedLine().AppendLine();
			return expectedString;
		}

		private static IEnumerable<SysInfoResult> CreateSysInfoResult()
		{
			IList<SysInfoResult> results = new List<SysInfoResult>();
			var result = new SysInfoResult("Memory");
			result.Nodes.Add("Physical Memory");
			var resultChild = new SysInfoResult("Bla");
			result.ChildResults.Add(resultChild);
			resultChild.Nodes.Add("Version:2.66");
			results.Add(result);
			return results;
		}
	}
}