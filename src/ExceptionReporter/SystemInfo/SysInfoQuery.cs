using System.Collections.Generic;

namespace ExceptionReporting.SystemInfo
{
	/// <summary>
	/// 
	/// </summary>
	public class SysInfoQuery
	{
		private readonly string _name;
		private readonly bool _useNameAsDisplayField;
		private readonly string _queryText;
		private readonly IList<SysInfoResult> _sysInfoResults = new List<SysInfoResult>();

		public SysInfoQuery(string name, string query, bool useNameAsDisplayField)
		{
			_name = name;
			_useNameAsDisplayField = useNameAsDisplayField;
			_queryText = query;
		}

		public string QueryText
		{
			get { return _queryText; }
		}

		public string DisplayField
		{
			get { return _useNameAsDisplayField ? "Name" : "Caption"; }
		}

		public string Name
		{
			get { return _name; }
		}

		public IList<SysInfoResult> SysInfoResults
		{
			get { return _sysInfoResults; }
		}
	}
}