using System;
using System.Reflection;
using System.Text;

namespace ExceptionReporting
{
	public class ExceptionStringBuilder
	{
		public ExceptionReportInfo _info;
		private StringBuilder _stringBuilder;

		/// <summary>
		/// the (only) constructor
		/// </summary>
		/// <param name="info">ExceptionReportInfo </param>
		public ExceptionStringBuilder(ExceptionReportInfo info)
		{
			_info = info;
		}

		/// <summary>
		/// Build the exception string
		/// </summary>
		public string Build()
		{
			_stringBuilder = new StringBuilder();

			if (_info.ShowGeneralTab)
				BuildGeneralInfo();

			if (_info.ShowExceptionsTab)
				BuildExceptionInfo();

			if (_info.ShowAssembliesTab)
				BuildAssemblyInfo();

			if (_info.ShowSettingsTab)
				// TreeToString(tvwSettings, stringBuilder);		//TODO put back in but isolate the functionality out of here
				_stringBuilder.AppendDottedLine().AppendLine();

			if (_info.ShowEnvironmentTab)
				// TreeToString(tvwEnvironment, stringBuilder);
				_stringBuilder.AppendDottedLine().AppendLine();

			if (_info.ShowContactTab)
				BuildContactInfo();

			return _stringBuilder.ToString();
		}

		private void BuildContactInfo()
		{
			_stringBuilder.AppendLine("Contact")
						  .AppendLine()
						  .AppendLine("E-Mail: " + _info.ContactEmail)
						  .AppendLine("Web:    " + _info.WebUrl)
						  .AppendLine("Phone:  " + _info.Phone)
						  .AppendLine("Fax:    " + _info.Fax)
						  .AppendDottedLine().AppendLine();
		}

		private void BuildAssemblyInfo()
		{
			_stringBuilder.AppendLine("Assemblies")
						  .AppendLine()
						  .AppendLine(ReferencedAssembliesToString(_info.AppAssembly))
						  .AppendDottedLine().AppendLine();
		}

		private void BuildExceptionInfo()
		{
			_stringBuilder.AppendLine("Exceptions")
						  .AppendLine()
						  .AppendLine(ExceptionHierarchyToString(_info.RootException))
						  .AppendLine().AppendDottedLine().AppendLine();
		}

		private void BuildGeneralInfo()
		{
			if (!_info.isForPrinting)
			{
				_stringBuilder.AppendLine(_info.GeneralInfo)
							  .AppendLine().AppendDottedLine().AppendLine();
			}

			_stringBuilder.AppendLine("General")
						  .AppendLine()
						  .AppendLine("Application: " + _info.AppName)
						  .AppendLine("Version:     " + _info.AppVersion)
						  .AppendLine("Region:      " + _info.RegionInfo)
						  .AppendLine("Machine:     " + _info.MachineName)
						  .AppendLine("User:        " + _info.UserName)
						  .AppendDottedLine();

			if (!_info.isForPrinting)
			{
				_stringBuilder.AppendLine()
							  .AppendLine("Date: " + _info.ExceptionDate.ToShortDateString())
							  .AppendLine("Time: " + _info.ExceptionDate.ToShortTimeString())
							  .AppendDottedLine();
			}

			_stringBuilder.AppendLine()
						  .AppendLine("Explanation")
						  .AppendLine(_info.UserExplanation)
						  .AppendLine().AppendDottedLine().AppendLine();
		}

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
					stringBuilder.AppendLine("Inner Exception " + count);

				stringBuilder.AppendLine("Type:        " + currentException.GetType())
						     .AppendLine("Message:     " + currentException.Message)
							 .AppendLine("Source:      " + currentException.Source);

				if (currentException.StackTrace != null)
					stringBuilder.AppendLine("Stack Trace: " + currentException.StackTrace.Trim());

				stringBuilder.AppendLine();
				currentException = currentException.InnerException;
			}

			return stringBuilder.ToString();
		}

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