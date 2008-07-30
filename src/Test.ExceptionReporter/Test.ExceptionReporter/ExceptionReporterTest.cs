using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ExceptionReporting;
using ExceptionReporting.Extensions;
using ExceptionReporting.SystemInfo;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ExceptionReporterTest
	{
		[Test]
		public void CanCreate_Referenced_Assemblies_String()
		{
			Assembly dataAssembly = Assembly.GetExecutingAssembly();

			var stringBuilder = new ExceptionStringBuilder(new ExceptionReportInfo());
			string assemblyNames = stringBuilder.ReferencedAssembliesToString(dataAssembly);

			Assert.That(assemblyNames, Is.Not.Null);
			Assert.That(assemblyNames.Length, Is.GreaterThan(0));

			StringAssert.Contains("nunit", assemblyNames);	// not too precise and coupled to NUnit, but better than nothing
			StringAssert.Contains("ExceptionReporter", assemblyNames);
			StringAssert.Contains(Environment.NewLine, assemblyNames);
			Assert.IsFalse(assemblyNames.Contains("\r\n\r\n"));		// to ensure we don't have any extra lines
		}

		[Test]
		public void CanCreate_Referenced_Assemblies_String_If_Assembly_IsNull()
		{
			var stringBuilder = new ExceptionStringBuilder(new ExceptionReportInfo());
			string assemblyNames = stringBuilder.ReferencedAssembliesToString(null);

			Assert.That(assemblyNames, Is.Not.Null);
			Assert.That(assemblyNames.Length, Is.EqualTo(0));
		}

		[Test]
		public void CanCreate_Hierarchy_String_With_Root_And_Inner_Exception()
		{
			var stringBuilder = new ExceptionStringBuilder(new ExceptionReportInfo());
			string hierarchyString = stringBuilder.ExceptionHierarchyToString(
				new ArgumentOutOfRangeException("OuterException", new ArgumentNullException("InnerException")));

			Assert.That(hierarchyString, Is.Not.Null);
			Assert.That(hierarchyString, Is.Not.Empty);
			Assert.That(hierarchyString.Length, Is.GreaterThan(0));
			StringAssert.Contains("OuterException", hierarchyString);
			StringAssert.Contains("Inner Exception 1", hierarchyString);
			Assert.IsFalse(hierarchyString.EndsWith("\r\n"));		// a test to ensure not appending 2 blank lines at the end
		}

		[Test]
		public void CanBuild_SysInfoString()
		{
			//setup SysInfoResult object
			ICollection<SysInfoResult> results = new List<SysInfoResult>();
			var result = new SysInfoResult("Memory");
			result.Nodes.Add("Physical Memory");
			var resultChild = new SysInfoResult("Bla");
			result.ChildResults.Add(resultChild);
			resultChild.Nodes.Add("Version:2.66");
			results.Add(result);

			// created expected string
			StringBuilder expectedString = new StringBuilder().AppendDottedLine();
			expectedString.AppendLine("[Environment Variables]").AppendLine();
			expectedString.AppendLine("TODO");
			expectedString.AppendDottedLine().AppendLine();

			// TODO the other tests (above) should use ExceptionStringBuilder's design in this way (ie no methods should be public)
			// we force only the chosen method to execution by passing the appropriate ExceptionReportInfo object in

			// make the call
			var stringBuilder = new ExceptionStringBuilder(new ExceptionReportInfo() { ShowSysInfoTab = true }, results);
			string sysInfoString = stringBuilder.Build();
			
			Assert.That(sysInfoString, Is.EqualTo(expectedString.ToString()));
		}
	}
}
