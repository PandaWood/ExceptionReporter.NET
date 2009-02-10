using ExceptionReporter.Extensions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class Extensions_Test
	{
		[Test]
		public void CanAssign_IfNotNullOrEmpty()
		{
			string oldString = "OldString";

			oldString = "NewString".ReturnStringIfNotNull_Else(oldString);
			Assert.That(oldString, Is.EqualTo("NewString"));
		}

		[Test]
		public void CanNotAssign_IfEmpty()
		{
			string oldString = "OldString";

			oldString = "".ReturnStringIfNotNull_Else(oldString);
			Assert.That(oldString, Is.EqualTo("OldString"));
		}

		[Test]
		public void CanNotAssign_IfNull()
		{
			string newString = null;
			string oldString = "OldString";

			oldString = newString.ReturnStringIfNotNull_Else(oldString);
			Assert.That(oldString, Is.EqualTo("OldString"));
		}

		[Test]
		public void CanNotAssign_False_If_N()
		{
			bool newValue = "N".ReturnBoolfNotNull_Else(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanAssign_True_If_Y()
		{
			bool newValue = "Y".ReturnBoolfNotNull_Else(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_True_If_Y_LowerCase()
		{
			bool newValue = "y".ReturnBoolfNotNull_Else(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_False_If_N_LowerCase()
		{
			bool newValue = "n".ReturnBoolfNotNull_Else(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanAssign_True_If_String_True()
		{
			bool newValue = "true".ReturnBoolfNotNull_Else(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_False_If_String_False()
		{
			bool newValue = "false".ReturnBoolfNotNull_Else(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanNotAssign_False_If_Null()
		{
			bool newValue = "".ReturnBoolfNotNull_Else(false);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanNotAssign_True_If_Null_And_CurrentValue_Is_True()
		{
			bool newValue = "".ReturnBoolfNotNull_Else(true);
			Assert.That(newValue, Is.True);
		}
	}
}
