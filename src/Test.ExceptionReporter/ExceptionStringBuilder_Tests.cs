using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ExceptionReporting.Core;
using ExceptionReporting.Extensions;
using ExceptionReporting.SystemInfo;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ExceptionStringBuilder_Tests
	{
		[Test]
		public void CanBuild_ReferencedAssemblies_Section()
		{
			ExceptionReportInfo reportInfo = CreateReportInfo();
			reportInfo.ShowAssembliesTab = true;
			reportInfo.AppAssembly = Assembly.GetExecutingAssembly();

			string exceptionReport = new ExceptionStringBuilder(reportInfo).Build();

			Assert.That(exceptionReport, Is.Not.Null);
			Assert.That(exceptionReport.Length, Is.GreaterThan(0));

			Assert.That(exceptionReport, Text.Contains("nunit"));	// coupled to NUnit, but better than nothing
			Assert.That(exceptionReport, Text.Contains("ExceptionReporter, Version="));
			Assert.That(exceptionReport, Text.Contains("System.Core, Version="));
			Assert.That(exceptionReport, Text.Contains(Environment.NewLine));
		}

		[Test]
		public void CanBuild_SysInfoSection()
		{
			IList<SysInfoResult> sysInfoResults = CreateSysInfoResult();
			StringBuilder expectedExceptionReport = CreateExpectedReport();
			ExceptionReportInfo reportInfo = CreateReportInfo();
			reportInfo.ShowSysInfoTab = true;

			string exceptionReport = new ExceptionStringBuilder(reportInfo, sysInfoResults).Build();

			Assert.That(exceptionReport, Is.EqualTo(expectedExceptionReport.ToString()));
		}

		[Test]
		public void CanBuild_HierarchyString_With_Root_And_InnerException()
		{
			ExceptionReportInfo reportInfo = CreateReportInfo();
			reportInfo.ShowExceptionsTab = true;
			reportInfo.Exception = new ArgumentOutOfRangeException("OuterException", new ArgumentNullException("Inner" + "Exception"));

			StringBuilder expectedExceptionReport = new StringBuilder().AppendDottedLine()
				.AppendLine("[Exception Info]").AppendLine()
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

			string exceptionReport = new ExceptionStringBuilder(reportInfo).Build();

			Assert.That(exceptionReport, Is.EqualTo(expectedExceptionReport.ToString()));
		}

		private static ExceptionReportInfo CreateReportInfo()
		{
			return new ExceptionReportInfo
			{
				ShowAssembliesTab = false,
				ShowConfigTab = false,
				ShowGeneralTab = false,
				ShowSysInfoTab = false,
				ShowExceptionsTab = false,
			};
		}

		private static StringBuilder CreateExpectedReport() 
		{
			StringBuilder expectedString = new StringBuilder().AppendDottedLine();
			expectedString.AppendLine("[System Info]").AppendLine();
			expectedString.AppendLine("Memory");
			expectedString.AppendLine("-Physical Memory");
			expectedString.AppendLine("--Version:2.66");
			expectedString.AppendLine().AppendDottedLine().AppendLine();
			return expectedString;
		}

		private static IList<SysInfoResult> CreateSysInfoResult() 
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