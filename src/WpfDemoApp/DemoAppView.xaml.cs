using System;
using System.IO;
using System.Windows;
using ExceptionReporting;

namespace WpfDemoApp
{
	/// <summary>
	/// Interaction logic for DemoAppView.xaml
	/// </summary>
	public partial class DemoAppView : Window
	{
		public DemoAppView()
		{
			InitializeComponent();
		}

		void ShowDefault(object sender, RoutedEventArgs e)
		{
			ShowExceptionReporter(true);
		}

		private static void ShowExceptionReporter(bool useConfig)
		{
			try
			{
				SomeMethod();
			}
			catch (Exception exception)
			{
				var exceptionReporter = new ExceptionReporter();

				if (useConfig)
					exceptionReporter.ReadConfig();

				exceptionReporter.Show(exception);
			}
		}

		private static void SomeMethod()
		{
			CallAnotherMethod();
		}

		private static void CallAnotherMethod()
		{
			AndAnotherOne();
		}

		private static void AndAnotherOne()
		{
			var exception = new IOException(
				"Unable to establish a connection with the Foo bank account service. The error number is #FFF474678.",
				new Exception(
					"This is an Inner Exception message - with a message that is not too small but perhaps it should be smaller"));
			throw exception;
		}
	}
}
