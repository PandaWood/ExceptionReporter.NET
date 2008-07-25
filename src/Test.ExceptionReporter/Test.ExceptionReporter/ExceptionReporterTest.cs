using System;
using System.Reflection;
using ExceptionReporting;
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
			string assemblyNames = stringBuilder.GetReferencedAssemblies(dataAssembly);

			Assert.That(assemblyNames, Is.Not.Null);
			Assert.That(assemblyNames.Length, Is.GreaterThan(0));

			StringAssert.Contains("nunit", assemblyNames);	// not too precise and a little coupled, but better than nothing
			StringAssert.Contains("ExceptionReporter", assemblyNames);
			StringAssert.Contains(Environment.NewLine, assemblyNames);
		}

		[Test]
		public void CanReturn_Nothing_If_Assembly_IsNull()
		{
			var stringBuilder = new ExceptionStringBuilder(new ExceptionReportInfo());
			string assemblyNames = stringBuilder.GetReferencedAssemblies(null);

			Assert.That(assemblyNames, Is.Not.Null);
			Assert.That(assemblyNames.Length, Is.EqualTo(0));
		}

		[Test]
		public void CanCreate_Hierarchy_String_With_Root_And_Inner_Exception()
		{
			var stringBuilder = new ExceptionStringBuilder(new ExceptionReportInfo());
			string hierarchyString = stringBuilder.GetExceptionHierarchy(
				new ArgumentOutOfRangeException("OuterException", new ArgumentNullException("InnerException")));

			Assert.That(hierarchyString, Is.Not.Null);
			Assert.That(hierarchyString, Is.Not.Empty);
			Assert.That(hierarchyString.Length, Is.GreaterThan(0));
			StringAssert.Contains("OuterException", hierarchyString);
			StringAssert.Contains("InnerException", hierarchyString);
		}
	}
}
