using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ExceptionReporter.Core;
using ExceptionReporter.Extensions;
using ExceptionReporter.SystemInfo;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace ExceptionReporter.Tests
{
	[TestFixture]
	public class ExceptionReportBuilder_Tests
	{
		[Test]
		public void CanBuild_ReferencedAssemblies_Section()
		{
            using (ExceptionReportInfo reportInfo = CreateReportInfo())
            {
                reportInfo.ShowAssembliesTab = true;
                reportInfo.AppAssembly = Assembly.GetExecutingAssembly();

                ExceptionReport exceptionReport = new ExceptionReportBuilder(reportInfo).Build();

                Assert.That(exceptionReport, Is.Not.Null);
                Assert.That(exceptionReport.ToString().Length, Is.GreaterThan(0));

				Assert.That(exceptionReport.ToString(), Text.Contains("nunit")); // coupled to NUnit, but better than nothing
				Assert.That(exceptionReport.ToString(), Text.Contains("ExceptionReporter"));
				Assert.That(exceptionReport.ToString(), Text.Contains("System.Core, Version="));
				Assert.That(exceptionReport.ToString(), Text.Contains(Environment.NewLine));
            }
		}

		[Test]
		public void CanBuild_SysInfoSection()
		{
			IList<SysInfoResult> sysInfoResults = CreateSysInfoResult();
			StringBuilder expectedExceptionReport = CreateExpectedReport();
            using (ExceptionReportInfo reportInfo = CreateReportInfo())
            {
                reportInfo.ShowSysInfoTab = true;

                ExceptionReport exceptionReport = new ExceptionReportBuilder(reportInfo, sysInfoResults).Build();

				Assert.That(exceptionReport.ToString(), Is.EqualTo(expectedExceptionReport.ToString()));
            }
		}

		[Test]
		public void CanBuild_HierarchyString_With_Root_And_InnerException()
		{
            using (ExceptionReportInfo reportInfo = CreateReportInfo())
            {
                reportInfo.ShowExceptionsTab = true;
                reportInfo.SetExceptions(new List<Exception>
                                            {
                                                new ArgumentOutOfRangeException("OuterException", 
												new ArgumentNullException("Inner" + "Exception"))
                                            });

                StringBuilder expectedExceptionReportString = new StringBuilder().AppendDottedLine()
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

                ExceptionReport exceptionReport = new ExceptionReportBuilder(reportInfo).Build();

				Assert.That(exceptionReport.ToString(), Is.EqualTo(expectedExceptionReportString.ToString()));
            }
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