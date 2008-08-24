using System.Collections.Generic;
using System.Configuration;
using ExceptionReporting.Config;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class ConfigReaderTest
	{
		[Test]
		public void CanCreate_ConfigStringList_With_1()
		{
			// using ConfigurationManager.AppSettings in a UnitTest works, although a tad brittle (and could cause issues one day)
			ConfigurationManager.AppSettings["AKey"] = "AValue";

			IList<string> stringList = ConfigReader.GetConfigKeyValuePairsToString();

			Assert.That(stringList.Count, Is.EqualTo(1));
			Assert.That(stringList[0], Is.EqualTo("AKey = AValue"));
		}

		[Test]
		public void CanCreate_ConfigStringList_With_2()
		{
			ConfigurationManager.AppSettings["Key2"] = "Value2";

			IList<string> stringList = ConfigReader.GetConfigKeyValuePairsToString();

			Assert.That(stringList.Count, Is.EqualTo(2));
			Assert.That(stringList[0], Is.EqualTo("AKey = AValue"));
			Assert.That(stringList[1], Is.EqualTo("Key2 = Value2"));
		}

//		[Test]
//		public void CanGet_ConfigSection()
//		{
//			ConfigurationManager.
//
//			IList<string> stringList = ConfigReader.GetConfigKeyValuePairsToString();
//
//			Assert.That(stringList.Count, Is.EqualTo(2));
//			Assert.That(stringList[0], Is.EqualTo("AKey = AValue"));
//			Assert.That(stringList[1], Is.EqualTo("Key2 = Value2"));
//		}
	}
}