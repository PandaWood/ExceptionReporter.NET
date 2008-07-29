namespace ExceptionReporting.SystemInfo
{
	public class SysInfoQueries
	{
		public static SysInfoQuery CPU = new SysInfoQuery("CPU", "Win32_Processor", true);
		public static SysInfoQuery OperatingSystem = new SysInfoQuery("Operating System", "Win32_OperatingSystem", false);
		public static SysInfoQuery Memory = new SysInfoQuery("Memory", "Win32_PhysicalMemory", true);
		public static SysInfoQuery Environment = new SysInfoQuery("Environment", "Win32_Environment", true);
		public static SysInfoQuery System = new SysInfoQuery("System", "Win32_ComputerSystem", true);
	}
}