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

			_reportInfo.ExceptionDate = DateTime.Now;
			_reportInfo.UserName = Environment.UserName;
			_reportInfo.MachineName = Environment.MachineName;
			_reportInfo.AppName = Application.ProductName;
			_reportInfo.RegionInfo = Application.CurrentCulture.DisplayName;
			_reportInfo.AppVersion = Application.ProductVersion;
			_reportInfo.AppAssembly = Assembly.GetEntryAssembly();
		}

		public string CreateExceptionReport()
		{
			var stringBuilder = new ExceptionStringBuilder(_reportInfo, GetOrFetchSysInfoResults());
			return stringBuilder.Build();
		}

		public IList<SysInfoResult> GetOrFetchSysInfoResults()
		{
			if (_sysInfoResults.Count == 0)
				_sysInfoResults.AddRange(CreateSysInfoResults());

			return _sysInfoResults.AsReadOnly();
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