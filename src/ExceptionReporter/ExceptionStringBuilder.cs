using System;
using System.Configuration;
using System.Reflection;
using System.Text;
using ExceptionReporting.Extensions;

namespace ExceptionReporting
{
	public class ExceptionStringBuilder
	{
		public ExceptionReportInfo _reportInfo;
		private StringBuilder _stringBuilder;

		/// <summary>
		/// the (only) constructor
		/// </summary>
		/// <param name="reportInfo">ExceptionReportInfo </param>
		public ExceptionStringBuilder(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}

		/// <summary>
		/// Build the exception string
		/// </summary>
		public string Build()
		{
			_stringBuilder = new StringBuilder();

			BuildGeneralInfo();
			BuildExceptionInfo();
            BuildAssemblyInfo();
			BuildSettingsInfo();
			BuildEnvironmentInfo();
			BuildContactInfo();

			return _stringBuilder.ToString();
		}

		private void BuildGeneralInfo()
		{
			if (!_reportInfo.ShowGeneralTab) return;

			if (!_reportInfo.isForPrinting)
			{
				_stringBuilder.AppendLine(_reportInfo.GeneralInfo)
					.AppendLine().AppendDottedLine().AppendLine();
			}

			_stringBuilder.AppendLine("General")
				.AppendLine()
				.AppendLine("Application: " + _reportInfo.AppName)
				.AppendLine("Version:     " + _reportInfo.AppVersion)
				.AppendLine("Region:      " + _reportInfo.RegionInfo)
				.AppendLine("Machine:     " + _reportInfo.MachineName)
				.AppendLine("User:        " + _reportInfo.UserName)
				.AppendDottedLine();

			if (!_reportInfo.isForPrinting)
			{
				_stringBuilder.AppendLine()
					.AppendLine("Date: " + _reportInfo.ExceptionDate.ToShortDateString())
					.AppendLine("Time: " + _reportInfo.ExceptionDate.ToShortTimeString())
					.AppendDottedLine();
			}

			_stringBuilder.AppendLine("User Explanation")
				.AppendLine()
				.AppendFormat("{0} said \"{1}\"", _reportInfo.UserName, _reportInfo.UserExplanation)
				.AppendLine().AppendDottedLine().AppendLine();
		}

		private void BuildExceptionInfo()
		{
			if (!_reportInfo.ShowExceptionsTab) return;

			_stringBuilder.AppendLine("Exceptions")
				.AppendLine()
				.AppendLine(ExceptionHierarchyToString(_reportInfo.Exception))
				.AppendLine().AppendDottedLine().AppendLine();
		}

		private void BuildAssemblyInfo()
		{
			if (!_reportInfo.ShowAssembliesTab) return;

			_stringBuilder.AppendLine("Assemblies")
				.AppendLine()
				.AppendLine(ReferencedAssembliesToString(_reportInfo.AppAssembly))
				.AppendDottedLine().AppendLine();
		}

		private void BuildSettingsInfo()
		{
			if (!_reportInfo.ShowSettingsTab) return;

			_stringBuilder.AppendLine("Config Settings").AppendLine();

			// TODO commonise this with CreateSettingsTree in ExceptionReportPresenter
			foreach (var appSetting in ConfigurationManager.AppSettings)
			{
				string settingText = ConfigurationManager.AppSettings.Get(appSetting.ToString());
				_stringBuilder.AppendLine(appSetting + " : " + settingText);
			}

			_stringBuilder.AppendDottedLine().AppendLine();
		}

		private void BuildEnvironmentInfo()
		{
			if (!_reportInfo.ShowEnvironmentTab) return;

			// TreeToString(tvwEnvironment, stringBuilder);
			_stringBuilder.AppendDottedLine().AppendLine();
		}

		private void BuildContactInfo()
		{
			if (!_reportInfo.ShowContactTab) return;

			_stringBuilder.AppendLine("Contact")
						  .AppendLine()
						  .AppendLine("Email:  " + _reportInfo.ContactEmail)
						  .AppendLine("Web:    " + _reportInfo.WebUrl)
						  .AppendLine("Phone:  " + _reportInfo.Phone)
						  .AppendLine("Fax:    " + _reportInfo.Fax)
						  .AppendDottedLine().AppendLine();
		}

		/// <summary>
		/// Create a line-delimited string of the exception hierarchy
		/// </summary>
		public string ExceptionHierarchyToString(Exception exception)
		{
			Exception currentException = exception;
			var stringBuilder = new StringBuilder();
			int count = 0;

			while (currentException != null)
			{
				if (count++ == 0)
					stringBuilder.AppendLine("Top-level Exception");
				else
					stringBuilder.AppendLine("Inner Exception " + (count-1));

				stringBuilder.AppendLine("Type:        " + currentException.GetType())
						     .AppendLine("Message:     " + currentException.Message)
							 .AppendLine("Source:      " + currentException.Source);

				if (currentException.StackTrace != null)
					stringBuilder.AppendLine("Stack Trace: " + currentException.StackTrace.Trim());

				stringBuilder.AppendLine();
				currentException = currentException.InnerException;
			}

			string exceptionString = stringBuilder.ToString();
			return exceptionString.Remove(exceptionString.Length-2, 2);
		}

		/// <summary>
		/// Create a line-delimited string of all the assemblies that are referenced by the given assembly
		/// </summary>
		public string ReferencedAssembliesToString(Assembly assembly)
		{
			var stringBuilder = new StringBuilder();

			if (assembly != null)
			{
				foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
				{
					stringBuilder.AppendLine(assemblyName.FullName).AppendLine();
				}
			}
			return stringBuilder.ToString();
		}
	}
}