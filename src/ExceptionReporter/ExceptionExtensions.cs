using System.Text;

/// a hack to ensure that we can use Extension Methods and still target .NET2
/// see http://www.danielmoth.com/Blog/2007/05/using-extension-methods-in-fx-20.html
namespace System.Runtime.CompilerServices
{
	public class ExtensionAttribute : Attribute { }
}

namespace ExceptionReporting
{
	public static class ExceptionExtensions
	{
		public static StringBuilder AppendDottedLine(this StringBuilder stringBuilder)
		{
			return stringBuilder.AppendLine("-----------------------------");
		}
	}
}