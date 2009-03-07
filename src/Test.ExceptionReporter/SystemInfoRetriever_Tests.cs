using ExceptionReporter.SystemInfo;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Linq;

namespace ExceptionReporter.Tests
{
	/// <summary>
	/// These are really "integration tests"
	/// The code queryies the system on which it's running, so testing is kept "generic"
	/// Not great, but better than nothing
	/// </summary>
	[TestFixture]
	public class SystemInfoRetriever_Tests
	{
		private readonly SysInfoRetriever _retriever = new SysInfoRetriever();

		[Test]
		public void CanRetrieve_SysInfo_For_CPU()
		{
			SysInfoResult result = _retriever.Retrieve(SysInfoQueries.Machine);

			Assert.That(result.Nodes.Count, Is.EqualTo(1));		// at least 1 machine name
			Assert.That(result.ChildResults[0].Nodes.Count, Is.GreaterThan(0));
			Assert.That(result.ChildResults[0].Nodes.Where(r => r.Contains("CurrentTimeZone")).Count(), Is.GreaterThan(0));
		}

		[Test]
		public void CanRetrieve_SysInfo_For_OS()
		{
			SysInfoResult result = _retriever.Retrieve(SysInfoQueries.OperatingSystem);
			StringAssert.Contains("Windows", result.Nodes[0]);
		}
	}
}