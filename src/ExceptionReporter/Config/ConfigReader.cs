using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using ExceptionReporting.Core;
using ExceptionReporting.Extensions;

namespace ExceptionReporting.Config
{
	internal class ConfigReader
	{
		const string SMTP = "SMTP";

		private readonly ExceptionReportInfo _info;

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

		private static string GetConfigSetting(string sectionName, string configName)
		{
			NameValueCollection sections = ConfigurationManager.GetSection(@"ExceptionReporter/" + sectionName) as NameValueCollection;
			if (sections == null) return string.Empty;

			string setting = sections[configName];
			return setting;
		}

		/// <summary>Read all settings from the config file</summary>
		public void ReadConfig()
		{
			ReadContactSettings();
			ReadTabSettings();
			ReadMailSettings();
			ReadLabelSettings();
		}

		private void ReadContactSettings()
		{
			_info.ContactEmail = GetContactSetting("email").ReturnStringIfNotNullElse(_info.ContactEmail);
			_info.WebUrl = GetContactSetting("web").ReturnStringIfNotNullElse(_info.WebUrl);
			_info.Phone = GetContactSetting("phone").ReturnStringIfNotNullElse(_info.Phone);
			_info.Fax = GetContactSetting("fax").ReturnStringIfNotNullElse(_info.Fax);
		}

		private void ReadTabSettings()
		{
			_info.ShowExceptionsTab = GetTabSetting("exceptions").ReturnBoolfNotNullElse(_info.ShowExceptionsTab);
			_info.ShowAssembliesTab = GetTabSetting("assemblies").ReturnBoolfNotNullElse(_info.ShowAssembliesTab);
			_info.ShowConfigTab = GetTabSetting("config").ReturnBoolfNotNullElse(_info.ShowConfigTab);
			_info.ShowSysInfoTab = GetTabSetting("system").ReturnBoolfNotNullElse(_info.ShowSysInfoTab);
			_info.ShowContactTab = GetTabSetting("contact").ReturnBoolfNotNullElse(_info.ShowContactTab);
		}

		private void ReadMailSettings()
		{
			ReadMailMethod();
			ReadMailValues();
		}

		private void ReadMailValues()
		{
			_info.SmtpServer = GetMailSetting("SmtpServer").ReturnStringIfNotNullElse(_info.SmtpServer);
			_info.SmtpUsername = GetMailSetting("SmtpUsername").ReturnStringIfNotNullElse(_info.SmtpUsername);
			_info.SmtpPassword = GetMailSetting("SmtpPassword").ReturnStringIfNotNullElse(_info.SmtpPassword);
			_info.SmtpFromAddress = GetMailSetting("from").ReturnStringIfNotNullElse(_info.SmtpFromAddress);
			_info.EmailReportAddress = GetMailSetting("to").ReturnStringIfNotNullElse(_info.EmailReportAddress);
		}

		private void ReadMailMethod()
		{
			string mailMethod = GetMailSetting("method");
			if (string.IsNullOrEmpty(mailMethod)) return;

			_info.MailMethod = mailMethod.Equals(SMTP) ? ExceptionReportInfo.EmailMethod.SMTP : ExceptionReportInfo.EmailMethod.SimpleMAPI;
		}

		private void ReadLabelSettings()
		{
			_info.ExceptionOccuredMessage = GetLabelSetting("general").ReturnStringIfNotNullElse(_info.ExceptionOccuredMessage);
			_info.UserExplanationLabel = GetLabelSetting("explanation").ReturnStringIfNotNullElse(_info.UserExplanationLabel);
			_info.ContactMessageTop = GetLabelSetting("contact_top").ReturnStringIfNotNullElse(_info.ContactMessageTop);
			_info.ContactMessageBottom = GetLabelSetting("contact_bottom").ReturnStringIfNotNullElse(_info.ContactMessageBottom);
		}

		public static IList<string> GetConfigKeyValuePairsToString()
		{
			var configList = new List<string>();
			foreach (var configKey in ConfigurationManager.AppSettings.AllKeys)
			{
				string configValue = ConfigurationManager.AppSettings[configKey];
				string keyValueString = string.Format("{0} = {1}", configKey, configValue);
				configList.Add(keyValueString);
			}
			return configList.AsReadOnly();
		}
	}
}