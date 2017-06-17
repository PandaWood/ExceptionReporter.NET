using System;
namespace ExceptionReporting.Core
{
	/// <summary>
	/// Email send event.
	/// </summary>
	public interface IEmailSendEvent
	{
		/// <summary>
		/// email send completed
		/// </summary>
		/// <param name="success">If set to <c>true</c> success.</param>
		void Completed(bool success);

		/// <summary>
		/// Shows an error - only if occurred
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="exception">Exception.</param>
		void ShowError(string message, Exception exception);
	}
}
