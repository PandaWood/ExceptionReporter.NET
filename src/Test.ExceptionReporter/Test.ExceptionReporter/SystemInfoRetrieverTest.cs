using ExceptionReporting.SystemInfo;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	/// <summary>
	/// these are probably best labelled "integration tests"
	/// I've left the code actually querying the system, but tried to keep the tests generic enough 
	/// to pass on anyone's machine - maybe not a great idea long term
	/// </summary>
	[TestFixture]
	public class SystemInfoRetrieverTest
	{
		private readonly SystemInfoRetriever _retriever = new SystemInfoRetriever();

		[Test]
		public void CanRetrieve_SysInfo_For_CPU()
		{
			SysInfoResult result = _retriever.Retrieve(SysInfoQueries.CPU);

			Assert.That(result.Nodes.Count, Is.GreaterThan(0));		// at least 1 cpu
			StringAssert.Contains("GHz", result.Nodes[0]);
			Assert.That(result.ChildResults[0].Nodes.Count, Is.GreaterThan(0));
			Assert.That(result.ChildResults[0].Nodes.Find(x => x.Contains("CpuStatus")), Is.Not.Null);
		}

		[Test]
		public void CanRetrieve_SysInfo_For_OS()
		{
			SysInfoResult result = _retriever.Retrieve(SysInfoQueries.OperatingSystem);
			StringAssert.Contains("Windows", result.Nodes[0]);
		}

		[Test]
		public void CanRetrieve_SysInfo_For_Memory()
		{
			SysInfoResult result = _retriever.Retrieve(SysInfoQueries.Memory);

			StringAssert.Contains("Memory", result.Nodes[0]);
			Assert.That(result.ChildResults[0].Nodes.Count, Is.GreaterThan(0));
			Assert.That(result.ChildResults[0].Nodes.Find(x => x.Contains("HotSwappable")), Is.Not.Null);
		}
	}
}