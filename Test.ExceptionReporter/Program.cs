using System;
using System.Windows.Forms;
using TestExceptionReporter;

namespace Test.ExceptionReporter
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new TestExceptionReporterView());
		}
	}
}
