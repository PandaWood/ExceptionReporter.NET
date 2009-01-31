using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.Core
{
	public class ExceptionReportGenerator
	{
		private readonly ExceptionReportInfo _reportInfo;
		private readonly List<SysInfoResult> _sysInfoResults = new List<SysInfoResult>();

		public ExceptionReportGenerator(ExceptionReportInfo reportInfo)
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

		public IList<SysInfoResult> GetOrFetchSysInfoResults()
		{
			if (_sysInfoResults.Count == 0)
				_sysInfoResults.AddRange(CreateSysInfoResults());

			return _sysInfoResults.AsReadOnly();
		}

		public string CreateExceptionReport()
		{
			var stringBuilder = new ExceptionStringBuilder(_reportInfo, GetOrFetchSysInfoResults());
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