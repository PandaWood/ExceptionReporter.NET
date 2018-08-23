using System;

namespace ExceptionReporting.Tests
{
	internal class TestException : Exception
	{
		public const string ErrorMessage = "NullRef";

		public TestException() : base(ErrorMessage)
		{ }
	}
}