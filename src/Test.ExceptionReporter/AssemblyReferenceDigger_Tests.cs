using System.Reflection;
using ExceptionReporting.Core;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class AssemblyReferenceDigger_Tests
	{
		[Test]
		public void TestName()
		{
			var digger = new AssemblyReferenceDigger(Assembly.Load("ExceptionReporter.Wpf"));
			var references = digger.CreateReferencesString();

			Assert.That(references, Is.StringContaining("ExceptionReporter.Core, Version="));
			Assert.That(references, Is.StringContaining("PresentationCore, Version="));
		}
	}
}