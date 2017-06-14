using ExceptionReporting.Extensions;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class Extensions_Test
	{
		[Test]
		public void CanAssign_IfNotNullOrEmpty()
		{
			var oldString = "OldString";

			oldString = "NewString".GetString(oldString);
			Assert.That(oldString, Is.EqualTo("NewString"));
		}

		[Test]
		public void CanNotAssign_IfEmpty()
		{
			var oldString = "OldString";

			oldString = "".GetString(oldString);
			Assert.That(oldString, Is.EqualTo("OldString"));
		}

		[Test]
		public void CanNotAssign_IfNull()
		{
			const string newString = null;
			var oldString = "OldString";

			oldString = newString.GetString(oldString);
			Assert.That(oldString, Is.EqualTo("OldString"));
		}
	}
}
