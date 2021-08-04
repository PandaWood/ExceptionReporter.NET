using System;
using System.Windows;
using ExceptionReporting;
using ExceptionReporting.WPF.MvvM.View;

namespace Demo.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			var config = new ExceptionReportInfo
			{
				SendMethod = ReportSendMethod.SimpleMAPI,
				EmailReportAddress = "support@acme.com",
				CompanyName = "Acme",
				TitleText = "Acme Error Report",
				ShowLessDetailButton = true,
				ReportTemplateFormat = TemplateFormat.Text,
			};

			var window = new Window
			{
				Title = "Error Report",
				Height = 250,
				Width = 400,
				Content = new WpfExceptionReporter(new Exception("WPF confluxion error"), config)
			};

			window.ShowDialog();
		}
	}
}
