// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System;
using ExceptionReporting.Core;

namespace ExceptionReporting.Mail
{
	/// <summary>
	/// Email send event - default implementation
	/// Override this to create your own or implement the IEmailSendEvent with your own class
	/// </summary>
	public class ConsoleSendEvent : IReportSendEvent
	{
		/// <summary>
		/// email send completed
		/// </summary>
		public virtual void Completed(bool success)
		{
			Console.WriteLine("Report sent: " + success);
		}

		/// <summary>
		/// Shows an error - only if occurred
		/// </summary>
		public virtual void ShowError(string message, Exception exception)
		{
			Console.WriteLine("Report error: " + message + Environment.NewLine + exception);
		}
	}
}
