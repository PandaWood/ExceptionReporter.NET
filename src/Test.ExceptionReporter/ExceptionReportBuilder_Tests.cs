using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ExceptionReporting.Extensions;
using ExceptionReporting.Config;
using ExceptionReporting.Core;
using ExceptionReporting.SystemInfo;
using NUnit.Framework;
using Rhino.Mocks;

namespace ExceptionReporting.Tests
{
    [TestFixture]
    public class ExceptionReportBuilder_Tests
    {
        private IFileReader _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileReader = MockRepository.GenerateStub<IFileReader>();
            _fileReader.Stub(c => c.ReadAll("")).IgnoreArguments().Return("");
        }

        [Test]
        public void CanBuild_ReferencedAssemblies_Section()
        {
            using (var reportInfo = CreateReportInfo())
            {
                reportInfo.ShowAssembliesTab = true;
                reportInfo.AppAssembly = Assembly.GetExecutingAssembly();

                var builder = new ExceptionReportBuilder(reportInfo) { FileReader = _fileReader };

                var exceptionReport = builder.Build();

                Assert.That(exceptionReport, Is.Not.Null);

                var exceptionReportString = exceptionReport.ToString();
                Assert.That(exceptionReportString.Length, Is.GreaterThan(0));
                Assert.That(exceptionReportString, Is.StringContaining("System.Core, Version="));
                Assert.That(exceptionReportString, Is.StringContaining(Environment.NewLine));
            }
        }

        [Test]
        public void CanBuild_SysInfoSection()
        {
            var sysInfoResults = CreateSysInfoResult();
            var expectedExceptionReport = CreateExpectedReport();

            using (var reportInfo = CreateReportInfo())
            {
                reportInfo.ShowSysInfoTab = true;

                var builder = new ExceptionReportBuilder(reportInfo, sysInfoResults) { FileReader = _fileReader };
                var exceptionReport = builder.Build();

                Assert.That(exceptionReport.ToString(), Is.EqualTo(expectedExceptionReport.ToString()));
            }
        }

        [Test]
        public void CanBuild_HierarchyString_With_Root_And_InnerException()
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

                var builder = new ExceptionReportBuilder(reportInfo) { FileReader = _fileReader };
                var exceptionReport = builder.Build();

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