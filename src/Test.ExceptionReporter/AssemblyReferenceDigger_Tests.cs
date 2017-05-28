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
            var digger = new AssemblyReferenceDigger(Assembly.Load("ExceptionReporter.WinForms"));
            var references = digger.CreateReferencesString();

            Assert.That(references, Is.StringContaining("System.Windows.Forms, Version="));
        }
    }
}
