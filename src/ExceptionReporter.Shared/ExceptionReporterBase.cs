/*
 * https://github.com/PandaWood/ExceptionReporter.NET
 */

using System;
using System.Runtime.CompilerServices;
using ExceptionReporting.Network;
using ExceptionReporting.Network.Events;

// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertToAutoPropertyWhenPossible
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global

[assembly: InternalsVisibleTo("Tests.ExceptionReporter.NET")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]	// for moq

namespace ExceptionReporting
{
	/// <summary>
	/// The entry-point (class) to invoking either a WinForms dialog or sending
	/// eg new ExceptionReporter().Show(exceptions)
	/// </summary>
	public class ExceptionReporterBase
	{
		/// <summary>
		/// 
		/// </summary>
		protected readonly ExceptionReportInfo _info;
		
		/// <summary>
		/// Initialise the ExceptionReporter
		/// </summary>
		public ExceptionReporterBase()
		{
			_info = new ExceptionReportInfo();
		}

		/// <summary>
		/// Public access to configuration/settings
		/// </summary>
		public ExceptionReportInfo Config => _info;

		/// <summary>
		/// Send the report, asynchronously, without showing a dialog (silent send)
		/// <see cref="ExceptionReportInfo.SendMethod"/>must be SMTP or WebService, else this is ignored (silently)
		/// </summary>
		/// <param name="sendEvent">Provide implementation of IReportSendEvent to receive error/updates on calling thread</param>
		/// <param name="exceptions">The exception/s to include in the report</param>
		public void Send(IReportSendEvent sendEvent = null, params Exception[] exceptions)
		{
			_info.SetExceptions(exceptions);
			
			var sender = new SenderFactory(_info, sendEvent ?? new SilentSendEvent()).Get();
			var report = new ReportGenerator(_info);
			sender.Send(report.Generate());
		}

		/// <summary>
		/// Send the report, asynchronously, without showing a dialog (silent send)
		/// <see cref="ExceptionReportInfo.SendMethod"/>must be SMTP or WebService, else this is ignored (silently)
		/// </summary>
		/// <param name="exceptions">The exception/s to include in the report</param>
		public void Send(params Exception[] exceptions)
		{
			Send(new SilentSendEvent(), exceptions);
		}
	}
}

// ReSharper restore UnusedMember.Global
