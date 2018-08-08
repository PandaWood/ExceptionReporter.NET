/*
 * https://github.com/PandaWood/ExceptionReporter.NET
 */

using System.Collections.Generic;
using System.Text;
using ExceptionReporting.SystemInfo;

// ReSharper disable ConvertToAutoProperty
// ReSharper disable UnusedMember.Global

namespace ExceptionReporting
{
	/// <summary>
	/// Encapsulates the concept of an ExceptionReport
	/// </summary>
	public class ExceptionReport
	{
		private readonly StringBuilder _reportString;
		private readonly ExceptionReportInfo _reportInfo;
		private readonly IList<SysInfoResult> _sysInfoResults;

		/// <summary>
		/// Construct an ExceptionReport from a StringBuilder
		/// </summary>
		public ExceptionReport(StringBuilder stringBuilder, ExceptionReportInfo reportInfo, IList<SysInfoResult> sysInfoResults)
		{
			_reportString = stringBuilder;
			_reportInfo = reportInfo;
			_sysInfoResults = sysInfoResults;
		}

		/// <returns>
		/// The report as a string
		/// </returns>
		public override string ToString()
		{
			return _reportString.ToString();
		}

		/// <summary>
		/// Gets the report info/config
		/// </summary>
		public ExceptionReportInfo ReportInfo
		{
			get { return _reportInfo; }
		}

		/// <summary>
		/// Gets the sys info results
		/// </summary>
		public IList<SysInfoResult> SysInfoResults
		{
			get { return _sysInfoResults; }
		}

		private bool Equals(ExceptionReport obj)
		{
			return Equals(obj._reportString.ToString(), _reportString.ToString());
		}

		/// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
		/// <see cref="T:ExceptionReporting.ExceptionReport"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			return Equals((ExceptionReport)obj);
		}
			
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures </returns>
		public override int GetHashCode()
		{
			return _reportString.GetHashCode();
		}
	}
}