using System;
using System.Reflection;

namespace ExceptionReporting
{
	public class ExceptionReportInfo
	{
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public bool EnumeratePrinters { get; set; }
		public string SmtpFromAddress { get; set; }
		public string SmtpServer { get; set; }
		public Exception Exception { get; set; }
		public bool isForPrinting { get; set; }
		public string ContactEmail { get; set; }
		public string GeneralInfo { get; set; }
		public string AppName { get; set; }
		public string AppVersion { get; set; }
		public string RegionInfo { get; set; }
		public string MachineName { get; set; }
		public string UserName { get; set; }
		public DateTime ExceptionDate { get; set; }
		public string UserExplanation { get; set; }
		public Exception RootException { get; set; }
		public Assembly AppAssembly { get; set; }
		public string WebUrl { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }

		public bool ShowGeneralTab { get; set; }
		public bool ShowSettingsTab { get; set; }
		public bool ShowContactTab { get; set; }
		public bool ShowExceptionsTab { get; set; }
		public bool ShowEnvironmentTab { get; set; }
		public bool ShowAssembliesTab { get; set; }

		public string Email { get; set; }
		public string UserExplanationLabel { get; set; }
		public string ExceptionOccuredMessage { get; set; }
		public string ContactMessageBottom { get; set; }
		public string ContactMessageTop { get; set; }

		/// <summary>
		/// Enumerated type used to represent supported e-mail mechanisms 
		/// </summary>
		public enum slsMailType
		{
			SimpleMAPI,
			SMTP
		};

		public slsMailType MailType { get; set; }
	}
}