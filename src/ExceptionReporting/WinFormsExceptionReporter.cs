using System;
using System.Diagnostics;
using ExceptionReporting.WinForms;

// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertToAutoPropertyWhenPossible
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global

namespace ExceptionReporting
{
	/// <summary>
	/// The entry-point (class) to invoking a (WinForms) ExceptionReporter dialog
	/// eg new ExceptionReporter().Show(exceptions)
	/// </summary>
	public class ExceptionReporter : ExceptionReporterBase
	{
		/// <summary>
		/// Initialise the ExceptionReporter
		/// </summary>
		public ExceptionReporter()
		{
			ViewMaker = new ViewMaker(_info);
		}

		/// <summary>
		/// Contract by which to show any dialogs/view
		/// </summary>
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public IViewMaker ViewMaker { get; set; }

		/// <summary>
		/// Show the ExceptionReport dialog
		/// </summary>
		/// <remarks>The <see cref="ExceptionReporter"/> will analyze the <see cref="Exception"/>s and 
		/// create and show the report dialog.</remarks>
		/// <param name="exceptions">The <see cref="Exception"/>s to show.</param>
		public bool Show(params Exception[] exceptions)
		{
			// silently ignore the mistake of passing null
			if (exceptions == null || exceptions.Length == 0 || exceptions.Length >= 1 && exceptions[0] == null) return false;
			if (ViewMaker == null)
			{
				Debug.WriteLine("ViewMaker must be initialized. Add `er.ViewMaker = new ViewMaker(er.Config);` where 'er'` is ExceptionReporter object");
				return false;
			}

			try
			{
				_info.SetExceptions(exceptions);

				var view = ViewMaker.Create();
				view.ShowWindow();
				return true;
			}
			catch (Exception ex)
			{
				//TODO figure out shared resources between WinForms and WPF or move messages out of common code
				// ViewMaker.ShowError(ex.Message, Resources.Failed_trying_to_report_an_Error);
				ViewMaker.ShowError(ex.Message, "Failed_trying_to_report_an_Error");
				return false;
			}
		}

		/// <summary>
		/// Show the ExceptionReport dialog with a custom message instead of the Exception's Message property
		/// </summary>
		/// <param name="customMessage">custom (exception) message</param>
		/// <param name="exceptions">The exception/s to display in the exception report</param>
		public void Show(string customMessage, params Exception[] exceptions)
		{
			_info.CustomMessage = customMessage;
			Show(exceptions);
		}
	}
}
