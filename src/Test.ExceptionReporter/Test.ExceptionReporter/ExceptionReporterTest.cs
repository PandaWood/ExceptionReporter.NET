using ExceptionReporting.Extensions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ExceptionReporterTest
	{
		[Test]
		public void CanAssign_IfNotNullOrEmpty()
		{
			string oldString = "OldString";

			oldString = "NewString".ReturnStringOr(oldString);
			Assert.That(oldString, Is.EqualTo("NewString"));
		}

		[Test]
		public void CanNotAssign_IfEmpty()
		{
			string oldString = "OldString";

			oldString = "".ReturnStringOr(oldString);
			Assert.That(oldString, Is.EqualTo("OldString"));
		}

		[Test]
		public void CanNotAssign_IfNull()
		{
			string newString = null;
			string oldString = "OldString";

			oldString = newString.ReturnStringOr(oldString);
			Assert.That(oldString, Is.EqualTo("OldString"));
		}

		[Test]
		public void CanNotAssign_False_If_N()
		{
			bool newValue = "N".ReturnBoolOr(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanAssign_True_If_Y()
		{
			bool newValue = "Y".ReturnBoolOr(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_True_If_Y_LowerCase()
		{
			bool newValue = "y".ReturnBoolOr(false);
			Assert.That(newValue, Is.True);
		}

		[Test]
		public void CanAssign_False_If_N_LowerCase()
		{
			bool newValue = "n".ReturnBoolOr(true);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanNotAssign_False_If_Null()
		{
			bool newValue = "".ReturnBoolOr(false);
			Assert.That(newValue, Is.False);
		}

		[Test]
		public void CanNotAssign_True_If_Null_And_CurrentValue_Is_True()
		{
			bool newValue = "".ReturnBoolOr(true);
			Assert.That(newValue, Is.True);
		}
	}
}
