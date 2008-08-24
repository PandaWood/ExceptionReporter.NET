namespace ExceptionReporting.SystemInfo
{
	public class SysInfoQueries
	{
		public static SysInfoQuery OperatingSystem = new SysInfoQuery("Operating System", "Win32_OperatingSystem", false);
		public static SysInfoQuery Machine = new SysInfoQuery("Machine", "Win32_ComputerSystem", true);
	}
}