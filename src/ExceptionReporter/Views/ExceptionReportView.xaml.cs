using System;
using System.Collections.Generic;
using System.Windows.Controls;
using ExceptionReporter.Core;

namespace ExceptionReporter.Views
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
	public partial class ExceptionReportView : UserControl, IExceptionReportView
	{
		private ExceptionReportPresenter _presenter;

		public ExceptionReportView(ExceptionReportInfo reportInfo)
		{
			InitializeComponent();

			_presenter = new ExceptionReportPresenter(this, reportInfo)
			{
//				Clipboard = new WindowsClipboard()
			};
		}

		public string ProgressMessage
		{
			set { throw new NotImplementedException(); }
		}

		public bool EnableEmailButton
		{
			set { throw new NotImplementedException(); }
		}

		public bool ShowProgressBar
		{
			set { throw new NotImplementedException(); }
		}

		public bool ShowFullDetail
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string UserExplanation
		{
			get { throw new NotImplementedException(); }
		}

		public void ShowErrorDialog(string message, Exception exception)
		{
			throw new NotImplementedException();
		}

		public void SetEmailCompletedState(bool success)
		{
			throw new NotImplementedException();
		}

		public void SetEmailCompletedState_WithMessageIfSuccess(bool success, string successMessage)
		{
			throw new NotImplementedException();
		}

		public void SetInProgressState()
		{
			throw new NotImplementedException();
		}

		public void PopulateConfigTab(string filePath)
		{
			throw new NotImplementedException();
		}

		public void PopulateExceptionTab(IList<Exception> exceptions)
		{
			throw new NotImplementedException();
		}

		public void PopulateAssembliesTab()
		{
			throw new NotImplementedException();
		}

		public void PopulateSysInfoTab()
		{
			throw new NotImplementedException();
		}

		public void SetProgressCompleteState()
		{
			throw new NotImplementedException();
		}

		public void ToggleShowFullDetail()
		{
			throw new NotImplementedException();
		}

		public void ShowExceptionReport()
		{
			throw new NotImplementedException();
		}
	}
}
