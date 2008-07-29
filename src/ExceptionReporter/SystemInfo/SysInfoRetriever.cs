using System;
using System.Management;

namespace ExceptionReporting.SystemInfo
{
	public class SysInfoRetriever
	{
		public SysInfoResult Retrieve(SysInfoQuery sysInfoQuery)
		{
			var result = new SysInfoResult(sysInfoQuery.Name);

			var objectSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0}", sysInfoQuery.QueryText));

			foreach (ManagementObject mgtObject in objectSearcher.Get())
			{
				string propertyValue = mgtObject.GetPropertyValue(sysInfoQuery.DisplayField).ToString().Trim();
				result.Nodes.Add(propertyValue);

				SysInfoResult childResult = null;
				foreach (PropertyData propertyData in mgtObject.Properties)
				{
					if (childResult == null)
					{
						childResult = new SysInfoResult(sysInfoQuery.Name + "_ChildResult");
						result.ChildResults.Add(childResult);
					}

					string nodeValue = propertyData.Name + ':' + Convert.ToString(propertyData.Value);
					childResult.Nodes.Add(nodeValue);
				}
			}
			return result;
		}
	}
}