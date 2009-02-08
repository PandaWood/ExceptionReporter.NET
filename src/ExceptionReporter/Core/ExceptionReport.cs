using System.Text;

namespace ExceptionReporting.Core
{
	/// <summary>
	/// Encapsulates the concept of an ExceptionReport, although just a wrapper around a StringBuilder, at the moment
	/// </summary>
	public class ExceptionReport
	{
		private readonly StringBuilder _reportString;

		public ExceptionReport(StringBuilder stringBuilder)
		{
			_reportString = stringBuilder;
		}

		public override string ToString()
		{
			return _reportString.ToString();
		}

		public bool Equals(ExceptionReport obj)
		{
			return Equals(obj._reportString.ToString(), _reportString.ToString());
		}

		public override bool Equals(object obj)
		{
			return Equals((ExceptionReport) obj);
		}

		public override int GetHashCode()
		{
			return _reportString.GetHashCode();
		}
	}
}