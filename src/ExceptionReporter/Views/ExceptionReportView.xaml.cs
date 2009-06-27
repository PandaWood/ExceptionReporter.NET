using System;
using System.Collections.Generic;
using System.Windows;
using ExceptionReporting.Core;

#pragma warning disable 1591

namespace ExceptionReporting.Wpf.Views
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
			set {  }
		}

		public bool EnableEmailButton
		{
			set {  }
		}

		public bool ShowProgressBar
		{
			set {  }
		}

		public bool ShowFullDetail
		{
			get; set;
		}

		public string UserExplanation
		{
			get { return null;}
		}

		public void ShowErrorDialog(string message, Exception exception)
		{
			
		}

		public void SetEmailCompletedState(bool success)
		{
			
		}

		public void SetEmailCompletedState_WithMessageIfSuccess(bool success, string successMessage)
		{
			
		}

		public void SetInProgressState()
		{
			
		}

		public void PopulateConfigTab(string filePath)
		{
			
		}

		public void PopulateExceptionTab(IList<Exception> exceptions)
		{
			
		}

		public void PopulateAssembliesTab()
		{
			
		}

		public void PopulateSysInfoTab()
		{
			
		}

		public void SetProgressCompleteState()
		{
			
		}

		public void ToggleShowFullDetail()
		{
			
		}

		public void ShowExceptionReport()
		{
			Show();
		}
	}
}

#pragma warning restore 1591