/*
 * https://github.com/PandaWood/ExceptionReporter.NET
 */

using System.Linq;
using System.Windows.Forms;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.WinForms
{
	///<summary>
	/// WinForms extension 
	///</summary>
	// ReSharper disable once ClassNeverInstantiated.Global
	internal class SysInfoResultMapperWinForm : SysInfoResultMapper
	{
		/// <summary>
		/// Add a tree node to an existing parentNode, by passing the SysInfoResult
		/// </summary>
		public static void AddTreeViewNode(TreeNode parentNode, SysInfoResult result)
		{
			var nodeRoot = new TreeNode(result.Name);

			foreach (var nodeLeaf in result.Nodes.Select(nodeValueParent => new TreeNode(nodeValueParent)))
			{
				nodeRoot.Nodes.Add(nodeLeaf);

				foreach (var nodeValue in result.ChildResults.SelectMany(childResult => childResult.Nodes))
				{
					nodeLeaf.Nodes.Add(new TreeNode(nodeValue));
				}
			}
			parentNode.Nodes.Add(nodeRoot);
		}
	}
}