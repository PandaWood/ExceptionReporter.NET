using System.Text;

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