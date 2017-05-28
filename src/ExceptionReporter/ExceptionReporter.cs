using System;
using System.Reflection;
using ExceptionReporting.Config;
using ExceptionReporting.Core;

// ReSharper disable UnusedMember.Global

namespace ExceptionReporting
{
	/// <summary>
	/// The entry-point (class) to invoking an ExceptionReporter dialog
	/// eg new ExceptionReporter().Show()
	/// </summary>
	public class ExceptionReporter
	{
		private readonly ExceptionReportInfo _reportInfo;
		private ExceptionReporting.IExceptionReportView _view;

		/// <summary>
		/// Initialise the ExceptionReporter
		/// <remarks>readConfig() should be called (explicitly) if you need to override default config settings</remarks>
		/// </summary>
		public ExceptionReporter()
		{
			var callingAssembly = Assembly.GetCallingAssembly();

			_reportInfo = new ExceptionReportInfo { AppAssembly = callingAssembly };
		}

		/// <summary>
		/// Public access to configuration
		/// </summary>
		public ExceptionReportInfo Config
		{
			get { return _reportInfo; }
		}

		/// <summary>
		/// Show the ExceptionReport dialog
		/// </summary>
		/// <remarks>The <see cref="ExceptionReporter"/> will analyze the <see cref="Exception"/>s and 
		/// create and show the report dialog.</remarks>
		/// <param name="exceptions">The <see cref="Exception"/>s to show.</param>
		// ReSharper disable MemberCanBePrivate.Global
		public void Show(params Exception[] exceptions)
		{
			if (exceptions == null) return;		//TODO perhaps show a dialog that says "No exception to show" ?

			try
			{
				_reportInfo.SetExceptions(exceptions);
				_view = new WinForms.Views.ExceptionReportView( _reportInfo);
				_view.ShowExceptionReport();
			}
			catch (Exception internalException)
			{
                System.Windows.Forms.MessageBox.Show(internalException.Message);
			}
		}
		// ReSharper restore MemberCanBePrivate.Global

		/// <summary>
		/// Show the ExceptionReport dialog with a custom message instead of the Exception's Message property
		/// </summary>
		/// <param name="customMessage">custom (exception) message</param>
		/// <param name="exceptions">The exception/s to display in the exception report</param>
		public void Show(string customMessage, params Exception[] exceptions)
		{
			_reportInfo.CustomMessage = customMessage;
			Show(exceptions);
		}

		/// <summary>
		/// Read settings from config file
		/// <remarks> This method must be explicitly called - config is not automatically read</remarks>
		/// </summary>
		public void ReadConfig()
		{
			try
			{
				var configReader = new ConfigReader(_reportInfo);
				configReader.ReadConfig();
			}
			catch (Exception ex)
			{
                System.Windows.Forms.MessageBox.Show(ex.Message);    // last resort, hope it never happens
			}
		}
	}
}

// ReSharper restore UnusedMember.Global
