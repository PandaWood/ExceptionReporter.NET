// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System;
using System.Threading;
using System.Windows.Forms;
using ExceptionReporting;

namespace Demo.WinForms
{
  public static class Program
  {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main()
		{

			Application.ThreadException += new ThreadExceptionHandler().ApplicationThreadException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DemoAppView());
		}

		internal class ThreadExceptionHandler
		{
			public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
			{
			ExceptionReporter reporter = new ExceptionReporter();
			reporter.Show(e.Exception);
			}
		}

  }
}