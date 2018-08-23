/*
 * https://github.com/PandaWood/ExceptionReporter.NET
 */

using System;
using System.Reflection;
using ExceptionReporting.MVP.Views;
using ExceptionReporting.Network;
using ExceptionReporting.Network.Events;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace ExceptionReporting
{
	/// <summary>
	/// The entry-point (class) to invoking an ExceptionReporter dialog
	/// eg new ExceptionReporter().Show(exceptions)
	/// </summary>
	public class ExceptionReporter
	{
		private readonly ExceptionReportInfo _reportInfo;
		
		/// <summary>
		/// Contract by which to show any dialogs/view
		/// </summary>
		public IViewMaker ViewMaker { get; set; }
		
		/// <summary>
		/// Initialise the ExceptionReporter
		/// </summary>
		public ExceptionReporter()
		{
			_reportInfo = new ExceptionReportInfo
			{
				AppAssembly = Assembly.GetCallingAssembly()
			};
			ViewMaker = new ViewMaker(_reportInfo);
		}

		// One issue we have with Config property here is that we store the exception and other info on it as well
		// This prevents us from allowing code like this new ExceptionReporter { Config = new ExceptionReportInfo { A = 1 } } 
		// which I would much prefer
		// TODO eventually allow this code above  
		
		/// <summary>
		/// Public access to configuration/settings
		/// </summary>
		public ExceptionReportInfo Config
		{
			get { return _reportInfo; }
		}

		/// <summary>
		/// Show the ExceptionReport dialog
		/// </summary>
		/// <remarks>The <see cref="ExceptionReporter"/> will analyze the <see cref="Exception"/>s and 
		/// create and show the report dialog.</remarks>
		/// <param name="exceptions">The <see cref="Exception"/>s to show.</param>
		public bool Show(params Exception[] exceptions)
		{
			// silently ignore the mistake of passing null
			if (exceptions == null || exceptions.Length >= 1 && exceptions[0] == null) return false;		
			
			bool status;

			try
			{
				_reportInfo.SetExceptions(exceptions);
				
				var view = ViewMaker.Create();
				view.ShowWindow();
				status = true;
			}
			catch (Exception ex)
			{
				status = false;
				ViewMaker.ShowError(ex.Message, "Failed trying to report an Error");
			}

			return status;
		}

		/// <summary>
		/// Show the ExceptionReport dialog with a custom message instead of the Exception's Message property
		/// </summary>
		/// <param name="customMessage">custom (exception) message</param>
		/// <param name="exceptions">The exception/s to display in the exception report</param>
		public void Show(string customMessage, params Exception[] exceptions)
		{
			_reportInfo.CustomMessage = customMessage;
			Show(exceptions);
		}

		/// <summary>
		/// Send the report, asynchronously, without showing a dialog (silent send)
		/// <see cref="ExceptionReportInfo.SendMethod"/>must be SMTP or WebService, else this is ignored (silently)
		/// </summary>
		/// <param name="sendEvent">Provide implementation of IReportSendEvent to receive error/updates on calling thread</param>
		/// <param name="exceptions">The exception/s to include in the report</param>
		public void Send(IReportSendEvent sendEvent = null, params Exception[] exceptions)
		{
			_reportInfo.SetExceptions(exceptions);
			
			var sender = new SenderFactory(_reportInfo, sendEvent ?? new SilentSendEvent()).Get();
			var report = new ReportGenerator(_reportInfo);
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

		static readonly bool _isRunningMono = System.Type.GetType("Mono.Runtime") != null;

		/// <returns><c>true</c>, if running mono <c>false</c> otherwise.</returns>
		public static bool IsRunningMono() { return _isRunningMono; }
		
		/// <returns><c>true</c>, if not running mono <c>false</c> otherwise.</returns>
		public static bool NotRunningMono() { return !_isRunningMono; }
	}
}

// ReSharper restore UnusedMember.Global
