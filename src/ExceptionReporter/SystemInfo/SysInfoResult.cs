using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable 1591

namespace ExceptionReporting.SystemInfo
{
	/// <summary>
	/// SysInfoResult holds results from a (ultimately WMI) query into system information
	/// </summary>
	public class SysInfoResult
    {
        private readonly string _name;
        private List<string> _nodes = new List<string>();
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

        public List<string> Nodes
        {
        	get { return _nodes; }
        	set { _nodes = value; }
        }

		public string Name
        {
            get { return _name; }
        }

        public List<SysInfoResult> ChildResults
        {
            get { return _childResults; }
        }

		public SysInfoResult Filter(string[] filterStrings)
		{
			var filteredNodes = (from node in ChildResults[0].Nodes
								 from filter in filterStrings
								 where node.Contains(filter + " = ")	//TODO a little too primitive
			                     select node).ToList();

			ChildResults[0].Nodes.Clear();								//TODO doesn't seem like a good idea
			ChildResults[0].Nodes.AddRange(filteredNodes);
			return this;
		}
    }
}
#pragma warning restore 1591
