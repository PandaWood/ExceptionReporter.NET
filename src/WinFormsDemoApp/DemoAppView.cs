using System;
using System.IO;
using System.Windows.Forms;
using ExceptionReporting;

namespace WinFormsDemoApp
{
	public partial class DemoAppView : Form
	{
		public DemoAppView()
		{
			InitializeComponent();

			urlConfigured.Click += Throw_Configured_Click;
			urlDefault.Click += Throw_NonConfigured_Click;
			urlCustomMessage.Click += Throw_CustomMessage_Click;
			urlConfiguredMultiple.Click += Throw_ConfiguredMultiple_Click;
		}

		private static void Throw_NonConfigured_Click(object sender, EventArgs e)
		{
			ShowExceptionReporter(false);
		}

		private static void Throw_Configured_Click(object sender, EventArgs e)
		{
			ShowExceptionReporter(true);
		}
		private static void Throw_ConfiguredMultiple_Click(object sender, EventArgs e)
		{
			ShowMultipleExceptionReporter();
		}

		private static void Throw_CustomMessage_Click(object sender, EventArgs e)
		{
			try
			{
				SomeMethod();
			}
			catch (Exception exception)
			{
				var file1 = Path.GetTempFileName() + "-file1.txt";	
				var file2 = Path.GetTempFileName() + "-file2.txt";	
				// has to be a registered mime type, and be non-zero bytes, I believe (for MAPI at least) else it won't attach
				File.WriteAllText(file1, "test text file 1");
				File.WriteAllText(file2, "test text file 2");
				var exceptionReporter = new ExceptionReporter();
				exceptionReporter.Config.ShowAssembliesTab = false;
				exceptionReporter.Config.FilesToAttach = new[] { file1, file2 };
				exceptionReporter.Show("This is a custom message. As another test, 2 temp files will be attached to the email sent", exception);
			}
		}


		private static void ShowMultipleExceptionReporter()
		{
			Exception exception1 = null;
			Exception exception2 = null;
			try
			{
				SomeMethod();
			}
			catch (Exception exception)
			{
				exception1 = exception;
			}
			try
			{
				CallAnotherMethod();
			}
			catch (Exception exception)
			{
				exception2 = exception;
			}
			var exceptionReporter = new ExceptionReporter();

			exceptionReporter.Show(exception1, exception2);
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