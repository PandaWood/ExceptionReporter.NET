using System.Windows.Forms;
using ExceptionReporting.Core;
using ExceptionReporting.MVP.Views;
using ExceptionReporting.Properties;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ExceptionReporting.WinForms
{
	/// <summary>
	/// Default/WinForms implementation of IViewmaker 
	/// </summary>
	internal class DefaultWinFormsViewmaker : IViewMaker
	{
		private readonly ExceptionReportInfo _reportInfo;

		public DefaultWinFormsViewmaker(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}
		
		public IExceptionReportView Create()
		{
			return new ExceptionReportView(_reportInfo);
		}

		public void ShowError(string message)
		{
			MessageBox.Show(message, Resources.Failed_trying_to_report_an_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}