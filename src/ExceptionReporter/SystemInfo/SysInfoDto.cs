using System.Collections.Generic;

namespace ExceptionReporting.SystemInfo
{
	public class SysInfoDto
	{
		private readonly IList<ManagementObjectDto> _managedObjectList = new List<ManagementObjectDto>();
		private readonly SysInfoQuery _query;

		public SysInfoDto(SysInfoQuery query)
		{
			_query = query;
		}

		public IList<ManagementObjectDto> ManagedObjectList
		{
			get { return _managedObjectList; }
		}

		public SysInfoQuery Query
		{
			get { return _query; }
		}

		public string Name
		{
			get { return _query.Name; }
		}
	}
}