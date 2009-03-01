using System;
using System.Management;

namespace ExceptionReporter.SystemInfo
{
	/// <summary>
	/// Retrieves system information using WMI
	/// </summary>
    public class SysInfoRetriever
    {
        private ManagementObjectSearcher _sysInfoSearcher;
        private SysInfoResult _sysInfoResult;
        private SysInfoQuery _sysInfoQuery;

		/// <summary>
		/// Retrieve system information, using the given SysInfoQuery to determine what information to retrieve
		/// </summary>
		/// <param name="sysInfoQuery">the query to determine what information to retrieve</param>
		/// <returns>a SysInfoResult containing the results</returns>
        public SysInfoResult Retrieve(SysInfoQuery sysInfoQuery)
        {
            _sysInfoQuery = sysInfoQuery;
            _sysInfoSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0}", _sysInfoQuery.QueryText));
            _sysInfoResult = new SysInfoResult(_sysInfoQuery.Name);

            foreach (ManagementObject managementObject in _sysInfoSearcher.Get())
            {
                AddPropertyValue(managementObject);
                AddChildren(managementObject);
            }
            return _sysInfoResult;
        }

        private void AddPropertyValue(ManagementBaseObject managementObject) 
        {
            string propertyValue = managementObject.GetPropertyValue(_sysInfoQuery.DisplayField).ToString().Trim();
            _sysInfoResult.Nodes.Add(propertyValue);
        }

        private void AddChildren(ManagementBaseObject managementObject) 
        {
            SysInfoResult childResult = null;
            foreach (PropertyData propertyData in managementObject.Properties)
            {
                if (childResult == null)
                {
                    childResult = new SysInfoResult(_sysInfoQuery.Name + "_Child");
                    _sysInfoResult.ChildResults.Add(childResult);
                }

                string nodeValue = string.Format("{0} = {1}", propertyData.Name, Convert.ToString(propertyData.Value));
                childResult.Nodes.Add(nodeValue);
            }
        }
    }
}