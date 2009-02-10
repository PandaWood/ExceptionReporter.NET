using System;
using ExceptionReporter.Config;
using ExceptionReporter.Core;
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

			//TODO create a Regex to test this more thoroughly
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

		// NB this test relies on the app.config being as it is at the time of writing - it's an integration test
		[Test]
		public void CanReadConfig()
		{
			var info = new ExceptionReportInfo();
			var configReader = new ConfigReader(info);
			configReader.ReadConfig();

			Assert.That(info.ContactEmail, Is.EqualTo("test@test.com"));
			Assert.That(info.ShowLessMoreDetailButton, Is.False);
		}

		[Test][Ignore("Need to be able to simulate the resource not existing")]
		[ExpectedException(typeof(Exception), ExpectedMessage = "File not found in manifest: ExceptionReporter.XmlToHtml.xslt")]
		public void CanThrow_ADecentException_IfManifestResourceNotFound()
		{
			new ConfigHtmlConverter().Convert();
		}
	}
}