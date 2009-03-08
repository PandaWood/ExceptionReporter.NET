using System.Reflection;
using System.Text;

namespace ExceptionReporter.Core
{
	/// <summary>
	/// 
	/// </summary>
	public class AssemblyReferenceDigger
	{
		private readonly Assembly _assembly;

		///<summary>Initialise with assembly</summary>
		public AssemblyReferenceDigger(Assembly assembly)
		{
			_assembly = assembly;
		}

		/// <summary>
		/// Finds all assemblies references by the given assembly and return a string
		/// </summary>
		/// <returns>a string, delimited by newlines, displaying all referenced assemblies</returns>
		public string CreateReferencesString()
		{
			var stringBuilder = new StringBuilder();

			foreach (AssemblyName assemblyName in _assembly.GetReferencedAssemblies())
			{
				stringBuilder.AppendLine(string.Format("{0}, Version={1}", assemblyName.Name, assemblyName.Version));
			}

			return stringBuilder.ToString();
		}
	}
}