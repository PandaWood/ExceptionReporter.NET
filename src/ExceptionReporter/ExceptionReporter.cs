using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Extensions;
using ExceptionReporting.Views;

namespace ExceptionReporting
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof (ExceptionReporter), "ExceptionReporter.ico")]
	public class ExceptionReporter : Component
	{
		private readonly ExceptionReportInfo _info;

		public ExceptionReporter(IContainer container) : this()
		{
			container.Add(this);
		}

		public ExceptionReporter()
		{
			InitializeComponent();

			_info = new ExceptionReportInfo
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
				_info.Exception = exception;

				var reportView = new ExceptionReportView(_info);
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
		/// \<add key="ER_CONTACT_EMAIL" value="TheAddress@theserver.com"/> 
		/// \<add key="ER_CONTACT_PHONE" value="012 3456 7890"/> 
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
			_info.ContactEmail = ConfigurationManager.AppSettings.Get("ER_CONTACT_EMAIL").ReturnStringOr(_info.ContactEmail);
			_info.WebUrl = ConfigurationManager.AppSettings.Get("ER_CONTACT_WEB").ReturnStringOr(_info.WebUrl);
			_info.Phone = ConfigurationManager.AppSettings.Get("ER_CONTACT_PHONE").ReturnStringOr( _info.Phone);
			_info.Fax = ConfigurationManager.AppSettings.Get("ER_CONTACT_FAX").ReturnStringOr( _info.Fax);

			_info.ShowAssembliesTab = ConfigurationManager.AppSettings.Get("ER_SHOW_GENERAL").ReturnBoolOr(_info.ShowAssembliesTab);
			_info.ShowExceptionsTab = ConfigurationManager.AppSettings.Get("ER_SHOW_EXCEPTIONS").ReturnBoolOr(_info.ShowExceptionsTab);
			_info.ShowAssembliesTab = ConfigurationManager.AppSettings.Get("ER_SHOW_ASSEMBLIES").ReturnBoolOr(_info.ShowAssembliesTab);
			_info.ShowConfigTab = ConfigurationManager.AppSettings.Get("ER_SHOW_SETTINGS").ReturnBoolOr(_info.ShowConfigTab);
			_info.ShowSysInfoTab = ConfigurationManager.AppSettings.Get("ER_SHOW_ENVIRONMENT").ReturnBoolOr(_info.ShowSysInfoTab);
			_info.ShowContactTab = ConfigurationManager.AppSettings.Get("ER_SHOW_CONTACT").ReturnBoolOr(_info.ShowContactTab);
		}

		private void ReadMailConfig()
		{
			ReadMailType();
			ReadMailValues();
		}

		private void ReadMailValues()
		{
			_info.SmtpServer = ConfigurationManager.AppSettings.Get("ER_SMTP_SERVER").ReturnStringOr(_info.SmtpServer);
			_info.SmtpUsername = ConfigurationManager.AppSettings.Get("ER_SMTP_USERNAME").ReturnStringOr( _info.SmtpUsername);
			_info.SmtpPassword = ConfigurationManager.AppSettings.Get("ER_SMTP_PASSWORD").ReturnStringOr( _info.SmtpPassword);
			_info.SmtpFromAddress = ConfigurationManager.AppSettings.Get("ER_SMTP_FROM").ReturnStringOr( _info.SmtpFromAddress);
			_info.EmailSendAddress = ConfigurationManager.AppSettings.Get("ER_SEND_ADDRESS").ReturnStringOr(_info.EmailSendAddress);
		}

		private void ReadMailType()
		{
			string strCompare = "SMTP";
			const string strCompare2 = "SIMPLEMAPI";
			if ((ConfigurationManager.AppSettings.Get("ER_MAIL_TYPE") == null)) return;

			if (strCompare.Equals(ConfigurationManager.AppSettings.Get("ER_MAIL_TYPE")))
			{
				_info.MailType = ExceptionReportInfo.slsMailType.SMTP;
			}
			strCompare = "MAPI";
			if (strCompare.Equals(ConfigurationManager.AppSettings.Get("ER_MAIL_TYPE"))
			    || strCompare2.Equals(ConfigurationManager.AppSettings.Get("ER_MAIL_TYPE")))
			{
				_info.MailType = ExceptionReportInfo.slsMailType.SimpleMAPI;
			}
		}

		private void ReadInterfaceMessageConfig()
		{
			_info.ExceptionOccuredMessage = ConfigurationManager.AppSettings.Get("ER_GENERAL_MESSAGE").ReturnStringOr(_info.ExceptionOccuredMessage);
			_info.UserExplanationLabel = ConfigurationManager.AppSettings.Get("ER_EXPLANATION_MESSAGE").ReturnStringOr(_info.UserExplanationLabel);
			_info.ContactMessageTop = ConfigurationManager.AppSettings.Get("ER_CONTACT_MESSAGE_1").ReturnStringOr(_info.ContactMessageTop);
			_info.ContactMessageBottom = ConfigurationManager.AppSettings.Get("ER_CONTACT_MESSAGE_2").ReturnStringOr(_info.ContactMessageBottom);
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