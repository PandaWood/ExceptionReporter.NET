using System;
using System.Reflection;
using ExceptionReporter.Core;

namespace ExceptionReporter
{
	internal static class ViewCreator
	{
		public static T Create<T>(ViewResolver viewResolver, ExceptionReportInfo reportInfo) where T : class
		{
			Type view = viewResolver.Resolve<T>();

			//TODO this condition is ugly, try to be more polymorphic, perhaps
			if (reportInfo == null)
			{
				return Activator.CreateInstance(view) as T;
			}
			else
			{
				ConstructorInfo constructor = view.GetConstructor(new[] { typeof(ExceptionReportInfo) });
				var newInstance = constructor.Invoke(new object[] { reportInfo });
				return newInstance as T;
			}
		}
	}
}