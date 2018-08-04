using System;

namespace ExceptionReporting.Mail
{
	/// <summary>
	/// Email send event.
	/// </summary>
	public interface IReportSendEvent
	{
		/// <summary>
		/// send completed
		/// </summary>
		void Completed(bool success);

		/// <summary>
		/// show an error
		/// </summary>
		void ShowError(string message, Exception exception);
	}
}
