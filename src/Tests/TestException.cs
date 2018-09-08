using System;

namespace Tests.ExceptionReporting
{
	internal class TestException : Exception
	{
		public const string ErrorMessage = "NullRef";

		public TestException() : base(ErrorMessage)
		{ }
	}
	
	internal class TestContainsInnerException : Exception
	{
		public const string ErrorMessage = "OuterNullRef";

		public TestContainsInnerException() : base(ErrorMessage, new TestInnerException())
		{ }
	}
	
	internal class TestInnerException : Exception
	{
		public const string ErrorMessage = "InnerNullRef";

		public TestInnerException() : base(ErrorMessage)		{ }
	}
}