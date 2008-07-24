using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Views;

//-------------------------------------------------------------------------
// ExceptionReporter - Error Reporting Component for .Net
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//--------------------------------------------------------------------------

namespace ExceptionReporting
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof (ExceptionReporter), "ExceptionReporter.ico")]
	public class ExceptionReporter : Component
	{
		// Enumerated type used to represent supported e-mail mechanisms
		public enum slsMailType
		{
			SimpleMAPI,
			SMTP
		} ;

		public ExceptionReporter(IContainer container) : this()
		{
			container.Add(this);
		}

		public ExceptionReporter()
		{
			InitializeComponent();

			ExplanationMessage = "Please enter a brief explanation detailing the actions and events leading up to the occurrence of this error.";
			GeneralMessage = "An error has occured in this application.";
			ContactMessageBottom = "The information shown on this form describing the error and envrionment may be relevant when contacting support.";
			ContactMessageTop = "The following details can be used to obtain support for this application..";

			ShowPrintButton = true;
			ShowSaveButton = true;
			ShowEmailButton = true;
			ShowCopyButton = true;
			ShowExceptionsTab = true;
			ShowContactTab = true;
			ShowSettingsTab = true;
			ShowAssembliesTab = true;
			EnumeratePrinters = true;
			ShowEnvironmentTab = true;
			ShowGeneralTab = true;
		}

		public bool ShowGeneralTab { get; set; }
		public bool ShowEnvironmentTab { get; set; }
		public bool EnumeratePrinters { get; set; }
		public bool ShowAssembliesTab { get; set; }
		public string SMTPServer { get; set; }
		public string SMTPUsername { get; set; }
		public string SMTPPassword { get; set; }
		public string SMTPFromAddress { get; set; }
		public string SendEmailAddress { get; set; }
		public bool ShowSettingsTab { get; set; }
		public bool ShowContactTab { get; set; }
		public bool ShowExceptionsTab { get; set; }
		public bool ShowCopyButton { get; set; }
		public bool ShowEmailButton { get; set; }
		public bool ShowSaveButton { get; set; }
		public bool ShowPrintButton { get; set; }
		public string ContactEmail { get; set; }
		public string ContactWeb { get; set; }
		public string ContactPhone { get; set; }
		public string ContactFax { get; set; }
		public string ContactMessageTop { get; set; }
		public string ContactMessageBottom { get; set; }
		public string GeneralMessage { get; set; }
		public slsMailType MailType { get; set; }
		public string ExplanationMessage { get; set; }

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

		/// </summary>
		/// <example>An example set of entries in the config file (ie appname.exe.config) may look like:
		/// <?xml version="1.0" encoding="utf-8" ?>
		/// <configuration>
		/// \<appSettings>
		/// \<add key="SLS_ER_CONTACT_EMAIL" value="TheAddress@theserver.com"/> 
		/// \<add key="SLS_ER_CONTACT_PHONE" value="012 3456 7890"/> 
		/// \</appSettings>
		/// \</configuration>
		/// </example>
		/// <remarks>This method provides a flexible way of changing the Exception Reporters behaviour without having 
		/// to change properties on a form at design time and recompile.
		/// Important: This method must be explicitly called within your code, properties are not automatically read from the config file</remarks>
		public void LoadPropertiesFromConfig()
		{
			try
			{
				ContactEmail = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_EMAIL"), ContactEmail);
				ContactWeb = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_WEB"), ContactWeb);
				ContactPhone = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_PHONE"), ContactPhone);
				ContactFax = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_FAX"), ContactFax);
				ShowAssembliesTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_GENERAL"), ShowAssembliesTab);
				ShowExceptionsTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_EXCEPTIONS"), ShowExceptionsTab);
				ShowAssembliesTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_ASSEMBLIES"), ShowAssembliesTab);
				ShowSettingsTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_SETTINGS"), ShowSettingsTab);
				ShowEnvironmentTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_ENVIRONMENT"), ShowEnvironmentTab);
				ShowContactTab = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SHOW_CONTACT"), ShowContactTab);
				ShowPrintButton = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_PRINT_BUTTON"), ShowPrintButton);
				ShowSaveButton = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_SAVE_BUTTON"), ShowSaveButton);
				ShowCopyButton = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_COPY_BUTTON"), ShowCopyButton);
				ShowEmailButton = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ER_EMAIL_BUTTON"), ShowEmailButton);
				EnumeratePrinters = AssignBoolValue(ConfigurationManager.AppSettings.Get("SLS_ENUMERATE_PRINTERS"), EnumeratePrinters);

				// determine the Mail Type (SMTP or Simple MAPI)
				string strCompare = "SMTP";
				const string strCompare2 = "SIMPLEMAPI";
				if (!(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE") == null))
				{
					if (strCompare.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE")))
					{
						MailType = slsMailType.SMTP;
					}
					strCompare = "MAPI";
					if (strCompare.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE"))
						|| strCompare2.Equals(ConfigurationManager.AppSettings.Get("SLS_ER_MAIL_TYPE")))
					{
						MailType = slsMailType.SimpleMAPI;
					}
				}
				// mail settings
				SMTPServer = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_SERVER"), SMTPServer);
				SMTPUsername = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_USERNAME"), SMTPUsername);
				SMTPPassword = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_PASSWORD"), SMTPPassword);
				SMTPFromAddress = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SMTP_FROM"), SMTPFromAddress);
				SendEmailAddress = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_SEND_ADDRESS"), SendEmailAddress);

				// messages displayed on the form
				GeneralMessage = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_GENERAL_MESSAGE"), GeneralMessage);
				ExplanationMessage = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_EXPLANATION_MESSAGE"), ExplanationMessage);
				ContactMessageTop = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_MESSAGE_1"), ContactMessageTop);
				ContactMessageBottom = AssignIfNotNull(ConfigurationManager.AppSettings.Get("SLS_ER_CONTACT_MESSAGE_2"), ContactMessageBottom);
			}
			catch (Exception ex)
			{
				handleError("There has been a problem loading Exception Reporter properties from the config file.", ex);
			}
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

		public void DisplayException(Exception ex)
		{
			if (ex == null)
			{
				MessageBox.Show("The Exception Reporter was called, but no Exception was provided!",
				                "Exception Reporter called without an Exception");
				return;
			}

			try
			{
				var exceptionReportView = new ExceptionReportView
				                   	{
				                   		EnumeratePrinters = EnumeratePrinters,
				                   		ContactEmail = ContactEmail,
				                   		ContactWeb = ContactWeb,
				                   		ContactPhone = ContactPhone,
				                   		ContactFax = ContactFax,
				                   		ShowAssembliesTab = ShowAssembliesTab,
				                   		ShowEnvironmentTab = ShowEnvironmentTab,
				                   		ShowGeneralTab = ShowGeneralTab,
				                   		ShowSettingsTab = ShowSettingsTab,
				                   		ShowContactTab = ShowContactTab,
				                   		ShowExceptionsTab = ShowExceptionsTab,
				                   		ShowPrintButton = ShowPrintButton,
				                   		ShowCopyButton = ShowCopyButton,
				                   		ShowSaveButton = ShowSaveButton,
				                   		ShowEmailButton = ShowEmailButton,
				                   		MailType = MailType,
				                   		SMTPServer = SMTPServer,
				                   		SMTPUsername = SMTPUsername,
				                   		SMTPPassword = SMTPPassword,
				                   		SMTPFromAddress = SMTPFromAddress,
				                   		SendEmailAddress = SendEmailAddress,
				                   		GeneralMessage = GeneralMessage,
				                   		ExplanationMessage = ExplanationMessage,
				                   		ContactMessageBottom = ContactMessageBottom,
				                   		ContactMessageTop = ContactMessageTop
				                   	};

				exceptionReportView.displayException(ex, Assembly.GetCallingAssembly());
			}
			catch (Exception exc)
			{
				handleError("There has been a problem displaying the Exception Reporter", exc);
			}
		}

		/// <summary>
		/// Handles application error by displaying a simple error form to the user
		/// The form contains 2 tabs, one for simple information and the other for a more detailed
		/// exception message
		/// </summary>
		private static void handleError(string strMessage, Exception ex)
		{
			var exceptionView = new SimpleExceptionView();
			exceptionView.ShowException(strMessage, ex);
		}
	}
}