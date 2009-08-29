using System.Reflection;
using ExceptionReporting.Config;
using ExceptionReporting.Core;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class Config_Tests
	{
		private ConfigHtmlConverter _converter;

		[SetUp]
		public void SetUp() 
		{
			//TODO don't need to pass assembly anymore
			_converter = new ConfigHtmlConverter(Assembly.Load("ExceptionReporter.WinForms"));
		}

		[Test]
		public void CanConvert_Config_ToHtml()
		{
			var html = _converter.Convert();

			Assert.That(html.StartsWith("<?xml"));
			Assert.That(html, Is.StringContaining("<HTML>"));
			Assert.That(html, Is.StringContaining("</HTML>"));
		}

		[Test]
		public void CanConvert_Config_ToHtml_ThatIncludes_ConfigSectionNames()
		{
			var html = _converter.Convert();

			Assert.That(html, Is.StringContaining("Contact"));
			Assert.That(html, Is.StringContaining("TabsToShow"));
			Assert.That(html, Is.StringContaining("Email"));
			Assert.That(html, Is.StringContaining("LabelMessages"));
			Assert.That(html, Is.StringContaining("UserInterface"));
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