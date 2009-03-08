using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ExceptionReporter.SystemInfo
{
    ///<summary>
    /// Map SysInfoResults to human-readable formats
    ///</summary>
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

        public string CreateStringList(IEnumerable<SysInfoResult> results)
        {
            var stringBuilder = new StringBuilder();

            foreach (SysInfoResult result in results)
            {
                stringBuilder.AppendLine(result.Name);

                foreach (string nodeValueParent in result.Nodes)
                {
                    stringBuilder.AppendLine("-" + nodeValueParent);

                    foreach (SysInfoResult childResult in result.ChildResults)
                    {
                        foreach (var nodeValue in childResult.Nodes)
                        {
                            stringBuilder.AppendLine("--" + nodeValue);		// the max no. of levels is 2, ie '--' is as deep as we go
                        }
                    }
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}