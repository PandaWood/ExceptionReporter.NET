using System;
using System.Management;

namespace ExceptionReporting.SystemInfo
{
	public class SysInfoRetriever
	{
		private ManagementObjectSearcher _sysInfoSearcher;
		private SysInfoResult _sysInfo;
		private SysInfoQuery _sysInfoQuery;

		public SysInfoResult Retrieve(SysInfoQuery sysInfoQuery)
		{
			_sysInfoQuery = sysInfoQuery;
			_sysInfoSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0}", _sysInfoQuery.QueryText));
			_sysInfo = new SysInfoResult(_sysInfoQuery.Name);

			foreach (ManagementObject managementObject in _sysInfoSearcher.Get())
			{
				AddPropertyValue(managementObject);
				AddChildren(managementObject);
			}
			return _sysInfo;
		}

		private void AddPropertyValue(ManagementBaseObject managementObject) 
		{
			string propertyValue = managementObject.GetPropertyValue(_sysInfoQuery.DisplayField).ToString().Trim();
			_sysInfo.Nodes.Add(propertyValue);
		}

		private void AddChildren(ManagementBaseObject managementObject) 
		{
			SysInfoResult childResult = null;
			foreach (PropertyData propertyData in managementObject.Properties)
			{
				if (childResult == null)
				{
					childResult = new SysInfoResult(_sysInfoQuery.Name + "_Child");
					_sysInfo.ChildResults.Add(childResult);
				}

				string nodeValue = string.Format("{0} = {1}", propertyData.Name, Convert.ToString(propertyData.Value));
				childResult.Nodes.Add(nodeValue);
			}
		}
	}
}