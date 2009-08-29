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

		[Test]
		public void CanNotAssign_False_If_N()
		{
			var newValue = "N".GetBool(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanAssign_True_If_Y()
		{
			var newValue = "Y".GetBool(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_True_If_Y_LowerCase()
		{
			var newValue = "y".GetBool(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_False_If_N_LowerCase()
		{
			var newValue = "n".GetBool(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanAssign_True_If_String_True()
		{
			var newValue = "true".GetBool(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_False_If_String_False()
		{
			var newValue = "false".GetBool(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanNotAssign_False_If_Null()
		{
			var newValue = "".GetBool(false);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanNotAssign_True_If_Null_And_CurrentValue_Is_True()
		{
			var newValue = "".GetBool(true);
			Assert.That(newValue, Is.True);
		}
	}
}
