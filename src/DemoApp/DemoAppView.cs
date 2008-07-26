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

			button1.Click += Throw_Click;
		}

		private static void Throw_Click(object sender, EventArgs e)
		{
			try
			{
				var exception = new IOException("Unable to establish a connection with something interesting. Incidentally, the error message number is #FFF474. Hope that helps.",
				                                new Exception("This is an Inner Exception message"));
				throw exception;
			}
			catch (Exception ex)
			{
				var exceptionReporter = new ExceptionReporter();
				exceptionReporter.ReadConfig();
				exceptionReporter.Show(ex);
			}
		}
	}
}