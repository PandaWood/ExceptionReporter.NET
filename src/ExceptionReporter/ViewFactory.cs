using System;
using System.Reflection;
using ExceptionReporter.Core;

namespace ExceptionReporter
{
	/// <summary>
	/// Create views by finding them (using the ViewResolver) and then instantiating it, using reflection
	/// All this is necessary because the calling code honestly does't know what the implementing class 
	/// they're supposed to use is going to be, ViewFactory finds it and gives it to them
	/// </summary>
	internal static class ViewFactory
	{
		public static T Create<T>(ViewResolver viewResolver, ExceptionReportInfo reportInfo) where T : class
		{
			Type view = viewResolver.Resolve<T>();

			ConstructorInfo constructor = view.GetConstructor(new[] {typeof (ExceptionReportInfo)});
			var newInstance = constructor.Invoke(new object[] {reportInfo});
			return newInstance as T;
		}

		public static T Create<T>(ViewResolver viewResolver) where T : class
		{
			return Activator.CreateInstance(viewResolver.Resolve<T>()) as T;
		}
	}
}