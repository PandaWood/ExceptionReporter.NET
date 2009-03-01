using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace ExceptionReporter.Config
{
    internal class ConfigHtmlConverter
    {
        private const string EmbeddedXsltFileName = "ExceptionReporter.XmlToHtml.xslt";

        private readonly XslCompiledTransform _xslCompiledTransform;
        private readonly StringBuilder _stringBuilder;
        private readonly Assembly _assembly;
    	private string _xsltFilename = EmbeddedXsltFileName;

    	public ConfigHtmlConverter()
		{
			_xslCompiledTransform = new XslCompiledTransform();
			_stringBuilder = new StringBuilder();
			_assembly = Assembly.GetExecutingAssembly();
		}

		public ConfigHtmlConverter(string assemblyName)
		{
			_xslCompiledTransform = new XslCompiledTransform();
			_stringBuilder = new StringBuilder();
			_assembly = Assembly.Load(new AssemblyName(assemblyName));
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
					throw new XsltFileNotFoundException(string.Format("Xslt file not found ({0}) in {1}",
						_xsltFilename, _assembly.FullName));

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