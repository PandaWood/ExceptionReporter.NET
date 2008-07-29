using ExceptionReporting.SystemInfo;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class SystemInfoRetrieverTest
	{
		[Test]
		public void CanRetrieve_SysInfo_For_CPU()
		{
			var retriever = new SystemInfoRetriever();
			var sysInfoClass = new SysInfoDto(SysInfoQueries.CpuStrategy);

			sysInfoClass = retriever.GetSysInfo(sysInfoClass);

			Assert.That(sysInfoClass.ManagedObjectList.Count, Is.GreaterThan(0));
			Assert.That(sysInfoClass.ManagedObjectList[0].PropertyKeys.Count, Is.GreaterThan(0));
		}

		[Test]
		public void CanRetrieve_SysInfo_For_OS()
		{
			var retriever = new SystemInfoRetriever();
			var sysInfoClass = new SysInfoDto(SysInfoQueries.OsStrategy);

			sysInfoClass = retriever.GetSysInfo(sysInfoClass);

			StringAssert.Contains("Windows", sysInfoClass.ManagedObjectList[0].PropertyValue);
		}

		[Test]
		public void CanRetrieve_SysInfo_For_Memory()
		{
			var retriever = new SystemInfoRetriever();
			var sysInfoClass = new SysInfoDto(SysInfoQueries.Memory);

			sysInfoClass = retriever.GetSysInfo(sysInfoClass);

			StringAssert.Contains("Memory", sysInfoClass.ManagedObjectList[0].PropertyValue);
			Assert.That(sysInfoClass.ManagedObjectList[0].PropertyKeys.Count, Is.GreaterThan(0));
			Assert.That(sysInfoClass.ManagedObjectList[0].PropertyKeys.Find(x => x.Contains("HotSwappable")), Is.Not.Null);
		}
	}
}