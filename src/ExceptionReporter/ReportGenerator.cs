/*
 * https://github.com/PandaWood/ExceptionReporter.NET
 */

using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Core;
using ExceptionReporting.SystemInfo;

// ReSharper disable MemberCanBePrivate.Global

#pragma warning disable 1591

namespace ExceptionReporting
{
	/// <summary>
	/// ReportGenerator is the entry point to use 'ExceptionReporter.NET' to retrieve the report/data only.
	/// ie if the user only requires the report info but has no need to use the show or send functionality available
	/// </summary>
	public class ReportGenerator : Disposable
	{
		private readonly ExceptionReportInfo _reportInfo;
		private readonly List<SysInfoResult> _sysInfoResults = new List<SysInfoResult>();

		/// <summary>
		/// Initialises some ExceptionReportInfo properties related to the application/system
		/// </summary>
		/// <param name="reportInfo">an ExceptionReportInfo, can be pre-populated with config
		/// however 'base' properties such as MachineName</param>
		public ReportGenerator(ExceptionReportInfo reportInfo)
		{
			// this is going to be a dev/learning mistake, so let them now fast and hard
			_reportInfo = reportInfo ?? throw new ReportGeneratorException("reportInfo cannot be null");

			_reportInfo.ExceptionDate = _reportInfo.ExceptionDateKind != DateTimeKind.Local ? DateTime.UtcNow : DateTime.Now;
			_reportInfo.RegionInfo = Application.CurrentCulture.DisplayName;

			_reportInfo.AppName = string.IsNullOrEmpty(_reportInfo.AppName) ? Application.ProductName : _reportInfo.AppName;
			_reportInfo.AppVersion = string.IsNullOrEmpty(_reportInfo.AppVersion) ? GetAppVersion() : _reportInfo.AppVersion;
			
			if (_reportInfo.AppAssembly == null)
				_reportInfo.AppAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
		}

		private string GetAppVersion()
		{
			return ApplicationDeployment.IsNetworkDeployed ? 
				ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : Application.ProductVersion;
		}
		
//		leave commented out for mono to toggle in/out to be able to compile
//		private string GetAppVersion()
//		{
//			return Application.ProductVersion;
//		}

		/// <summary>
		/// Generate the exception report
		/// </summary>
		/// <returns><see cref="ExceptionReport"/>object</returns>
		public ExceptionReport Generate()
		{
			var sysInfoResults = GetOrFetchSysInfoResults();
			var builder = new ReportBuilder(_reportInfo, sysInfoResults);
			return builder.Build();
		}

		/// <summary>
		/// Create an exception report
		/// NB This method re-uses the same information retrieved from the system on subsequent calls
		/// Create a new ExceptionReportGenerator if you need to refresh system information from the computer
		/// </summary>
		/// <returns></returns>
		[Obsolete("Use Generate() instead")]
		public ExceptionReport CreateExceptionReport()
		{
			return Generate();
		}

		internal IList<SysInfoResult> GetOrFetchSysInfoResults()
		{
			if (ExceptionReporter.IsRunningMono()) return new List<SysInfoResult>();
			if (_sysInfoResults.Count == 0)
				_sysInfoResults.AddRange(CreateSysInfoResults());

			return _sysInfoResults.AsReadOnly();
		}

		private static IEnumerable<SysInfoResult> CreateSysInfoResults()
		{
			var retriever = new SysInfoRetriever();
			var results = new List<SysInfoResult>
			{
			retriever.Retrieve(SysInfoQueries.OperatingSystem).Filter(
				new[]
				{
					"CodeSet", "CurrentTimeZone", "FreePhysicalMemory",
					"OSArchitecture", "OSLanguage", "Version"
				}),
			retriever.Retrieve(SysInfoQueries.Machine).Filter(
				new[]
				{
					"TotalPhysicalMemory", "Manufacturer", "Model"
				}),
			};
			return results;
		}

		/// <summary>
		/// Disposes the managed resources.
		/// </summary>
		protected override void DisposeManagedResources()
		{
			_reportInfo.Dispose();
			base.DisposeManagedResources();
		}
	}

	/// <summary>
	/// Exception report generator exception.
	/// </summary>
	public class ReportGeneratorException : Exception
	{
		public ReportGeneratorException(string message) : base(message)
		{ }
	}
}