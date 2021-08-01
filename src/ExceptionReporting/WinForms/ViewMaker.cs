using System.Windows.Forms;
using ExceptionReporting.Core;
using ExceptionReporting.MVP.Views;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ExceptionReporting.WinForms
{
	/// <summary>
	/// WinForms implementation of ViewMaker 
	/// </summary>
	public class ViewMaker : IViewMaker
	{
		private readonly ExceptionReportInfo _reportInfo;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reportInfo"></param>
		public ViewMaker(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IExceptionReportView Create()
		{
			return new ExceptionReportView(_reportInfo);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="description"></param>
		public void ShowError(string message, string description)
		{
			MessageBox.Show(message, description, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}