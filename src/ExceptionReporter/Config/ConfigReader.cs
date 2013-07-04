using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using ExceptionReporting.Extensions;
using ExceptionReporting.Core;

namespace ExceptionReporting.Config
{
	/// <summary>
	/// Read ExceptionReport configuration from the main application's (ie not this assembly's) config file
	/// </summary>
	public class ConfigReader
	{
		const string SMTP = "SMTP";

		private readonly ExceptionReportInfo _info;

		/// <param name="reportInfo">the ExceptionReportInfo object to fill with configuration information</param>
		public ConfigReader(ExceptionReportInfo reportInfo)
		{
			_info = reportInfo;
		}

		private static string GetMailSetting(string configName)
		{
			return GetConfigSetting("Email", configName);
		}

		private static string GetContactSetting(string configName)
		{
			return GetConfigSetting("Contact", configName);
		}

		private static string GetTabSetting(string configName)
		{
			return GetConfigSetting("TabsToShow", configName);
		}

		private static string GetLabelSetting(string configName)
		{
			return GetConfigSetting("LabelMessages", configName);
		}

		private static string GetUserInterfaceSetting(string configName)
		{
			return GetConfigSetting("UserInterface", configName);
		}

		private static string GetConfigSetting(string sectionName, string configName)
		{
			var sections = ConfigurationManager.GetSection(@"ExceptionReporter/" + sectionName) as NameValueCollection;
			if (sections == null) return string.Empty;

			return sections[configName];
		}

		/// <summary>Read all settings from the application config file</summary>
		public void ReadConfig()
		{
			ReadContactSettings();
			ReadTabSettings();
			ReadMailSettings();
			ReadLabelSettings();
			ReadUserInterfaceSettings();
		}

		private void ReadContactSettings()
		{
			_info.ContactEmail = GetContactSetting("email").GetString(_info.ContactEmail);
			_info.WebUrl = GetContactSetting("web").GetString(_info.WebUrl);
			_info.Phone = GetContactSetting("phone").GetString(_info.Phone);
			_info.Fax = GetContactSetting("fax").GetString(_info.Fax);
			_info.CompanyName= GetContactSetting("CompanyName").GetString(_info.CompanyName);
		}

		private void ReadTabSettings()
		{
			_info.ShowExceptionsTab = GetTabSetting("exceptions").GetBool(_info.ShowExceptionsTab);
			_info.ShowAssembliesTab = GetTabSetting("assemblies").GetBool(_info.ShowAssembliesTab);
			_info.ShowConfigTab = GetTabSetting("config").GetBool(_info.ShowConfigTab);
			_info.ShowSysInfoTab = GetTabSetting("system").GetBool(_info.ShowSysInfoTab);
			_info.ShowContactTab = GetTabSetting("contact").GetBool(_info.ShowContactTab);
		}

		private void ReadMailSettings()
		{
			ReadMailMethod();
			ReadMailValues();
		}

		private void ReadMailValues()
		{
			_info.SmtpServer = GetMailSetting("SmtpServer").GetString(_info.SmtpServer);
			_info.SmtpUsername = GetMailSetting("SmtpUsername").GetString(_info.SmtpUsername);
			_info.SmtpPassword = GetMailSetting("SmtpPassword").GetString(_info.SmtpPassword);
			_info.SmtpFromAddress = GetMailSetting("from").GetString(_info.SmtpFromAddress);
			_info.EmailReportAddress = GetMailSetting("to").GetString(_info.EmailReportAddress);
		}

		private void ReadMailMethod()
		{
			var mailMethod = GetMailSetting("method");
			if (string.IsNullOrEmpty(mailMethod)) return;

			_info.MailMethod = mailMethod.Equals(SMTP) ? ExceptionReportInfo.EmailMethod.SMTP : 
														 ExceptionReportInfo.EmailMethod.SimpleMAPI;
		}

		private void ReadLabelSettings()
		{
			_info.UserExplanationLabel = GetLabelSetting("explanation").GetString(_info.UserExplanationLabel);
			_info.ContactMessageTop = GetLabelSetting("ContactTop").GetString(_info.ContactMessageTop);
		}

		private void ReadUserInterfaceSettings()
		{
			_info.ShowFlatButtons = GetUserInterfaceSetting("ShowFlatButtons").GetBool(_info.ShowFlatButtons);
			_info.ShowFullDetail = GetUserInterfaceSetting("ShowFullDetail").GetBool(_info.ShowFullDetail);

			if (!_info.ShowFullDetail)
				_info.ShowLessMoreDetailButton = true;	// prevent seeing only the simplified view, position of this line is important

			_info.ShowLessMoreDetailButton = GetUserInterfaceSetting("ShowLessMoreDetailButton").GetBool(_info.ShowLessMoreDetailButton);
			_info.ShowButtonIcons = GetUserInterfaceSetting("ShowButtonIcons").GetBool(_info.ShowButtonIcons);
			_info.TitleText = GetUserInterfaceSetting("TitleText").GetString(_info.TitleText);
			_info.TakeScreenshot = GetUserInterfaceSetting("TakeScreenshot").GetBool(_info.TakeScreenshot);

			float fontSize;
			var fontSizeAsString = GetUserInterfaceSetting("UserExplanationFontSize");
			if (float.TryParse(fontSizeAsString, NumberStyles.Float, CultureInfo.CurrentCulture.NumberFormat, out fontSize))
			{
				_info.UserExplanationFontSize = fontSize;
			}
		}

		internal static string GetConfigFilePath()
		{
			return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
		}
	}
}