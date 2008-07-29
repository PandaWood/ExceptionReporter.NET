using System;
using System.Management;

namespace ExceptionReporting.SystemInfo
{
	public class SystemInfoRetriever
	{
		public SysInfoDto GetSysInfo(SysInfoDto sysInfoDto)
		{
			var objectSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0}", sysInfoDto.Query.Query));

			foreach (ManagementObject mgtObject in objectSearcher.Get())
			{
				string propertyValue = mgtObject.GetPropertyValue(sysInfoDto.Query.DisplayField).ToString().Trim();
				var dto = new ManagementObjectDto(propertyValue);
				sysInfoDto.ManagedObjectList.Add(dto);

				foreach (PropertyData propertyData in mgtObject.Properties)
				{
					string propertyKey = propertyData.Name + ':' + Convert.ToString(propertyData.Value);
					dto.PropertyKeys.Add(propertyKey);
				}
			}
			return sysInfoDto;
		}
	}
}