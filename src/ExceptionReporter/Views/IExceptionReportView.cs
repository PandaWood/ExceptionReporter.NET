using System;
using System.Collections.Generic;
using ExceptionReporting.Mail;

#pragma warning disable 1591

namespace ExceptionReporting.Views
{
	/// <summary>
	/// The interface (contract) for an ExceptionReportView
	/// </summary>
	public interface IExceptionReportView : IReportSendEvent
	{
		string ProgressMessage { set; }
		bool EnableEmailButton { set; }
		bool ShowProgressBar { set; }
		bool ShowFullDetail { get; set; }
		string UserExplanation { get; }
		void SetEmailCompletedState_WithMessageIfSuccess(bool success, string successMessage);
		void SetInProgressState();
		void PopulateExceptionTab(IList<Exception> exceptions);
		void PopulateAssembliesTab();
		void PopulateSysInfoTab();
		void SetProgressCompleteState();
		void ToggleShowFullDetail();
		void ShowExceptionReport();
	}
}

#pragma warning restore 1591
