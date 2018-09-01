using System.Windows.Forms;
using ExceptionReporting.MVP.Views;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

/// <summary>
/// contract to show view-related things
/// </summary>
public interface IViewMaker
{
	/// <summary>
	/// create the main view/dialog
	/// </summary>
	/// <returns><see cref="IExceptionReportView"/></returns>
	IExceptionReportView Create();

	/// <summary>
	/// show error 
	/// </summary>
	/// <param name="message"></param>
	/// <param name="description"></param>
	void ShowError(string message, string description);
}

namespace ExceptionReporting.MVP.Views
{
	internal class ViewMaker : IViewMaker
	{
		private readonly ExceptionReportInfo _reportInfo;

		public ViewMaker(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}
		
		/// <summary>
		/// set this to an <see cref="IExceptionReportView"/> to be used in place of the default view
		/// </summary>
		public IExceptionReportView View { get; set; }

		public IExceptionReportView Create()
		{
			return View ?? new ExceptionReportView(_reportInfo);
		}

		public void ShowError(string message, string description)
		{
			MessageBox.Show(message, description, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}