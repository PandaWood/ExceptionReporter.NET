using ExceptionReporting.SystemInfo;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class SysInfoResult_Tests
	{
		readonly SysInfoResult _sysInfoResult = new SysInfoResult("OS");

		[SetUp]
		public void SetUp()
		{
			var child = new SysInfoResult("_Child");
			_sysInfoResult.ChildResults.Add(child);
			child.Nodes.AddRange(new[] { "CountryCode = 1", "CodeSet = 7", "OSLanguage = Portugese" });
		}

		[Test]
		public void CanFilter()
		{
			var filterResults = _sysInfoResult.Filter(new[] {"CountryCode", "CodeSet"});

			Assert.That(filterResults.ChildResults[0].Nodes.Count, Is.EqualTo(2));
			CollectionAssert.Contains(filterResults.ChildResults[0].Nodes, "CountryCode = 1");
			CollectionAssert.Contains(filterResults.ChildResults[0].Nodes, "CodeSet = 7");
		}
	}
}