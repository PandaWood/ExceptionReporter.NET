using System.Reflection;
using ExceptionReporter.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace ExceptionReporter.Tests
{
	[TestFixture]
	public class AssemblyReferenceDigger_Tests
	{
		[Test]
		public void TestName()
		{
			var digger = new AssemblyReferenceDigger(Assembly.Load("ExceptionReporter.Wpf"));
			var references = digger.CreateReferencesString();

			Assert.That(references, Text.Contains("ExceptionReporter.Core, Version="));
			Assert.That(references, Text.Contains("PresentationCore, Version="));
		}
	}
}