using System.Collections.Generic;

namespace ExceptionReporting.SystemInfo
{
	public class ManagementObjectDto
	{
		private readonly List<string> _propertyKeys = new List<string>();
		private readonly string _propertyValue;

		public ManagementObjectDto(string propertyValue)
		{
			_propertyValue = propertyValue;
		}

		public string PropertyValue
		{
			get { return _propertyValue; }
		}

		public List<string> PropertyKeys
		{
			get { return _propertyKeys; }
		}
	}
}