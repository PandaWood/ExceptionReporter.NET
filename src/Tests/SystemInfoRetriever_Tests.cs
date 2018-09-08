using System.Linq;
using ExceptionReporting;
using ExceptionReporting.SystemInfo;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	/// <summary>
	/// These are really "integration tests"
	/// The code queries the system on which it's running, so testing is kept "generic"
	/// Not great, but better than nothing
	/// </summary>
	public class SystemInfoRetriever_Tests
	{
		private readonly SysInfoRetriever _retriever = new SysInfoRetriever();

		[Test, Ignore("avoid system info calls during tests")]
		public void Can_Retrieve_SysInfo_For_CPU()
		{
			var sysInfoResult = _retriever.Retrieve(SysInfoQueries.Machine);
			if (ExceptionReporter.IsRunningMono())
			{
				Assert.That(sysInfoResult, Is.Null);
				return;
			};

			Assert.That(sysInfoResult.Nodes.Count, Is.EqualTo(1));      // at least 1 machine name
			Assert.That(sysInfoResult.ChildResults[0].Nodes.Count, Is.GreaterThan(0));
			Assert.That(sysInfoResult.ChildResults[0].Nodes.Count(r => r.Contains("CurrentTimeZone")), Is.GreaterThan(0));
		}

		[Test, Ignore("avoid system info calls during tests")]
		public void Can_Retrieve_SysInfo_For_OS()
		{
			var sysInfoResult = _retriever.Retrieve(SysInfoQueries.OperatingSystem);
			if (ExceptionReporter.IsRunningMono())
			{
				Assert.That(sysInfoResult, Is.Null);
				return;
			};
			Assert.That(sysInfoResult.Nodes[0], Does.Contain("Windows"));
		}
	}
}