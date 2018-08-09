// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

namespace ExceptionReporting.SystemInfo
{
	internal class SysInfoQueries
	{
		public static readonly SysInfoQuery OperatingSystem = new SysInfoQuery("Operating System", "Win32_OperatingSystem", false);
		public static readonly SysInfoQuery Machine = new SysInfoQuery("Machine", "Win32_ComputerSystem", true);
	}
}