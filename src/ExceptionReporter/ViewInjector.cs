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
		/// Resolve an interface to a concrete view class, limited to 2 particular expected 'View' classes in ExceptionReporter
		/// </summary>
		/// <typeparam name="T">The interface type (currenty just IExceptionReportView or IInternalExceptionView)</typeparam>
		/// <param name="reportInfo">ExceptionReportInfo if instance T is IExceptionReportView, otherwise null</param>
		/// <returns>An instance of a type that implements the interface (T) in the given assembly (see constructor)</returns>
		public T Resolve<T>(ExceptionReportInfo reportInfo) where T : class
		{
			Type viewType = typeof(T);

			foreach (var currentType in _assembly.GetTypes())
			{
				if (currentType.FullName.StartsWith("System.") || currentType.IsInterface) continue;

				if (viewType.IsAssignableFrom(currentType))
				{
					return CreateInstance<T>(currentType, reportInfo);
				}
			}

			throw new ApplicationException(string.Format("Invalid ExceptionReporter assembly - type {0} not found", viewType));
		}

		/// <summary>
		/// Resolve an interface to a concrete view class
		/// The interface must be implemented by only 1 class and have a public no arg-constructor
		/// Although written for 1 particular case, this is functionally generic (within the limitations specified)
		/// </summary>
		/// <typeparam name="T">an instance of the first class found, that implements the interface specified (T), 
		/// using it's default constructor</typeparam>
		/// <returns></returns>
		public T Resolve<T>() where T : class
		{
			return Resolve<T>(null);
		}

		private static T CreateInstance<T>(Type currentType, ExceptionReportInfo reportInfo) where T : class
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
}