using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.Core
{
	/// <summary>
	/// ExceptionReportGenerator gathers up all the stuff that needs to happen to generate an ExceptionReport
	/// Hence this class is also the entry point to use 'ExceptionReporter' as a general-purpose exception string
	/// maker (ie use this class to create an exception report without even needing to show a GUI/dialog)
	/// </summary>
	public class ExceptionReportGenerator : Disposable
	{
		private readonly ExceptionReportInfo _reportInfo;
		private readonly List<SysInfoResult> _sysInfoResults = new List<SysInfoResult>();

		public ExceptionReportGenerator(ExceptionReportInfo reportInfo)
		{
			if (reportInfo == null)
				throw new ExceptionReportGeneratorException("reportInfo cannot be null");

			_reportInfo = reportInfo;

			_reportInfo.ExceptionDate = DateTime.Now;
			_reportInfo.UserName = Environment.UserName;
			_reportInfo.MachineName = Environment.MachineName;
			_reportInfo.AppName = Application.ProductName;
			_reportInfo.RegionInfo = Application.CurrentCulture.DisplayName;
			_reportInfo.AppVersion = Application.ProductVersion;
			_reportInfo.AppAssembly = Assembly.GetEntryAssembly();
		}

		public ExceptionReport CreateExceptionReport()
		{
			IList<SysInfoResult> sysInfoResults = GetOrFetchSysInfoResults();
			var reportBuilder = new ExceptionReportBuilder(_reportInfo, sysInfoResults);
			return reportBuilder.Build();
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

		protected override void DisposeManagedResources()
		{
			_reportInfo.Dispose();
			base.DisposeManagedResources();
		}
	}

	public class ExceptionReportGeneratorException : Exception
	{
		public ExceptionReportGeneratorException(string message) : base(message)
		{ }
	}
}