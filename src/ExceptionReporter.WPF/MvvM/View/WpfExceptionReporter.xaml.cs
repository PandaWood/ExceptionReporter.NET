using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ExceptionReporting.Mail;
using ExceptionReporting.Network;
using ExceptionReporting.Network.Events;
using ExceptionReporting.Report;
using ExceptionReporting.WPF.MvvM.ViewModel;

// ReSharper disable CheckNamespace
namespace ExceptionReporting.WPF.MvvM.View
{
	/// <summary>
	/// Interaction logic for WpfExceptionReporter
	/// </summary>
	public partial class WpfExceptionReporter : UserControl
	{
		private readonly ExceptionReportInfo _info;
		private readonly ReportGenerator _reportGenerator;

		public WpfExceptionReporter(Exception exception, ExceptionReportInfo info)
		{
			InitializeComponent();

			_info = info;
			_info.MainException = exception;
			_reportGenerator = new ReportGenerator(_info);
			this.DataContext = new ExceptionReporterViewModel(info);
		}

		private void CopyCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CopyExecute(object sender, ExecutedRoutedEventArgs e)
		{
			Clipboard.SetText(_reportGenerator.Generate());
		}

		private void EmailCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;		//TODO check MAPI compatible system?
		}

		private void EmailExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var report = _info.IsSimpleMAPI() ? new EmailReporter(_info).Create() : _reportGenerator.Generate();
			var sendFactory  = new SenderFactory(_info, new SilentSendEvent(), new NoScreenShot()).Get();
			sendFactory.Send(report);
		}
	}
}