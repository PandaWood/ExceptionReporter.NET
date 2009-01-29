using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.Core
{
	//note this is a spike to determine how to get the exception report without using the form/dialog
	public class ExceptionReportController
	{
		private readonly ExceptionReportInfo _reportInfo;

		public ExceptionReportController(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;

			reportInfo.ExceptionDate = DateTime.Now;
			reportInfo.UserName = Environment.UserName;
			reportInfo.MachineName = Environment.MachineName;
			reportInfo.AppName = Application.ProductName;
			reportInfo.RegionInfo = Application.CurrentCulture.DisplayName;
			reportInfo.AppVersion = Application.ProductVersion;
			reportInfo.AppAssembly = Assembly.GetCallingAssembly();
		}

		public string CreateExceptionReport()
		{
			var stringBuilder = new ExceptionStringBuilder(_reportInfo, CreateSysInfoResults());
			return stringBuilder.Build();
		}

		private static IEnumerable<SysInfoResult> CreateSysInfoResults()
		{
			var retriever = new SysInfoRetriever();
			var results = new List<SysInfoResult>
			{
          		retriever.Retrieve(SysInfoQueries.OperatingSystem),
          		retriever.Retrieve(SysInfoQueries.Machine)
			};
			return results;
		}
	}
}