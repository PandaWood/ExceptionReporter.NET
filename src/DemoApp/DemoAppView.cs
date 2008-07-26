using System;
using System.Windows.Forms;

namespace ExceptionReporting.DemoApp
{
	public partial class DemoAppView : Form
	{
		public DemoAppView()
		{
			InitializeComponent();

			button1.Click += Throw_Click;
		}

		private static void Throw_Click(object sender, EventArgs e)
		{
			try
			{
				int? x = null;
				int y = x.Value;
			}
			catch (Exception ex)
			{
				var exceptionReporter = new ExceptionReporter();
				exceptionReporter.ReadConfig();
				exceptionReporter.DisplayException(ex);
			}
		}
	}
}