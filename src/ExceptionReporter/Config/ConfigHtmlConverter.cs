using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace ExceptionReporting.Config
{
    internal class ConfigHtmlConverter
    {
        private const string EmbeddedXsltFileName = "ExceptionReporter.XmlToHtml.xslt";
		private string _xsltFilename = EmbeddedXsltFileName;

		private readonly XslCompiledTransform _xslCompiledTransform = new XslCompiledTransform();
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private readonly Assembly _assembly;

		public ConfigHtmlConverter(Assembly assembly)
		{
			_assembly = assembly;
		}

    	public string XsltFilename
    	{
    		set { _xsltFilename = value; }
    	}

    	public string Convert()
    	{
			using (Stream stream = _assembly.GetManifestResourceStream(_xsltFilename))
    		{
    			if (stream == null) 
					throw new XsltFileNotFoundException(string.Format("Xslt file not found ({0}) in {1}", _xsltFilename, _assembly.FullName));

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

	internal class XsltFileNotFoundException : Exception 
	{
		public XsltFileNotFoundException(string message) : base(message) {}
	}
}