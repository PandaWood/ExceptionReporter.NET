using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ExceptionReporting.DemoApp
{
	public partial class DemoAppView : Form
	{
		public DemoAppView()
		{
			InitializeComponent();

			urlConfigured.Click += Throw_Configured_Click;
			urlDefault.Click += Throw_NonConfigured_Click;
		}

		private static void Throw_NonConfigured_Click(object sender, EventArgs e)
		{
			ShowExceptionReporter(false);
		}

		private static void Throw_Configured_Click(object sender, EventArgs e)
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
				{
					exceptionReporter.ReadConfig();
					exceptionReporter.Config.ShowButtonIcons = false;
					exceptionReporter.Config.ShowFlatButtons = false;
					exceptionReporter.Config.BackgroundColor = Color.White;
				}
				
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
			var exception = new IOException("Unable to establish a connection with the Foo bank account service. The error number is #FFF474678.",
												new Exception("This is an Inner Exception message - with a message that is not too small but then again, perhaps it should be smaller"));
			throw exception;
		}
	}
}