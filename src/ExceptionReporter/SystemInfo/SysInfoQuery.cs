namespace ExceptionReporting.SystemInfo
{
	/// <summary>
	/// 
	/// </summary>
	public class SysInfoQuery
	{
		private readonly string _name;
		private readonly bool _useNameAsDisplayField;
		private readonly string _query;

		public SysInfoQuery(string name, bool useNameAsDisplayField, string classQuery)
		{
			_name = name;
			_useNameAsDisplayField = useNameAsDisplayField;
			_query = classQuery;
		}

		public string Query
		{
			get { return _query; }
		}

		public string DisplayField
		{
			get { return _useNameAsDisplayField ? "Name" : "Caption"; }
		}

		public string Name
		{
			get { return _name; }
		}
	}
}