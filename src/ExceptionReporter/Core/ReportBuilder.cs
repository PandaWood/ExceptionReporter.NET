using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.Core
{
	internal class ReportBuilder
	{
		private readonly ExceptionReportInfo _reportInfo;
		private StringBuilder _stringBuilder;
		private readonly IEnumerable<SysInfoResult> _sysInfoResults;

		private ReportBuilder(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}

		public ReportBuilder(ExceptionReportInfo reportInfo, IEnumerable<SysInfoResult> sysInfoResults)
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

			if (_reportInfo.ShowGeneralTab) GeneralInfo();
			if (_reportInfo.ShowExceptionsTab) ExceptionInfo();
			if (_reportInfo.ShowAssembliesTab) AssemblyInfo();
			if (_reportInfo.ShowSysInfoTab) SysInfo();
			if (_reportInfo.ShowContactTab) ContactInfo();

			return new ExceptionReport(_stringBuilder, _reportInfo, _sysInfoResults.ToList());
		}

		private void GeneralInfo()
		{
			_stringBuilder.AppendLine("[General Info]")
				.AppendLine()
				.AppendLine("Application: " + _reportInfo.AppName)
				.AppendLine("Version:     " + _reportInfo.AppVersion)
				.AppendLine("Region:      " + _reportInfo.RegionInfo)
				
				.AppendLine("Date: " + _reportInfo.ExceptionDate.ToShortDateString())
				.AppendLine("Time: " + _reportInfo.ExceptionDate.ToShortTimeString())
				.AppendLine();

			_stringBuilder.AppendLine("User Explanation:")
				.AppendLine()
				.AppendFormat("User said \"{0}\"", _reportInfo.UserExplanation)
				.AppendLine().AppendDottedLine().AppendLine();
		}

		private void ExceptionInfo()
		{
			for (var index = 0; index < _reportInfo.Exceptions.Count; index++)
			{
				var exception = _reportInfo.Exceptions[index];

				//TODO maybe omit a number when there's only 1 exception
				_stringBuilder.AppendLine(string.Format("[Exception Info {0}]", index + 1))
						.AppendLine()
						.AppendLine(ExceptionHierarchyToString(exception))
						.AppendLine().AppendDottedLine().AppendLine();
			}
		}

		private void AssemblyInfo()
		{
			var digger = new AssemblyReferenceDigger(_reportInfo.AppAssembly);

			_stringBuilder.AppendLine("[Assembly Info]")
				.AppendLine()
				.AppendLine(digger.CreateReferencesString())
				.AppendDottedLine().AppendLine();
		}

		private void SysInfo()
		{
			_stringBuilder.AppendLine("[System Info]").AppendLine();
			_stringBuilder.Append(SysInfoResultMapper.CreateStringList(_sysInfoResults));
			_stringBuilder.AppendDottedLine().AppendLine();
		}

		private void ContactInfo()
		{
			_stringBuilder.AppendLine("[Contact Info]")
				.AppendLine()
				.AppendLine("Email:  " + _reportInfo.ContactEmail)
				.AppendLine("Web:    " + _reportInfo.WebUrl)
				.AppendLine("Phone:  " + _reportInfo.Phone)
				.AppendLine("Fax:    " + _reportInfo.Fax)
				.AppendDottedLine().AppendLine();
		}

		/// <summary>
		/// Create a line-delimited string of the exception hierarchy 
		/// //TODO see Label='EH' in View, this is doing too much and is duplicated
		/// </summary>
		private static string ExceptionHierarchyToString(Exception exception)
		{
			var currentException = exception;
			var stringBuilder = new StringBuilder();
			var count = 0;

			while (currentException != null)
			{
				if (count++ == 0)
					stringBuilder.AppendLine("Top-level Exception");
				else
					stringBuilder.AppendLine("Inner Exception " + (count - 1));

				stringBuilder.
					AppendLine("Type:        " + currentException.GetType())
					.AppendLine("Message:     " + currentException.Message)
					.AppendLine("Source:      " + currentException.Source);

				if (currentException.StackTrace != null)
				{
					stringBuilder.AppendLine("Stack Trace: " + currentException.StackTrace.Trim());
				}

				stringBuilder.AppendLine();
				currentException = currentException.InnerException;
			}

			var exceptionString = stringBuilder.ToString();
			return exceptionString.TrimEnd();
		}
	}
}
