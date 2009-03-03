using System;
using System.Reflection;
using ExceptionReporter;
using ExceptionReporter.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	// - NB Resharper's test runner addin can't run these tests, however TestMatrix can (if option 'Apartment State=STA')
	// - We can't have a reference to both Wpf and WinForms in the test project (won't compile, amiguous references would result)
	//   so only WinForms, currently only is explicitly tested here (if anyone changes it to include Wpf, these tests will fail)
	// - NB we don't/can't/shouldn't make any explicit references to ExceptionReportView or InternalExceptionView here
	[TestFixture]
	public class ViewFactory_Tests
	{
		private Assembly _assembly;
		private ViewResolver _viewResolver;

// ReSharper disable UnusedMember.Global

		[SetUp]
		public void SetUp()
		{
			_assembly = Assembly.Load(new AssemblyName("ExceptionReporter.WinForms"));
			_viewResolver = new ViewResolver(_assembly);
		}

// ReSharper restore UnusedMember.Global

		[Test]
		public void CanResolve_WinForms_IInternalExceptionView_Interface()
		{
			Type view = _viewResolver.Resolve<IInternalExceptionView>();
			Assert.That(view.ToString(), Text.StartsWith("ExceptionReporter.WinForms.Views.InternalExceptionView"));
		}

		[Test]
		public void CanCreate_WinForms_InternalExceptionView()
		{
			var view = ViewFactory.Create<IInternalExceptionView>(_viewResolver);
			Assert.That(view.ToString(), Text.StartsWith("ExceptionReporter.WinForms.Views.InternalExceptionView"));
		}

		[Test]
		public void CanResolve_WinForms_IExceptionReportView_Interface()
		{
			var exceptionReportView = _viewResolver.Resolve<IExceptionReportView>();

			Assert.That(exceptionReportView.ToString(), Text.StartsWith("ExceptionReporter.WinForms.Views.ExceptionReportView"));
		}

		[Test]
		[Ignore("Looks like the IE WebControl thingy on the form, prevents us from instantiating the class here")]
		public void CanCreate_WinForms_ExceptionReportView()
		{
			var view = ViewFactory.Create<IExceptionReportView>(_viewResolver, new ExceptionReportInfo());

			Assert.That(view.ToString(), Text.StartsWith("ExceptionReporter.WinForms.Views.ExceptionReportView"));
		}

		[Test]
//		[Ignore("this should pass if the LoadFile path is correct, haven't decided the best way to make the dll findable")]
		public void CanResolve_Wpf_InternalExceptionView()
		{
			_assembly = Assembly.Load(new AssemblyName("ExceptionReporter.WinForms"));
			_viewResolver = new ViewResolver(_assembly);

			Assert.That(_viewResolver.Resolve<IExceptionReportView>().ToString(),
				Is.EqualTo("ExceptionReporter.WinForms.Views.ExceptionReportView"));
		}
	}
}