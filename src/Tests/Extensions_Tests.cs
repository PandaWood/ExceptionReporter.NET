using ExceptionReporting.Core;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	public class Extensions_Tests
	{
		[TestCase("123",  4, ExpectedResult = "123")]
		[TestCase("1234", 4, ExpectedResult = "1234")]
		[TestCase("1234", 5, ExpectedResult = "1234")]
		[TestCase("1234", 3, ExpectedResult = "123")]
		[TestCase("",     3, ExpectedResult = "")]
		[TestCase(null,   3, ExpectedResult = null)]
		public string Can_Truncate(string test, int count)
		{
			return test.Truncate(count);
		}
	}
}
