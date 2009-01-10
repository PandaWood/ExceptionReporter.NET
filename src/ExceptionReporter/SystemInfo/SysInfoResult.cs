using System.Collections.Generic;

namespace ExceptionReporting.SystemInfo
{
	public class SysInfoResult
	{
		private readonly string _name;
		private readonly List<string> _nodes = new List<string>();
		private readonly IList<SysInfoResult> _childResults = new List<SysInfoResult>();

		public SysInfoResult(string name)
		{
			_name = name;
		}

		public IList<string> Nodes
		{
			get { return _nodes; }
		}

		public string Name
		{
			get { return _name; }
		}

		public IList<SysInfoResult> ChildResults
		{
			get { return _childResults; }
		}
	}
}