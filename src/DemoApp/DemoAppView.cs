using System;
using System.IO;
using System.Windows.Forms;

namespace ExceptionReporting.DemoApp
{
	public partial class DemoAppView : Form
	{
		public DemoAppView()
		{
			InitializeComponent();

			btnShowExceptionReport.Click += Throw_Click;
		}

		private static void Throw_Click(object sender, EventArgs e)
		{
			try
			{
				SomeMethod();
				
			}
			catch (Exception ex)
			{
				var exceptionReporter = new ExceptionReporter();
				exceptionReporter.ReadConfig();
//				exceptionReporter.Info.ShowSysInfoTab = false;
				exceptionReporter.Show(ex);
			}

			Application.Exit();
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