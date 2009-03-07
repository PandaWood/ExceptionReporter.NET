using System;
using System.Collections.Generic;
using System.Windows;
using ExceptionReporter.Core;

#pragma warning disable 1591

namespace ExceptionReporter.Wpf.Views
{
	public class WpfClipboard : IClipboard
	{
		public void CopyTo(string text)
		{
			Clipboard.SetData(DataFormats.Text, text);
		}
	}

	/// <summary>
	/// Interaction logic for ExceptionReportView.xaml
	/// </summary>
// ReSharper disable UnusedMember.Global
	public partial class ExceptionReportView : IExceptionReportView
// ReSharper restore UnusedMember.Global
	{
		private ExceptionReportPresenter _presenter;

		public ExceptionReportView(ExceptionReportInfo reportInfo)
		{
			InitializeComponent();

			_presenter = new ExceptionReportPresenter(this, reportInfo)
			             {
			             	Clipboard = new WpfClipboard()
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

#pragma warning restore 1591