using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ExceptionReporting.WPF.MvvM.ViewModel;

// ReSharper disable once CheckNamespace
namespace ExceptionReporting.WPF.MvvM.View
{
	/// <summary>
	/// Interaction logic for WpfExceptionReporter
	/// </summary>
	public partial class WpfExceptionReporter : UserControl
	{
		private readonly ExceptionReportInfo _info;

		public WpfExceptionReporter(Exception exception, ExceptionReportInfo info)
		{
			_info = info;
			InitializeComponent();

			info.MainException = exception;
			this.DataContext = new MainViewModel(info);
		}

		private void CopyExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var report = new ReportGenerator(_info).Generate();
			Clipboard.SetText(report);
		}

		private void CopyCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
	}
}