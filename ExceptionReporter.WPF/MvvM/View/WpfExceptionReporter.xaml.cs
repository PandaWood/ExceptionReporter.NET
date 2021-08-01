using System;
using System.Windows.Controls;
using ExceptionReporter.WPF.MvvM.ViewModel;
using ExceptionReporting;

namespace ExceptionReporter.WPF.MvvM.View
{
	/// <summary>
	/// Interaction logic for WpfExceptionReporter
	/// </summary>
	public partial class WpfExceptionReporter : UserControl
	{
		public WpfExceptionReporter(Exception exception, ExceptionReportInfo info)
		{
			InitializeComponent();

			info.MainException = exception;
			this.DataContext = new MainViewModel(info);
		}
	}
}