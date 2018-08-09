// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System;

namespace ExceptionReporting
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
