using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace ExceptionReporting.Config
{
	public class ConfigHtmlConverter
	{
		private const string XsltFileName = "ExceptionReporting.XmlToHtml.xslt";
		private readonly XslCompiledTransform _xslCompiledTransform;
		private readonly StringBuilder _stringBuilder;
		private readonly Assembly _assembly;

		public ConfigHtmlConverter()
		{
			_xslCompiledTransform = new XslCompiledTransform();
			_stringBuilder = new StringBuilder();
			_assembly = typeof(ExceptionReporter).Assembly;
		}

		public string Convert()
		{
			using (Stream stream = _assembly.GetManifestResourceStream(XsltFileName))
			{
				if (stream == null) return string.Empty;

				using (XmlReader reader = XmlReader.Create(stream))
				{
					_xslCompiledTransform.Load(reader);

					using (XmlWriter xmlWriter = XmlWriter.Create(_stringBuilder))
					{
						_xslCompiledTransform.Transform(ConfigReader.GetConfigFilePath(), xmlWriter);
					}

					return _stringBuilder.ToString();
				}
			}
		}
	}
}