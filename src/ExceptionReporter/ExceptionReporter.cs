using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Views;

namespace ExceptionReporting
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof (ExceptionReporter), "ExceptionReporter.ico")]
	public class ExceptionReporter : Component
	{
		private readonly ExceptionReportInfo _reportInfo;

		public ExceptionReporter(IContainer container) : this()
		{
			container.Add(this);
		}

		public ExceptionReporter()
		{
			InitializeComponent();

			_reportInfo = new ExceptionReportInfo
			              	{
			              		UserExplanationLabel = "Please enter a brief explanation of events leading up to this exception",
			              		ExceptionOccuredMessage = "An exception has occured in this application",
			              		ContactMessageBottom =
			              			"The information shown on this form describing the error and environment may be relevant when contacting support",
			              		ContactMessageTop = "The following details can be used to obtain support for this application",
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
	
		/// <summary>
		/// Show the ExceptionReporter dialog
		/// </summary>
		/// <remarks>The ExceptionReporter will analyze the exception (and app.config) to determine the 
		/// contents of the report</remarks>
		/// <param name="exception">the exception to show</param>
		public void Show(Exception exception)
		{
			if (exception == null) return;

			try
			{
				_reportInfo.Exception = exception;

				var reportView = new ExceptionReportView(_reportInfo);
				reportView.ShowExceptionReport();
			}
			catch (Exception internalException)
			{
				ShowInternalException("Internal Exception occurred while trying to show the Exception Report", internalException);
			}
		}

		/// <summary>
		/// Read settings from the app.config file
		/// <example>
		/// <configuration>
		/// \<appSettings>
		/// \<add key="SLS_ER_CONTACT_EMAIL" value="TheAddress@theserver.com"/> 
		/// \<add key="SLS_ER_CONTACT_PHONE" value="012 3456 7890"/> 
		/// \</appSettings>
		/// \</configuration>
		/// </example>
		/// <remarks> This method must be explicitly called - properties are not automatically read from the config file </remarks>
		/// </summary>
		public void ReadConfig()
		{
			try
			{
				ReadGeneralConfig();
				ReadMailConfig();
				ReadInterfaceMessageConfig();
			}
			catch (Exception ex)
			{
				ShowInternalException("There has been a problem loading Exception Reporter properties from the config file.", ex);
			}
		}

		private void ReadGeneralConfig()
		{
			_reportInfo.ContactEmail = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_EMAIL"), _reportInfo.ContactEmail);
			_reportInfo.WebUrl = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_WEB"), _reportInfo.WebUrl);
			_reportInfo.Phone = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_PHONE"), _reportInfo.Phone);
			_reportInfo.Fax = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_FAX"), _reportInfo.Fax);
			_reportInfo.ShowAssembliesTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_GENERAL"), _reportInfo.ShowAssembliesTab);
			_reportInfo.ShowExceptionsTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_EXCEPTIONS"), _reportInfo.ShowExceptionsTab);
			_reportInfo.ShowAssembliesTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_ASSEMBLIES"), _reportInfo.ShowAssembliesTab);
			_reportInfo.ShowConfigTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_SETTINGS"), _reportInfo.ShowConfigTab);
			_reportInfo.ShowSysInfoTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_ENVIRONMENT"), _reportInfo.ShowSysInfoTab);
			_reportInfo.ShowContactTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_CONTACT"), _reportInfo.ShowContactTab);
			_reportInfo.EnumeratePrinters = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ENUMERATE_PRINTERS"), _reportInfo.EnumeratePrinters);
		}

		private void ReadMailConfig()
		{
			ReadMailType();
			ReadMailValues();
		}

		private void ReadMailValues()
		{
			_reportInfo.SmtpServer = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_SERVER"), _reportInfo.SmtpServer);
			_reportInfo.SmtpUsername = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_USERNAME"), _reportInfo.SmtpUsername);
			_reportInfo.SmtpPassword = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_PASSWORD"), _reportInfo.SmtpPassword);
			_reportInfo.SmtpFromAddress = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_FROM"), _reportInfo.SmtpFromAddress);
			_reportInfo.EmailSendAddress = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SEND_ADDRESS"), _reportInfo.EmailSendAddress);
		}

		private void ReadMailType()
		{
			string strCompare = "SMTP";
			const string strCompare2 = "SIMPLEMAPI";
			if ((ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE") == null)) return;

			if (strCompare.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE")))
			{
				_reportInfo.MailType = ExceptionReportInfo.slsMailType.SMTP;
			}
			strCompare = "MAPI";
			if (strCompare.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE"))
			    || strCompare2.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE")))
			{
				_reportInfo.MailType = ExceptionReportInfo.slsMailType.SimpleMAPI;
			}
		}

		private void ReadInterfaceMessageConfig()
		{
			_reportInfo.ExceptionOccuredMessage = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_GENERAL_MESSAGE"), _reportInfo.ExceptionOccuredMessage);
			_reportInfo.UserExplanationLabel = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_EXPLANATION_MESSAGE"), _reportInfo.UserExplanationLabel);
			_reportInfo.ContactMessageTop = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_MESSAGE_1"), _reportInfo.ContactMessageTop);
			_reportInfo.ContactMessageBottom = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_MESSAGE_2"), _reportInfo.ContactMessageBottom);
		}

		/// <summary>
		/// Returns the newString if it is not NULL or 0 length, otherwise the currentString value is returned
		/// //TODO write the method in a way that avoids passing the same argument
		/// </summary>
		private static string AssignIfNotNull(string newString, string currentString)
		{
			if (newString == null)
				return currentString;
			
			return newString.Length == 0 ? currentString : newString;
		}

		/// <summary>
		/// Returns the true if strNew is a string representing Yes (Y) or false if it represents No (N)
		/// if strNew is NULL or zero length the current value is returned
		/// </summary>
		private static bool AssignBoolValue(string strNew, bool boolCurrent)
		{
			if (strNew == null)
			{
				return boolCurrent;
			}
			if (strNew.Length == 0)
			{
				return boolCurrent;
			}
			if (strNew.Equals("Y"))
			{
				return true;
			}
			if (strNew.Equals("N"))
			{
				return false;
			}
			return boolCurrent;
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