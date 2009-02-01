using System;
using ExceptionReporting.Config;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.ExceptionReporter
{
	[TestFixture]
	public class Config_Tests
	{
		[Test]
		public void CanConvert_Config_ToHtml()
		{
			var converter = new ConfigHtmlConverter();
			string html = converter.Convert();

			Assert.That(html.StartsWith("<?xml"));
			Assert.That(html, Text.Contains("<HTML>"));
			Assert.That(html, Text.Contains("</HTML>"));
		}

		[Test]
		public void CanConvert_Config_ToHtml_ThatIncludes_ConfigSectionNames()
		{
			var converter = new ConfigHtmlConverter();
			string html = converter.Convert();

			Assert.That(html, Text.Contains("Contact"));
			Assert.That(html, Text.Contains("TabsToShow"));
			Assert.That(html, Text.Contains("Email"));
			Assert.That(html, Text.Contains("LabelMessages"));
			Assert.That(html, Text.Contains("UserInterface"));
		}

		[Test][Ignore("Need to be able to simulate the resource not existing")]
		[ExpectedException(typeof(Exception), ExpectedMessage = "File not found in manifest: ExceptionReporting.XmlToHtml.xslt")]
		public void CanThrow_ADecentException_IfManifestResourceNotFound()
		{
			new ConfigHtmlConverter().Convert();
		}
	}
}