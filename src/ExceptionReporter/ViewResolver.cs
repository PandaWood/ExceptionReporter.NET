using System;
using System.Linq;
using System.Reflection;

namespace ExceptionReporting
{
	/// <summary>
	/// Resolve view's from the current assembly (limited to only ExceptionReportView and InternalExceptionView)
	/// This is required to be able to load either WPF or WinForms versions of the view class
	/// </summary>
	public class ViewResolver
	{
		private readonly Assembly _assembly;

		///<summary>
		/// Initialise the ViewResolver with an assembly to search
		///</summary>
		///<param name="assembly">the Assembly to search in subsequent requests to this class</param>
		public ViewResolver(Assembly assembly)
		{
			_assembly = assembly;
		}

		/// <summary>
		/// Resolve an interface to a concrete view class, limited to 2 particular expected 'View' classes in ExceptionReporter
		/// </summary>
		/// <typeparam name="T">The interface type (currenty just IExceptionReportView or IInternalExceptionView)</typeparam>
		/// <returns>An instance of a type that implements the interface (T) in the given assembly (see constructor)</returns>
		public Type Resolve<T>() where T : class
		{
			var viewType = typeof(T);

			//TODO this is not covered in unit tests

			var matchingTypes =
				from assemblyName in
					from assembly in _assembly.GetReferencedAssemblies()
					where assembly.Name.Contains("ExceptionReporter")
					select assembly
				from type in Assembly.Load(assemblyName.Name).GetExportedTypes()
				where !type.IsInterface
				where viewType.IsAssignableFrom(type)
				select type;

			if (matchingTypes.Count() == 1)
				return matchingTypes.First();
			
			throw new ApplicationException(string.Format("Unable to resolve single instance of '{0}'", viewType));
		}
	}
}