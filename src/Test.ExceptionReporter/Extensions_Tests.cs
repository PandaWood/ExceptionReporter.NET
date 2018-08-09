using ExceptionReporting.Core;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	public class Extensions_Test
	{
		[Test]
		public void Can_Truncate()
		{
			Assert.That("A message too long".Truncate(9), Is.EqualTo("A message"));
		}
	}
}
