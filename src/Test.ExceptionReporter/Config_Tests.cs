using ExceptionReporter.Config;
using ExceptionReporter.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace ExceptionReporter.Tests
{
	[TestFixture]
	public class Config_Tests
	{
		private ConfigHtmlConverter _converter;

		[SetUp]
		public void SetUp() 
		{
			_converter = new ConfigHtmlConverter("ExceptionReporter.Core");
		}

		[Test]
		public void CanConvert_Config_ToHtml()
		{
			var html = _converter.Convert();

			Assert.That(html.StartsWith("<?xml"));
			Assert.That(html, Text.Contains("<HTML>"));
			Assert.That(html, Text.Contains("</HTML>"));
		}

		[Test]
		public void CanConvert_Config_ToHtml_ThatIncludes_ConfigSectionNames()
		{
			var html = _converter.Convert();

			Assert.That(html, Text.Contains("Contact"));
			Assert.That(html, Text.Contains("TabsToShow"));
			Assert.That(html, Text.Contains("Email"));
			Assert.That(html, Text.Contains("LabelMessages"));
			Assert.That(html, Text.Contains("UserInterface"));
		}

		[Test]
		[ExpectedException(typeof(XsltFileNotFoundException))]
		public void CanThrow_Exception_IfManifestResource_NotFound()
		{
			_converter.XsltFilename = "SomeDodgyFilename";
			_converter.Convert();
		}

		[Test]  // NB this test relies on the app.config being as it is at the time of writing - it's really an integration test
		public void CanReadConfig()
		{
			var info = new ExceptionReportInfo();
			var configReader = new ConfigReader(info);
			configReader.ReadConfig();

			Assert.That(info.ContactEmail, Is.EqualTo("test@test.com"));
			Assert.That(info.ShowLessMoreDetailButton, Is.False);
		}
	}
}