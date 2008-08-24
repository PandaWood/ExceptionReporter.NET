using ExceptionReporting.Extensions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ExtensionsTest
	{
		[Test]
		public void CanAssign_IfNotNullOrEmpty()
		{
			string oldString = "OldString";

			oldString = "NewString".ReturnStringIfNotNullElse(oldString);
			Assert.That(oldString, Is.EqualTo("NewString"));
		}

		[Test]
		public void CanNotAssign_IfEmpty()
		{
			string oldString = "OldString";

			oldString = "".ReturnStringIfNotNullElse(oldString);
			Assert.That(oldString, Is.EqualTo("OldString"));
		}

		[Test]
		public void CanNotAssign_IfNull()
		{
			string newString = null;
			string oldString = "OldString";

			oldString = newString.ReturnStringIfNotNullElse(oldString);
			Assert.That(oldString, Is.EqualTo("OldString"));
		}

		[Test]
		public void CanNotAssign_False_If_N()
		{
			bool newValue = "N".ReturnBoolfNotNullElse(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanAssign_True_If_Y()
		{
			bool newValue = "Y".ReturnBoolfNotNullElse(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_True_If_Y_LowerCase()
		{
			bool newValue = "y".ReturnBoolfNotNullElse(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_False_If_N_LowerCase()
		{
			bool newValue = "n".ReturnBoolfNotNullElse(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanAssign_True_If_String_True()
		{
			bool newValue = "true".ReturnBoolfNotNullElse(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_False_If_String_False()
		{
			bool newValue = "false".ReturnBoolfNotNullElse(true);
			Assert.That(newValue, Is.False);
		}


		[Test]
		public void CanNotAssign_False_If_Null()
		{
			bool newValue = "".ReturnBoolfNotNullElse(false);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanNotAssign_True_If_Null_And_CurrentValue_Is_True()
		{
			bool newValue = "".ReturnBoolfNotNullElse(true);
			Assert.That(newValue, Is.True);
		}
	}
}
