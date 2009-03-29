using System.Collections.Generic;

#pragma warning disable 1591

namespace ExceptionReporting.SystemInfo
{
	/// <summary>
	/// SysInfoResult holds results from a (ultimately WMI) query into system information
	/// </summary>
	public class SysInfoResult
    {
        private readonly string _name;
        private readonly List<string> _nodes = new List<string>();
        private readonly List<SysInfoResult> _childResults = new List<SysInfoResult>();

        public SysInfoResult(string name)
        {
            _name = name;
        }

		public void AddNode(string node)
		{
			_nodes.Add(node);
		}

		public void AddChildren(IEnumerable<SysInfoResult> children)
		{
			ChildResults.AddRange(children);
		}

        public IList<string> Nodes
        {
            get { return _nodes; }
        }

        public string Name
        {
            get { return _name; }
        }

        public List<SysInfoResult> ChildResults
        {
            get { return _childResults; }
        }
    }
}
#pragma warning restore 1591
