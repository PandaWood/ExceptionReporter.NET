using System;

namespace ExceptionReporter
{
	public interface IInternalExceptionView
	{
		void ShowException(string message, Exception exception);
	}
}