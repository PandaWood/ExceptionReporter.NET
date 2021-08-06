using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ExceptionReporting.Network;
using ExceptionReporting.Network.Events;
using ExceptionReporting.Templates;
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

		private void CopyExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var report = _reportGenerator.Generate();
			Clipboard.SetText(report);
		}

		private void CopyCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void EmailCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;		//TODO check MAPI compatible system?
		}

		private void EmailExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var sendFactory  = new SenderFactory(_info, new SilentSendEvent()).Get();
			var report = _info.IsSimpleMAPI() ? CreateEmailReport() : _reportGenerator.Generate();
			sendFactory.Send(report);
		}

		private string CreateEmailReport()
		{
			var template = new TemplateRenderer(new EmailIntroModel
			{
				ScreenshotTaken = _info.TakeScreenshot
			});
			var emailIntro = template.RenderPreset();
			var report = _reportGenerator.Generate();

			return emailIntro + report;
		}
	}
}