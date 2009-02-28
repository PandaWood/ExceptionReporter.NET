using System.Reflection;
using ExceptionReporter;
using ExceptionReporter.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ViewInjector_Tests
	{
		private Assembly _assembly;
		private ViewInjector _viewInjector;
		// NB Resharper isn't able to run this test, however TestMatrix can, because it has an option for 'Apartment State=STA'
		// We can't have a reference to both Wpf and WinForms in the test project, so we only test one of them (currently)
		// NB we don't/can't/shouldn't make any explicit references to ExceptionReportView or even the Wpf/WinForms assembly

// ReSharper disable UnusedMember.Global
		[SetUp]
		public void SetUp()
		{
			_assembly = Assembly.Load(new AssemblyName("ExceptionReporter.Wpf"));
			_viewInjector = new ViewInjector(_assembly);
		}
// ReSharper restore UnusedMember.Global

		[Test]
		public void CanResolve_Wpf_InternalExceptionView()
		{
			Assert.That(_viewInjector.Resolve<IInternalExceptionView>(null).ToString(), 
				Is.EqualTo("ExceptionReporter.Wpf.Views.InternalExceptionView"));
		}

		[Test]
		public void CanResolve_Wpf_ExceptionReportView()
		{
			var exceptionReportView = _viewInjector.Resolve<IExceptionReportView>(new ExceptionReportInfo());

			Assert.That(exceptionReportView.ToString(), Is.EqualTo("ExceptionReporter.Wpf.Views.ExceptionReportView"));
		}

		[Test]
		[Ignore("my first attempt to test WinForms explicitly (have to find the assembly because we can't include both wpf and winforms)")]
		public void CanResolve_WinForms_InternalExceptionView()
		{
			Assembly assembly = Assembly.LoadFile(@"S:\Projects\ExceptionReporter\src\ExceptionReporter\bin\Debug\ExceptionReporter.WinForms.dll");
			var viewInjector = new ViewInjector(assembly);

			Assert.That(viewInjector.Resolve<IInternalExceptionView>(null).ToString(),
				Is.EqualTo("ExceptionReporter.WinForms.Views.InternalExceptionView"));
		}
	}
}