using System;
using System.Reflection;
using System.Text;

namespace ExceptionReporting
{
	public class ExceptionStringBuilder
	{
		private string _email;
		private string _general;
		private string _applicationName;
		private string _appVersion;
		private string _region;
		private string _machineName;
		private string _userName;
		private DateTime _exceptionDate;
		private string _explanation;
		private Exception _exception;
		private Assembly _assembly;
		private string _url;
		private string _phone;
		private string _fax;

		private void Build(bool blnGeneral, bool blnExceptions, bool blnAssemblies, 
			bool blnSettings, bool blnEnvironment, bool blnContact, bool blnForPrint)
		{
			try
			{
				var _exceptionString = new StringBuilder();

				if (blnGeneral)
				{
					if (!blnForPrint)
					{
						_exceptionString.AppendLine(_general);
						_exceptionString.AppendLine();
						AppendDottedLine(_exceptionString);
						_exceptionString.AppendLine();
					}
					_exceptionString.AppendLine("General");
					_exceptionString.AppendLine();
					_exceptionString.AppendLine("Application: " + _applicationName);
					_exceptionString.AppendLine("Version:     " + _appVersion);
					_exceptionString.AppendLine("Region:      " + _region);
					_exceptionString.AppendLine("Machine:     " + " " + _machineName);
					_exceptionString.AppendLine("User:        " + _userName);
					AppendDottedLine(_exceptionString);
					if (!blnForPrint)
					{
						_exceptionString.AppendLine();
						_exceptionString.AppendLine("Date: " + _exceptionDate.ToShortDateString());
						_exceptionString.AppendLine("Time: " + _exceptionDate.ToShortTimeString());
						AppendDottedLine(_exceptionString);
					}
					_exceptionString.AppendLine();
					_exceptionString.AppendLine("Explanation");
					_exceptionString.AppendLine(_explanation);
					_exceptionString.AppendLine();
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnExceptions)
				{
					_exceptionString.AppendLine("Exceptions");
					_exceptionString.AppendLine();
					_exceptionString.AppendLine(ExceptionHierarchyToString(_exception));
					_exceptionString.AppendLine();
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnAssemblies)
				{
					_exceptionString.AppendLine("Assemblies");
					_exceptionString.AppendLine();
					_exceptionString.AppendLine(ReferencedAssembliesToString(_assembly));
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnSettings)
				{
					//					TreeToString(tvwSettings, stringBuilder);		//TODO put back in but isolate the functionality out of here
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnEnvironment)
				{
					//					TreeToString(tvwEnvironment, stringBuilder);
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnContact)
				{
					_exceptionString.AppendLine("Contact");
					_exceptionString.AppendLine();
					_exceptionString.AppendLine("E-Mail: " + _email);
					_exceptionString.AppendLine("Web:    " + _url);
					_exceptionString.AppendLine("Phone:  " + _phone);
					_exceptionString.AppendLine("Fax:    " + _fax);
					_exceptionString.AppendLine("-----------------------------");
					_exceptionString.AppendLine();
				}
			}
			catch (Exception ex)
			{
//				ShowError(
//					"There has been a problem building exception details into a string for printing, copying, saving or e-mailing", ex);
			}
		}

		public static string ExceptionHierarchyToString(Exception exception)
		{
			int count = 0;
			Exception current = exception;
			var stringBuilder = new StringBuilder();

			while (current != null)
			{
				if (count == 0)
				{
					stringBuilder.AppendLine("Top-level Exception");
				}
				else
				{
					stringBuilder.AppendLine("Inner Exception " + count);
				}
				stringBuilder.AppendLine("Type:        " + current.GetType());
				stringBuilder.AppendLine("Message:     " + current.Message);
				stringBuilder.AppendLine("Source:      " + current.Source);
				if (current.StackTrace != null)
					stringBuilder.AppendLine("Stack Trace: " + current.StackTrace.Trim());
				stringBuilder.AppendLine();

				current = current.InnerException;
				count++;
			}

			return stringBuilder.ToString();
		}

		public static string ReferencedAssembliesToString(Assembly assembly)
		{
			var writer = new StringBuilder();
			if (assembly != null)
			{
				foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
				{
					writer.AppendLine(assemblyName.FullName);
					writer.AppendLine();
				}
			}

			return writer.ToString();
		}

		private static void AppendDottedLine(StringBuilder stringBuilder)
		{
			stringBuilder.AppendLine("-----------------------------");
		}
	}
}