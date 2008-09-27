using System;
using System.Reflection;
using System.Windows.Forms;
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
		private const string DefaultExplanationLabel = "Please enter a brief explanation of events leading up to this exception";
		private const string DefaultExceptionOccuredMessage = "An exception has occured in this application";
		private const string DefaultContactMessageBottom 
			= "The information shown on this form describing the error and environment may be relevant when contacting support";
		private const string DefaultContactMessageTop = "The following details can be used to obtain support for this application";

		private readonly ExceptionReportInfo _reportInfo;

		/// <summary>
		/// initialise the ExceptionReporter
		/// <remarks>readConfig() must be called explicitly if need to override defaults</remarks>
		/// </summary>
		public ExceptionReporter()
		{
			InitializeComponent();

			_reportInfo = new ExceptionReportInfo
			              	{
			              		UserExplanationLabel = DefaultExplanationLabel,
			              		ExceptionOccuredMessage = DefaultExceptionOccuredMessage,
			              		ContactMessageBottom = DefaultContactMessageBottom,
			              		ContactMessageTop = DefaultContactMessageTop,
			              		ShowExceptionsTab = true,
			              		ShowContactTab = true,
			              		ShowConfigTab = true,
			              		ShowAssembliesTab = true,
			              		EnumeratePrinters = true,
			              		ShowSysInfoTab = true,
			              		ShowGeneralTab = true,
			              		ExceptionDate = DateTime.Now,
			              		UserName = Environment.UserName,
			              		MachineName = Environment.MachineName,
			              		AppName = Application.ProductName,
			              		RegionInfo = Application.CurrentCulture.DisplayName,
			              		AppVersion = Application.ProductVersion,
			              		AppAssembly = Assembly.GetCallingAssembly()
			        	};
		}

		public ExceptionReportInfo Config
		{
			get { return _reportInfo; }
		}

		/// <summary>
		/// Show the ExceptionReporter dialog
		/// </summary>
		/// <remarks>The ExceptionReporter will analyze the exception and create and show the report</remarks>
		/// <param name="exception">the exception to show</param>
		public void Show(Exception exception)
		{
			if (exception == null) return;		//TODO consider what is best to do here

			try
			{
				_reportInfo.Exception = exception;

				var reportView = new ExceptionReportView(_reportInfo);
				reportView.ShowExceptionReport();
			}
			catch (Exception internalException)
			{
				ShowInternalException("An exception occurred while trying to show the Exception Report", internalException);
			}
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