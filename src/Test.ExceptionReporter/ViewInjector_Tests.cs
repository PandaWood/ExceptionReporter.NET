using System.Reflection;
using ExceptionReporter;
using ExceptionReporter.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	// - NB Resharper's test runner addin can't run these tests, however TestMatrix can (if option 'Apartment State=STA')
	// - We can't have a reference to both Wpf and WinForms in the test project (won't compile, amiguous references would result)
	//   so only WinForms, currently only that is explicitly tested here (consequently if anyone changes it, these tests will fail)
	// - NB we don't/can't/shouldn't make any explicit references to ExceptionReportView or even the Wpf/WinForms assembly here
	[TestFixture]
	public class ViewInjector_Tests
	{
		private Assembly _assembly;
		private ViewInjector _viewInjector;
		

// ReSharper disable UnusedMember.Global
		[SetUp]
		public void SetUp()
		{
			_assembly = Assembly.Load(new AssemblyName("ExceptionReporter.WinForms"));
			_viewInjector = new ViewInjector(_assembly);
		}
// ReSharper restore UnusedMember.Global

		[Test]
		public void CanResolve_WinForms_InternalExceptionView()
		{
			Assert.That(_viewInjector.Resolve<IInternalExceptionView>().ToString(),
				Text.StartsWith("ExceptionReporter.WinForms.Views.InternalExceptionView"));
		}

		[Test]
		public void CanResolve_WinForms_ExceptionReportView()
		{
			var exceptionReportView = _viewInjector.Resolve<IExceptionReportView>(new ExceptionReportInfo());

			Assert.That(exceptionReportView.ToString(), Text.StartsWith("ExceptionReporter.WinForms.Views.ExceptionReportView"));
		}

		[Test]
		[Ignore("test WinForms explicitly (have to find the assembly because we can't include both wpf and winforms)")]
		public void CanResolve_Wpf_InternalExceptionView()
		{
			Assembly assembly = Assembly.LoadFile(@"S:\Projects\ExceptionReporter\src\ExceptionReporter\bin\Debug\ExceptionReporter.Wpf.dll");
			var viewInjector = new ViewInjector(assembly);

			Assert.That(viewInjector.Resolve<IInternalExceptionView>().ToString(),
				Is.EqualTo("ExceptionReporter.WinForms.Views.InternalExceptionView"));
		}
	}
}