using System;
using System.Windows;
using ExceptionReporting;
using ExceptionReporting.WPF.MvvM.View;

namespace Demo.WPF
{
	/// <summary>
	/// Interaction logic for DemoWindow.xaml
	/// </summary>
	public partial class DemoWindow : Window
	{
		public DemoWindow()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			new Window
			{
				Title = "Error Report",
				Height = 250,
				Width = 400,
				Content = new WpfExceptionReporter(new Exception("WPF confluxion error"), new ExceptionReportInfo
				{
					SendMethod = ReportSendMethod.SimpleMAPI,
					EmailReportAddress = "support@acme.com",
					CompanyName = "Acme",
					TitleText = "Acme Error Report",
					ShowLessDetailButton = true,
					ReportTemplateFormat = TemplateFormat.Text,
				})
			}.ShowDialog();
		}
	}
}
