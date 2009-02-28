using System;
using System.Reflection;
using ExceptionReporter.Core;

namespace ExceptionReporter
{
	/// <summary>
	/// Resolve view's from the current assembly (limited to only ExceptionReportView and InternalExceptionView)
	/// </summary>
	public class ViewInjector
	{
		private readonly Assembly _assembly;

		///<summary>
		/// Initialise the ViewInjector with an assembly to search
		///</summary>
		///<param name="assembly">the Assembly to search in subsequent requests to this class</param>
		public ViewInjector(Assembly assembly)
		{
			_assembly = assembly;
		}

		/// <summary>
		/// Resolve an interface to a concrete class, written especially for 2 particular expected cases 
		/// (the 2 'View' classes in ExceptionReporter)
		/// </summary>
		/// <typeparam name="T">The interface type</typeparam>
		/// <param name="reportInfo">ExceptionReportInfo instance if ExceptionReportView, otherwise null</param>
		/// <returns>An instance of a type that implements the interface (T) in the given assembly (see constructor)</returns>
		// ReSharper disable RedundantIfElseBlock
		public T Resolve<T>(ExceptionReportInfo reportInfo) where T : class
		{
			Type viewType = typeof(T);

			foreach (var currentType in _assembly.GetTypes())
			{
				if (currentType.FullName.StartsWith("System.") || currentType.IsInterface) continue;

				if (viewType.IsAssignableFrom(currentType))
				{
					if (reportInfo == null)
					{
						return Activator.CreateInstance(currentType) as T;
					}
					else
					{
						ConstructorInfo constructor = currentType.GetConstructor(new[] { typeof(ExceptionReportInfo) });
						var newInstance = constructor.Invoke(new object[] { reportInfo });
						return newInstance as T;
					}
				}
			}

			throw new ApplicationException(string.Format("Invalid ExceptionReporter assembly - type {0} not found", viewType));
		}
		// ReSharper restore RedundantIfElseBlock

		public T Resolve<T>() where T : class
		{
			return Resolve<T>(null);
		}
	}
}