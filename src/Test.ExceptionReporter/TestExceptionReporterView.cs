using System;
using System.Windows.Forms;
using SLSExceptionReporter;

namespace TestExceptionReporter
{
	public partial class TestExceptionReporterView : Form
	{
		public TestExceptionReporterView()
		{
			InitializeComponent();

			button1.Click += Throw_Click;
		}

		private static void Throw_Click(object sender, EventArgs e)
		{
			try
			{
				throw new Exception("Something went boom!");
			}
			catch (Exception ex)
			{
				var exceptionReporter = new ExceptionReporter();
				exceptionReporter.LoadPropertiesFromConfig();
				exceptionReporter.DisplayException(ex);
			}
		}
	}
}