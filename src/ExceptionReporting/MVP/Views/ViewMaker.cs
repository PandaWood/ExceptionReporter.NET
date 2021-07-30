using System.Windows.Forms;
using ExceptionReporting.Core;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ExceptionReporting.MVP.Views
{
	internal class ViewMaker : IViewMaker
	{
		private readonly ExceptionReportInfo _reportInfo;

		public ViewMaker(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}
		
		public IExceptionReportView Create()
		{
			return new ExceptionReportView(_reportInfo);
		}

		public void ShowError(string message, string description)
		{
			MessageBox.Show(message, description, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}