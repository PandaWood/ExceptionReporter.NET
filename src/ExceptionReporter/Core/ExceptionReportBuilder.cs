using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ExceptionReporting.Config;
using ExceptionReporting.Extensions;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.Core
{
	public class ExceptionReportBuilder
	{
		private readonly ExceptionReportInfo _reportInfo;
		private StringBuilder _stringBuilder;
		private readonly IEnumerable<SysInfoResult> _sysInfoResults;

		/// <summary>
		/// the non-SysInfo constructor
		/// </summary>
		/// <param name="reportInfo">ExceptionReportInfo </param>
		public ExceptionReportBuilder(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}

		/// <summary>
		/// constructor that includes support for SysInfo
		/// </summary>
		public ExceptionReportBuilder(ExceptionReportInfo reportInfo, IEnumerable<SysInfoResult> sysInfoResults)
			: this(reportInfo)
		{
			_sysInfoResults = sysInfoResults;
		}

		/// <summary>
		/// Build the exception report
		/// </summary>
		public ExceptionReport Build()
		{
			_stringBuilder = new StringBuilder().AppendDottedLine();

			BuildGeneralInfo();
			BuildExceptionInfo();
			BuildAssemblyInfo();
			BuildConfigInfo();
			BuildSysInfo();
			BuildContactInfo();

			return new ExceptionReport(_stringBuilder);
		}

		private void BuildGeneralInfo()
		{
			if (!_reportInfo.ShowGeneralTab) return;

			_stringBuilder.AppendLine("[General Info]")
				.AppendLine()
				.AppendLine("Application: " + _reportInfo.AppName)
				.AppendLine("Version:     " + _reportInfo.AppVersion)
				.AppendLine("Region:      " + _reportInfo.RegionInfo)
				.AppendLine("Machine:     " + _reportInfo.MachineName)
				.AppendLine("User:        " + _reportInfo.UserName)
				.AppendLine("Date: " + _reportInfo.ExceptionDate.ToShortDateString())
				.AppendLine("Time: " + _reportInfo.ExceptionDate.ToShortTimeString())
				.AppendLine();

			_stringBuilder.AppendLine("User Explanation:")
				.AppendLine()
				.AppendFormat("{0} said \"{1}\"", _reportInfo.UserName, _reportInfo.UserExplanation)
				.AppendLine().AppendDottedLine().AppendLine();
		}

		private void BuildExceptionInfo()
		{
		    if (!_reportInfo.ShowExceptionsTab) return;

		    for (int index = 0; index < _reportInfo.Exceptions.Count; index++)
		    {
		        var exception = _reportInfo.Exceptions[index];

				//TODO maybe omit a number when there's only 1 exception
		        _stringBuilder.AppendLine(string.Format("[Exception Info {0}]", index+1))
		            .AppendLine()
		            .AppendLine(ExceptionHierarchyToString(exception))
		            .AppendLine().AppendDottedLine().AppendLine();
		    }
		}

	    private void BuildAssemblyInfo()
		{
			if (!_reportInfo.ShowAssembliesTab) return;

			_stringBuilder.AppendLine("[Assembly Info]")
				.AppendLine()
				.AppendLine(ReferencedAssembliesToString(_reportInfo.AppAssembly))
				.AppendDottedLine().AppendLine();
		}

		private void BuildConfigInfo()
		{
			if (!_reportInfo.ShowConfigTab) return;

			_stringBuilder.AppendLine("[Config Settings]").AppendLine();
            _stringBuilder.AppendLine(File.ReadAllText(ConfigReader.GetConfigFilePath()));
			_stringBuilder.AppendDottedLine().AppendLine();
		}

		private void BuildSysInfo()
		{
			if (!_reportInfo.ShowSysInfoTab) return;

			_stringBuilder.AppendLine("[System Info]").AppendLine();
			_stringBuilder.Append(new SysInfoResultMapper().CreateStringList(_sysInfoResults));
			_stringBuilder.AppendDottedLine().AppendLine();
		}

		private void BuildContactInfo()
		{
			if (!_reportInfo.ShowContactTab) return;

			_stringBuilder.AppendLine("[Contact Info]")
				.AppendLine()
				.AppendLine("Email:  " + _reportInfo.ContactEmail)
				.AppendLine("Web:    " + _reportInfo.WebUrl)
				.AppendLine("Phone:  " + _reportInfo.Phone)
				.AppendLine("Fax:    " + _reportInfo.Fax)
				.AppendDottedLine().AppendLine();
		}

		/// <summary>
		/// Create a line-delimited string of the exception hierarchy //TODO see Label='EH' in View
		/// </summary>
		private static string ExceptionHierarchyToString(Exception exception)
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
			return exceptionString.TrimEnd();
		}

		/// <summary>
		/// Create a line-delimited string of all the assemblies that are referenced by the given assembly
		/// </summary>
		private static string ReferencedAssembliesToString(Assembly assembly)
		{
			var stringBuilder = new StringBuilder();

			foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
			{
				stringBuilder.AppendLine(string.Format("{0}, Version={1}", assemblyName.Name, assemblyName.Version));
			}

			return stringBuilder.ToString();
		}
	}
}
