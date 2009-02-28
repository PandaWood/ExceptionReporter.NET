using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using ExceptionReporter.Core;

namespace ExceptionReporter.Config
{
    internal class ConfigHtmlConverter
    {
        private const string XsltFileName = "ExceptionReporter.XmlToHtml.xslt";
        private readonly XslCompiledTransform _xslCompiledTransform;
        private readonly StringBuilder _stringBuilder;
        private readonly Assembly _assembly;

        public ConfigHtmlConverter()
        {
            _xslCompiledTransform = new XslCompiledTransform();
            _stringBuilder = new StringBuilder();
            _assembly = typeof(ExceptionReport).Assembly;
        }

        public string Convert()
        {
            using (Stream stream = _assembly.GetManifestResourceStream(XsltFileName))
            {
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