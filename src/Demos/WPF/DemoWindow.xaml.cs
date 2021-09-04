using System;
using System.Data;
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
			try
			{
				TestMethod();
			}
			catch (Exception ex)
			{
				new Window
				{
					Title = "Error Report",
					Height = 300,
					Width = 480,
					Content = new WpfExceptionReporter(ex, new ExceptionReportInfo
					{
						SendMethod = ReportSendMethod.SimpleMAPI,
						EmailReportAddress = "support@amcotest.com",
						CompanyName = "Amco",
						TitleText = "Amco Error Report",
						ShowLessDetailButton = true,
						ReportTemplateFormat = TemplateFormat.Text,
					})
				}.ShowDialog();
			}
		}

		private void TestMethod()
		{
			throw new DataException("The server was unable to respond (internal error). \nError code 777999 - please contact support for assistance");
		}
	}
}
