using System;
using System.Reflection;

namespace ExceptionReporting
{
	public class ExceptionReportInfo
	{
		public bool isForPrinting { get; set; }
		public string Email { get; set; }
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
		public string Url { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }

		public bool ShowGeneralTab { get; set; }
		public bool ShowSettingsTab { get; set; }
		public bool ShowContactTab { get; set; }
		public bool ShowExceptionsTab { get; set; }
		public bool ShowEnvironmentTab { get; set; }
		public bool ShowAssembliesTab { get; set; }
	}
}