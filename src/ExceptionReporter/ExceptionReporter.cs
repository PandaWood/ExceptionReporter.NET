using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using ExceptionReporting.Views;

namespace ExceptionReporting
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof (ExceptionReporter), "ExceptionReporter.ico")]
	public class ExceptionReporter : Component
	{
		private readonly ExceptionReportInfo _info = new ExceptionReportInfo();

		public ExceptionReporter(IContainer container) : this()
		{
			container.Add(this);
		}

		public ExceptionReporter()
		{
			InitializeComponent();

			_info.UserExplanationLabel = "Please enter a brief explanation of events leading up to this exception";
			_info.ExceptionOccurredMessage = "An exception has occured in this application";
			_info.ContactMessageBottom = "The information shown on this form describing the error and envrionment may be relevant when contacting support";
			_info.ContactMessageTop = "The following details can be used to obtain support for this application";

			_info.ShowExceptionsTab = true;
			_info.ShowContactTab = true;
			_info.ShowSettingsTab = true;
			_info.ShowAssembliesTab = true;
			_info.EnumeratePrinters = true;
			_info.ShowEnvironmentTab = true;
			_info.ShowGeneralTab = true;
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
				var reportView = new ExceptionReportView(_info);
				reportView.ShowException(exception, Assembly.GetCallingAssembly());
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
			_info.ContactEmail = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_EMAIL"), _info.ContactEmail);
			_info.Url = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_WEB"), _info.Url);
			_info.Phone = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_PHONE"), _info.Phone);
			_info.Fax = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_FAX"), _info.Fax);
			_info.ShowAssembliesTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_GENERAL"), _info.ShowAssembliesTab);
			_info.ShowExceptionsTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_EXCEPTIONS"), _info.ShowExceptionsTab);
			_info.ShowAssembliesTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_ASSEMBLIES"), _info.ShowAssembliesTab);
			_info.ShowSettingsTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_SETTINGS"), _info.ShowSettingsTab);
			_info.ShowEnvironmentTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_ENVIRONMENT"), _info.ShowEnvironmentTab);
			_info.ShowContactTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_CONTACT"), _info.ShowContactTab);
			_info.EnumeratePrinters = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ENUMERATE_PRINTERS"), _info.EnumeratePrinters);
		}

		private void ReadMailConfig()
		{
			ReadMailType();
			ReadMailValues();
		}

		private void ReadMailValues()
		{
			_info.SmtpServer = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_SERVER"), _info.SmtpServer);
			_info.SmtpUsername = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_USERNAME"), _info.SmtpUsername);
			_info.SmtpPassword = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_PASSWORD"), _info.SmtpPassword);
			_info.SmtpFromAddress = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_FROM"), _info.SmtpFromAddress);
			_info.Email = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SEND_ADDRESS"), _info.Email);
		}

		private void ReadMailType()
		{
			string strCompare = "SMTP";
			const string strCompare2 = "SIMPLEMAPI";
			if ((ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE") == null)) return;

			if (strCompare.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE")))
			{
				_info.MailType = ExceptionReportInfo.slsMailType.SMTP;
			}
			strCompare = "MAPI";
			if (strCompare.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE"))
			    || strCompare2.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE")))
			{
				_info.MailType = ExceptionReportInfo.slsMailType.SimpleMAPI;
			}
		}

		private void ReadInterfaceMessageConfig()
		{
			_info.ExceptionOccurredMessage = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_GENERAL_MESSAGE"), _info.ExceptionOccurredMessage);
			_info.UserExplanationLabel = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_EXPLANATION_MESSAGE"), _info.UserExplanationLabel);
			_info.ContactMessageTop = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_MESSAGE_1"), _info.ContactMessageTop);
			_info.ContactMessageBottom = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_MESSAGE_2"), _info.ContactMessageBottom);
		}

		/// <summary>
		/// Returns the newString if it is not NULL or 0 length, otherwise the currentString value is returned
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