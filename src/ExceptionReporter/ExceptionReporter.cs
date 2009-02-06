using System;
using System.Collections.Generic;
using ExceptionReporting.Config;
using ExceptionReporting.Core;
using ExceptionReporting.Views;

namespace ExceptionReporting
{
//	[ToolboxItem(true)]
//	[DesignerCategory("Components")]	//TODO we don't have the 'component' aspect going yet
//	[ToolboxBitmap(typeof(ExceptionReporter), "ExceptionReporter")]
	public class ExceptionReporter //: Component
	{
		private readonly ExceptionReportInfo _reportInfo;

		/// <summary>
		/// initialise the ExceptionReporter
		/// <remarks>readConfig() should be called (explicitly) if you need to override default config</remarks>
		/// </summary>
		public ExceptionReporter()
		{
			InitializeComponent();

			_reportInfo = new ExceptionReportInfo();
		}

		public ExceptionReportInfo Config
		{
			get { return _reportInfo; }
		}

	    /// <summary>
		/// Show the <see cref="ExceptionReportView"/> dialog
		/// </summary>
		/// <remarks>The <see cref="ExceptionReporter"/> will analyze the <see cref="Exception"/>s and create and show the report dialog.</remarks>
		/// <param name="exceptions">The <see cref="Exception"/>s to show.</param>
		public void Show(params Exception[] exceptions)
		{
            if (exceptions == null)
            {
                return; //TODO consider what is best to do here
            }

	        try
			{
				_reportInfo.Exceptions = new List<Exception>(exceptions);

                var reportView = new ExceptionReportView(_reportInfo);
				reportView.ShowExceptionReport();
			}
			catch (Exception internalException)
			{
				ShowInternalException("An exception occurred while trying to show the Exception Report", internalException);
			}
		}

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
				ShowInternalException("Unable to read ExceptionReporter configuration - default values will be used", ex);
			}
		}

		/// <summary>
		/// A cut-down version of the ExceptionReport to show internal exceptions	
		/// </summary>
		private static void ShowInternalException(string message, Exception ex)
		{
			var exceptionView = new InternalExceptionView();
			exceptionView.ShowException(message, ex);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			new System.ComponentModel.Container();
		}

		#endregion
	}
}
