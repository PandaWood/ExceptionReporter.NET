using ExceptionReporting.SystemInfo;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	public class SysInfoResult_Tests
	{
		private SysInfoResult _sysInfoResult;
		private SysInfoResult _child;

		[SetUp]
		public void SetUp()
		{
			_sysInfoResult = new SysInfoResult("OS");
			_child = new SysInfoResult("OS_Child");
			_sysInfoResult.ChildResults.Add(_child);
			_child.Nodes.AddRange(new[] { "CountryCode = 1", "CodeSet = 7", "OSLanguage = Portugese" });
		}

		[Test]
		public void Can_Filter()
		{
			var filterResults = _sysInfoResult.Filter(new[] {"CountryCode", "CodeSet"});

			Assert.That(filterResults.ChildResults[0].Nodes.Count, Is.EqualTo(2));
			CollectionAssert.Contains(filterResults.ChildResults[0].Nodes, "CountryCode = 1");
			CollectionAssert.Contains(filterResults.ChildResults[0].Nodes, "CodeSet = 7");
		}

		[Test]
		public void Can_Filter_Just_Key()
		{
			_child.AddNode("Code = CodeSet");
			var filterResults = _sysInfoResult.Filter(new[] { "CodeSet"});

			Assert.That(filterResults.ChildResults[0].Nodes.Count, Is.EqualTo(1));
			CollectionAssert.Contains(filterResults.ChildResults[0].Nodes, "CodeSet = 7");
		}
	}
}