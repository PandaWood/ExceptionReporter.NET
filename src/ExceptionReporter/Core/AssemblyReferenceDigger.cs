// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System.Reflection;
using System.Text;

namespace ExceptionReporting.Core
{
	/// <summary>
	/// Used to find and do things with referenced assemblies
	/// </summary>
	public class AssemblyReferenceDigger
	{
		private readonly Assembly _assembly;

		///<summary>Initialise with assembly</summary>
		public AssemblyReferenceDigger(Assembly assembly)
		{
			_assembly = assembly;
		}

		/// <summary> Finds all assemblies referenced and return a string </summary>
		/// <returns>line-delimited string of referenced assemblies</returns>
		public string CreateReferencesString()
		{
			var stringBuilder = new StringBuilder();

			foreach (var assemblyName in _assembly.GetReferencedAssemblies())
			{
				stringBuilder.AppendLine(string.Format("{0}, Version={1}", assemblyName.Name, assemblyName.Version));
			}

			return stringBuilder.ToString();
		}
	}
}