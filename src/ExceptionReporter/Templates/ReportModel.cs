using System;
using System.Collections.Generic;
using System.Globalization;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable 1591

namespace ExceptionReporting.Templates
{
	/// <summary>
	/// The top-level model/object passed to report templates
	/// </summary>
	public class ReportModel
	{
		public App App { get; set; }
		public Error Error { get; set; }
		public string SystemInfo { get; set; }
	}

	public class App
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public string Region { get; set; } = CultureInfo.CurrentCulture.DisplayName;
		
		/// <summary> optional - will not show field at all if empty </summary>
		public string User { get; set; }
		
		public IEnumerable<AssemblyRef> AssemblyRefs{ get; set; }
	}

	public class Error
	{
		//===== required variables =====
		/// <summary> The top-level exception </summary>
		public Exception Exception { get; set; }		//todo cater for multiple exceptions passed?
		
		/// <summary> DateTime of exception - defaults to now but would normally set via config </summary>
		public DateTime When { get; set; } = DateTime.Now;
		
		/// <summary> Full stack trace string, including any and all inner exception and/or multiple exceptions </summary>
		public string FullStackTrace { get; set; }
		
		/// <summary> Optional - user input </summary>
		public string Explanation { get; set; }
		//=====
		
		//===== calculated variables
		public string Message { get { return Exception.Message; } }
		
		public string Date { get { return When.ToShortDateString(); } }
		
		public string Time { get { return When.ToShortTimeString(); } }
		
		public string InnerException
		{
			get { return Exception?.InnerException?.ToString(); }
		}
		//=====
	}

	public class AssemblyRef
	{
		public string Name  { get; set; }
		public string Version { get; set; }
	}
}