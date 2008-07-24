using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ExceptionReporting
{
	/// <summary>
	/// just a stepping stone in trying to extract some logic out of the view
	/// methods here are not likely to stay here or remain static helper members
	/// </summary>
	public class ExceptionUtils
	{
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
	}
}