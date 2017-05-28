using ExceptionReporting.SystemInfo;
using NUnit.Framework;
using System.Linq;
using ExceptionReporting.Core;

namespace ExceptionReporting.Tests
{
    /// <summary>
    /// These are really "integration tests"
    /// The code queries the system on which it's running, so testing is kept "generic"
    /// Not great, but better than nothing
    /// </summary>
    [TestFixture]
    public class SystemInfoRetriever_Tests
    {
        private readonly SysInfoRetriever _retriever = new SysInfoRetriever();

        [Test]
        public void CanRetrieve_SysInfo_For_CPU()
        {
            var sysInfoResult = _retriever.Retrieve(SysInfoQueries.Machine);
            if (ExceptionReport.IsRunningMono()) {
                Assert.That(sysInfoResult, Is.Null);
                return;
            };

            Assert.That(sysInfoResult.Nodes.Count, Is.EqualTo(1));      // at least 1 machine name
            Assert.That(sysInfoResult.ChildResults[0].Nodes.Count, Is.GreaterThan(0));
            Assert.That(sysInfoResult.ChildResults[0].Nodes.Where(r => r.Contains("CurrentTimeZone")).Count(), Is.GreaterThan(0));
        }

        [Test]
        public void CanRetrieve_SysInfo_For_OS()
        {
            var sysInfoResult = _retriever.Retrieve(SysInfoQueries.OperatingSystem);
			if (ExceptionReport.IsRunningMono())
			{
				Assert.That(sysInfoResult, Is.Null);
				return;
			};
            Assert.That(sysInfoResult.Nodes[0], Is.StringContaining("Windows"));
        }
    }
}