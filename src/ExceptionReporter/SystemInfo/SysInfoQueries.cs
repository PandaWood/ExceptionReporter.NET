namespace ExceptionReporting.SystemInfo
{
	public class SysInfoQueries
	{
		public static SysInfoQuery CpuStrategy = new SysInfoQuery("CPU", true, "Win32_Processor");
		public static SysInfoQuery OsStrategy = new SysInfoQuery("Operating System", false, "Win32_OperatingSystem");
		public static SysInfoQuery Memory = new SysInfoQuery("Memory", true, "Win32_PhysicalMemory");
		public static SysInfoQuery Environment = new SysInfoQuery("Environment", true, "Win32_Environment");
		public static SysInfoQuery System = new SysInfoQuery("System", true, "Win32_ComputerSystem");
	}
}