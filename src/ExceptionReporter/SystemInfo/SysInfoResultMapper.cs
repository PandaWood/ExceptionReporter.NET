using System.Windows.Forms;

namespace ExceptionReporting.SystemInfo
{
	public class SysInfoResultMapper
	{
		public void AddTreeViewNode(TreeNode parentNode, SysInfoResult result)
		{
			var nodeRoot = new TreeNode(result.Name);

			foreach (string nodeValueParent in result.Nodes)
			{
				var nodeLeaf = new TreeNode(nodeValueParent);
				nodeRoot.Nodes.Add(nodeLeaf);

				foreach (SysInfoResult childResult in result.ChildResults)
				{
					foreach (var nodeValue in childResult.Nodes)
					{
						nodeLeaf.Nodes.Add(new TreeNode(nodeValue));
					}
				}
			}
			parentNode.Nodes.Add(nodeRoot);
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}