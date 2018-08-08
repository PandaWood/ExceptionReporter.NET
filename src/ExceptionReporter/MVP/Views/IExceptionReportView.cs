using System;
using System.Collections.Generic;
using ExceptionReporting.Network.Events;

#pragma warning disable 1591

namespace ExceptionReporting.MVP.Views
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
		void Mapi_Completed();
		void SetInProgressState();
		void PopulateExceptionTab(IList<Exception> exceptions);
		void PopulateAssembliesTab();
		void PopulateSysInfoTab();
		void SetProgressCompleteState();
		void ToggleShowFullDetail();
	}
}

#pragma warning restore 1591
