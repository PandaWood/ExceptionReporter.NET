using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Mail;
using ExceptionReporting.SystemInfo;
#pragma warning disable 1591

namespace ExceptionReporting.Core
{
	/// <summary>
	/// ExceptionReportGenerator does everything that needs to happen to generate an ExceptionReport
	/// This class is the entry point to use 'ExceptionReporter' as a general-purpose exception reporter
	/// (ie use this class to create an exception report without showing a GUI/dialog)
	/// </summary>
	public class ExceptionReportGenerator : Disposable
	{
		readonly ExceptionReportInfo _reportInfo;
		readonly List<SysInfoResult> _sysInfoResults = new List<SysInfoResult>();

		/// <summary>
		/// Initialises some ExceptionReportInfo properties related to the application/system
		/// </summary>
		/// <param name="reportInfo">an ExceptionReportInfo, can be pre-populated with config
		/// however 'base' properties such as MachineName</param>
		public ExceptionReportGenerator(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo ?? throw new ExceptionReportGeneratorException("reportInfo cannot be null");

			_reportInfo.ExceptionDate = DateTime.UtcNow;
			_reportInfo.RegionInfo = Application.CurrentCulture.DisplayName;

			_reportInfo.AppName = string.IsNullOrEmpty(_reportInfo.AppName) ? Application.ProductName : _reportInfo.AppName;
			_reportInfo.AppVersion = string.IsNullOrEmpty(_reportInfo.AppVersion) ? Application.ProductVersion : _reportInfo.AppVersion;

			if (_reportInfo.AppAssembly == null)
				_reportInfo.AppAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
		}

		/// <summary>
		/// Create an exception report
		/// NB This method re-uses the same information retrieved from the system on subsequent calls
		/// Create a new ExceptionReportGenerator if you need to refresh system information from the computer
		/// </summary>
		/// <returns></returns>
		public ExceptionReport CreateExceptionReport()
		{
			var sysInfoResults = GetOrFetchSysInfoResults();
			var reportBuilder = new ExceptionReportBuilder(_reportInfo, sysInfoResults);
			return reportBuilder.Build();
		}

		/// <summary>
		/// Sends the report by email (assumes SMTP - a silent/async send)
		/// <param name="reportSendEvent">Implementation of cref="IEmailSendEvent"/ to receive completed event and error object, if any</param>
		/// <returns>whether the initial mail connection setup succeeded - not mail sent - use emailSendEvent to determine send/success</returns>
		/// </summary>
		public void SendReportByEmail(IReportSendEvent reportSendEvent = null) 
		{
			var mailSender = new MailSender(_reportInfo, reportSendEvent ?? new ReportSendEvent());
			var report = CreateExceptionReport();
			var reportString = report.ToString();
			mailSender.SendSmtp(reportString);
		}

		internal IList<SysInfoResult> GetOrFetchSysInfoResults()
		{
			if (ExceptionReporter.IsRunningMono()) return new List<SysInfoResult>();
			if (_sysInfoResults.Count == 0)
				_sysInfoResults.AddRange(CreateSysInfoResults());

			return _sysInfoResults.AsReadOnly();
		}

		static IEnumerable<SysInfoResult> CreateSysInfoResults()
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
	public class ExceptionReportGeneratorException : Exception
	{
		public ExceptionReportGeneratorException(string message) : base(message)
		{ }
	}
}